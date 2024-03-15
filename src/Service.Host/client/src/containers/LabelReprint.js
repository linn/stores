import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import printConsignmentLabelActions from '../actions/printConsignmentLabelActions';
import printConsignmentLabelSelectors from '../selectors/printConsignmentLabelSelectors';
import consignmentSelectors from '../selectors/consignmentSelectors';
import consignmentActions from '../actions/consignmentActions';
import { getUserNumber } from '../selectors/userSelectors';
import LabelReprint from '../components/LabelReprint';

const mapStateToProps = state => ({
    userNumber: getUserNumber(state),
    printConsignmentLabelWorking: printConsignmentLabelSelectors.getWorking(state),
    printConsignmentLabelResult: printConsignmentLabelSelectors.getData(state),
    consignmentItem: consignmentSelectors.getItem(state),
    consignmentLoading: consignmentSelectors.getLoading(state)
});

const mapDispatchToProps = {
    printConsignmentLabel: printConsignmentLabelActions.requestProcessStart,
    clearConsignmentLabelData: printConsignmentLabelActions.clearProcessData,
    getConsignment: consignmentActions.fetch
};

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(LabelReprint));
