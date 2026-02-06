import { RSAA } from 'redux-api-middleware';
import { getAccessToken } from '../selectors/getAccessToken';
import { addressesActionTypes as actionTypes } from '../actions/index';
import { addressesActionTypes } from '../actions';

export default ({ getState }) => next => action => {
    if (action[RSAA]) {
        if (action[RSAA].options 
            && action[RSAA].options.requiresAuth 
            && action.types?.includes(addressesActionTypes.REQUEST_SEARCH_ADDRESSES)) {
            // eslint-disable-next-line no-param-reassign
            action[RSAA].headers = {
                Authorization: `Bearer ${getAccessToken(getState())}`,
                ...action[RSAA].headers
            };
        }
    }

    return next(action);
};
