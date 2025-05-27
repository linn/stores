import React from 'react';
import PropTypes from 'prop-types';
import { GroupEditTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';

function SuppliersTab({
    searchSuppliers,
    clearSuppliersSearch,
    suppliersSearchResults,
    suppliersSearchLoading,
    handleSupplierChange,
    rows,
    setRowToBeDeleted,
    setRowToBeSaved,
    setEditing,
    removeRow,
    addRow,
    updateRow,
    resetRow
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
            searchTitle: 'Search Suppliers',
            minimumSearchTermLength: 3
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
            {rows && (
                <GroupEditTable
                    columns={columns}
                    rows={rows}
                    updateRow={updateRow}
                    addRow={addRow}
                    removeRow={removeRow}
                    resetRow={resetRow}
                    handleEditClick={setEditing}
                    tableValid={() => true}
                    editable
                    allowNewRowCreation
                    deleteRowPreEdit={false}
                    setRowToBeSaved={setRowToBeSaved}
                    setRowToBeDeleted={setRowToBeDeleted}
                    closeRowOnClickAway={false}
                    removeRowOnDelete
                />
            )}
        </Grid>
    );
}

SuppliersTab.propTypes = {
    handleSupplierChange: PropTypes.func.isRequired,
    resetRow: PropTypes.func.isRequired,
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired,
    suppliersSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    suppliersSearchLoading: PropTypes.bool,
    updateRow: PropTypes.func.isRequired,
    rows: PropTypes.arrayOf(PropTypes.shape({})),
    setRowToBeDeleted: PropTypes.func.isRequired,
    setRowToBeSaved: PropTypes.func.isRequired,
    setEditing: PropTypes.func.isRequired,
    removeRow: PropTypes.func.isRequired,
    addRow: PropTypes.func.isRequired
};

SuppliersTab.defaultProps = {
    rows: [],
    suppliersSearchResults: [],
    suppliersSearchLoading: false
};

export default SuppliersTab;
