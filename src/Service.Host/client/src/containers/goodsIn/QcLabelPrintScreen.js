import { connect } from 'react-redux';
import QcLabelPrintScreen from '../../components/goodsIn/QcLabelPrintScreen';
import printGoodsInLabelsActions from '../../actions/printGoodsInLabelsActions';
import printGoodsInLabelsSelectors from '../../selectors/printGoodsInLabelsSelectors';
import reqActions from '../../actions/reqActions';
import reqSelectors from '../../selectors/reqSelectors';

const mapStateToProps = (state, { match }) => {
    const props = {
        printLabelsResult: printGoodsInLabelsSelectors.getData(state),
        printLabelsLoading: printGoodsInLabelsSelectors.getWorking(state)
    };
    return match?.url?.endsWith('/labels')
        ? {
              ...props,
              req: reqSelectors.getItem(state),
              initialNumContainers: null
          }
        : {
              ...props,
              initialNumContainers: 1
          };
};

const mapDispatchToProps = {
    printLabels: printGoodsInLabelsActions.requestProcessStart,
    fetchReq: reqActions.fetch
};

export default connect(mapStateToProps, mapDispatchToProps)(QcLabelPrintScreen);
