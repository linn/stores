import React, { useState, useRef, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import TextField from '@material-ui/core/TextField';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import { DataGrid } from '@material-ui/data-grid';
import { Title, Dropdown, Loading, InputField } from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/core/styles';
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
    unallocateRequisition
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
        }
    }, [wandResult]);

    useEffect(() => {
        if (
            wandResult &&
            wandResult.wandLog &&
            wandResult.wandLog.id &&
            lastWandLogId !== wandResult.wandLog.id
        ) {
            const consignmentDetails = wandConsignments.find(
                a => a.consignmentId === consignmentId
            );
            const wandedItem = items.find(
                a =>
                    a.orderNumber === wandResult.wandLog.orderNumber &&
                    a.orderLine === wandResult.wandLog.orderLine
            );

            // we now have enough information to optionally print a label here
            setLastWandLogId(wandResult.wandLog.id);
        }
    }, [wandResult, consignmentId, wandConsignments, lastWandLogId, items]);

    const useStyles = makeStyles({
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
        }
    });

    const classes = useStyles();

    const handleConsignmentChange = (_propertyName, newValue) => {
        setConsignmentId(newValue ? parseInt(newValue, 10) : null);
        if (newValue) {
            getItems(newValue);
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

    const handleUnallocateConsignment = () => {
        if (items && items.length > 0) {
            unallocateRequisition({ requisitionNumber: items[0].requisitionNumber, userNumber });
        }
    };

    const handleUnallocateLine = () => {
        if (selectedRow) {
            unallocateRequisition({
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

    const consignmentOptions = () => {
        return wandConsignments?.map(c => ({
            id: c.consignmentId,
            displayText: `Consignment: ${c.consignmentId} Addressee: ${c.addressee} Country: ${
                c.countryCode
            } ${c.isDone ? c.isDone : ' '} `
        }));
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

    const columns = [
        { field: 'partNumber', headerName: 'Article No', width: 140 },
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
            valueGetter: params => getBoxValue(params, 1)
        },
        {
            field: 'box2',
            headerName: '2',
            description: 'Box 2',
            width: 60,
            valueGetter: params => getBoxValue(params, 2)
        },
        {
            field: 'box3',
            description: 'Box 3',
            headerName: '3',
            width: 60,
            valueGetter: params => getBoxValue(params, 3)
        },
        {
            field: 'box4',
            headerName: '4',
            description: 'Box 4',
            width: 60,
            valueGetter: params => getBoxValue(params, 4)
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

    return (
        <Page>
            <Dialog open={showAlert} onClose={() => setShowAlert(false)}>
                <DialogContent>
                    <DialogContentText>{wandString}</DialogContentText>
                </DialogContent>
            </Dialog>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Wand" />
                </Grid>
            </Grid>
            {loadingWandConsignments ? (
                <Loading />
            ) : (
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <Dropdown
                            label="Consignment"
                            propertyName="consignment"
                            items={consignmentOptions()}
                            value={consignmentId}
                            onChange={handleConsignmentChange}
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
                        {doWandItemWorking ? (
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
                                pagination={false}
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
    unallocateRequisition: PropTypes.func.isRequired
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
    }
};

export default Wand;
