import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import makeStyles from '@material-ui/styles/makeStyles';
import { DataGrid } from '@mui/x-data-grid';
import {
    InputField,
    Typeahead,
    Dropdown,
    DatePicker,
    Title,
    CheckboxWithLabel,
    Loading
} from '@linn-it/linn-form-components-library';
import QcLabelPrintScreen from '../../containers/goodsIn/QcLabelPrintScreen';
import Page from '../../containers/Page';
import Parcel from '../../containers/parcels/Parcel';
import LoanDetails from './LoanDetails';
import RsnDetails from './RsnDetails';

function GoodsInUtility({
    validatePurchaseOrder,
    validatePurchaseOrderResult,
    validatePurchaseOrderResultLoading,
    searchDemLocations,
    demLocationsSearchLoading,
    demLocationsSearchResults,
    searchStoragePlaces,
    storagePlacesSearchResults,
    storagePlacesSearchLoading,
    searchSalesArticles,
    salesArticlesSearchResults,
    salesArticlesSearchLoading,
    bookInResult,
    bookInResultLoading,
    doBookIn,
    validatePurchaseOrderBookInQtyResult,
    validatePurchaseOrderBookInQty,
    validatePurchaseOrderBookInQtyResultLoading,
    userNumber,
    validateStorageType,
    validateStorageTypeResult,
    validateStorageTypeResultLoading,
    match,
    history,
    loanDetails,
    getLoanDetails,
    loanDetailsLoading,
    rsnConditions,
    rsnConditionsLoading,
    getRsnConditions,
    rsnAccessories,
    rsnAccessoriesLoading,
    getRsnAccessories
}) {
    const [formData, setFormData] = useState({
        orderNumber: null,
        dateReceived: new Date(),
        lines: []
    });

    const [message, setMessage] = useState({ error: false, text: '' });

    const [multipleBookIn, setMultipleBookIn] = useState(false);

    const [logEntries, setLogEntries] = useState([]);

    const [lines, setLines] = useState([]);

    const [rows, setRows] = useState([...logEntries, ...lines]);
    const [selectedRows, setSelectedRows] = useState([]);

    const getMessageColour = () => {
        if (bookInResult?.success) {
            return 'limegreen';
        }
        if (message?.error) {
            return 'red';
        }
        return 'black';
    };

    const useStyles = makeStyles(theme => ({
        dialog: {
            margin: theme.spacing(6),
            minWidth: theme.spacing(70)
        },
        notchedOutline: {
            borderWidth: '3px',
            borderColor: `${getMessageColour()} !important`
        }
    }));

    const handleFieldChange = (propertyName, newValue) => {
        setFormData({ ...formData, [propertyName]: newValue });
    };
    const [bookInPoExpanded, setBookInPoExpanded] = useState(false);

    const [loanExpanded, setLoanExpanded] = useState(false);

    const [bookInRsnExpanded, setBookInRsnExpanded] = useState(false);

    const [printDialogOpen, setPrintDialogOpen] = useState(false);

    const [parcelDialogOpen, setParcelDialogOpen] = useState(false);

    const [loanDetailsDialogOpen, setLoanDetailsDialogOpen] = useState(false);

    const [rsnDetailsDialogOpen, setRsnDetailsDialogOpen] = useState(false);

    const handleSelectLoanDetails = details => {
        setLoanDetailsDialogOpen(false);
        setLines(l => [
            ...l,
            ...details.map((detail, i) => ({
                id: l.length + i,
                articleNumber: detail.articleNumber,
                transactionType: 'L',
                dateCreated: new Date().toISOString(),
                storagePlace: formData?.ontoLocation,
                locationId: formData?.ontoLocationId,
                quantity: detail.return,
                serialNumber: detail.serialNumber,
                serialNumber2: details.serialNumber2,
                loanNumber: detail.loanNumber,
                loanLine: detail.line,
                createdBy: userNumber
            }))
        ]);
    };

    const handleSelectRsnDetails = details => {
        setLoanDetailsDialogOpen(false);
        setLines(l => [
            ...l,
            ...details.map((detail, i) => ({
                id: l.length + i,
                articleNumber: detail.articleNumber,
                transactionType: 'L',
                dateCreated: new Date().toISOString(),
                storagePlace: formData?.ontoLocation,
                locationId: formData?.ontoLocationId,
                quantity: detail.return,
                serialNumber: detail.serialNumber,
                serialNumber2: details.serialNumber2,
                loanNumber: detail.loanNumber,
                loanLine: detail.line,
                createdBy: userNumber
            }))
        ]);
    };

    useEffect(() => {
        setRows([...logEntries, ...lines]);
    }, [logEntries, lines]);

    useEffect(() => {
        if (validatePurchaseOrderResult?.documentType === 'PO') {
            setBookInPoExpanded(true);
        } else {
            setBookInPoExpanded(false);
        }
    }, [validatePurchaseOrderResult]);

    useEffect(() => {
        if (validatePurchaseOrderResult) {
            setMessage({
                error: !!validatePurchaseOrderResult.message,
                text: validatePurchaseOrderResult.message
            });
        }
    }, [validatePurchaseOrderResult]);

    useEffect(() => {
        if (loanDetails?.length > 0) {
            setLoanDetailsDialogOpen(true);
        }
    }, [loanDetails]);

    useEffect(() => {
        if (rsnConditions?.length > 0 && rsnAccessories?.length > 0) {
            setRsnDetailsDialogOpen(true);
        }
    }, [rsnConditions, rsnAccessories]);

    useEffect(() => {
        if (validatePurchaseOrderBookInQtyResult) {
            if (validatePurchaseOrderBookInQtyResult.success) {
                setFormData(d => ({ ...d, numberOfLines: 1 }));
            }
            setMessage({
                error: !validatePurchaseOrderBookInQtyResult?.success,
                text: validatePurchaseOrderBookInQtyResult?.message
            });
        }
    }, [validatePurchaseOrderBookInQtyResult]);

    useEffect(() => {
        if (validateStorageTypeResult?.message) {
            setMessage({ error: true, text: validateStorageTypeResult?.message });
        } else {
            setFormData(d => ({ ...d, ontoLocation: validateStorageTypeResult?.locationCode }));
        }
    }, [validateStorageTypeResult]);

    useEffect(() => {
        if (bookInResult?.message) {
            setMessage({ error: !bookInResult.success, text: bookInResult.message });
        }
        if (bookInResult?.success && bookInResult.printLabels) {
            setPrintDialogOpen(true);
            setLines([]);
            setLogEntries(r => [...r, ...bookInResult.lines]);
        }
        if (bookInResult?.createParcel) {
            setParcelDialogOpen(true);
        }
    }, [bookInResult]);

    const classes = useStyles();

    const tableColumns = [
        {
            headerName: 'id',
            field: 'id',
            width: 100,
            hide: true
        },
        {
            headerName: 'Trans Type',
            field: 'transactionType',
            width: 200
        },
        {
            headerName: 'Created',
            field: 'dateCreated',
            width: 100,
            hide: true
        },
        {
            headerName: 'By',
            field: 'createdBy',
            width: 100,
            hide: true
        },
        {
            headerName: 'Wandstring',
            field: 'wandstring',
            hide: true
        },
        {
            headerName: 'Article',
            field: 'articleNumber',
            width: 200
        },
        {
            headerName: 'LocId',
            field: 'locationId',
            width: 200,
            hide: true
        },
        {
            headerName: 'Loc',
            field: 'storagePlace',
            width: 200
        },
        {
            headerName: 'Qty',
            field: 'quantity',
            width: 100
        },
        {
            headerName: 'Serial',
            field: 'serialNumber',
            width: 200,
            hide: false
        },
        {
            headerName: 'Serial 2',
            field: 'serialNumber2',
            width: 200,
            hide: true
        },
        {
            headerName: 'Order',
            field: 'orderNumber',
            width: 200
        },
        {
            headerName: 'Line',
            field: 'orderLine',
            width: 200,
            hide: true
        },
        {
            headerName: 'S/Type',
            field: 'storageType',
            width: 200
        },
        {
            headerName: 'Manuf Part',
            field: 'manufacturersPartNumber',
            width: 200
        },
        {
            headerName: 'Loan',
            field: 'loanNumber',
            width: 200
        },
        {
            headerName: 'Line',
            field: 'loanLine',
            width: 200
        },
        {
            headerName: 'State',
            field: 'state',
            width: 200
        },
        {
            headerName: 'Description',
            field: 'description',
            width: 300,
            hide: true
        }
    ];

    const handleSelectRow = selected => {
        setSelectedRows(rows.filter(r => selected.includes(r.id)));
    };

    if (bookInResultLoading) {
        return (
            <Page>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                </Grid>
            </Page>
        );
    }

    return (
        <Page>
            <Grid container spacing={3}>
                <Dialog open={printDialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setPrintDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <QcLabelPrintScreen
                                kardexLocation={bookInResult?.kardexLocation}
                                partNumber={validatePurchaseOrderResult?.partNumber}
                                partDescription={validatePurchaseOrderResult?.partNumber}
                                reqNumber={bookInResult?.reqNumber}
                                orderNumber={validatePurchaseOrderResult?.orderNumber}
                                qcState={bookInResult?.qcState}
                                qcInfo={bookInResult?.qcInfo}
                                docType={bookInResult?.docType}
                                unitOfMeasure={bookInResult?.unitOfMeasure}
                                qtyReceived={bookInResult?.qtyReceived}
                            />
                        </div>
                    </div>
                </Dialog>
                <Dialog open={parcelDialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setParcelDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <Parcel
                                comments={bookInResult?.parcelComments}
                                supplierId={bookInResult?.supplierId}
                                match={match}
                                inDialogBox
                                history={history}
                            />
                        </div>
                    </div>
                </Dialog>
                <Dialog open={loanDetailsDialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setLoanDetailsDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <LoanDetails
                                loanDetails={loanDetails?.map(d => ({
                                    ...d,
                                    id: d.line,
                                    return: d.qtyOnLoan,
                                    selected: false
                                }))}
                                onConfirm={handleSelectLoanDetails}
                            />
                        </div>
                    </div>
                </Dialog>
                <Dialog open={rsnDetailsDialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setRsnDetailsDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <RsnDetails
                                rsnAccessories={rsnAccessories?.map(d => ({
                                    ...d,
                                    id: d.code
                                }))}
                                rsnConditions={rsnConditions?.map(d => ({
                                    ...d,
                                    id: d.code
                                }))}
                                onConfirm={handleSelectRsnDetails}
                            />
                        </div>
                    </div>
                </Dialog>
                <Grid item xs={12}>
                    <Title text="Goods In Utility" />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        fullWidth
                        disabled
                        textFieldProps={{
                            InputProps: {
                                classes: {
                                    notchedOutline: classes.notchedOutline
                                }
                            }
                        }}
                        error={message.error}
                        rows={3}
                        value={
                            validatePurchaseOrderResultLoading ||
                            validatePurchaseOrderBookInQtyResultLoading ||
                            bookInResultLoading ||
                            validateStorageTypeResultLoading ||
                            loanDetailsLoading
                                ? 'loading'
                                : message.text
                        }
                        label="Message"
                        propertyName="message"
                    />
                </Grid>
                <Grid item xs={6} />

                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        type="number"
                        value={formData.orderNumber}
                        label="Order Number"
                        disabled={validatePurchaseOrderResultLoading}
                        propertyName="orderNumber"
                        onChange={handleFieldChange}
                        textFieldProps={{
                            onBlur: () =>
                                formData.orderNumber
                                    ? validatePurchaseOrder(formData.orderNumber)
                                    : {}
                        }}
                    />
                </Grid>

                <Grid item xs={1}>
                    <InputField
                        fullWidth
                        value={formData.qty}
                        label="Qty"
                        propertyName="qty"
                        type="number"
                        disabled={
                            !validatePurchaseOrderResult || !!validatePurchaseOrderResult?.message
                        }
                        textFieldProps={{
                            onBlur: () =>
                                formData.qty &&
                                validatePurchaseOrderBookInQty(
                                    `qty=${formData.qty}&orderLine=${1}&orderNumber`,
                                    formData.orderNumber
                                )
                        }}
                        onChange={handleFieldChange}
                    />
                </Grid>
                <Grid item xs={2}>
                    <InputField
                        fullWidth
                        value={formData.storageType}
                        label="S/Type"
                        disabled={
                            !validatePurchaseOrderResult ||
                            (validatePurchaseOrderResult.message !==
                                'New part - enter storage type or location' &&
                                validatePurchaseOrderResult.storage === 'BB')
                        }
                        propertyName="storageType"
                        onChange={handleFieldChange}
                        textFieldProps={{
                            onBlur: () =>
                                formData.storageType &&
                                validateStorageType(`storageType`, formData.storageType)
                        }}
                    />
                </Grid>
                <Grid item xs={5} />
                <Grid item xs={3}>
                    <InputField
                        fullWidth
                        value={formData.serialNumber}
                        label="Serial"
                        propertyName="serialNumber"
                        onChange={handleFieldChange}
                    />
                </Grid>
                <Grid item xs={3}>
                    <Typeahead
                        onSelect={newValue => {
                            handleFieldChange('salesArticle', newValue.name);
                        }}
                        label="Article"
                        modal
                        items={salesArticlesSearchResults}
                        propertyName="salesArticle"
                        value={formData?.salesArticle}
                        loading={salesArticlesSearchLoading}
                        fetchItems={searchSalesArticles}
                        links={false}
                        clearSearch={() => {}}
                        placeholder="Search Articles"
                    />
                </Grid>
                <Grid item xs={3}>
                    <Typeahead
                        onSelect={newValue => {
                            handleFieldChange('demLocation', newValue.name);
                        }}
                        label="Dem Location"
                        propertyName="demLocation"
                        modal
                        items={demLocationsSearchResults}
                        value={formData?.demLocation}
                        loading={demLocationsSearchLoading}
                        fetchItems={searchDemLocations}
                        links={false}
                        clearSearch={() => {}}
                        placeholder="Search Locations"
                    />
                </Grid>
                <Grid item xs={3} />
                <Grid item xs={3}>
                    <Typeahead
                        onSelect={newValue =>
                            setFormData(d => ({
                                ...d,
                                ontoLocation: newValue.name,
                                ontoLocationId: newValue.locationId,
                                palletNumber: newValue.palletNumber
                            }))
                        }
                        label="Onto Location"
                        modal
                        propertyName="ontoLocation"
                        items={storagePlacesSearchResults}
                        value={formData?.ontoLocation}
                        loading={storagePlacesSearchLoading}
                        fetchItems={searchStoragePlaces}
                        links={false}
                        clearSearch={() => {}}
                        placeholder="Search Locations"
                        minimumSearchTermLength={3}
                    />
                </Grid>
                <Grid item xs={3}>
                    <Dropdown
                        items={['STORES', 'QC', 'FAIL']}
                        propertyName="state"
                        fullWidth
                        allowNoValue
                        value={validatePurchaseOrderResult?.state || ''}
                        label="State"
                        onChange={handleFieldChange}
                        type="state"
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        fullWidth
                        value={formData.comments}
                        label="Comments"
                        propertyName="comments"
                        onChange={handleFieldChange}
                    />
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        label="Date Received"
                        value={formData?.dateReceived}
                        onChange={value => {
                            handleFieldChange('dateReceived', value);
                        }}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Accordion expanded={bookInPoExpanded}>
                        <AccordionSummary
                            onClick={() => setBookInPoExpanded(!bookInPoExpanded)}
                            expandIcon={<ExpandMoreIcon />}
                            aria-controls="panel1a-content"
                            id="panel1a-header"
                        >
                            <Typography>Book In PO</Typography>
                        </AccordionSummary>
                        <AccordionDetails>
                            <Grid container spacing={3}>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.orderNumber}
                                        label="Order No"
                                        propertyName="purchaseOrderNumber"
                                        disabled
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={1}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.orderLine}
                                        label="Line"
                                        propertyName="orderLine"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={1}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.documentType}
                                        label="Type"
                                        propertyName="documentType"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.orderQty}
                                        label="Order Qty"
                                        disabled={!validatePurchaseOrderResult}
                                        type="number"
                                        propertyName="orderQty"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.qtyBookedIn}
                                        label="Booked In"
                                        type="number"
                                        propertyName="qtyBookedIn"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={formData?.thisBookIn}
                                        label="This Bookin"
                                        type="number"
                                        propertyName="thisBookIn"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={1}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.storage}
                                        label="Storage"
                                        propertyName="storage"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={1}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.qcPart}
                                        label="QC"
                                        propertyName="qcPart"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.partNumber}
                                        label="Part"
                                        propertyName="partNumber"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={5}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.partDescription}
                                        label="Description"
                                        propertyName="partDescription"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.orderUnitOfMeasure}
                                        label="UOM"
                                        propertyName="orderUnitOfMeasure"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={validatePurchaseOrderResult?.manufacturersPartNumber}
                                        label="MFPN"
                                        propertyName="manufacturersPartNumber"
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                            </Grid>
                        </AccordionDetails>
                    </Accordion>
                </Grid>
                <Grid item xs={12}>
                    <Accordion expanded={loanExpanded}>
                        <AccordionSummary
                            onClick={() => setLoanExpanded(!loanExpanded)}
                            expandIcon={<ExpandMoreIcon />}
                            aria-controls="panel1a-content"
                            id="panel1a-header"
                        >
                            <Typography>Book In Loan</Typography>
                        </AccordionSummary>
                        <AccordionDetails>
                            <Grid container spacing={3}>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={formData?.loanNumber}
                                        label="Loan Number"
                                        propertyName="loanNumber"
                                        disabled={!formData?.ontoLocation}
                                        onChange={handleFieldChange}
                                        textFieldProps={{
                                            onBlur: () =>
                                                formData.loanNumber
                                                    ? getLoanDetails(
                                                          'loanNumber',
                                                          formData.loanNumber
                                                      )
                                                    : {}
                                        }}
                                    />
                                </Grid>
                                <Grid item xs={10} />
                            </Grid>
                        </AccordionDetails>
                    </Accordion>
                </Grid>
                <Grid item xs={12}>
                    <Accordion expanded={bookInRsnExpanded}>
                        <AccordionSummary
                            onClick={() => setBookInRsnExpanded(!bookInRsnExpanded)}
                            expandIcon={<ExpandMoreIcon />}
                            aria-controls="panel1a-content"
                            id="panel1a-header"
                        >
                            <Typography>Book In RSN</Typography>
                        </AccordionSummary>
                        <AccordionDetails>
                            <Grid container spacing={3}>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={formData?.rsnNumber}
                                        label="RSN Number"
                                        propertyName="rsnNumber"
                                        disabled={!formData?.ontoLocation}
                                        onChange={handleFieldChange}
                                        textFieldProps={{
                                            onBlur: () => {
                                                getRsnAccessories();
                                                getRsnConditions();
                                            }
                                        }}
                                    />
                                </Grid>
                                <Grid item xs={10} />
                            </Grid>
                        </AccordionDetails>
                    </Accordion>
                </Grid>
                <Grid item xs={12}>
                    <CheckboxWithLabel
                        label="Multiple Book In?"
                        checked={multipleBookIn}
                        onChange={() => setMultipleBookIn(m => !m)}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Button
                        variant="contained"
                        disabled={
                            !validatePurchaseOrderResult ||
                            !!validatePurchaseOrderResult?.message ||
                            !formData.ontoLocation ||
                            !formData.qty
                        }
                        onClick={() =>
                            setLines(l => [
                                ...l,
                                {
                                    id: l.length + 1,
                                    articleNumber: validatePurchaseOrderResult.partNumber,
                                    transactionType: validatePurchaseOrderResult.transactionType,
                                    dateCreated: new Date().toISOString(),
                                    storagePlace: formData.ontoLocation,
                                    locationId: formData.ontoLocationId,
                                    quantity: formData.qty,
                                    orderNumber: validatePurchaseOrderResult.orderNumber,
                                    state: validatePurchaseOrderResult.state,
                                    orderLine: validatePurchaseOrderResult.orderLine,
                                    storageType: formData.storageType,
                                    createdBy: userNumber
                                }
                            ])
                        }
                    >
                        Add Line
                    </Button>
                    <Button
                        variant="contained"
                        disabled={
                            (!validatePurchaseOrderResult && !lines.length) ||
                            !!validatePurchaseOrderResult?.message ||
                            !formData.ontoLocation ||
                            (!formData.qty && !lines.length)
                        }
                        onClick={() => {
                            const row = {
                                articleNumber: validatePurchaseOrderResult?.partNumber,
                                transactionType: formData.loanNumber
                                    ? 'L'
                                    : validatePurchaseOrderResult.transactionType,
                                dateCreated: new Date().toISOString(),
                                location: formData.ontoLocation,
                                locationId: formData.ontoLocationId,
                                quantity: formData.qty,
                                orderNumber: validatePurchaseOrderResult?.orderNumber,
                                state: validatePurchaseOrderResult?.state,
                                orderLine: validatePurchaseOrderResult?.orderLine,
                                storageType: formData.storageType,
                                createdBy: userNumber
                            };

                            doBookIn({
                                ...formData,
                                multipleBookIn,
                                lines: lines.length > 0 ? lines : [row],
                                createdBy: userNumber,
                                transactionType: formData.loanNumber
                                    ? 'L'
                                    : validatePurchaseOrderResult.transactionType,
                                partNumber: validatePurchaseOrderResult?.partNumber,
                                manufacturersPartNumber:
                                    validatePurchaseOrderResult?.manufacturersPartNumber,
                                state: validatePurchaseOrderResult?.state
                            });
                        }}
                    >
                        Book In
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    <div style={{ width: '100%', marginTop: '100px', height: '300px' }}>
                        <DataGrid
                            autoHeight
                            rows={rows}
                            columns={tableColumns}
                            density="standard"
                            rowHeight={34}
                            checkboxSelection
                            onSelectionModelChange={handleSelectRow}
                            hideFooter
                        />
                    </div>
                </Grid>
                <Grid item xs={2}>
                    <Button
                        style={{ marginTop: '22px' }}
                        variant="contained"
                        color="secondary"
                        disabled={selectedRows.length < 1}
                        onClick={() => {
                            setLines(
                                lines.filter(r => !selectedRows.map(x => x.id).includes(r.id))
                            );
                            setLogEntries(
                                logEntries.filter(r => !selectedRows.map(x => x.id).includes(r.id))
                            );
                        }}
                    >
                        Clear Selected
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
}

