import { makeActionTypes, makeReportActionTypes } from '@linn-it/linn-form-components-library';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';

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

export const sosAllocHeadsActionTypes = makeActionTypes(itemTypes.sosAllocHeads.actionType);

export const mechPartSourceActionTypes = makeActionTypes(itemTypes.mechPartSource.actionType);

export const manufacturersActionTypes = makeActionTypes(itemTypes.manufacturers.actionType);

export const employeesActionTypes = makeActionTypes(itemTypes.employees.actionType);

export const partDataSheetValuesActionTypes = makeActionTypes(
    itemTypes.partDataSheetValues.actionType
);
