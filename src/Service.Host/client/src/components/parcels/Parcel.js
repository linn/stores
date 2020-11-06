import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function Parcel({
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
    employees,
    suppliers,
    carriers,
    options
}) {
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const [parcel, setPart] = useState(
        creating()
            ? {
                  partNumber: '',
                  description: '',
                  accountingCompany: 'LINN',
                  psuPart: false,
                  stockControlled: true,
                  cccCriticalPart: false,
                  safetyCriticalPart: false,
                  paretoCode: 'U',
                  createdBy: userNumber,
                  dateCreated: new Date(),
                  railMethod: 'POLICY',
                  preferredSupplier: 4415,
                  preferredSupplierName: 'Linn Products Ltd',
                  qcInformation: '',
                  qcOnReceipt: false,
                  orderHold: false
              }
            : null
    );
    const [prevPart, setPrevPart] = useState({});

    const canPhaseOut = () => {
        if (!(privileges.length < 1)) {
            return privileges.some(priv => priv === 'part.admin');
        }
        return false;
    };

    const handleSaveClick = () => {
        if (creating()) {
            addItem(parcel);
        } else {
            updateItem(itemId, parcel);
        }
        setEditStatus('view');
    };

    const handleCancelClick = () => {
        setPart(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/inventory/parcels');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing() && propertyName !== 'reasonPhasedOut') {
            setEditStatus('edit');
        }

        setPart({ ...part, [propertyName]: newValue });
    };

    const handlePhaseOutClick = () => {
        updateItem(itemId, {
            ...part,
            datePhasedOut: new Date(),
            phasedOutBy: userNumber,
            phasedOutByName: userName
        });
    };

    const handleChangeLiveness = () => {
        console.log('0');
        if (!item.dateLive) {
            console.log('1');
            updateItem(itemId, {
                ...part,
                dateLive: new Date(),
                madeLiveBy: userNumber,
                madeLiveByName: userName
            });
        } else {
            console.log('2');

            updateItem(itemId, {
                ...part,
                dateLive: null,
                madeLiveBy: null,
                madeLiveByName: null
            });
        }
    };

    const handleIgnoreWorkstationStockChange = (_, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        if (newValue === 'Yes') {
            setPart({ ...part, ignoreWorkstationStock: newValue === 'Yes' });
        } else {
            setPart({ ...part, ignoreWorkstationStock: null });
        }
    };

    const handleDepartmentChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        fetchNominal(newValue.name);
        setPart({
            ...part,
            department: newValue.name,
            departmentDescription: newValue.description
        });
    };

    const handleProductAnalysisCodeChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPart({
            ...part,
            productAnalysisCode: newValue.name,
            productAnalysisCodeDescription: newValue.description
        });
    };

    const handleSernosSequenceChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPart({
            ...part,
            sernosSequenceName: newValue.name,
            sernosSequenceDescription: newValue.description
        });
    };

    const handlePrefferedSupplierChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPart({
            ...part,
            preferredSupplier: newValue.name,
            preferredSupplierName: newValue.description
        });
    };

    const handleAccountingCompanyChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        if (newValue === 'RECORDS') {
            setPart({
                ...part,
                accountingCompany: newValue,
                paretoCode: 'R',
                bomType: 'C',
                linnProduced: 'No',
                qcOnReceipt: 'No'
            });
        } else {
            setPart({ ...part, accountingCompany: newValue, paretoCode: 'U' });
        }
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? <Title text="Create Part" /> : <Title text="Part Details" />}
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
                    part &&
                    itemError?.part !== 404 && (
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
                                    value={part.partNumber}
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
                                    value={part.description}
                                    label="Description"
                                    maxLength={200}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="description"
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing()}
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

Part.propTypes = {
    item: PropTypes.shape({
        part: PropTypes.string,
        description: PropTypes.string,
        nextSerialNumber: PropTypes.number,
        dateClosed: PropTypes.string,
        dateLive: PropTypes.string
    }),
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
    nominal: PropTypes.shape({ nominalCode: PropTypes.string, description: PropTypes.string }),
    fetchNominal: PropTypes.func.isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string),
    userName: PropTypes.string,
    userNumber: PropTypes.number,
    options: PropTypes.shape({ template: PropTypes.string }),
    partTemplates: PropTypes.arrayOf(PropTypes.shape({ partRoot: PropTypes.string })),
    liveTest: PropTypes.shape({ canMakeLive: PropTypes.bool, message: PropTypes.string }),
    fetchLiveTest: PropTypes.func.isRequired
};

Part.defaultProps = {
    item: {},
    snackbarVisible: false,
    loading: null,
    itemError: null,
    itemId: null,
    nominal: null,
    privileges: null,
    userName: null,
    userNumber: null,
    options: null,
    partTemplates: [],
    liveTest: null
};

export default Parcel;
