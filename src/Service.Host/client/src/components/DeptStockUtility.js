import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../containers/Page';

function DeptStockUtility({
    items,
    fetchItems,
    itemsLoading,
    clearSearch,
    departments,
    clearDepartmentsSearch,
    searchDepartments,
    departmentsLoading,
    storagePlaces,
    clearStoragePlacesSearch,
    searchStoragePlaces,
    storagePlacesLoading
}) {
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typeahead
                        items={departments}
                        fetchItems={searchDepartments}
                        clearSearch={clearDepartmentsSearch}
                        modal
                        links={false}
                        loading={departmentsLoading}
                        title="Search Departments"
                        onSelect={newValue => {
                            console.log(newValue.name);
                        }}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Typeahead
                        items={items}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        modal
                        links={false}
                        loading={itemsLoading}
                        title="Search Parts"
                        onSelect={newValue => {
                            console.log(newValue.name);
                        }}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Typeahead
                        items={storagePlaces}
                        fetchItems={searchStoragePlaces}
                        clearSearch={clearStoragePlacesSearch}
                        modal
                        links={false}
                        loading={storagePlacesLoading}
                        title="Search Storage Places"
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
    itemsLoading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    departments: PropTypes.arrayOf(PropTypes.shape({})),
    clearDepartmentsSearch: PropTypes.func.isRequired,
    searchDepartments: PropTypes.func.isRequired,
    departmentsLoading: PropTypes.bool,
    storagePlaces: PropTypes.arrayOf(PropTypes.shape({})),
    clearStoragePlacesSearch: PropTypes.func.isRequired,
    searchStoragePlaces: PropTypes.func.isRequired,
    storagePlacesLoading: PropTypes.bool
};

DeptStockUtility.defaultProps = {
    itemsLoading: false,
    departments: [],
    departmentsLoading: false,
    storagePlacesLoading: false,
    storagePlaces: []
};

export default DeptStockUtility;
