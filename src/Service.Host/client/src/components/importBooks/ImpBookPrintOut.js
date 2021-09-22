import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { TableWithInlineEditing } from '@linn-it/linn-form-components-library';
import Divider from '@material-ui/core/Divider';
import { makeStyles } from '@material-ui/styles';
import { Typography } from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import InputLabel from '@material-ui/core/InputLabel';

function ImpBookPrintOut({
    impbookId,
    dateCreated,
    createdBy,
    supplierId,
    supplierName,
    supplierCountry,
    eecMember,
    currency,
    parcelNumber,
    totalImportValue,
    invoiceDetails,
    carrierId,
    carrierName,
    transportCode,
    transportBillNumber,
    transactionCode,
    numPallets,
    numCartons,
    weight,
    deliveryTermCode,
    customsEntryCodePrefix,
    customsEntryCode,
    customsEntryCodeDate,
    linnDuty,
    linnVat,
    arrivalDate,
    remainingInvoiceValue,
    remainingDutyValue,
    remainingWeightValue,
    orderDetails,
    comments,
    arrivalPort
}) {
    const useStyles = makeStyles(theme => ({
        gapAbove: {
            marginTop: theme.spacing(4)
        },
        dividerMargins: {
            marginTop: '10px',
            marginBottom: '10px'
        }
    }));
    const classes = useStyles();

    return (
        <>
            <Grid container spacing={1}>
                <Grid item xs={4}>
                    Impbook ID: <b>{impbookId}</b>
                </Grid>

                <Grid item xs={4}>
                    Date Created: <b>{dateCreated}</b>
                </Grid>
                <Grid item xs={4}>
                    Created By: <b>{createdBy}</b>
                </Grid>

                <Grid item xs={4}>
                    Supplier:
                    <b>
                        {supplierId} - {supplierName}
                    </b>
                </Grid>
                <Grid item xs={4}>
                    Country: <b>{supplierCountry}</b>
                </Grid>
                <Grid item xs={4}>
                    EEC Member: <b>{eecMember}</b>
                </Grid>

                <Grid item xs={12}>
                    Currency: <b>{currency}</b>
                </Grid>
                <Grid item xs={12}>
                    Parcel Number: <b>{parcelNumber}</b>
                </Grid>

                <Grid item xs={12} className={classes.gapAbove}>
                    <Grid item xs={12}>
                        <Typography variant="h6">Invoice Details</Typography>
                    </Grid>
                    <TableWithInlineEditing
                        columnsInfo={[
                            {
                                title: 'Invoice Number',
                                key: 'invoiceNumber',
                                type: 'text'
                            },
                            {
                                title: 'Invoice Value (Currency)',
                                key: 'invoiceValue',
                                type: 'number',
                                decimalPlaces: 2
                            }
                        ]}
                        content={invoiceDetails ?? [{}]}
                        updateContent={() => {}}
                        allowedToEdit={false}
                        allowedToCreate={false}
                        allowedToDelete={false}
                    />
                </Grid>

                <Grid item xs={12} className={classes.gapAbove} />

                <Grid item xs={4} />
                <Grid item xs={4}>
                    Total Import Value (GBP): <b>{totalImportValue}</b>
                </Grid>
                <Grid item xs={4} />

                <Grid item xs={12} className={classes.gapAbove} />

                <Grid item xs={9}>
                    Carrier:
                    <b>
                        {carrierId} - {carrierName}
                    </b>
                </Grid>
                <Grid item xs={3}>
                    Num Cartons: <b>{numCartons}</b>
                </Grid>

                <Grid item xs={9}>
                    Transport Code: <b>{transportCode}</b>
                </Grid>
                <Grid item xs={3}>
                    Num Pallets: <b>{numPallets}</b>
                </Grid>

                <Grid item xs={9}>
                    Transport Bill Number: <b>{transportBillNumber}</b>
                </Grid>
                <Grid item xs={3}>
                    Weight: <b>{weight}</b>
                </Grid>

                <Grid item xs={9}>
                    Transaction Code: <b>{transactionCode}</b>
                </Grid>
                <Grid item xs={3}>
                    Customs Entry Code:{' '}
                    <b>
                        {customsEntryCodePrefix} {customsEntryCode}
                    </b>
                </Grid>

                <Grid item xs={9}>
                    Delivery Term Code: <b>{deliveryTermCode}</b>
                </Grid>
                <Grid item xs={3}>
                    Customs Entry Date: <b>{customsEntryCodeDate}</b>
                </Grid>

                <Grid item xs={9}>
                    Arrival port: <b>{arrivalPort}</b>
                </Grid>
                <Grid item xs={3}>
                    Linn Duty: <b>{linnDuty}</b>
                </Grid>

                <Grid item xs={9}>
                    Arrival Date: <b>{arrivalDate}</b>
                </Grid>
                <Grid item xs={3}>
                    Linn Vat: <b>{linnVat}</b>
                </Grid>

                <Grid item xs={12} className={classes.gapAbove} />

                <Grid item xs={4}>
                    Remaining Invoice Value: <b>{remainingInvoiceValue}</b>
                </Grid>

                <Grid item xs={4}>
                    Remaining Duty: <b>{remainingDutyValue}</b>
                </Grid>
                <Grid item xs={4}>
                    Remaining Weight: <b>{remainingWeightValue}</b>
                </Grid>

                <Grid item xs={12} className={classes.gapAbove}>
                    <Typography variant="h6">Order Details</Typography>
                </Grid>

                {orderDetails
                    ?.sort((a, b) => {
                        return a.lineNumber - b.lineNumber;
                    })
                    .map(row => (
                        <>
                            <Grid item xs={1}>
                                <InputLabel>Line Type</InputLabel>
                                <TextField value={row.lineType} variant="standard" fullWidth />
                            </Grid>

                            {(row.lineType === 'PO' || row.lineType === 'RO') && (
                                <Grid item xs={2}>
                                    <InputLabel>Order Number</InputLabel>
                                    <TextField
                                        value={row.orderNumber}
                                        variant="standard"
                                        fullWidth
                                    />
                                </Grid>
                            )}

                            {row.lineType === 'RSN' && (
                                <Grid item xs={2}>
                                    <InputLabel>RSN Number</InputLabel>
                                    <TextField value={row.rsnNumber} variant="standard" fullWidth />
                                </Grid>
                            )}

                            {row.lineType === 'LOAN' && (
                                <Grid item xs={2}>
                                    <InputLabel>Loan Number</InputLabel>
                                    <TextField
                                        value={row.loanNumber}
                                        variant="standard"
                                        fullWidth
                                    />
                                </Grid>
                            )}

                            {row.lineType === 'INS' && (
                                <Grid item xs={2}>
                                    <InputLabel>INS Number</InputLabel>
                                    <TextField value={row.rsnNumber} variant="standard" fullWidth />
                                </Grid>
                            )}

                            <Grid item xs={3}>
                                <InputLabel>Order Description</InputLabel>
                                <TextField
                                    value={row.orderDescription}
                                    variant="standard"
                                    fullWidth
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputLabel>Tariff Code</InputLabel>
                                <TextField value={row.tariffCode} variant="standard" fullWidth />
                            </Grid>
                            <Grid item xs={1}>
                                <InputLabel>Tariff No</InputLabel>
                                <TextField value={row.tariffNumber} variant="standard" fullWidth />
                            </Grid>
                            <Grid item xs={1}>
                                <InputLabel>Qty</InputLabel>
                                <TextField value={row.qty} variant="standard" fullWidth />
                            </Grid>
                            <Grid item xs={12} />
                            <Grid item xs={2}>
                                <InputLabel>Order Value</InputLabel>
                                <TextField value={row.orderValue} variant="standard" fullWidth />
                            </Grid>
                            <Grid item xs={2}>
                                <InputLabel>Duty Value</InputLabel>
                                <TextField value={row.dutyValue} variant="standard" fullWidth />
                            </Grid>
                            <Grid item xs={1}>
                                <InputLabel>Vat Rate</InputLabel>
                                <TextField value={row.vatRate} variant="standard" fullWidth />
                            </Grid>
                            <Grid item xs={2}>
                                <InputLabel>Vat Value</InputLabel>
                                <TextField value={row.vatValue} variant="standard" fullWidth />
                            </Grid>
                            <Grid item xs={1}>
                                <InputLabel>Weight</InputLabel>
                                <TextField value={row.weight} variant="standard" fullWidth />
                            </Grid>
                            <Grid item xs={2}>
                                <InputLabel>Cpc Number</InputLabel>
                                <TextField value={row.cpcNumber} variant="standard" fullWidth />
                            </Grid>

                            <Grid item xs={12}>
                                <Divider className={classes.dividerMargins} />
                            </Grid>
                        </>
                    ))}

                <Grid item xs={12}>
                    {comments}
                </Grid>
            </Grid>
        </>
    );
}

