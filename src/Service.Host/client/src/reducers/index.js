import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import parts from './parts/parts';
import part from './parts/part';
import departments from './departments';
import partCategories from './partCategories';
import rootProducts from './rootProducts';
import sernosSequences from './sernosSequences';
import suppliers from './suppliers';

const rootReducer = combineReducers({
    departments,
    oidc,
    part,
    partCategories,
    parts,
    rootProducts,
    sernosSequences,
    suppliers,
    ...sharedLibraryReducers
});

export default rootReducer;
