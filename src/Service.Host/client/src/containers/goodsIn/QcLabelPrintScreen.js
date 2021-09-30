import { connect } from 'react-redux';
import queryString from 'query-string';
import QcLabelPrintScreen from '../../components/goodsIn/QcLabelPrintScreen';
import printGoodsInLabelsActions from '../../actions/printGoodsInLabelsActions';
import printGoodsInLabelsSelectors from '../../selectors/printGoodsInLabelsSelectors';

const mapStateToProps = (state, { match, location }) =>
    match?.url?.endsWith('/test-labels')
        ? {
              printLabelsResult: printGoodsInLabelsSelectors.getData(state),
              printLabelsLoading: printGoodsInLabelsSelectors.getWorking(state),
              docType: 'PO',
              orderNumber: 123456,
              reqNumber: 456789,
              partNumber: 'TEST PART',
              partDescription: 'A TEST LABEL',
              qtyReceived: 1,
              unitOfMeasure: 'ONES',
              qcInfo: 'QC INFO GOES HERE',
              kardexLocation: queryString.parse(location?.search)?.kardexLocation,
              qcState: queryString.parse(location?.search)?.qcState
          }
        : {
              printLabelsResult: printGoodsInLabelsSelectors.getData(state),
              printLabelsLoading: printGoodsInLabelsSelectors.getWorking(state)
          };

const mapDispatchToProps = {
    printLabels: printGoodsInLabelsActions.requestProcessStart
};

export default connect(mapStateToProps, mapDispatchToProps)(QcLabelPrintScreen);
