import React, { useEffect, useState } from 'react';
import { DataGrid } from '@material-ui/data-grid';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import makeStyles from '@material-ui/styles/makeStyles';
import Dialog from '@material-ui/core/Dialog';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import Typography from '@material-ui/core/Typography';
import {
    Title,
    Loading,
    SnackbarMessage,
    ErrorCard,
    BackButton,
    InputField
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function DebitNoteUtility({
    items,
    itemsLoading,
    itemsError,
    updateDebitNote,
    updateDebitNoteError,
    debitNoteLoading,
    snackbarVisible,
    setSnackbarVisible
}) {
    //const [prevDebitNotes, setPrevDebitNotes] = useState(null);
    const [rows, setRows] = useState([]);
    const [selectedRows, setSelectedRows] = useState([]);
    const [dialogOpen, setDialogOpen] = useState(false);
    const [closeReason, setCloseReason] = useState('');
    const useStyles = makeStyles(theme => ({
        dialog: {
            margin: theme.spacing(6),
            minWidth: theme.spacing(62)
        }
    }));

    const classes = useStyles();

    // useEffect(() => {
    //     if (items !== prevDebitNotes) {
    //         if (items) {
    //             setPrevDebitNotes(items);
    //             setDebitNotes(items);
    //         }
    //     }
    // }, [items, debitNotes, prevDebitNotes, options]);

    useEffect(() => {
        setRows(
            items.map(s => ({
                ...s,
                id: s.noteNumber
            }))
        );
    }, [items]);

    const handleSelectRow = selected => {
        setSelectedRows(rows.filter(r => selected.rowIds.includes(r.id.toString())));
    };

    const columns = [
        {
            headerName: '#',
            field: 'noteNumber',
            width: 100
        },
        {
            headerName: 'Part',
            field: 'partNumber',
            width: 200
        },
        {
            headerName: 'Supplier',
            field: 'supplierName',
            width: 200
        },
        {
            headerName: 'Qty',
            field: 'orderQty',
            width: 200
        },
        {
            headerName: 'Order No',
            field: 'originalOrderNumber',
            width: 200
        },
        {
            headerName: 'Returns Order',
            field: 'returnsOrderNumber',
            width: 200
        },
        {
            headerName: 'Net Total',
            field: 'netTotal',
            width: 200
        },
        {
            headerName: 'Comments',
            field: 'notes',
            width: 400
        }
    ];
    return (
        <Page>
            <SnackbarMessage
                visible={snackbarVisible}
                onClose={() => setSnackbarVisible(false)}
                message="Save Successful"
            />
            <Grid container spacing={3}>
                <Dialog open={dialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <Grid container spacing={3}>
                                <Grid item xs={12}>
                                    <Typography variant="h5" gutterBottom>
                                        Mark Selected as Closed
                                    </Typography>
                                </Grid>

                                <Grid item xs={12}>
                                    <InputField
                                        fullWidth
                                        value={closeReason}
                                        onChange={(_, newValue) => setCloseReason(newValue)}
                                        label="Reason? (optional)"
                                        propertyName="closeReason"
                                    />
                                </Grid>
                            </Grid>
                        </div>
                    </div>
                </Dialog>
                <Grid item xs={12}>
                    <Title text="Open Debit Notes" />
                </Grid>
                {/* {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard
                            errorMessage={itemError?.details?.errors?.[0] || itemError.statusText}
                        />
                    </Grid>
                )} */}
                {itemsLoading || debitNoteLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <Grid item xs={12}>
                        {rows && (
                            <>
                                <Grid item xs={12}>
                                    <div style={{ height: 500, width: '100%' }}>
                                        <DataGrid
                                            rows={rows}
                                            columns={columns}
                                            density="standard"
                                            rowHeight={34}
                                            checkboxSelection
                                            onSelectionChange={handleSelectRow}
                                            loading={itemsLoading}
                                            hideFooter
                                            filterModel={{
                                                items: [
                                                    {
                                                        columnField: 'supplierName',
                                                        operatorValue: 'contains',
                                                        value: ''
                                                    }
                                                ]
                                            }}
                                        />
                                    </div>
                                </Grid>
                                <Grid item xs={2}>
                                    <Button
                                        style={{ marginTop: '22px' }}
                                        variant="contained"
                                        color="secondary"
                                        onClick={() => {
                                            setDialogOpen(true);
                                            //clearErrors();
                                            //selectedRows.forEach(r => deleteShipfile(r.id, null));
                                        }}
                                    >
                                        Close Selected
                                    </Button>
                                </Grid>
                                <Grid item xs={10} />
                            </>
                        )}
                    </Grid>
                )}
                {/* <Grid item xs={12}>
                    <BackButton backClick={() => history.push('/inventory/dept-stock-parts')} />
                </Grid> */}
            </Grid>
        </Page>
    );
}

DebitNoteUtility.propTypes = {
    items: PropTypes.arrayOf(PropTypes.shape({})),
    itemsLoading: PropTypes.bool,
    debitNoteLoading: PropTypes.bool,
    options: PropTypes.shape({ partNumber: PropTypes.string }).isRequired,
    snackbarVisible: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        item: PropTypes.string,
        details: PropTypes.shape({
            errors: PropTypes.arrayOf(PropTypes.shape({}))
        })
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired
};

DebitNoteUtility.defaultProps = {
    itemsLoading: false,
    items: [],
    debitNoteLoading: false,
    snackbarVisible: false,
    itemError: null
};

export default DebitNoteUtility;
