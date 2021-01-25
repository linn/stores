import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { Title, SingleEditTable, Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function DeptStockUtility({
    items,
    itemsLoading,
    departments,
    clearDepartmentsSearch,
    searchDepartments,
    departmentsLoading,
    updateStockLocator,
    stockLocatorLoading,
    storagePlaces,
    clearStoragePlacesSearch,
    searchStoragePlaces,
    storagePlacesLoading
}) {
    const [prevStockLocators, setPrevStockLocators] = useState([]);
    const [stockLocators, setStockLocators] = useState(null);
    useEffect(() => {
        if (items !== prevStockLocators) {
            if (items.length > 0) {
                setPrevStockLocators(items);
                setStockLocators(items);
            }
        }
    }, [items, stockLocators, prevStockLocators]);

    const selectStoragePlaceSearchResult = (_propertyName, storagePlace, updatedItem) => {
        setStockLocators(s =>
            s.map(x => {
                return x.id === updatedItem.id
                    ? {
                          ...x,
                          storagePlaceName: storagePlace.name,
                          storagePlaceDescription: storagePlace.description
                      }
                    : x;
            })
        );
    };
    const editableColumns = [
        {
            title: 'Storage Place',
            id: 'storagePlaceName',
            type: 'search',
            editable: true,
            search: searchStoragePlaces,
            clearSearch: clearStoragePlacesSearch,
            searchResults: storagePlaces,
            searchLoading: storagePlacesLoading,
            selectSearchResult: selectStoragePlaceSearchResult,
            searchTitle: 'Search Storage Places'
        },
        {
            title: 'Description',
            id: 'storagePlaceDescription',
            type: 'text',
            editable: false
        },
        {
            title: 'Batch Ref',
            id: 'batchRef',
            type: 'text',
            editable: false,
            required: true
        },
        {
            title: 'Batch Date',
            id: 'stockRotationDate',
            type: 'text', //date
            editable: true
        },
        {
            title: 'Qty',
            id: 'quantity',
            type: 'text', //date
            editable: true
        },
        {
            title: 'Remarks',
            id: 'remarks',
            type: 'text', //date
            editable: true
        }
    ];
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Departmental Pallets Utility" />
                </Grid>
                {itemsLoading || stockLocatorLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <Grid item xs={12}>
                        {stockLocators && (
                            <SingleEditTable
                                columns={editableColumns}
                                rows={stockLocators}
                                //updateRow={updateStockLocator}
                                saveRow={item => {
                                    updateStockLocator(item.id, item);
                                }} // updateRow Functions
                                editable
                                allowNewRowCreations
                            />
                        )}
                    </Grid>
                )}
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
    ),
    itemsLoading: PropTypes.bool,
    departments: PropTypes.arrayOf(PropTypes.shape({})),
    clearDepartmentsSearch: PropTypes.func.isRequired,
    searchDepartments: PropTypes.func.isRequired,
    departmentsLoading: PropTypes.bool,
    storagePlaces: PropTypes.arrayOf(PropTypes.shape({})),
    clearStoragePlacesSearch: PropTypes.func.isRequired,
    searchStoragePlaces: PropTypes.func.isRequired,
    storagePlacesLoading: PropTypes.bool,
    updateStockLocator: PropTypes.func.isRequired
};

DeptStockUtility.defaultProps = {
    itemsLoading: false,
    items: null,
    departments: [],
    departmentsLoading: false,
    storagePlacesLoading: false,
    storagePlaces: []
};

export default DeptStockUtility;
