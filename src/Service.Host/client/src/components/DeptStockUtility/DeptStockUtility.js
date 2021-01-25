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
    createStockLocator,
    deleteStockLocator,
    stockLocatorLoading,
    storagePlaces,
    clearStoragePlacesSearch,
    searchStoragePlaces,
    storagePlacesLoading,
    options
}) {
    const [prevStockLocators, setPrevStockLocators] = useState([]);
    const [stockLocators, setStockLocators] = useState(null);
    const [newRow, setNewRow] = useState({
        id: -1,
        partNumber: options.partNumber,
        stockRotationDate: new Date()
    });
    useEffect(() => {
        if (items !== prevStockLocators) {
            if (items.length > 0) {
                setPrevStockLocators(items);
                setStockLocators(items);
            }
        }
    }, [items, stockLocators, prevStockLocators]);

    const selectDepartmentsSearchResult = (_propertyName, department, updatedItem) => {
        setStockLocators(s =>
            s.map(x =>
                x.id === updatedItem.id
                    ? { ...x, AuditDepartmentCode: department.departmentCode }
                    : x
            )
        );
    };

    const selectStoragePlaceSearchResult = (_propertyName, storagePlace, updatedItem) =>
        setStockLocators(s => {
            let updatedExisting = false;
            const updatedStockLocators = s.map(x => {
                if (x.id === updatedItem.id) {
                    updatedExisting = true;
                    return {
                        ...x,
                        storagePlaceName: storagePlace.name,
                        storagePlaceDescription: storagePlace.description,
                        palletNumber: storagePlace.palletNumber,
                        locationId: storagePlace.locationId
                    };
                }
                return x;
            });
            if (!updatedExisting) {
                setNewRow(x => ({
                    ...x,
                    storagePlaceName: storagePlace.name,
                    storagePlaceDescription: storagePlace.description,
                    palletNumber: storagePlace.palletNumber,
                    locationId: storagePlace.locationId
                }));
            }
            return updatedStockLocators;
        });

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
            type: 'date',
            editable: true
        },
        {
            title: 'Qty',
            id: 'quantity',
            type: 'text',
            editable: true
        },
        {
            title: 'Remarks',
            id: 'remarks',
            type: 'text',
            editable: true
        },
        {
            title: 'Audit Dept',
            id: 'auditDepartmentCode',
            type: 'search',
            editable: true,
            search: searchDepartments,
            clearSearch: clearDepartmentsSearch,
            searchResults: departments,
            searchLoading: departmentsLoading,
            selectSearchResult: selectDepartmentsSearchResult,
            searchTitle: 'Search Storage Places'
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
                                saveRow={item => {
                                    updateStockLocator(item.id, item);
                                }}
                                createRow={item => createStockLocator(item)}
                                newRow={newRow}
                                editable
                                allowNewRowCreations
                                deleteRow={deleteStockLocator}
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
    updateStockLocator: PropTypes.func.isRequired,
    createStockLocator: PropTypes.func.isRequired,
    deleteStockLocator: PropTypes.func.isRequired,
    stockLocatorLoading: PropTypes.bool,
    options: PropTypes.shape({ partNumber: PropTypes.string }).isRequired
};

DeptStockUtility.defaultProps = {
    itemsLoading: false,
    items: null,
    departments: [],
    departmentsLoading: false,
    storagePlacesLoading: false,
    storagePlaces: [],
    stockLocatorLoading: false
};

export default DeptStockUtility;
