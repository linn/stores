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
    useGroupEditTable,
    LinkButton
} from '@linn-it/linn-form-components-library';
import Page from '../../../containers/Page';
import DataSheetsTab from './tabs/DataSheetsTab';
import ProposalTab from '../../../containers/parts/mechPartSource/tabs/ProposalTab';
import QualityRequirementsTab from './tabs/QualityRequirementsTab';
import ManufacturersTab from '../../../containers/parts/mechPartSource/tabs/ManufacturersTab';
import SuppliersTab from '../../../containers/parts/mechPartSource/tabs/SuppliersTab';
import ParamDataTab from '../../../containers/parts/mechPartSource/tabs/ParamDataTab';
import CadDataTab from './tabs/CadDataTab';
import UsagesTab from './tabs/UsagesTab';
import VerificationTab from './tabs/VerificationTab';
import PurchasingQuotesTab from '../../../containers/parts/mechPartSource/tabs/PurchasingQuotesTab';
import handleBackClick from '../../../helpers/handleBackClick';

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
    previousPaths,
    clearErrors
}) {
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const [mechPartSource, setMechPartSource] = useState(
        creating()
            ? {
                  proposedBy: userNumber,
                  proposedByName: userName,
                  dateEntered: new Date().toISOString(),
                  createPart: true,
                  mechPartAlts: [],
                  mechPartManufacturerAlts: [],
                  resistanceUnits: 'Ω',
                  capacitanceUnits: 'uF',
                  usages: [],
                  mechanicalOrElectrical: 'E',
                  samplesRequired: 'N',
                  part: { dataSheets: [] }
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
        quotes: 7,
        usages: 8,
        verification: 9
    };

    const [tab, setTab] = useState(options?.tab ? tabDictionary[options?.tab] : 0);

    const handleTabChange = (_, value) => {
        setTab(value);
    };

    useEffect(() => {
        if (item !== prevMechPartSource && editStatus !== 'create') {
            setMechPartSource({
                ...item,
                resistanceUnits: 'KΩ',
                capacitanceUnits: 'uF',
                mechPartManufacturerAlts: item?.mechPartManufacturerAlts?.map(m => ({
                    ...m,
                    id: m.sequence
                })),
                mechPartAlts: item?.mechPartAlts?.map(m => ({
                    ...m,
                    id: m.sequence
                }))
            });
            setPrevMechPartSource(item);
        }
    }, [item, prevMechPartSource, editStatus, itemId]);

    const {
        data: manufacturersData,
        addRow: addManufacturersRow,
        updateRow: updateManufacturersRow,
        removeRow: removeManufacturersRow,
        setEditing: setManufacturersEditing,
        setData: setManufacturersData,
        setRowToBeDeleted: setManufacturersRowToBeDeleted,
        setRowToBeSaved: setManufacturersRowToBeSaved
    } = useGroupEditTable({
        rows: mechPartSource?.mechPartManufacturerAlts,
        setEditStatus
    });

    const {
        data: quotesData,
        addRow: addQuotesRow,
        updateRow: updateQuotesRow,
        removeRow: removeQuotesRow,
        setEditing: setQuotesEditing,
        setData: setQuotesData,
        setRowToBeDeleted: setQuotesRowToBeDeleted,
        setRowToBeSaved: setQuotesRowToBeSaved
    } = useGroupEditTable({
        rows: mechPartSource?.purchasingQuotes,
        setEditStatus
    });

    const {
        data: usagesData,
        addRow: addUsagesRow,
        updateRow: updateUsagesRow,
        removeRow: removeUsagesRow,
        setEditing: setUsagesEditing,
        setRowToBeDeleted: setUsagesRowToBeDeleted,
        setRowToBeSaved: setUsagesRowToBeSaved
    } = useGroupEditTable({
        rows: mechPartSource?.usages,
        setEditStatus
    });

    const {
        data: suppliersData,
        addRow: addSuppliersRow,
        updateRow: updateSuppliersRow,
        removeRow: removeSuppliersRow,
        setEditing: setSuppliersEditing,
        setData: setSuppliersData,
        setRowToBeDeleted: setSuppliersRowToBeDeleted,
        setRowToBeSaved: setSuppliersRowToBeSaved
    } = useGroupEditTable({
        rows: mechPartSource?.mechPartAlts,
        setEditStatus
    });

    const handleSaveClick = () => {
        const body = mechPartSource;
        const rkmLetters = { KΩ: 'K', MΩ: 'M', Ω: '' };
        const capacitanceUnits = { uF: 'u', nF: 'n', pF: 'p' };
        clearErrors();
        body.resistanceUnits = rkmLetters[mechPartSource.resistanceUnits];
        body.capacitanceUnit = capacitanceUnits[mechPartSource.capacitanceUnits];
        body.mechPartAlts = suppliersData;
        body.mechPartManufacturerAlts = manufacturersData;
        console.log(mechPartSource.capacitance, typeof mechPartSource.capacitance);
        body.capicatance =
            typeof mechPartSource.capacitance === 'string'
                ? mechPartSource.capacitance
                : mechPartSource.capacitance?.toFixed(13);
        body.usages = usagesData?.map((u, i) => ({ ...u, id: i }));
        body.purchasingQuotes = quotesData;
        if (creating()) {
            addItem(body);
        } else {
            updateItem(itemId, body);
        }
        setEditStatus('view');
    };

    const mechPartSourceInvalid = () =>
        !mechPartSource.samplesRequired ||
        !mechPartSource.assemblyType ||
        (mechPartSource.mechanicalOrElectrical === 'E' && !mechPartSource.partType);

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
            setEditStatus('edit');
        }
        setMechPartSource({ ...mechPartSource, [propertyName]: newValue });
    };

    const handleVerificationFieldChange = (propertyName, newValue) => {
        setEditStatus('edit');

        // user will change Date field of some base property
        // remove Date from end of that propertyName to work out which property they changed
        const basePropertyName = propertyName.substring(0, propertyName.indexOf('Date'));

        // now upate the changeBy and changedByName fields that correspond to that date
        setMechPartSource({
            ...mechPartSource,
            [propertyName]: newValue,
            [`${basePropertyName}By`]: userNumber,
            [`${basePropertyName}ByName`]: userName
        });
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

    const resetRow = (current, collectionName, idFieldName) => {
        setMechPartSource(m => ({
            ...m,
            [collectionName]: [
                ...m[collectionName].map(a =>
                    a[idFieldName] === current.id
                        ? { ...item[collectionName]?.find(s => s[idFieldName] === current.id) }
                        : a
                )
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
        setSuppliersData(s =>
            s.map(x =>
                x.sequence === sequence
                    ? {
                          ...x,
                          supplierId: newValue.name,
                          supplierName: newValue.description,
                          editing: true
                      }
                    : x
            )
        );
    };

    const handleManufacturerChange = (sequence, newValue) => {
        setManufacturersData(m =>
            m.map(x => (x.sequence === sequence ? { ...x, manufacturerCode: newValue.name } : x))
        );
    };

    const handleQuotesManufacturerChange = (supplierId, newValue) => {
        setQuotesData(q =>
            q.map(x => (x.id === supplierId ? { ...x, manufacturerCode: newValue.name } : x))
        );
    };

    const handleQuotesSupplierChange = (supplierId, newValue) => {
        setQuotesData(q =>
            q.map(x =>
                x.id === supplierId
                    ? {
                          ...x,
                          supplierId: newValue.name,
                          supplierName: newValue.description
                      }
                    : x
            )
        );
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={10}>
                    {creating() ? (
                        <Title text="Create Part Source Sheet" />
                    ) : (
                        <Title text="Part Source Sheet Details" />
                    )}
                </Grid>
                {mechPartSource?.part?.id ? (
                    <Grid item xs={2}>
                        <LinkButton to={`/parts/${mechPartSource?.part?.id}`} text="PART RECORD" />
                    </Grid>
                ) : (
                    <Grid item xs={2} />
                )}
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard
                            errorMessage={
                                itemError?.details?.errors?.[0] ||
                                itemError?.details?.details ||
                                itemError?.statusText
                            }
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
                                variant="scrollable"
                                scrollButtons="on"
                                textColor="primary"
                                style={{ paddingBottom: '40px' }}
                            >
                                <Tab label="Proposal" />
                                <Tab
                                    label="DataSheets"
                                    disabled={mechPartSource.mechanicalOrElectrical !== 'E'}
                                />
                                <Tab label="Quality Requirements" />
                                <Tab label="Suppliers" />
                                <Tab label="Manufacturers" />
                                <Tab
                                    label="Param Data"
                                    disabled={mechPartSource.mechanicalOrElectrical !== 'E'}
                                />
                                <Tab label="Cad Data" />
                                <Tab label="Quotes" />
                                <Tab label="Usages" />
                                <Tab label="Verification" />
                            </Tabs>
                            {tab === 0 && (
                                <ProposalTab
                                    handleFieldChange={handleFieldChange}
                                    notes={mechPartSource.notes}
                                    partType={mechPartSource.partType}
                                    description={mechPartSource.description}
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
                                    setRowToBeDeleted={setSuppliersRowToBeDeleted}
                                    setRowToBeSaved={setSuppliersRowToBeSaved}
                                    setEditing={setSuppliersEditing}
                                    resetRow={row => resetRow(row, 'mechPartAlts', 'sequence')}
                                    removeRow={removeSuppliersRow}
                                    addRow={addSuppliersRow}
                                    updateRow={updateSuppliersRow}
                                    handleSupplierChange={handleSupplierChange}
                                    rows={suppliersData}
                                />
                            )}
                            {tab === 4 && mechPartSource.mechPartManufacturerAlts && (
                                <ManufacturersTab
                                    setRowToBeDeleted={setManufacturersRowToBeDeleted}
                                    setRowToBeSaved={setManufacturersRowToBeSaved}
                                    setEditing={setManufacturersEditing}
                                    resetRow={row =>
                                        resetRow(row, 'mechPartManufacturerAlts', 'id')
                                    }
                                    removeRow={removeManufacturersRow}
                                    addRow={addManufacturersRow}
                                    updateRow={updateManufacturersRow}
                                    handleApprovedByChange={handleApprovedByChange}
                                    handleManufacturerChange={handleManufacturerChange}
                                    rows={manufacturersData}
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
                                            : mechPartSource?.capacitanceLetterAndNumeralCode?.replace(
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
                                <PurchasingQuotesTab
                                    handleManufacturerChange={handleQuotesManufacturerChange}
                                    handleSupplierChange={handleQuotesSupplierChange}
                                    setRowToBeDeleted={setQuotesRowToBeDeleted}
                                    setRowToBeSaved={setQuotesRowToBeSaved}
                                    setEditing={setQuotesEditing}
                                    handleFieldChange={handleFieldChange}
                                    resetRow={row => resetRow(row, 'purchasingQuotes', 'id')}
                                    removeRow={removeQuotesRow}
                                    addRow={addQuotesRow}
                                    updateRow={updateQuotesRow}
                                    configuration={mechPartSource.configuration}
                                    lifeExpectancyPart={mechPartSource.lifeExpectancyPart}
                                    rows={quotesData}
                                />
                            )}
                            {tab === 8 && (
                                <UsagesTab
                                    setRowToBeDeleted={setUsagesRowToBeDeleted}
                                    setRowToBeSaved={setUsagesRowToBeSaved}
                                    setEditing={setUsagesEditing}
                                    resetRow={row => resetRow(row, 'usages', 'id')}
                                    removeRow={removeUsagesRow}
                                    addRow={addUsagesRow}
                                    updateRow={updateUsagesRow}
                                    rows={usagesData}
                                />
                            )}
                            {tab === 9 && (
                                <VerificationTab
                                    handleFieldChange={handleVerificationFieldChange}
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
                                    applyTCodeByName={mechPartSource.applyTCodeByName}
                                    applyTCodeDate={mechPartSource.applyTCodeDate}
                                    removeTCodeBy={mechPartSource.removeTCodeBy}
                                    removeTCodeByName={mechPartSource.removeTCodeByName}
                                    removeTCodeDate={mechPartSource.removeTCodeDate}
                                    cancelledBy={mechPartSource.cancelledBy}
                                    cancelledByName={mechPartSource.cancelledByName}
                                    cancelledDate={mechPartSource.cancelledDate}
                                    userNumber={userNumber}
                                    userName={userName}
                                />
                            )}
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing() || mechPartSourceInvalid()}
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

MechPartSource.propTypes = {
    item: PropTypes.shape({
        mechPartManufacturerAlts: PropTypes.arrayOf(PropTypes.shape({})),
        mechPartAlts: PropTypes.arrayOf(PropTypes.shape({}))
    }),
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
    previousPaths: PropTypes.arrayOf(PropTypes.string),
    clearErrors: PropTypes.func.isRequired
};

MechPartSource.defaultProps = {
    item: { mechPartManufacturerAlts: [], mechPartAlts: [] },
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
