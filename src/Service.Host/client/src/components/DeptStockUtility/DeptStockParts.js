import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Title, Typeahead } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function DeptStockParts({ items, fetchItems, itemsLoading, clearSearch, history }) {
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Departmental Pallets" />
                </Grid>
                <Grid item xs={12}>
                    <Typeahead
                        items={items}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={itemsLoading}
                        title="Search Part"
                        history={history}
                        minimumSearchTermLength={3}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

DeptStockParts.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string,
            href: PropTypes.string
        })
    ).isRequired,
    itemsLoading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired
};

DeptStockParts.defaultProps = {
    itemsLoading: false
};

export default DeptStockParts;
