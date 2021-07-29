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
import { InputField, Typeahead, Dropdown, DatePicker } from '@linn-it/linn-form-components-library';
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
    validatePurchaseOrderBookInQtyResultLoading
}) {
    const [formData, setFormData] = useState({
        purchaseOrderNumber: null,
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

    const [dialogOpen, setDialogOpen] = useState(false);

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
                                bookinLocationId={formData?.ontoLocationId}
                                palletNumber={formData?.palletNumber}
                                partNumber={validatePurchaseOrderResult?.partNumber}
                                reqNumber={bookInResult?.reqNumber}
                                qcState={bookInResult?.qcState}
                                docType={bookInResult?.docType}
                            />
                        </div>
                    </div>
                </Dialog>
                <Grid item xs={12}>
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
                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={formData.purchaseOrderNumber}
                        label="PO Number"
                        disabled={validatePurchaseOrderResultLoading}
                        propertyName="purchaseOrderNumber"
                        onChange={handleFieldChange}
                        textFieldProps={{
                            onBlur: () =>
                                formData.purchaseOrderNumber
                                    ? validatePurchaseOrder(formData.purchaseOrderNumber)
                                    : {}
                        }}
                    />
                </Grid>
                <Grid item xs={1}>
                    <InputField
                        fullWidth
                        value={validatePurchaseOrderResult?.orderLine}
                        disabled
                        label="Line"
                        propertyName="orderLine"
                        onChange={handleFieldChange}
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
                                    formData.purchaseOrderNumber
                                )
                        }}
                        onChange={handleFieldChange}
                    />
                </Grid>
                <Grid item xs={2}>
                    <InputField
                        fullWidth
                        value={formData.sType}
                        label="S/Type"
                        propertyName="sType"
                        onChange={handleFieldChange}
                    />
                </Grid>
                <Grid item xs={4} />
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
                <Grid item xs={2}>
                    <InputField
                        fullWidth
                        value={formData.bookInRef}
                        label="Bookin Ref"
                        propertyName="bookInRef"
                        onChange={handleFieldChange}
                    />
                </Grid>
                <Grid item xs={1}>
                    <InputField
                        fullWidth
                        value={formData.numberOfLines}
                        label="#Lines"
                        propertyName="numberOfLines"
                        onChange={handleFieldChange}
                    />
                </Grid>
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
                        className="hide-when-printing"
                        variant="contained"
                        onClick={() => doBookIn(formData)}
                    >
                        Book In
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
        manufacturersPartNumber: PropTypes.string
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
        message: PropTypes.string
    }),
    bookInResultLoading: PropTypes.bool,
    doBookIn: PropTypes.func.isRequired,
    validatePurchaseOrderBookInQtyResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    validatePurchaseOrderBookInQty: PropTypes.func.isRequired,
    validatePurchaseOrderBookInQtyResultLoading: PropTypes.bool
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
