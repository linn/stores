import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import Tpk from '../components/tpk/Tpk';
import transferableStockSelectors from '../selectors/transferableStockSelectors';
import transferableStockActions from '../actions/transferableStockActions';
import tpkActions from '../actions/tpkTransferStockActions';
import tpkSelectors from '../selectors/tpkTransferStockSelectors';
import unpickStockSelectors from '../selectors/unpickStockSelectors';
import unallocateReqSelectors from '../selectors/uanllocateReqSelectors';
import * as processTypes from '../processTypes';
import unpickStockActions from '../actions/unpickStockActions';
import unallocateReqActions from '../actions/unallocateReqActions';

const mapStateToProps = state => ({
    transferableStock: transferableStockSelectors.getItems(state),
    transferableStockLoading: transferableStockSelectors.getLoading(state),
    itemError: getItemError(state, processTypes.tpkTransferStock.item),
    unpickError: getItemError(state, processTypes.unpickStock.item),
    unallocateError: getItemError(state, processTypes.unallocateReq.item),
    transferredStock: tpkSelectors.getData(state)?.transferred,
    whatToWandReport: tpkSelectors.getData(state)?.whatToWandReport,
    tpkLoading: tpkSelectors.getWorking(state),
    unpickStockLoading: unpickStockSelectors.getWorking(state),
    unpickStockResult: unpickStockSelectors.getData(state),
    unallocateReqLoading: unallocateReqSelectors.getWorking(state),
    unallocateReqResult: unallocateReqSelectors.getData(state)
});

const initialise = () => dispatch => {
    dispatch(transferableStockActions.fetch());
    tpkActions.clearErrorsForItem();
    unpickStockActions.clearErrorsForItem();
    unallocateReqActions.clearErrorsForItem();
};

const mapDispatchToProps = {
    initialise,
    transferStock: tpkActions.requestProcessStart,
    unpickStock: unpickStockActions.requestProcessStart,
    unallocateReq: unallocateReqActions.requestProcessStart,
    clearUnpickErrors: unpickStockActions.clearErrorsForItem,
    clearUnallocateErrors: unallocateReqActions.clearErrorsForItem,
    clearErrors: tpkActions.clearErrorsForItem,
    clearData: tpkActions.clearProcessData
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Tpk));
