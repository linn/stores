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
    smartGoBack
} from '@linn-it/linn-form-components-library';
import Page from '../../../containers/Page';
import DataSheetsTab from './tabs/DataSheetsTab';
import ProposalTab from '../../../containers/parts/mechPartSource/tabs/ProposalTab';
import QualityRequirementsTab from './tabs/QualityRequirementsTab';
import Manufacturerstab from '../../../containers/parts/mechPartSource/tabs/ManufacturersTab';
import SuppliersTab from '../../../containers/parts/mechPartSource/tabs/SuppliersTab';
import ParamDataTab from '../../../containers/parts/mechPartSource/tabs/ParamDataTab';
import CadDataTab from './tabs/CadDataTab';
import UsagesTab from '../../../containers/parts/mechPartSource/tabs/UsagesTab';
import VerificationTab from './tabs/VerificationTab';

function MechPartSource({
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
    options,
    userName,
    userNumber,
    previousPaths
}) {
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const [mechPartSource, setMechPartSource] = useState(
        creating()
            ? {
                  proposedBy: userNumber,
                  proposedByName: userName,
                  dateEntered: new Date(),
                  createPart: true,
                  mechPartAlts: [],
                  mechPartManufacturerAlts: [],
                  mechanicalOrElectrical: 'E',
                  samplesRequired: 'N'
              }
            : null
    );
    const [prevMechPartSource, setPrevMechPartSource] = useState({});

    const tabDictionary = {
        proposal: 0,
        dataSheets: 1,
        qualityRequirements: 2,
        suppliers: 3,
        manufacturers: 4,
        paramData: 5,
        cadData: 6,
        usages: 7,
        verification: 8
    };

    const [tab, setTab] = useState(options?.tab ? tabDictionary[options?.tab] : 0);
    const [newUsagesRow, setNewUsagesRow] = useState({});

    const handleTabChange = (_, value) => {
        setTab(value);
    };

    const mechPartSourceInvalid = () =>
        !mechPartSource.samplesRequired ||
        (mechPartSource.mechanicalOrElectrical === 'E' && !mechPartSource.partType);

    useEffect(() => {
        if (item !== prevMechPartSource && editStatus !== 'create') {
            setMechPartSource({ ...item, resistanceUnits: 'KΩ', capacitanceUnits: 'uF' });
            setPrevMechPartSource(item);
        }
    }, [item, prevMechPartSource, editStatus, itemId]);

    const handleSaveClick = () => {
        const body = mechPartSource;
        const rkmLetters = { KΩ: 'K', MΩ: 'M', Ω: '' };
        body.resistanceUnits = rkmLetters[mechPartSource.resistanceUnits];
        if (creating()) {
            addItem(body);
        } else {
            updateItem(itemId, body);
        }
        setEditStatus('view');
    };

    const handleCancelClick = () => {
        setMechPartSource(item);
        setEditStatus('view');
    };

    const handleDatasheetsChange = dataSheets => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setMechPartSource(m => ({ ...m, part: { ...m.part, dataSheets } }));
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('editing');
        }
        setMechPartSource({ ...mechPartSource, [propertyName]: newValue });
    };

    const handlePartFieldChange = (propertyName, newValue) => {
        if (viewing) {
            setEditStatus('editing');
        }
        setMechPartSource({
            ...mechPartSource,
            part: { ...mechPartSource.part, [propertyName]: newValue }
        });
    };

    const handleLinnPartChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setMechPartSource({
            ...mechPartSource,
            linnPartNumber: newValue.name,
            linnPartDescription: newValue.description
        });
    };

    const deleteUsagesRow = row => {
        setEditStatus('edit');
        setMechPartSource(m => ({
            ...m,
            usages: m.usages.filter(a => a.rootProductName === row.rootProductName)
        }));
    };

    // this function gets the current max val for a specified field in an array of objects with a value for that field
    // useful for getting the next 'id' for a new row added to a table, as seen below where 'sequence' is the id field
    // returns 0 if no entries
    const getMaxFieldValue = (objectArray, fieldName) =>
        objectArray?.length > 0
            ? objectArray.reduce((prev, current) =>
                  prev[fieldName] > current[fieldName] ? prev : current
              )[fieldName]
            : 0;

    const addSuppliersRow = () => {
        setEditStatus('edit');
        setMechPartSource(m => ({
            ...m,
            mechPartAlts: [
                ...m.mechPartAlts,
                {
                    sequence: getMaxFieldValue(mechPartSource.mechPartAlts, 'sequence') + 1,
                    supplierId: null,
                    supplierName: null,
                    partNumber: null
                }
            ]
        }));
    };

    const deleteSuppliersRow = id => {
        setEditStatus('edit');
        setMechPartSource(m => ({
            ...m,
            mechPartAlts: m.mechPartAlts.filter(a => a.sequence !== id)
        }));
    };

    const updateSuppliersRow = (row, _, propertyName, newValue) => {
        setEditStatus('edit');
        setMechPartSource(m => ({
            ...m,
            mechPartAlts: m.mechPartAlts.map(e =>
                e.sequence === row.sequence ? { ...e, [propertyName]: newValue } : e
            )
        }));
    };

    const resetSuppliersRow = i => {
        setMechPartSource(m => ({
            ...m,
            mechPartAlts: [
                ...m.mechPartAlts.filter(a => a.sequence !== i.id),
                { ...item.mechPartAlts?.find(s => s.sequence === i.id) }
            ]
        }));
    };

    const addManufacturersRow = () => {
        setEditStatus('edit');
        setMechPartSource(m => ({
            ...m,
            mechPartManufacturerAlts: [
                ...m.mechPartManufacturerAlts,
                {
                    sequence:
                        getMaxFieldValue(mechPartSource.mechPartManufacturerAlts, 'sequence') + 1,
                    manufacturerCode: null,
                    manufacturerDescription: null,
                    preference: null,
                    partNumber: null,
                    reelSuffix: null,
                    rohsCompliant: null,
                    approvedBy: null,
                    approvedByName: null,
                    dateApproved: null
                }
            ]
        }));
    };

    const deleteManufacturersRow = id => {
        setEditStatus('edit');
        setMechPartSource(m => ({
            ...m,
            mechPartManufacturerAlts: m.mechPartManufacturerAlts.filter(a => a.sequence !== id)
        }));
    };

    const updateManufacturersRow = (row, _, propertyName, newValue) => {
        setEditStatus('edit');
        setMechPartSource(m => ({
            ...m,
            mechPartManufacturerAlts: m.mechPartManufacturerAlts.map(e =>
                e.sequence === row.sequence ? { ...e, [propertyName]: newValue } : e
            )
        }));
    };

    const resetManufacturersRow = i => {
        setMechPartSource(m => ({
            ...m,
            mechPartManufacturerAlts: [
                ...m.mechPartManufacturerAlts.filter(a => a.sequence !== i.id),
                { ...item.mechPartManufacturerAlts?.find(s => s.sequence === i.id) }
            ]
        }));
    };

    const handleApprovedByChange = (sequence, newValue) => {
        setMechPartSource(m => ({
            ...m,
            mechPartManufacturerAlts: m.mechPartManufacturerAlts.map(x =>
                x.sequence === sequence
                    ? { ...x, approvedBy: newValue.name, approvedByName: newValue.description }
                    : x
            )
        }));
    };

    const handleSupplierChange = (sequence, newValue) => {
        setMechPartSource(m => ({
            ...m,
            mechPartAlts: m.mechPartAlts.map(x =>
                x.sequence === sequence
                    ? { ...x, supplierId: newValue.name, supplierName: newValue.description }
                    : x
            )
        }));
    };

    const handleRootProductChange = (rootProductName, newValue) => {
        setMechPartSource(m => ({
            ...m,
            usages: m.usages.map(x =>
                x.rootProductName === rootProductName
                    ? {
                          ...x,
                          rootProduct: newValue.name,
                          rootProductDescription: newValue.description
                      }
                    : x
            )
        }));
    };

    const handleManufacturerChange = (sequence, newValue) => {
        setMechPartSource(m => ({
            ...m,
            mechPartManufacturerAlts: m.mechPartManufacturerAlts.map(x =>
                x.sequence === sequence ? { ...x, manufacturerCode: newValue.name } : x
            )
        }));
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create Mech Part Source" />
                    ) : (
                        <Title text="Mech Part Source Details" />
                    )}
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
                    mechPartSource &&
                    itemError?.mechPartSource !== 404 && (
                        <>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            {!creating() && (
                                <>
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
                                            disabled={!creating()}
                                            value={mechPartSource.partNumber}
                                            label="Part Number"
                                            maxLength={14}
                                            helperText={
                                                !creating() ? 'This field cannot be changed' : ''
                                            }
                                            required
                                            onChange={handleFieldChange}
                                            propertyName="partNumber"
                                        />
                                    </Grid>
                                    <Grid item xs={8}>
                                        <InputField
                                            fullWidth
                                            value={mechPartSource.part?.description}
                                            label="Description"
                                            maxLength={200}
                                            required
                                            onChange={handlePartFieldChange}
                                            propertyName="description"
                                        />
                                    </Grid>
                                </>
                            )}
                            <Tabs
                                value={tab}
                                onChange={handleTabChange}
                                indicatorColor="primary"
                                scrollButtons="on"
                                textColor="primary"
                                style={{ paddingBottom: '40px' }}
                            >
                                <Tab label="Proposal" />
                                <Tab label="DataSheets" disabled={!mechPartSource.part} />
                                <Tab label="Quality Requirements" />
                                <Tab label="Suppliers" />
                                <Tab label="Manufacturers" />
                                <Tab
                                    label="Param Data"
                                    disabled={mechPartSource.mechanicalOrElectrical !== 'E'}
                                />
                                <Tab label="Cad Data" />
                                <Tab label="Usages" />
                                <Tab label="Verification" />
                            </Tabs>
                            {tab === 0 && (
                                <ProposalTab
                                    handleFieldChange={handleFieldChange}
                                    notes={mechPartSource.notes}
                                    partType={mechPartSource.partType}
                                    proposedBy={mechPartSource.proposedBy}
                                    proposedByName={mechPartSource.proposedByName}
                                    dateEntered={mechPartSource.dateEntered}
                                    mechanicalOrElectrical={mechPartSource.mechanicalOrElectrical}
                                    safetyCritical={mechPartSource.safetyCritical}
                                    performanceCritical={mechPartSource.performanceCritical}
                                    emcCritical={mechPartSource.emcCritical}
                                    singleSource={mechPartSource.singleSource}
                                    safetyDataDirectory={mechPartSource.safetyDataDirectory}
                                    productionDate={mechPartSource.productionDate}
                                    estimatedVolume={mechPartSource.estimatedVolume}
                                    samplesRequired={mechPartSource.samplesRequired}
                                    sampleQuantity={mechPartSource.sampleQuantity}
                                    dateSamplesRequired={mechPartSource.dateSamplesRequired}
                                    rohsReplace={mechPartSource.rohsReplace}
                                    linnPartNumber={mechPartSource.linnPartNumber}
                                    linnPartDescription={mechPartSource.linnPartDescription}
                                    assemblyType={mechPartSource.assemblyType}
                                    handleLinnPartChange={handleLinnPartChange}
                                />
                            )}
                            {tab === 1 && mechPartSource.part && (
                                <DataSheetsTab
                                    dataSheets={mechPartSource.part?.dataSheets}
                                    handleDataSheetsChange={handleDatasheetsChange}
                                />
                            )}
                            {tab === 2 && (
                                <QualityRequirementsTab
                                    handleFieldChange={handleFieldChange}
                                    drawingsPackage={mechPartSource.drawingsPackage}
                                    drawingsPackageAvailable={
                                        mechPartSource.drawingsPackageAvailable
                                    }
                                    drawingsPackageDate={mechPartSource.drawingsPackageDate}
                                    drawingFile={mechPartSource.drawingFile}
                                    checklistCreated={mechPartSource.checklistCreated}
                                    checklistAvailable={mechPartSource.checklistAvailable}
                                    checklistDate={mechPartSource.checklistDate}
                                    packingRequired={mechPartSource.packingRequired}
                                    packingAvailable={mechPartSource.packingAvailable}
                                    packingDate={mechPartSource.packingDate}
                                    productKnowledge={mechPartSource.productKnowledge}
                                    productKnowledgeAvailable={
                                        mechPartSource.productKnowledgeAvailable
                                    }
                                    productKnowledgeDate={mechPartSource.productKnowledgeDate}
                                    testEquipment={mechPartSource.testEquipment}
                                    testEquipmentAvailable={mechPartSource.testEquipmentAvailable}
                                    testEquipmentDate={mechPartSource.testEquipmentDate}
                                    approvedReferenceStandards={
                                        mechPartSource.approvedReferenceStandards
                                    }
                                    approvedReferencesAvailable={
                                        mechPartSource.approvedReferencesAvailable
                                    }
                                    approvedReferencesDate={mechPartSource.approvedReferencesDate}
                                    processEvaluation={mechPartSource.processEvaluation}
                                    processEvaluationAvailable={
                                        mechPartSource.processEvaluationAvailable
                                    }
                                    processEvaluationDate={mechPartSource.processEvaluationDate}
                                />
                            )}
                            {tab === 3 && (
                                <SuppliersTab
                                    handleSupplierChange={handleSupplierChange}
                                    suppliers={mechPartSource.mechPartAlts}
                                    deleteRow={deleteSuppliersRow}
                                    addNewRow={addSuppliersRow}
                                    resetRow={resetSuppliersRow}
                                    updateRow={updateSuppliersRow}
                                />
                            )}
                            {tab === 4 && (
                                <Manufacturerstab
                                    handleApprovedByChange={handleApprovedByChange}
                                    handleManufacturerChange={handleManufacturerChange}
                                    manufacturers={mechPartSource.mechPartManufacturerAlts}
                                    deleteRow={deleteManufacturersRow}
                                    addNewRow={addManufacturersRow}
                                    resetRow={resetManufacturersRow}
                                    updateRow={updateManufacturersRow}
                                />
                            )}
                            {tab === 5 && mechPartSource.mechanicalOrElectrical === 'E' && (
                                <ParamDataTab
                                    partType={mechPartSource.partType}
                                    resistance={mechPartSource.resistance}
                                    resistanceUnits={
                                        creating()
                                            ? mechPartSource.resistanceUnits
                                            : mechPartSource?.rkmCode
                                                  ?.replace(/[^a-zA-Z]/g, '')
                                                  .concat('Ω')
                                    }
                                    handleFieldChange={handleFieldChange}
                                    capacitorRippleCurrent={mechPartSource.capacitorRippleCurrent}
                                    capacitance={mechPartSource.capacitance}
                                    capacitanceUnits={
                                        creating()
                                            ? mechPartSource.capacitanceUnits
                                            : mechPartSource?.capacitanceLetterAndNumeralCodestring?.replace(
                                                  /[^a-zA-Z]/g,
                                                  ''
                                              )
                                    }
                                    capacitorVoltageRating={mechPartSource.capacitorVoltageRating}
                                    capacitorPositiveTolerance={
                                        mechPartSource.capacitorPositiveTolerance
                                    }
                                    capacitorNegativeTolerance={
                                        mechPartSource.capacitorNegativeTolerance
                                    }
                                    creating={creating}
                                    capacitorDielectric={mechPartSource.capacitorDielectric}
                                    packageName={mechPartSource.packageName}
                                    capacitorPitch={mechPartSource.capacitorPitch}
                                    capacitorLength={mechPartSource.capacitorLength}
                                    capacitorWidth={mechPartSource.capacitorWidth}
                                    capacitorHeight={mechPartSource.capacitorHeight}
                                    capacitorDiameter={mechPartSource.capacitorDiameter}
                                    resistorTolerance={mechPartSource.resistorTolerance}
                                    construction={mechPartSource.construction}
                                    resistorLength={mechPartSource.resistorLength}
                                    resistorWidth={mechPartSource.resistorWidth}
                                    resistorHeight={mechPartSource.resistorHeight}
                                    resistorPowerRating={mechPartSource.resistorPowerRating}
                                    resistorVoltageRating={mechPartSource.resistorVoltageRating}
                                    temperatureCoefficient={mechPartSource.temperatureCoefficient}
                                    transistorDeviceName={mechPartSource.transistorDeviceName}
                                    transistorPolarity={mechPartSource.transistorPolarity}
                                    transistorVoltage={mechPartSource.transistorVoltage}
                                    transistorCurrent={mechPartSource.transistorCurrent}
                                    icType={mechPartSource.icType}
                                    icFunction={mechPartSource.icFunction}
                                    libraryRef={mechPartSource.libraryRef}
                                    footPrintRef={mechPartSource.footPrintRef}
                                />
                            )}
                            {tab === 6 && (
                                <CadDataTab
                                    libraryRef={mechPartSource.libraryRef}
                                    footprintRef={mechPartSource.footprintRef}
                                    handleFieldChange={handleFieldChange}
                                />
                            )}
                            {tab === 7 && (
                                <UsagesTab
                                    handleRootProductChange={handleRootProductChange}
                                    usages={mechPartSource.usages}
                                    saveRow={saveUsagesRow}
                                    deleteRow={deleteUsagesRow}
                                    newRow={newUsagesRow}
                                    setNewRow={setNewUsagesRow}
                                />
                            )}
                            {tab === 8 && (
                                <VerificationTab
                                    handleFieldChange={handleFieldChange}
                                    partCreatedBy={mechPartSource.partCreatedBy}
                                    partCreatedByName={mechPartSource.partCreatedByName}
                                    partCreatedDate={mechPartSource.partCreatedDate}
                                    verifiedBy={mechPartSource.verifiedBy}
                                    verifiedByName={mechPartSource.verifiedByName}
                                    verifiedDate={mechPartSource.verifiedDate}
                                    qualityVerifiedBy={mechPartSource.qualityVerifiedBy}
                                    qualityVerifiedByName={mechPartSource.qualityVerifiedByName}
                                    qualityVerifiedDate={mechPartSource.qualityVerifiedDate}
                                    mcitVerifiedBy={mechPartSource.mcitVerifiedBy}
                                    mcitVerifiedByName={mechPartSource.mcitVerifiedByName}
                                    mcitVerifiedDate={mechPartSource.mcitVerifiedDate}
                                    applyTCodeBy={mechPartSource.applyTCodeBy}
                                    applyTCodeName={mechPartSource.applyTCodeName}
                                    applyTCodeDate={mechPartSource.applyTCodeDate}
                                    removeTCodeBy={mechPartSource.removeTCodeBy}
                                    removeTCodeName={mechPartSource.removeTCodeName}
                                    removeTCodeDate={mechPartSource.removeTCodeDate}
                                    cancelledBy={mechPartSource.cancelledBy}
                                    cancelledByName={mechPartSource.cancelledByName}
                                    dateCancelled={mechPartSource.dateCancelled}
                                />
                            )}
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing() || mechPartSourceInvalid()}
                                    saveClick={handleSaveClick}
                                    cancelClick={handleCancelClick}
                                    backClick={() => {
                                        smartGoBack(previousPaths, history.goBack);
                                    }}
                                />
                            </Grid>
                        </>
                    )
                )}
            </Grid>
        </Page>
    );
}

MechPartSource.propTypes = {
    item: PropTypes.shape({}),
    history: PropTypes.shape({ goBack: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape(),
        item: PropTypes.string,
        mechPartSource: PropTypes.number
    }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    addItem: PropTypes.func.isRequired,
    updateItem: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    userName: PropTypes.string,
    userNumber: PropTypes.number,
    options: PropTypes.shape({ tab: PropTypes.string }),
    liveTest: PropTypes.shape({ canMakeLive: PropTypes.bool, message: PropTypes.string }),
    previousPaths: PropTypes.arrayOf(PropTypes.string)
};

MechPartSource.defaultProps = {
    item: {},
    snackbarVisible: false,
    loading: null,
    itemError: null,
    itemId: null,
    options: null,
    liveTest: null,
    userName: null,
    userNumber: null,
    previousPaths: []
};

export default MechPartSource;
