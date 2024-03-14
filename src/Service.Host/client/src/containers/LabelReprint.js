import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import printConsignmentLabelActions from '../actions/printConsignmentLabelActions';
import printConsignmentLabelSelectors from '../selectors/printConsignmentLabelSelectors';
import { getUserNumber } from '../selectors/userSelectors';
import LabelReprint from '../components/LabelReprint';

const mapStateToProps = state => ({
    userNumber: getUserNumber(state),
    printConsignmentLabelWorking: printConsignmentLabelSelectors.getWorking(state),
    printConsignmentLabelResult: printConsignmentLabelSelectors.getData(state)
});

const mapDispatchToProps = {
    printConsignmentLabel: printConsignmentLabelActions.requestProcessStart,
    clearConsignmentLabelData: printConsignmentLabelActions.clearProcessData
};

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(LabelReprint));
