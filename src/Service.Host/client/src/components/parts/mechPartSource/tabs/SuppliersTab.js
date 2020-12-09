import React from 'react';
import PropTypes from 'prop-types';
import { EditableTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';

function SuppliersTab({
    suppliers,
    searchSuppliers,
    clearSuppliersSearch,
    suppliersSearchResults,
    suppliersSearchLoading,
    handleSupplierChange,
    deleteRow,
    saveRow,
    newRow,
    setNewRow
}) {
    const selectSupplierSearchResult = (_propertyName, supplier, updatedItem) => {
        handleSupplierChange(updatedItem.sequence, supplier);
        setNewRow(() => ({
            ...updatedItem,
            supplierId: supplier.name,
            supplierName: supplier.description
        }));
    };

    const columns = [
        {
            title: 'Supplier',
            id: 'supplierId',
            type: 'search',
            editable: true,
            search: searchSuppliers,
            clearSearch: clearSuppliersSearch,
            searchResults: suppliersSearchResults,
            searchLoading: suppliersSearchLoading,
            selectSearchResult: selectSupplierSearchResult,
            searchTitle: 'Search Suppliers'
        },
        {
            title: 'Name',
            id: 'supplierName',
            type: 'text',
            editable: false
        },
        {
            title: 'Part Number',
            id: 'partNumber',
            type: 'text',
            editable: true
        }
    ];

    return (
        <Grid item xs={12}>
            <EditableTable
                columns={columns}
                rows={suppliers.map(m => ({ ...m, id: m.sequence }))}
                newRow={newRow}
                createRow={saveRow}
                saveRow={saveRow}
                deleteRow={deleteRow}
            />
        </Grid>
    );
}

SuppliersTab.propTypes = {
    handleSupplierChange: PropTypes.func.isRequired,
    saveRow: PropTypes.func.isRequired,
    deleteRow: PropTypes.func.isRequired,
    suppliers: PropTypes.arrayOf(PropTypes.shape({})),
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired,
    suppliersSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    suppliersSearchLoading: PropTypes.bool,
    setNewRow: PropTypes.func.isRequired,
    newRow: PropTypes.arrayOf(PropTypes.shape({}))
};

SuppliersTab.defaultProps = {
    suppliers: [],
    suppliersSearchResults: [],
    suppliersSearchLoading: false,
    newRow: []
};

export default SuppliersTab;
