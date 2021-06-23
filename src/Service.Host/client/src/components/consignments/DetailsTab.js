import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Dropdown, utilities, InputField, DatePicker } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import { makeStyles } from '@material-ui/core/styles';
import moment from 'moment';

function DetailsTab({
    consignment,
    updateField,
    viewMode,
    editStatus,
    hub,
    hubs,
    hubsLoading,
    carrier,
    carriers,
    carriersLoading,
    shippingTerm,
    shippingTerms,
    shippingTermsLoading
}) {
    const useStyles = makeStyles(() => ({
        tableCell: {
            borderBottom: 0,
            whiteSpace: 'pre-line',
            verticalAlign: 'top'
        }
    }));
    const classes = useStyles();

    const TablePromptItem = ({ text, width }) => (
        <TableCell style={{ width, borderBottom: 0, whiteSpace: 'pre-line', verticalAlign: 'top' }}>
            {text}
        </TableCell>
    );

    TablePromptItem.propTypes = {
        text: PropTypes.string,
        width: PropTypes.number
    };

    TablePromptItem.defaultProps = {
        text: null,
        width: 150
    };

    const showText = (displayText, displayDescription) => {
        if (displayText) {
            return `${displayText} ${displayDescription ? ` - ${displayDescription}` : ''} `;
        }

        return '';
    };

    const DisplayEditItem = ({
        currentEditStatus,
        displayText,
        displayDescription,
        editComponent,
        allowCreate
    }) => {
        if (currentEditStatus === 'view' || (currentEditStatus === 'create' && !allowCreate)) {
            if (displayText) {
                return `${displayText} ${displayDescription ? ` - ${displayDescription}` : ''} `;
            }

            return '';
        }

        return editComponent;
    };

    DisplayEditItem.propTypes = {
        currentEditStatus: PropTypes.string,
        displayText: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        displayDescription: PropTypes.string,
        editComponent: PropTypes.shape(),
        allowCreate: PropTypes.bool
    };

    DisplayEditItem.defaultProps = {
        currentEditStatus: 'view',
        displayText: null,
        displayDescription: null,
        editComponent: <></>,
        allowCreate: true
    };

    const hubOptions = () => {
        return utilities.sortEntityList(hubs, 'hubId')?.map(h => ({
            id: h.hubId,
            displayText: `${h.hubId} - ${h.description}`
        }));
    };

    const carrierOptions = () => {
        return utilities.sortEntityList(carriers, 'carrierCode')?.map(c => ({
            id: c.carrierCode,
            displayText: `${c.carrierCode} - ${c.name}`
        }));
    };

    const shippingTermOptions = () => {
        return utilities.sortEntityList(shippingTerms, 'code')?.map(h => ({
            id: h.code,
            displayText: `${h.code} - ${h.description}`
        }));
    };

    const freightOptions = () => {
        return [
            { id: 'S', displayText: 'Surface' },
            { id: 'A', displayText: 'Air' },
            { id: 'W', displayText: 'Sea' }
        ];
    };

    const showShippingMethod = shippingMethod => {
        switch (shippingMethod) {
            case 'S':
                return 'Surface';
            case 'A':
                return 'Air';
            case 'W':
                return 'Sea';
            default:
                return 'Other';
        }
    };

    return (
        <>
            <Grid item xs={12}>
                <Table size="small" style={{ paddingTop: '30px' }}>
                    <TableBody>
                        <TableRow key="Account">
                            <TablePromptItem text="Account" width={160} />
                            <TableCell className={classes.tableCell} style={{ width: 350 }}>
                                {consignment.salesAccountId} {consignment.customerName}
                            </TableCell>
                        </TableRow>
                        <TableRow key="Address">
                            <TablePromptItem text="Address" />
                            <TableCell className={classes.tableCell}>
                                {consignment.address && consignment.address.displayAddress}
                            </TableCell>
                        </TableRow>
                        <TableRow key="Despatch Location">
                            <TablePromptItem text="Despatch Location" />
                            <TableCell className={classes.tableCell}>
                                {consignment.despatchLocationCode}
                            </TableCell>
                        </TableRow>
                        <TableRow key="Freight">
                            <TablePromptItem text="Freight" />
                            <TableCell className={classes.tableCell}>
                                <DisplayEditItem
                                    currentEditStatus={editStatus}
                                    displayText={showShippingMethod(consignment.shippingMethod)}
                                    editComponent={
                                        <Dropdown
                                            propertyName="shippingMethod"
                                            items={freightOptions()}
                                            onChange={updateField}
                                            value={consignment.shippingMethod}
                                            allowNoValue={false}
                                        />
                                    }
                                />
                            </TableCell>
                        </TableRow>
                        <TableRow key="Carrier">
                            <TablePromptItem text="Carrier" />
                            <TableCell className={classes.tableCell}>
                                <DisplayEditItem
                                    currentEditStatus={editStatus}
                                    displayText={consignment.carrier}
                                    displayDescription={carrier && carrier.name}
                                    editComponent={
                                        <Dropdown
                                            propertyName="carrier"
                                            items={carrierOptions()}
                                            onChange={updateField}
                                            value={consignment.carrier}
                                            optionsLoading={carriersLoading}
                                            allowNoValue={false}
                                        />
                                    }
                                />
                            </TableCell>
                        </TableRow>
                        <TableRow key="Terms">
                            <TablePromptItem text="Terms" />
                            <TableCell className={classes.tableCell}>
                                <DisplayEditItem
                                    currentEditStatus={editStatus}
                                    displayText={consignment.terms}
                                    displayDescription={shippingTerm?.description}
                                    editComponent={
                                        <Dropdown
                                            propertyName="terms"
                                            items={shippingTermOptions()}
                                            onChange={updateField}
                                            value={consignment.terms}
                                            optionsLoading={shippingTermsLoading}
                                        />
                                    }
                                />
                            </TableCell>
                        </TableRow>
                        <TableRow key="Hub">
                            <TablePromptItem text="Hub" />
                            <TableCell className={classes.tableCell}>
                                <DisplayEditItem
                                    currentEditStatus={editStatus}
                                    displayText={consignment.hubId}
                                    displayDescription={hub && hub.description}
                                    editComponent={
                                        <Dropdown
                                            propertyName="hubId"
                                            items={hubOptions()}
                                            onChange={updateField}
                                            value={consignment.hubId}
                                            optionsLoading={hubsLoading}
                                        />
                                    }
                                />
                            </TableCell>
                        </TableRow>
                        <TableRow key="CustomsEntry">
                            <TablePromptItem text="Customs Entry Code" />
                            <TableCell className={classes.tableCell}>
                                {viewMode ? (
                                    `${showText(consignment.customsEntryCodePrefix)} ${showText(
                                        consignment.customsEntryCode
                                    )}`
                                ) : (
                                    <>
                                        <InputField
                                            placeholder="Prefix"
                                            propertyName="customsEntryCodePrefix"
                                            value={consignment.customsEntryCodePrefix}
                                            onChange={updateField}
                                            maxLength={3}
                                        />
                                        <InputField
                                            placeholder="Entry Code"
                                            propertyName="customsEntryCode"
                                            value={consignment.customsEntryCode}
                                            onChange={updateField}
                                            maxLength={20}
                                        />
                                    </>
                                )}
                            </TableCell>
                            <TablePromptItem text="Entry Code Date" />
                            <TableCell className={classes.tableCell}>
                                {viewMode ? (
                                    consignment.customsEntryCodeDate &&
                                    moment(consignment.customsEntryCodeDate).format('DD MMM YYYY')
                                ) : (
                                    <DatePicker
                                        value={
                                            consignment.customsEntryCodeDate
                                                ? consignment.customsEntryCodeDate
                                                : null
                                        }
                                        onChange={value => {
                                            updateField('customsEntryCodeDate', value);
                                        }}
                                    />
                                )}
                            </TableCell>
                        </TableRow>
                        <TableRow key="DateOpened">
                            <TablePromptItem text="Date Opened" />
                            <TableCell className={classes.tableCell}>
                                {moment(consignment.dateOpened).format('DD MMM YYYY')}
                            </TableCell>
                        </TableRow>
                        <TableRow key="DateClosed">
                            <TablePromptItem text="Date Closed" />
                            <TableCell className={classes.tableCell}>
                                {consignment.dateClosed &&
                                    moment(consignment.dateClosed).format('DD MMM YYYY')}
                            </TableCell>
                            <TablePromptItem text="Closed By" />
                            <TableCell className={classes.tableCell}>
                                {consignment.closedBy && consignment.closedBy.fullName}
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </Grid>
        </>
    );
}

