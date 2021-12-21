import React, { useState, useCallback } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { Dropdown } from '@linn-it/linn-form-components-library';
import { DataGrid } from '@mui/x-data-grid';

function LoanDetails({ loanDetails, onConfirm }) {
    const columns = [
        { field: 'return', headerName: 'Return', width: 150, editable: true },
        { field: 'line', headerName: '#', width: 100 },
        { field: 'articleNumber', headerName: 'Article', width: 250 },
        { field: 'qtyOnLoan', headerName: 'Qty', width: 100 },
        { field: 'serialNumber', headerName: 'Serial', width: 150 },
        { field: 'serialNumber2', headerName: 'Serial 2', width: 150 },
        { field: 'itemNumber', headerName: 'Item', width: 100 },
        { field: 'selected', headerName: 'Selected', width: 100, hide: true }
    ];
    const [rows, setRows] = useState(loanDetails);

    const [state, setState] = useState('STORES');

    const handleSelectRow = selected => {
        setRows(rows.map(r => (selected.includes(r.id) ? { ...r, selected: true } : r)));
    };

    const handleEditRowsModelChange = useCallback(
        model => {
            const key = Object.keys(model)[0];
            setRows(
                rows.map(r => {
                    return r.id === Number(key) ? { ...r, return: model[key].return.value } : r;
                })
            );
        },
        [rows]
    );

    return (
        <Grid container spacing={3}>
            <Grid item xs={12}>
                <Typography variant="h4">Select Details</Typography>
            </Grid>
            <Grid item xs={12}>
                <div style={{ height: 500, width: '100%' }}>
                    <DataGrid
                        rows={rows}
                        columns={columns}
                        editMode="row"
                        onEditRowsModelChange={handleEditRowsModelChange}
                        columnBuffer={9}
                        density="standard"
                        rowHeight={34}
                        loading={false}
                        hideFooter
                        disableSelectionOnClick
                        onSelectionModelChange={handleSelectRow}
                        checkboxSelection
                        isCellEditable={params => params.row.selected}
                    />
                </div>
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={2}>
                <Dropdown
                    label="STATE"
                    items={['STORES', 'FAIL']}
                    value={state}
                    onChange={(_, newValue) => setState(newValue)}
                />
            </Grid>
            <Grid item xs={2}>
                <Button
                    disabled={!rows.some(r => r.selected)}
                    style={{ marginTop: '22px' }}
                    variant="contained"
                    onClick={() =>
                        onConfirm(rows.filter(r => r.selected).map(r => ({ ...r, state })))
                    }
                >
                    Confirm
                </Button>
            </Grid>
        </Grid>
    );
}

LoanDetails.propTypes = {
    loanDetails: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    onConfirm: PropTypes.func.isRequired
};

export default LoanDetails;
