import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    GroupEditTable,
    useGroupEditTable,
    utilities,
    InputField
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';

function ItemsTab({
    editableItems,
    editablePallets,
    dispatch,
    setSaveDisabled,
    cartonTypes,
    setEditStatus,
    viewing
}) {
    const {
        data: itemsData,
        addRow: addItem,
        updateRow: updateItem,
        resetRow: resetItemRow,
        removeRow: removeItem,
        setEditing: setItemsEditing,
        setRowToBeDeleted: setItemRowToBeDeleted,
        setRowToBeSaved: setItemRowToBeSaved
    } = useGroupEditTable({
        rows: editableItems,
        setEditStatus
    });

    const {
        data: palletData,
        addRow: addPallet,
        updateRow: updatePallet,
        resetRow,
        removeRow: removePallet,
        setEditing: setPalletsEditing,
        setRowToBeDeleted: setPalletRowToBeDeleted,
        setRowToBeSaved: setPalletRowToBeSaved
    } = useGroupEditTable({
        rows: editablePallets,
        setEditStatus
    });

    const [addToPalletNumber, setAddToPalletNumber] = useState(0);
    const [addToCartonNumber, setAddToCartonNumber] = useState(0);
    const [firstItem, setFirstItem] = useState(1);
    const [lastItem, setLastItem] = useState(1);
    const [firstCarton, setFirstCarton] = useState(1);
    const [lastCarton, setLastCarton] = useState(1);

    const checkRow = row => {
        if (palletData.filter(pallet => pallet.palletNumber === row.palletNumber).length > 1) {
            return false;
        }

        return true;
    };

    useEffect(() => {
        if (itemsData) {
            if (!itemsData.some(a => a.editing)) {
                dispatch({
                    type: 'updateItems',
                    payload: itemsData
                });
                setSaveDisabled(false);
            } else {
                setSaveDisabled(true);
            }
        }
    }, [itemsData, dispatch, setSaveDisabled]);

    useEffect(() => {
        if (palletData) {
            if (!palletData.some(a => a.editing)) {
                dispatch({
                    type: 'updatePallets',
                    payload: palletData
                });
                setSaveDisabled(false);
            } else {
                setSaveDisabled(true);
            }
        }
    }, [palletData, dispatch, setSaveDisabled]);

    const cartonTypesOptions = () => {
        return utilities.sortEntityList(cartonTypes, 'cartonTypeName')?.map(ct => ({
            id: ct.cartonTypeName,
            displayText: `${ct.cartonTypeName} - ${ct.description}`
        }));
    };

    const addToPallet = palletNumber => {
        if (!palletNumber) {
            const selectedPallet = palletData.find(a => a.editing);
            if (selectedPallet) {
                setAddToPalletNumber(selectedPallet.palletNumber);
            } else {
                setAddToPalletNumber(1);
            }
        } else {
            setAddToPalletNumber(palletNumber);
        }
    };

    const addToCarton = containerNumber => {
        if (!containerNumber) {
            const selectedCarton = itemsData.find(a => a.editing);
            if (selectedCarton) {
                setAddToCartonNumber(selectedCarton.containerNumber);
            } else {
                setAddToCartonNumber(1);
            }
        } else {
            setAddToCartonNumber(containerNumber);
        }
    };

    const handleSetFirstItem = (_, itemNumber) => {
        setFirstItem(itemNumber);
    };

    const handleSetLastItem = (_, itemNumber) => {
        setLastItem(itemNumber);
    };

    const handleSetFirstCarton = (_, cartonNumber) => {
        setFirstCarton(cartonNumber);
    };

    const handleSetLastCarton = (_, cartonNumber) => {
        setLastCarton(cartonNumber);
    };

    const handleChangePalletNumber = (_, palletNumber) => {
        setAddToPalletNumber(palletNumber);
    };

    const handleChangeCartonNumber = (_, containerNumber) => {
        setAddToCartonNumber(containerNumber);
    };

    const recalculatePallet = palletNumber => {
        let weight = 10;
        let height = 0;
        const setValues = item => {
            if (item.height > height) {
                height = item.height;
            }

            weight += item.weight;
        };

        const selectedPallet = palletData.find(a => a.palletNumber === palletNumber);
        if (selectedPallet) {
            itemsData.filter(i => i.palletNumber === palletNumber).forEach(b => setValues(b));

            selectedPallet.height = height > selectedPallet.height ? height : selectedPallet.height;
            selectedPallet.weight = weight;
            selectedPallet.width = selectedPallet.width < 120 ? 120 : selectedPallet.width;
            selectedPallet.depth = selectedPallet.depth < 100 ? 100 : selectedPallet.depth;

            setPalletRowToBeSaved(palletNumber, true);
        }
    };

    const okToPalletise = item => {
        if (item && !item.palletNumber) {
            if (item.itemType === 'I' && item.containerNumber) {
                return false;
            }

            return true;
        }

        return false;
    };

    const addItemToPallet = (palletNumber, itemNumber) => {
        const selectedPallet = palletData.find(a => a.palletNumber === palletNumber);
        const currentItem = itemsData.find(a => a.itemNumber === itemNumber);

        if (selectedPallet && okToPalletise(currentItem)) {
            currentItem.palletNumber = palletNumber;
            setItemRowToBeSaved(currentItem.itemNumber, true);
        }
    };

    const addItemsToPallet = (palletNumber, first, last) => {
        const selectedPallet = palletData.find(a => a.palletNumber === palletNumber);
        if (selectedPallet) {
            for (let i = first; i <= last; i += 1) {
                addItemToPallet(palletNumber, i);
            }

            recalculatePallet(palletNumber);
            setEditStatus('edit');
            setPalletRowToBeSaved(palletNumber, true);
            setAddToPalletNumber(0);
        }
    };

    const addCartonToPallet = (palletNumber, containerNumber) => {
        const selectedPallet = palletData.find(a => a.palletNumber === palletNumber);
        const currentItem = itemsData.find(a => a.containerNumber === containerNumber);

        if (selectedPallet && okToPalletise(currentItem)) {
            currentItem.palletNumber = palletNumber;
            setItemRowToBeSaved(currentItem.itemNumber, true);
        }
    };

    const addCartonsToPallet = (palletNumber, first, last) => {
        const selectedPallet = palletData.find(a => a.palletNumber === palletNumber);
        if (selectedPallet) {
            for (let i = first; i <= last; i += 1) {
                addCartonToPallet(palletNumber, i);
            }

            recalculatePallet(palletNumber);
            setEditStatus('edit');
            setPalletRowToBeSaved(palletNumber, true);
            setAddToPalletNumber(0);
        }
    };

    const recalculateCarton = containerNumber => {
        let weight = 0;
        const setValues = item => {
            if (item.itemType === 'C'){
                weight += item.itemBaseWeight;
            } else {
                weight += item.weight;
            }
        };

        // eslint-disable-next-line prettier/prettier
        const selectedCarton = itemsData.find(a => a.itemType === 'C' && a.containerNumber === containerNumber);

        if (selectedCarton) {
            itemsData.filter(i => i.containerNumber === containerNumber).forEach(b => setValues(b));

            selectedCarton.weight = weight;

            setItemRowToBeSaved(selectedCarton.itemNumber, true);
        }
    };

    const okToCartonise = item => {
        if (item && item.itemType === 'I' && !item.containerNumber && !item.palletNumber) {
            return true;
        }

        return false;
    };

    const addItemToCarton = (containerNumber, itemNumber) => {
        const selectedCarton = itemsData.find(a => a.containerNumber === containerNumber);
        const selectedItem = itemsData.find(a => a.itemNumber === itemNumber);

        if (selectedCarton && okToCartonise(selectedItem)) {
            selectedItem.containerNumber = containerNumber;
            setItemRowToBeSaved(selectedItem.itemNumber, true);
        }
    };

    const addItemsToCarton = (containerNumber, first, last) => {
        const selectedCarton = itemsData.find(a => a.containerNumber === containerNumber);
        if (selectedCarton) {
            setItemRowToBeSaved(selectedCarton.itemNumber, true);
            for (let i = first; i <= last; i += 1) {
                addItemToCarton(containerNumber, i);
            }

            recalculateCarton(containerNumber);
            setEditStatus('edit');
            setAddToCartonNumber(0);
        }
    };

    const handleAddItemsToPallet = () => {
        addItemsToPallet(addToPalletNumber, firstItem, lastItem);
    };

    const handleAddItemsToCarton = () => {
        addItemsToCarton(addToCartonNumber, firstItem, lastItem);
    };

    const handleAddCartonsToPallet = () => {
        addCartonsToPallet(addToPalletNumber, firstCarton, lastCarton);
    };

    const palletColumns = [
        {
            title: 'Pallet No',
            id: 'palletNumber',
            type: 'number',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '110px', maxWidth: '110px' }
            }
        },
        {
            title: 'Weight',
            id: 'weight',
            type: 'number',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '110px', maxWidth: '110px' }
            }
        },
        {
            title: 'Height',
            id: 'height',
            type: 'number',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '110px', maxWidth: '110px' }
            }
        },
        {
            title: 'Width',
            id: 'width',
            type: 'number',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '110px', maxWidth: '110px' }
            }
        },
        {
            title: 'Depth',
            id: 'depth',
            type: 'number',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '110px', maxWidth: '110px' }
            }
        }
    ];

    const itemColumns = [
        {
            title: 'Item No',
            id: 'itemNumber',
            type: 'number',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '100px' }
            }
        },
        {
            title: 'Carton',
            id: 'containerNumber',
            type: 'number',
            editable: true,
            style: {
                body: { minWidth: '100px' }
            }
        },
        {
            title: 'Pallet No',
            id: 'palletNumber',
            type: 'number',
            editable: true,
            style: {
                body: { minWidth: '100px' }
            }
        },
        {
            title: 'Item Type',
            id: 'itemTypeDisplay',
            type: 'dropdown',
            editable: true,
            options: [
                { id: 'Loose Item', displayText: 'Loose Item' },
                { id: 'Open Carton', displayText: 'Open Carton' },
                { id: 'Sealed Box', displayText: 'Sealed Box' }
            ],
            style: {
                body: { minWidth: '180px' }
            }
        },
        {
            title: 'Qty',
            id: 'quantity',
            type: 'number',
            editable: true,
            required: true
        },
        {
            title: 'Item Description',
            id: 'itemDescription',
            type: 'text',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '300px' }
            }
        },
        {
            title: 'Weight',
            id: 'weight',
            type: 'number',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '110px' }
            }
        },
        {
            title: 'Height',
            id: 'height',
            type: 'number',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '110px' }
            }
        },
        {
            title: 'Width',
            id: 'width',
            type: 'number',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '110px' }
            }
        },
        {
            title: 'Depth',
            id: 'depth',
            type: 'number',
            editable: true,
            required: true,
            style: {
                body: { minWidth: '110px' }
            }
        },
        {
            title: 'Order No',
            id: 'orderNumber',
            type: 'number',
            editable: true
        },
        {
            title: 'Line',
            id: 'orderLine',
            type: 'number',
            editable: true,
            style: {
                body: { minWidth: '100px' }
            }
        },
        {
            title: 'Rsn No',
            id: 'rsnNumber',
            type: 'number',
            editable: true
        },
        {
            title: 'Container Type',
            id: 'containerType',
            type: 'dropdown',
            editable: true,
            options: cartonTypesOptions(),
            style: {
                body: { minWidth: '180px' }
            }
        }
    ];

    return (
        <>
            <Grid container spacing={3} style={{ paddingTop: '30px' }}>
                <Grid item xs={1}>
                    <Typography variant="subtitle2">Pallets</Typography>
                </Grid>
                <Grid item xs={7}>
                    {palletData && (
                        <GroupEditTable
                            columns={palletColumns}
                            rows={palletData}
                            updateRow={updatePallet}
                            addRow={addPallet}
                            removeRow={removePallet}
                            resetRow={resetRow}
                            handleEditClick={setPalletsEditing}
                            editable
                            allowNewRowCreation={false}
                            deleteRowPreEdit={false}
                            setRowToBeSaved={setPalletRowToBeSaved}
                            setRowToBeDeleted={setPalletRowToBeDeleted}
                            closeRowOnClickAway
                            removeRowOnDelete
                            validateRow={checkRow}
                            editOnRowClick
                        />
                    )}
                </Grid>
                <Grid item xs={2}>
                    {palletData.length > 0 ? (
                        <Button
                            style={{ marginTop: '40px' }}
                            onClick={() => addToPallet()}
                            variant="outlined"
                            color="primary"
                            disabled={viewing}
                        >
                            Add To Pallet
                        </Button>
                    ) : (
                        ''
                    )}
                </Grid>
                <Grid item xs={2}>
                    {itemsData.length > 0 && itemsData.find(a => a.containerNumber) ? (
                        <Button
                            style={{ marginTop: '40px' }}
                            onClick={() => addToCarton()}
                            variant="outlined"
                            color="primary"
                            disabled={viewing}
                        >
                            Add To Carton
                        </Button>
                    ) : (
                        ''
                    )}
                </Grid>
            </Grid>
            <Grid container spacing={3} style={{ paddingTop: '50px' }}>
                <Grid item xs={1}>
                    <Typography variant="subtitle2">Items</Typography>
                </Grid>
                <Grid item xs={11}>
                    {itemsData && (
                        <GroupEditTable
                            columns={itemColumns}
                            rows={itemsData}
                            updateRow={updateItem}
                            addRow={addItem}
                            removeRow={removeItem}
                            resetRow={resetItemRow}
                            handleEditClick={setItemsEditing}
                            editable
                            allowNewRowCreation={false}
                            deleteRowPreEdit={false}
                            setRowToBeSaved={setItemRowToBeSaved}
                            setRowToBeDeleted={setItemRowToBeDeleted}
                            closeRowOnClickAway
                            removeRowOnDelete
                            validateRow={checkRow}
                            editOnRowClick
                        />
                    )}
                </Grid>
            </Grid>
            <Dialog
                open={addToPalletNumber > 0}
                onClose={() => setAddToPalletNumber(0)}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
                fullWidth
                maxWidth="sm"
            >
                <DialogTitle id="alert-dialog-title">Add to pallet {addToPalletNumber}</DialogTitle>
                <DialogContent>
                    <>
                        <Grid container>
                            <Grid item xs={3} style={{ marginBottom: '40px' }}>
                                <InputField
                                    label="Pallet Number"
                                    placeholder="Pallet Number"
                                    propertyName="palletNumber"
                                    type="number"
                                    value={addToPalletNumber}
                                    onChange={handleChangePalletNumber}
                                    maxLength={3}
                                />
                            </Grid>
                            <Grid item xs={1} />
                            <Grid item xs={3}>
                                <Button
                                    style={{ marginTop: '23px', marginBottom: '40px' }}
                                    variant="contained"
                                    color="primary"
                                    onClick={() => recalculatePallet(addToPalletNumber)}
                                >
                                    Recalculate
                                </Button>
                            </Grid>
                            <Grid item xs={5} />
                            <Grid item xs={3}>
                                <InputField
                                    label="First Item"
                                    placeholder="First Item"
                                    propertyName="firstItem"
                                    type="number"
                                    value={firstItem}
                                    onChange={handleSetFirstItem}
                                    maxLength={3}
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <InputField
                                    label="Last Item"
                                    placeholder="Last Item"
                                    propertyName="lastItem"
                                    type="number"
                                    value={lastItem}
                                    onChange={handleSetLastItem}
                                    maxLength={3}
                                />
                            </Grid>
                            <Grid item xs={1} />
                            <Grid item xs={5}>
                                <Button
                                    style={{ marginTop: '23px', marginBottom: '40px' }}
                                    onClick={handleAddItemsToPallet}
                                    variant="contained"
                                    color="primary"
                                >
                                    Add To Pallet {addToPalletNumber}
                                </Button>
                            </Grid>
                            <Grid item xs={3}>
                                <InputField
                                    label="First Carton"
                                    placeholder="First Carton"
                                    propertyName="firstCarton"
                                    type="number"
                                    value={firstCarton}
                                    onChange={handleSetFirstCarton}
                                    maxLength={3}
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <InputField
                                    label="Last Carton"
                                    placeholder="Last Carton"
                                    propertyName="lastCarton"
                                    type="number"
                                    value={lastCarton}
                                    onChange={handleSetLastCarton}
                                    maxLength={3}
                                />
                            </Grid>
                            <Grid item xs={1} />
                            <Grid item xs={5}>
                                <Button
                                    style={{ marginTop: '23px', marginBottom: '40px' }}
                                    onClick={handleAddCartonsToPallet}
                                    variant="contained"
                                    color="primary"
                                >
                                    Add To Pallet {addToPalletNumber}
                                </Button>
                            </Grid>
                        </Grid>
                    </>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setAddToPalletNumber(0)} variant="contained" autoFocus>
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
            <Dialog
                open={addToCartonNumber > 0}
                onClose={() => setAddToCartonNumber(0)}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
                fullWidth
                maxWidth="sm"
            >
                <DialogTitle id="alert-dialog-title">Add to carton {addToCartonNumber}</DialogTitle>
                <DialogContent>
                    <>
                        <Grid container>
                            <Grid item xs={3} style={{ marginBottom: '40px' }}>
                                <InputField
                                    label="Carton Number"
                                    placeholder="Carton Number"
                                    propertyName="containerNumber"
                                    type="number"
                                    value={addToCartonNumber}
                                    onChange={handleChangeCartonNumber}
                                    maxLength={3}
                                />
                            </Grid>
                            <Grid item xs={1} />
                            <Grid item xs={3}>
                                <Button
                                    style={{ marginTop: '23px', marginBottom: '40px' }}
                                    variant="contained"
                                    color="primary"
                                    onClick={() => recalculateCarton(addToCartonNumber)}
                                >
                                    Recalculate
                                </Button>
                            </Grid>
                            <Grid item xs={5} />
                            <Grid item xs={3}>
                                <InputField
                                    label="First Item"
                                    placeholder="First Item"
                                    propertyName="firstItem"
                                    type="number"
                                    value={firstItem}
                                    onChange={handleSetFirstItem}
                                    maxLength={3}
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <InputField
                                    label="Last Item"
                                    placeholder="Last Item"
                                    propertyName="lastItem"
                                    type="number"
                                    value={lastItem}
                                    onChange={handleSetLastItem}
                                    maxLength={3}
                                />
                            </Grid>
                            <Grid item xs={1} />
                            <Grid item xs={5}>
                                <Button
                                    style={{ marginTop: '23px', marginBottom: '40px' }}
                                    onClick={handleAddItemsToCarton}
                                    variant="contained"
                                    color="primary"
                                >
                                    Add To Carton {addToCartonNumber}
                                </Button>
                            </Grid>
                        </Grid>
                    </>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setAddToCartonNumber(0)} variant="contained" autoFocus>
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}

ItemsTab.propTypes = {
    dispatch: PropTypes.func.isRequired,
    setSaveDisabled: PropTypes.func.isRequired,
    editableItems: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    editablePallets: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    cartonTypes: PropTypes.arrayOf(
        PropTypes.shape({ cartonTypeName: PropTypes.string, description: PropTypes.string })
    ).isRequired,
    setEditStatus: PropTypes.func.isRequired,
    viewing: PropTypes.bool.isRequired
};

export default ItemsTab;
