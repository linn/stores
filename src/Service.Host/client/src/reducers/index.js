import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import departments from './departments';
import accountingCompanies from './accountingCompanies';
import nominal from './nominal';
import parts from './parts/parts';
import part from './parts/part';
import partCategories from './partCategories';
import productAnalysisCodes from './productAnalysisCodes';
import rootProducts from './rootProducts';
import sernosSequences from './sernosSequences';
import suppliers from './suppliers';
import unitsOfMeasure from './unitsOfMeasure';
import allocation from './allocation';

const rootReducer = combineReducers({
    accountingCompanies,
    allocation,
    departments,
    oidc,
    nominal,
    part,
    partCategories,
    parts,
    productAnalysisCodes,
    rootProducts,
    sernosSequences,
    suppliers,
    unitsOfMeasure,
    ...sharedLibraryReducers
});

export default rootReducer;
