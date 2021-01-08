import * as actionTypes from '../actions';
import sosAllocDetailsActions from '../actions/sosAllocDetailsActions';

const fetchDetails = (data, dispatch) => {
    const { jobId } = data;
    if (jobId) {
        dispatch(sosAllocDetailsActions.searchWithOptions(null, `&jobId=${jobId}`));
    }
};

const fetchProcessDetails = (data, dispatch) => {
    if (data && data.length) {
        const { jobId } = data[0];
        if (jobId) {
            dispatch(sosAllocDetailsActions.searchWithOptions(null, `&jobId=${jobId}`));
        }
    }
};

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.sosAllocDetailActionTypes.RECEIVE_UPDATED_SOS_ALLOC_DETAIL:
            fetchDetails(action.payload.data, dispatch);
            break;
        case actionTypes.pickItemsAllocationActionTypes.RECEIVE_PICK_ITEMS_ALLOCATION:
        case actionTypes.unpickItemsAllocationActionTypes.RECEIVE_UNPICK_ITEMS_ALLOCATION:
            fetchProcessDetails(action.payload.data, dispatch);
            break;
        default:
    }

    return result;
};