ImpBookPrintOut.propTypes = {
    impbookId: PropTypes.number.isRequired,
    dateCreated: PropTypes.string.isRequired,
    parcelNumber: PropTypes.number.isRequired,
    supplierId: PropTypes.string.isRequired,
    currency: PropTypes.string.isRequired,
    carrierId: PropTypes.string.isRequired,
    transportCode: PropTypes.number.isRequired,
    transportBillNumber: PropTypes.string.isRequired,
    transactionCode: PropTypes.number.isRequired,
    deliveryTermCode: PropTypes.string.isRequired,
    arrivalPort: PropTypes.string.isRequired,
    arrivalDate: PropTypes.string.isRequired,
    totalImportValue: PropTypes.number.isRequired,
    weight: PropTypes.number.isRequired,
    customsEntryCodePrefix: PropTypes.string.isRequired,
    customsEntryCode: PropTypes.string.isRequired,
    customsEntryCodeDate: PropTypes.string.isRequired,
    linnDuty: PropTypes.number.isRequired,
    linnVat: PropTypes.number.isRequired,
    numCartons: PropTypes.number.isRequired,
    numPallets: PropTypes.number.isRequired,
    createdBy: PropTypes.number.isRequired,
    invoiceDetails: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    orderDetails: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    comments: PropTypes.string,
    supplierName: PropTypes.string.isRequired,
    supplierCountry: PropTypes.string.isRequired,
    eecMember: PropTypes.string.isRequired,
    carrierName: PropTypes.string.isRequired,
    remainingInvoiceValue: PropTypes.string.isRequired,
    remainingDutyValue: PropTypes.string.isRequired,
    remainingWeightValue: PropTypes.string.isRequired
};

ImpBookPrintOut.defaultProps = {
    comments: ''
};

export default ImpBookPrintOut;
