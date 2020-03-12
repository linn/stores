import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';

const rootReducer = combineReducers({
    oidc,
    ...sharedLibraryReducers
});

export default rootReducer;
