import * as actionTypes from '../actions';
import wandConsignmentsActions from '../actions/wandConsignmentsActions';
import wandItemsActions from '../actions/wandItemsActions';

const fetchRemainingConsignments = (dispatch, data) => {
    if (data.success) {
        dispatch(wandConsignmentsActions.fetch());
    }
};

const fetchRemainingItems = (dispatch, data) => {
    if (data.requisitionHeader && data.requisitionHeader.document1 && data.success) {
        dispatch(wandItemsActions.search(data.requisitionHeader.document1));
    }
};

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.unallocateConsignmentActionTypes.RECEIVE_UNALLOCATE_CONSIGNMENT:
            fetchRemainingConsignments(dispatch, action.payload.data);
            break;
        case actionTypes.unallocateConsignmentLineActionTypes.RECEIVE_UNALLOCATE_CONSIGNMENT_LINE:
            fetchRemainingItems(dispatch, action.payload.data);
            break;
        default:
    }

    return result;
};
