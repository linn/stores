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
    resetRow,
    addNewRow,
    updateRow
}) {
    const selectSupplierSearchResult = (_propertyName, supplier, updatedItem) => {
        handleSupplierChange(updatedItem.sequence, supplier);
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
                groupEdit
                columns={columns}
                rows={suppliers.map(m => ({
                    ...m,
                    id: m.sequence,
                    supplierId: m.supplierId?.toString()
                }))}
                removeRow={deleteRow}
                //deleteRow={() => true}
                resetRow={resetRow}
                addRow={addNewRow}
                tableValid={() => true}
                updateRow={updateRow}
                //closeRowOnClickAway
            />
        </Grid>
    );
}

SuppliersTab.propTypes = {
    handleSupplierChange: PropTypes.func.isRequired,
    resetRow: PropTypes.func.isRequired,
    deleteRow: PropTypes.func.isRequired,
    suppliers: PropTypes.arrayOf(PropTypes.shape({})),
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired,
    suppliersSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    suppliersSearchLoading: PropTypes.bool,
    updateRow: PropTypes.func.isRequired,
    addNewRow: PropTypes.func.isRequired
};
SuppliersTab.defaultProps = {
    suppliers: [],
    suppliersSearchResults: [],
    suppliersSearchLoading: false
};

export default SuppliersTab;
