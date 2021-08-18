import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    Dropdown,
    InputField,
    LinkButton,
    SingleEditTable
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';

function OrderDetailsTab({
    orderDetails,
    handleFieldChange,
    remainingTotal,
    remainingDutyTotal,
    remainingWeight,
    invoiceDate,
    handleOrderDetailChange,
    cpcNumbers,
    allowedToEdit
}) {
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

    const lineTypes = ['PO', 'RSN', 'RETURNS', 'RO', 'LOAN', 'SAMPLES', 'SUNDRY', 'INS'];

    // const EditableRow = ({ row, updateRow }) => {
    const editRow = (row, propertyName, newValue) => {
        updateRow({ ...row, [propertyName]: newValue });
    };

    // return (
    //     <>
    //         <Grid item xs={2}>
    //             <Dropdown
    //                 items={lineTypes}
    //                 label="Line Type"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="lineType"
    //                 value={row.lineType}
    //             />
    //         </Grid>

    //         {(row.lineType === 'PO' || row.lineType === 'RO') && (
    //             <Grid item xs={2}>
    //                 <InputField
    //                     label="Order Number"
    //                     fullWidth
    //                     onChange={editRow}
    //                     propertyName="orderNumber"
    //                     type="number"
    //                     value={row.orderNumber}
    //                 />
    //             </Grid>
    //         )}

    //         {row.lineType === 'RSN' && (
    //             <Grid item xs={2}>
    //                 <InputField
    //                     label="RSN Number"
    //                     fullWidth
    //                     onChange={editRow}
    //                     propertyName="rsnNumber"
    //                     type="number"
    //                     value={row.rsnNumber}
    //                 />
    //             </Grid>
    //         )}

    //         {row.lineType === 'LOAN' && (
    //             <Grid item xs={2}>
    //                 <InputField
    //                     label="Loan Number"
    //                     fullWidth
    //                     onChange={editRow}
    //                     propertyName="loanNumber"
    //                     type="number"
    //                     value={row.loanNumber}
    //                 />
    //             </Grid>
    //         )}

    //         {row.lineType === 'INS' && (
    //             <Grid item xs={2}>
    //                 <InputField
    //                     label="Ins Number"
    //                     fullWidth
    //                     onChange={editRow}
    //                     propertyName="insNumber"
    //                     type="number"
    //                     value={row.insNumber}
    //                 />
    //             </Grid>
    //         )}

    //         <Grid item xs={2}>
    //             <InputField
    //                 label="Order Description"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="orderDescription"
    //                 type="text"
    //                 value={row.orderDescription}
    //             />
    //         </Grid>
    //         <Grid item xs={2}>
    //             <InputField
    //                 label="Tariff Code"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="tariffCode"
    //                 type="text"
    //                 value={row.tariffCode}
    //             />
    //         </Grid>
    //         <Grid item xs={2}>
    //             <InputField
    //                 label="Tariff Number"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="tariffNumber"
    //                 type="number"
    //                 value={row.tariffNumber}
    //             />
    //         </Grid>
    //         <Grid item xs={2}>
    //             <InputField
    //                 label="Qty"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="qty"
    //                 type="number"
    //                 value={row.qty}
    //             />
    //         </Grid>
    //         <Grid item xs={2}>
    //             <InputField
    //                 label="Order Value"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="orderValue"
    //                 type="number"
    //                 value={row.orderValue}
    //             />
    //         </Grid>
    //         <Grid item xs={2}>
    //             <InputField
    //                 label="Duty Value"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="dutyValue"
    //                 type="number"
    //                 value={row.dutyValue}
    //             />
    //         </Grid>
    //         <Grid item xs={2}>
    //             <InputField
    //                 label="Vat Rate"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="vatRate"
    //                 type="number"
    //                 value={row.vatRate}
    //             />
    //         </Grid>
    //         <Grid item xs={2}>
    //             <InputField
    //                 label="Vat Value"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="vatValue"
    //                 type="number"
    //                 value={row.vatValue}
    //             />
    //         </Grid>
    //         <Grid item xs={2}>
    //             <InputField
    //                 label="Weight"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="weight"
    //                 type="number"
    //                 value={row.weight}
    //             />
    //         </Grid>
    //         <Grid item xs={2}>
    //             <Dropdown
    //                 items={cpcNumbers}
    //                 label="Cpc Number"
    //                 fullWidth
    //                 onChange={editRow}
    //                 propertyName="cpcNumber"
    //                 value={row.cpcNumber}
    //             />
    //         </Grid>
    //         <Grid item xs={2}>
    //             {/* //todo check that this works when editing is sorted & that
    //     // "IPR" text for id 13 is obvious enough to prevent mistakes
    //     // if not maybe consider a popup or an IPR button to fill in the IPR cpc number

    //     //also work out if need below when I just want it to act like every other field
    //     //selectSearchResult: update */}
    //         </Grid>
    //         <Grid item xs={2}>
    //             <LinkButton
    //                 text="Post Duty"
    //                 //todo to={`/logistics/`}
    //                 disabled
    //                 external
    //             />
    //         </Grid>
    //         </>
    //     );
    // };
    //todo - implement the below checks to disable fields
    //looks like I'll need to edit the shared components to check for disabled with the row

    //maybe could make this just new components for each linetype? Like a small component with the fields
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
                {/* Are these fields even used? Check with Rhona */}
                <Grid item xs={3}>
                    <LinkButton
                        text="Calculate Weights"
                        //todo to={`/logistics/`}
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
                        //todo to={`/logistics/`}
                        disabled
                        external
                    />
                </Grid>
            </Grid>

            {/* foreach orderDetails 
            
            if lineType = blah, use blah row component with those fields

            create needs a dropdown for each type
            can't edit the row type, delete the row and make a new one for ease?
            */}

            <Grid container spacing={1} item xs={12} className={classes.gapAbove}>
                {orderDetails.map(row => (
                    <>
                        <Grid item xs={2}>
                            <Dropdown
                                items={lineTypes}
                                label="Line Type"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="lineType"
                                value={row.lineType}
                            />
                        </Grid>

                        {(row.lineType === 'PO' || row.lineType === 'RO') && (
                            <Grid item xs={2}>
                                <InputField
                                    label="Order Number"
                                    fullWidth
                                    onChange={(propertyName, newValue) =>
                                        editRow(row, propertyName, newValue)
                                    }
                                    propertyName="orderNumber"
                                    type="number"
                                    value={row.orderNumber}
                                />
                            </Grid>
                        )}

                        {row.lineType === 'RSN' && (
                            <Grid item xs={2}>
                                <InputField
                                    label="RSN Number"
                                    fullWidth
                                    onChange={(propertyName, newValue) =>
                                        editRow(row, propertyName, newValue)
                                    }
                                    propertyName="rsnNumber"
                                    type="number"
                                    value={row.rsnNumber}
                                />
                            </Grid>
                        )}

                        {row.lineType === 'LOAN' && (
                            <Grid item xs={2}>
                                <InputField
                                    label="Loan Number"
                                    fullWidth
                                    onChange={(propertyName, newValue) =>
                                        editRow(row, propertyName, newValue)
                                    }
                                    propertyName="loanNumber"
                                    type="number"
                                    value={row.loanNumber}
                                />
                            </Grid>
                        )}

                        {row.lineType === 'INS' && (
                            <Grid item xs={2}>
                                <InputField
                                    label="Ins Number"
                                    fullWidth
                                    onChange={(propertyName, newValue) =>
                                        editRow(row, propertyName, newValue)
                                    }
                                    propertyName="insNumber"
                                    type="number"
                                    value={row.insNumber}
                                />
                            </Grid>
                        )}

                        <Grid item xs={2}>
                            <InputField
                                label="Order Description"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="orderDescription"
                                type="text"
                                value={row.orderDescription}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                label="Tariff Code"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="tariffCode"
                                type="text"
                                value={row.tariffCode}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                label="Tariff Number"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="tariffNumber"
                                type="number"
                                value={row.tariffNumber}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                label="Qty"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="qty"
                                type="number"
                                value={row.qty}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                label="Order Value"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="orderValue"
                                type="number"
                                value={row.orderValue}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                label="Duty Value"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="dutyValue"
                                type="number"
                                value={row.dutyValue}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                label="Vat Rate"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="vatRate"
                                type="number"
                                value={row.vatRate}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                label="Vat Value"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="vatValue"
                                type="number"
                                value={row.vatValue}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                label="Weight"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="weight"
                                type="number"
                                value={row.weight}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <Dropdown
                                items={cpcNumbers}
                                label="Cpc Number"
                                fullWidth
                                onChange={(propertyName, newValue) =>
                                    editRow(row, propertyName, newValue)
                                }
                                propertyName="cpcNumber"
                                value={row.cpcNumber}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            {/* //todo check that this works when editing is sorted & that
            // "IPR" text for id 13 is obvious enough to prevent mistakes
            // if not maybe consider a popup or an IPR button to fill in the IPR cpc number

            //also work out if need below when I just want it to act like every other field
            //selectSearchResult: update */}
                        </Grid>
                        <Grid item xs={2}>
                            <LinkButton
                                text="Post Duty"
                                //todo to={`/logistics/`}
                                disabled
                                external
                            />
                        </Grid>

                        <Grid item xs={12} />
                    </>
                ))}

                {/* <SingleEditTable
                    columns={columns}
                    rows={orderDetails ?? [{}]}
                    saveRow={updateRow}
                    // editable={!displayOnly}
                    //todo add createRow function
                    allowNewRowCreation={false}
                /> */}
            </Grid>
        </>
    );
}

OrderDetailsTab.propTypes = {
    orderDetails: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    handleFieldChange: PropTypes.func.isRequired,
    remainingTotal: PropTypes.number.isRequired,
    remainingDutyTotal: PropTypes.number.isRequired,
    remainingWeight: PropTypes.number.isRequired,
    invoiceDate: PropTypes.string.isRequired,
    handleOrderDetailChange: PropTypes.func.isRequired,
    cpcNumbers: PropTypes.arrayOf(PropTypes.shape({})).isRequired
};

OrderDetailsTab.defaultProps = {};

export default OrderDetailsTab;
