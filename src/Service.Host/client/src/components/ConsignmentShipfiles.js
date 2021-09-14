import React, { useState, useEffect } from 'react';
import { DataGrid } from '@material-ui/data-grid';
import Grid from '@material-ui/core/Grid';
import { Title, ErrorCard, Loading, InputField } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Page from '../containers/Page';

export default function ConsignmentShipfiles({
    consignmentShipfiles,
    consignmentShipfilesLoading,
    sendEmails,
    processedShipfiles,
    itemError,
    clearErrors,
    deleteShipfile,
    deleteLoading
}) {
    const [selectedRows, setSelectedRows] = useState([]);
    const [rows, setRows] = useState([]);
    const [testEmailAddress, setTestEmailAddress] = useState();

    useEffect(() => {
        setRows(
            consignmentShipfiles.map(s => ({
                ...s,
                id: s.id
            }))
        );
    }, [consignmentShipfiles]);

    useEffect(() => {
        if (processedShipfiles?.length) {
            processedShipfiles.forEach(processed =>
                setRows(r =>
                    r.map(row => (row.id === processed.id ? { ...processed, id: row.id } : row))
                )
            );
        }
    }, [processedShipfiles]);

    const columns = [
        { field: 'id', headerName: 'Id', width: 0, hide: true },
        { field: 'consignmentId', headerName: 'Consignment', width: 140 },
        { field: 'dateClosed', headerName: 'DispatchedOn', width: 200 },
        { field: 'customerName', headerName: 'Customer', width: 300 },
        { field: 'invoiceNumbers', headerName: 'Invoices', width: 300 },
        { field: 'status', headerName: 'Status', width: 200 }
    ];
    const handleSelectRow = selected => {
        setSelectedRows(rows.filter(r => selected.rowIds.includes(r.id.toString())));
    };
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Send Shipfile Emails" />
                </Grid>
                {deleteLoading && !itemError ? (
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
                                disabled={selectedRows?.length < 1}
                                onClick={() => {
                                    clearErrors();
                                    selectedRows.forEach(r => {
                                        setRows(shipfiles =>
                                            shipfiles.map(s =>
                                                s.id === r.id ? { ...r, status: 'Processing' } : s
                                            )
                                        );
                                    });
                                    sendEmails({
                                        shipfiles: selectedRows
                                    });
                                }}
                            >
                                Send Selected
                            </Button>
                        </Grid>
                        <Grid item xs={2}>
                            <Button
                                style={{ marginTop: '22px' }}
                                variant="contained"
                                color="secondary"
                                disabled={selectedRows?.length < 1}
                                onClick={() => {
                                    clearErrors();
                                    selectedRows.forEach(r => deleteShipfile(r.id, null));
                                }}
                            >
                                Delete Selected
                            </Button>
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                label="Send Test Email to Address"
                                propertyName="testEmailAddress"
                                onChange={(_, newValue) => setTestEmailAddress(newValue)}
                                value={testEmailAddress}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <Button
                                style={{ marginTop: '22px' }}
                                variant="contained"
                                disabled={!testEmailAddress}
                                onClick={() => {
                                    clearErrors();
                                    selectedRows.forEach(r => {
                                        setRows(shipfiles =>
                                            shipfiles.map(s =>
                                                s.id === r.id ? { ...r, status: 'Processing' } : s
                                            )
                                        );
                                    });
                                    sendEmails({
                                        shipfiles: selectedRows,
                                        test: true,
                                        testEmailAddress
                                    });
                                }}
                            >
                                Test Selected
                            </Button>
                        </Grid>
                        <Grid item xs={12}>
                            <div style={{ height: 500, width: '100%' }}>
                                <DataGrid
                                    rows={rows}
                                    columns={columns}
                                    density="standard"
                                    rowHeight={34}
                                    autoHeight
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
    processedShipfiles: PropTypes.arrayOf(PropTypes.shape({ id: PropTypes.number })),
    consignmentShipfilesLoading: PropTypes.bool,
    sendEmails: PropTypes.func.isRequired,
    deleteShipfile: PropTypes.func.isRequired,
    deleteLoading: PropTypes.bool,
    clearErrors: PropTypes.func.isRequired,
    itemError: PropTypes.shape({
        statusText: PropTypes.string,
        details: PropTypes.shape({ errors: PropTypes.arrayOf(PropTypes.string) })
    }),
    whatToWandReport: PropTypes.shape({})
};

ConsignmentShipfiles.defaultProps = {
    consignmentShipfiles: [],
    processedShipfiles: null,
    consignmentShipfilesLoading: false,
    itemError: null,
    whatToWandReport: null,
    deleteLoading: false
};
