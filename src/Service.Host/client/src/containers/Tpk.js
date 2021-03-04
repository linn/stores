import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import Tpk from '../components/Tpk';
import transferableStockSelectors from '../selectors/transferableStockSelectors';
import transferableStockActions from '../actions/transferableStockActions';

const mapStateToProps = state => ({
    transferableStock: transferableStockSelectors.getItems(state),
    transferableStockLoading: transferableStockSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(transferableStockActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Tpk));
