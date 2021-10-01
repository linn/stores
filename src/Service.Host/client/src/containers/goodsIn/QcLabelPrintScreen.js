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
              orderNumber: 57190,
              reqNumber: 456789,
              partNumber: 'PCB 002/L1',
              partDescription: 'A TEST LABEL',
              qtyReceived: queryString.parse(location?.search)?.qtyReceived,
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
