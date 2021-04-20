import * as actionTypes from '../actions';
import exportReturnActions from '../actions/exportReturnActions';

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.makeIntercompanyInvoicesActionTypes.RECEIVE_MAKE_INTERCOMPANY_INVOICES: {
            const { returnId } = action.payload.data;
            if (returnId) {
                dispatch(exportReturnActions.fetch(returnId));
            }

            break;
        }

        default:
    }

    return result;
};
