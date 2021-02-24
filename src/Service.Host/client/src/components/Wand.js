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
import { makeStyles } from "@material-ui/core/styles";
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
    doWandItem
}) {
    const [consignmentId, setConsignmentId] = useState('');
    const [wandAction, setWandAction] = useState('W');
    const [wandString, setWandString] = useState(null);
    const [showAlert, setShowAlert] = useState(false);
    const [wandMessage, setWandMessage] = useState(null);

    const wandStringInput = useRef(null);

    useEffect(() => {
        setWandString(null);
    }, [items]);

    const handleConsignmentChange = (_propertyName, newValue) => {
        setConsignmentId(newValue);
        if (newValue) {
            getItems(newValue);
        } else {
            clearItems();
        }
        wandStringInput.current.focus();
    };

    const useStyles = makeStyles({
        ok: {
            color: 'black',
            backgroundColor: 'lightGreen'
        },
        notOk: {
            color: 'black',
            backgroundColor: 'red'
        },
        noMessage:{
            color: 'black',
            backgroundColor: 'white'
        }
    });

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
            //setShowAlert(wandString);
            setWandMessage(`Wanded ${wandString}`);
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

    const columns = [
        { field: 'partNumber', headerName: 'Article No', width: 140 },
        { field: 'quantity', headerName: 'Qty', width: 100 },
        { field: 'quantityScanned', headerName: 'Scanned', width: 120 },
        { field: 'partDescription', headerName: 'Description', width: 230 },
        { field: 'orderNumber', headerName: 'Order', width: 100 },
        { field: 'orderLine', headerName: 'Line', width: 80 },
        { field: 'linnBarCode', headerName: 'Bar Code', width: 120, hide: true },
        { field: 'requisitionNumber', headerName: 'Req No', width: 100, hide: true },
        { field: 'requisitionLine', headerName: 'Req Line', width: 110, hide: true }
    ];
    const focusProp = { inputRef: wandStringInput, onKeyDown: handleOnKeyPress };
    const classes = useStyles();

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
                        <TextField
                            style={{ padding: 10 }}
                            className={classes.ok}
                            id="wand-status"
                            fullWidth
                            value={wandMessage}
                            InputProps={{
                                readOnly: true,
                                disableUnderline: true
                            }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <div style={{ display: 'flex', height: 600 }}>
                            <div style={{ flexGrow: 1 }}>
                                <DataGrid
                                    rows={getDetailRows(items)}
                                    columns={columns}
                                    density="compact"
                                    autoHeight
                                    loading={itemsLoading}
                                    hideFooter
                                />
                            </div>
                        </div>
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
    items: PropTypes.arrayOf(PropTypes.shape({})),
    itemsLoading: PropTypes.bool,
    userNumber: PropTypes.number.isRequired,
    doWandItemWorking: PropTypes.bool,
    doWandItem: PropTypes.func.isRequired
};

Wand.defaultProps = {
    wandConsignments: [],
    loadingWandConsignments: false,
    items: [],
    itemsLoading: false,
    doWandItemWorking: false
};

export default Wand;
