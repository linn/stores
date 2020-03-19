import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import parts from './parts/parts';
import part from './parts/part';

const rootReducer = combineReducers({
    oidc,
    part,
    parts,
    ...sharedLibraryReducers
});

export default rootReducer;
