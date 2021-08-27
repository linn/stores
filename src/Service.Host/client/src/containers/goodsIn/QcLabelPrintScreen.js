import { connect } from 'react-redux';

import QcLabelPrintScreen from '../../components/goodsIn/QcLabelPrintScreen';
import printGoodsInLabelsActions from '../../actions/printGoodsInLabelsActions';
import printGoodsInLabelsSelectors from '../../selectors/printGoodsInLabelsSelectors';

const mapStateToProps = state => ({
    printLabelsResult: printGoodsInLabelsSelectors.getData(state),
    printLabelsLoading: printGoodsInLabelsSelectors.getWorking(state)
});

const mapDispatchToProps = {
    printLabels: printGoodsInLabelsActions.requestProcessStart
};

export default connect(mapStateToProps, mapDispatchToProps)(QcLabelPrintScreen);
