import { connect } from 'react-redux';
import validatePurchaseOrderActions from '../../actions/validatePurchaseOrderActions';
import GoodsInUtility from '../../components/goodsIn/GoodsInUtility';
import validatePurchaseOrderResultSelectors from '../../selectors/validatePurchaseOrderResultSelectors';

const mapStateToProps = state => ({
    validatePurchaseOrderResult: validatePurchaseOrderResultSelectors.getItem(state)
});

const mapDispatchToProps = {
    validatePurchaseOrder: validatePurchaseOrderActions.fetchById
};

export default connect(mapStateToProps, mapDispatchToProps)(GoodsInUtility);
