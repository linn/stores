import * as actionTypes from '../actions';
import wandItemsActions from '../actions/wandItemsActions';

const fetchWandItems = (data, dispatch) => {
    if (data && data.consignmentId && data.success) {
        dispatch(wandItemsActions.search(data.consignmentId));
    }
};

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.doWandItemActionTypes.RECEIVE_DO_WAND_ITEM:
            fetchWandItems(action.payload.data, dispatch);
            break;
        default:
    }

    return result;
};
