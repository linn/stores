import React from 'react';
import PropTypes from 'prop-types';
import { GroupEditTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';

function ManufacturersTab({
    rows,
    searchManufacturers,
    clearManufacturersSearch,
    rowsSearchResults,
    rowsSearchLoading,
    searchEmployees,
    clearEmployeesSearch,
    employeesSearchResults,
    employeesSearchLoading,
    handleApprovedByChange,
    handleManufacturerChange,
    resetRow,
    setRowToBeDeleted,
    setRowToBeSaved,
    setEditing,
    removeRow,
    addRow,
    updateRow
}) {
    const selectApprovedBySearchResult = (_propertyName, approvedBy, updatedItem) => {
        handleApprovedByChange(updatedItem.sequence, approvedBy);
    };

    const selectManufacturerSearchResult = (_propertyName, manufacturer, updatedItem) => {
        handleManufacturerChange(updatedItem.sequence, manufacturer);
    };

    const columns = [
        {
            title: 'Preference',
            id: 'preference',
            type: 'number',
            editable: true,
            required: true
        },
        {
            title: 'Manufacturer',
            id: 'manufacturerCode',
            type: 'search',
            editable: true,
            search: searchManufacturers,
            clearSearch: clearManufacturersSearch,
            searchResults: rowsSearchResults,
            searchLoading: rowsSearchLoading,
            selectSearchResult: selectManufacturerSearchResult,
            searchTitle: 'Search Manufacturers',
            minimumSearchTermLength: 4
        },
        {
            title: 'Their Part Number',
            id: 'partNumber',
            type: 'text',
            editable: true
        },
        {
            title: 'Reel Suffix',
            id: 'reelSuffix',
            type: 'text',
            editable: true
        },
        {
            title: 'ROHS',
            id: 'rohsCompliant',
            type: 'dropdown',
            editable: true,
            options: ['Y', 'N', 'F']
        },
        {
            title: 'Approved By',
            id: 'approvedBy',
            type: 'search',
            editable: true,
            search: searchEmployees,
            clearSearch: clearEmployeesSearch,
            searchResults: employeesSearchResults,
            searchLoading: employeesSearchLoading,
            searchTitle: 'Search Employees',
            selectSearchResult: selectApprovedBySearchResult
        },
        {
            title: 'Name',
            id: 'approvedByName',
            type: 'text',
            editable: false
        },
        {
            title: 'Date Approved',
            id: 'dateApproved',
            type: 'date',
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
                closeRowOnClickAway={false}
                removeRowOnDelete
            />
        </Grid>
    );
}

ManufacturersTab.propTypes = {
    handleManufacturerChange: PropTypes.func.isRequired,
    setRowToBeDeleted: PropTypes.func.isRequired,
    setRowToBeSaved: PropTypes.func.isRequired,
    setEditing: PropTypes.func.isRequired,
    removeRow: PropTypes.func.isRequired,
    addRow: PropTypes.func.isRequired,
    updateRow: PropTypes.func.isRequired,
    rows: PropTypes.arrayOf(PropTypes.shape({})),
    searchManufacturers: PropTypes.func.isRequired,
    clearManufacturersSearch: PropTypes.func.isRequired,
    rowsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    rowsSearchLoading: PropTypes.bool,
    searchEmployees: PropTypes.func.isRequired,
    clearEmployeesSearch: PropTypes.func.isRequired,
    employeesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    employeesSearchLoading: PropTypes.bool,
    handleApprovedByChange: PropTypes.func.isRequired,
    resetRow: PropTypes.func.isRequired
};

ManufacturersTab.defaultProps = {
    rows: [],
    rowsSearchResults: [],
    rowsSearchLoading: false,
    employeesSearchResults: [],
    employeesSearchLoading: false
};

export default ManufacturersTab;
