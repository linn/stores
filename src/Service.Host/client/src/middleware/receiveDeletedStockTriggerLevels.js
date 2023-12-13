import * as actionTypes from '../actions';
import stockTriggerLevelActions from '../actions/stockTriggerLevelActions';

export default ({ dispatch }) => next => action => {
    console.log(actionTypes.stockTriggerLevelsActionTypes.RECEIVE_DELETED_STOCK_TRIGGER_LEVEL);
    const result = next(action);
    switch (action.type) {
        case actionTypes.stockTriggerLevelsActionTypes.RECEIVE_UPDATED_STOCK_TRIGGER_LEVEL:
        case actionTypes.stockTriggerLevelsActionTypes.RECEIVE_NEW_STOCK_TRIGGER_LEVEL:
        case actionTypes.stockTriggerLevelsActionTypes.RECEIVE_DELETED_STOCK_TRIGGER_LEVEL:
            console.log('CASE HIT');
            dispatch(
                stockTriggerLevelActions.searchWithOptions(
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
