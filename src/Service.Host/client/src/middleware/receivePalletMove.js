import * as actionTypes from '../actions';
import actions from '../actions/despatchPalletQueueReportActions';

const fetchProcessDetails = dispatch => {
    dispatch(actions.fetchReport({}));
};

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.movePalletToUpperActionTypes.RECEIVE_MOVE_PALLET_TO_UPPER:
        case actionTypes.movePalletsToUpperActionTypes.RECEIVE_MOVE_PALLETS_TO_UPPER:
            fetchProcessDetails(dispatch);
            break;
        default:
    }

    return result;
};
