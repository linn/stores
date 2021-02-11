import {
    makeActionTypes,
    makeReportActionTypes,
    makeProcessActionTypes
} from '@linn-it/linn-form-components-library';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';
import * as processTypes from '../processTypes';

export const partActionTypes = makeActionTypes(itemTypes.part.actionType);
export const partsActionTypes = makeActionTypes(itemTypes.parts.actionType, false);

export const accountingCompaniesActionTypes = makeActionTypes(
    itemTypes.accountingCompanies.actionType
);

export const departmentsActionTypes = makeActionTypes(itemTypes.departments.actionType);

export const rootProductsActionTypes = makeActionTypes(itemTypes.rootProducts.actionType);

export const partCategoriesActionTypes = makeActionTypes(itemTypes.partCategories.actionType);

export const partTemplatesActionTypes = makeActionTypes(itemTypes.partTemplates.actionType);

export const partLiveTestActionTypes = makeActionTypes(itemTypes.partLiveTest.actionType);

export const suppliersActionTypes = makeActionTypes(itemTypes.suppliers.actionType);

export const carriersActionTypes = makeActionTypes(itemTypes.carriers.actionType);

export const sernosSequencesActionTypes = makeActionTypes(itemTypes.sernosSequences.actionType);

export const unitsOfMeasureActionTypes = makeActionTypes(itemTypes.unitsOfMeasure.actionType);

export const allocationActionTypes = makeActionTypes(itemTypes.allocation.actionType);

export const productAnalysisCodesActionTypes = makeActionTypes(
    itemTypes.productAnalysisCodes.actionType
);

export const nominalActionTypes = makeActionTypes(itemTypes.nominal.actionType);

export const decrementRulesActionTypes = makeActionTypes(itemTypes.decrementRules.actionType);

export const assemblyTechnologiesActionTypes = makeActionTypes(
    itemTypes.assemblyTechnologies.actionType
);

export const wwdReportActionTypes = makeReportActionTypes(reportTypes.wwdReport.actionType);

export const stockPoolsActionTypes = makeActionTypes(itemTypes.stockPools.actionType);

export const despatchLocationsActionTypes = makeActionTypes(itemTypes.despatchLocations.actionType);

export const countriesActionTypes = makeActionTypes(itemTypes.countries.actionType);

export const storagePlaceActionTypes = makeActionTypes(itemTypes.storagePlace.actionType);

export const storagePlacesActionTypes = makeActionTypes(itemTypes.storagePlaces.actionType);

export const auditLocationActionTypes = makeActionTypes(itemTypes.auditLocation.actionType);

export const auditLocationsActionTypes = makeActionTypes(itemTypes.auditLocations.actionType);

export const storagePlaceAuditReportActionTypes = makeReportActionTypes(
    reportTypes.storagePlaceAuditReport.actionType
);

export const createAuditReqsActionTypes = makeProcessActionTypes(
    processTypes.createAuditReqs.actionType
);

export const sosAllocHeadsActionTypes = makeActionTypes(itemTypes.sosAllocHeads.actionType);

export const sosAllocDetailActionTypes = makeActionTypes(itemTypes.sosAllocDetail.actionType);

export const sosAllocDetailsActionTypes = makeActionTypes(itemTypes.sosAllocDetails.actionType);

export const mechPartSourceActionTypes = makeActionTypes(itemTypes.mechPartSource.actionType);

export const manufacturersActionTypes = makeActionTypes(itemTypes.manufacturers.actionType);

export const employeesActionTypes = makeActionTypes(itemTypes.employees.actionType);

export const partDataSheetValuesActionTypes = makeActionTypes(
    itemTypes.partDataSheetValues.actionType
);

export const finishAllocationActionTypes = makeActionTypes(
    processTypes.finishAllocation.actionType
);

export const pickItemsAllocationActionTypes = makeActionTypes(
    processTypes.pickItemsAllocation.actionType
);

export const unpickItemsAllocationActionTypes = makeActionTypes(
    processTypes.unpickItemsAllocation.actionType
);

export const tqmsCategoriesActionTypes = makeActionTypes(itemTypes.tqmsCategories.actionType);

export const workstationTopUpStatusActionTypes = makeActionTypes(
    itemTypes.workstationTopUpStatus.actionType
);

export const deptStockPartsActionTypes = makeActionTypes(itemTypes.deptStockParts.actionType);

export const stockLocatorsActionTypes = makeActionTypes(itemTypes.stockLocators.actionType);

export const stockLocatorActionTypes = makeActionTypes(
    itemTypes.stockLocator.actionType,
    true,
    true
);

export const despatchPickingSummaryReportActionTypes = makeReportActionTypes(
    reportTypes.despatchPickingSummaryReport.actionType
);

export const despatchPalletQueueReportActionTypes = makeReportActionTypes(
    reportTypes.despatchPalletQueueReport.actionType
);

export const stockLocatorBatchesActionTypes = makeActionTypes(
    itemTypes.stockLocatorBatches.actionType
);

export const storageLocationsActionTypes = makeActionTypes(itemTypes.storageLocations.actionType);

export const inspectedStatesActionTypes = makeActionTypes(itemTypes.inspectedStates.actionType);

export const movePalletToUpperActionTypes = makeActionTypes(
    processTypes.movePalletToUpper.actionType
);

export const movePalletsToUpperActionTypes = makeActionTypes(
    processTypes.movePalletsToUpper.actionType
);

export const parcelActionTypes = makeActionTypes(itemTypes.parcel.actionType);

export const parcelsActionTypes = makeActionTypes(itemTypes.parcels.actionType, false);
