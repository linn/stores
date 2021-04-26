import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import Tpk from '../components/tpk/Tpk';
import transferableStockSelectors from '../selectors/transferableStockSelectors';
import transferableStockActions from '../actions/transferableStockActions';
import tpkActions from '../actions/tpkTransferStockActions';
import tpkSelectors from '../selectors/tpkTransferStockSelectors';
import * as processTypes from '../processTypes';

const mapStateToProps = state => ({
    transferableStock: transferableStockSelectors.getItems(state),
    transferableStockLoading: transferableStockSelectors.getLoading(state),
    itemError: getItemError(state, processTypes.tpkTransferStock.item),
    transferredStock: tpkSelectors.getData(state)?.transferred,
    whatToWandReport: tpkSelectors.getData(state)?.whatToWandReport,
    tpkLoading: tpkSelectors.getWorking(state)
});

const initialise = () => dispatch => {
    dispatch(transferableStockActions.fetch());
    tpkActions.clearErrorsForItem();
};

const mapDispatchToProps = {
    initialise,
    transferStock: tpkActions.requestProcessStart,
    clearErrors: tpkActions.clearErrorsForItem,
    clearData: tpkActions.clearProcessData
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Tpk));
