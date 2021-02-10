import { connect } from 'react-redux';
import { ReportSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import DespatchPalletQueueReport from '../../components/reports/DespatchPalletQueueReport';
import actions from '../../actions/despatchPalletQueueReportActions';
import config from '../../config';
import * as reportTypes from '../../reportTypes';
import movePalletToUpperActions from '../../actions/movePalletToUpperActions';
import movePalletsToUpperActions from '../../actions/movePalletsToUpperActions';
import movePalletToUpperSelectors from '../../selectors/movePalletToUpperSelectors';
import movePalletsToUpperSelectors from '../../selectors/movePalletsToUpperSelectors';

const reportSelectors = new ReportSelectors(reportTypes.despatchPalletQueueReport.item);

const mapStateToProps = state => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    movePalletWorking: movePalletToUpperSelectors.getWorking(state),
    movePalletsWorking: movePalletsToUpperSelectors.getWorking(state),
    config
});

const initialise = ({ options }) => dispatch => {
    dispatch(actions.fetchReport(options));
};

const mapDispatchToProps = {
    initialise,
    movePalletToUpper: movePalletToUpperActions.requestProcessStart,
    movePalletsToUpper: movePalletsToUpperActions.requestProcessStart
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(DespatchPalletQueueReport));
