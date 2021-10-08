import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Divider from '@material-ui/core/Divider';
import Tooltip from '@material-ui/core/Tooltip';
import DeleteIcon from '@material-ui/icons/Delete';
import { Dropdown, InputField, LinkButton, Typeahead } from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';

function OrderDetailsTab({
    orderDetails,
    handleFieldChange,
    invoiceDate,
    handleOrderDetailChange,
    cpcNumbers,
    allowedToEdit,
    addOrderDetailRow,
    removeOrderDetailRow,
    remainingInvoiceValue,
    remainingDutyValue,
    remainingWeightValue,
    rsnsSearchResults,
    rsnsSearchLoading,
    searchRsns,
    clearRsnsSearch,
    purchaseOrdersSearchResults,
    purchaseOrdersSearchLoading,
    loanHeadersSearchResults,
    loanHeadersSearchLoading,
    searchLoanHeaders,
    clearLoanHeadersSearch,
    searchPurchaseOrders,
    clearPurchaseOrdersSearch,
    supplierId
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

    const handleRsnUpdate = (row, rsn) => {
        updateRow({
            ...row,
            rsnNumber: rsn.id,
            orderDescription: rsn.description,
            qty: rsn.quantity,
            tariffCode: rsn.tariffCode,
            weight: rsn.weight
        });
    };

    const handleOrderNoUpdate = (row, order) => {
        if (order.supplierId !== supplierId) {
            //eslint-disable-next-line no-alert
            alert(
                `This PO has supplier ${order.supplierId}, while the supplier on this impbook is set to supplier ${supplierId}. Is this right?`
            );
        }
        updateRow({
            ...row,
            orderNumber: order.id,
            orderDescription: order.description,
            tariffCode: order.tariffCode
        });
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
                        value={remainingInvoiceValue}
                        disabled
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="Remaining Duty Total"
                        fullWidth
                        propertyName="remainingDutyTotal"
                        type="number"
                        value={remainingDutyValue}
                        disabled
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="Remaining Weight"
                        fullWidth
                        propertyName="remainingWeight"
                        type="number"
                        value={remainingWeightValue}
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
                                    <div className={classes.displayInline}>
                                        <Typeahead
                                            label="Order Number"
                                            propertyName="orderNumber"
                                            title="Search for an Order Number"
                                            onSelect={newOrder =>
                                                handleOrderNoUpdate(row, newOrder)
                                            }
                                            items={purchaseOrdersSearchResults}
                                            loading={purchaseOrdersSearchLoading}
                                            fetchItems={searchPurchaseOrders}
                                            clearSearch={() => clearPurchaseOrdersSearch}
                                            value={row.orderNumber}
                                            modal
                                            links={false}
                                            debounce={1000}
                                            minimumSearchTermLength={2}
                                            required
                                            disabled={!allowedToEdit}
                                            maxLength={6}
                                        />
                                    </div>
                                    <div className={classes.marginTop1}>
                                        <Tooltip title="Clear Order No search">
                                            <Button
                                                variant="outlined"
                                                onClick={() => editRow(row, 'orderNumber', '')}
                                                disabled={!allowedToEdit}
                                            >
                                                X
                                            </Button>
                                        </Tooltip>
                                    </div>
                                </Grid>
                            )}

                            {row.lineType === 'RSN' && (
                                <Grid item xs={2}>
                                    <div className={classes.displayInline}>
                                        <Typeahead
                                            label="RSN Number"
                                            propertyName="rsnNumber"
                                            title="Search for an rsn"
                                            onSelect={newRsn => handleRsnUpdate(row, newRsn)}
                                            items={rsnsSearchResults}
                                            loading={rsnsSearchLoading}
                                            fetchItems={searchRsns}
                                            clearSearch={() => clearRsnsSearch}
                                            value={row.rsnNumber}
                                            modal
                                            links={false}
                                            debounce={1000}
                                            minimumSearchTermLength={2}
                                            required
                                            disabled={!allowedToEdit}
                                            maxLength={6}
                                        />
                                    </div>
                                    <div className={classes.marginTop1}>
                                        <Tooltip title="Clear Rsn search">
                                            <Button
                                                variant="outlined"
                                                onClick={() => editRow(row, 'rsnNumber', '')}
                                                disabled={!allowedToEdit}
                                            >
                                                X
                                            </Button>
                                        </Tooltip>
                                    </div>
                                </Grid>
                            )}

                            {row.lineType === 'LOAN' && (
                                <Grid item xs={2}>
                                    <div className={classes.displayInline}>
                                        <Typeahead
                                            label="Loan Number"
                                            propertyName="loanNumber"
                                            title="Search for a Loan Number"
                                            onSelect={newValue =>
                                                editRow(row, 'loanNumber', newValue.id)
                                            }
                                            items={loanHeadersSearchResults}
                                            loading={loanHeadersSearchLoading}
                                            fetchItems={searchLoanHeaders}
                                            clearSearch={() => clearLoanHeadersSearch}
                                            value={row.loanNumber}
                                            modal
                                            links={false}
                                            debounce={1000}
                                            minimumSearchTermLength={2}
                                            required
                                            disabled={!allowedToEdit}
                                            maxLength={6}
                                        />
                                    </div>
                                    <div className={classes.marginTop1}>
                                        <Tooltip title="Clear Loan Number search">
                                            <Button
                                                variant="outlined"
                                                onClick={() => editRow(row, 'loanNumber', '')}
                                                disabled={!allowedToEdit}
                                            >
                                                X
                                            </Button>
                                        </Tooltip>
                                    </div>

                                    {/* <InputField
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
                                    /> */}
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
                                    type="number"
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
    remainingInvoiceValue: PropTypes.string.isRequired,
    remainingDutyValue: PropTypes.number.isRequired,
    remainingWeightValue: PropTypes.number.isRequired,
    invoiceDate: PropTypes.string,
    handleOrderDetailChange: PropTypes.func.isRequired,
    cpcNumbers: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    allowedToEdit: PropTypes.bool.isRequired,
    addOrderDetailRow: PropTypes.func.isRequired,
    removeOrderDetailRow: PropTypes.func.isRequired,
    rsnsSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string,
            description: PropTypes.string,
            quantity: PropTypes.number,
            tariffCode: PropTypes.string,
            weight: PropTypes.number
        })
    ),
    rsnsSearchLoading: PropTypes.bool.isRequired,
    searchRsns: PropTypes.func.isRequired,
    clearRsnsSearch: PropTypes.func.isRequired,
    purchaseOrdersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string,
            description: PropTypes.string,
            supplierId: PropTypes.number,
            tariffCode: PropTypes.string,
            lineNumber: PropTypes.number
        })
    ),
    purchaseOrdersSearchLoading: PropTypes.bool.isRequired,
    loanHeadersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string,
            description: PropTypes.string
        })
    ),
    loanHeadersSearchLoading: PropTypes.bool.isRequired,
    searchLoanHeaders: PropTypes.func.isRequired,
    clearLoanHeadersSearch: PropTypes.func.isRequired,
    searchPurchaseOrders: PropTypes.func.isRequired,
    clearPurchaseOrdersSearch: PropTypes.func.isRequired
};

OrderDetailsTab.defaultProps = {
    invoiceDate: '',
    rsnsSearchResults: null,
    purchaseOrdersSearchResults: null,
    loanHeadersSearchResults: null
};

export default OrderDetailsTab;
