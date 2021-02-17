import React, { useState, useEffect, useReducer } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    LinkButton
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';
import GeneralTab from '../../containers/parts/tabs/GeneralTab';
import BuildTab from '../../containers/parts/tabs/BuildTab';
import PurchTab from '../../containers/parts/tabs/PurchTab';
import StoresTab from '../../containers/parts/tabs/StoresTab';
import LifeCycleTab from './tabs/LifeCycleTab';

function Part({
    editStatus,
    itemError,
    history,
    itemId,
    item,
    loading,
    snackbarVisible,
    addItem,
    updateItem,
    setEditStatus,
    setSnackbarVisible,
    privileges,
    userName,
    userNumber,
    options,
    partTemplates,
    liveTest,
    fetchLiveTest,
    fetchParts,
    partsSearchResults
}) {
    const defaultPart = {
        partNumber: '',
        description: '',
        accountingCompany: 'LINN',
        psuPart: false,
        stockControlled: true,
        cccCriticalPart: false,
        safetyCriticalPart: false,
        paretoCode: 'U',
        createdBy: userNumber,
        createdByName: userName,
        dateCreated: new Date(),
        railMethod: 'POLICY',
        preferredSupplier: 4415,
        preferredSupplierName: 'Linn Products Ltd',
        qcInformation: '',
        qcOnReceipt: false,
        orderHold: false
    };
    const creating = () => editStatus === 'create';

    const initialState = {
        part: creating() ? defaultPart : null,
        prevPart: null
    };

    // all updates to state are now enacted by dispatching actions
    // and processing them with this reducer, just like redux does
    function partReducer(state, action) {
        switch (action.type) {
            case 'initialise':
                if (creating()) {
                    return { ...state, part: defaultPart, prevPart: action.payload };
                }
                return { ...state, part: action.payload, prevPart: action.payload };
            case 'fieldChange':
                if (action.fieldName === 'rawOrFinished') {
                    return {
                        ...state,
                        part: {
                            ...state.part,
                            rawOrFinished: 'F',
                            nominalAccount: 564,
                            nominal: '0000000417',
                            nominalDescription: 'TOTAL COST OF SALES',
                            department: '0000002106',
                            departmentDescription: 'GROSS PROFIT'
                        }
                    };
                }
                if (action.fieldName === 'nominalAccount') {
                    return {
                        ...state,
                        part: {
                            ...state.part,
                            nominalAccount: action.payload.id,
                            nominal: action.payload.values[0].value,
                            nominalDescription: action.payload.values[1].value,
                            department: action.payload.values[2].value,
                            departmentDescription: action.payload.values[3].value
                        }
                    };
                }
                if (action.fieldName === 'accountingCompany') {
                    const updated =
                        action.payload === 'RECORDS'
                            ? {
                                  ...state.part,
                                  accountingCompany: action.payload,
                                  paretoCode: 'R',
                                  bomType: 'C',
                                  linnProduced: 'N',
                                  qcOnReceipt: 'N'
                              }
                            : { ...state.part, accountingCompany: action.payload, paretoCode: 'U' };
                    return {
                        ...state,
                        part: updated
                    };
                }
                if (action.fieldName === 'sernosSequenceName') {
                    return {
                        ...state,
                        part: {
                            ...state.part,
                            sernosSequenceName: action.payload.name,
                            sernosSequenceDescription: action.payload.description
                        }
                    };
                }
                if (action.fieldName === 'linnProduced') {
                    const linnProduced = action.payload === 'Y';
                    if (linnProduced) {
                        return {
                            ...state,
                            part: {
                                ...state.part,
                                linnProduced,
                                sernosSequenceName: 'SERIAL 1',
                                sernosSequenceDescription: 'MASTER SERIAL NUMBER RECORDS.'
                            }
                        };
                    }
                    return {
                        ...state,
                        part: {
                            ...state.part,
                            linnProduced
                        }
                    };
                }
                if (action.fieldName === 'preferredSupplier') {
                    return {
                        ...state,
                        part: {
                            ...state.part,
                            preferredSupplier: action.payload.name,
                            preferredSupplierName: action.payload.description
                        }
                    };
                }
                if (action.fieldName === 'manufacturersPartNumber') {
                    return {
                        ...state,
                        part: {
                            ...state.part,
                            preferredSupplier: action.payload.name,
                            preferredSupplierName: action.payload.description
                        }
                    };
                }
                return {
                    ...state,
                    part: { ...state.part, [action.fieldName]: action.payload }
                };
            default:
                return state;
        }
    }

    const [state, dispatch] = useReducer(partReducer, initialState);

    // all sideEffects are now clearly outlined here

    // checking whether partNumber already exists
    useEffect(() => {
        if (editStatus === 'create') {
            if (state.part.partNumber.match(/\/[1-9]$/)) {
                //if new partNumber ends in /[1-9] then user is creating a new revision of existing part
                fetchParts(state.part.partNumber.split('/')[0]); // so fetch the existing parts for any crosschecking we need to do
            } else {
                fetchParts(state.part.partNumber); // else they are creating a new part entirely. Check to see if it already exists.
            }
        }
    }, [state.part, fetchParts, editStatus]);

    // checking whether part can be made live
    useEffect(() => {
        if (itemId) {
            fetchLiveTest(itemId);
        }
    }, [fetchLiveTest, itemId]);

    const viewing = () => editStatus === 'view';

    const [part, setPart] = useState(
        creating()
            ? {
                  partNumber: '',
                  description: '',
                  accountingCompany: 'LINN',
                  psuPart: false,
                  stockControlled: true,
                  cccCriticalPart: false,
                  safetyCriticalPart: false,
                  paretoCode: 'U',
                  createdBy: userNumber,
                  createdByName: userName,
                  dateCreated: new Date(),
                  railMethod: 'POLICY',
                  preferredSupplier: 4415,
                  preferredSupplierName: 'Linn Products Ltd',
                  qcInformation: '',
                  qcOnReceipt: false,
                  orderHold: false
              }
            : null
    );
    const [tab, setTab] = useState(0);

    const handleTabChange = (event, value) => {
        setTab(value);
    };

    const canPhaseOut = () => {
        if (!(privileges.length < 1)) {
            return privileges.some(priv => priv === 'part.admin');
        }
        return false;
    };

    useEffect(() => {
        if (item !== state.prevPart && editStatus !== 'create') {
            dispatch({ type: 'initialise', payload: item });
        }
        if (editStatus === 'create') {
            setPart(p => ({ ...p, bomId: null }));
        }
    }, [item, state.prevPart, editStatus, fetchLiveTest, itemId]);

    useEffect(() => {
        if (options?.template && partTemplates.length) {
            const template = partTemplates.find(t => t.partRoot === options.template);
            const formatNextNumber = () => {
                if (template.nextNumber < 1000) {
                    return template.nextNumber.toString().padStart(3, 0);
                }
                return template.nextNumber.toString();
            };
            setPart(p => ({
                ...p,
                description: template.description,
                partNumber:
                    template.hasNumberSequence === 'Y'
                        ? `${template.partRoot} ${formatNextNumber()}`
                        : template.partRoot,
                accountingCompany: template.accountingCompany,
                assemblyTechnologyName: template.assemblyTechnologyName,
                bomType: template.bomType,
                linnProduced: template.linnProduced,
                paretoCode: template.paretoCode,
                stockControlled: template.stockControlled
            }));
        }
    }, [options, partTemplates]);

    const partInvalid = () => !state.part?.partNumber || !state.part?.description;

    const getPartNumberHelperText = () => {
        if (partsSearchResults.some(p => p.partNumber === state.part?.partNumber?.toUpperCase())) {
            return 'PART NUMBER ALREADY EXISTS.';
        }
        return '';
    };

    const getSafetyCriticalHelperText = () => {
        const prevRevision = partsSearchResults.find(
            p => p.partNumber === state?.part?.partNumber.toUpperCase().split('/')[0]
        );

        if (prevRevision?.safetyCriticalPart === true) {
            return 'Note: Previous Revision Was Safety Critical';
        }

        if (prevRevision?.safetyCriticalPart === false) {
            return 'Note: Previous Revision Was NOT Safety Critical';
        }

        return '';
    };

    const handleSaveClick = () => {
        if (creating()) {
            addItem(state.part);
        } else {
            updateItem(itemId, state.part);
        }
        setEditStatus('view');
    };

    const handleCancelClick = () => {
        dispatch({ type: 'initialise', payload: item });
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/inventory/parts');
    };

    const handleFieldChange = (propertyName, newValue) => {
        setEditStatus('edit');
        dispatch({ type: 'fieldChange', fieldName: propertyName, payload: newValue });

        // if (viewing() && propertyName !== 'reasonPhasedOut') {
        //     setEditStatus('edit');
        // } // do this in an effect on reasonPhased out
    };

    const handleLinnProducedChange = (_, newValue) => {
        const linnProduced = newValue === 'Y';
        if (linnProduced) {
            setPart({
                ...part,
                linnProduced,
                sernosSequenceName: 'SERIAL 1',
                sernosSequenceDescription: 'MASTER SERIAL NUMBER RECORDS.'
            });
        } else {
            setPart({
                ...part,
                linnProduced
            });
        }
    };

    const handlePhaseOutClick = () => {
        updateItem(itemId, {
            ...state.part,
            datePhasedOut: new Date(),
            phasedOutBy: userNumber,
            phasedOutByName: userName
        });
    };

    const handleChangeLiveness = () => {
        if (!item.dateLive) {
            updateItem(itemId, {
                ...state.part,
                dateLive: new Date(),
                madeLiveBy: userNumber,
                madeLiveByName: userName
            });
        } else {
            updateItem(itemId, {
                ...state.part,
                dateLive: null,
                madeLiveBy: null,
                madeLiveByName: null
            });
        }
    };

    const handleProductAnalysisCodeChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPart({
            ...part,
            productAnalysisCode: newValue.name,
            productAnalysisCodeDescription: newValue.description
        });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={10}>
                    {creating() ? <Title text="Create Part" /> : <Title text="Part Details" />}
                </Grid>
                {creating() ? (
                    <Grid item xs={2} />
                ) : (
                    <Grid item xs={2}>
                        <LinkButton to="/inventory/parts/create" text="Copy" />
                    </Grid>
                )}
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard
                            errorMessage={itemError?.details?.errors?.[0] || itemError.statusText}
                        />
                    </Grid>
                )}
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    state.part &&
                    itemError?.status !== 404 && (
                        <>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            <Grid item xs={3}>
                                <InputField
                                    fullWidth
                                    disabled={!creating()}
                                    value={state.part?.partNumber}
                                    label="Part Number"
                                    maxLength={14}
                                    helperText={
                                        !creating()
                                            ? 'This field cannot be changed'
                                            : getPartNumberHelperText()
                                    }
                                    error={creating() && !!getPartNumberHelperText()}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="partNumber"
                                />
                            </Grid>
                            <Grid item xs={7}>
                                <InputField
                                    fullWidth
                                    value={state.part?.description}
                                    label="Description"
                                    maxLength={200}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="description"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                {!creating() &&
                                    item?.links.some(
                                        l => l.rel === 'mechanical-sourcing-sheet'
                                    ) && (
                                        <LinkButton
                                            text="Datasheets"
                                            to={`${
                                                item.links.find(
                                                    l => l.rel === 'mechanical-sourcing-sheet'
                                                ).href
                                            }?tab=dataSheets`}
                                        />
                                    )}
                            </Grid>
                            <Tabs
                                value={tab}
                                onChange={handleTabChange}
                                indicatorColor="primary"
                                textColor="primary"
                                style={{ paddingBottom: '40px' }}
                            >
                                <Tab label="General" />
                                <Tab label="Build" />
                                <Tab label="Purch" />
                                <Tab label="Stores" />
                                <Tab label="LifeCycle" />
                            </Tabs>
                            {tab === 0 && (
                                <GeneralTab
                                    accountingCompany={state.part.accountingCompany}
                                    handleFieldChange={handleFieldChange}
                                    productAnalysisCode={state.part.productAnalysisCode}
                                    productAnalysisCodeDescription={
                                        state.part.productAnalysisCodeDescription
                                    }
                                    handleProductAnalysisCodeChange={
                                        handleProductAnalysisCodeChange
                                    }
                                    rootProduct={state.part.rootProduct}
                                    department={state.part.department}
                                    departmentDescription={state.part.departmentDescription}
                                    paretoCode={state.part.paretoCode}
                                    nominal={state.part.nominal}
                                    nominalDescription={state.part.nominalDescription}
                                    stockControlled={state.part.stockControlled}
                                    safetyCriticalPart={state.part.safetyCriticalPart}
                                    safetyCriticalHelperText={
                                        creating() ? getSafetyCriticalHelperText() : null
                                    }
                                    performanceCriticalPart={state.part.performanceCriticalPart}
                                    emcCriticalPart={state.part.emcCriticalPart}
                                    singleSourcePart={state.part.singleSourcePart}
                                    cccCriticalPart={state.part.cccCriticalPart}
                                    psuPart={state.part.psuPart}
                                    safetyCertificateExpirationDate={
                                        state.part.safetyCertificateExpirationDate
                                    }
                                    safetyDataDirectory={state.part.safetyDataDirectory}
                                    rawOrFinished={state.part.rawOrFinished}
                                />
                            )}
                            {tab === 1 && (
                                <BuildTab
                                    handleFieldChange={handleFieldChange}
                                    linnProduced={state.part.linnProduced}
                                    sernosSequenceName={state.part.sernosSequenceName}
                                    sernosSequenceDescription={state.part.sernosSequenceDescription}
                                    decrementRuleName={state.part.decrementRuleName}
                                    assemblyTechnologyName={state.part.assemblyTechnologyName}
                                    bomType={state.part.bomType}
                                    bomId={state.part.bomId}
                                    optionSet={state.part.optionSet}
                                    drawingReference={state.part.drawingReference}
                                    safetyCriticalPart={state.part.safetyCriticalPart}
                                    plannedSurplus={state.part.plannedSurplus}
                                    handleLinnProducedChange={handleLinnProducedChange}
                                />
                            )}
                            {tab === 2 && (
                                <PurchTab
                                    handleFieldChange={handleFieldChange}
                                    ourUnitOfMeasure={state.part.ourUnitOfMeasure}
                                    preferredSupplier={state.part.preferredSupplier}
                                    preferredSupplierName={state.part.preferredSupplierName}
                                    currency={state.part.currency}
                                    currencyUnitPrice={state.part.currencyUnitPrice}
                                    baseUnitPrice={state.part.baseUnitPrice}
                                    materialPrice={state.part.materialPrice}
                                    labourPrice={state.part.labourPrice}
                                    costingPrice={state.part.costingPrice}
                                    orderHold={state.part.orderHold}
                                    partCategory={state.part.partCategory}
                                    nonForecastRequirement={state.part.nonForecastRequirement}
                                    oneOffRequirement={state.part.oneOffRequirement}
                                    sparesRequirement={state.part.sparesRequirement}
                                    ignoreWorkstationStock={state.part.ignoreWorkstationStock}
                                    imdsIdNumber={state.part.imdsIdNumber}
                                    imdsWeight={state.part.imdsWeight}
                                    mechanicalOrElectronic={state.part.mechanicalOrElectronic}
                                    manufacturers={state.part.manufacturers}
                                    links={item?.links}
                                />
                            )}
                            {tab === 3 && (
                                <StoresTab
                                    handleFieldChange={handleFieldChange}
                                    qcOnReceipt={state.part.qcOnReceipt}
                                    qcInformation={state.part.qcInformation}
                                    rawOrFinished={state.part.rawOrFinished}
                                    ourInspectionWeeks={state.part.ourInspectionWeeks}
                                    safetyWeeks={state.part.safetyWeeks}
                                    railMethod={state.part.railMethod}
                                    minStockrail={state.part.minStockrail}
                                    maxStockRail={state.part.maxStockRail}
                                    secondStageBoard={state.part.secondStageBoard}
                                    secondStageDescription={state.part.secondStageDescription}
                                    tqmsCategoryOverride={state.part.tqmsCategoryOverride}
                                    stockNotes={state.part.stockNotes}
                                />
                            )}
                            {tab === 4 && (
                                <LifeCycleTab
                                    handleFieldChange={handleFieldChange}
                                    handlePhaseOutClick={handlePhaseOutClick}
                                    editStatus={editStatus}
                                    canPhaseOut={canPhaseOut()}
                                    dateCreated={state.part.dateCreated}
                                    createdBy={state.part.createdBy}
                                    createdByName={state.part.createdByName}
                                    dateLive={state.part.dateLive}
                                    madeLiveBy={state.part.madeLiveBy}
                                    madeLiveByName={part.madeLiveByName}
                                    phasedOutBy={state.part.phasedOutBy}
                                    phasedOutByName={state.part.phasedOutByName}
                                    reasonPhasedOut={state.part.reasonPhasedOut}
                                    scrapOrConvert={state.part.scrapOrConvert}
                                    purchasingPhaseOutType={state.part.purchasingPhaseOutType}
                                    datePhasedOut={state.part.datePhasedOut}
                                    dateDesignObsolete={state.part.dateDesignObsolete}
                                    liveTest={liveTest}
                                    handleChangeLiveness={handleChangeLiveness}
                                />
                            )}
                            {/* 
                            {tab === 2 && (
                                <PurchTab
                                    handleFieldChange={handleFieldChange}
                                    ourUnitOfMeasure={part.ourUnitOfMeasure}
                                    preferredSupplier={part.preferredSupplier}
                                    handlePrefferedSupplierChange={handlePrefferedSupplierChange}
                                    preferredSupplierName={part.preferredSupplierName}
                                    currency={part.currency}
                                    currencyUnitPrice={part.currencyUnitPrice}
                                    baseUnitPrice={part.baseUnitPrice}
                                    materialPrice={part.materialPrice}
                                    labourPrice={part.labourPrice}
                                    costingPrice={part.costingPrice}
                                    orderHold={part.orderHold}
                                    partCategory={part.partCategory}
                                    nonForecastRequirement={part.nonForecastRequirement}
                                    oneOffRequirement={part.oneOffRequirement}
                                    sparesRequirement={part.sparesRequirement}
                                    ignoreWorkstationStock={part.ignoreWorkstationStock}
                                    handleIgnoreWorkstationStockChange={
                                        handleIgnoreWorkstationStockChange
                                    }
                                    imdsIdNumber={part.imdsIdNumber}
                                    imdsWeight={part.imdsWeight}
                                    mechanicalOrElectronic={part.mechanicalOrElectronic}
                                    manufacturers={part.manufacturers}
                                    handleManufacturersPartNumberChange={
                                        handleManufacturersPartNumberChange
                                    }
                                    links={item?.links}
                                />
                            )}
                            {tab === 3 && (
                                <StoresTab
                                    handleFieldChange={handleFieldChange}
                                    qcOnReceipt={part.qcOnReceipt}
                                    qcInformation={part.qcInformation}
                                    rawOrFinished={part.rawOrFinished}
                                    ourInspectionWeeks={part.ourInspectionWeeks}
                                    safetyWeeks={part.safetyWeeks}
                                    railMethod={part.railMethod}
                                    minStockrail={part.minStockrail}
                                    maxStockRail={part.maxStockRail}
                                    secondStageBoard={part.secondStageBoard}
                                    secondStageDescription={part.secondStageDescription}
                                    tqmsCategoryOverride={part.tqmsCategoryOverride}
                                    stockNotes={part.stockNotes}
                                />
                            )}
                            {tab === 4 && (
                                <LifeCycleTab
                                    handleFieldChange={handleFieldChange}
                                    handlePhaseOutClick={handlePhaseOutClick}
                                    editStatus={editStatus}
                                    canPhaseOut={canPhaseOut()}
                                    dateCreated={part.dateCreated}
                                    createdBy={part.createdBy}
                                    createdByName={part.createdByName}
                                    dateLive={part.dateLive}
                                    madeLiveBy={part.madeLiveBy}
                                    madeLiveByName={part.madeLiveByName}
                                    phasedOutBy={part.phasedOutBy}
                                    phasedOutByName={part.phasedOutByName}
                                    reasonPhasedOut={part.reasonPhasedOut}
                                    scrapOrConvert={part.scrapOrConvert}
                                    purchasingPhaseOutType={part.purchasingPhaseOutType}
                                    datePhasedOut={part.datePhasedOut}
                                    dateDesignObsolete={part.dateDesignObsolete}
                                    liveTest={liveTest}
                                    handleChangeLiveness={handleChangeLiveness}
                                />
                            )} */}
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing() || partInvalid()}
                                    saveClick={handleSaveClick}
                                    cancelClick={handleCancelClick}
                                    backClick={handleBackClick}
                                />
                            </Grid>
                        </>
                    )
                )}
            </Grid>
        </Page>
    );
}

