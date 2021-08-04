import * as actionTypes from '../actions';
import debitNotesActions from '../actions/purchasing/debitNotesActions';

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.debitNoteActionTypes.RECEIVE_UPDATED_DEBIT_NOTE:
            dispatch(debitNotesActions.fetch(action.payload.data, dispatch));
            break;
        default:
    }

    return result;
};
