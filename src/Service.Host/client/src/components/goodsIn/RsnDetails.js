import React, { useState, useCallback } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { DataGrid } from '@mui/x-data-grid';

function RsnDetails({ rsnAccessories, rsnConditions, onConfirm }) {
    const columns = [
        { field: 'code', headerName: 'Return', width: 150, hide: true },
        { field: 'description', headerName: '#', width: 100 },
        { field: 'extraInfo', headerName: 'Extra Info', width: 500, editable: true }
    ];
    const [accessoriesRows, setAccessoriesRows] = useState(rsnAccessories);

    const handleSelectAccessoriesRows = selected => {
        setAccessoriesRows(
            accessoriesRows.map(r => (selected.includes(r.id) ? { ...r, selected: true } : r))
        );
    };

    const handleEditAccessoriesRowsModelChange = useCallback(
        model => {
            const key = Object.keys(model)[0];
            setAccessoriesRows(
                accessoriesRows.map(r => {
                    return r.id === key ? { ...r, return: model[key].extraInfo.value } : r;
                })
            );
        },
        [accessoriesRows]
    );

    const [conditionsRows, setConditionsRows] = useState(rsnConditions);

    const handleSelectConditionsRows = selected => {
        setConditionsRows(
            conditionsRows.map(r => (selected.includes(r.id) ? { ...r, selected: true } : r))
        );
    };

    const handleEditConditionsRowsModelChange = useCallback(
        model => {
            const key = Object.keys(model)[0];
            setAccessoriesRows(
                conditionsRows.map(r => {
                    return r.id === key ? { ...r, return: model[key].extraInfo.value } : r;
                })
            );
        },
        [conditionsRows]
    );

    return (
        <Grid container spacing={3}>
            <Grid item xs={12}>
                <Typography variant="h4">RSN ACCESSORIES</Typography>
            </Grid>
            <Grid item xs={12}>
                <div style={{ height: 500, width: '100%' }}>
                    <DataGrid
                        rows={accessoriesRows}
                        columns={columns}
                        editMode="row"
                        onEditAccessoriesRowsModelChange={handleEditAccessoriesRowsModelChange}
                        columnBuffer={9}
                        density="standard"
                        rowHeight={34}
                        loading={false}
                        hideFooter
                        disableSelectionOnClick
                        onSelectionModelChange={handleSelectAccessoriesRows}
                        checkboxSelection
                        isCellEditable={params => params.row.selected}
                    />
                </div>
            </Grid>
            <Grid item xs={12}>
                <Typography variant="h4">RSN CONDITION</Typography>
            </Grid>
            <Grid item xs={12}>
                <div style={{ height: 500, width: '100%' }}>
                    <DataGrid
                        rows={conditionsRows}
                        columns={columns}
                        editMode="row"
                        onEditAccessoriesRowsModelChange={handleEditConditionsRowsModelChange}
                        columnBuffer={9}
                        density="standard"
                        rowHeight={34}
                        loading={false}
                        hideFooter
                        disableSelectionOnClick
                        onSelectionModelChange={handleSelectConditionsRows}
                        checkboxSelection
                        isCellEditable={params => params.row.selected}
                    />
                </div>
            </Grid>
            <Grid item xs={10} />
            <Grid item xs={2}>
                <Button
                    disabled={!conditionsRows.concat(accessoriesRows).some(r => r.selected)}
                    style={{ marginTop: '22px' }}
                    variant="contained"
                    onClick={() => onConfirm()}
                >
                    Confirm
                </Button>
            </Grid>
        </Grid>
    );
}

RsnDetails.propTypes = {
    rsnAccessories: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    rsnConditions: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    onConfirm: PropTypes.func.isRequired
};

export default RsnDetails;
