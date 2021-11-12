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
    Typeahead,
    Dropdown,
    LinkButton
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import { Decimal } from 'decimal.js';
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
    suppliersSearchResults,
    suppliersSearchLoading,
    searchSuppliers,
    clearSuppliersSearch,
    carriersSearchResults,
    carriersSearchLoading,
    searchCarriers,
    clearCarriersSearch,
    userNumber,
    supplierId,
    comments,
    inDialogBox,
    previousPaths,
    privileges,
    closeDialog
}) {
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const [parcel, setParcel] = useState(
        creating()
            ? {
                  dateCreated: new Date().toISOString(),
                  carrierId: '',
                  supplierInvoiceNo: '',
                  consignmentNo: '',
                  cartonCount: null,
                  palletCount: null,
                  weight: 0.0,
                  dateReceived: new Date().toISOString(),
                  checkedById: userNumber,
                  comments,
                  supplierId,
                  importBookNo: null
              }
            : null
    );
    const [prevParcel, setPrevParcel] = useState(
        creating()
            ? {
                  supplierId: '',
                  dateCreated: new Date().toISOString(),
                  carrierId: '',
                  supplierInvoiceNo: '',
                  consignmentNo: '',
                  cartonCount: null,
                  palletCount: null,
                  weight: 0.0,
                  dateReceived: new Date().toISOString(),
                  checkedById: userNumber,
                  comments: '',
                  importBookNo: null,
                  dateCancelled: null
              }
            : null
    );
    const [localSuppliers, setLocalSuppliers] = useState([{}]);

    useEffect(() => {
        if (item && item !== prevParcel) {
            setParcel(item);
            setPrevParcel(item);
        }
    }, [item, prevParcel]);

    useEffect(() => {
        if (suppliers) {
            setLocalSuppliers([...suppliers]);
        }
    }, [suppliers]);

    const supplierCountryValue = () => {
        if (localSuppliers.length && parcel.supplierId) {
            const supplier = localSuppliers.find(x => x.id === parcel.supplierId);
            if (!supplier) {
                return '-';
            }
            return supplier.countryCode;
        }
        if (!parcel.supplierId) {
            return '';
        }

        return 'loading..';
    };

    const handleBackClick = () => {
        if (previousPaths?.some(p => p.path?.includes('/logistics/goods-in-utility'))) {
            history.push('/logistics/goods-in-utility');
        } else {
            history.push('/logistics/parcels');
        }
    };

    const supplierNameValue = () => {
        if (localSuppliers.length && parcel.supplierId) {
            const supplier = localSuppliers.find(x => x.id === parcel.supplierId);
            if (!supplier) {
                return 'undefined supplier';
            }
            return supplier.name;
        }
        if (!parcel.supplierId) {
            return '';
        }

        return 'loading..';
    };

    const carrierNameValue = () => {
        if (localSuppliers.length && parcel.carrierId) {
            const supplier = localSuppliers.find(x => x.id === parcel.carrierId);
            if (!supplier) {
                return 'undefined carrier';
            }
            return supplier.name;
        }
        if (!parcel.carrierId) {
            return '';
        }

        return 'loading..';
    };

    const useStyles = makeStyles(() => ({
        marginTop1: {
            marginTop: '10px',
            display: 'inline-block',
            width: '2em'
        },
        marginTopWiderLinkButton: {
            marginTop: '-14px',
            display: 'inline-block'
        },
        displayInline: {
            display: 'inline'
        },
        thinPage: {
            width: '60%',
            margin: '0 auto'
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

    const saveEnabled = () => {
        return (
            !parcel.dateCreated ||
            !parcel.dateReceived ||
            !parcel.consignmentNo ||
            !parcel.checkedById ||
            !parcel.weight ||
            new Decimal(parcel.weight).lessThanOrEqualTo(0)
        );
    };

    const handleCancelClick = () => {
        if (creating()) {
            setParcel(prevParcel);
        } else {
            setParcel(item);
            setEditStatus('view');
        }
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }

        setParcel({ ...parcel, [propertyName]: newValue });
    };

    const handleSupplierChange = supplier => {
        handleFieldChange('supplierId', supplier.id);
    };

    const handleCarrierChange = carrier => {
        handleFieldChange('carrierId', carrier.id);
    };

    const clearSupplier = () => {
        handleFieldChange('supplierId', '');
    };

    const clearCarrier = () => {
        handleFieldChange('carrierId', '');
    };

    const allowedToKill = () => {
        return privileges?.some(priv => priv === 'parcel-kill.admin');
    };

    const allowedToEdit = () => {
        return privileges?.some(priv => priv === 'parcel.admin');
    };

    const content = () => (
        <Grid container spacing={3}>
            <Grid item xs={12}>
                {creating() ? <Title text="Create Parcel" /> : <Title text="Parcel Details" />}
            </Grid>
            {itemError && (
                <Grid item xs={12}>
                    <ErrorCard errorMessage={itemError?.details?.message || itemError.statusText} />
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
                        <Grid item xs={5}>
                            {!creating() && (
                                <InputField
                                    fullWidth
                                    disabled
                                    value={parcel.parcelNumber}
                                    label="Parcel Number"
                                    maxLength={10}
                                    helperText="This field cannot be changed"
                                    required
                                    propertyName="parcelNumber"
                                />
                            )}
                        </Grid>
                        <Grid item xs={7} />

                        <Grid item xs={5}>
                            <SearchInputField
                                label="Date Created"
                                fullWidth
                                onChange={handleFieldChange}
                                propertyName="dateCreated"
                                type="date"
                                value={parcel.dateCreated}
                                required
                                disabled={!allowedToEdit()}
                            />
                        </Grid>
                        <Grid item xs={1} />

                        <Grid item xs={5}>
                            <SearchInputField
                                label="Date Received"
                                fullWidth
                                onChange={handleFieldChange}
                                propertyName="dateReceived"
                                type="date"
                                value={parcel.dateReceived}
                                required
                                disabled={!allowedToEdit()}
                            />
                        </Grid>
                        <Grid item xs={1} />

                        <Grid item xs={6}>
                            <div className={classes.displayInline}>
                                <Typeahead
                                    label="Supplier"
                                    title="Search for a supplier"
                                    onSelect={handleSupplierChange}
                                    items={suppliersSearchResults}
                                    loading={suppliersSearchLoading}
                                    propertyName="supplier"
                                    fetchItems={searchSuppliers}
                                    clearSearch={() => clearSuppliersSearch}
                                    value={`${parcel.supplierId} - ${supplierNameValue()}`}
                                    modal
                                    links={false}
                                    history={history}
                                    debounce={1000}
                                    minimumSearchTermLength={2}
                                    disabled={!allowedToEdit()}
                                />
                            </div>
                            <div className={classes.marginTop1}>
                                <Tooltip title="Clear Supplier search">
                                    <Button
                                        variant="outlined"
                                        onClick={clearSupplier}
                                        disabled={!allowedToEdit()}
                                    >
                                        X
                                    </Button>
                                </Tooltip>
                            </div>
                        </Grid>
                        <Grid item xs={3}>
                            <InputField
                                label="Supplier Country"
                                value={supplierCountryValue()}
                                disabled
                                fullwidth
                                propertyName="country"
                            />
                        </Grid>
                        <Grid item xs={3} />

                        <Grid item xs={6}>
                            <div className={classes.displayInline}>
                                <Typeahead
                                    label="Carrier"
                                    title="Search for a Carrier"
                                    onSelect={handleCarrierChange}
                                    items={carriersSearchResults}
                                    loading={carriersSearchLoading}
                                    fetchItems={searchCarriers}
                                    clearSearch={() => clearCarriersSearch}
                                    value={`${parcel.carrierId} - ${carrierNameValue()}`}
                                    modal
                                    links={false}
                                    history={history}
                                    debounce={1000}
                                    minimumSearchTermLength={2}
                                    propertyName="carrier"
                                    disabled={!allowedToEdit()}
                                />
                            </div>
                            <div className={classes.marginTop1}>
                                <Tooltip title="Clear Carrier search">
                                    <Button
                                        variant="outlined"
                                        onClick={clearCarrier}
                                        disabled={!allowedToEdit()}
                                    >
                                        X
                                    </Button>
                                </Tooltip>
                            </div>
                        </Grid>
                        <Grid item xs={6} />
                        <Grid item xs={6}>
                            <InputField
                                fullWidth
                                value={parcel.supplierInvoiceNo}
                                label="Supplier Invoice Number(s)"
                                maxLength={500}
                                onChange={handleFieldChange}
                                propertyName="supplierInvoiceNo"
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={6}>
                            <InputField
                                fullWidth
                                value={parcel.consignmentNo}
                                label="Consignment Number"
                                maxLength={20}
                                required
                                onChange={handleFieldChange}
                                propertyName="consignmentNo"
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={parcel.cartonCount}
                                label="Number of cartons"
                                maxLength={6}
                                onChange={handleFieldChange}
                                propertyName="cartonCount"
                                disabled={!allowedToEdit()}
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={parcel.palletCount}
                                label="Number of pallets"
                                maxLength={6}
                                onChange={handleFieldChange}
                                propertyName="palletCount"
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={parcel.weight}
                                label="Weight"
                                maxLength={12}
                                onChange={handleFieldChange}
                                propertyName="weight"
                                type="number"
                                decimalPlaces={2}
                                required
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={4}>
                            <Dropdown
                                items={employees.map(e => ({
                                    displayText: `${e.fullName} (${e.id})`,
                                    id: parseInt(e.id, 10)
                                }))}
                                propertyName="checkedById"
                                allowNoValue
                                fullWidth
                                value={parcel.checkedById || ''}
                                label="Checked by"
                                required
                                onChange={handleFieldChange}
                                type="number"
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <InputField
                                fullWidth
                                value={parcel.comments}
                                label="Comments"
                                maxLength={2000}
                                onChange={handleFieldChange}
                                propertyName="comments"
                                rows={3}
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={8}>
                            {parcel.importBookNos ? (
                                parcel.importBookNos.map(impNumber => {
                                    return (
                                        <>
                                            <div className={classes.displayInline}>
                                                <InputField
                                                    value={impNumber}
                                                    label="Import Book Number"
                                                    maxLength={8}
                                                    onChange={handleFieldChange}
                                                    propertyName={`importBookNo${impNumber}`}
                                                    disabled
                                                />
                                            </div>
                                            <div className={classes.marginTopWiderLinkButton}>
                                                <Tooltip title="Ctrl + click to open in new tab">
                                                    <LinkButton
                                                        text="View Impbook"
                                                        to={`/logistics/import-books/${impNumber}`}
                                                    />
                                                </Tooltip>
                                            </div>
                                        </>
                                    );
                                })
                            ) : (
                                <InputField
                                    fullWidth
                                    value="No Impbooks for parcel"
                                    propertyName="importBookNo"
                                    disabled
                                />
                            )}
                        </Grid>

                        <Grid item xs={5}>
                            <SearchInputField
                                label="Date Cancelled"
                                fullWidth
                                onChange={handleFieldChange}
                                propertyName="dateCancelled"
                                type="date"
                                value={parcel.dateCancelled}
                                disabled={!allowedToKill()}
                            />
                        </Grid>
                        <Grid item xs={7}>
                            <Dropdown
                                items={employees.map(e => ({
                                    displayText: `${e.fullName} (${e.id})`,
                                    id: parseInt(e.id, 10)
                                }))}
                                propertyName="cancelledBy"
                                allowNoValue
                                fullWidth
                                value={parcel.cancelledBy || ''}
                                label="Cancelled by"
                                onChange={handleFieldChange}
                                type="number"
                                disabled={!allowedToKill()}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <InputField
                                fullWidth
                                value={parcel.cancellationReason}
                                label="Cancellation Reason"
                                maxLength={2000}
                                onChange={handleFieldChange}
                                propertyName="cancellationReason"
                                rows={3}
                                disabled={!allowedToKill()}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <SaveBackCancelButtons
                                saveDisabled={viewing() || saveEnabled()}
                                saveClick={handleSaveClick}
                                cancelClick={handleCancelClick}
                                backClick={() => (inDialogBox ? closeDialog() : handleBackClick())}
                            />
                        </Grid>
                    </>
                )
            )}
        </Grid>
    );

    return (
        <>
            {inDialogBox ? (
                content()
            ) : (
                <div className={classes.thinPage}>
                    <Page> {content()}</Page>
                </div>
            )}
        </>
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
        details: PropTypes.shape({ errors: PropTypes.shape({}), message: PropTypes.string }),
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
            name: PropTypes.string,
            description: PropTypes.string
        })
    ),
    suppliersSearchLoading: PropTypes.bool,
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired,
    carriersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string,
            description: PropTypes.string
        })
    ),
    carriersSearchLoading: PropTypes.bool,
    searchCarriers: PropTypes.func.isRequired,
    clearCarriersSearch: PropTypes.func.isRequired,
    userNumber: PropTypes.number.isRequired,
    suppliers: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string,
            description: PropTypes.string
        })
    ),
    supplierId: PropTypes.number,
    comments: PropTypes.string,
    inDialogBox: PropTypes.bool,
    previousPaths: PropTypes.string,
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired,
    closeDialog: PropTypes.func
};

Parcel.defaultProps = {
    item: {
        parcelNumber: -1,
        supplierId: '',
        countryCode: '',
        carrierId: '',
        dateCreated: '',
        dateReceived: '',
        supplierInvoiceNo: '',
        consignmentNo: '',
        cartonCount: '',
        palletCount: '',
        weight: '',
        checkedById: '-1',
        comments: '',
        importBookNo: '',
        links: {}
    },
    snackbarVisible: false,
    loading: false,
    itemError: null,
    itemId: null,
    employees: [{ id: -1, fullName: 'loading..' }],
    carriersSearchResults: [{ id: -1, name: '', description: '' }],
    suppliersSearchResults: [{ id: -1, name: '', description: '' }],
    suppliers: [{}],
    carriersSearchLoading: false,
    suppliersSearchLoading: false,
    supplierId: '',
    comments: '',
    inDialogBox: false,
    previousPaths: '',
    closeDialog: null
};

export default Parcel;
