import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import Divider from '@material-ui/core/Divider';
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
    storagePlaces,
    clearStoragePlacesSearch,
    searchStoragePlaces,
    storagePlacesLoading
}) {
    const [prevStockLocators, setPrevStockLocators] = useState([]);
    const [stockLocators, setStockLocators] = useState(null);
    useEffect(() => {
        if (stockLocators !== prevStockLocators) {
            if (items.length > 0) {
                setPrevStockLocators(items);
                setStockLocators(items);
            }
        }
    }, [items, stockLocators, prevStockLocators]);

    const selectStoragePlaceSearchResult = (_propertyName, storagePlace, updatedItem) => {
        console.log(updatedItem, storagePlace);
    };
    const editableColumns = [
        {
            title: 'Storage Place',
            id: 'storagePlace',
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
            id: 'stocragePlaceDescription',
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
                    <Title text="Departmental Pallets Utility" />{' '}
                </Grid>
                <Grid item xs={12}>
                    {itemsLoading && <Loading />}
                    {stockLocators && (
                        <SingleEditTable
                            columns={editableColumns}
                            rows={stockLocators}
                            saveRow={() => {}} // updateRow Functions
                            editable
                            allowNewRowCreations
                        />
                    )}
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
    ),
    itemsLoading: PropTypes.bool,
    fetchStockLocators: PropTypes.func.isRequired,
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
    items: null,
    departments: [],
    departmentsLoading: false,
    storagePlacesLoading: false,
    storagePlaces: []
};

export default DeptStockUtility;
