import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Title,
    SingleEditTable,
    Loading,
    SnackbarMessage,
    ErrorCard,
    BackButton
} from '@linn-it/linn-form-components-library';
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
    options,
    snackbarVisible,
    setSnackbarVisible,
    itemError,
    history
}) {
    const [prevStockLocators, setPrevStockLocators] = useState(null);
    const [stockLocators, setStockLocators] = useState([]);
    const [newRow, setNewRow] = useState({
        id: -1,
        partNumber: options.partNumber,
        stockRotationDate: new Date()
    });
    useEffect(() => {
        if (items !== prevStockLocators) {
            if (items) {
                setPrevStockLocators(items);
                setStockLocators(items);
                setNewRow({
                    id: -1,
                    partNumber: options.partNumber,
                    stockRotationDate: new Date()
                });
            }
        }
    }, [items, stockLocators, prevStockLocators, setNewRow, options]);

    const selectDepartmentsSearchResult = (_propertyName, department, updatedItem) => {
        setStockLocators(s =>
            s.map(x =>
                x.id === updatedItem.id
                    ? { ...x, auditDepartmentCode: department.departmentCode }
                    : x
            )
        );

        console.log(newRow);
        setNewRow(x => ({ ...x, auditDepartmentCode: department.departmentCode }));
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

    const handleValueChange = (item, propertyName, newValue) => {
        if (item.id === -1) {
            setNewRow(row => ({ ...row, [propertyName]: newValue }));
        } else {
            setStockLocators(s =>
                s.map(row => (row.id === item.id ? { ...row, [propertyName]: newValue } : row))
            );
        }
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
            searchTitle: 'Search Storage Places',
            minimumSearchTermLength: 3,
            required: true
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
            required: false,
            editable: true
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
            type: 'number',
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
            searchTitle: 'Search Departments',
            minimumSearchTermLength: 3,
            required: false
        }
    ];
    return (
        <Page>
            <SnackbarMessage
                visible={snackbarVisible}
                onClose={() => setSnackbarVisible(false)}
                message="Save Successful"
            />
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Departmental Pallets Utility" />
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard
                            errorMessage={itemError?.details?.errors?.[0] || itemError.statusText}
                        />
                    </Grid>
                )}
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
                                updateRow={(item, _setItem, propertyName, newValue) => {
                                    handleValueChange(item, propertyName, newValue);
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
                <Grid item xs={12}>
                    <BackButton backClick={() => history.push('/inventory/dept-stock-parts')} />
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
    options: PropTypes.shape({ partNumber: PropTypes.string }).isRequired,
    snackbarVisible: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        item: PropTypes.string,
        details: PropTypes.shape({
            errors: PropTypes.arrayOf(PropTypes.shape({}))
        })
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired
};

DeptStockUtility.defaultProps = {
    itemsLoading: false,
    items: [],
    departments: [],
    departmentsLoading: false,
    storagePlacesLoading: false,
    storagePlaces: [],
    stockLocatorLoading: false,
    snackbarVisible: false,
    itemError: null
};

export default DeptStockUtility;
