import React, { useState, useEffect, useReducer } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Loading,
    Dropdown,
    SaveBackCancelButtons,
    utilities,
    ErrorCard,
    InputField,
    Typeahead
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Tooltip from '@material-ui/core/Tooltip';
import Typography from '@material-ui/core/Typography';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';
import { makeStyles } from '@material-ui/core/styles';
import Page from '../../containers/Page';
import consignmentReducer from './consignmentReducer';
import DetailsTab from './DetailsTab';
import DetailsItemsTab from './DetailsItemsTab';
import ItemsTab from './ItemsTab';
import InvoicesTab from './InvoicesTab';
import PackingListTab from './PackingListTab';

function Consignment({
    item,
    loading,
    requestErrors,
    openConsignments,
    getConsignment,
    optionsLoading,
    startingTab,
    editStatus,
    setEditStatus,
    hub,
    getHub,
    clearHub,
    hubs,
    hubsLoading,
    carrier,
    getCarrier,
    carriers,
    carriersLoading,
    updateItem,
    shippingTerm,
    getShippingTerm,
    clearShippingTerm,
    shippingTerms,
    shippingTermsLoading,
    itemError,
    clearConsignmentErrors,
    cartonTypes,
    userNumber,
    printConsignmentLabel,
    printConsignmentLabelWorking,
    printConsignmentLabelResult,
    clearConsignmentLabelData,
    printDocuments,
    printDocumentsWorking,
    printDocumentsResult,
    printDocumentsClearData,
    consignmentPackingList,
    consignmentPackingListLoading,
    getConsignmentPackingList,
    clearConsignmentPackingList,
    createConsignment,
    addConsignment,
    searchCartonTypes,
    clearCartonTypesSearch,
    cartonTypesSearchResults,
    cartonTypesSearchLoading,
    saveDocuments,
    saveDocumentsWorking,
    saveDocumentsResult,
    saveDocumentsClearData
}) {
    const [currentTab, setcurrentTab] = useState(startingTab);
    const [editablePallets, setEditablePallets] = useState([]);
    const [consignmentIdSelect, setConsignmentIdSelect] = useState(null);
    const [editableItems, setEditableItems] = useState([]);
    const [saveDisabled, setSaveDisabled] = useState(false);
    const [showCartonLabel, setShowCartonLabel] = useState(false);
    const [showNewCartonDialog, setShowNewCartonDialog] = useState(false);
    const [cartonLabelOptions, setCartonLabelOptions] = useState({
        numberOfCopies: 1,
        firstItem: 1,
        lastItem: 1
    });
    const [showPalletLabel, setShowPalletLabel] = useState(false);
    const [palletLabelOptions, setPalletLabelOptions] = useState({
        numberOfCopies: 1,
        firstItem: 1,
        lastItem: 1
    });
    const [state, dispatch] = useReducer(consignmentReducer, {
        consignment: null,
        originalConsignment: null
    });
    const [newCarton, setNewCarton] = useState({});

    const getItemTypeDisplay = itemType => {
        switch (itemType) {
            case 'I':
                return 'Loose Item';
            case 'S':
                return 'Sealed Box';
            case 'C':
                return 'Open Carton';
            default:
                return itemType;
        }
    };

    useEffect(() => {
        const loadPackingList = () => {
            clearConsignmentPackingList();
            if (item) {
                getConsignmentPackingList(item.consignmentId, 'packing-list');
            }
        };

        if (editStatus === 'create') {
            dispatch({
                type: 'create',
                payload: null
            });
        } else {
            dispatch({
                type: 'initialise',
                payload: item
            });
        }
        setEditablePallets(
            item?.pallets
                ? utilities
                      .sortEntityList(item?.pallets, 'palletNumber')
                      ?.map(p => ({ ...p, id: p.palletNumber, addToPallet: p.palletNumber }))
                : []
        );

        setEditableItems(
            item?.items
                ? utilities.sortEntityList(item?.items, 'itemNumber')?.map(p => ({
                      ...p,
                      id: p.itemNumber,
                      itemTypeDisplay: getItemTypeDisplay(p.itemType)
                  }))
                : []
        );

        clearConsignmentErrors();
        loadPackingList();
    }, [
        item,
        clearConsignmentErrors,
        clearConsignmentPackingList,
        getConsignmentPackingList,
        editStatus
    ]);

    useEffect(() => {
        if (item) {
            const hubHref = utilities.getHref(item, 'hub');
            if (hubHref) {
                getHub(hubHref);
            } else {
                clearHub();
            }

            const carrierHref = utilities.getHref(item, 'carrier');
            if (carrierHref) {
                getCarrier(carrierHref);
            }

            const shippingTermHref = utilities.getHref(item, 'shipping-term');
            if (shippingTermHref) {
                getShippingTerm(shippingTermHref);
            } else {
                clearShippingTerm();
            }
        }
    }, [item, getHub, getCarrier, getShippingTerm, clearHub, clearShippingTerm]);

    const useStyles = makeStyles(() => ({
        pullRight: {
            float: 'right'
        }
    }));
    const classes = useStyles();

    const viewing = () => {
        return editStatus === 'view';
    };

    const editing = () => {
        return editStatus === 'edit';
    };

    const creating = () => {
        return editStatus === 'create';
    };

    const viewMode = (createOnly = false) => {
        if (viewing() || (editing() && createOnly)) {
            return true;
        }

        return false;
    };

    const updateField = (fieldName, newValue) => {
        dispatch({
            type: 'updateField',
            fieldName,
            payload: newValue
        });
    };

    const handleSelectConsignment = (_property, newValue) => {
        getConsignment(newValue);
        setcurrentTab(3);
    };

    const handleTabChange = (_event, newValue) => {
        setcurrentTab(newValue);
    };

    const startEdit = () => {
        setEditStatus('edit');
    };

    const handleCreate = () => {
        createConsignment();
        setcurrentTab(1);
    };

    const closeConsignment = () => {
        if (viewing()) {
            updateItem(item.consignmentId, { status: 'C', closedById: userNumber });
        }
    };

    const doSave = () => {
        if (editing()) {
            updateItem(item.consignmentId, state.consignment);
        }

        if (creating()) {
            addConsignment(state.consignment);
        }
    };

    const doCancel = () => {
        dispatch({
            type: 'reset',
            payload: null
        });

        setEditablePallets(
            item?.pallets
                ? utilities
                      .sortEntityList(item?.pallets, 'palletNumber')
                      ?.map(p => ({ ...p, id: p.palletNumber }))
                : []
        );

        setEditableItems(
            item?.items
                ? utilities.sortEntityList(item?.items, 'itemNumber')?.map(p => ({
                      ...p,
                      id: p.itemNumber,
                      itemTypeDisplay: getItemTypeDisplay(p.itemType)
                  }))
                : []
        );

        setEditStatus('view');
        clearConsignmentErrors();
    };

    const getMaxCarton = () => {
        let maxContainer = 0;
        const containerItems = editableItems?.filter(a => a.containerNumber);
        if (containerItems && containerItems.length > 0) {
            const containerNumbers = containerItems.map(a => a.containerNumber);
            maxContainer = Math.max(...containerNumbers);
        }

        return maxContainer;
    };

    const getMaxPalletNumber = () => {
        let maxPalletNo = 0;
        if (editablePallets && editablePallets.length > 0) {
            const palletNumbers = editablePallets.map(a => a.palletNumber);
            maxPalletNo = Math.max(...palletNumbers);
        }

        return maxPalletNo;
    };

    const showCartonLabelForm = () => {
        clearConsignmentLabelData();

        const maxCarton = getMaxCarton();

        setCartonLabelOptions({
            ...cartonLabelOptions,
            firstItem: maxCarton,
            lastItem: maxCarton
        });

        setShowCartonLabel(true);
    };

    const doPrintCartonLabel = () => {
        printConsignmentLabel({
            labelType: 'Carton',
            consignmentId: item.consignmentId,
            firstItem: cartonLabelOptions.firstItem,
            lastItem: cartonLabelOptions.lastItem,
            numberOfCopies: cartonLabelOptions.numberOfCopies,
            userNumber
        });
    };

    const updateCartonLabelOptions = (itemName, value) => {
        setCartonLabelOptions({ ...cartonLabelOptions, [itemName]: value });
    };

    const updatePalletLabelOptions = (itemName, value) => {
        setPalletLabelOptions({ ...palletLabelOptions, [itemName]: value });
    };

    const getMaxItemNumber = () => {
        let maxItem = 0;
        if (editableItems && editableItems.length > 0) {
            const itemNumbers = editableItems.map(a => a.itemNumber);
            maxItem = Math.max(...itemNumbers);
        }

        return maxItem;
    };

    const showPalletLabelForm = () => {
        clearConsignmentLabelData();
        const maxPallet = getMaxPalletNumber();

        setPalletLabelOptions({
            ...palletLabelOptions,
            firstItem: maxPallet,
            lastItem: maxPallet
        });

        setShowPalletLabel(true);
    };

    const doPrintPalletLabel = () => {
        printConsignmentLabel({
            labelType: 'Pallet',
            consignmentId: item.consignmentId,
            firstItem: palletLabelOptions.firstItem,
            lastItem: palletLabelOptions.lastItem,
            numberOfCopies: palletLabelOptions.numberOfCopies,
            userNumber
        });
    };

    const addPallet = () => {
        const pallets = editablePallets.slice();
        const maxPallet = getMaxPalletNumber();

        pallets.push({
            palletNumber: maxPallet + 1,
            id: maxPallet + 1,
            weight: 18,
            consignmentId: state.consignment.consignmentId,
            height: 10,
            width: 120,
            depth: 100
        });

        setEditablePallets(pallets);
    };

    const showAddNewCarton = () => {
        const maxCarton = getMaxCarton();
        const maxItem = getMaxItemNumber();

        setNewCarton({
            itemDescription: 'SUNDRIES',
            itemType: 'C',
            itemTypeDisplay: 'Open Carton',
            quantity: 1,
            itemNumber: maxItem ? maxItem + 1 : 1,
            containerNumber: maxCarton ? maxCarton + 1 : 1
        });

        setShowNewCartonDialog(true);
    };

    const addNewCarton = () => {
        const items = editableItems.slice();

        items.push(newCarton);

        setEditableItems(items);

        setShowNewCartonDialog(false);
    };

    const updateNewCartonField = (propertyName, newValue) => {
        setNewCarton({ ...newCarton, [propertyName]: newValue });

        if (propertyName === 'containerType') {
            const selectedCarton = cartonTypes.find(
                a => a.cartonTypeName === newValue.toUpperCase()
            );
            if (selectedCarton) {
                setNewCarton({
                    ...newCarton,
                    containerType: selectedCarton.cartonTypeName,
                    height: selectedCarton.height,
                    width: selectedCarton.width,
                    depth: selectedCarton.depth
                });
            }
        }
    };

    const addItem = () => {
        const maxItem = getMaxItemNumber();
        const items = editableItems.slice();

        items.push({
            id: maxItem + 1,
            itemNumber: maxItem + 1,
            consignmentId: state.consignment.consignmentId,
            itemTypeDisplay: getItemTypeDisplay('I'),
            itemType: 'I'
        });

        setEditableItems(items);
    };

    const handlePrintDocuments = () => {
        printDocumentsClearData();
        printDocuments({ consignmentId: item.consignmentId, userNumber });
    };

    const handleSaveDocuments = () => {
        saveDocumentsClearData();
        saveDocuments({ consignmentId: item.consignmentId, userNumber });
    };

    const cartonTypesResult = () => {
        return cartonTypesSearchResults?.map(cartonType => ({
            ...cartonType,
            name: cartonType.cartonTypeName,
            description: cartonType.description,
            id: cartonType.cartonTypeName
        }));
    };

    const handleOnSelect = selectedCartonType => {
        updateNewCartonField('containerType', selectedCartonType.cartonTypeName);
    };

    return (
        <div className="pageContainer">
            <Page requestErrors={requestErrors} showRequestErrors width="xl">
                <Grid container spacing={3}>
                    <Grid item xs={2} className="hide-when-printing">
                        <Typography variant="h6">Consignment</Typography>
                    </Grid>
                    <Grid item xs={7} className="hide-when-printing">
                        {state.consignment && (
                            <Typography variant="h6">
                                {state.consignment.consignmentId} {state.consignment.customerName}
                            </Typography>
                        )}
                    </Grid>
                    <Grid item xs={3} className="hide-when-printing">
                        <Tooltip title="Close Consignment">
                            <span>
                                <Button
                                    variant="outlined"
                                    className={classes.pullRight}
                                    onClick={closeConsignment}
                                    disabled={
                                        !viewing() ||
                                        !state.consignment ||
                                        state.consignment.status === 'C'
                                    }
                                >
                                    Close Consignment
                                </Button>
                            </span>
                        </Tooltip>
                    </Grid>
                    {itemError && (
                        <Grid item xs={12}>
                            <ErrorCard
                                errorMessage={
                                    itemError?.details?.errors?.[0] || itemError.statusText
                                }
                            />
                        </Grid>
                    )}
                    <>
                        <Tabs
                            className="hide-when-printing"
                            value={currentTab}
                            onChange={handleTabChange}
                            style={{ paddingBottom: '20px' }}
                        >
                            <Tab label="Select" />
                            <Tab label="Details" />
                            <Tab label="Items" />
                            <Tab label="Details And Items" />
                            <Tab label="Documents" />
                            <Tab label="Packing List" />
                        </Tabs>
                        {currentTab === 0 && (
                            <>
                                <Grid item xs={10}>
                                    <Dropdown
                                        label="Select open consignment"
                                        propertyName="consignmentSelect"
                                        items={openConsignments}
                                        onChange={handleSelectConsignment}
                                        optionsLoading={optionsLoading}
                                    />
                                </Grid>
                                <Grid>
                                    <Tooltip title="Create Consignment">
                                        <span>
                                            <Button
                                                variant="outlined"
                                                color="primary"
                                                className={classes.pullRight}
                                                onClick={handleCreate}
                                                disabled={creating() || editing()}
                                            >
                                                Create Consignment
                                            </Button>
                                        </span>
                                    </Tooltip>
                                </Grid>
                                <Grid item xs={12}>
                                    <InputField
                                        label="Select Consignment By Id"
                                        placeholder="Consignment Id"
                                        propertyName="consignmentIdSelect"
                                        value={consignmentIdSelect}
                                        onChange={(_, val) => setConsignmentIdSelect(val)}
                                    />
                                    <Button
                                        style={{ marginTop: '10px' }}
                                        variant="outlined"
                                        color="primary"
                                        onClick={() =>
                                            handleSelectConsignment(null, consignmentIdSelect)
                                        }
                                    >
                                        Show Consignment
                                    </Button>
                                </Grid>
                            </>
                        )}
                        {currentTab !== 0 && (loading || !state.consignment) ? (
                            <Loading />
                        ) : (
                            <>
                                {currentTab === 1 && (
                                    <DetailsTab
                                        consignment={state.consignment}
                                        hub={hub}
                                        hubs={hubs}
                                        updateField={updateField}
                                        viewMode={viewMode()}
                                        editStatus={editStatus}
                                        hubsLoading={hubsLoading}
                                        carrier={carrier}
                                        carriers={carriers}
                                        carriersLoading={carriersLoading}
                                        shippingTerm={shippingTerm}
                                        shippingTerms={shippingTerms}
                                        shippingTermsLoading={shippingTermsLoading}
                                    />
                                )}
                                {currentTab === 2 && (
                                    <ItemsTab
                                        editableItems={editableItems}
                                        editablePallets={editablePallets}
                                        dispatch={dispatch}
                                        setSaveDisabled={setSaveDisabled}
                                        cartonTypes={cartonTypes}
                                        setEditStatus={setEditStatus}
                                        viewing={viewing()}
                                    />
                                )}
                                {currentTab === 3 && (
                                    <DetailsItemsTab
                                        consignment={state.consignment}
                                        hub={hub}
                                        hubs={hubs}
                                        updateField={updateField}
                                        viewMode={viewMode()}
                                        editStatus={editStatus}
                                        hubsLoading={hubsLoading}
                                        carrier={carrier}
                                        carriers={carriers}
                                        carriersLoading={carriersLoading}
                                        shippingTerm={shippingTerm}
                                        shippingTerms={shippingTerms}
                                        shippingTermsLoading={shippingTermsLoading}
                                        editableItems={editableItems}
                                        editablePallets={editablePallets}
                                        dispatch={dispatch}
                                        setSaveDisabled={setSaveDisabled}
                                        cartonTypes={cartonTypes}
                                        setEditStatus={setEditStatus}
                                        viewing={viewing()}
                                    />
                                )}
                                {currentTab === 4 && (
                                    <InvoicesTab
                                        invoices={state.consignment.invoices}
                                        exportBooks={state.consignment.exportBooks}
                                        printDocuments={handlePrintDocuments}
                                        printDocumentsWorking={printDocumentsWorking}
                                        printDocumentsResult={printDocumentsResult}
                                        saveDocuments={handleSaveDocuments}
                                        saveDocumentsWorking={saveDocumentsWorking}
                                        saveDocumentsResult={saveDocumentsResult}
                                    />
                                )}
                                {currentTab === 5 && (
                                    <PackingListTab
                                        consignmentPackingList={consignmentPackingList}
                                        consignmentPackingListLoading={
                                            consignmentPackingListLoading
                                        }
                                    />
                                )}
                            </>
                        )}
                    </>
                    <Grid item xs={12} style={{ marginTop: '20px' }}>
                        {currentTab === 2 && (
                            <>
                                <Button
                                    variant="outlined"
                                    color="primary"
                                    onClick={addPallet}
                                    disabled={viewing()}
                                >
                                    Add Pallet
                                </Button>
                                <Button
                                    variant="outlined"
                                    color="primary"
                                    onClick={showAddNewCarton}
                                    disabled={viewing()}
                                >
                                    Add Carton
                                </Button>
                                <Button
                                    variant="outlined"
                                    color="primary"
                                    onClick={addItem}
                                    disabled={viewing()}
                                >
                                    Add Item
                                </Button>
                                <Button
                                    variant="outlined"
                                    color="primary"
                                    onClick={showCartonLabelForm}
                                    disabled={!viewMode()}
                                >
                                    Carton Label
                                </Button>
                                <Button
                                    variant="outlined"
                                    color="primary"
                                    onClick={showPalletLabelForm}
                                    disabled={!viewMode()}
                                >
                                    Pallet Label
                                </Button>
                            </>
                        )}
                        {editStatus === 'view' ? (
                            <Button
                                variant="outlined"
                                color="primary"
                                className={`${classes.pullRight} hide-when-printing`}
                                onClick={startEdit}
                                disabled={!state.consignment || state.consignment.status === 'C'}
                            >
                                Edit
                            </Button>
                        ) : (
                            <SaveBackCancelButtons
                                saveClick={doSave}
                                backClick={() => {}}
                                cancelClick={doCancel}
                                saveDisabled={saveDisabled}
                            />
                        )}
                    </Grid>
                </Grid>
                <Dialog
                    open={showCartonLabel}
                    onClose={() => setShowCartonLabel(false)}
                    aria-labelledby="alert-dialog-title"
                    aria-describedby="alert-dialog-description"
                    fullWidth
                    maxWidth="sm"
                >
                    <DialogTitle id="alert-dialog-title">Print Carton Label</DialogTitle>
                    <DialogContent>
                        <>
                            <Grid container>
                                <Grid item xs={6}>
                                    <InputField
                                        label="First Carton"
                                        placeholder="First Carton"
                                        propertyName="firstItem"
                                        value={cartonLabelOptions.firstItem}
                                        onChange={updateCartonLabelOptions}
                                        maxLength={3}
                                    />
                                    <InputField
                                        label="Last Carton"
                                        placeholder="Last Carton"
                                        propertyName="lastItem"
                                        value={cartonLabelOptions.lastItem}
                                        onChange={updateCartonLabelOptions}
                                        maxLength={3}
                                    />
                                    <InputField
                                        label="Copies"
                                        placeholder="Copies To Print"
                                        propertyName="numberOfCopies"
                                        value={cartonLabelOptions.numberOfCopies}
                                        onChange={updateCartonLabelOptions}
                                        maxLength={3}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <Button
                                        style={{ marginTop: '30px', marginBottom: '40px' }}
                                        onClick={doPrintCartonLabel}
                                        variant="contained"
                                        color="primary"
                                    >
                                        Print Carton Label
                                    </Button>
                                    {printConsignmentLabelWorking ? (
                                        <Loading />
                                    ) : (
                                        <Typography variant="h6">
                                            {printConsignmentLabelResult?.message}
                                        </Typography>
                                    )}
                                </Grid>
                            </Grid>
                        </>
                    </DialogContent>
                    <DialogActions>
                        <Button
                            onClick={() => setShowCartonLabel(false)}
                            variant="contained"
                            autoFocus
                        >
                            Close
                        </Button>
                    </DialogActions>
                </Dialog>
                <Dialog
                    open={showPalletLabel}
                    onClose={() => setShowPalletLabel(false)}
                    aria-labelledby="alert-dialog-title"
                    aria-describedby="alert-dialog-description"
                    fullWidth
                    maxWidth="sm"
                >
                    <DialogTitle id="alert-dialog-title">Print Pallet Label</DialogTitle>
                    <DialogContent>
                        <>
                            <Grid container>
                                <Grid item xs={6}>
                                    <InputField
                                        label="First Pallet"
                                        placeholder="First Pallet"
                                        propertyName="firstItem"
                                        value={palletLabelOptions.firstItem}
                                        onChange={updatePalletLabelOptions}
                                        maxLength={3}
                                    />
                                    <InputField
                                        label="Last Pallet"
                                        placeholder="Last Pallet"
                                        propertyName="lastItem"
                                        value={palletLabelOptions.lastItem}
                                        onChange={updatePalletLabelOptions}
                                        maxLength={3}
                                    />
                                    <InputField
                                        label="Copies"
                                        placeholder="Copies To Print"
                                        propertyName="numberOfCopies"
                                        value={palletLabelOptions.numberOfCopies}
                                        onChange={updatePalletLabelOptions}
                                        maxLength={3}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <Button
                                        style={{ marginTop: '30px', marginBottom: '40px' }}
                                        onClick={doPrintPalletLabel}
                                        variant="contained"
                                        color="primary"
                                    >
                                        Print Pallet Label
                                    </Button>
                                    {printConsignmentLabelWorking ? (
                                        <Loading />
                                    ) : (
                                        <Typography variant="h6">
                                            {printConsignmentLabelResult?.message}
                                        </Typography>
                                    )}
                                </Grid>
                            </Grid>
                        </>
                    </DialogContent>
                    <DialogActions>
                        <Button
                            onClick={() => setShowPalletLabel(false)}
                            variant="contained"
                            autoFocus
                        >
                            Close
                        </Button>
                    </DialogActions>
                </Dialog>
                <Dialog
                    open={showNewCartonDialog}
                    onClose={() => setShowNewCartonDialog(false)}
                    aria-labelledby="alert-dialog-title"
                    aria-describedby="alert-dialog-description"
                    fullWidth
                    maxWidth="sm"
                >
                    <DialogTitle id="alert-dialog-title">Add New Carton</DialogTitle>
                    <DialogContent>
                        <>
                            <Grid container>
                                <Grid item xs={6}>
                                    <Dropdown
                                        label="Item Type"
                                        propertyName="itemTypeDisplay"
                                        value={newCarton.itemTypeDisplay}
                                        items={[
                                            { id: 'Loose Item', displayText: 'Loose Item' },
                                            { id: 'Open Carton', displayText: 'Open Carton' },
                                            { id: 'Sealed Box', displayText: 'Sealed Box' }
                                        ]}
                                        onChange={updateNewCartonField}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <InputField
                                        label="Carton Number"
                                        placeholder="Carton Number"
                                        propertyName="containerNumber"
                                        value={newCarton.containerNumber}
                                        onChange={updateNewCartonField}
                                        maxLength={2}
                                    />
                                </Grid>
                                <Grid item xs={8}>
                                    <InputField
                                        label="Carton Type"
                                        placeholder="Carton Type"
                                        propertyName="containerType"
                                        value={newCarton.containerType}
                                        onChange={updateNewCartonField}
                                    />
                                    <Typeahead
                                        items={cartonTypesResult()}
                                        fetchItems={searchCartonTypes}
                                        clearSearch={clearCartonTypesSearch}
                                        loading={cartonTypesSearchLoading}
                                        debounce={1000}
                                        links={false}
                                        modal
                                        searchButtonOnly
                                        onSelect={p => handleOnSelect(p)}
                                        label="Search For Carton Type"
                                    />
                                </Grid>
                                <Grid item xs={4} />
                                <Grid item xs={8}>
                                    <InputField
                                        label="Description"
                                        placeholder="Description"
                                        fullWidth
                                        propertyName="itemDescription"
                                        value={newCarton.itemDescription}
                                        onChange={updateNewCartonField}
                                    />
                                </Grid>
                                <Grid item xs={4} />
                                <Grid item xs={6}>
                                    <InputField
                                        label="Quantity"
                                        placeholder="Quantity"
                                        propertyName="quantity"
                                        value={newCarton.quantity}
                                        onChange={updateNewCartonField}
                                        maxLength={4}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <InputField
                                        label="Weight"
                                        placeholder="Weight"
                                        propertyName="weight"
                                        value={newCarton.weight}
                                        onChange={updateNewCartonField}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        label="Height"
                                        placeholder="Height"
                                        propertyName="height"
                                        value={newCarton.height}
                                        onChange={updateNewCartonField}
                                        maxLength={4}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        label="Depth"
                                        placeholder="Depth"
                                        propertyName="depth"
                                        value={newCarton.depth}
                                        onChange={updateNewCartonField}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        label="Width"
                                        placeholder="Width"
                                        propertyName="width"
                                        value={newCarton.width}
                                        onChange={updateNewCartonField}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <Button
                                        style={{ marginTop: '30px', marginBottom: '40px' }}
                                        onClick={addNewCarton}
                                        variant="contained"
                                        color="primary"
                                    >
                                        Add Carton
                                    </Button>
                                </Grid>
                            </Grid>
                        </>
                    </DialogContent>
                    <DialogActions>
                        <Button
                            onClick={() => setShowNewCartonDialog(false)}
                            variant="contained"
                            autoFocus
                        >
                            Close
                        </Button>
                    </DialogActions>
                </Dialog>
            </Page>
        </div>
    );
}

