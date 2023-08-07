import React, { useState, useRef, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import TextField from '@material-ui/core/TextField';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import { DataGrid } from '@mui/x-data-grid';
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
    SnackbarMessage,
    LinkButton
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
    const [printLabels, setPrintLabels] = useState('Y');
    const wandStringInput = useRef(null);
    const manualSelectInput = useRef(null);

    useEffect(() => {
        setWandString(null);
        if (wandStringInput.current) {
            wandStringInput.current.focus();
        }
    }, [items]);

    useEffect(() => {
        if (unallocateConsignmentLineResult?.success) {
            if (selectedRow) {
                getItems(selectedRow.consignmentId);
            }
        }
    }, [unallocateConsignmentLineResult, selectedRow, getItems]);

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

    const loadConsignmentItems = id => {
        if (id) {
            getItems(id);
        } else {
            clearItems();
        }

        setResultStyle('noMessage');
        setWandMessage('');
        wandStringInput.current.focus();
    };

    const handleConsignmentChange = newValue => {
        setConsignmentId(newValue.target.value ? parseInt(newValue.target.value, 10) : null);
        loadConsignmentItems(newValue.target.value);
    };

    const handleSelectRow = rows => {
        setSelectedRow(items[rows[0]]);
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

    const handleWandActionChange = (_propertyName, newValue) => {
        setWandAction(newValue);
        wandStringInput.current.focus();
    };

    const handlePrintLabelsChange = (_propertyName, newValue) => {
        setPrintLabels(newValue);
        wandStringInput.current.focus();
    };

    const handleOnWandChange = (_propertyName, newValue) => {
        setWandString(newValue);
    };

    const handleManualSelectChange = (_propertyName, newValue) => {
        setConsignmentId(newValue);
    };

    const handleWand = () => {
        if (wandString && consignmentId) {
            doWandItem({ consignmentId, userNumber, wandAction, wandString, printLabels });
            setWandString(null);
            wandStringInput.current.focus();
        }
    };

    const handleOnKeyPress = data => {
        if (data.keyCode === 13 || data.keyCode === 9) {
            handleWand();
        }
    };

    const handleManualSelectOnKeyPress = data => {
        if (data.keyCode === 13 || data.keyCode === 9) {
            loadConsignmentItems(consignmentId);
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

    const getQtyClass = (functionCode, allWanded) => {
        if (functionCode === 'INVOICE' && allWanded) {
            return classes.ok;
        }

        if (functionCode === 'ALLOC') {
            return classes.notOk;
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
                return getQtyClass(params.row.functionCode, params.row.allWanded);
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
    const manualSelectProp = {
        inputRef: manualSelectInput,
        onKeyDown: handleManualSelectOnKeyPress
    };

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

    const showUnallocateConsignmentLineMessage = () =>
        unallocateConsignmentLineResult && unallocateConsignmentLineResult.message;

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
                        visible={showUnallocateConsignmentLineMessage()}
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
                    <Grid item xs={8}>
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
                        <InputField
                            value={consignmentId}
                            label="Manual Select"
                            propertyName="manualSelect"
                            onChange={handleManualSelectChange}
                            textFieldProps={manualSelectProp}
                        />
                    </Grid>
                    <Grid item xs={2}>
                        <Dropdown
                            label="Labels"
                            propertyName="printLabels"
                            items={[
                                { id: 'Y', displayText: 'Yes' },
                                { id: 'N', displayText: 'No' }
                            ]}
                            value={printLabels}
                            onChange={handlePrintLabelsChange}
                            allowNoValue={false}
                        />
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
                            onChange={handleWandActionChange}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            autoFocus
                            propertyName="wandString"
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
                    <Grid item xs={2}>
                        <LinkButton
                            style={{ marginTop: '22px' }}
                            className="hide-when-printing"
                            tooltip="Go to consignment"
                            disabled={!consignmentId}
                            text="Consignment"
                            to={`/logistics/consignments?consignmentId=${consignmentId}`}
                        />
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
                                onSelectionModelChange={handleSelectRow}
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
    wandResult: null,
    unallocateConsignmentResult: null,
    unallocateConsignmentLineResult: null,
    unallocateConsignmentWorking: false,
    unallocateConsignmentLineWorking: false
};

export default Wand;
