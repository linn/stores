import React, { useState } from 'react';
import { DataGrid } from '@material-ui/data-grid';
import Grid from '@material-ui/core/Grid';
import { Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Page from '../containers/Page';

export default function Tpk({ transferableStock, transferableStockLoading, transferStock }) {
    const [selectedRows, setSelectedRows] = useState([]);

    const columns = [
        { field: 'fromLocation', headerName: 'From', width: 140 },
        { field: 'quantity', headerName: 'Qty To Pick', width: 100 },
        { field: 'articleNumber', headerName: 'Article', width: 150 },
        { field: 'invoiceDescription', headerName: 'Invoice Desc', width: 400, hide: true },
        { field: 'consignmentId', headerName: 'Consignment', width: 100 },

        { field: 'notes', headerName: 'Notes', width: 200 },
        { field: 'addressee', headerName: 'Addressee', width: 200 },
        { field: 'orderNumber', headerName: 'Order', width: 80 },
        { field: 'orderLine', headerName: 'Line', width: 40 },
        { field: 'despatchLocationCode', headerName: 'Despatch Location' },
        { field: 'reqNumber', headerName: 'Req No', width: 100, hide: true },
        { field: 'reqLine', headerName: 'Req Line', width: 110, hide: true },
        { field: 'vaxPallet', headerName: 'VaxPallet', width: 110, hide: true },
        {
            field: 'storagePlaceDescription',
            headerName: 'Storage Place Desc',
            width: 200,
            hide: true
        },
        { field: 'locationId', headerName: 'LocationID', hide: true },
        { field: 'locationCode', headerName: 'Location', width: 140, hide: true }
    ];
    const rows = transferableStock.map(s => ({
        ...s,
        id: s.articleNumber + s.orderNumber + s.orderLine + s.fromLocation // is this guaranteed unique?
    }));
    const handleSelectRow = selected => {
        setSelectedRows(rows.filter(r => selected.rowIds.includes(r.id)));
    };
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={10}>
                    <Title text="TPK" />
                </Grid>
                <Grid item xs={2}>
                    <Button
                        style={{ marginTop: '22px' }}
                        variant="contained"
                        onClick={() => transferStock(selectedRows)}
                    >
                        Transfer Selected Stock
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    <div style={{ height: 500, width: '100%' }}>
                        <DataGrid
                            rows={rows}
                            columns={columns}
                            density="standard"
                            rowHeight={34}
                            checkboxSelection
                            onSelectionChange={handleSelectRow}
                            loading={transferableStockLoading}
                            hideFooter
                        />
                    </div>
                </Grid>
            </Grid>
        </Page>
    );
}

Tpk.propTypes = {
    transferableStock: PropTypes.arrayOf(PropTypes.shape({})),
    transferableStockLoading: PropTypes.bool,
    transferStock: PropTypes.func.isRequired
};

Tpk.defaultProps = {
    transferableStock: [],
    transferableStockLoading: true
};
