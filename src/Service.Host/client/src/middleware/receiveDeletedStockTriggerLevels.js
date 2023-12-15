import * as actionTypes from '../actions';
import stockTriggerLevelsActions from '../actions/stockTriggerLevelsActions';

export default ({ dispatch }) => next => action => {
    const result = next(action);
    switch (action.type) {
        case actionTypes.stockTriggerLevelActionTypes.RECEIVE_UPDATED_STOCK_TRIGGER_LEVEL:
        case actionTypes.stockTriggerLevelActionTypes.RECEIVE_NEW_STOCK_TRIGGER_LEVEL:
        case actionTypes.stockTriggerLevelActionTypes.RECEIVE_DELETED_STOCK_TRIGGER_LEVEL:
            dispatch(
                stockTriggerLevelsActions.searchWithOptions(
                    null,
                    `&partNumberSearchTerm=${action.payload.data.partNumber}&storagePlaceSearchTerm=${action.payload.data.storagePlace}`,

                    dispatch
                )
            );

            break;
        default:
    }

    return result;
};
