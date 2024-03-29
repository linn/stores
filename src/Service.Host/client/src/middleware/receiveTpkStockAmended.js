import * as actionTypes from '../actions';
import transferableStockActions from '../actions/transferableStockActions';

const refresh = (data, dispatch) => {
    if (data && data.success) {
        dispatch(transferableStockActions.fetch());
    }
};

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.unpickStockActionTypes.RECEIVE_UNPICK_STOCK:
        case actionTypes.unallocateReqActionTypes.RECEIVE_UNALLOCATE_REQ:
            refresh(action.payload.data, dispatch);
            break;
        default:
    }

    return result;
};
