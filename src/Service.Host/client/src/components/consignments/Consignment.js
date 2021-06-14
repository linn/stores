import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Loading,
    Dropdown,
    SaveBackCancelButtons,
    utilities
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import Typography from '@material-ui/core/Typography';
import { withStyles } from '@material-ui/core/styles';
import moment from 'moment';
import Page from '../../containers/Page';

function Consignment({
    item,
    loading,
    requestErrors,
    openConsignments,
    getConsignment,
    optionsLoading,
    startingTab,
    editStatus,
    setEditStatus,
    hub,
    getHub
}) {
    const [currentTab, setcurrentTab] = useState(startingTab);
    const [consignment, setConsignment] = useState(item);

    useEffect(() => {
        setConsignment(item);
    }, [item]);

    useEffect(() => {
        if (consignment) {
            const hubHref = utilities.getHref(consignment, 'hub');
            if (hubHref) {
                getHub(hubHref);
            }
        }
    }, [consignment, getHub]);

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

    const startEdit = () => {
        setEditStatus('edit');
    };

    const doSave = () => {};

    const doCancel = () => {
        setConsignment(item);
        setEditStatus('view');
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
                <>
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
                    {currentTab !== 0 && (loading || !consignment) ? (
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
                                                <TableItem>
                                                    {consignment.hubId} - {hub && hub.description}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="DateOpened">
                                                <TableItem>Date Opened</TableItem>
                                                <TableItem>
                                                    {moment(consignment.dateOpened).format(
                                                        'DD MMM YYYY'
                                                    )}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="DateClosed">
                                                <TableItem>Date Closed</TableItem>
                                                <TableItem>
                                                    {consignment.dateClosed &&
                                                        moment(consignment.dateClosed).format(
                                                            'DD MMM YYYY'
                                                        )}
                                                </TableItem>
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
                </>
                <Grid item xs={12}>
                    {editStatus === 'view' ? (
                        <Button variant="outlined" color="primary" onClick={startEdit}>
                            Edit
                        </Button>
                    ) : (
                        <SaveBackCancelButtons
                            saveClick={doSave}
                            backClick={() => {}}
                            cancelClick={doCancel}
                        />
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

Consignment.propTypes = {
    item: PropTypes.shape({
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
    startingTab: PropTypes.number,
    editStatus: PropTypes.string,
    setEditStatus: PropTypes.func.isRequired,
    getHub: PropTypes.func.isRequired,
    hub: PropTypes.shape({ hubId: PropTypes.number, description: PropTypes.string })
};

Consignment.defaultProps = {
    item: {},
    loading: false,
    requestErrors: null,
    openConsignments: [],
    optionsLoading: false,
    startingTab: 0,
    editStatus: 'view',
    hub: null
};

export default Consignment;
