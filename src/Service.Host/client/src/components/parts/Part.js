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
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';
import GeneralTab from '../../containers/parts/tabs/GeneralTab';
import BuildTab from '../../containers/parts/tabs/BuildTab';
import PurchTab from '../../containers/parts/tabs/PurchTab';
import StoresTab from './tabs/StoresTab';
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
    userNumber
}) {
    const creating = () => editStatus === 'create';
    // const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';
    const [part, setPart] = useState(
        creating()
            ? {
                  partNumber: '',
                  description: '',
                  accountingCompany: 'LINN',
                  psuPart: 'No',
                  stockControlled: 'Yes',
                  cccCriticalPart: 'No',
                  safetyCriticalPart: 'No',
                  paretoCode: 'U',
                  createdBy: userNumber,
                  dateCreated: new Date(),
                  railMethod: 'POLICY',
                  preferredSupplier: 4415,
                  preferredSupplierName: 'Linn Products Ltd',
                  qcInformation: ''
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
        }
    }, [item, prevPart, fetchNominal, editStatus]);

    useEffect(() => {
        setPart(p => ({
            ...p,
            nominalCode: nominal?.nominalCode,
            nominalDescription: nominal?.description
        }));
    }, [nominal, setPart]);

    const partInvalid = () => !part.partNumber || !part.description;

    const handleSaveClick = () => {
        const partResource = part;
        // convert Yes/No to true/false for resource to send
        Object.keys(partResource).forEach(k => {
            if (partResource[k] === 'Yes') {
                partResource[k] = true;
            }
            if (partResource[k] === 'No') {
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
        if (viewing() && propertyName !== 'reasonPhasedOut') {
            setEditStatus('edit');
        }
        if (newValue === 'Yes' || newValue === 'No') {
            setPart({ ...part, [propertyName]: newValue === 'Yes' });
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

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? <Title text="Create Part" /> : <Title text="Part Details" />}
                </Grid>
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
                    itemError?.part !== 404 && (
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
                                    helperText={!creating() ? 'This field cannot be changed' : ''}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="partNumber"
                                />
                            </Grid>
                            <Grid item xs={8}>
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
                                    nominal={part.nominalCode}
                                    nominalDescription={part.nominalDescription}
                                    stockControlled={part.stockControlled}
                                    safetyCriticalPart={part.safetyCriticalPart}
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
                                />
                            )}
                            {tab === 3 && (
                                <StoresTab
                                    handleFieldChange={handleFieldChange}
                                    qcOnReceipt={part.qcOnReceipt}
                                    qcInfo={part.qcInfo}
                                    rawOrFinished={part.rawOrFinished}
                                    ourInspectionWeeks={part.ourInspectionWeeks}
                                    safetyWeeks={part.safetyWeeks}
                                    railMethod={part.railMethod}
                                    minStockrail={part.minstockrail}
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
        dateClosed: PropTypes.string
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
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
    userNumber: PropTypes.number
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
    userNumber: null
};

export default Part;
