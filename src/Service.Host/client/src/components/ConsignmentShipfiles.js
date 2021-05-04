import React, { useState, useEffect } from 'react';
import { DataGrid } from '@material-ui/data-grid';
import Grid from '@material-ui/core/Grid';
import { Title, ErrorCard, Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Page from '../containers/Page';

export default function ConsignmentShipfiles({
    consignmentShipfiles,
    consignmentShipfilesLoading,
    sendShipfiles,
    itemError,
    clearErrors,
    sendShipfilesLoading,
    shipfilesSent,
    clearData
}) {
    const [selectedRows, setSelectedRows] = useState([]);
    const [rows, setRows] = useState([]);

    const compare = (row, sentShipfile) =>
        Object.keys(row).every(
            key => key === 'href' || key === 'id' || row[key] === sentShipfile[key]
        );

    useEffect(() => {
        setRows(
            consignmentShipfiles.map(s => ({
                ...s,
                id: s.consignmentId
            }))
        );
    }, [consignmentShipfiles]);

    useEffect(() => {
        if (shipfilesSent?.length) {
            shipfilesSent.forEach(transferred =>
                setRows(r =>
                    r.map(row => (compare(row, transferred) ? { ...transferred, id: row.id } : row))
                )
            );
        }
    }, [shipfilesSent]);

    const columns = [
        { field: 'consignmentId', headerName: 'From', width: 140 },
        { field: 'dateClosed', headerName: 'DispatchedOn', width: 100 },
        { field: 'customerName', headerName: 'Customer', width: 150 },
        { field: 'invoiceNumbers', headerName: 'Invoices', width: 400 },
        { field: 'status', headerName: 'Status', width: 200 }
    ];
    const handleSelectRow = selected => {
        setSelectedRows(rows.filter(r => selected.rowIds.includes(r.id)));
    };
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={10}>
                    <Title text="Send Shipfile Emails" />
                </Grid>
                {sendShipfilesLoading && !itemError ? (
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
                                    sendShipfiles({
                                        shipfiles: selectedRows
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
                                    columns={columns}
                                    density="standard"
                                    rowHeight={34}
                                    checkboxSelection
                                    onSelectionChange={handleSelectRow}
                                    loading={consignmentShipfilesLoading}
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

ConsignmentShipfiles.propTypes = {
    consignmentShipfiles: PropTypes.arrayOf(PropTypes.shape({})),
    shipfilesSent: PropTypes.arrayOf(PropTypes.shape({})),
    consignmentShipfilesLoading: PropTypes.bool,
    sendShipfiles: PropTypes.func.isRequired,
    clearErrors: PropTypes.func.isRequired,
    itemError: PropTypes.shape({
        statusText: PropTypes.string,
        details: PropTypes.shape({ errors: PropTypes.arrayOf(PropTypes.string) })
    }),
    sendShipfilesLoading: PropTypes.bool,
    whatToWandReport: PropTypes.shape({}),
    clearData: PropTypes.func.isRequired
};

ConsignmentShipfiles.defaultProps = {
    consignmentShipfiles: [],
    shipfilesSent: [],
    consignmentShipfilesLoading: true,
    itemError: null,
    sendShipfilesLoading: false,
    whatToWandReport: null
};
