import React from 'react';
import PropTypes from 'prop-types';
import { GroupEditTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';

function UsagesTab({
    rows,
    resetRow,
    setRowToBeDeleted,
    setRowToBeSaved,
    setEditing,
    removeRow,
    addRow,
    updateRow
}) {
    const columns = [
        {
            title: 'Product',
            id: 'rootProductName',
            type: 'text',
            editable: true
        },
        {
            title: 'Quantity Per Assembly',
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
    rows: PropTypes.arrayOf(PropTypes.shape({})),
    resetRow: PropTypes.func.isRequired,
    updateRow: PropTypes.func.isRequired,
    setRowToBeDeleted: PropTypes.func.isRequired,
    setRowToBeSaved: PropTypes.func.isRequired,
    setEditing: PropTypes.func.isRequired,
    removeRow: PropTypes.func.isRequired,
    addRow: PropTypes.func.isRequired
};

UsagesTab.defaultProps = {
    rows: []
};

export default UsagesTab;
