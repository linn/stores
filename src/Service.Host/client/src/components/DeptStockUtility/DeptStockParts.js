import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { BackButton, Loading, Title, Typeahead } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function DeptStockParts({ items, itemsLoading, history }) {
    const [searchResults, setSearchResults] = useState([]);
    const [hasSearched, setHasSearched] = useState(false);

    useEffect(() => {
        if (hasSearched) {
            setSearchResults(items);
        }
    }, [items, hasSearched]);

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Departmental Pallets" />
                </Grid>
                <Grid item xs={12}>
                    {itemsLoading ? (
                        <Loading />
                    ) : (
                        <Typeahead
                            items={searchResults.slice(0, 10)}
                            fetchItems={searchTerm => {
                                setHasSearched(true);
                                setSearchResults(
                                    items?.filter(i =>
                                        i.partNumber.includes(searchTerm?.toUpperCase())
                                    )
                                );
                            }}
                            clearSearch={() => {}}
                            loading={itemsLoading}
                            title="Filter Dept Stock Parts"
                            history={history}
                            minimumSearchTermLength={2}
                            debounce={1}
                        />
                    )}
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
    history: PropTypes.shape({}).isRequired
};

DeptStockParts.defaultProps = {
    itemsLoading: false
};

export default DeptStockParts;
