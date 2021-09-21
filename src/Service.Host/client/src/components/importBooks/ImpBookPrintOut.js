import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, TableWithInlineEditing } from '@linn-it/linn-form-components-library';
import Divider from '@material-ui/core/Divider';
import { makeStyles } from '@material-ui/styles';

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

    return (
        <>
            <Grid container spacing={1}>
                <Grid item xs={4}>
                    Impbook ID: <b>{{ impbookId }}</b>
                </Grid>

                <Grid item xs={4}>
                    Date Created: <b>{{ dateCreated }}</b>
                </Grid>
                <Grid item xs={4}>
                    Created By: <b>{{ createdBy }}</b>
                </Grid>

                <Grid item xs={4}>
                    Supplier:
                    <b>
                        {{ supplierId }} - {{ supplierName }}
                    </b>
                </Grid>
                <Grid item xs={4}>
                    Country: <b>{{ supplierCountry }}</b>
                </Grid>
                <Grid item xs={4}>
                    EEC Member: <b>{{ eecMember }}</b>
                </Grid>

                <Grid item xs={12}>
                    Currency: <b>{{ currency }}</b>
                </Grid>
                <Grid item xs={12}>
                    Parcel Number: <b>{{ parcelNumber }}</b>
                </Grid>

                <Grid item xs={12} className={classes.gapAbove}>
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

                <Grid item xs={12}>
                    Total Import Value (GBP): <b>{{ totalImportValue }}</b>
                </Grid>

                <Grid item xs={12}>
                    Carrier:
                    <b>
                        {{ carrierId }} - {{ carrierName }}
                    </b>
                </Grid>

                <Grid item xs={9}>
                    Transport Code: <b>{{ transportCode }}</b>
                </Grid>
                <Grid item xs={3}>
                    Num Cartons: <b>{{ numCartons }}</b>
                </Grid>

                <Grid item xs={9}>
                    Transport Bill Number: <b>{{ transportBillNumber }}</b>
                </Grid>
                <Grid item xs={3}>
                    Num Pallets: <b>{{ numPallets }}</b>
                </Grid>

                <Grid item xs={9}>
                    Transaction Code: <b>{{ transactionCode }}</b>
                </Grid>
                <Grid item xs={3}>
                    Weight: <b>{{ weight }}</b>
                </Grid>

                <Grid item xs={9}>
                    Delivery Term Code: <b>{{ deliveryTermCode }}</b>
                </Grid>
                <Grid item xs={3}>
                    Customs Entry Code:{' '}
                    <b>
                        {{ customsEntryCodePrefix }} {{ customsEntryCode }}
                    </b>
                </Grid>

                <Grid item xs={9}>
                    Arrival port: <b>{{ arrivalPort }}</b>
                </Grid>
                <Grid item xs={3}>
                    Customs Entry Date: <b>{{ customsEntryCodeDate }}</b>
                </Grid>

                <Grid item xs={9}>
                    Arrival Date: <b>{{ arrivalDate }}</b>
                </Grid>
                <Grid item xs={3}>
                    Linn Duty: <b>{{ linnDuty }}</b>
                </Grid>

                <Grid item xs={9} />
                <Grid item xs={3}>
                    Linn Vat: <b>{{ linnVat }}</b>
                </Grid>

                <Grid item xs={4}>
                    Remaining Invoice Value: <b>{{ remainingInvoiceValue }}</b>
                </Grid>

                <Grid item xs={4}>
                    Remaining Duty: <b>{{ remainingDutyValue }}</b>
                </Grid>
                <Grid item xs={4}>
                    Remaining Weight: <b>{{ remainingWeightValue }}</b>
                </Grid>

                {orderDetails
                    ?.sort((a, b) => {
                        return a.lineNumber - b.lineNumber;
                    })
                    .map(row => (
                        <>
                            <Grid item xs={1}>
                                <InputField
                                    label="Line Type"
                                    fullWidth
                                    propertyName="lineType"
                                    value={row.lineType}
                                    required
                                />
                            </Grid>

                            {(row.lineType === 'PO' || row.lineType === 'RO') && (
                                <Grid item xs={2}>
                                    <InputField
                                        label="Order Number"
                                        fullWidth
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
                                        propertyName="rsnNumber"
                                        type="number"
                                        value={row.rsnNumber}
                                        maxLength={6}
                                    />
                                </Grid>
                            )}

                            {row.lineType === 'LOAN' && (
                                <Grid item xs={2}>
                                    <InputField
                                        label="Loan Number"
                                        fullWidth
                                        propertyName="loanNumber"
                                        type="number"
                                        value={row.loanNumber}
                                        maxLength={6}
                                    />
                                </Grid>
                            )}

                            {row.lineType === 'INS' && (
                                <Grid item xs={2}>
                                    <InputField
                                        label="Ins Number"
                                        fullWidth
                                        propertyName="insNumber"
                                        type="number"
                                        value={row.insNumber}
                                        maxLength={10}
                                    />
                                </Grid>
                            )}

                            <Grid item xs={3}>
                                <InputField
                                    label="Order Description"
                                    fullWidth
                                    propertyName="orderDescription"
                                    type="text"
                                    value={row.orderDescription}
                                    required
                                    maxLength={2000}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    label="Tariff Code"
                                    fullWidth
                                    propertyName="tariffCode"
                                    type="text"
                                    value={row.tariffCode}
                                    maxLength={12}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    label="Tariff Number"
                                    fullWidth
                                    propertyName="tariffNumber"
                                    type="number"
                                    value={row.tariffNumber}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    label="Qty"
                                    fullWidth
                                    propertyName="qty"
                                    type="number"
                                    value={row.qty}
                                    required
                                    maxLength={6}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    label="Order Value"
                                    fullWidth
                                    propertyName="orderValue"
                                    type="number"
                                    value={row.orderValue}
                                    required
                                    maxLength={14}
                                    decimalPlaces={2}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    label="Duty Value"
                                    fullWidth
                                    propertyName="dutyValue"
                                    type="number"
                                    value={row.dutyValue}
                                    required
                                    maxLength={14}
                                    decimalPlaces={2}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    label="Vat Rate"
                                    fullWidth
                                    propertyName="vatRate"
                                    type="number"
                                    value={row.vatRate}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    label="Vat Value"
                                    fullWidth
                                    propertyName="vatValue"
                                    type="number"
                                    value={row.vatValue}
                                    required
                                    maxLength={14}
                                    decimalPlaces={2}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    label="Weight"
                                    fullWidth
                                    propertyName="weight"
                                    type="number"
                                    value={row.weight}
                                    maxLength={10}
                                    decimalPlaces={2}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField propertyName="cpcNumber" value={row.cpcNumber} />
                            </Grid>

                            <Grid item xs={12}>
                                <Divider className={classes.dividerMargins} />
                            </Grid>
                        </>
                    ))}

                <Grid item xs={12}>
                    {{ comments }}
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
