import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, CreateButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function PartFails({ items, fetchItems, loading, clearSearch, history }) {
    const searchItems = items.map(item => ({
        ...item,
        name: item.partNumber.toString(),
        description: item.description
    }));

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <>
                        <CreateButton createUrl="/parts/create" />
                    </>
                    <Typeahead
                        items={searchItems}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title="Part"
                        history={history}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

PartFails.propTypes = {
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
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired
};

PartFails.defaultProps = {
    loading: false
};

export default PartFails;
