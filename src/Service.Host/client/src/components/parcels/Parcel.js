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
    const [parcel, setParcel] = useState(
        creating()
            ? {
                  parcelNumber: '',
                  supplierId: '',
                  supplierName: '',
                  supplierCountry: 'GB',
                  dateCreated: new Date(),
                  carrierId: 0,
                  carrierName: '',
                  supplierInvoiceNo: 0,
                  consignmentNo: 0,
                  cartonCount: 0,
                  palletCount: 0,
                  weight: 0.0,
                  dateReceived: new Date(),
                  checkedById: 0,
                  checkedByName: 0,
                  comments: ''
              }
            : null
    );
    const [prevParcel, setPrevParcel] = useState({});

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
        if (viewing()) {
            setEditStatus('edit');
        }

        setParcel({ ...parcel, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? <Title text="Create Parcel" /> : <Title text="Parcel Details" />}
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
                    parcel && (
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
                                    value={parcel.parcelNumber}
                                    label="Parcel Number"
                                    maxLength={10}
                                    helperText={!creating() ? 'This field cannot be changed' : ''}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="parcelNumber"
                                />
                            </Grid>
                            <Grid item xs={9} />
                            <Grid item xs={8}>
                                {/* <Typeahead
                                    items={searchItems}
                                    fetchItems={fetchItems}
                                    clearSearch={clearSearch}
                                    loading={loading}
                                    title="Supplier"
                                    history={history}
                                /> */}
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
