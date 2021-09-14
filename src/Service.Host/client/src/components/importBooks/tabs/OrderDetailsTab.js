import React from 'react';
import { Decimal } from 'decimal.js';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Divider from '@material-ui/core/Divider';
import Tooltip from '@material-ui/core/Tooltip';
import DeleteIcon from '@material-ui/icons/Delete';
import { Dropdown, InputField, LinkButton } from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import { ContactsOutlined } from '@material-ui/icons';

function OrderDetailsTab({
    orderDetails,
    handleFieldChange,
    invoiceDate,
    handleOrderDetailChange,
    cpcNumbers,
    allowedToEdit,
    addOrderDetailRow,
    removeOrderDetailRow,
    totalInvoiceValue,
    duty,
    weight
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
            marginTop: theme.spacing(2)
        },
        dividerMargins: {
            marginTop: '10px',
            marginBottom: '10px'
        },
        dividerMarginBottomOnly: {
            marginBottom: '10px'
        }
    }));
    const classes = useStyles();

    const lineTypes = [
        { id: 'PO', displayText: 'PO' },
        { id: 'RSN', displayText: 'RSN' },
        { id: 'RETURNS', displayText: 'RETURNS' },
        { id: 'LOAN', displayText: 'LOAN' },
        { id: 'SAMPLES', displayText: 'SAMPLES' },
        { id: 'SUNDRY', displayText: 'SUNDRY' },
        { id: 'INS', displayText: 'INS' }
    ];

    const editRow = (row, propertyName, newValue) => {
        updateRow({ ...row, [propertyName]: newValue });
    };

    const calcRemainingTotal = () => {
        const orderDetailsTotal = orderDetails?.reduce(
            (a, v) => new Decimal(a).plus(v.orderValue),
            0
        );

        if (!orderDetailsTotal) {
            return totalInvoiceValue;
        }

        if (!totalInvoiceValue) {
            return orderDetailsTotal.isZero() ? 0 : orderDetailsTotal.neg();
        }

        return new Decimal(totalInvoiceValue).minus(orderDetailsTotal).valueOf();
    };

    const calcRemainingDuty = () => {
        const orderDetailsDutyTotal = orderDetails?.reduce(
            (a, v) => new Decimal(a).plus(v.dutyValue),
            0
        );
        if (!orderDetailsDutyTotal) {
            return duty;
        }

        if (!duty) {
            return orderDetailsDutyTotal.isZero() ? 0 : orderDetailsDutyTotal.neg();
        }

        return new Decimal(duty).minus(orderDetailsDutyTotal).valueOf();
    };

    const calcRemainingWeight = () => {
        const orderDetailsWeightTotal = orderDetails?.reduce(
            (a, v) => new Decimal(a).plus(v.weight),
            0
        );

        if (!orderDetailsWeightTotal) {
            return duty;
        }

        if (!weight) {
            return orderDetailsWeightTotal.isZero() ? 0 : orderDetailsWeightTotal.neg();
        }

        return new Decimal(weight).minus(orderDetailsWeightTotal).valueOf();
    };

    return (
        <>
            <Grid container spacing={1} item xs={7}>
                <Grid item xs={3}>
                    <InputField
                        label="Remaining Total"
                        fullWidth
                        propertyName="remainingTotal"
                        type="number"
                        value={calcRemainingTotal()}
                        disabled
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="Remaining Duty Total"
                        fullWidth
                        propertyName="remainingDutyTotal"
                        type="number"
                        value={calcRemainingDuty()}
                        required
                        disabled
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="Remaining Weight"
                        fullWidth
                        propertyName="remainingWeight"
                        type="number"
                        value={calcRemainingWeight()}
                        disabled
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
                        disabled={!allowedToEdit}
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

            <Grid container spacing={1} item xs={12} className={classes.gapAbove}>
                <Grid item xs={12}>
                    <Divider className={classes.dividerMarginBottomOnly} />
                </Grid>

                {orderDetails
                    ?.sort((a, b) => {
                        return a.lineNumber - b.lineNumber;
                    })
                    .map(row => (
                        <>
                            <Grid item xs={1}>
                                <Dropdown
                                    items={lineTypes}
                                    label="Line Type"
                                    fullWidth
                                    onChange={(propertyName, newValue) =>
                                        editRow(row, propertyName, newValue)
                                    }
                                    propertyName="lineType"
                                    value={row.lineType}
                                    disabled={!allowedToEdit}
                                    required
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
                                        disabled={!allowedToEdit}
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
                                        disabled={!allowedToEdit}
                                        maxLength={6}
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
                                        disabled={!allowedToEdit}
                                        maxLength={6}
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
                                        disabled={!allowedToEdit}
                                        maxLength={10}
                                    />
                                </Grid>
                            )}

                            <Grid item xs={3}>
                                <InputField
                                    label="Order Description"
                                    fullWidth
                                    onChange={(propertyName, newValue) =>
                                        editRow(row, propertyName, newValue)
                                    }
                                    propertyName="orderDescription"
                                    type="text"
                                    value={row.orderDescription}
                                    disabled={!allowedToEdit}
                                    required
                                    maxLength={2000}
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
                                    disabled={!allowedToEdit}
                                    maxLength={12}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    label="Tariff Number"
                                    fullWidth
                                    onChange={(propertyName, newValue) =>
                                        editRow(row, propertyName, newValue)
                                    }
                                    propertyName="tariffNumber"
                                    type="number"
                                    value={row.tariffNumber}
                                    disabled={!allowedToEdit}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    label="Qty"
                                    fullWidth
                                    onChange={(propertyName, newValue) =>
                                        editRow(row, propertyName, newValue)
                                    }
                                    propertyName="qty"
                                    type="number"
                                    value={row.qty}
                                    disabled={!allowedToEdit}
                                    required
                                    maxLength={6}
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
                                    disabled={!allowedToEdit}
                                    required
                                    maxLength={14}
                                    decimalPlaces={2}
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
                                    disabled={!allowedToEdit}
                                    required
                                    maxLength={14}
                                    decimalPlaces={2}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    label="Vat Rate"
                                    fullWidth
                                    onChange={(propertyName, newValue) =>
                                        editRow(row, propertyName, newValue)
                                    }
                                    propertyName="vatRate"
                                    type="number"
                                    value={row.vatRate}
                                    disabled={!allowedToEdit}
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
                                    disabled={!allowedToEdit}
                                    required
                                    maxLength={14}
                                    decimalPlaces={2}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    label="Weight"
                                    fullWidth
                                    onChange={(propertyName, newValue) =>
                                        editRow(row, propertyName, newValue)
                                    }
                                    propertyName="weight"
                                    type="number"
                                    value={row.weight}
                                    disabled={!allowedToEdit}
                                    required
                                    maxLength={10}
                                    decimalPlaces={2}
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
                                    disabled={!allowedToEdit}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <LinkButton
                                    text="Post Duty"
                                    //todo to={`/logistics/`}
                                    disabled
                                    external
                                />
                            </Grid>

                            <Grid item xs={2}>
                                <Tooltip title="Remove order detail" aria-label="add">
                                    <Button
                                        className={classes.marginTop1}
                                        onClick={() => removeOrderDetailRow(row.lineNumber)}
                                        disabled={!allowedToEdit}
                                    >
                                        <DeleteIcon data-testid="deleteIcon" />
                                    </Button>
                                </Tooltip>
                            </Grid>

                            <Grid item xs={12}>
                                <Divider className={classes.dividerMargins} />
                            </Grid>
                        </>
                    ))}

                <Button
                    style={{ marginTop: '22px' }}
                    variant="contained"
                    onClick={addOrderDetailRow}
                    disabled={!allowedToEdit}
                >
                    Add new order detail
                </Button>
            </Grid>
        </>
    );
}

OrderDetailsTab.propTypes = {
    orderDetails: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    handleFieldChange: PropTypes.func.isRequired,
    totalInvoiceValue: PropTypes.number.isRequired,
    duty: PropTypes.number.isRequired,
    weight: PropTypes.number.isRequired,
    invoiceDate: PropTypes.string.isRequired,
    handleOrderDetailChange: PropTypes.func.isRequired,
    cpcNumbers: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    allowedToEdit: PropTypes.bool.isRequired,
    addOrderDetailRow: PropTypes.func.isRequired,
    removeOrderDetailRow: PropTypes.func.isRequired
};

OrderDetailsTab.defaultProps = {};

export default OrderDetailsTab;
