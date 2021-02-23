import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Table from '@material-ui/core/Table';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import TableBody from '@material-ui/core/TableBody';
import {
    TypeaheadDialog,
    LinkButton,
    Dropdown,
    useSearch,
    InputField
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../containers/Page';

export default function SalesOutlets({ searchResults, searchLoading, fetchItems, clearSearch }) {
    const [salesOutlet, setSalesOutlet] = useState(null);

    const handleSalesOutletSelect = item => {
        setSalesOutlet(item);
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={4}>
                    <Typography>Search</Typography>
                </Grid>
                <Grid item xs={8}>
                    <TypeaheadDialog
                        searchItems={searchResults}
                        fetchItems={fetchItems}
                        clearSearch={() => clearSearch}
                        loading={searchLoading}
                        title="Sales Outlets"
                        onSelect={handleSalesOutletSelect}
                    />
                </Grid>
                {salesOutlet && (
                    <Grid item xs={12}>
                        <Table>
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
                                    <TableCell>{salesOutlet.name}</TableCell>
                                    <TableCell>{`${salesOutlet.accountId} / ${salesOutlet.outletNumber}`}</TableCell>
                                    <TableCell>{salesOutlet.countryName}</TableCell>
                                    <TableCell></TableCell>
                                    <TableCell></TableCell>
                                </TableRow>
                            </TableBody>
                        </Table>
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

SalesOutlets.propTypes = {
    searchResults: PropTypes.arrayOf(PropTypes.shape()),
    searchLoading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired
};

SalesOutlets.defaultProps = {
    searchResults: [],
    searchLoading: false
};
