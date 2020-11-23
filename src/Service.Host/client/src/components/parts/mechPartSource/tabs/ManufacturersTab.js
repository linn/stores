import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { EditableTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';

function ManufacturersTab({
    manufacturers,
    handleFieldChange,
    searchManufacturers,
    clearManufacturersSearch,
    manufacturersSearchResults,
    manufacturersSearchLoading,
    searchEmployees,
    clearEmployeesSearch,
    employeesSearchResults,
    employeesSearchLoading,
    handleApprovedByChange,
    handleManufacturerChange,
    saveRow
}) {
    const [newRow, setNewRow] = useState({});

    const selectApprovedBySearchResult = (_propertyName, approvedBy, updatedItem) => {
        handleApprovedByChange(updatedItem.sequence, approvedBy);
        setNewRow(() => ({
            ...updatedItem,
            approvedBy: approvedBy.name,
            approvedByName: approvedBy.description
        }));
    };

    const selectManufacturerSearchResult = (_propertyName, manufacturer, updatedItem) => {
        handleManufacturerChange(updatedItem.sequence, manufacturer);
        setNewRow(() => ({
            ...updatedItem,
            manufacturerCode: manufacturer.name
        }));
    };

    const updateRow = (item, setItem, propertyName, newValue) => {
        setItem({ ...item, [propertyName]: newValue });
    };
    const columns = [
        {
            title: 'Preferece',
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
            searchResults: manufacturersSearchResults,
            searchLoading: manufacturersSearchLoading,
            selectSearchResult: selectManufacturerSearchResult,
            searchTitle: 'Search Manufacturers'
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
            options: ['Y', 'N']
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
            <EditableTable
                columns={columns}
                rows={manufacturers.map(m => ({ ...m, id: m.sequence }))}
                newRow={newRow}
                createRow={saveRow}
                saveRow={saveRow}
                //updateRow={saveRow}
                //validateRow={() => {}}
                deleteRow={() => true}
            />
        </Grid>
    );
}

ManufacturersTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    manufacturers: PropTypes.arrayOf(PropTypes.shape({})),
    searchManufacturers: PropTypes.func.isRequired,
    clearManufacturersSearch: PropTypes.func.isRequired,
    manufacturersSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    manufacturersSearchLoading: PropTypes.bool,
    searchEmployees: PropTypes.func.isRequired,
    clearEmployeesSearch: PropTypes.func.isRequired,
    employeesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    employeesSearchLoading: PropTypes.bool,
    handleApprovedByChange: PropTypes.func.isRequired
};

ManufacturersTab.defaultProps = {
    manufacturers: [],
    manufacturersSearchResults: [],
    manufacturersSearchLoading: false,
    employeesSearchResults: [],
    employeesSearchLoading: false
};

export default ManufacturersTab;
