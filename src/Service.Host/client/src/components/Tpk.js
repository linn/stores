import React from 'react';
import { DataGrid } from '@material-ui/data-grid';
import Grid from '@material-ui/core/Grid';
import { Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../containers/Page';

export default function Tpk({ transferableStock, transferableStockLoading }) {
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
    const handleSelectRow = row => {
        console.log(row);
    };
    return (
        <Page>
            <Grid item xs={10}>
                <Title text="TPK" />
            </Grid>
            <Grid item xs={12}>
                <div style={{ height: 500, width: '100%' }}>
                    <DataGrid
                        rows={transferableStock.map(s => ({
                            ...s,
                            id: s.articleNumber + s.orderNumber + s.orderLine + s.fromLocation // is this guaranteed unique?
                        }))}
                        columns={columns}
                        density="standard"
                        rowHeight={34}
                        onRowSelected={handleSelectRow}
                        loading={transferableStockLoading}
                        hideFooter
                    />
                </div>
            </Grid>{' '}
        </Page>
    );
}

Tpk.propTypes = {
    transferableStock: PropTypes.arrayOf(PropTypes.shape({})),
    transferableStockLoading: PropTypes.bool
};

Tpk.defaultProps = {
    transferableStock: [],
    transferableStockLoading: true
};