Consignment.propTypes = {
    item: PropTypes.shape({
        consignmentId: PropTypes.number,
        customerName: PropTypes.string,
        salesAccountId: PropTypes.number,
        shippingMethod: PropTypes.string,
        dateOpened: PropTypes.string,
        dateClosed: PropTypes.string,
        carrier: PropTypes.string,
        terms: PropTypes.string,
        hubId: PropTypes.number,
        pallets: PropTypes.arrayOf(PropTypes.shape({})),
        items: PropTypes.arrayOf(PropTypes.shape({})),
        closedBy: PropTypes.shape({ id: PropTypes.number, fullName: PropTypes.string }),
        address: PropTypes.shape({ id: PropTypes.number, displayAddress: PropTypes.string })
    }),
    loading: PropTypes.bool,
    requestErrors: PropTypes.arrayOf(
        PropTypes.shape({ message: PropTypes.string, name: PropTypes.string })
    ),
    openConsignments: PropTypes.arrayOf(
        PropTypes.shape({ id: PropTypes.number, displayText: PropTypes.string })
    ),
    getConsignment: PropTypes.func.isRequired,
    optionsLoading: PropTypes.bool,
    startingTab: PropTypes.number,
    editStatus: PropTypes.string,
    setEditStatus: PropTypes.func.isRequired,
    getHub: PropTypes.func.isRequired,
    clearHub: PropTypes.func.isRequired,
    hub: PropTypes.shape({ hubId: PropTypes.number, description: PropTypes.string }),
    hubs: PropTypes.arrayOf(
        PropTypes.shape({ hubId: PropTypes.number, description: PropTypes.string })
    ),
    hubsLoading: PropTypes.bool,
    getCarrier: PropTypes.func.isRequired,
    carrier: PropTypes.shape({ carrierCode: PropTypes.string, name: PropTypes.string }),
    carriers: PropTypes.arrayOf(
        PropTypes.shape({ carrierCode: PropTypes.string, name: PropTypes.string })
    ),
    carriersLoading: PropTypes.bool,
    updateItem: PropTypes.func.isRequired,
    getShippingTerm: PropTypes.func.isRequired,
    clearShippingTerm: PropTypes.func.isRequired,
    clearConsignmentErrors: PropTypes.func.isRequired,
    shippingTerm: PropTypes.shape({ code: PropTypes.string, description: PropTypes.string }),
    shippingTerms: PropTypes.arrayOf(
        PropTypes.shape({ code: PropTypes.string, description: PropTypes.string })
    ),
    shippingTermsLoading: PropTypes.bool,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        item: PropTypes.string,
        details: PropTypes.shape({
            errors: PropTypes.arrayOf(PropTypes.shape({}))
        })
    }),
    cartonTypes: PropTypes.arrayOf(
        PropTypes.shape({ cartonTypeName: PropTypes.string, description: PropTypes.string })
    ),
    userNumber: PropTypes.number.isRequired,
    printConsignmentLabelWorking: PropTypes.bool,
    printConsignmentLabel: PropTypes.func.isRequired,
    printConsignmentLabelResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    clearConsignmentLabelData: PropTypes.func.isRequired,
    printDocumentsWorking: PropTypes.bool,
    printDocuments: PropTypes.func.isRequired,
    printDocumentsResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    printDocumentsClearData: PropTypes.func.isRequired,
    consignmentPackingList: PropTypes.shape({}),
    consignmentPackingListLoading: PropTypes.bool,
    getConsignmentPackingList: PropTypes.func.isRequired,
    clearConsignmentPackingList: PropTypes.func.isRequired,
    createConsignment: PropTypes.func.isRequired,
    addConsignment: PropTypes.func.isRequired,
    searchCartonTypes: PropTypes.func.isRequired,
    clearCartonTypesSearch: PropTypes.func.isRequired,
    cartonTypesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    cartonTypesSearchLoading: PropTypes.bool,
    saveDocumentsWorking: PropTypes.bool,
    saveDocuments: PropTypes.func.isRequired,
    saveDocumentsResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    saveDocumentsClearData: PropTypes.func.isRequired
};

Consignment.defaultProps = {
    item: {},
    loading: false,
    requestErrors: null,
    openConsignments: [],
    optionsLoading: false,
    startingTab: 0,
    editStatus: 'view',
    hub: null,
    hubs: [],
    hubsLoading: false,
    carrier: null,
    carriers: [],
    carriersLoading: false,
    shippingTerm: null,
    shippingTerms: [],
    shippingTermsLoading: false,
    itemError: null,
    cartonTypes: [],
    printConsignmentLabelWorking: false,
    printConsignmentLabelResult: null,
    printDocumentsWorking: false,
    printDocumentsResult: null,
    consignmentPackingList: null,
    consignmentPackingListLoading: false,
    cartonTypesSearchResults: [],
    cartonTypesSearchLoading: false,
    saveDocumentsWorking: false,
    saveDocumentsResult: null
};

export default Consignment;
