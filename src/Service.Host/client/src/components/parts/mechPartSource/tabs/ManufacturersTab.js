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
    employeesSearchLoading
}) {
    const [newRow, setNewRow] = useState({});
    
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
            // selectSearchResult: selectPartSearchResult,
            searchTitle: 'Search Parts'
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
            searchTitle: 'Search Employees'
            // selectSearchResult: selectPartSearchResult,
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
                rows={manufacturers}
                newRow={newRow}
                createRow={() => {}}
                saveRow={() => {}}
                updateRow={() => {}}
                validateRow={() => {}}
                deleteRow={() => {}}
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
    employeesSearchLoading: PropTypes.bool
};

ManufacturersTab.defaultProps = {
    manufacturers: [],
    manufacturersSearchResults: [],
    manufacturersSearchLoading: false,
    employeesSearchResults: [],
    employeesSearchLoading: false
};

export default ManufacturersTab;
