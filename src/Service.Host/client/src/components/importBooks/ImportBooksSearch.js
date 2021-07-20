import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, LinkButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function ImportBooksSearch({ items, fetchItems, loading, clearSearch, history, privileges }) {
    const createUrl = () => {
        '/logistics/import-books';
    };

    const searchItems = () => {
        return items?.map(item => ({
            ...item,
            name: item.id.toString(),
            description: `${item.id}, created ${item.dateCreated}`,
            href: item.href
        }));
    };

    const canCreate = () => {
        if (!(privileges.length < 1)) {
            return privileges.some(priv => priv === 'import-books.admin');
        }
        return false;
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={10} />
                <Grid item xs={1}>
                    <LinkButton
                        text="Create"
                        to={createUrl()}
                        disabled={!canCreate()}
                        tooltip={
                            canCreate() ? null : 'You are not authorised to create import books.'
                        }
                    />
                </Grid>
                <Grid item xs={1} />

                <Grid item xs={12}>
                    <Typeahead
                        items={searchItems()}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title="Import Books"
                        history={history}
                        debounce={1000}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

ImportBooksSearch.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            href: PropTypes.string
        })
    ).isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired
};

ImportBooksSearch.defaultProps = {
    loading: false
};

export default ImportBooksSearch;
