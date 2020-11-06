import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, LinkButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function ParcelsSearch({ items, fetchItems, loading, clearSearch, history }) {
    const searchItems = items.map(item => ({
        ...item,
        id: item.parcelNumber,
        name: item.parcelNumber.toString(),
        description: item.description
    }));

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={7} />
                <Grid item xs={1}>
                    <LinkButton text="Create" to="/inventory/parcels/create" />
                </Grid>
                <Grid item xs={1} />
                <Grid item xs={12}>
                    <Typeahead
                        items={searchItems}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title="Parcels"
                        history={history}
                        placeholder="Search for parcels"
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

ParcelsSearch.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            parcelNumber: PropTypes.int
        })
    ).isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired
};

ParcelsSearch.defaultProps = {
    loading: false
};

export default ParcelsSearch;
