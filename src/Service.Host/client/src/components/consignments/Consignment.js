import React, { useState, useEffect, useReducer } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Loading,
    Dropdown,
    SaveBackCancelButtons,
    utilities,
    ErrorCard,
    InputField
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
import ItemsTab from './ItemsTab';

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
    clearConsignmentLabelData
}) {
    const [currentTab, setcurrentTab] = useState(startingTab);
    const [editablePallets, setEditablePallets] = useState([]);
    const [editableItems, setEditableItems] = useState([]);
    const [saveDisabled, setSaveDisabled] = useState(false);
    const [showCartonLabel, setShowCartonLabel] = useState(false);
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
        dispatch({
            type: 'initialise',
            payload: item
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

        clearConsignmentErrors();
    }, [item, clearConsignmentErrors]);

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
        setcurrentTab(1);
    };

    const handleTabChange = (_event, newValue) => {
        setcurrentTab(newValue);
    };

    const startEdit = () => {
        setEditStatus('edit');
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

    const showCartonLabelForm = () => {
        clearConsignmentLabelData();

        let maxContainer = 1;
        const containerItems = state.consignment.items?.filter(a => a.containerNumber);
        if (containerItems && containerItems.length > 0) {
            const containerNumbers = containerItems.map(a => a.containerNumber);
            maxContainer = Math.max(...containerNumbers);
        }

        setCartonLabelOptions({
            ...cartonLabelOptions,
            firstItem: maxContainer,
            lastItem: maxContainer
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

    const showPalletLabelForm = () => {
        clearConsignmentLabelData();

        let maxPalletNumber = 1;
        const pallets = state.consignment.pallets?.filter(a => a.palletNumber);
        if (pallets && pallets.length > 0) {
            const palletNumbers = pallets.map(a => a.palletNumber);
            maxPalletNumber = Math.max(...palletNumbers);
        }

        setPalletLabelOptions({
            ...palletLabelOptions,
            firstItem: maxPalletNumber,
            lastItem: maxPalletNumber
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

    return (
        <Page requestErrors={requestErrors} showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={2}>
                    <Typography variant="h6">Consignment</Typography>
                </Grid>
                <Grid item xs={7}>
                    {state.consignment && (
                        <Typography variant="h6">
                            {state.consignment.consignmentId} {state.consignment.customerName}
                        </Typography>
                    )}
                </Grid>
                <Grid item xs={3}>
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
                            errorMessage={itemError?.details?.errors?.[0] || itemError.statusText}
                        />
                    </Grid>
                )}
                <>
                    <Tabs
                        value={currentTab}
                        onChange={handleTabChange}
                        style={{ paddingBottom: '20px' }}
                    >
                        <Tab label="Select" />
                        <Tab label="Details" />
                        <Tab label="Consignment Items" />
                    </Tabs>
                    {currentTab === 0 && (
                        <Grid item xs={12}>
                            <Dropdown
                                label="Select consignment"
                                propertyName="consignmentSelect"
                                items={openConsignments}
                                onChange={handleSelectConsignment}
                                optionsLoading={optionsLoading}
                            />
                        </Grid>
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
                                />
                            )}
                        </>
                    )}
                </>
                <Grid item xs={12}>
                    {currentTab === 2 && (
                        <>
                            <Button
                                variant="outlined"
                                color="primary"
                                onClick={showCartonLabelForm}
                            >
                                Carton Label
                            </Button>
                            <Button
                                variant="outlined"
                                color="primary"
                                onClick={showPalletLabelForm}
                            >
                                Pallet Label
                            </Button>
                        </>
                    )}
                    {editStatus === 'view' ? (
                        <Button
                            variant="outlined"
                            color="primary"
                            className={classes.pullRight}
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
                    <Button onClick={() => setShowCartonLabel(false)} variant="contained" autoFocus>
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
                    <Button onClick={() => setShowPalletLabel(false)} variant="contained" autoFocus>
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
        </Page>
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
    clearConsignmentLabelData: PropTypes.func.isRequired
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
    printConsignmentLabelResult: null
};

export default Consignment;
