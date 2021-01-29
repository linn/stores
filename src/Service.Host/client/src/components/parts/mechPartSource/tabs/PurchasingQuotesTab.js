import React from 'react';
import PropTypes from 'prop-types';
import { GroupEditTable, InputField } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';

function PurchasingQuotesTab({
    rows,
    searchSuppliers,
    clearSuppliersSearch,
    suppliersSearchResults,
    suppliersSearchLoading,
    handleSupplierChange,
    searchManufacturers,
    clearManufacturersSearch,
    manufacturersSearchResults,
    manufacturersSearchLoading,
    handleManufacturerChange,
    resetRow,
    setRowToBeDeleted,
    setRowToBeSaved,
    setEditing,
    removeRow,
    addRow,
    updateRow,
    handleFieldChange,
    configuration,
    lifeExpectancyPart
}) {
    const selectSupplierSearchResult = (_propertyName, supplier, updatedItem) => {
        handleSupplierChange(updatedItem.id, supplier);
    };

    const selectManufacturerSearchResult = (_propertyName, manufacturer, updatedItem) => {
        handleManufacturerChange(updatedItem.id, manufacturer);
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
            searchTitle: 'Search Manufacturers'
        },
        {
            title: 'Name',
            id: 'supplierName',
            type: 'text',
            editable: false
        },
        {
            title: 'Lead Time',
            id: 'leadTime',
            type: 'number',
            editable: true
        },
        {
            title: 'Unit Price',
            id: 'unitPrice',
            type: 'number',
            editable: true
        },
        {
            title: 'Manufacturer',
            id: 'manufacturerCode',
            type: 'search',
            editable: true,
            search: searchManufacturers,
            clearSearch: clearManufacturersSearch,
            searchResults: manufacturersSearchResults,
            searchLoading: manufacturersSearchLoading,
            selectSearchResult: selectManufacturerSearchResult,
            searchTitle: 'Search Manufacturers',
            tooltip: row => row?.manufacturerDescription,
            minimumSearchTermLength: 3
        },
        {
            title: 'MOQ',
            id: 'moq',
            type: 'number',
            editable: true
        },
        {
            title: 'Part Number',
            id: 'manufacturersPartNumber',
            type: 'text',
            editable: true
        },
        {
            title: 'ROHS',
            id: 'rohsCompliant',
            type: 'dropdown',
            editable: true,
            options: ['Y', 'N']
        }
    ];

    return (
        <>
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
                    closeRowOnClickAway={false}
                    removeRowOnDelete
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    value={configuration}
                    propertyName="configuration"
                    fullWidth
                    onChange={handleFieldChange}
                    label="Configuration"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    value={lifeExpectancyPart}
                    propertyName="lifeExpectancyPart"
                    fullWidth
                    onChange={handleFieldChange}
                    label="Life Expectancy Part"
                />
            </Grid>
        </>
    );
}

PurchasingQuotesTab.propTypes = {
    handleSupplierChange: PropTypes.func.isRequired,
    handleManufacturerChange: PropTypes.func.isRequired,
    resetRow: PropTypes.func.isRequired,
    updateRow: PropTypes.func.isRequired,
    rows: PropTypes.arrayOf(PropTypes.shape({})),
    setRowToBeDeleted: PropTypes.func.isRequired,
    setRowToBeSaved: PropTypes.func.isRequired,
    setEditing: PropTypes.func.isRequired,
    removeRow: PropTypes.func.isRequired,
    addRow: PropTypes.func.isRequired,
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired,
    suppliersSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    searchManufacturers: PropTypes.func.isRequired,
    clearManufacturersSearch: PropTypes.func.isRequired,
    manufacturersSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    manufacturersSearchLoading: PropTypes.bool,
    suppliersSearchLoading: PropTypes.bool,
    handleFieldChange: PropTypes.func.isRequired,
    configuration: PropTypes.string,
    lifeExpectancyPart: PropTypes.string
};

PurchasingQuotesTab.defaultProps = {
    rows: [],
    suppliersSearchResults: [],
    suppliersSearchLoading: false,
    manufacturersSearchResults: [],
    manufacturersSearchLoading: false,
    configuration: null,
    lifeExpectancyPart: null
};

export default PurchasingQuotesTab;
