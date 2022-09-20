import React, { useState, useEffect, useReducer } from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
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
    LinkButton,
    utilities
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';
import GeneralTab from '../../containers/parts/tabs/GeneralTab';
import BuildTab from '../../containers/parts/tabs/BuildTab';
import PurchTab from '../../containers/parts/tabs/PurchTab';
import StoresTab from '../../containers/parts/tabs/StoresTab';
import LifeCycleTab from './tabs/LifeCycleTab';
import partReducer from './partReducer';
import handleBackClick from '../../helpers/handleBackClick';

function Part({
    copy,
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
    templateName,
    partTemplates,
    liveTest,
    fetchLiveTest,
    fetchParts,
    partsSearchResults,
    clearErrors,
    previousPaths
}) {
    const defaultPart = {
        partNumber: '',
        description: '',
        accountingCompany: 'LINN',
        psuPart: 'N',
        stockControlled: 'Y',
        cccCriticalPart: 'N',
        safetyCriticalPart: 'N',
        bomType: '',
        paretoCode: 'U',
        createdBy: userNumber,
        createdByName: userName,
        dateCreated: new Date(),
        railMethod: 'LEADTIME',
        qcInformation: '',
        qcOnReceipt: 'N',
        orderHold: 'N'
    };
    const creating = () => editStatus === 'create';

    const [state, dispatch] = useReducer(partReducer, {
        part: creating() ? defaultPart : { partNumber: '' },
        prevPart: { partNumber: '' }
    });

    useEffect(() => {
        if (copy) {
            dispatch({
                type: 'initialiseCopy',
                payload: {
                    userNumber,
                    userName
                }
            });
        }
    }, [copy, userName, userNumber]);

    // checking whether partNumber already exists when partNumber is entered
    useEffect(() => {
        if (editStatus === 'create') {
            if (state.part.partNumber.match(/\/[1-9]$/)) {
                //if new partNumber ends in /[1-9] then user is creating a new revision of existing part
                fetchParts(state.part.partNumber.split('/')[0]); // so fetch the existing parts for any crosschecking we need to do
            } else {
                fetchParts(state.part.partNumber); // else they are creating a new part entirely. Check to see if it already exists.
            }
        }
    }, [state.part.partNumber, fetchParts, editStatus]);

    useEffect(() => {
        if (itemId) {
            fetchLiveTest(itemId);
        }
    }, [fetchLiveTest, itemId]);

    useEffect(() => {
        if (templateName && partTemplates.length) {
            const template = partTemplates.find(t => t.partRoot === templateName);
            const formatNextNumber = () => {
                if (template.nextNumber < 1000) {
                    return template.nextNumber.toString().padStart(3, 0);
                }
                return template.nextNumber.toString();
            };
            dispatch({
                type: 'initialise',
                payload: {
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
                    stockControlled: template.stockControlled,
                    cccCriticalPart: 'N',
                    safetyCriticalPart: 'N',
                    psuPart: 'N'
                }
            });
        }
    }, [templateName, partTemplates]);

    const viewing = () => editStatus === 'view';

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
        if (editStatus === 'create') {
            dispatch({ type: 'fieldChange', fieldName: 'bomId', payload: null });
        }
    }, [editStatus]);

    useEffect(() => {
        if (item && item !== state.prevPart) {
            if (editStatus === 'create') {
                dispatch({ type: 'initialise', payload: defaultPart });
            } else {
                dispatch({ type: 'initialise', payload: item });
            }
        }
    }, [item, state.prevPart, editStatus, defaultPart]);

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

        if (prevRevision?.safetyCriticalPart === 'Y') {
            return 'Note: Previous Revision Was Safety Critical';
        }

        if (prevRevision?.safetyCriticalPart === 'N') {
            return 'Note: Previous Revision Was NOT Safety Critical';
        }

        return '';
    };

    const handleSaveClick = () => {
        clearErrors();
        if (creating()) {
            addItem({ ...state.part, fromTemplate: !!templateName });
        } else {
            updateItem(itemId, state.part);
        }
        setEditStatus('view');
    };

    const handleCancelClick = () => {
        if (editStatus === 'create') {
            dispatch({ type: 'initialise', payload: defaultPart });
        } else {
            dispatch({ type: 'initialise', payload: item });
        }
        setEditStatus('view');
    };

    const handleFieldChange = (propertyName, newValue) => {
        setEditStatus('edit');
        dispatch({ type: 'fieldChange', fieldName: propertyName, payload: newValue });
    };

    const handlePhaseOutClick = () => {
        updateItem(itemId, {
            ...state.part,
            datePhasedOut: new Date(),
            phasedOutBy: userNumber,
            phasedOutByName: userName
        });
    };

    const handlePhaseInClick = () => {
        updateItem(itemId, {
            ...state.part,
            datePhasedOut: null,
            phasedOutBy: null,
            phasedOutByName: null,
            reasonPhasedOut: `PHASED BACK IN BY ${userName?.toUpperCase()} on ${moment(
                new Date()
            ).format('DD MMM YYYY')}`
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

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={8}>
                    {creating() ? <Title text="Create Part" /> : <Title text="Part Details" />}
                </Grid>
                {creating() ? (
                    <Grid item xs={4} />
                ) : (
                    <>
                        {state.part?.sourceId ? (
                            <Grid item xs={2}>
                                <LinkButton
                                    to={`/parts/sources/${state.part?.sourceId}`}
                                    text="SOURCE SHEET"
                                />
                            </Grid>
                        ) : (
                            <Grid item xs={2} />
                        )}
                        <Grid item xs={2}>
                            <LinkButton to="/parts/create?copy=true" text="Copy" />
                        </Grid>
                    </>
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
                    (state.part.partNumber || creating()) &&
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
                                    salesArticleNumber={state.part.salesArticleNumber}
                                    editStatus={editStatus}
                                    bomTreeLink={utilities.getHref(state.part, 'bom-tree')}
                                />
                            )}
                            {tab === 1 && (
                                <BuildTab
                                    handleFieldChange={handleFieldChange}
                                    creating={creating}
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
                                    nonForecastRequirement={state.part.nonForecastRequirement}
                                    oneOffRequirement={state.part.oneOffRequirement}
                                    sparesRequirement={state.part.sparesRequirement}
                                    ignoreWorkstationStock={state.part.ignoreWorkstationStock}
                                    imdsIdNumber={state.part.imdsIdNumber}
                                    imdsWeight={state.part.imdsWeight}
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
                                    minStockRail={state.part.minStockRail}
                                    maxStockRail={state.part.maxStockRail}
                                    secondStageBoard={state.part.secondStageBoard}
                                    secondStageDescription={state.part.secondStageDescription}
                                    tqmsCategoryOverride={state.part.tqmsCategoryOverride}
                                    stockNotes={state.part.stockNotes}
                                    plannerStory={state.part.plannerStory}
                                />
                            )}
                            {tab === 4 && (
                                <LifeCycleTab
                                    handleFieldChange={handleFieldChange}
                                    handlePhaseOutClick={handlePhaseOutClick}
                                    handlePhaseInClick={handlePhaseInClick}
                                    editStatus={editStatus}
                                    canPhaseOut={canPhaseOut()}
                                    dateCreated={state.part.dateCreated}
                                    createdBy={state.part.createdBy}
                                    createdByName={state.part.createdByName}
                                    dateLive={state.part.dateLive}
                                    madeLiveBy={state.part.madeLiveBy}
                                    madeLiveByName={state.part.madeLiveByName}
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
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing() || partInvalid()}
                                    saveClick={handleSaveClick}
                                    cancelClick={handleCancelClick}
                                    backClick={() => handleBackClick(previousPaths, history.goBack)}
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
    copy: PropTypes.bool,
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
    history: PropTypes.shape({ push: PropTypes.func, goBack: PropTypes.func }).isRequired,
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
    templateName: PropTypes.string,
    partTemplates: PropTypes.arrayOf(PropTypes.shape({ partRoot: PropTypes.string })),
    liveTest: PropTypes.shape({ canMakeLive: PropTypes.bool, message: PropTypes.string }),
    fetchLiveTest: PropTypes.func.isRequired,
    fetchParts: PropTypes.func.isRequired,
    clearErrors: PropTypes.func.isRequired,
    previousPaths: PropTypes.arrayOf(PropTypes.shape({}))
};

Part.defaultProps = {
    copy: false,
    item: null,
    snackbarVisible: false,
    loading: true,
    itemError: null,
    itemId: null,
    nominal: null,
    privileges: null,
    userName: null,
    userNumber: null,
    templateName: null,
    partTemplates: [],
    previousPaths: [],
    liveTest: null,
    partsSearchResults: []
};

export default Part;
