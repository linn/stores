import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    GroupEditTable,
    utilities,
    InputField,
    Dropdown,
    DatePicker,
    useGroupEditTable
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import { DataGrid } from '@mui/x-data-grid';
import { makeStyles } from '@material-ui/core/styles';
import moment from 'moment';

function DetailsItemsTab({
    consignment,
    updateField,
    viewMode,
    editStatus,
    hub,
    hubs,
    hubsLoading,
    carrier,
    carriers,
    carriersLoading,
    shippingTerm,
    shippingTerms,
    shippingTermsLoading,
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
        setRowToBeSaved: setItemRowToBeSaved,
        replaceRow: replaceItem
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
    const [palletDialogPalletNumber, setPalletDialogPalletNumber] = useState(0);
    const [cartonDialogContainerNumber, setCartonDialogContainerNumber] = useState(0);
    const [addToCartonNumber, setAddToCartonNumber] = useState(0);
    const [firstItem, setFirstItem] = useState(1);
    const [lastItem, setLastItem] = useState(1);
    const [firstCarton, setFirstCarton] = useState(1);
    const [lastCarton, setLastCarton] = useState(1);
    const [selectedPalletItems, setSelectedPalletItems] = useState([]);
    const [selectedLooseItems, setSelectedLooseItems] = useState([]);
    const [selectedCartonItems, setSelectedCartonItems] = useState([]);
    const [showOptionalColumns, setShowOptionalColumns] = useState(false);

    const useStyles = makeStyles(() => ({
        tableCell: {
            borderBottom: 0,
            whiteSpace: 'pre-line',
            verticalAlign: 'top'
        }
    }));
    const classes = useStyles();

    const TablePromptItem = ({ text, width }) => (
        <TableCell style={{ width, borderBottom: 0, whiteSpace: 'pre-line', verticalAlign: 'top' }}>
            {text}
        </TableCell>
    );

    TablePromptItem.propTypes = {
        text: PropTypes.string,
        width: PropTypes.number
    };

    TablePromptItem.defaultProps = {
        text: null,
        width: 150
    };

    const showText = (displayText, displayDescription) => {
        if (displayText) {
            return `${displayText} ${displayDescription ? ` - ${displayDescription}` : ''} `;
        }

        return '';
    };

    const DisplayEditItem = ({
        currentEditStatus,
        displayText,
        displayDescription,
        editComponent,
        allowCreate
    }) => {
        if (currentEditStatus === 'view' || (currentEditStatus === 'create' && !allowCreate)) {
            if (displayText) {
                return `${displayText} ${displayDescription ? ` - ${displayDescription}` : ''} `;
            }

            return '';
        }

        return editComponent;
    };

    DisplayEditItem.propTypes = {
        currentEditStatus: PropTypes.string,
        displayText: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        displayDescription: PropTypes.string,
        editComponent: PropTypes.shape(),
        allowCreate: PropTypes.bool
    };

    DisplayEditItem.defaultProps = {
        currentEditStatus: 'view',
        displayText: null,
        displayDescription: null,
        editComponent: <></>,
        allowCreate: true
    };

    const hubOptions = () => {
        return utilities.sortEntityList(hubs, 'hubId')?.map(h => ({
            id: h.hubId,
            displayText: `${h.hubId} - ${h.description}`
        }));
    };

    const carrierOptions = () => {
        return utilities.sortEntityList(carriers, 'carrierCode')?.map(c => ({
            id: c.carrierCode,
            displayText: `${c.carrierCode} - ${c.name}`
        }));
    };

    const shippingTermOptions = () => {
        return utilities.sortEntityList(shippingTerms, 'code')?.map(h => ({
            id: h.code,
            displayText: `${h.code} - ${h.description}`
        }));
    };

    const freightOptions = () => {
        return [
            { id: 'S', displayText: 'Surface' },
            { id: 'A', displayText: 'Air' },
            { id: 'W', displayText: 'Sea' }
        ];
    };

    const showShippingMethod = shippingMethod => {
        switch (shippingMethod) {
            case 'S':
                return 'Surface';
            case 'A':
                return 'Air';
            case 'W':
                return 'Sea';
            default:
                return 'Other';
        }
    };

    const showStatus = statusCode => {
        switch (statusCode) {
            case 'L':
                return 'Open';
            case 'H':
                return 'Hold';
            case 'C':
                return 'Closed';
            default:
                return 'Unknown';
        }
    };

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

    const addRemoveToFromPallet = palletNumber => {
        if (!palletNumber) {
            const selectedPallet = palletData.find(a => a.editing);
            if (selectedPallet) {
                setPalletDialogPalletNumber(selectedPallet.palletNumber);
            } else {
                setPalletDialogPalletNumber(1);
            }
        } else {
            setPalletDialogPalletNumber(palletNumber);
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

    const addRemoveToFromCarton = containerNumber => {
        if (!containerNumber) {
            const selectedCarton = itemsData.find(a => a.editing);
            if (selectedCarton) {
                setCartonDialogContainerNumber(selectedCarton.containerNumber);
            } else {
                setCartonDialogContainerNumber(1);
            }
        } else {
            setCartonDialogContainerNumber(containerNumber);
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

    const removeItemFromPallet = (palletNumber, itemNumber) => {
        const currentItem = itemsData.find(a => a.itemNumber === itemNumber);

        if (currentItem.palletNumber === palletNumber) {
            currentItem.palletNumber = null;
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
            if (item.itemType === 'C') {
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

    const removeItemFromCarton = (containerNumber, itemNumber) => {
        const selectedItem = itemsData.find(a => a.itemNumber === itemNumber);

        if (selectedItem.containerNumber === containerNumber) {
            selectedItem.containerNumber = null;
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

    const handlePalletDialogSelectPalletRow = row => {
        setSelectedPalletItems(itemsData.filter(i => row.includes(i.id)));
    };

    const handleCartonDialogSelectCartonRow = row => {
        setSelectedCartonItems(itemsData.filter(i => row.includes(i.id)));
    };

    const handleDialogSelectItemRow = row => {
        setSelectedLooseItems(itemsData.filter(i => row.includes(i.id)));
    };

    const removeSelectedPalletItems = () => {
        selectedPalletItems.forEach(item => {
            removeItemFromPallet(palletDialogPalletNumber, item.itemNumber);
        });

        recalculatePallet(palletDialogPalletNumber);
    };

    const addSelectedLooseItemsToPallet = () => {
        selectedLooseItems.forEach(item => {
            addItemToPallet(palletDialogPalletNumber, item.itemNumber);
        });

        recalculatePallet(palletDialogPalletNumber);
    };

    const removeAllPalletItems = () => {
        itemsData
            .filter(a => a.palletNumber === palletDialogPalletNumber)
            .forEach(item => {
                removeItemFromPallet(palletDialogPalletNumber, item.itemNumber);
            });

        recalculatePallet(palletDialogPalletNumber);
    };

    const openPalletDialog = palletNumber => {
        setSelectedLooseItems([]);
        setSelectedPalletItems([]);
        setPalletDialogPalletNumber(palletNumber);
    };

    const openCartonDialog = palletNumber => {
        setSelectedLooseItems([]);
        setSelectedCartonItems([]);
        setCartonDialogContainerNumber(palletNumber);
    };

    const removeAllCartonItems = () => {
        itemsData
            .filter(a => a.containerNumber === cartonDialogContainerNumber && a.itemType === 'I')
            .forEach(item => {
                removeItemFromCarton(cartonDialogContainerNumber, item.itemNumber);
            });

        recalculateCarton(cartonDialogContainerNumber);
    };

    const removeSelectedCartonItems = () => {
        selectedCartonItems.forEach(item => {
            removeItemFromCarton(cartonDialogContainerNumber, item.itemNumber);
        });

        recalculateCarton(cartonDialogContainerNumber);
    };

    const addSelectedLooseItemsToCarton = () => {
        selectedLooseItems.forEach(item => {
            addItemToCarton(cartonDialogContainerNumber, item.itemNumber);
        });

        recalculateCarton(cartonDialogContainerNumber);
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
            title: 'Item',
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
        }
    ];

    const optionalItemColumns = [
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

    const displayedColumns = () => {
        if (showOptionalColumns) {
            return itemColumns.concat(optionalItemColumns);
        }

        return itemColumns;
    };

    const toggleColumns = () => {
        setShowOptionalColumns(!showOptionalColumns);
    };

    const updateItemRow = (item, _setItem, propertyName, newValue) => {
        if (
            item.itemType === 'S' &&
            newValue === 'Loose Item' &&
            propertyName === 'itemTypeDisplay'
        ) {
            const newItem = {
                ...item,
                itemTypeDisplay: 'Loose Item',
                itemType: 'I',
                containerNumber: null,
                editing: true
            };
            replaceItem(item, newItem);
        } else {
            updateItem(item, _setItem, propertyName, newValue);
        }
    };

    const palletDialogColumns = [
        { field: 'itemNumber', headerName: 'Item', minWidth: 80, disableColumnMenu: true },
        { field: 'containerNumber', headerName: 'Box', minWidth: 80, disableColumnMenu: true },
        { field: 'itemTypeDisplay', headerName: 'Type', minWidth: 100, disableColumnMenu: true },
        {
            field: 'itemDescription',
            headerName: 'Description',
            minWidth: 150,
            disableColumnMenu: true
        }
    ];

    const cartonDialogColumns = [
        { field: 'itemNumber', headerName: 'Item', minWidth: 80, disableColumnMenu: true },
        {
            field: 'itemDescription',
            headerName: 'Description',
            minWidth: 150,
            disableColumnMenu: true
        }
    ];

    return (
        <>
            <Grid item xs={12}>
                <Table size="small" style={{ paddingTop: '30px' }}>
                    <TableBody>
                        <TableRow key="Account">
                            <TablePromptItem text="Account" />
                            <TableCell className={classes.tableCell}>
                                {editStatus !== 'create' ? (
                                    `${showText(consignment.salesAccountId)} ${showText(
                                        consignment.customerName
                                    )}`
                                ) : (
                                    <>
                                        <InputField
                                            placeholder="Account Id"
                                            propertyName="salesAccountId"
                                            value={consignment.salesAccountId}
                                            onChange={updateField}
                                            maxLength={10}
                                        />
                                        <InputField
                                            placeholder="Customer Name"
                                            propertyName="customerName"
                                            value={consignment.customerName}
                                            onChange={updateField}
                                        />
                                    </>
                                )}
                            </TableCell>
                        </TableRow>
                        <TableRow key="Address">
                            <TablePromptItem text="Address" />
                            <TableCell className={classes.tableCell}>
                                {editStatus !== 'create' ? (
                                    consignment.address && consignment.address.displayAddress
                                ) : (
                                    <>
                                        <InputField
                                            placeholder="AddressId"
                                            propertyName="addressId"
                                            value={consignment.addressId}
                                            onChange={updateField}
                                            maxLength={10}
                                        />
                                    </>
                                )}
                            </TableCell>
                        </TableRow>
                        <TableRow key="Despatch Location">
                            <TablePromptItem text="Despatch Location" />
                            <TableCell className={classes.tableCell}>
                                {consignment.despatchLocationCode}
                            </TableCell>
                        </TableRow>
                        <TableRow key="Freight">
                            <TablePromptItem text="Freight" />
                            <TableCell className={classes.tableCell}>
                                <DisplayEditItem
                                    currentEditStatus={editStatus}
                                    displayText={showShippingMethod(consignment.shippingMethod)}
                                    editComponent={
                                        <Dropdown
                                            propertyName="shippingMethod"
                                            items={freightOptions()}
                                            onChange={updateField}
                                            value={consignment.shippingMethod}
                                            allowNoValue={false}
                                        />
                                    }
                                />
                            </TableCell>
                        </TableRow>
                        <TableRow key="Carrier">
                            <TablePromptItem text="Carrier" />
                            <TableCell className={classes.tableCell}>
                                <DisplayEditItem
                                    currentEditStatus={editStatus}
                                    displayText={consignment.carrier}
                                    displayDescription={carrier && carrier.name}
                                    editComponent={
                                        <Dropdown
                                            propertyName="carrier"
                                            items={carrierOptions()}
                                            onChange={updateField}
                                            value={consignment.carrier}
                                            optionsLoading={carriersLoading}
                                            allowNoValue={false}
                                        />
                                    }
                                />
                            </TableCell>
                        </TableRow>
                        <TableRow key="Terms">
                            <TablePromptItem text="Terms" />
                            <TableCell className={classes.tableCell}>
                                <DisplayEditItem
                                    currentEditStatus={editStatus}
                                    displayText={consignment.terms}
                                    displayDescription={shippingTerm?.description}
                                    editComponent={
                                        <Dropdown
                                            propertyName="terms"
                                            items={shippingTermOptions()}
                                            onChange={updateField}
                                            value={consignment.terms}
                                            optionsLoading={shippingTermsLoading}
                                        />
                                    }
                                />
                            </TableCell>
                        </TableRow>
                        <TableRow key="Hub">
                            <TablePromptItem text="Hub" />
                            <TableCell className={classes.tableCell}>
                                <DisplayEditItem
                                    currentEditStatus={editStatus}
                                    displayText={consignment.hubId}
                                    displayDescription={hub && hub.description}
                                    editComponent={
                                        <Dropdown
                                            propertyName="hubId"
                                            items={hubOptions()}
                                            onChange={updateField}
                                            value={consignment.hubId}
                                            optionsLoading={hubsLoading}
                                        />
                                    }
                                />
                            </TableCell>
                        </TableRow>
                        <TableRow key="CustomsEntry">
                            <TablePromptItem text="Customs Entry Code" />
                            <TableCell className={classes.tableCell}>
                                {viewMode ? (
                                    `${showText(consignment.customsEntryCodePrefix)} ${showText(
                                        consignment.customsEntryCode
                                    )}`
                                ) : (
                                    <>
                                        <InputField
                                            placeholder="Prefix"
                                            propertyName="customsEntryCodePrefix"
                                            value={consignment.customsEntryCodePrefix}
                                            onChange={updateField}
                                            maxLength={3}
                                        />
                                        <InputField
                                            placeholder="Entry Code"
                                            propertyName="customsEntryCode"
                                            value={consignment.customsEntryCode}
                                            onChange={updateField}
                                            maxLength={20}
                                        />
                                    </>
                                )}
                            </TableCell>
                            <TablePromptItem text="Entry Code Date" />
                            <TableCell className={classes.tableCell}>
                                {viewMode ? (
                                    consignment.customsEntryCodeDate &&
                                    moment(consignment.customsEntryCodeDate).format('DD MMM YYYY')
                                ) : (
                                    <DatePicker
                                        value={
                                            consignment.customsEntryCodeDate
                                                ? consignment.customsEntryCodeDate
                                                : null
                                        }
                                        onChange={value => {
                                            updateField('customsEntryCodeDate', value);
                                        }}
                                    />
                                )}
                            </TableCell>
                        </TableRow>
                        <TableRow key="Status">
                            <TablePromptItem text="Status" />
                            <TableCell className={classes.tableCell}>
                                {showStatus(consignment?.status)}
                            </TableCell>
                        </TableRow>
                        <TableRow key="DateOpened">
                            <TablePromptItem text="Date Opened" />
                            <TableCell className={classes.tableCell}>
                                {moment(consignment.dateOpened).format('DD MMM YYYY')}
                            </TableCell>
                        </TableRow>
                        <TableRow key="DateClosed">
                            <TablePromptItem text="Date Closed" />
                            <TableCell className={classes.tableCell}>
                                {consignment.dateClosed &&
                                    moment(consignment.dateClosed).format('DD MMM YYYY')}
                            </TableCell>
                            <TablePromptItem text="Closed By" />
                            <TableCell className={classes.tableCell}>
                                {consignment.closedBy && consignment.closedBy.fullName}
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </Grid>
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
                        <>
                            <Button
                                onClick={() => addRemoveToFromPallet()}
                                variant="outlined"
                                color="primary"
                                disabled={viewing}
                            >
                                Pallet Add/Remove
                            </Button>
                            <Button
                                style={{ marginTop: '10px' }}
                                onClick={() => addToPallet()}
                                variant="outlined"
                                color="primary"
                                disabled={viewing}
                            >
                                Add Items To Pallet
                            </Button>
                        </>
                    ) : (
                        ''
                    )}
                </Grid>
                <Grid item xs={2}>
                    {itemsData.length > 0 && itemsData.find(a => a.containerNumber) ? (
                        <>
                            <Button
                                onClick={() => addRemoveToFromCarton()}
                                variant="outlined"
                                color="primary"
                                disabled={viewing}
                            >
                                Carton Add/Remove
                            </Button>
                            <Button
                                style={{ marginTop: '10px' }}
                                onClick={() => addToCarton()}
                                variant="outlined"
                                color="primary"
                                disabled={viewing}
                            >
                                Add Items To Carton
                            </Button>
                        </>
                    ) : (
                        ''
                    )}
                </Grid>
            </Grid>
            <Grid container spacing={3} style={{ paddingTop: '50px' }}>
                <Grid item xs={1}>
                    <Typography variant="subtitle2">Items</Typography>
                    <Button
                        style={{ marginTop: '10px' }}
                        onClick={() => toggleColumns()}
                        variant="outlined"
                        color="primary"
                    >
                        {showOptionalColumns ? 'Hide Cols' : 'Expand'}
                    </Button>
                </Grid>
                <Grid item xs={11}>
                    {itemsData && (
                        <GroupEditTable
                            columns={displayedColumns()}
                            rows={itemsData}
                            updateRow={updateItemRow}
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
                            <Grid item xs={3}>
                                <Button
                                    style={{ marginTop: '23px', marginBottom: '40px' }}
                                    variant="contained"
                                    color="primary"
                                    onClick={() => openPalletDialog(addToPalletNumber)}
                                >
                                    Add/Remove
                                </Button>
                            </Grid>
                            <Grid item xs={2} />
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
                open={palletDialogPalletNumber > 0}
                onClose={() => setPalletDialogPalletNumber(0)}
                fullWidth
                maxWidth="xl"
            >
                <DialogTitle id="alert-dialog-title">
                    Add/Remove items on pallet {palletDialogPalletNumber}
                </DialogTitle>
                <DialogContent>
                    <>
                        <Grid container justifyContent="center">
                            <Grid item xs={5}>
                                <div style={{ height: 500, width: '100%' }}>
                                    <DataGrid
                                        rows={itemsData.filter(
                                            a => a.palletNumber === palletDialogPalletNumber
                                        )}
                                        columns={palletDialogColumns}
                                        density="compact"
                                        rowHeight={34}
                                        checkboxSelection
                                        hideFooter
                                        onSelectionModelChange={handlePalletDialogSelectPalletRow}
                                    />
                                </div>
                            </Grid>
                            <Grid item xs={1}>
                                <Button
                                    onClick={() => removeAllPalletItems()}
                                    variant="contained"
                                    autoFocus
                                    fullWidth
                                >
                                    &gt;&gt;
                                </Button>
                                <Button
                                    style={{ marginTop: '20px', marginBottom: '20px' }}
                                    onClick={() => removeSelectedPalletItems()}
                                    variant="contained"
                                    autoFocus
                                    fullWidth
                                >
                                    -&gt;
                                </Button>
                                <Button
                                    onClick={() => addSelectedLooseItemsToPallet()}
                                    variant="contained"
                                    autoFocus
                                    fullWidth
                                >
                                    &lt;-
                                </Button>
                            </Grid>
                            <Grid item xs={5}>
                                <div style={{ height: 500, width: '100%' }}>
                                    <DataGrid
                                        rows={itemsData.filter(a => okToPalletise(a))}
                                        columns={palletDialogColumns}
                                        density="compact"
                                        rowHeight={34}
                                        checkboxSelection
                                        hideFooter
                                        onSelectionModelChange={handleDialogSelectItemRow}
                                    />
                                </div>
                            </Grid>
                            <Grid item xs={1} />
                        </Grid>
                    </>
                </DialogContent>
                <DialogActions>
                    <Button
                        onClick={() => setPalletDialogPalletNumber(0)}
                        variant="contained"
                        autoFocus
                    >
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
                            <Grid item xs={3}>
                                <Button
                                    style={{ marginTop: '23px', marginBottom: '40px' }}
                                    variant="contained"
                                    color="primary"
                                    onClick={() => openCartonDialog(addToCartonNumber)}
                                >
                                    Add/Remove
                                </Button>
                            </Grid>
                            <Grid item xs={2} />
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
            <Dialog
                open={cartonDialogContainerNumber > 0}
                onClose={() => setCartonDialogContainerNumber(0)}
                fullWidth
                maxWidth="xl"
            >
                <DialogTitle id="alert-dialog-title">
                    Add/Remove items on carton {cartonDialogContainerNumber}
                </DialogTitle>
                <DialogContent>
                    <>
                        <Grid container justifyContent="center">
                            <Grid item xs={5}>
                                <div style={{ height: 500, width: '100%' }}>
                                    <DataGrid
                                        rows={itemsData.filter(
                                            a =>
                                                a.containerNumber === cartonDialogContainerNumber &&
                                                a.itemType === 'I'
                                        )}
                                        columns={cartonDialogColumns}
                                        density="compact"
                                        rowHeight={34}
                                        checkboxSelection
                                        hideFooter
                                        onSelectionModelChange={handleCartonDialogSelectCartonRow}
                                    />
                                </div>
                            </Grid>
                            <Grid item xs={1}>
                                <Button
                                    onClick={() => removeAllCartonItems()}
                                    variant="contained"
                                    autoFocus
                                    fullWidth
                                >
                                    &gt;&gt;
                                </Button>
                                <Button
                                    style={{ marginTop: '20px', marginBottom: '20px' }}
                                    onClick={() => removeSelectedCartonItems()}
                                    variant="contained"
                                    autoFocus
                                    fullWidth
                                >
                                    -&gt;
                                </Button>
                                <Button
                                    onClick={() => addSelectedLooseItemsToCarton()}
                                    variant="contained"
                                    autoFocus
                                    fullWidth
                                >
                                    &lt;-
                                </Button>
                            </Grid>
                            <Grid item xs={5}>
                                <div style={{ height: 500, width: '100%' }}>
                                    <DataGrid
                                        rows={itemsData.filter(a => okToCartonise(a))}
                                        columns={cartonDialogColumns}
                                        density="compact"
                                        rowHeight={34}
                                        checkboxSelection
                                        hideFooter
                                        onSelectionModelChange={handleDialogSelectItemRow}
                                    />
                                </div>
                            </Grid>
                            <Grid item xs={1} />
                        </Grid>
                    </>
                </DialogContent>
                <DialogActions>
                    <Button
                        onClick={() => setCartonDialogContainerNumber(0)}
                        variant="contained"
                        autoFocus
                    >
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}

DetailsItemsTab.propTypes = {
    consignment: PropTypes.shape({
        consignmentId: PropTypes.number,
        customerName: PropTypes.string,
        salesAccountId: PropTypes.number,
        shippingMethod: PropTypes.string,
        addressId: PropTypes.number,
        dateOpened: PropTypes.string,
        dateClosed: PropTypes.string,
        carrier: PropTypes.string,
        terms: PropTypes.string,
        status: PropTypes.string,
        hubId: PropTypes.number,
        customsEntryCodePrefix: PropTypes.string,
        customsEntryCode: PropTypes.string,
        customsEntryCodeDate: PropTypes.string,
        despatchLocationCode: PropTypes.string,
        pallets: PropTypes.arrayOf(PropTypes.shape({})),
        closedBy: PropTypes.shape({ id: PropTypes.number, fullName: PropTypes.string }),
        address: PropTypes.shape({ id: PropTypes.number, displayAddress: PropTypes.string })
    }),
    updateField: PropTypes.func.isRequired,
    viewMode: PropTypes.bool.isRequired,
    editStatus: PropTypes.string.isRequired,
    hub: PropTypes.shape({ hubId: PropTypes.number, description: PropTypes.string }),
    hubs: PropTypes.arrayOf(
        PropTypes.shape({ hubId: PropTypes.number, description: PropTypes.string })
    ),
    hubsLoading: PropTypes.bool,
    carrier: PropTypes.shape({ carrierCode: PropTypes.string, name: PropTypes.string }),
    carriers: PropTypes.arrayOf(
        PropTypes.shape({ carrierCode: PropTypes.string, name: PropTypes.string })
    ),
    carriersLoading: PropTypes.bool,
    shippingTerm: PropTypes.shape({ code: PropTypes.string, description: PropTypes.string }),
    shippingTerms: PropTypes.arrayOf(
        PropTypes.shape({ code: PropTypes.string, description: PropTypes.string })
    ),
    shippingTermsLoading: PropTypes.bool,
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

DetailsItemsTab.defaultProps = {
    consignment: {},
    hub: null,
    hubs: [],
    hubsLoading: false,
    carrier: null,
    carriers: [],
    carriersLoading: false,
    shippingTerm: null,
    shippingTerms: [],
    shippingTermsLoading: false
};

export default DetailsItemsTab;
