import React, { useState, useEffect, useReducer } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Loading,
    Dropdown,
    SaveBackCancelButtons,
    utilities,
    ErrorCard
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Tooltip from '@material-ui/core/Tooltip';
import Typography from '@material-ui/core/Typography';
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
    clearConsignmentErrors
}) {
    const [currentTab, setcurrentTab] = useState(startingTab);
    const [editablePallets, setEditablePallets] = useState([]);
    const [editableItems, setEditableItems] = useState([]);
    const [saveDisabled, setSaveDisabled] = useState(false);

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

    const openConsignmentOptions = () => {
        return openConsignments?.map(c => ({
            id: c.consignmentId,
            displayText: c.customerName
        }));
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

    const closeConsignment = () => {};
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
                    <Tooltip title="Close Consignment - coming soon">
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
                                items={openConsignmentOptions()}
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
                                    viewing={viewing()}
                                    dispatch={dispatch}
                                    setSaveDisabled={setSaveDisabled}
                                />
                            )}
                        </>
                    )}
                </>
                <Grid item xs={12}>
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
    openConsignments: PropTypes.arrayOf(PropTypes.shape({})),
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
    })
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
    itemError: null
};

export default Consignment;
