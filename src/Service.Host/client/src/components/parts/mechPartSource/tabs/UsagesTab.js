import React from 'react';
import PropTypes from 'prop-types';
import { GroupEditTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';

function UsagesTab({
    rows,
    searchRootProducts,
    clearRootProductsSearch,
    rootProductsSearchResults,
    rootProductsSearchLoading,
    handleRootProductChange,
    resetRow,
    setRowToBeDeleted,
    setRowToBeSaved,
    setEditing,
    removeRow,
    addRow,
    updateRow
}) {
    const selectRootProductSearchResult = (_propertyName, rootProduct, updatedItem) => {
        handleRootProductChange(updatedItem.rootProductName, rootProduct);
    };

    const columns = [
        {
            title: 'Product',
            id: 'rootProductName',
            type: 'search',
            editable: true,
            search: searchRootProducts,
            clearSearch: clearRootProductsSearch,
            searchResults: rootProductsSearchResults.filter(
                p => !rows.some(u => u.rootProductName === p.name)
            ),
            searchLoading: rootProductsSearchLoading,
            selectSearchResult: selectRootProductSearchResult,
            searchTitle: 'Search Parts',
            minimumSearchTermLength: 3
        },
        {
            title: 'Description',
            id: 'rootProductDescription',
            type: 'text',
            editable: false
        },
        {
            title: 'Quantity Used',
            id: 'quantityUsed',
            type: 'number',
            editable: true
        }
    ];

    return (
        <Grid item xs={12}>
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
                removeRowOnDelete
                closeRowOnClickAway={false}
            />
        </Grid>
    );
}

UsagesTab.propTypes = {
    handleRootProductChange: PropTypes.func.isRequired,
    rows: PropTypes.arrayOf(PropTypes.shape({})),
    searchRootProducts: PropTypes.func.isRequired,
    clearRootProductsSearch: PropTypes.func.isRequired,
    rootProductsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    rootProductsSearchLoading: PropTypes.bool,
    resetRow: PropTypes.func.isRequired,
    updateRow: PropTypes.func.isRequired,
    setRowToBeDeleted: PropTypes.func.isRequired,
    setRowToBeSaved: PropTypes.func.isRequired,
    setEditing: PropTypes.func.isRequired,
    removeRow: PropTypes.func.isRequired,
    addRow: PropTypes.func.isRequired
};

UsagesTab.defaultProps = {
    rows: [],
    rootProductsSearchResults: [],
    rootProductsSearchLoading: false
};

export default UsagesTab;
