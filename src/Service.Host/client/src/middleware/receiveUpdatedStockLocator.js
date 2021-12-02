import * as actionTypes from '../actions';
import stockLocatorsActions from '../actions/stockLocatorsActions';

export default ({ dispatch }) => next => action => {
    const result = next(action);
    switch (action.type) {
        case actionTypes.stockLocatorActionTypes.RECEIVE_UPDATED_STOCK_LOCATOR:
        case actionTypes.stockLocatorActionTypes.RECEIVE_NEW_STOCK_LOCATOR:
        case actionTypes.stockLocatorActionTypes.RECEIVE_DELETED_LOCATOR:
            if (action.payload.partId) {
                dispatch(
                    stockLocatorsActions.searchWithOptions(
                        null,
                        `&partId=${action.payload.partId}`,
                        dispatch
                    )
                );
            }

            break;
        default:
    }

    return result;
};