DetailsTab.propTypes = {
    consignment: PropTypes.shape({
        consignmentId: PropTypes.number,
        customerName: PropTypes.string,
        salesAccountId: PropTypes.number,
        shippingMethod: PropTypes.string,
        dateOpened: PropTypes.string,
        dateClosed: PropTypes.string,
        carrier: PropTypes.string,
        terms: PropTypes.string,
        hubId: PropTypes.number,
        customsEntryCodePrefix: PropTypes.string,
        customsEntryCode: PropTypes.string,
        customsEntryCodeDate: PropTypes.string,
        despatchLocationCode: PropTypes.string,
        pallets: PropTypes.arrayOf(PropTypes.shape({})),
        closedBy: PropTypes.shape({ id: PropTypes.number, fullName: PropTypes.string }),
        address: PropTypes.shape({ id: PropTypes.number, displayAddress: PropTypes.string })
    }),
    updateField: PropTypes.func.isRequired,
    viewMode: PropTypes.bool.isRequired,
    editStatus: PropTypes.string.isRequired,
    hub: PropTypes.shape({ hubId: PropTypes.number, description: PropTypes.string }),
    hubs: PropTypes.arrayOf(
        PropTypes.shape({ hubId: PropTypes.number, description: PropTypes.string })
    ),
    hubsLoading: PropTypes.bool,
    carrier: PropTypes.shape({ carrierCode: PropTypes.string, name: PropTypes.string }),
    carriers: PropTypes.arrayOf(
        PropTypes.shape({ carrierCode: PropTypes.string, name: PropTypes.string })
    ),
    carriersLoading: PropTypes.bool,
    shippingTerm: PropTypes.shape({ code: PropTypes.string, description: PropTypes.string }),
    shippingTerms: PropTypes.arrayOf(
        PropTypes.shape({ code: PropTypes.string, description: PropTypes.string })
    ),
    shippingTermsLoading: PropTypes.bool
};

DetailsTab.defaultProps = {
    consignment: {},
    hub: null,
    hubs: [],
    hubsLoading: false,
    carrier: null,
    carriers: [],
    carriersLoading: false,
    shippingTerm: null,
    shippingTerms: [],
    shippingTermsLoading: false
};

export default DetailsTab;
