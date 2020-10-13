import { connect } from 'react-redux';
import {
    ReportSelectors,
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import WwdReport from '../../components/reports/WwdReport';
import actions from '../../actions/wwdReportActions';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.wwdReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    error: getItemErrorDetailMessage(state, reportTypes.wwdReport.item),
    options: getOptions(ownProps)
});

const initialise = ({ options }) => dispatch => {
    dispatch(actions.fetchReport(options));
};

const mapDispatchToProps = { initialise };

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(WwdReport));
