import { utilities } from '@linn-it/linn-form-components-library';
import history from '../history';
import * as actionTypes from '../actions';

export default ({ getState }) => next => action => {
    const result = next(action);
    if (
        action.type.startsWith('RECEIVE_NEW_') &&
        action.type !== actionTypes.bomStandardPricesActionTypes.RECEIVE_NEW_BON_STANDARD_PRICES
    ) {
        if (!getState().router?.location?.pathname?.includes('/logistics/goods-in-utility')) {
            history.push(utilities.getSelfHref(action.payload.data));
        }
    }

    return result;
};
