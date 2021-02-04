import {
    reducers as sharedLibraryReducers,
    fetchErrorReducer
} from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import assemblyTechnologies from './parts/assemblyTechnologies';
import decrementRules from './parts/decrementRules';
import departments from './departments';
import accountingCompanies from './accountingCompanies';
import nominal from './nominal';
import parts from './parts/parts';
import part from './parts/part';
import partTemplates from './parts/partTemplates';
import partLiveTest from './parts/partLiveTest';
import partCategories from './partCategories';
import productAnalysisCodes from './productAnalysisCodes';
import rootProducts from './rootProducts';
import sernosSequences from './sernosSequences';
import suppliers from './suppliers';
import unitsOfMeasure from './unitsOfMeasure';
import allocation from './allocation';
import wwdReport from './wwdReport';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';
import * as processTypes from '../processTypes';
import stockPools from './stockPools';
import despatchLocations from './despatchLocations';
import countries from './countries';
import storagePlaceAuditReport from './storagePlaceAuditReport';
import storagePlace from './storagePlace';
import storagePlaces from './storagePlaces';
import auditLocation from './auditLocation';
import auditLocations from './auditLocations';
import createAuditReqs from './createAuditReqs';
import sosAllocHeads from './sosAllocHeads';
import sosAllocDetails from './sosAllocDetails';
import mechPartSource from './parts/mechPartSource';
import manufacturers from './manufacturers';
import employees from './employees';
import partDataSheetValues from './partDataSheetValues';
import finishAllocation from './finishAllocation';
import pickItemsAllocation from './pickItemsAllocation';
import unpickItemsAllocation from './unpickItemsAllocation';
import tqmsCategories from './tqmsCategories';
import workstationTopUpStatus from './workstationTopUpStatus';
import deptStockParts from './parts/deptStockParts';
import stockLocators from './stockLocators/stockLocators';
import stockLocator from './stockLocators/stockLocator';
import despatchPickingSummaryReport from './despatchPickingSummaryReport';
import stockLocatorBatches from './stockLocators/stockLocatorBatches';
import storageLocations from './stockLocators/storageLocations';
import inspectedStates from './stockLocators/inspectedStates';
import despatchPalletQueueReport from './despatchPalletQueueReport';

const errors = fetchErrorReducer({ ...itemTypes, ...reportTypes, ...processTypes });

const rootReducer = combineReducers({
    accountingCompanies,
    allocation,
    assemblyTechnologies,
    auditLocation,
    auditLocations,
    countries,
    createAuditReqs,
    departments,
    decrementRules,
    deptStockParts,
    despatchLocations,
    despatchPalletQueueReport,
    despatchPickingSummaryReport,
    employees,
    finishAllocation,
    inspectedStates,
    manufacturers,
    mechPartSource,
    nominal,
    oidc,
    part,
    partCategories,
    parts,
    partDataSheetValues,
    partLiveTest,
    partTemplates,
    pickItemsAllocation,
    productAnalysisCodes,
    rootProducts,
    sernosSequences,
    sosAllocDetails,
    sosAllocHeads,
    stockLocator,
    stockLocatorBatches,
    stockLocators,
    stockPools,
    storageLocations,
    storagePlace,
    storagePlaces,
    storagePlaceAuditReport,
    suppliers,
    tqmsCategories,
    unitsOfMeasure,
    unpickItemsAllocation,
    workstationTopUpStatus,
    wwdReport,
    ...sharedLibraryReducers,
    errors
});

export default rootReducer;
