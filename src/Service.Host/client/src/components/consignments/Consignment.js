import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { Loading, Dropdown } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import Typography from '@material-ui/core/Typography';
import { withStyles } from '@material-ui/core/styles';
import Page from '../../containers/Page';

function Consignment({
    consignment,
    loading,
    requestErrors,
    openConsignments,
    getConsignment,
    optionsLoading,
    startingTab
}) {
    const [currentTab, setcurrentTab] = useState(startingTab);

    const TableItem = withStyles(() => ({
        body: {
            borderBottom: 0,
            whiteSpace: 'pre-line',
            verticalAlign: 'top'
        }
    }))(TableCell);

    const openConsignmentOptions = () => {
        return openConsignments?.map(c => ({
            id: c.consignmentId,
            displayText: c.customerName
        }));
    };

    const handleSelectConsignment = (_property, newValue) => {
        getConsignment(newValue);
        setcurrentTab(1);
    };

    const handleTabChange = (_event, newValue) => {
        setcurrentTab(newValue);
    };

    const showFreight = freigt => {
        switch (freigt) {
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
        <Page requestErrors={requestErrors} showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={2}>
                    <Typography variant="h6">Consignment</Typography>
                </Grid>
                <Grid item xs={10}>
                    {consignment && (
                        <Typography variant="h6">
                            {consignment.consignmentId} {consignment.customerName}
                        </Typography>
                    )}
                </Grid>
                <div>
                    <Tabs
                        value={currentTab}
                        onChange={handleTabChange}
                        style={{ paddingBottom: '20px' }}
                    >
                        <Tab label="Select" />
                        <Tab label="Details" />
                        <Tab label="Consignment Items" />
                    </Tabs>
                    {currentTab === 0 && (
                        <Grid item xs={12}>
                            <Dropdown
                                label="Select consignment"
                                propertyName="consignmentSelect"
                                items={openConsignmentOptions()}
                                onChange={handleSelectConsignment}
                                optionsLoading={optionsLoading}
                            />
                        </Grid>
                    )}
                    {loading || !consignment ? (
                        <Loading />
                    ) : (
                        currentTab === 1 && (
                            <>
                                <Grid item xs={12}>
                                    <Table style={{ paddingTop: '30px' }}>
                                        <TableBody>
                                            <TableRow key="Account">
                                                <TableItem>Account</TableItem>
                                                <TableItem>
                                                    {consignment.salesAccountId}{' '}
                                                    {consignment.customerName}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="Address">
                                                <TableItem>Address</TableItem>
                                                <TableItem>
                                                    {consignment.address &&
                                                        consignment.address.displayAddress}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="Freight">
                                                <TableItem>Freight</TableItem>
                                                <TableItem>
                                                    {showFreight(consignment.shippingMethod)}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="Carrier">
                                                <TableItem>Carrier</TableItem>
                                                <TableItem>{consignment.carrier}</TableItem>
                                            </TableRow>
                                            <TableRow key="Terms">
                                                <TableItem>Terms</TableItem>
                                                <TableItem>{consignment.terms}</TableItem>
                                            </TableRow>
                                            <TableRow key="Hub">
                                                <TableItem>Hub</TableItem>
                                                <TableItem>{consignment.hubId}</TableItem>
                                            </TableRow>
                                            <TableRow key="DateOpened">
                                                <TableItem>Date Opened</TableItem>
                                                <TableItem>{consignment.dateOpened}</TableItem>
                                            </TableRow>
                                            <TableRow key="DateClosed">
                                                <TableItem>Date Closed</TableItem>
                                                <TableItem>{consignment.dateClosed}</TableItem>
                                                <TableItem>Closed By</TableItem>
                                                <TableItem>
                                                    {consignment.closedBy &&
                                                        consignment.closedBy.fullName}
                                                </TableItem>
                                            </TableRow>
                                        </TableBody>
                                    </Table>
                                </Grid>
                            </>
                        )
                    )}
                </div>
            </Grid>
        </Page>
    );
}

Consignment.propTypes = {
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
        closedBy: PropTypes.shape({ id: PropTypes.number, fullName: PropTypes.string }),
        address: PropTypes.shape({ id: PropTypes.number, displayAddress: PropTypes.string })
    }),
    loading: PropTypes.bool,
    requestErrors: PropTypes.arrayOf(
        PropTypes.shape({ message: PropTypes.string, name: PropTypes.string })
    ),
    openConsignments: PropTypes.arrayOf(PropTypes.shape({})),
    getConsignment: PropTypes.func.isRequired,
    optionsLoading: PropTypes.bool,
    startingTab: PropTypes.number
};

Consignment.defaultProps = {
    consignment: null,
    loading: false,
    requestErrors: null,
    openConsignments: [],
    optionsLoading: false,
    startingTab: 0
};

export default Consignment;
