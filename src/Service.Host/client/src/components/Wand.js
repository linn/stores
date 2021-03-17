import React, { useState, useRef, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import TextField from '@material-ui/core/TextField';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import { DataGrid } from '@material-ui/data-grid';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';
import MenuItem from '@material-ui/core/MenuItem';
import InputLabel from '@material-ui/core/InputLabel';
import { makeStyles } from '@material-ui/core/styles';

import {
    Title,
    Dropdown,
    Loading,
    InputField,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';

import Page from '../containers/Page';

function Wand({
    wandConsignments,
    loadingWandConsignments,
    getItems,
    items,
    itemsLoading,
    clearItems,
    userNumber,
    doWandItemWorking,
    doWandItem,
    wandResult,
    unallocateConsignment,
    unallocateConsignmentLine,
    unallocateConsignmentResult,
    unallocateConsignmentLineResult,
    clearUnallocateConsignment,
    clearUnallocateConsignmentLine,
    unallocateConsignmentWorking,
    unallocateConsignmentLineWorking
}) {
    const [consignmentId, setConsignmentId] = useState('');
    const [wandAction, setWandAction] = useState('W');
    const [wandString, setWandString] = useState(null);
    const [showAlert, setShowAlert] = useState(false);
    const [resultStyle, setResultStyle] = useState('noMessage');
    const [wandMessage, setWandMessage] = useState('');
    const [selectedRow, setSelectedRow] = useState(null);
    const [lastWandLogId, setLastWandLogId] = useState(null);

    const wandStringInput = useRef(null);

    useEffect(() => {
        setWandString(null);
    }, [items]);

    useEffect(() => {
        if (!wandString) {
            setResultStyle('noMessage');
            setWandMessage('');
        }
    }, [wandString]);

    useEffect(() => {
        if (!wandResult || !wandResult.message) {
            setResultStyle('noMessage');
            setWandMessage('');
        } else if (wandResult.success) {
            setResultStyle('ok');
            setWandMessage('Success');
        } else {
            setResultStyle('notOk');
            setWandMessage(wandResult.message);
            setShowAlert(true);
        }
    }, [wandResult]);

    useEffect(() => {
        if (
            wandResult &&
            wandResult.wandLog &&
            wandResult.wandLog.id &&
            lastWandLogId !== wandResult.wandLog.id
        ) {
            // data that will be needed to print address label
            // const consignmentDetails = wandConsignments.find(
            //     a => a.consignmentId === consignmentId
            // );
            // const wandedItem = items.find(
            //     a =>
            //         a.orderNumber === wandResult.wandLog.orderNumber &&
            //         a.orderLine === wandResult.wandLog.orderLine
            // );

            // we now have enough information to optionally print a label here
            setLastWandLogId(wandResult.wandLog.id);
        }
    }, [wandResult, consignmentId, wandConsignments, lastWandLogId, items]);

    const useStyles = makeStyles(theme => ({
        ok: {
            color: 'black',
            backgroundColor: 'lightGreen'
        },
        notOk: {
            color: 'black',
            backgroundColor: 'red'
        },
        noMessage: {
            color: 'black',
            backgroundColor: 'white'
        },
        root: {
            paddingTop: 0,
            marginTop: theme.spacing(1)
        },
        label: {
            fontSize: theme.typography.fontSize
        },
        labelAsterisk: {
            color: theme.palette.error.main
        }
    }));

    const classes = useStyles();

    const handleConsignmentChange = newValue => {
        setConsignmentId(newValue.target.value ? parseInt(newValue.target.value, 10) : null);
        if (newValue.target.value) {
            getItems(newValue.target.value);
        } else {
            clearItems();
        }

        setResultStyle('noMessage');
        setWandMessage('');
        wandStringInput.current.focus();
    };

    const handleSelectRow = row => {
        setSelectedRow(items[row.rowIds[0]]);
    };

    const handleArticleNumberDoubleClick = wandStringSuggestion => {
        setWandString(wandStringSuggestion);
        wandStringInput.current.focus();
    };

    const handleUnallocateConsignment = () => {
        if (items && items.length > 0) {
            unallocateConsignment({ requisitionNumber: items[0].requisitionNumber, userNumber });
        }

        clearItems();
        setConsignmentId('');
    };

    const handleUnallocateLine = () => {
        if (selectedRow) {
            unallocateConsignmentLine({
                requisitionNumber: selectedRow.requisitionNumber,
                requisitionLine: selectedRow.requisitionLine,
                userNumber
            });
        }
    };

    const handlewandActionChange = (_propertyName, newValue) => {
        setWandAction(newValue);
        wandStringInput.current.focus();
    };

    const handleOnWandChange = (_propertyName, newValue) => {
        setWandString(newValue);
    };

    const handleWand = () => {
        if (wandString && consignmentId) {
            doWandItem({ consignmentId, userNumber, wandAction, wandString });
            setWandString(null);
            wandStringInput.current.focus();
        }
    };

    const handleOnKeyPress = data => {
        if (data.keyCode === 13 || data.keyCode === 9) {
            handleWand();
        }
    };

    const getDetailRows = details => {
        if (!details) {
            return [];
        }

        return details.map((d, i) => ({ id: i, ...d }));
    };

    const getBoxValue = (params, box) => {
        if (params.row.boxesPerProduct >= box) {
            return box;
        }

        return '';
    };

    const getBoxClass = (params, box) => {
        if (params.row.boxesWanded && params.row.boxesWanded.includes(box)) {
            return classes.ok;
        }

        return classes.noMessage;
    };

    const columns = [
        {
            field: 'partNumber',
            headerName: 'Article No',
            width: 140,
            renderCell: params => (
                <div
                    onDoubleClick={() =>
                        handleArticleNumberDoubleClick(params.row.wandStringSuggestion)
                    }
                >
                    {params.row.partNumber}
                </div>
            )
        },
        { field: 'quantity', headerName: 'Qty', width: 100 },
        {
            field: 'quantityScanned',
            headerName: 'Scanned',
            width: 120,
            cellClassName: params => {
                return params.row.allWanded ? classes.ok : classes.noMessage;
            }
        },
        { field: 'partDescription', headerName: 'Description', width: 230 },
        { field: 'orderNumber', headerName: 'Order', width: 100 },
        { field: 'orderLine', headerName: 'Line', width: 80 },
        {
            field: 'boxesPerProduct',
            description: 'Boxes Per Product',
            headerName: 'Boxes',
            width: 100
        },
        {
            field: 'box1',
            headerName: '1',
            description: 'Box 1',
            width: 60,
            valueGetter: params => getBoxValue(params, 1),
            cellClassName: params => getBoxClass(params, 1)
        },
        {
            field: 'box2',
            headerName: '2',
            description: 'Box 2',
            width: 60,
            valueGetter: params => getBoxValue(params, 2),
            cellClassName: params => getBoxClass(params, 2)
        },
        {
            field: 'box3',
            description: 'Box 3',
            headerName: '3',
            width: 60,
            valueGetter: params => getBoxValue(params, 3),
            cellClassName: params => getBoxClass(params, 3)
        },
        {
            field: 'box4',
            headerName: '4',
            description: 'Box 4',
            width: 60,
            valueGetter: params => getBoxValue(params, 4),
            cellClassName: params => getBoxClass(params, 4)
        },
        { field: 'linnBarCode', headerName: 'Bar Code', width: 120, hide: true },
        { field: 'requisitionNumber', headerName: 'Req No', width: 100, hide: true },
        { field: 'requisitionLine', headerName: 'Req Line', width: 110, hide: true }
    ];
    const focusProp = { inputRef: wandStringInput, onKeyDown: handleOnKeyPress };

    const getResultClass = style => {
        switch (style) {
            case 'ok':
                return classes.ok;
            case 'notOk':
                return classes.notOk;
            default:
                return classes.noMessage;
        }
    };

    const showUnallocateConsignmentError = () =>
        unallocateConsignmentResult &&
        !unallocateConsignmentResult.success &&
        unallocateConsignmentResult.message;

    const closeUnallocateConsignment = () => {
        clearUnallocateConsignment({});
    };

    const showUnallocateConsignmentLineError = () =>
        unallocateConsignmentLineResult &&
        !unallocateConsignmentLineResult.success &&
        unallocateConsignmentLineResult.message;

    const closeUnallocateConsignmentLine = () => {
        clearUnallocateConsignmentLine({});
    };

    return (
        <Page>
            <Dialog
                open={showAlert}
                onClose={() => setShowAlert(false)}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">Wand Error</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {wandMessage}
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button
                        onClick={() => setShowAlert(false)}
                        variant="contained"
                        color="secondary"
                        autoFocus
                    >
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Wand" />
                </Grid>
                <Grid item xs={12}>
                    <SnackbarMessage
                        visible={showUnallocateConsignmentError()}
                        onClose={closeUnallocateConsignment}
                        message={unallocateConsignmentResult?.message}
                    />
                    <SnackbarMessage
                        visible={showUnallocateConsignmentLineError()}
                        onClose={closeUnallocateConsignmentLine}
                        message={unallocateConsignmentLineResult?.message}
                    />
                </Grid>
                {/* {unallocateConsignmentLineResult && !unallocateConsignmentLineResult.success && (
                    <Grid item xs={12}>
                        <SnackbarMessage message={unallocateConsignmentLineResult.message} />
                    </Grid>
                )} */}
            </Grid>
            {loadingWandConsignments || unallocateConsignmentWorking ? (
                <Loading />
            ) : (
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <InputLabel
                            classes={{ root: classes.label, asterisk: classes.labelAsterisk }}
                        >
                            Consignment
                        </InputLabel>
                        <TextField
                            select
                            classes={{
                                root: classes.root
                            }}
                            margin="dense"
                            style={{ width: '800px' }}
                            value={consignmentId}
                            onChange={handleConsignmentChange}
                            variant="outlined"
                        >
                            {wandConsignments.map(option => (
                                <MenuItem key={option.consignmentId} value={option.consignmentId}>
                                    <Grid container spacing={3}>
                                        <Grid item xs={2}>
                                            {option.consignmentId}
                                        </Grid>
                                        <Grid item xs={6}>
                                            {option.addressee}
                                        </Grid>
                                        <Grid item xs={2}>
                                            {option.isDone ? option.isDone : '-'}
                                        </Grid>
                                        <Grid item xs={2}>
                                            {option.countryCode}
                                        </Grid>
                                    </Grid>
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>
                    <Grid item xs={2}>
                        <Dropdown
                            label="Action"
                            propertyName="wandAction"
                            items={[
                                { id: 'W', displayText: 'Wand' },
                                { id: 'U', displayText: 'Unwand' }
                            ]}
                            value={wandAction}
                            onChange={handlewandActionChange}
                        />
                    </Grid>
                    <Grid item xs={8}>
                        <InputField
                            fullWidth
                            autoFocus
                            value={wandString}
                            label="Wand String"
                            onChange={handleOnWandChange}
                            textFieldProps={focusProp}
                        />
                    </Grid>
                    <Grid item xs={2}>
                        <Button
                            style={{ marginTop: '22px' }}
                            className="hide-when-printing"
                            variant="contained"
                            onClick={handleWand}
                        >
                            {wandAction === 'W' ? 'Wand' : 'Unwand'}
                        </Button>
                    </Grid>
                    <Grid item xs={12}>
                        {doWandItemWorking || unallocateConsignmentLineWorking ? (
                            <Loading />
                        ) : (
                            <TextField
                                style={{ padding: 10 }}
                                className={getResultClass(resultStyle)}
                                id="wand-status"
                                fullWidth
                                value={wandMessage}
                                InputProps={{
                                    readOnly: true,
                                    disableUnderline: true
                                }}
                            />
                        )}
                    </Grid>
                    <Grid item xs={12}>
                        <div style={{ height: 500, width: '100%' }}>
                            <DataGrid
                                rows={getDetailRows(items)}
                                columns={columns}
                                density="compact"
                                rowHeight={34}
                                loading={itemsLoading}
                                hideFooter
                                onSelectionChange={handleSelectRow}
                            />
                        </div>
                    </Grid>
                    <Grid item xs={12}>
                        <Button
                            style={{ marginTop: '22px' }}
                            className="hide-when-printing"
                            variant="contained"
                            onClick={handleUnallocateConsignment}
                        >
                            Unallocate Consignment
                        </Button>
                        <Button
                            style={{ marginTop: '22px' }}
                            className="hide-when-printing"
                            variant="contained"
                            onClick={handleUnallocateLine}
                        >
                            Unallocate Line
                        </Button>
                    </Grid>
                </Grid>
            )}
        </Page>
    );
}

Wand.propTypes = {
    wandConsignments: PropTypes.arrayOf(PropTypes.shape({})),
    loadingWandConsignments: PropTypes.bool,
    getItems: PropTypes.func.isRequired,
    clearItems: PropTypes.func.isRequired,
    items: PropTypes.arrayOf(
        PropTypes.shape({ requisitionNumber: PropTypes.number, requisitionLine: PropTypes.number })
    ),
    itemsLoading: PropTypes.bool,
    userNumber: PropTypes.number.isRequired,
    doWandItemWorking: PropTypes.bool,
    doWandItem: PropTypes.func.isRequired,
    wandResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string,
        wandLog: PropTypes.shape({
            id: PropTypes.number,
            orderNumber: PropTypes.number,
            orderLine: PropTypes.number
        })
    }),
    unallocateConsignmentResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    unallocateConsignmentLineResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    unallocateConsignment: PropTypes.func.isRequired,
    unallocateConsignmentLine: PropTypes.func.isRequired,
    clearUnallocateConsignment: PropTypes.func.isRequired,
    clearUnallocateConsignmentLine: PropTypes.func.isRequired,
    unallocateConsignmentWorking: PropTypes.bool,
    unallocateConsignmentLineWorking: PropTypes.bool
};

Wand.defaultProps = {
    wandConsignments: [],
    loadingWandConsignments: false,
    items: [],
    itemsLoading: false,
    doWandItemWorking: false,
    wandResult: {
        message: null,
        success: true
    },
    unallocateConsignmentResult: {
        message: 'ok',
        success: true
    },
    unallocateConsignmentLineResult: {
        message: 'ok',
        success: true
    },
    unallocateConsignmentWorking: false,
    unallocateConsignmentLineWorking: false
};

export default Wand;
