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
    sendEmailsLoading,
    deleteShipfile,
    deleteLoading
}) {
    const [selectedRows, setSelectedRows] = useState([227165]);
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
        { field: 'dateClosed', headerName: 'DispatchedOn', width: 100 },
        { field: 'customerName', headerName: 'Customer', width: 150 },
        { field: 'invoiceNumbers', headerName: 'Invoices', width: 400 },
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
                {(sendEmailsLoading || deleteLoading) && !itemError ? (
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
                                onClick={() => {
                                    clearErrors();
                                    selectedRows.forEach(r => deleteShipfile(r.id, null));
                                }}
                            >
                                Delete Selected
                            </Button>
                        </Grid>
                        <Grid item xs={8} />
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
                                onClick={() => {
                                    clearErrors();
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
                    </>
                )}
            </Grid>
        </Page>
    );
}

ConsignmentShipfiles.propTypes = {
    consignmentShipfiles: PropTypes.arrayOf(PropTypes.shape({})),
    processedShipfiles: PropTypes.arrayOf(PropTypes.shape({})),
    consignmentShipfilesLoading: PropTypes.bool,
    sendEmails: PropTypes.func.isRequired,
    deleteShipfile: PropTypes.func.isRequired,
    deleteLoading: PropTypes.bool,
    clearErrors: PropTypes.func.isRequired,
    itemError: PropTypes.shape({
        statusText: PropTypes.string,
        details: PropTypes.shape({ errors: PropTypes.arrayOf(PropTypes.string) })
    }),
    sendEmailsLoading: PropTypes.bool,
    whatToWandReport: PropTypes.shape({})
};

ConsignmentShipfiles.defaultProps = {
    consignmentShipfiles: [],
    processedShipfiles: [],
    consignmentShipfilesLoading: true,
    itemError: null,
    sendEmailsLoading: false,
    whatToWandReport: null,
    deleteLoading: false
};
