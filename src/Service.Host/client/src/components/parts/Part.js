import React, { useState, useEffect } from 'react';
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
    nominal,
    fetchNominal,
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
    const creating = () => editStatus === 'create';
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
    const [prevPart, setPrevPart] = useState({});

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
        if (item?.department) {
            fetchNominal(item?.department);
        }
        if (item !== prevPart && editStatus !== 'create') {
            setPart(item);
            setPrevPart(item);
            fetchLiveTest(itemId);
        }
        if (editStatus === 'create') {
            setPart(p => ({ ...p, bomId: null }));
        }
    }, [item, prevPart, fetchNominal, editStatus, fetchLiveTest, itemId]);

    useEffect(() => {
        setPart(p => ({
            ...p,
            nominal: nominal?.nominalCode,
            nominalDescription: nominal?.description
        }));
    }, [nominal, setPart]);

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

    const partInvalid = () => !part.partNumber || !part.description;

    const getPartNumberHelperText = () => {
        if (partsSearchResults.some(p => p.partNumber === part.partNumber.toUpperCase())) {
            return 'PART NUMBER ALREADY EXISTS.';
        }
        return '';
    };

    const getSafetyCriticalHelperText = () => {
        const prevRevision = partsSearchResults.find(
            p => p.partNumber === part.partNumber.toUpperCase().split('/')[0]
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
        const partResource = part;
        // convert Yes/No to true/false for resource to send
        Object.keys(partResource).forEach(k => {
            if (partResource[k] === 'Yes' || partResource[k] === 'Y') {
                partResource[k] = true;
            }
            if (partResource[k] === 'No' || partResource[k] === 'N') {
                partResource[k] = false;
            }
        });
        if (creating()) {
            addItem(partResource);
        } else {
            updateItem(itemId, partResource);
        }
        setEditStatus('view');
    };

    const handleCancelClick = () => {
        setPart(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/inventory/parts');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (propertyName === 'partNumber' && creating()) {
            if (newValue.match(/\/[1-9]$/)) {
                //if new partNumber ends in /[1-9] then user is creating a new revision of existing part
                fetchParts(newValue.split('/')[0]); // so fetch the existing parts for any crosschecking we need to do
            } else {
                fetchParts(newValue); // else they are creating a new part entirely. Check to see if it already exists.
            }
        }
        if (viewing() && propertyName !== 'reasonPhasedOut') {
            setEditStatus('edit');
        }
        if (newValue === 'Yes' || newValue === 'No') {
            setPart({ ...part, [propertyName]: newValue === 'Yes' });
        } else if (typeof newValue === 'string') {
            setPart({ ...part, [propertyName]: newValue });
        } else {
            setPart({ ...part, [propertyName]: newValue });
        }
    };

    const handlePhaseOutClick = () => {
        updateItem(itemId, {
            ...part,
            datePhasedOut: new Date(),
            phasedOutBy: userNumber,
            phasedOutByName: userName
        });
    };

    const handleChangeLiveness = () => {
        if (!item.dateLive) {
            updateItem(itemId, {
                ...part,
                dateLive: new Date(),
                madeLiveBy: userNumber,
                madeLiveByName: userName
            });
        } else {
            updateItem(itemId, {
                ...part,
                dateLive: null,
                madeLiveBy: null,
                madeLiveByName: null
            });
        }
    };

    const handleIgnoreWorkstationStockChange = (_, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        if (newValue === 'Yes') {
            setPart({ ...part, ignoreWorkstationStock: newValue === 'Yes' });
        } else {
            setPart({ ...part, ignoreWorkstationStock: null });
        }
    };

    const handleDepartmentChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        fetchNominal(newValue.name);
        setPart({
            ...part,
            department: newValue.name,
            departmentDescription: newValue.description
        });
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

    const handleSernosSequenceChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPart({
            ...part,
            sernosSequenceName: newValue.name,
            sernosSequenceDescription: newValue.description
        });
    };

    const handlePrefferedSupplierChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPart({
            ...part,
            preferredSupplier: newValue.name,
            preferredSupplierName: newValue.description
        });
    };

    const handleAccountingCompanyChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        if (newValue === 'RECORDS') {
            setPart({
                ...part,
                accountingCompany: newValue,
                paretoCode: 'R',
                bomType: 'C',
                linnProduced: 'No',
                qcOnReceipt: 'No'
            });
        } else {
            setPart({ ...part, accountingCompany: newValue, paretoCode: 'U' });
        }
    };

    const handleManufacturersPartNumberChange = (manufacturerCode, newValue) => {
        setEditStatus('edit');
        setPart(p => ({
            ...p,
            manufacturers: p.manufacturers.map(m =>
                m.manufacturerCode === manufacturerCode ? { ...m, partNumber: newValue } : m
            )
        }));
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
                    part &&
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
                                    value={part.partNumber}
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
                                    value={part.description}
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
                                    accountingCompany={part.accountingCompany}
                                    handleFieldChange={handleFieldChange}
                                    productAnalysisCode={part.productAnalysisCode}
                                    productAnalysisCodeDescription={
                                        part.productAnalysisCodeDescription
                                    }
                                    handleProductAnalysisCodeChange={
                                        handleProductAnalysisCodeChange
                                    }
                                    rootProduct={part.rootProduct}
                                    department={part.department}
                                    departmentDescription={part.departmentDescription}
                                    handleDepartmentChange={handleDepartmentChange}
                                    paretoCode={part.paretoCode}
                                    handleAccountingCompanyChange={handleAccountingCompanyChange}
                                    nominal={part.nominal}
                                    nominalDescription={part.nominalDescription}
                                    stockControlled={part.stockControlled}
                                    safetyCriticalPart={part.safetyCriticalPart}
                                    safetyCriticalHelperText={
                                        creating() ? getSafetyCriticalHelperText() : null
                                    }
                                    performanceCriticalPart={part.performanceCriticalPart}
                                    emcCriticalPart={part.emcCriticalPart}
                                    singleSourcePart={part.singleSourcePart}
                                    cccCriticalPart={part.cccCriticalPart}
                                    psuPart={part.psuPart}
                                    safetyCertificateExpirationDate={
                                        part.safetyCertificateExpirationDate
                                    }
                                    safetyDataDirectory={part.safetyDataDirectory}
                                />
                            )}
                            {tab === 1 && (
                                <BuildTab
                                    handleFieldChange={handleFieldChange}
                                    linnProduced={part.linnProduced}
                                    sernosSequenceName={part.sernosSequenceName}
                                    sernosSequenceDescription={part.sernosSequenceDescription}
                                    handleSernosSequenceChange={handleSernosSequenceChange}
                                    decrementRuleName={part.decrementRuleName}
                                    assemblyTechnologyName={part.assemblyTechnologyName}
                                    bomType={part.bomType}
                                    bomId={part.bomId}
                                    optionSet={part.optionSet}
                                    drawingReference={part.drawingReference}
                                    safetyCriticalPart={part.safetyCriticalPart}
                                    plannedSurplus={part.plannedSurplus}
                                />
                            )}
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
                            )}
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
    fetchNominal: PropTypes.func.isRequired,
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
    item: {},
    snackbarVisible: false,
    loading: null,
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
