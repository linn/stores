import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, LinkButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function PartsSearch({ items, fetchItems, loading, clearSearch, history, privileges }) {
    const searchItems = items.map(item => ({
        ...item,
        name: item.partNumber.toString(),
        description: item.description
    }));

    const canCreate = () => {
        if (!(privileges.length < 1)) {
            return privileges.some(priv => priv === 'part.admin');
        }
        return false;
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={11} />
                <Grid item xs={1}>
                    <LinkButton
                        text="Create"
                        to="/inventory/parts/create"
                        disabled={!canCreate()}
                        tooltip={canCreate() ? null : 'You are not authorised to create parts.'}
                    />
                </Grid>
                <Grid item xs={12}>
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

PartsSearch.propTypes = {
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
    history: PropTypes.shape({}).isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired
};

PartsSearch.defaultProps = {
    loading: false
};

export default PartsSearch;
