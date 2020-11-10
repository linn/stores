import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Page from '../../../containers/Page';
import DataSheetsTab from './tabs/DataSheetsTab';
import ProposalTab from '../../../containers/parts/mechPartSource/tabs/ProposalTab';

function MechPartSource({
    editStatus,
    itemError,
    history,
    itemId,
    item,
    loading,
    snackbarVisible,
    addItem,
    updateItem,
    setEditStatus,
    setSnackbarVisible,
    options
    // privileges,
    // userName,
    // userNumber,
    // options
}) {
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const [mechPartSource, setMechPartSource] = useState(creating() ? {} : null);
    const [prevMechPartSource, setPrevMechPartSource] = useState({});

    const tabDictionary = {
        proposal: 0,
        dataSheets: 1
    };

    const [tab, setTab] = useState(options?.tab ? tabDictionary[options?.tab] : 0);

    const handleTabChange = (event, value) => {
        setTab(value);
    };

    const mechPartSourceInvalid = () => false;

    useEffect(() => {
        if (item !== prevMechPartSource && editStatus !== 'create') {
            setMechPartSource(item);
            setPrevMechPartSource(item);
        }
    }, [item, prevMechPartSource, editStatus, itemId]);

    const handleSaveClick = () => {
        const mechPartSourceResource = mechPartSource;
        // convert Yes/No to true/false for resource to send
        // Object.keys(mechPartSourceResource).forEach(k => {
        //     if (mechPartSourceResource[k] === 'Yes' || mechPartSourceResource[k] === 'Y') {
        //         mechPartSourceResource[k] = true;
        //     }
        //     if (mechPartSourceResource[k] === 'No' || mechPartSourceResource[k] === 'N') {
        //         mechPartSourceResource[k] = false;
        //     }
        // });
        if (creating()) {
            addItem(mechPartSourceResource);
        } else {
            updateItem(itemId, mechPartSourceResource);
        }
        setEditStatus('view');
    };

    const handleCancelClick = () => {
        setMechPartSource(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/inventory/stores/parts/sources');
    };

    const handleDatasheetsChange = dataSheets => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setMechPartSource(m => ({ ...m, part: { ...m.part, dataSheets } }));
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('editing');
        }
        // if (newValue === 'Yes' || newValue === 'No') {
        //     setMechPartSource({ ...mechPartSource, [propertyName]: newValue === 'Yes' });
        // } else if (typeof newValue === 'string') {
        //     setMechPartSource({ ...mechPartSource, [propertyName]: newValue.toUpperCase() });
        // } else {
        setMechPartSource({ ...mechPartSource, [propertyName]: newValue });
        //}
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create MechPartSource" />
                    ) : (
                        <Title text="Mech Part Source Details" />
                    )}
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard
                            errorMessage={itemError?.details?.errors?.[0] || itemError.statusText}
                        />
                    </Grid>
                )}
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    mechPartSource &&
                    itemError?.mechPartSource !== 404 && (
                        <>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            <Grid item xs={3}>
                                <InputField
                                    fullWidth
                                    disabled={!creating()}
                                    value={mechPartSource.partNumber}
                                    label="Part Number"
                                    maxLength={14}
                                    helperText={!creating() ? 'This field cannot be changed' : ''}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="partNumber"
                                />
                            </Grid>
                            <Grid item xs={8}>
                                <InputField
                                    fullWidth
                                    value={mechPartSource.part.description}
                                    label="Description"
                                    maxLength={200}
                                    required
                                    onChange={handleFieldChange} // todo - how to handle change?
                                    propertyName="partDescription"
                                />
                            </Grid>
                            <Tabs
                                value={tab}
                                onChange={handleTabChange}
                                indicatorColor="primary"
                                textColor="primary"
                                style={{ paddingBottom: '40px' }}
                            >
                                <Tab label="Proposal" />
                                <Tab label="DataSheets" />
                            </Tabs>
                            {tab === 0 && (
                                <ProposalTab
                                    handleFieldChange={handleFieldChange}
                                    notes={mechPartSource.notes}
                                    proposedBy={mechPartSource.proposedBy}
                                    proposedByName={mechPartSource.proposedByName}
                                    dateEntered={mechPartSource.dateEntered}
                                    mechanicalOrElectrical={mechPartSource.mechanicalOrElectrical}
                                    safetyCritical={mechPartSource.safetyCritical}
                                    performanceCritical={mechPartSource.performanceCritical}
                                    emcCritical={mechPartSource.emcCritical}
                                    singleSource={mechPartSource.singleSource}
                                    safetyDataDirectory={mechPartSource.safetyDataDirectory}
                                    productionDate={mechPartSource.productionDate}
                                    estimatedVolume={mechPartSource.estimatedVolume}
                                    samplesRequired={mechPartSource.samplesRequired}
                                    sampleQuantity={mechPartSource.sampleQuantity}
                                    dateSamplesRequired={mechPartSource.dateSamplesRequired}
                                    rohsReplace={mechPartSource.rohsReplace}
                                    linnPartNumber={mechPartSource.linnPartNumber}
                                    linnPartDescription={mechPartSource.linnPartDescription}
                                    assemblyType={mechPartSource.assemblyType}
                                />
                            )}
                            {tab === 1 && (
                                <DataSheetsTab
                                    dataSheets={mechPartSource.part.dataSheets}
                                    handleDataSheetsChange={handleDatasheetsChange}
                                />
                            )}
                            {tab === 2 && <></>}
                            {tab === 3 && <></>}
                            {tab === 4 && <></>}
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing() || mechPartSourceInvalid()}
                                    saveClick={handleSaveClick}
                                    cancelClick={handleCancelClick}
                                    backClick={handleBackClick}
                                />
                            </Grid>
                        </>
                    )
                )}
            </Grid>
        </Page>
    );
}

MechPartSource.propTypes = {
    item: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    addItem: PropTypes.func.isRequired,
    updateItem: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    // privileges: PropTypes.arrayOf(PropTypes.string),
    // userName: PropTypes.string,
    // userNumber: PropTypes.number,
    options: PropTypes.shape({ template: PropTypes.string }),
    liveTest: PropTypes.shape({ canMakeLive: PropTypes.bool, message: PropTypes.string })
};

MechPartSource.defaultProps = {
    item: {},
    snackbarVisible: false,
    loading: null,
    itemError: null,
    itemId: null,
    // privileges: null,
    // userName: null,
    // userNumber: null,
    options: null,
    liveTest: null
};

export default MechPartSource;
