import * as actionTypes from '../actions';
import consignmentShipfilesActions from '../actions/consignmentShipfilesActions';

export default ({ dispatch }) => next => action => {
    const result = next(action);

    switch (action.type) {
        case actionTypes.consignmentShipfileActionTypes.RECEIVE_DELETED_CONSIGNMENT_SHIPFILE:
            console.log('we caught you');
            dispatch(consignmentShipfilesActions.fetch(action.payload.data, dispatch));
            break;
        default:
    }

    return result;
};
