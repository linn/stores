import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Tooltip from '@material-ui/core/Tooltip';
import Grid from '@material-ui/core/Grid';
import {
    InputField,
    Dropdown,
    Typeahead,
    LinkButton,
    SearchInputField,
    SingleEditTable
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';

function OrderDetailsTab({
    orderDetails,
    impbookId,
    handleFieldChange,
    remainingTotal,
    remainingDutyTotal,
    remainingWeight,
    invoiceDate,
    handleOrderDetailChange,
    cpcNumbers
}) {

    const [supplier, setSupplier] = useState({ id: -1, name: 'loading', country: 'loading' });

    const handleCarrierChange = carrierParam => {
        handleFieldChange('carrierId', carrierParam.id);
    };

    const updateRow = detail => {
        handleOrderDetailChange(detail.lineNumber, detail);
    };

    const useStyles = makeStyles(theme => ({
        displayInline: {
            display: 'inline'
        },
        marginTop1: {
            marginTop: theme.spacing(1),
            display: 'inline-block',
            width: '2em'
        },
        gapAbove: {
            marginTop: theme.spacing(8)
        },
        negativeTopMargin: {
            marginTop: theme.spacing(-4)
        }
    }));
    const classes = useStyles();

    const columns = [
        {
            title: 'Line Type',
            id: 'lineType',
            type: 'dropdown',
            //fill out options
            options: ['PO', 'RSN', 'RETURNS', 'RO', 'LOAN', 'SAMPLES', 'SUNDRY', 'INS'],
            editable: false
        },
        {
            title: 'Order Number',
            id: 'orderNumber',
            type: 'text',
            editable: false
        },
        {
            title: 'Rsn Number',
            id: 'rsnNumber',
            type: 'text',
            editable: false
        },
        {
            title: 'Loan Number',
            id: 'loanNumber',
            type: 'number',
            editable: false
        },
        {
            title: 'Ins Number',
            id: 'insNumber',
            type: 'number',
            editable: false
        },
        {
            title: 'Order Description',
            id: 'orderDescription',
            type: 'text',
            editable: false
        },
        {
            title: 'Order Description',
            id: 'orderDescription',
            type: 'text',
            editable: false
        },
        {
            title: 'Tariff Number',
            id: 'tariffCode',
            type: 'text',
            editable: false
        },
        {
            title: 'Qty',
            id: 'qty',
            type: 'number',
            editable: false
        },
        {
            title: 'Order Value',
            id: 'orderValue',
            type: 'number',
            editable: false
        },
        {
            title: 'Duty Value',
            id: 'dutyValue',
            type: 'number',
            editable: false
        },
        {
            title: 'VatRate',
            id: 'vatRate',
            type: 'number',
            editable: false
        },
        {
            title: 'VatValue',
            id: 'vatValue',
            type: 'number',
            editable: false
        },
        {
            title: 'Weight',
            id: 'weight',
            type: 'number',
            editable: false
        },
        {
            title: 'Cpc Number',
            id: 'cpcNumber',
            type: 'dropdown',
            options: cpcNumbers
            //todo fill out this with dropdown
            //include something to make it obvious that the one with id 13 is IPR
            // and not the very similar code with extra 0s - an extra badge or "IPR" button to set it to id 13?

            //todo work out if need below when I just want it to act like every other field
            //selectSearchResult: update
        },
        {
            title: 'Post Duty',
            id: 'postDuty',
            type: 'checkbox',
            //todo figure out what this is meant to do - it doesn't seem to be stored in db so must be to run an operation related to the duty?
            //also check if freightValue is ever needed, it isn't in the old form
            editable: false
        }
    ];

    //todo
    // BEGIN
//     IF :IMPBOOK_ORDER_DETAIL.LINE_TYPE IN ('PO','RO') THEN
//     ACTIVATE_FIELD('ORDER_NUMBER');
//     DEACTIVATE_FIELD('RSN_NUMBER');
//     DEACTIVATE_FIELD('LOAN_NUMBER');
//     DEACTIVATE_FIELD('INS_NUMBER2');
// ELSIF :IMPBOOK_ORDER_DETAIL.LINE_TYPE = 'RSN' THEN
//     ACTIVATE_FIELD('RSN_NUMBER');
//     DEACTIVATE_FIELD('ORDER_NUMBER');
//     DEACTIVATE_FIELD('LOAN_NUMBER');
//     DEACTIVATE_FIELD('INS_NUMBER2');
// ELSIF :IMPBOOK_ORDER_DETAIL.LINE_TYPE = 'LOAN' THEN
//     ACTIVATE_FIELD('LOAN_NUMBER');
//     DEACTIVATE_FIELD('RSN_NUMBER');
//     DEACTIVATE_FIELD('ORDER_NUMBER');
//     DEACTIVATE_FIELD('INS_NUMBER2');
// ELSIF :IMPBOOK_ORDER_DETAIL.LINE_TYPE = 'INS' THEN
//     DEACTIVATE_FIELD('LOAN_NUMBER');
//     DEACTIVATE_FIELD('RSN_NUMBER');
//     DEACTIVATE_FIELD('ORDER_NUMBER');
//     ACTIVATE_FIELD('INS_NUMBER2');
// ELSE
//     DEACTIVATE_FIELD('LOAN_NUMBER');
//     DEACTIVATE_FIELD('RSN_NUMBER');
//     DEACTIVATE_FIELD('ORDER_NUMBER');
//     DEACTIVATE_FIELD('INS_NUMBER2');
// END IF;


// if :IMPBOOK_ORDER_DETAIL.LINE_TYPE IN ('RSN','RETURNS','LOAN') then
//  :impbook_order_detail.vat_rate := 0;
// else
//      :impbook_order_detail.vat_rate := sales_tax_pack.GET_VAT_RATE_country(:impbook.l_supp_country);
// end if;

    return (
        <>
            <Grid container spacing={1} item xs={7}>
                <Grid item xs={3}>
                    <InputField
                        label="Remaining Total"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="remainingTotal"
                        type="number"
                        value={remainingTotal}
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="Remaining Duty Total"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="remainingDutyTotal"
                        type="number"
                        value={remainingDutyTotal}
                        required
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="Remaining Weight"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="remainingWeight"
                        type="number"
                        value={remainingWeight}
                    />
                </Grid>

                <Grid item xs={3}>
                    <LinkButton
                        text="Calculate Weights"
                        // to={`/logistics/parcels/${parcelNumber}`}
                        disabled
                        external
                    />
                </Grid>

                <Grid item xs={3}>
                    <InputField
                        label="Invoice Date"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="invoiceDate"
                        type="date"
                        value={invoiceDate}
                    />
                </Grid>
                <Grid item xs={3}>
                    <LinkButton
                        text="Post Duty"
                        // to={`/logistics/parcels/${parcelNumber}`}
                        disabled
                        external
                    />
                </Grid>
            </Grid>

            <Grid container spacing={1} item xs={12} className={classes.gapAbove}>
                <SingleEditTable
                    columns={columns}
                    rows={orderDetails ?? [{}]}
                    saveRow={updateRow}
                    // editable={!displayOnly}
                    //todo add createRow function
                    allowNewRowCreation={false}
                />
            </Grid>
        </>
    );
}

OrderDetailsTab.propTypes = {
    orderDetails: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    handleFieldChange: PropTypes.func.isRequired
};

OrderDetailsTab.defaultProps = {};

export default OrderDetailsTab;
