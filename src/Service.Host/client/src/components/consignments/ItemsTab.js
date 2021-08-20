import React, { useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    GroupEditTable,
    useGroupEditTable,
    utilities
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';

function ItemsTab({
    editableItems,
    editablePallets,
    dispatch,
    setSaveDisabled,
    cartonTypes,
    setEditStatus
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
                <Grid item xs={8}>
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
                            allowNewRowCreation
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
                <Grid item xs={3} />
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
    setEditStatus: PropTypes.func.isRequired
};

export default ItemsTab;
