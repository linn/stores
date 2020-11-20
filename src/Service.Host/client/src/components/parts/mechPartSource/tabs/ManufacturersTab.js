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
    manufacturersSearchLoading
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
            searchTitle: 'Search Parts',
            // selectSearchResult: selectPartSearchResult,
            required: true
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
            type: 'dropdown',
            editable: true,
            options: ['Y', 'N']
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
    manufacturers: PropTypes.arrayOf(PropTypes.shape({}))
};

ManufacturersTab.defaultProps = {
    manufacturers: []
};

export default ManufacturersTab;
