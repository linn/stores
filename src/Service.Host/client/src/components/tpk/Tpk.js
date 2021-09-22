import React, { useState, useEffect, useRef } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { useReactToPrint } from 'react-to-print';
import Grid from '@material-ui/core/Grid';
import { Title, ErrorCard, Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Page from '../../containers/Page';
import WhatToWandPrintOut from './WhatToWandPrintOut';

export default function Tpk({
    transferableStock,
    transferableStockLoading,
    transferStock,
    transferredStock,
    itemError,
    clearErrors,
    tpkLoading,
    whatToWandReport,
    clearData
}) {
    const [selectedRows, setSelectedRows] = useState([]);
    const [dateTimeTpkViewQueried, setDateTimeTpkViewQueried] = useState(new Date());
    const [rows, setRows] = useState([]);
    const componentRef = useRef();

    const compare = (row, transferred) =>
        Object.keys(row).every(
            key => key === 'href' || key === 'id' || row[key] === transferred[key]
        );

    useEffect(() => {
        setRows(
            transferableStock.map(s => ({
                ...s,
                id: s.articleNumber + s.orderNumber + s.orderLine + s.fromLocation
            }))
        );
        setDateTimeTpkViewQueried(new Date());
    }, [transferableStock]);

    const handlePrint = useReactToPrint({
        content: () => componentRef.current,
        onAfterPrint: () => {
            clearData();
        }
    });

    useEffect(() => {
        if (whatToWandReport) {
            handlePrint();
        }
    }, [whatToWandReport, handlePrint]);

    useEffect(() => {
        if (transferredStock?.length) {
            transferredStock.forEach(transferred =>
                setRows(r =>
                    r.map(row => (compare(row, transferred) ? { ...transferred, id: row.id } : row))
                )
            );
        }
    }, [transferredStock]);

    const columns = [
        { field: 'fromLocation', headerName: 'From', width: 140 },
        { field: 'quantity', headerName: 'Qty To Pick', width: 100 },
        { field: 'articleNumber', headerName: 'Article', width: 150 },
        { field: 'invoiceDescription', headerName: 'Invoice Desc', width: 400, hide: true },
        { field: 'consignmentId', headerName: 'Consignment', width: 100 },

        { field: 'notes', headerName: 'Notes', width: 200 },
        { field: 'addressee', headerName: 'Addressee', width: 200 },
        { field: 'orderNumber', headerName: 'Order', width: 100 },
        { field: 'orderLine', headerName: 'Line', width: 100 },
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
    const handleSelectRow = selected => {
        setSelectedRows(rows.filter(r => selected.includes(r.id)));
    };
    if (whatToWandReport) {
        return (
            <Page>
                <WhatToWandPrintOut ref={componentRef} whatToWandReport={whatToWandReport} />
                <Loading />
            </Page>
        );
    }
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={10}>
                    <Title text="TPK" />
                </Grid>
                {tpkLoading && !itemError ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        {itemError && (
                            <Grid item xs={12}>
                                <ErrorCard
                                    errorMessage={
                                        itemError?.details?.errors?.[0] || itemError.statusText
                                    }
                                />
                            </Grid>
                        )}
                        <Grid item xs={2}>
                            <Button
                                style={{ marginTop: '22px' }}
                                variant="contained"
                                onClick={() => {
                                    clearErrors();
                                    transferStock({
                                        stockToTransfer: selectedRows,
                                        dateTimeTpkViewQueried: dateTimeTpkViewQueried?.toISOString()
                                    });
                                }}
                            >
                                Transfer
                            </Button>
                        </Grid>
                        <Grid item xs={12}>
                            <div style={{ height: 500, width: '100%' }}>
                                <DataGrid
                                    rows={rows}
                                    columnBuffer={9}
                                    columns={columns}
                                    density="standard"
                                    rowHeight={34}
                                    checkboxSelection
                                    onSelectionModelChange={handleSelectRow}
                                    loading={transferableStockLoading}
                                    hideFooter
                                />
                            </div>
                        </Grid>
                    </>
                )}
            </Grid>
        </Page>
    );
}

Tpk.propTypes = {
    transferableStock: PropTypes.arrayOf(PropTypes.shape({})),
    transferredStock: PropTypes.arrayOf(PropTypes.shape({})),
    transferableStockLoading: PropTypes.bool,
    transferStock: PropTypes.func.isRequired,
    clearErrors: PropTypes.func.isRequired,
    itemError: PropTypes.shape({
        statusText: PropTypes.string,
        details: PropTypes.shape({ errors: PropTypes.arrayOf(PropTypes.string) })
    }),
    tpkLoading: PropTypes.bool,
    whatToWandReport: PropTypes.shape({}),
    clearData: PropTypes.func.isRequired
};

Tpk.defaultProps = {
    transferableStock: [],
    transferredStock: [],
    transferableStockLoading: true,
    itemError: null,
    tpkLoading: false,
    whatToWandReport: null
};
