import React from 'react';
import Grid from '@material-ui/core/Grid';
import { GroupEditTable } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';

function ItemsTab({
    palletData,
    addPallet,
    updatePallet,
    resetRow,
    removePallet,
    setPalletsEditing,
    setPalletRowToBeDeleted,
    setPalletRowToBeSaved,
    viewing
}) {
    const checkRow = row => {
        if (palletData.filter(pallet => pallet.palletNumber === row.palletNumber).length > 1) {
            return false;
        }

        return true;
    };

    const palletColumns = [
        {
            title: 'Pallet No',
            id: 'palletNumber',
            type: 'number',
            editable: true,
            required: true
        },
        {
            title: 'Weight',
            id: 'weight',
            type: 'number',
            editable: true,
            required: true
        },
        {
            title: 'Height',
            id: 'height',
            type: 'number',
            editable: true,
            required: true
        },
        {
            title: 'Width',
            id: 'width',
            type: 'number',
            editable: true,
            required: true
        },
        {
            title: 'Depth',
            id: 'depth',
            type: 'number',
            editable: true,
            required: true
        }
    ];

    return (
        <>
            <Grid container spacing={3} style={{ paddingTop: '30px' }}>
                <Grid item xs={1}>
                    <Typography variant="subtitle2">Pallets</Typography>
                </Grid>
                <Grid item xs={8}>
                    {palletData && (
                        <GroupEditTable
                            columns={palletColumns}
                            rows={palletData}
                            updateRow={updatePallet}
                            addRow={addPallet}
                            removeRow={removePallet}
                            resetRow={resetRow}
                            handleEditClick={setPalletsEditing}
                            editable={!viewing}
                            allowNewRowCreation
                            deleteRowPreEdit={false}
                            setRowToBeSaved={setPalletRowToBeSaved}
                            setRowToBeDeleted={setPalletRowToBeDeleted}
                            closeRowOnClickAway={false}
                            removeRowOnDelete
                            validateRow={checkRow}
                        />
                    )}
                </Grid>
                <Grid item xs={3} />
            </Grid>
        </>
    );
}

ItemsTab.propTypes = {
    palletData: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    addPallet: PropTypes.func.isRequired,
    updatePallet: PropTypes.func.isRequired,
    resetRow: PropTypes.func.isRequired,
    removePallet: PropTypes.func.isRequired,
    setPalletsEditing: PropTypes.func.isRequired,
    setPalletRowToBeDeleted: PropTypes.func.isRequired,
    setPalletRowToBeSaved: PropTypes.func.isRequired,
    viewing: PropTypes.bool.isRequired
};

export default ItemsTab;
