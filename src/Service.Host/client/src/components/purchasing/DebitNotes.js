import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import makeStyles from '@material-ui/styles/makeStyles';
import { DataGrid } from '@material-ui/data-grid';
import Dialog from '@material-ui/core/Dialog';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import Typography from '@material-ui/core/Typography';
import {
    Title,
    Loading,
    SnackbarMessage,
    ErrorCard,
    InputField
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function DebitNotes({
    items,
    itemsLoading,
    updateDebitNote,
    updateDebitNoteError,
    debitNoteLoading,
    snackbarVisible,
    setSnackbarVisible
}) {
    const [rows, setRows] = useState([]);
    const [selectedRows, setSelectedRows] = useState([]);
    const [dialogOpen, setDialogOpen] = useState(false);
    const [commentsDialogOpen, setCommentsDialogOpen] = useState(false);

    const [closeReason, setCloseReason] = useState('');
    const [comments, setComments] = useState('');
    const useStyles = makeStyles(theme => ({
        dialog: {
            margin: theme.spacing(6),
            minWidth: theme.spacing(62)
        },
        total: {
            float: 'right'
        }
    }));

    const classes = useStyles();

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
        console.log(selectedRows);
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
            width: 150
        },
        {
            headerName: 'Created',
            field: 'dateCreated',
            width: 150
        },
        {
            headerName: 'Supplier',
            field: 'supplierName',
            width: 250
        },
        {
            headerName: 'Qty',
            field: 'orderQty',
            width: 100
        },
        {
            headerName: 'Order No',
            field: 'originalOrderNumber',
            width: 100
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
        <Page showBreadcrumbs={false}>
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
                                <Grid item xs={2}>
                                    <Button
                                        style={{ marginTop: '22px' }}
                                        variant="contained"
                                        color="primary"
                                        onClick={() => {
                                            selectedRows.forEach(r =>
                                                updateDebitNote(r.noteNumber, {
                                                    ...r,
                                                    close: true,
                                                    reasonClosed: closeReason
                                                })
                                            );
                                            setDialogOpen(false);
                                        }}
                                    >
                                        Confirm
                                    </Button>
                                </Grid>
                            </Grid>
                        </div>
                    </div>
                </Dialog>

                <Dialog open={commentsDialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setCommentsDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <Grid container spacing={3}>
                                <Grid item xs={12}>
                                    <Typography variant="h5" gutterBottom>
                                        Edit Comments
                                    </Typography>
                                </Grid>

                                <Grid item xs={12}>
                                    <InputField
                                        fullWidth
                                        value={comments}
                                        onChange={(_, newValue) => setComments(newValue)}
                                        label="Comments"
                                        propertyName="comments"
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <Button
                                        style={{ marginTop: '22px' }}
                                        variant="contained"
                                        color="primary"
                                        onClick={() => {
                                            updateDebitNote(selectedRows[0].noteNumber, {
                                                ...selectedRows[0],
                                                notes: comments
                                            });
                                            setSelectedRows([]);
                                            setCommentsDialogOpen(false);
                                        }}
                                    >
                                        Save
                                    </Button>
                                </Grid>
                            </Grid>
                        </div>
                    </div>
                </Dialog>

                <Grid item xs={12}>
                    <Title text="Open Debit Notes" />
                </Grid>
                {updateDebitNoteError && (
                    <Grid item xs={12}>
                        <ErrorCard
                            errorMessage={
                                updateDebitNoteError?.details?.errors?.[0] ||
                                updateDebitNoteError.statusText
                            }
                        />
                    </Grid>
                )}
                {itemsLoading || debitNoteLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
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
                                            onSelectionModelChange={handleSelectRow}
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
                                        colour="primary"
                                        variant="outlined"
                                        onClick={() => {
                                            setDialogOpen(true);
                                        }}
                                    >
                                        Close Selected
                                    </Button>
                                </Grid>
                                <Grid item xs={10} />
                                <Grid item xs={4}>
                                    <Button
                                        style={{ marginTop: '22px' }}
                                        colour="primary"
                                        variant="outlined"
                                        disabled={selectedRows.length !== 1}
                                        onClick={() => {
                                            setComments(selectedRows[0].notes);
                                            setCommentsDialogOpen(true);
                                        }}
                                    >
                                        Edit Comments of Selected
                                    </Button>
                                </Grid>
                                <Grid item xs={4} />
                                <Grid item xs={4}>
                                    <Typography
                                        className={classes.dialog}
                                        variant="h5"
                                        gutterBottom
                                    >
                                        Total Outstanding: £
                                        {rows.length > 0
                                            ? rows
                                                  .map(r => r.netTotal)
                                                  .reduce((a, b) => a + b, 0)
                                                  .toFixed(2)
                                            : ''}
                                    </Typography>
                                </Grid>
                            </>
                        )}
                    </>
                )}
            </Grid>
        </Page>
    );
}

DebitNotes.propTypes = {
    items: PropTypes.arrayOf(PropTypes.shape({})),
    itemsLoading: PropTypes.bool,
    debitNoteLoading: PropTypes.bool,
    options: PropTypes.shape({ partNumber: PropTypes.string }).isRequired,
    snackbarVisible: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    updateDebitNote: PropTypes.func.isRequired,
    updateDebitNoteError: PropTypes.shape({
        statusText: PropTypes.string,
        details: PropTypes.shape({ errors: PropTypes.arrayOf(PropTypes.string) })
    })
};

DebitNotes.defaultProps = {
    itemsLoading: false,
    items: [],
    debitNoteLoading: false,
    snackbarVisible: false,
    updateDebitNoteError: null
};

export default DebitNotes;
