import * as actionTypes from '../actions';
import consignmentShipfilesActions from '../actions/consignmentShipfilesActions';

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.consignmentShipfileActionTypes.RECEIVE_DELETED_CONSIGNMENT_SHIPFILE:
            dispatch(consignmentShipfilesActions.fetch(action.payload.data, dispatch));
            break;
        case actionTypes.consignmentShipfileActionTypes.RECEIVE_NEW_CONSIGNMENT_SHIPFILE:
            dispatch(consignmentShipfilesActions.fetch(action.payload.data, dispatch));
            break;
        default:
    }

    return result;
};
