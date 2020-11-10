import React from 'react';
import PropTypes from 'prop-types';
import { TableWithInlineEditing } from '@linn-it/linn-form-components-library';

function DataSheetsTab({ dataSheets, handleDataSheetsChange }) {
    const columnsInfo = [
        {
            title: 'Path',
            key: 'pdfFilePath',
            type: 'text'
        }
    ];
    return (
        <TableWithInlineEditing
            columnsInfo={columnsInfo}
            content={dataSheets.map(d => ({ ...d, id: d.sequence }))}
            updateContent={handleDataSheetsChange}
            editStatus="viewing"
            allowedToEdit
            allowedToCreate
            allowedToDelete
        />
    );
}

DataSheetsTab.propTypes = {
    dataSheets: PropTypes.arrayOf(PropTypes.shape({})),
    handleDataSheetsChange: PropTypes.func.isRequired
};

DataSheetsTab.defaultProps = {
    dataSheets: []
};

export default DataSheetsTab;
