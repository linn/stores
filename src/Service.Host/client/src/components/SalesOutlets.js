import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import {
    TypeaheadDialog,
    LinkButton,
    Dropdown,
    useSearch
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../containers/Page';

export default function SalesOutlets({ searchResults, searchLoading, fetchItems, clearSearch }) {
    const handleSalesOutletSelect = () => {
        console.log('select');
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
