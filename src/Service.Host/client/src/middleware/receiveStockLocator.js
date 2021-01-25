import * as actionTypes from '../actions';
import stockLocatorsActions from '../actions/stockLocatorsActions';

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.stockLocatorActionTypes.RECEIVE_UPDATED_STOCK_LOCATOR:
            console.log(action.payload);
            dispatch(stockLocatorsActions.searchWithOptions(null, `&partNumber=${action.payload.data.partNumber}`));
            break;
        default:
    }

    return result;
};
