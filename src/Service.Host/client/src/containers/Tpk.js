import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import Tpk from '../components/Tpk';
import transferableStockSelectors from '../selectors/transferableStockSelectors';
import transferableStockActions from '../actions/transferableStockActions';
import tpkActions from '../actions/tpkTransferStockActions';
import tpkSelectors from '../selectors/tpkTransferStockSelectors';

const mapStateToProps = state => ({
    transferableStock: transferableStockSelectors.getItems(state),
    transferableStockLoading: transferableStockSelectors.getLoading(state),
    transferredStock: tpkSelectors.getData(state)?.transferred
});

const initialise = () => dispatch => {
    dispatch(transferableStockActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    transferStock: tpkActions.requestProcessStart
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Tpk));
