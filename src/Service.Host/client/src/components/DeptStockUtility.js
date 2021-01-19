import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../containers/Page';

function DeptStockUtility({ items, fetchItems, loading, clearSearch }) {
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typeahead
                        items={items}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        modal
                        loading={loading}
                        title="Search Parts"
                        onSelect={newValue => {
                            console.log(newValue.name);
                        }}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

DeptStockUtility.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string,
            href: PropTypes.string
        })
    ).isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired
};

DeptStockUtility.defaultProps = {
    loading: false
};

export default DeptStockUtility;
