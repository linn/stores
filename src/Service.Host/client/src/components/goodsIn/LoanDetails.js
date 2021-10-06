import React, { useState, useCallback, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { DataGrid } from '@mui/x-data-grid';

function LoanDetails({ loanDetails, onConfirm }) {
    const columns = [
        { field: 'return', headerName: 'Return', width: 150, editable: true },
        { field: 'line', headerName: '#', width: 100 },
        { field: 'articleNumber', headerName: 'Article', width: 250 },
        { field: 'qtyOnLoan', headerName: 'Qty', width: 100 },
        { field: 'serialNumber', headerName: 'Serial', width: 150 },
        { field: 'serialNumber2', headerName: 'Serial 2', width: 150 },
        { field: 'itemNumber', headerName: 'Item', width: 100 }
    ];
    const [rows, setRows] = useState(loanDetails);
    const [selectedRows, setSelectedRows] = useState([]);

    const handleSelectRow = selected => {
        setSelectedRows(rows.filter(r => selected.includes(r.id)));
    };

    // rows is what the table displays, but selectedRows is ultimately what gets passed up to the parent onConfirm(...)
    // so update selectedRows when the user edits a cell with this callBack
    const handleEditRowsModelChange = useCallback(
        model => {
            const key = Object.keys(model)[0];
            setSelectedRows(
                selectedRows.map(r => {
                    return r.id === Number(key) ? { ...r, return: model[key].return.value } : r;
                })
            );
        },
        [selectedRows]
    );

    // and update the corresponding row when selectedRows changes with this effect, so that change is visible in the table
    // as well as being 'saved' in the selectedRows array
    useEffect(() => {
        setRows(r =>
            r.map(row => {
                const match = selectedRows.find(s => s.id === row.id);
                return match || row;
            })
        );
    }, [selectedRows]);

    return (
        <Grid container spacing={3}>
            <Grid item xs={12}>
                <Typography variant="h4">Select Details</Typography>
            </Grid>
            <Grid item xs={12}>
                <div style={{ height: 500, width: '100%' }}>
                    <DataGrid
                        rows={loanDetails}
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
                        isCellEditable={params => selectedRows.some(x => params.row.id === x.id)} // only selected rows are editable
                    />
                </div>
            </Grid>
            <Grid item xs={10} />
            <Grid item xs={2}>
                <Button
                    disabled={!selectedRows.length}
                    style={{ marginTop: '22px' }}
                    variant="contained"
                    onClick={() => onConfirm(selectedRows)}
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
