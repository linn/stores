import React, { useState, useEffect, useReducer, useCallback } from 'react';
import { Decimal } from 'decimal.js';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Dialog from '@material-ui/core/Dialog';
import Typography from '@material-ui/core/Typography';
import Page from '../../containers/Page';
import ImpBookTab from '../../containers/importBooks/tabs/ImpBookTab';
import OrderDetailsTab from '../../containers/importBooks/tabs/OrderDetailsTab';
import PostEntriesTab from './tabs/PostEntriesTab';
import CommentsTab from '../../containers/importBooks/tabs/CommentsTab';
import ImportBookReducer from './ImportBookReducer';
import ImpBookPrintOut from './ImpBookPrintOut';
import currencyConvert from '../../helpers/currencyConvert';

function ImportBook({
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
    privileges,
    allSuppliers,
    countries,
    employees,
    userNumber,
    getExchangeRatesForDate,
    exchangeRates,
    cpcNumbers
}) {
    const defaultImpBook = {
        id: -1,
        dateCreated: new Date().toISOString(),
        parcelNumber: null,
        supplierId: '',
        foreignCurrency: 'N',
        currency: 'GBP',
        carrierId: '',
        transportId: null,
        transportBillNumber: '',
        transactionId: null,
        deliveryTermCode: '',
        arrivalPort: '',
        arrivalDate: '',
        totalImportValue: null,
        weight: null,
        customsEntryCode: '',
        customsEntryCodeDate: '',
        linnDuty: null,
        linnVat: null,
        dateCancelled: '',
        cancelledBy: null,
        cancelledReason: '',
        numCartons: null,
        numPallets: null,
        comments: '',
        createdBy: userNumber,
        customsEntryCodePrefix: '',
        importBookInvoiceDetails: [],
        importBookOrderDetails: [{ lineNumber: 1 }],
        importBookPostEntries: []
    };
    const creating = () => editStatus === 'create';

    const [state, dispatch] = useReducer(ImportBookReducer, {
        impbook: creating() ? defaultImpBook : { id: '' },
        prevImpBook: { id: '' }
    });

    useEffect(() => {
        if (item && item.id !== state.prevImpBook.id) {
            if (editStatus === 'create') {
                dispatch({ type: 'initialise', payload: defaultImpBook });
            } else {
                dispatch({ type: 'initialise', payload: item });
            }
        }
    }, [item, state.prevImpBook, editStatus, defaultImpBook]);

    useEffect(() => {
        if (state.impbook?.dateCreated) {
            getExchangeRatesForDate(state.impbook.dateCreated);
        }
    }, [state.impbook.dateCreated, getExchangeRatesForDate]);

    const viewing = () => editStatus === 'view';

    const [tab, setTab] = useState(0);

    const handleTabChange = (event, value) => {
        setTab(value);
    };

    const handleCancelClick = () => {
        if (editStatus === 'create') {
            dispatch({ type: 'initialise', payload: defaultImpBook });
        } else {
            dispatch({ type: 'initialise', payload: item });
        }
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/logistics/import-books');
    };

    const handleFieldChange = useCallback(
        (propertyName, newValue) => {
            setEditStatus('edit');
            dispatch({ type: 'fieldChange', fieldName: propertyName, payload: newValue });
        },
        [setEditStatus]
    );

    const handleOrderDetailChange = (lineNumber, newValue) => {
        setEditStatus('edit');
        dispatch({
            type: 'orderDetailFieldChange',
            lineNumber,
            payload: newValue
        });
    };

    const handleAddOrderDetailRow = () => {
        setEditStatus('edit');
        dispatch({
            type: 'orderDetailAdd'
        });
    };

    const handleRemoveOrderDetailRow = lineNumber => {
        setEditStatus('edit');
        dispatch({
            type: 'orderDetailRemove',
            lineNumber
        });
    };

    const handleUpdatePostEntries = entries => {
        setEditStatus('edit');
        dispatch({
            type: 'postEntriesUpdate',
            entries
        });
    };

    const handleUpdateInvoiceDetails = details => {
        setEditStatus('edit');
        dispatch({
            type: 'invoiceDetailsUpdate',
            details
        });
    };

    const handleParcelChange = parcel => {
        setEditStatus('edit');
        dispatch({
            type: 'parcelChange',
            parcel
        });
    };

    const allowedToEdit = () => {
        return privileges?.some(priv => priv === 'import-books.admin');
    };

    const totalInvoiceValue = useCallback(() => {
        let total = `${state.impbook.importBookInvoiceDetails?.reduce(
            (a, v) => new Decimal(a).plus(v.invoiceValue ? v.invoiceValue : 0),
            0
        )}`;
        if (total) {
            total = `${parseFloat(total).toFixed(2)}`;
        }
        return total;
    }, [state.impbook.importBookInvoiceDetails]);

    const [localSuppliers, setLocalSuppliers] = useState([{}]);

    useEffect(() => {
        if (allSuppliers) {
            setLocalSuppliers([...allSuppliers]);
        }
    }, [allSuppliers]);

    const supplierCountryValue = () => {
        if (localSuppliers.length && state.impbook.supplierId) {
            const tempSupplier = localSuppliers.find(x => x.id === state.impbook.supplierId);
            if (!tempSupplier) {
                return '-';
            }
            return tempSupplier.countryCode;
        }
        if (!state.impbook.supplierId) {
            return '';
        }

        return 'loading..';
    };

    const supplierNameValue = () => {
        if (localSuppliers.length && state.impbook.supplierId) {
            const tempSupplier = localSuppliers.find(x => x.id === state.impbook.supplierId);
            if (!tempSupplier) {
                return 'undefined supplier';
            }
            return tempSupplier.name;
        }
        if (!state.impbook.supplierId) {
            return '';
        }
        return 'loading..';
    };

    const carrierNameValue = () => {
        if (localSuppliers.length && state.impbook.carrierId) {
            const tempCarrier = localSuppliers.find(x => x.id === state.impbook.carrierId);
            if (!tempCarrier) {
                return 'undefined carrier';
            }
            return tempCarrier.name;
        }
        if (!state.impbook.carrierId) {
            return '';
        }

        return 'loading..';
    };

    const countryIsInEU = () => {
        if (localSuppliers.length && state.impbook.supplierId) {
            const tempSupplier = localSuppliers.find(x => x.id === state.impbook.supplierId);
            if (!tempSupplier) {
                return '';
            }
            const country = countries.find(x => x.countryCode === tempSupplier.countryCode);
            return country?.eCMember;
        }
        if (!state.impbook.supplierId) {
            return '';
        }
        return 'loading..';
    };

    const calcRemainingTotal = () => {
        const orderDetailsTotal = state.impbook.importBookOrderDetails?.reduce(
            (a, v) => new Decimal(a).plus(v.orderValue ?? 0),
            0
        );

        if (!orderDetailsTotal) {
            return state.impbook.totalImportValue;
        }

        if (!state.impbook.totalImportValue) {
            return orderDetailsTotal.isZero() ? 0 : `-${orderDetailsTotal}`;
        }

        return new Decimal(state.impbook.totalImportValue).minus(orderDetailsTotal).valueOf();
    };

    const calcRemainingDuty = () => {
        const orderDetailsDutyTotal = state.impbook.importBookOrderDetails?.reduce(
            (a, v) => new Decimal(a).plus(v.dutyValue ?? 0),
            0
        );
        if (!orderDetailsDutyTotal) {
            return state.impbook.linnDuty;
        }
        if (!state.impbook.linnDuty) {
            return orderDetailsDutyTotal.isZero() ? 0 : `-${orderDetailsDutyTotal}`;
        }

        return new Decimal(state.impbook.linnDuty).minus(orderDetailsDutyTotal).valueOf();
    };

    const calcRemainingWeight = () => {
        const orderDetailsWeightTotal = state.impbook.importBookOrderDetails?.reduce(
            (a, v) => new Decimal(a).plus(v.weight ?? 0),
            0
        );

        if (!orderDetailsWeightTotal) {
            return state.impbook.weight;
        }

        if (!state.impbook.weight) {
            return orderDetailsWeightTotal.isZero() ? 0 : `-${orderDetailsWeightTotal}`;
        }

        return new Decimal(state.impbook.weight).minus(orderDetailsWeightTotal).valueOf();
    };

    const getEmployeeNameById = id => {
        if (employees.length && id) {
            return employees.find(x => x.id === id)?.fullName;
        }
        return '';
    };

    const currentExchangeRate = useCallback(() => {
        if (exchangeRates.length) {
            if (state.impbook?.currency) {
                return exchangeRates.find(
                    x => x.exchangeCurrency === state.impbook.currency && x.baseCurrency === 'GBP'
                )?.exchangeRate;
            }
        }
        return '';
    }, [exchangeRates, state.impbook.currency]);

    useEffect(() => {
        const exchangeRate = currentExchangeRate();
        const invoiceValue = totalInvoiceValue();
        if (exchangeRate && invoiceValue && invoiceValue > 0) {
            const convertedValue = currencyConvert(invoiceValue, exchangeRate);
            handleFieldChange('totalImportValue', convertedValue);
        }
    }, [totalInvoiceValue, currentExchangeRate, handleFieldChange]);

    const impbookInvalid = () => {
        return (
            !state.impbook.supplierId ||
            !state.impbook.carrierId ||
            !state.impbook.parcelNumber ||
            !state.impbook.transportId ||
            !state.impbook.transactionId ||
            !state.impbook.totalImportValue ||
            !state.impbook.deliveryTermCode ||
            !state.impbook.pva ||
            !state.impbook.foreignCurrency
        );
    };

    const [dialogOpen, setDialogOpen] = useState(false);

    const handleSaveClick = () => {
        if (`${calcRemainingWeight()}` !== '0' && !dialogOpen) {
            setDialogOpen(true);
        } else if (`${calcRemainingTotal()}` !== '0' && !dialogOpen) {
            setDialogOpen(true);
        } else if (`${calcRemainingDuty()}` !== '0' && !dialogOpen) {
            setDialogOpen(true);
        } else if (creating()) {
            addItem(state.impbook);
            setEditStatus('view');
        } else {
            updateItem(itemId, state.impbook);
            setEditStatus('view');
        }
    };

    const print = () => {
        window.print();
    };

    useEffect(() => {
        if (snackbarVisible) {
            print();
        }
    }, [snackbarVisible]);

    const useStyles = makeStyles(theme => ({
        spaceAbove: {
            marginTop: theme.spacing(2)
        },
        dialog: {
            margin: theme.spacing(6),
            minWidth: theme.spacing(70),
            textAlign: 'center'
        }
    }));

    const classes = useStyles();

    const ImportValueMismatchWarning = () => {
        if (`${calcRemainingTotal()}` !== '0') {
            return (
                <Grid item xs={12} className={classes.spaceAbove}>
                    <Typography variant="h6">
                        Import Value Mismatch! Difference of: {calcRemainingTotal()}
                        <br />
                        (Import Book value is: {state.impbook?.totalImportValue})
                    </Typography>
                </Grid>
            );
        }
        return <></>;
    };

    const DutyMismatchWarning = () => {
        if (`${calcRemainingDuty()}` !== '0') {
            return (
                <Grid item xs={12} className={classes.spaceAbove}>
                    <Typography variant="h6">
                        Duty Mismatch! Difference of: {calcRemainingDuty()}
                        <br />
                        (Import Book tab duty is: {state.impbook?.linnDuty})
                    </Typography>
                </Grid>
            );
        }
        return <></>;
    };

    const WeightMismatchWarning = () => {
        if (`${calcRemainingWeight()}` !== '0') {
            return (
                <Grid item xs={12} className={classes.spaceAbove}>
                    <Typography variant="h6">
                        Weight Mismatch! Difference of: {calcRemainingWeight()}
                        <br />
                        (Import Book tab weight is: {state.impbook?.weight})
                    </Typography>
                </Grid>
            );
        }
        return <></>;
    };

    return (
        <>
            <div className="pageContainer show-only-when-printing">
                <Page width="xl">
                    <ImpBookPrintOut
                        impbookId={state.impbook.id}
                        dateCreated={state.impbook.dateCreated}
                        createdBy={state.impbook.createdBy}
                        createdByName={getEmployeeNameById(state.impbook.createdBy)}
                        supplierId={state.impbook.supplierId}
                        supplierName={supplierNameValue()}
                        supplierCountry={supplierCountryValue()}
                        eecMember={countryIsInEU()}
                        currency={state.impbook.currency}
                        parcelNumber={state.impbook.parcelNumber}
                        totalImportValue={state.impbook.totalImportValue}
                        invoiceDetails={state.impbook.importBookInvoiceDetails}
                        carrierId={state.impbook.carrierId}
                        carrierName={carrierNameValue()}
                        transportCode={state.impbook.transportId}
                        transportBillNumber={state.impbook.transportBillNumber}
                        transactionCode={state.impbook.transactionId}
                        numPallets={state.impbook.numPallets}
                        numCartons={state.impbook.numCartons}
                        weight={state.impbook.weight}
                        deliveryTermCode={state.impbook.deliveryTermCode}
                        customsEntryCodePrefix={state.impbook.customsEntryCodePrefix}
                        customsEntryCode={state.impbook.customsEntryCode}
                        customsEntryCodeDate={state.impbook.customsEntryCodeDate}
                        linnDuty={state.impbook.linnDuty}
                        linnVat={state.impbook.linnVat}
                        arrivalDate={state.impbook.arrivalDate}
                        remainingInvoiceValue={calcRemainingTotal()}
                        remainingDutyValue={calcRemainingDuty()}
                        remainingWeightValue={calcRemainingWeight()}
                        orderDetails={state.impbook.importBookOrderDetails}
                        comments={state.impbook.comments}
                        arrivalPort={state.impbook.arrivalPort}
                        pva={state.impbook.pva}
                        cpcNumbers={cpcNumbers}
                    />
                </Page>
            </div>

            <div className="hide-when-printing">
                <Dialog open={dialogOpen} fullWidth maxWidth="md">
                    <>
                        <div className={classes.dialog}>
                            <Grid item xs={12}>
                                <Typography variant="h5">Warning</Typography>
                            </Grid>
                            <Grid item xs={12} />
                            <ImportValueMismatchWarning />
                            <DutyMismatchWarning />
                            <WeightMismatchWarning />
                            <Grid item xs={12} />
                            <Grid item xs={12} container className={classes.spaceAbove}>
                                <Grid item xs={6}>
                                    <Button
                                        variant="outlined"
                                        color="secondary"
                                        onClick={() => {
                                            handleSaveClick();
                                            setDialogOpen(false);
                                        }}
                                    >
                                        Save Anyway
                                    </Button>
                                </Grid>
                                <Grid item xs={6}>
                                    <Button
                                        variant="outlined"
                                        color="primary"
                                        onClick={() => {
                                            setDialogOpen(false);
                                        }}
                                    >
                                        Go Back
                                    </Button>
                                </Grid>
                            </Grid>
                        </div>
                    </>
                </Dialog>
                <Page width="xl">
                    <Grid container spacing={3}>
                        <Grid item xs={10}>
                            {creating() ? (
                                <Title text="Create Import Book" />
                            ) : (
                                <Title text="Import Book" />
                            )}
                        </Grid>
                        <Grid item xs={2} />

                        {itemError && (
                            <Grid item xs={12}>
                                <ErrorCard
                                    errorMessage={
                                        itemError?.details?.errors?.[0] || itemError.statusText
                                    }
                                />
                            </Grid>
                        )}
                        {loading ? (
                            <Grid item xs={12}>
                                <Loading />
                            </Grid>
                        ) : (
                            (state.impbook.id || creating()) &&
                            itemError?.status !== 404 && (
                                <>
                                    <SnackbarMessage
                                        visible={snackbarVisible}
                                        onClose={() => setSnackbarVisible(false)}
                                        message="Save Successful"
                                    />
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={creating() ? 'New' : state.impbook.id}
                                            label="Import Book Id"
                                            maxLength={8}
                                            helperText="This field cannot be changed"
                                            onChange={handleFieldChange}
                                            propertyName="importBookId"
                                        />
                                    </Grid>
                                    <Grid item xs={1} />
                                    <Grid item xs={8}>
                                        <Tabs
                                            value={tab}
                                            onChange={handleTabChange}
                                            indicatorColor="primary"
                                            textColor="primary"
                                            style={{ paddingBottom: '40px' }}
                                        >
                                            <Tab label="Import Book" />
                                            <Tab label="Order Details" />
                                            <Tab label="Post Entries" />
                                            <Tab label="Comments" />
                                        </Tabs>
                                    </Grid>

                                    {tab === 0 && (
                                        <ImpBookTab
                                            dateCreated={state.impbook.dateCreated}
                                            editStatus={editStatus}
                                            handleFieldChange={handleFieldChange}
                                            parcelNumber={state.impbook.parcelNumber}
                                            supplierId={state.impbook.supplierId}
                                            foreignCurrency={state.impbook.foreignCurrency}
                                            currency={state.impbook.currency}
                                            totalImportValue={state.impbook.totalImportValue}
                                            carrierId={state.impbook.carrierId}
                                            carrierInvDate={state.impbook.carrierInvDate}
                                            transportId={state.impbook.transportId}
                                            transportBillNumber={state.impbook.transportBillNumber}
                                            transactionId={state.impbook.transactionId}
                                            deliveryTermCode={state.impbook.deliveryTermCode}
                                            arrivalPort={state.impbook.arrivalPort}
                                            arrivalDate={state.impbook.arrivalDate}
                                            createdBy={state.impbook.createdBy}
                                            numPallets={state.impbook.numPallets}
                                            numCartons={state.impbook.numCartons}
                                            weight={state.impbook.weight}
                                            customsEntryCode={state.impbook.customsEntryCode}
                                            customsEntryCodePrefix={
                                                state.impbook.customsEntryCodePrefix
                                            }
                                            customsEntryCodeDate={
                                                state.impbook.customsEntryCodeDate
                                            }
                                            linnDuty={state.impbook.linnDuty}
                                            linnVat={state.impbook.linnVat}
                                            allowedToEdit={allowedToEdit()}
                                            invoiceDetails={state.impbook.importBookInvoiceDetails}
                                            handleUpdateInvoiceDetails={handleUpdateInvoiceDetails}
                                            totalInvoiceValue={totalInvoiceValue()}
                                            handleParcelChange={handleParcelChange}
                                            supplierCountryValue={supplierCountryValue}
                                            supplierNameValue={supplierNameValue}
                                            carrierNameValue={carrierNameValue}
                                            countryIsInEU={countryIsInEU}
                                            employees={employees}
                                            pva={state.impbook.pva}
                                            exchangeRate={currentExchangeRate()}
                                        />
                                    )}

                                    {tab === 1 && (
                                        <OrderDetailsTab
                                            orderDetails={state.impbook.importBookOrderDetails}
                                            handleFieldChange={handleFieldChange}
                                            invoiceDate={state.impbook.invoiceDate}
                                            handleOrderDetailChange={handleOrderDetailChange}
                                            allowedToEdit={allowedToEdit()}
                                            addOrderDetailRow={handleAddOrderDetailRow}
                                            removeOrderDetailRow={handleRemoveOrderDetailRow}
                                            remainingInvoiceValue={calcRemainingTotal()}
                                            remainingDutyValue={calcRemainingDuty()}
                                            remainingWeightValue={calcRemainingWeight()}
                                            supplierId={state.impbook.supplierId}
                                            impbookWeight={state.impbook.weight}
                                            currentUserNumber={userNumber}
                                            impbookId={state.impbook.id}
                                            exchangeRate={currentExchangeRate()}
                                            cpcNumbers={cpcNumbers}
                                        />
                                    )}
                                    {tab === 2 && (
                                        <PostEntriesTab
                                            postEntries={state.impbook.importBookPostEntries}
                                            updatePostEntries={handleUpdatePostEntries}
                                            allowedToEdit={allowedToEdit()}
                                            totalInvoiceValue={totalInvoiceValue()}
                                        />
                                    )}
                                    {tab === 3 && (
                                        <CommentsTab
                                            comments={state.impbook.comments}
                                            dateCancelled={state.impbook.dateCancelled}
                                            cancelledBy={state.impbook.cancelledBy}
                                            cancelledReason={state.impbook.cancelledReason}
                                            handleFieldChange={handleFieldChange}
                                            allowedToEdit={allowedToEdit()}
                                        />
                                    )}

                                    <Grid item xs={2}>
                                        <Button
                                            style={{ marginTop: '40px' }}
                                            onClick={print}
                                            variant="outlined"
                                            color="primary"
                                            disabled={impbookInvalid()}
                                        >
                                            Reprint
                                        </Button>
                                    </Grid>
                                    <Grid item xs={10}>
                                        <SaveBackCancelButtons
                                            saveDisabled={
                                                viewing() || !allowedToEdit() || impbookInvalid()
                                            }
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
            </div>
        </>
    );
}

ImportBook.propTypes = {
    item: PropTypes.shape({
        id: PropTypes.number.isRequired,
        dateCreated: PropTypes.string.isRequired,
        parcelNumber: PropTypes.number,
        supplierId: PropTypes.number.isRequired,
        foreignCurrency: PropTypes.string.isRequired,
        currency: PropTypes.string,
        carrierId: PropTypes.number.isRequired,
        transportId: PropTypes.number.isRequired,
        transportBillNumber: PropTypes.string,
        transactionId: PropTypes.number.isRequired,
        deliveryTermCode: PropTypes.string.isRequired,
        arrivalPort: PropTypes.string,
        totalImportValue: PropTypes.number,
        weight: PropTypes.number,
        customsEntryCode: PropTypes.string,
        customsEntryCodeDate: PropTypes.string,
        linnDuty: PropTypes.number,
        linnVat: PropTypes.number,
        dateCancelled: PropTypes.string,
        cancelledBy: PropTypes.number,
        cancelledReason: PropTypes.string,
        carrierInvDate: PropTypes.string,
        numCartons: PropTypes.number,
        numPallets: PropTypes.number,
        comments: PropTypes.string,
        createdBy: PropTypes.number,
        customsEntryCodePrefix: '',
        links: PropTypes.arrayOf(PropTypes.shape({ href: PropTypes.string, rel: PropTypes.string }))
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        item: PropTypes.string,
        details: PropTypes.shape({
            errors: PropTypes.arrayOf(PropTypes.shape({}))
        })
    }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    addItem: PropTypes.func.isRequired,
    updateItem: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired,
    countries: PropTypes.arrayOf(PropTypes.shape({})),
    allSuppliers: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string,
            country: PropTypes.string
        })
    ),
    employees: PropTypes.arrayOf(
        PropTypes.shape({ id: PropTypes.number, fullName: PropTypes.string })
    ),
    userNumber: PropTypes.number.isRequired,
    getExchangeRatesForDate: PropTypes.func.isRequired,
    exchangeRates: PropTypes.arrayOf(PropTypes.shape({})),
    cpcNumbers: PropTypes.arrayOf(PropTypes.shape({})).isRequired
};

ImportBook.defaultProps = {
    item: null,
    snackbarVisible: false,
    loading: true,
    itemError: null,
    itemId: null,
    countries: [{ id: '-1', countryCode: 'loading..' }],
    allSuppliers: [{ id: 0, name: 'loading', country: 'loading' }],
    employees: [{ id: '-1', fullname: 'loading..' }],
    exchangeRates: [
        {
            periodNumber: PropTypes.number,
            baseCurrency: PropTypes.string,
            exchangeCurrency: PropTypes.string,
            exchangeRate: PropTypes.number
        }
    ]
};

export default ImportBook;
