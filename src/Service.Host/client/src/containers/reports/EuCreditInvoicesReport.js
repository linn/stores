import { connect } from 'react-redux';
import actions from '../../actions/euCreditInvoicesReportActions';
import config from '../../config';
import * as reportTypes from '../../reportTypes';
import EuCreditInvoicesReport from '../../components/reports/EuCreditInvoicesReport';

const mapStateToProps = state => ({
    reportData: state[reportTypes.euCreditInvoicesReport.item].data,
    loading: state[reportTypes.euCreditInvoicesReport.item].loading,
    config
});

const mapDispatchToProps = {
    getReport: actions.fetchReport
};

export default connect(mapStateToProps, mapDispatchToProps)(EuCreditInvoicesReport);
