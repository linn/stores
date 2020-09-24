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
import partCategories from './partCategories';
import productAnalysisCodes from './productAnalysisCodes';
import rootProducts from './rootProducts';
import sernosSequences from './sernosSequences';
import suppliers from './suppliers';
import unitsOfMeasure from './unitsOfMeasure';
import allocation from './allocation';
import * as itemTypes from '../itemTypes';

const errors = fetchErrorReducer({ ...itemTypes });

const rootReducer = combineReducers({
    accountingCompanies,
    assemblyTechnologies,
    allocation,
    departments,
    decrementRules,
    oidc,
    nominal,
    part,
    partCategories,
    parts,
    partTemplates,
    productAnalysisCodes,
    rootProducts,
    sernosSequences,
    suppliers,
    unitsOfMeasure,
    ...sharedLibraryReducers,
    errors
});

export default rootReducer;
