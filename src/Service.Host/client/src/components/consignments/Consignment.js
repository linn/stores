import React, { useState, useEffect, useReducer } from 'react';
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
import { withStyles, makeStyles } from '@material-ui/core/styles';
import moment from 'moment';
import Page from '../../containers/Page';
import consignmentReducer from './consignmentReducer';

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
    getHub,
    hubs,
    hubsLoading,
    carrier,
    getCarrier,
    carriers,
    carriersLoading
}) {
    const [currentTab, setcurrentTab] = useState(startingTab);

    const [state, dispatch] = useReducer(consignmentReducer, {
        consignment: null,
        originalConsignment: null
    });

    useEffect(() => {
        dispatch({
            type: 'initialise',
            payload: item
        });
    }, [item]);

    useEffect(() => {
        if (state.consignment) {
            const hubHref = utilities.getHref(state.consignment, 'hub');
            if (hubHref) {
                getHub(hubHref);
            }

            const carrierHref = utilities.getHref(state.consignment, 'carrier');
            if (carrierHref) {
                getCarrier(carrierHref);
            }
        }
    }, [state.consignment, getHub, getCarrier]);

    const useStyles = makeStyles(() => ({
        pullRight: {
            float: 'right'
        }
    }));
    const classes = useStyles();

    const TableItem = withStyles(() => ({
        body: {
            borderBottom: 0,
            whiteSpace: 'pre-line',
            verticalAlign: 'top'
        }
    }))(TableCell);

    const viewing = () => {
        return editStatus === 'view';
    };

    const updateField = (fieldName, newValue) => {
        dispatch({
            type: 'updateField',
            fieldName,
            payload: newValue
        });
    };

    const openConsignmentOptions = () => {
        return openConsignments?.map(c => ({
            id: c.consignmentId,
            displayText: c.customerName
        }));
    };

    const hubOptions = () => {
        return hubs?.map(h => ({
            id: h.hubId,
            displayText: `${h.hubId} - ${h.description}`
        }));
    };

    const carrierOptions = () => {
        return carriers?.map(c => ({
            id: c.carrierCode,
            displayText: `${c.carrierCode} - ${c.name}`
        }));
    };

    const freightOptions = () => {
        return [
            { id: 'S', displayText: 'Surface' },
            { id: 'A', displayText: 'Air' },
            { id: 'W', displayText: 'Sea' }
        ];
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

    const closeConsignment = () => {};
    const doSave = () => {};

    const doCancel = () => {
        dispatch({
            type: 'reset',
            payload: null
        });
        setEditStatus('view');
    };

    return (
        <Page requestErrors={requestErrors} showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={2}>
                    <Typography variant="h6">Consignment</Typography>
                </Grid>
                <Grid item xs={8}>
                    {state.consignment && (
                        <Typography variant="h6">
                            {state.consignment.consignmentId} {state.consignment.customerName}
                        </Typography>
                    )}
                </Grid>
                <Grid item xs={2}>
                    <Button
                        variant="outlined"
                        className={classes.pullRight}
                        onClick={closeConsignment}
                        disabled={
                            !viewing() || !state.consignment || !state.consignment.status === 'L'
                        }
                    >
                        Close Consignment
                    </Button>
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
                    {currentTab !== 0 && (loading || !state.consignment) ? (
                        <Loading />
                    ) : (
                        currentTab === 1 && (
                            <>
                                <Grid item xs={12}>
                                    <Table size="small" style={{ paddingTop: '30px' }}>
                                        <TableBody>
                                            <TableRow key="Account">
                                                <TableItem>Account</TableItem>
                                                <TableItem>
                                                    {state.consignment.salesAccountId}{' '}
                                                    {state.consignment.customerName}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="Address">
                                                <TableItem>Address</TableItem>
                                                <TableItem>
                                                    {state.consignment.address &&
                                                        state.consignment.address.displayAddress}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="Despatch Location">
                                                <TableItem>Despatch Location</TableItem>
                                                <TableItem>
                                                    {state.consignment.despatchLocationCode}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="Freight">
                                                <TableItem>Freight</TableItem>
                                                <TableItem>
                                                    {viewing() ? (
                                                        showFreight(
                                                            state.consignment.shippingMethod
                                                        )
                                                    ) : (
                                                        <Dropdown
                                                            propertyName="shippingMethod"
                                                            items={freightOptions()}
                                                            onChange={updateField}
                                                            value={state.consignment.shippingMethod}
                                                        />
                                                    )}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="Carrier">
                                                <TableItem>Carrier</TableItem>
                                                <TableItem>
                                                    {viewing() ? (
                                                        <>
                                                            {state.consignment.carrier} {' - '}
                                                            {carrier && carrier.name}
                                                        </>
                                                    ) : (
                                                        <Dropdown
                                                            propertyName="carrier"
                                                            items={carrierOptions()}
                                                            onChange={updateField}
                                                            value={state.consignment.carrier}
                                                            optionsLoading={carriersLoading}
                                                        />
                                                    )}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="Terms">
                                                <TableItem>Terms</TableItem>
                                                <TableItem>{state.consignment.terms}</TableItem>
                                            </TableRow>
                                            <TableRow key="Hub">
                                                <TableItem>Hub</TableItem>
                                                <TableItem>
                                                    {viewing() ? (
                                                        <>
                                                            {state.consignment.hubId} {' - '}
                                                            {hub && hub.description}
                                                        </>
                                                    ) : (
                                                        <Dropdown
                                                            propertyName="hubId"
                                                            items={hubOptions()}
                                                            onChange={updateField}
                                                            value={state.consignment.hubId}
                                                            optionsLoading={hubsLoading}
                                                        />
                                                    )}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="DateOpened">
                                                <TableItem>Date Opened</TableItem>
                                                <TableItem>
                                                    {moment(state.consignment.dateOpened).format(
                                                        'DD MMM YYYY'
                                                    )}
                                                </TableItem>
                                            </TableRow>
                                            <TableRow key="DateClosed">
                                                <TableItem>Date Closed</TableItem>
                                                <TableItem>
                                                    {state.consignment.dateClosed &&
                                                        moment(state.consignment.dateClosed).format(
                                                            'DD MMM YYYY'
                                                        )}
                                                </TableItem>
                                                <TableItem>Closed By</TableItem>
                                                <TableItem>
                                                    {state.consignment.closedBy &&
                                                        state.consignment.closedBy.fullName}
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
                        <Button
                            variant="outlined"
                            color="primary"
                            className={classes.pullRight}
                            onClick={startEdit}
                        >
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
    hub: PropTypes.shape({ hubId: PropTypes.number, description: PropTypes.string }),
    hubs: PropTypes.arrayOf(
        PropTypes.shape({ hubId: PropTypes.number, description: PropTypes.string })
    ),
    hubsLoading: PropTypes.bool,
    getCarrier: PropTypes.func.isRequired,
    carrier: PropTypes.shape({ carrierCode: PropTypes.string, name: PropTypes.string }),
    carriers: PropTypes.arrayOf(
        PropTypes.shape({ carrierCode: PropTypes.string, name: PropTypes.string })
    ),
    carriersLoading: PropTypes.bool
};

Consignment.defaultProps = {
    item: {},
    loading: false,
    requestErrors: null,
    openConsignments: [],
    optionsLoading: false,
    startingTab: 0,
    editStatus: 'view',
    hub: null,
    hubs: [],
    hubsLoading: false,
    carrier: null,
    carriers: [],
    carriersLoading: false
};

export default Consignment;