Part.propTypes = {
    item: PropTypes.shape({
        part: PropTypes.string,
        description: PropTypes.string,
        nextSerialNumber: PropTypes.number,
        dateClosed: PropTypes.string,
        dateLive: PropTypes.string,
        department: PropTypes.string,
        links: PropTypes.arrayOf(PropTypes.shape({ href: PropTypes.string, rel: PropTypes.string }))
    }),
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        item: PropTypes.string,
        details: PropTypes.shape({
            errors: PropTypes.arrayOf(PropTypes.shape({}))
        })
    }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    addItem: PropTypes.func.isRequired,
    updateItem: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    nominal: PropTypes.shape({ nominalCode: PropTypes.string, description: PropTypes.string }),
    privileges: PropTypes.arrayOf(PropTypes.string),
    userName: PropTypes.string,
    userNumber: PropTypes.number,
    options: PropTypes.shape({ template: PropTypes.string }),
    partTemplates: PropTypes.arrayOf(PropTypes.shape({ partRoot: PropTypes.string })),
    liveTest: PropTypes.shape({ canMakeLive: PropTypes.bool, message: PropTypes.string }),
    fetchLiveTest: PropTypes.func.isRequired,
    fetchParts: PropTypes.func.isRequired
};

Part.defaultProps = {
    item: null,
    snackbarVisible: false,
    loading: true,
    itemError: null,
    itemId: null,
    nominal: null,
    privileges: null,
    userName: null,
    userNumber: null,
    options: null,
    partTemplates: [],
    liveTest: null,
    partsSearchResults: []
};

export default Part;