GoodsInUtility.propTypes = {
    validatePurchaseOrderResult: PropTypes.shape({
        bookInMessage: PropTypes.string,
        orderLine: PropTypes.number,
        state: PropTypes.string,
        orderNumber: PropTypes.number,
        documentType: PropTypes.string,
        orderQty: PropTypes.number,
        qtyBookedIn: PropTypes.number,
        storage: PropTypes.string,
        qcPart: PropTypes.string,
        partNumber: PropTypes.string,
        partDescription: PropTypes.string,
        orderUnitOfMeasure: PropTypes.string,
        manufacturersPartNumber: PropTypes.string,
        transactionType: PropTypes.string,
        message: PropTypes.string
    }),
    validateStorageType: PropTypes.func.isRequired,
    validateStorageTypeResult: PropTypes.shape({
        message: PropTypes.string,
        locationCode: PropTypes.string
    }),
    validateStorageTypeResultLoading: PropTypes.bool,
    validatePurchaseOrderResultLoading: PropTypes.bool,
    searchDemLocations: PropTypes.func.isRequired,
    validatePurchaseOrder: PropTypes.func.isRequired,
    demLocationsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    demLocationsSearchLoading: PropTypes.bool,
    searchSalesArticles: PropTypes.func.isRequired,
    salesArticlesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    salesArticlesSearchLoading: PropTypes.bool,
    searchStoragePlaces: PropTypes.func.isRequired,
    storagePlacesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    storagePlacesSearchLoading: PropTypes.bool,
    bookInResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string,
        reqNumber: PropTypes.number,
        qcState: PropTypes.string,
        docType: PropTypes.string,
        unitOfMeasure: PropTypes.string,
        qtyReceived: PropTypes.number,
        qcInfo: PropTypes.string,
        kardexLocation: PropTypes.string,
        parcelComments: PropTypes.string,
        supplierId: PropTypes.number,
        createParcel: PropTypes.bool,
        lines: PropTypes.arrayOf(PropTypes.shape({})),
        printLabels: PropTypes.bool
    }),
    bookInResultLoading: PropTypes.bool,
    doBookIn: PropTypes.func.isRequired,
    validatePurchaseOrderBookInQtyResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    validatePurchaseOrderBookInQty: PropTypes.func.isRequired,
    validatePurchaseOrderBookInQtyResultLoading: PropTypes.bool,
    userNumber: PropTypes.number.isRequired,
    match: PropTypes.shape({}).isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loanDetails: PropTypes.arrayOf(PropTypes.shape({})),
    getLoanDetails: PropTypes.func.isRequired,
    loanDetailsLoading: PropTypes.bool,
    rsnConditions: PropTypes.arrayOf(PropTypes.shape({})),
    rsnConditionsLoading: PropTypes.bool,
    getRsnConditions: PropTypes.func.isRequired,
    rsnAccessories: PropTypes.arrayOf(PropTypes.shape({})),
    rsnAccessoriesLoading: PropTypes.bool,
    getRsnAccessories: PropTypes.func.isRequired
};

GoodsInUtility.defaultProps = {
    bookInResult: null,
    bookInResultLoading: false,
    validatePurchaseOrderResult: null,
    validatePurchaseOrderResultLoading: false,
    validateStorageTypeResult: null,
    validateStorageTypeResultLoading: false,
    demLocationsSearchResults: [],
    demLocationsSearchLoading: false,
    salesArticlesSearchResults: [],
    salesArticlesSearchLoading: false,
    storagePlacesSearchResults: [],
    storagePlacesSearchLoading: false,
    validatePurchaseOrderBookInQtyResult: null,
    validatePurchaseOrderBookInQtyResultLoading: false,
    loanDetails: [],
    loanDetailsLoading: false.partNumber,
    rsnConditions: [],
    rsnConditionsLoading: false,
    rsnAccessories: [],
    rsnAccessoriesLoading: false
};

export default GoodsInUtility;
