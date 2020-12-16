import React from 'react';
import PropTypes from 'prop-types';
import { EditableTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';

function UsagesTab({
    usages,
    searchRootProducts,
    clearhRootProductsSearch,
    rootProductsSearchResults,
    rootProductsSearchLoading,
    handleRootProductChange,
    deleteRow,
    saveRow,
    newRow,
    setNewRow
}) {
    const selectRootProductSearchResult = (_propertyName, part, updatedItem) => {
        handleRootProductChange(updatedItem.sequence, part);
        setNewRow(() => ({
            ...updatedItem,
            partNumber: part.partNumber,
            supplierName: part.description
        }));
    };

    const columns = [
        {
            title: 'Product',
            id: 'rootProductName',
            type: 'search',
            editable: true,
            search: searchRootProducts,
            clearSearch: clearhRootProductsSearch,
            searchResults: rootProductsSearchResults,
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
                columns={columns}
                rows={usages.map(m => ({ ...m, id: m.rootProduct }))}
                newRow={newRow}
                createRow={saveRow}
                saveRow={saveRow}
                deleteRow={deleteRow}
                closeEditingOnSave
            />
        </Grid>
    );
}

UsagesTab.propTypes = {
    handleRootProductChange: PropTypes.func.isRequired,
    saveRow: PropTypes.func.isRequired,
    deleteRow: PropTypes.func.isRequired,
    usages: PropTypes.arrayOf(PropTypes.shape({})),
    searchRootProducts: PropTypes.func.isRequired,
    clearhRootProductsSearch: PropTypes.func.isRequired,
    rootProductsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    rootProductsSearchLoading: PropTypes.bool,
    setNewRow: PropTypes.func.isRequired,
    newRow: PropTypes.arrayOf(PropTypes.shape({}))
};

UsagesTab.defaultProps = {
    usages: [],
    rootProductsSearchResults: [],
    rootProductsSearchLoading: false,
    newRow: []
};

export default UsagesTab;
