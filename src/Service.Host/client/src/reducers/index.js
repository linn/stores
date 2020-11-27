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
import mechPartSource from './parts/mechPartSource';

const errors = fetchErrorReducer({ ...itemTypes });

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
    despatchLocations,
    mechPartSource,
    nominal,
    oidc,
    part,
    partCategories,
    parts,
    partLiveTest,
    partTemplates,
    productAnalysisCodes,
    rootProducts,
    sernosSequences,
    sosAllocHeads,
    stockPools,
    storagePlace,
    storagePlaces,
    storagePlaceAuditReport,
    suppliers,
    unitsOfMeasure,
    wwdReport,
    ...sharedLibraryReducers,
    errors
});

export default rootReducer;
