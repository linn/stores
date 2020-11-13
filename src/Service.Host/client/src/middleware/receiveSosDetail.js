import * as actionTypes from '../actions';
import sosAllocDetailsActions from '../actions/sosAllocDetailsActions';

const fetchDetails = (data, dispatch) => {
    const { jobId } = data;
    if (jobId) {
        dispatch(sosAllocDetailsActions.searchWithOptions(null, `&jobId=${jobId}`));
    }
};

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.sosAllocDetailActionTypes.RECEIVE_UPDATED_SOS_ALLOC_DETAIL:
            fetchDetails(action.payload.data, dispatch);
            break;
        default:
    }

    return result;
};
