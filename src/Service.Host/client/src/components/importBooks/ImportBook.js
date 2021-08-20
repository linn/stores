import React, { useState, useEffect, useReducer } from 'react';
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
import Page from '../../containers/Page';
import ImpBookTab from '../../containers/importBooks/tabs/ImpBookTab';
import OrderDetailsTab from '../../containers/importBooks/tabs/OrderDetailsTab';
import PostEntriesTab from './tabs/PostEntriesTab';
import CommentsTab from '../../containers/importBooks/tabs/CommentsTab';

import ImportBookReducer from './ImportBookReducer';

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
    privileges
}) {
    const defaultImpBook = {
        id: null,
        dateCreated: new Date(),
        parcelNumber: null,
        supplierId: null,
        foreignCurrency: '',
        currency: '',
        carrierId: null,
        OldArrivalPort: '',
        flightNumber: '',
        transportId: null,
        transportBillNumber: '',
        transactionId: null,
        deliveryTermCode: '',
        arrivalPort: '',
        lineVatTotal: null,
        hwb: '',
        supplierCostCurrency: '',
        transNature: '',
        arrivalDate: new Date(),
        freightCharges: null,
        handlingCharge: null,
        clearanceCharge: null,
        cartage: null,
        duty: null,
        vat: null,
        misc: null,
        carriersInvTotal: null,
        carriersVatTotal: null,
        totalImportValue: null,
        pieces: null,
        weight: null,
        customsEntryCode: '',
        customsEntryCodeDate: new Date(),
        linnDuty: null,
        linnVat: null,
        iprCpcNumber: null,
        eecgNumber: null,
        dateCancelled: null,
        cancelledBy: null,
        cancelledReason: '',
        carrierInvNumber: '',
        carrierInvDate: new Date(),
        countryOfOrigin: '',
        fcName: '',
        vaxRef: '',
        storage: null,
        numCartons: null,
        numPallets: null,
        comments: '',
        exchangeRate: null,
        exchangeCurrency: '',
        baseCurrency: '',
        periodNumber: null,
        createdBy: null,
        portCode: '',
        customsEntryCodePrefix: '',
        importBookOrderDetails: null,
        importBookPostEntries: null
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

    const viewing = () => editStatus === 'view';

    const [tab, setTab] = useState(0);

    const handleTabChange = (event, value) => {
        setTab(value);
    };

    const impbookInvalid = () => {
        return false;
    };
    const handleSaveClick = () => {
        if (creating()) {
            addItem(state.impbook);
        } else {
            updateItem(itemId, state.impbook);
        }
        setEditStatus('view');
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

    const handleFieldChange = (propertyName, newValue) => {
        setEditStatus('edit');
        dispatch({ type: 'fieldChange', fieldName: propertyName, payload: newValue });
    };

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

    const allowedToEdit = () => {
        if (!(privileges.length < 1)) {
            return privileges.some(priv => priv === 'import-books.admin');
        }
        return false;
    };

    return (
        <Page>
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
                            errorMessage={itemError?.details?.errors?.[0] || itemError.statusText}
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
                                    propertyName="id"
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
                                    vaxRef={state.impbook.vaxRef}
                                    supplierId={state.impbook.supplierId}
                                    foreignCurrency={state.impbook.foreignCurrency}
                                    eecgNumber={state.impbook.eecgNumber}
                                    currency={state.impbook.currency}
                                    exchangeRate={state.impbook.exchangeRate}
                                    totalImportValue={state.impbook.totalImportValue}
                                    carrierId={state.impbook.carrierId}
                                    carrierInvDate={state.impbook.carrierInvDate}
                                    carrierInvNumber={state.impbook.carrierInvNumber}
                                    transportId={state.impbook.transportId}
                                    transportBillNumber={state.impbook.transportBillNumber}
                                    transactionId={state.impbook.transactionId}
                                    deliveryTermCode={state.impbook.deliveryTermCode}
                                    arrivalPort={state.impbook.arrivalPort}
                                    flightNumber={state.impbook.flightNumber}
                                    hwb={state.impbook.hwb}
                                    arrivalDate={state.impbook.arrivalDate}
                                    createdBy={state.impbook.createdBy}
                                    freightCharges={state.impbook.freightCharges}
                                    handlingCharge={state.impbook.handlingCharge}
                                    clearanceCharge={state.impbook.clearanceCharge}
                                    cartage={state.impbook.cartage}
                                    storage={state.impbook.storage}
                                    duty={state.impbook.duty}
                                    vat={state.impbook.vat}
                                    misc={state.impbook.misc}
                                    carriersInvTotal={state.impbook.carriersInvTotal}
                                    carriersVatTotal={state.impbook.carriersVatTotal}
                                    numPallets={state.impbook.numPallets}
                                    numCartons={state.impbook.numCartons}
                                    weight={state.impbook.weight}
                                    customsEntryCode={state.impbook.customsEntryCode}
                                    customsEntryCodePrefix={state.impbook.customsEntryCodePrefix}
                                    customsEntryCodeDate={state.impbook.customsEntryCodeDate}
                                    linnDuty={state.impbook.linnDuty}
                                    linnVat={state.impbook.linnVat}
                                    allowedToEdit={allowedToEdit()}
                                />
                            )}

                            {tab === 1 && (
                                <OrderDetailsTab
                                    orderDetails={state.impbook.importBookOrderDetails}
                                    handleFieldChange={handleFieldChange}
                                    handleOrderDetailChange={handleOrderDetailChange}
                                    allowedToEdit={allowedToEdit()}
                                    addOrderDetailRow={handleAddOrderDetailRow}
                                    removeOrderDetailRow={handleRemoveOrderDetailRow}
                                />
                            )}
                            {tab === 2 && (
                                <PostEntriesTab
                                    postEntries={state.impbook.importBookPostEntries}
                                    updatePostEntries={handleUpdatePostEntries}
                                    allowedToEdit={allowedToEdit()}
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

                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing() || !allowedToEdit() || impbookInvalid()}
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

ImportBook.propTypes = {
    item: PropTypes.shape({
        id: PropTypes.number.isRequired,
        dateCreated: PropTypes.string.isRequired,
        parcelNumber: PropTypes.number,
        supplierId: PropTypes.number.isRequired,
        foreignCurrency: PropTypes.string.isRequired,
        currency: PropTypes.string,
        carrierId: PropTypes.number.isRequired,
        OldArrivalPort: PropTypes.string,
        flightNumber: PropTypes.string,
        transportId: PropTypes.number.isRequired,
        transportBillNumber: PropTypes.string,
        transactionId: PropTypes.number.isRequired,
        deliveryTermCode: PropTypes.string.isRequired,
        arrivalPort: PropTypes.string,
        lineVatTotal: PropTypes.number,
        hwb: PropTypes.string,
        supplierCostCurrency: PropTypes.string,
        transNature: PropTypes.string,
        arrivalDate: PropTypes.string,
        freightCharges: PropTypes.number,
        handlingCharge: PropTypes.number,
        clearanceCharge: PropTypes.number,
        cartage: PropTypes.number,
        duty: PropTypes.number,
        vat: PropTypes.number,
        misc: PropTypes.number,
        carriersInvTotal: PropTypes.number,
        carriersVatTotal: PropTypes.number,
        totalImportValue: PropTypes.number.isRequired,
        pieces: PropTypes.number,
        weight: PropTypes.number,
        customsEntryCode: PropTypes.string,
        customsEntryCodeDate: PropTypes.string,
        linnDuty: PropTypes.number,
        linnVat: PropTypes.number,
        iprCpcNumber: PropTypes.number,
        eecgNumber: PropTypes.number,
        dateCancelled: PropTypes.string,
        cancelledBy: PropTypes.number,
        cancelledReason: PropTypes.string,
        carrierInvNumber: PropTypes.string,
        carrierInvDate: PropTypes.string,
        countryOfOrigin: PropTypes.string,
        fcName: PropTypes.string,
        vaxRef: PropTypes.string,
        storage: PropTypes.number,
        numCartons: PropTypes.number,
        numPallets: PropTypes.number,
        comments: PropTypes.string,
        exchangeRate: PropTypes.number,
        exchangeCurrency: PropTypes.string,
        baseCurrency: PropTypes.string,
        periodNumber: PropTypes.number,
        createdBy: PropTypes.number,
        portCode: PropTypes.string,
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
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired
};

ImportBook.defaultProps = {
    item: null,
    snackbarVisible: false,
    loading: true,
    itemError: null,
    itemId: null
};

export default ImportBook;
