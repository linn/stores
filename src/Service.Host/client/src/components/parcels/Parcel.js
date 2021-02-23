import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Tooltip from '@material-ui/core/Tooltip';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SearchInputField,
    SnackbarMessage,
    Typeahead
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';

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
    privileges,
    suppliersSearchResults,
    suppliersSearchLoading,
    searchSuppliers,
    clearSuppliersSearch,
    carriersSearchResults,
    carriersSearchLoading,
    searchCarriers,
    clearCarriersSearch
}) {
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const [parcel, setParcel] = useState(
        creating()
            ? {
                  parcelNumber: '',
                  supplierId: '',
                  supplierName: '',
                  supplierCountry: '',
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

    useEffect(() => {
        if (item !== prevParcel) {
            setParcel(item);
            setPrevParcel(item);
        }
    }, [item, prevParcel]);

    const useStyles = makeStyles(theme => ({
        marginTop2: {
            marginTop: theme.spacing(2)
        },
        marginTop3: {
            marginTop: theme.spacing(3)
        },
        displayInline: {
            display: 'inline-block'
        }
    }));
    const classes = useStyles();

    const handleSaveClick = () => {
        if (creating()) {
            addItem(parcel);
        } else {
            updateItem(itemId, parcel);
        }
        setEditStatus('view');
    };

    const handleCancelClick = () => {
        setParcel(item);
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

    const handleSupplierChange = supplier => {
        handleFieldChange('supplierId', supplier.Id);
        handleFieldChange('supplierName', supplier.description);
        handleFieldChange('supplierCountry', supplier.country);
    };

    const handleCarrierChange = carrier => {
        handleFieldChange('carrierId', carrier.Id);
        handleFieldChange('carrierName', carrier.description);
    };

    const clearSupplier = () => {
        handleFieldChange('supplierId', '');
        handleFieldChange('supplierName', '');
        handleFieldChange('supplierCountry', '');
    };

    const clearCarrier = () => {
        handleFieldChange('carrierId', '');
        handleFieldChange('carrierName', '');
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
                            <Grid item xs={3} />

                            <Grid item xs={3}>
                                <SearchInputField
                                    label="Date Created"
                                    fullWidth
                                    onChange={handleFieldChange}
                                    propertyName="dateCreated"
                                    type="date"
                                    value={parcel.dateCreated}
                                    required
                                />
                            </Grid>

                            <Grid item xs={3} />

                            <Grid item xs={1}>
                                <div className={classes.displayInline}>
                                    <Typeahead
                                        label="Supplier"
                                        title="Search for a supplier"
                                        onSelect={handleSupplierChange}
                                        items={suppliersSearchResults}
                                        loading={suppliersSearchLoading}
                                        fetchItems={searchSuppliers}
                                        clearSearch={() => clearSuppliersSearch}
                                        value={parcel.supplier}
                                        modal
                                        links={false}
                                        history={history}
                                        debounce={1000}
                                        minimumSearchTermLength={2}
                                    />
                                </div>
                                <div className={classes.marginTop1}>
                                    <Tooltip title="Clear Supplier search">
                                        <Button variant="outlined" onClick={clearSupplier}>
                                            X
                                        </Button>
                                    </Tooltip>
                                </div>
                            </Grid>
                            <Grid item xs={2} />

                            <Grid item xs={3}>
                                <div className={classes.displayInline}>
                                    <Typeahead
                                        label="Carrier"
                                        title="Search for a Carrier"
                                        onSelect={handleCarrierChange}
                                        items={carriersSearchResults}
                                        loading={carriersSearchLoading}
                                        fetchItems={searchCarriers}
                                        clearSearch={() => clearCarriersSearch}
                                        value={parcel.carrier}
                                        modal
                                        links={false}
                                        history={history}
                                        debounce={1000}
                                        minimumSearchTermLength={2}
                                    />
                                </div>
                                <div className={classes.marginTop1}>
                                    <Tooltip title="Clear Carrier search">
                                        <Button variant="outlined" onClick={clearCarrier}>
                                            X
                                        </Button>
                                    </Tooltip>
                                </div>
                            </Grid>

                            <Grid item xs={2} />

                            <Grid item xs={3}>
                                <InputField
                                    fullWidth
                                    value={parcel.supplierInvoiceNo}
                                    label="Supplier Invoice Number(s)"
                                    maxLength={500}
                                    onChange={handleFieldChange}
                                    propertyName="supplierInvoiceNo"
                                />
                            </Grid>

                            <Grid item xs={3}>
                                <InputField
                                    fullWidth
                                    value={parcel.consignmentNo}
                                    label="Consignment Number"
                                    maxLength={20}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="consignmentNo"
                                />
                            </Grid>

                            <Grid item xs={3}>
                                <InputField
                                    fullWidth
                                    value={parcel.cartonCount}
                                    label="Number of cartons"
                                    maxLength={6}
                                    onChange={handleFieldChange}
                                    propertyName="cartonCount"
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <InputField
                                    fullWidth
                                    value={parcel.palletCount}
                                    label="Number of pallets"
                                    maxLength={6}
                                    onChange={handleFieldChange}
                                    propertyName="palletCount"
                                />
                            </Grid>

                            <Grid item xs={3}>
                                <InputField
                                    fullWidth
                                    value={parcel.weight}
                                    label="Weight"
                                    maxLength={12}
                                    onChange={handleFieldChange}
                                    propertyName="weight"
                                    decimalPlaces={2}
                                />
                            </Grid>

                            <Grid item xs={3}>
                                <SearchInputField
                                    label="Date Received"
                                    fullWidth
                                    onChange={handleFieldChange}
                                    propertyName="dateCreated"
                                    type="date"
                                    value={parcel.dateCreated}
                                />
                            </Grid>

                            <Grid item xs={12}>
                                <InputField
                                    fullWidth
                                    value={parcel.comments}
                                    label="Comments"
                                    maxLength={2000}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="comments"
                                    rows={3}
                                />
                            </Grid>

                            <Grid item xs={1}>
                                <Typeahead
                                    label="Checked By"
                                    title="Search for an employee"
                                    onSelect={handleCheckedByChange}
                                    items={employeesSearchResults}
                                    loading={employeesSearchLoading}
                                    fetchItems={searchEmployees}
                                    clearSearch={() => clearEmployeesSearch}
                                    value={parcel.checkedById}
                                    modal
                                    links={false}
                                    history={history}
                                    debounce={1000}
                                    minimumSearchTermLength={2}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <div className={classes.displayInline}>
                                    <InputField
                                        value={parcel.carrierId}
                                        label="Carrier"
                                        propertyName="checkedBy"
                                        required
                                        disabled
                                    />
                                </div>
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

Parcel.propTypes = {
    item: PropTypes.shape({
        parcelNumber: PropTypes.number
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
    employees: PropTypes.arrayOf(PropTypes.shape({ name: PropTypes.string, id: PropTypes.number })),
    suppliersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.number,
            description: PropTypes.string
        })
    ),
    suppliersSearchLoading: PropTypes.bool,
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired,
    carriersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.number,
            description: PropTypes.string
        })
    ),
    carriersSearchLoading: PropTypes.bool,
    searchCarriers: PropTypes.func.isRequired,
    clearCarriersSearch: PropTypes.func.isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string)
};

Parcel.defaultProps = {
    item: {},
    snackbarVisible: false,
    loading: false,
    itemError: null,
    itemId: null,
    employees: [{ id: -1, name: 'loading..' }],
    carriersSearchResults: [{ id: -1, name: '', description: '' }],
    suppliersSearchResults: [{ id: -1, name: '', description: '' }],
    privileges: null,
    carriersSearchLoading: false,
    suppliersSearchLoading: false
};

export default Parcel;
