import React, { useEffect, useReducer } from 'react';
import moment from 'moment';
import Button from '@material-ui/core/Button';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import TableBody from '@material-ui/core/TableBody';
import { makeStyles } from '@material-ui/core/styles';
import {
    TypeaheadDialog,
    Loading,
    Title,
    CheckboxWithLabel
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../containers/Page';
import reducer from '../reducers/componentReducers/exportRsnReducer';

const useStyles = makeStyles(theme => ({
    label: {
        paddingLeft: theme.spacing(2),
        paddingTop: theme.spacing(1)
    }
}));

export default function ExportRsns({
    salesOutletsSearchResults,
    salesOutletsSearchLoading,
    salesAccountsSearchResults,
    salesAccountsSearchLoading,
    rsnsSearchResults,
    rsnsSearchLoading,
    searchSalesOutlets,
    searchSalesAccounts,
    searchRsns,
    createExportReturn
}) {
    const [state, dispatch] = useReducer(reducer, {
        searchResults: [],
        selectedAccount: null,
        rsns: [],
        belgiumShipping: false
    });

    useEffect(() => {
        dispatch({
            type: 'receiveSalesAccountsSearchResults',
            payload: salesAccountsSearchResults
        });
    }, [salesAccountsSearchResults]);

    useEffect(() => {
        dispatch({ type: 'receiveSalesOutletsSearchResults', payload: salesOutletsSearchResults });
    }, [salesOutletsSearchResults]);

    useEffect(() => {
        dispatch({ type: 'receiveRsnsSearchResults', payload: rsnsSearchResults });
    }, [rsnsSearchResults]);

    const classes = useStyles();

    const searchAccountsAndOultets = name => {
        searchSalesOutlets(name);
        searchSalesAccounts(name);
    };

    const getOldestDate = () =>
        moment(
            state.rsns.reduce((rsn, oldest) =>
                oldest.dateEntered < rsn.dateEntered ? oldest : rsn
            ).dateEntered
        ).format('DD MMM YYYY');

    const handleSelectAccount = item => {
        dispatch({ type: 'selectAccount', payload: item });
        if (item.type === 'account') {
            searchRsns(null, `&accountId=${item.accountId}`);
        } else {
            searchRsns(null, `&accountId=${item.accountId}&outletNumber=${item.outletNumber}`);
        }
    };

    const handleCreateExportReturn = () => {
        createExportReturn({
            rsns: state.rsns,
            hubReturn: state.belgiumShipping
        });
    };

    const OutletTable = () => (
        <Grid item xs={12}>
            <Paper>
                <Table size="small">
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Account</TableCell>
                            <TableCell>Country</TableCell>
                            <TableCell>No RSNs</TableCell>
                            <TableCell>Oldest RSN</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        <TableRow>
                            <TableCell>{state.selectedAccount.name}</TableCell>
                            <TableCell>{`${state.selectedAccount.accountId} / ${state.selectedAccount.outletNumber}`}</TableCell>
                            <TableCell>{state.selectedAccount.countryName}</TableCell>
                            <TableCell>{state.rsns ? state.rsns.length : ''}</TableCell>
                            <TableCell>{state.rsns.length ? getOldestDate() : ''}</TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </Paper>
        </Grid>
    );

    const AccountTable = () => (
        <Grid item xs={12}>
            <Paper>
                <Table size="small">
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Account</TableCell>
                            <TableCell>No RSNs</TableCell>
                            <TableCell>Oldest RSN</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        <TableRow>
                            <TableCell>{state.selectedAccount.name}</TableCell>
                            <TableCell>{state.selectedAccount.accountId}</TableCell>
                            <TableCell>{state.rsns ? state.rsns.length : ''}</TableCell>
                            <TableCell>{state.rsns.length ? getOldestDate() : ''}</TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </Paper>
        </Grid>
    );

    const RsnsTable = () => (
        <Grid item xs={12}>
            <Paper>
                <Table size="small">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell>RSN</TableCell>
                            <TableCell>Reason Code</TableCell>
                            <TableCell>Article Number</TableCell>
                            <TableCell>Date Entered</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {state.rsns.map(rsn => (
                            <TableRow key={rsn.rsnNumber}>
                                <TableCell>
                                    <Checkbox
                                        checked={rsn.selected}
                                        onChange={() => {
                                            dispatch({ type: 'selectRsn', payload: rsn });
                                        }}
                                        color="primary"
                                        data-testid="rsnCheckbox"
                                    />
                                </TableCell>
                                <TableCell>{rsn.rsnNumber}</TableCell>
                                <TableCell>{rsn.reasonCodeAlleged}</TableCell>
                                <TableCell>{rsn.articleNumber}</TableCell>
                                <TableCell>
                                    {moment(rsn.dateEntered).format('DD MMM YYYY')}
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </Paper>
        </Grid>
    );

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Make Export Return" />
                </Grid>
                <Grid item xs={12}>
                    <FormControlLabel
                        label="Search for Sales Outlet or Account"
                        classes={{ label: classes.label }}
                        control={
                            <TypeaheadDialog
                                searchItems={state.searchResults}
                                fetchItems={searchAccountsAndOultets}
                                clearSearch={() => dispatch({ type: 'clearSearchResults' })}
                                loading={salesAccountsSearchLoading || salesOutletsSearchLoading}
                                title="Search Accounts and Outlets"
                                onSelect={item => handleSelectAccount(item)}
                            />
                        }
                    />
                </Grid>
                {state.selectedAccount?.type === 'outlet' && <OutletTable />}
                {state.selectedAccount?.type === 'account' && <AccountTable />}
                {rsnsSearchLoading && (
                    <div data-testid="rsnsLoading">
                        <Loading />
                    </div>
                )}
                {!!state.rsns.length && (
                    <>
                        <RsnsTable />
                        <Grid item xs={12}>
                            <CheckboxWithLabel
                                label="Pick up from Belgium hub"
                                onChange={() => dispatch({ type: 'setBelgiumShipping' })}
                                checked={state.belgiumShipping}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <Button
                                variant="outlined"
                                color="primary"
                                disabled={!state.rsns.some(rsn => rsn.selected)}
                                onClick={() => handleCreateExportReturn()}
                            >
                                Make Export Return
                            </Button>
                        </Grid>
                    </>
                )}
            </Grid>
        </Page>
    );
}

ExportRsns.propTypes = {
    salesOutletsSearchResults: PropTypes.arrayOf(PropTypes.shape()),
    salesOutletsSearchLoading: PropTypes.bool,
    salesAccountsSearchResults: PropTypes.arrayOf(PropTypes.shape()),
    salesAccountsSearchLoading: PropTypes.bool,
    rsnsSearchResults: PropTypes.arrayOf(PropTypes.shape()),
    rsnsSearchLoading: PropTypes.bool,
    searchSalesOutlets: PropTypes.func.isRequired,
    searchSalesAccounts: PropTypes.func.isRequired,
    searchRsns: PropTypes.func.isRequired,
    createExportReturn: PropTypes.func.isRequired
};

ExportRsns.defaultProps = {
    salesOutletsSearchResults: [],
    salesOutletsSearchLoading: false,
    salesAccountsSearchResults: [],
    salesAccountsSearchLoading: false,
    rsnsSearchResults: [],
    rsnsSearchLoading: false
};
