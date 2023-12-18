import { utilities } from '@linn-it/linn-form-components-library';
import history from '../history';
import * as actionTypes from '../actions';

export default ({ getState }) => next => action => {
    const result = next(action);
    if (
        action.type.startsWith('RECEIVE_NEW_') &&
        action.type !== actionTypes.bomStandardPricesActionTypes.RECEIVE_NEW_BOM_STANDARD_PRICES &&
        action.type !== actionTypes.stockTriggerLevelActionTypes.RECEIVE_NEW_STOCK_TRIGGER_LEVEL
    ) {
        if (!getState().router?.location?.pathname?.includes('/logistics/goods-in-utility')) {
            history.push(utilities.getSelfHref(action.payload.data));
        }
    }

    return result;
};
