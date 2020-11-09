import React, { useState, useEffect } from 'react';
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
            content={dataSheets
                .map(d => ({ ...d, id: d.sequence }))
                }
            updateContent={handleDataSheetsChange}
            editStatus={'viewing'}
            allowedToEdit={true}
            allowedToCreate={true}
            allowedToDelete={true}
        />
    );
}

DataSheetsTab.propTypes = {
    dataSheets: PropTypes.arrayOf(PropTypes.shape({}))
};

DataSheetsTab.defaultProps = {
    dataSheets: []
};

export default DataSheetsTab;
