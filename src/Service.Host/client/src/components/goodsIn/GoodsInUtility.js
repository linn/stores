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
import { DataGrid } from '@material-ui/data-grid';
import {
    InputField,
    Typeahead,
    Dropdown,
    DatePicker,
    Title
} from '@linn-it/linn-form-components-library';
import QcLabelPrintScreen from '../../containers/goodsIn/QcLabelPrintScreen';
import Page from '../../containers/Page';

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
    userNumber
}) {
    const [formData, setFormData] = useState({
        orderNumber: null,
        dateReceived: new Date(),
        lines: []
    });

    const useStyles = makeStyles(theme => ({
        dialog: {
            margin: theme.spacing(6),
            minWidth: theme.spacing(62)
        }
    }));

    const [message, setMessage] = useState({ error: false, text: '' });

    const handleFieldChange = (propertyName, newValue) => {
        setFormData({ ...formData, [propertyName]: newValue });
    };
    const [bookInPoExpanded, setBookInPoExpanded] = useState(false);

    const [dialogOpen, setDialogOpen] = useState(true);

    useEffect(() => {
        if (validatePurchaseOrderResult?.documentType === 'PO') {
            setBookInPoExpanded(true);
        } else {
            setBookInPoExpanded(false);
        }
    }, [validatePurchaseOrderResult]);

    useEffect(() => {
        if (validatePurchaseOrderBookInQtyResult?.success) {
            setFormData(d => ({ ...d, numberOfLines: 1 }));
        }

        setMessage({
            error: !validatePurchaseOrderBookInQtyResult?.success,
            text: validatePurchaseOrderBookInQtyResult?.message
        });
    }, [validatePurchaseOrderBookInQtyResult]);

    useEffect(() => {
        if (bookInResult?.message) {
            setMessage({ error: false, text: bookInResult.message });
        }
        if (bookInResult?.success) {
            setDialogOpen(true);
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
            field: 'location',
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

    const [rows, setRows] = useState([]);
    const [selectedRows, setSelectedRows] = useState([]);

    const handleSelectRow = selected => {
        setSelectedRows(rows.filter(r => selected.rowIds.includes(r.id.toString())));
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Dialog open={dialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <QcLabelPrintScreen
                                // partNumber={validatePurchaseOrderResult?.partNumber}
                                // partDescription={validatePurchaseOrder?.description}
                                // reqNumber={bookInResult?.reqNumber}
                                // qcState={bookInResult?.qcState}
                                // qcInfo={bookInResult?.qcInfo}
                                // docType={bookInResult?.docType}
                                // unitOfMeasure={bookInResult?.unitOfMeasure}
                                // qtyReceived={bookInResult?.qtyReceived}
                                partNumber="PART"
                                partDescription="DESCRIPTION"
                                reqNumber={12345}
                                qcState="PASS"
                                qcInfo="info"
                                docType="PO"
                                unitOfMeasure="ONES"
                                qtyReceived={1}
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
                        value={
                            validatePurchaseOrderResultLoading ||
                            validatePurchaseOrderBookInQtyResultLoading ||
                            bookInResultLoading
                                ? 'loading'
                                : message.text
                        }
                        label="Message"
                        propertyName="bookInMessage"
                    />
                </Grid>
                <Grid item xs={6} />

                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={formData.orderNumber}
                        label="PO Number"
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
                        propertyName="storageType"
                        onChange={handleFieldChange}
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
                        value={validatePurchaseOrderResult?.state}
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
                                        propertyName="orderNumber"
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
                                        label="Qty"
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
                    <Button
                        variant="contained"
                        disabled={
                            !validatePurchaseOrderResult ||
                            !formData.ontoLocationId ||
                            !formData.qty
                        }
                        onClick={() =>
                            setRows(r => [
                                ...r,
                                {
                                    id: r.length + 1,
                                    articleNumber: validatePurchaseOrderResult.partNumber,
                                    transactionType: validatePurchaseOrderResult.transactionType,
                                    dateCreated: new Date().toISOString(),
                                    location: formData.ontoLocation,
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
                        onClick={() => {
                            const row = {
                                id: 1,
                                articleNumber: validatePurchaseOrderResult.partNumber,
                                transactionType: validatePurchaseOrderResult.transactionType,
                                dateCreated: new Date().toISOString(),
                                location: formData.ontoLocation,
                                locationId: formData.ontoLocationId,
                                quantity: formData.qty,
                                orderNumber: validatePurchaseOrderResult.orderNumber,
                                state: validatePurchaseOrderResult.state,
                                orderLine: validatePurchaseOrderResult.orderLine,
                                storageType: formData.storageType,
                                createdBy: userNumber
                            };
                            if (rows.length === 0) {
                                setRows(r => [...r, row]);
                            }
                            doBookIn({
                                ...formData,
                                lines: rows.length > 0 ? rows : [row],
                                createdBy: userNumber,
                                transactionType: validatePurchaseOrderResult.transactionType,
                                partNumber: validatePurchaseOrderResult.partNumber,
                                manufacturersPartNumber:
                                    validatePurchaseOrderResult.manufacturersPartNumber,
                                state: validatePurchaseOrderResult.state
                            });
                        }}
                    >
                        Book In
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    <div style={{ height: 250, width: '100%', marginTop: '100px' }}>
                        <DataGrid
                            rows={rows}
                            columns={tableColumns}
                            density="standard"
                            rowHeight={34}
                            checkboxSelection
                            onSelectionChange={handleSelectRow}
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
                        onClick={() =>
                            setRows(rows.filter(r => !selectedRows.map(x => x.id).includes(r.id)))
                        }
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
        transactionType: PropTypes.string
    }),
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
        qcInfo: PropTypes.string
    }),
    bookInResultLoading: PropTypes.bool,
    doBookIn: PropTypes.func.isRequired,
    validatePurchaseOrderBookInQtyResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    validatePurchaseOrderBookInQty: PropTypes.func.isRequired,
    validatePurchaseOrderBookInQtyResultLoading: PropTypes.bool,
    userNumber: PropTypes.number.isRequired
};

GoodsInUtility.defaultProps = {
    bookInResult: null,
    bookInResultLoading: false.valueOf,
    validatePurchaseOrderResult: null,
    validatePurchaseOrderResultLoading: false,
    demLocationsSearchResults: [],
    demLocationsSearchLoading: false,
    salesArticlesSearchResults: [],
    salesArticlesSearchLoading: false,
    storagePlacesSearchResults: [],
    storagePlacesSearchLoading: false,
    validatePurchaseOrderBookInQtyResult: null,
    validatePurchaseOrderBookInQtyResultLoading: false
};

export default GoodsInUtility;
