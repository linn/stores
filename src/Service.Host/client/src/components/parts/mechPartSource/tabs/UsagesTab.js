import React from 'react';
import PropTypes from 'prop-types';
import { EditableTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';

function UsagesTab({
    usages,
    searchRootProducts,
    clearRootProductsSearch,
    rootProductsSearchResults,
    rootProductsSearchLoading,
    handleRootProductChange,
    deleteRow,
    resetRow,
    addNewRow,
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
                p => !usages.some(u => u.rootProductName === p.name)
            ),
            searchLoading: rootProductsSearchLoading,
            selectSearchResult: selectRootProductSearchResult,
            searchTitle: 'Search Parts'
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
            <EditableTable
                groupEdit
                columns={columns}
                rows={usages.map(m => ({ ...m, id: m.id }))}
                removeRow={deleteRow}
                deleteRow={() => true}
                resetRow={resetRow}
                addRow={addNewRow}
                tableValid={() => true}
                updateRow={updateRow}
                closeRowOnClickAway
            />
        </Grid>
    );
}

UsagesTab.propTypes = {
    handleRootProductChange: PropTypes.func.isRequired,
    deleteRow: PropTypes.func.isRequired,
    usages: PropTypes.arrayOf(PropTypes.shape({})),
    searchRootProducts: PropTypes.func.isRequired,
    clearRootProductsSearch: PropTypes.func.isRequired,
    rootProductsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    rootProductsSearchLoading: PropTypes.bool,
    resetRow: PropTypes.func.isRequired,
    addNewRow: PropTypes.func.isRequired,
    updateRow: PropTypes.func.isRequired
};

UsagesTab.defaultProps = {
    usages: [],
    rootProductsSearchResults: [],
    rootProductsSearchLoading: false
};

export default UsagesTab;
