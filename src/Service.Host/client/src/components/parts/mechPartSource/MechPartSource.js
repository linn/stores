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
import QualityRequirementsTab from './tabs/QualityRequirementsTab';
import Manufacturerstab from '../../../containers/parts/mechPartSource/tabs/ManufacturersTab';
import SuppliersTab from '../../../containers/parts/mechPartSource/tabs/SuppliersTab';
import ParamDataTab from './tabs/ParamDataTab';

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
    options,
    userName,
    userNumber
}) {
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const [mechPartSource, setMechPartSource] = useState(
        creating()
            ? { proposedBy: userNumber, proposedByName: userName, dateEntered: new Date() }
            : null
    );
    const [prevMechPartSource, setPrevMechPartSource] = useState({});

    const tabDictionary = {
        proposal: 0,
        dataSheets: 1,
        qualityRequirements: 2,
        suppliers: 3,
        manufacturers: 4
    };

    const [tab, setTab] = useState(options?.tab ? tabDictionary[options?.tab] : 0);

    const handleTabChange = (_, value) => {
        setTab(value);
    };

    const mechPartSourceInvalid = () => !mechPartSource.assemblyType;

    useEffect(() => {
        if (item !== prevMechPartSource && editStatus !== 'create') {
            setMechPartSource(item);
            setPrevMechPartSource(item);
        }
    }, [item, prevMechPartSource, editStatus, itemId]);

    const handleSaveClick = () => {
        if (creating()) {
            addItem(mechPartSource);
        } else {
            updateItem(itemId, mechPartSource);
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
        setMechPartSource({ ...mechPartSource, [propertyName]: newValue });
    };

    const handlePartFieldChange = (propertyName, newValue) => {
        if (viewing) {
            setEditStatus('editing');
        }
        setMechPartSource({
            ...mechPartSource,
            part: { ...mechPartSource.part, [propertyName]: newValue }
        });
    };

    const handleParamDataFieldChange = (propertyName, newValue) => {
        console.log(propertyName, newValue);
        if (viewing) {
            setEditStatus('editing');
        }
        setMechPartSource({
            ...mechPartSource,
            part: {
                ...mechPartSource.part,
                paramData: { ...mechPartSource.part.paramData, [propertyName]: newValue }
            }
        });
    };

    const handleLinnPartChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setMechPartSource({
            ...mechPartSource,
            linnPartNumber: newValue.name,
            linnPartDescription: newValue.description
        });
    };

    const deleteManufacturersRow = row => {
        setEditStatus('edit');
        setMechPartSource(m => ({
            ...m,
            mechPartManufacturerAlts: m.mechPartManufacturerAlts.filter(
                a => a.sequence === row.sequence
            )
        }));
    };

    const deleteSuppliersRow = row => {
        setEditStatus('edit');
        setMechPartSource(m => ({
            ...m,
            mechPartAlts: m.mechPartAlts.filter(a => a.sequence === row.sequence)
        }));
    };

    const saveManufacturersRow = row => {
        setEditStatus('edit');
        // we are adding a new row
        if (!row.sequence) {
            setMechPartSource(m => ({
                ...m,
                mechPartManufacturerAlts: [
                    ...m.mechPartManufacturerAlts,
                    {
                        ...row,
                        sequence:
                            m.mechPartManufacturerAlts?.length > 0
                                ? m.mechPartManufacturerAlts.reduce((prev, current) =>
                                      prev.sequence > current.sequence ? prev : current
                                  ).sequence + 1
                                : 1
                    }
                ]
            }));
        }
        // or we are updating an existing row
        setMechPartSource(m => ({
            ...m,
            mechPartManufacturerAlts: m.mechPartManufacturerAlts.map(x =>
                x.sequence === row.sequence ? row : x
            )
        }));
    };

    const saveSuppliersRow = row => {
        setEditStatus('edit');
        // we are adding a new row
        if (!row.sequence) {
            setMechPartSource(m => ({
                ...m,
                mechPartAlts: [
                    ...m.mechPartAlts,
                    {
                        ...row,
                        sequence:
                            m.mechPartAlts?.length > 0
                                ? m.mechPartAlts.reduce((prev, current) =>
                                      prev.sequence > current.sequence ? prev : current
                                  ).sequence + 1
                                : 1
                    }
                ]
            }));
        }
        // or we are updating an existing row
        setMechPartSource(m => ({
            ...m,
            mechPartAlts: m.mechPartAlts.map(x => (x.sequence === row.sequence ? row : x))
        }));
    };

    const handleApprovedByChange = (sequence, newValue) => {
        setMechPartSource(m => ({
            ...m,
            mechPartManufacturerAlts: m.mechPartManufacturerAlts.map(x =>
                x.sequence === sequence
                    ? { ...x, approvedBy: newValue.name, approvedByName: newValue.description }
                    : x
            )
        }));
    };

    const handleSupplierChange = (sequence, newValue) => {
        setMechPartSource(m => ({
            ...m,
            mechPartAlts: m.mechPartAlts.map(x =>
                x.sequence === sequence
                    ? { ...x, supplierId: newValue.name, supplierName: newValue.description }
                    : x
            )
        }));
    };

    const handleManufacturerChange = (sequence, newValue) => {
        setMechPartSource(m => ({
            ...m,
            mechPartManufacturerAlts: m.mechPartManufacturerAlts.map(x =>
                x.sequence === sequence ? { ...x, manufacturerCode: newValue.name } : x
            )
        }));
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create Mech Part Source" />
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
                                    value={mechPartSource.part?.description}
                                    label="Description"
                                    maxLength={200}
                                    required
                                    onChange={handlePartFieldChange}
                                    propertyName="description"
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
                                <Tab label="Quality Requirements" />
                                <Tab label="Suppliers" />
                                <Tab label="Manufacturers" />
                                <Tab label="Param Data" />
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
                                    handleLinnPartChange={handleLinnPartChange}
                                />
                            )}
                            {tab === 1 && (
                                <DataSheetsTab
                                    dataSheets={mechPartSource.part?.dataSheets}
                                    handleDataSheetsChange={handleDatasheetsChange}
                                />
                            )}
                            {tab === 2 && (
                                <QualityRequirementsTab
                                    handleFieldChange={handleFieldChange}
                                    drawingsPackage={mechPartSource.drawingsPackage}
                                    drawingsPackageAvailable={
                                        mechPartSource.drawingsPackageAvailable
                                    }
                                    drawingsPackageDate={mechPartSource.drawingsPackageDate}
                                    drawingfile={mechPartSource.drawingFile}
                                    checklistCreated={mechPartSource.checklistCreated}
                                    checklistAvailable={mechPartSource.checklistAvailable}
                                    checklistDate={mechPartSource.checklistDate}
                                    packingRequired={mechPartSource.packingRequired}
                                    packingAvailable={mechPartSource.packingAvailable}
                                    packingDate={mechPartSource.packingDate}
                                    productKnowledge={mechPartSource.productKnowledge}
                                    productKnowledgeAvailable={
                                        mechPartSource.productKnowledgeAvailable
                                    }
                                    productKnowledgeDate={mechPartSource.productKnowledgeDate}
                                    testEquipment={mechPartSource.testEquipment}
                                    testEquipmentAvailable={mechPartSource.testEquipmentAvailable}
                                    testEquipmentDate={mechPartSource.testEquipmentDate}
                                    approvedReferenceStandards={
                                        mechPartSource.approvedReferenceStandards
                                    }
                                    approvedReferencesAvailable={
                                        mechPartSource.approvedReferencesAvailable
                                    }
                                    approvedReferencesDate={mechPartSource.approvedReferencesDate}
                                    processEvaluation={mechPartSource.processEvaluation}
                                    processEvaluationAvailable={
                                        mechPartSource.processEvaluationAvailable
                                    }
                                    processEvaluationDate={mechPartSource.processEvaluationDate}
                                />
                            )}
                            {tab === 3 && (
                                <SuppliersTab
                                    handleSupplierChange={handleSupplierChange}
                                    suppliers={mechPartSource.mechPartAlts}
                                    saveRow={saveSuppliersRow}
                                    deleteRow={deleteSuppliersRow}
                                />
                            )}
                            {tab === 4 && (
                                <Manufacturerstab
                                    handleApprovedByChange={handleApprovedByChange}
                                    handleManufacturerChange={handleManufacturerChange}
                                    manufacturers={mechPartSource.mechPartManufacturerAlts}
                                    saveRow={saveManufacturersRow}
                                    deleteRow={deleteManufacturersRow}
                                />
                            )}
                            {tab === 5 && (
                                <ParamDataTab
                                    paramData={mechPartSource.part.paramData}
                                    handleFieldChange={handleParamDataFieldChange}
                                />
                            )}
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
    userName: PropTypes.string,
    userNumber: PropTypes.number,
    options: PropTypes.shape({ template: PropTypes.string }),
    liveTest: PropTypes.shape({ canMakeLive: PropTypes.bool, message: PropTypes.string })
};

MechPartSource.defaultProps = {
    item: {},
    snackbarVisible: false,
    loading: null,
    itemError: null,
    itemId: null,
    options: null,
    liveTest: null,
    userName: null,
    userNumber: null
};

export default MechPartSource;
