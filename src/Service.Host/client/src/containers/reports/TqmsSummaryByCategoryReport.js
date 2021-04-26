import { connect } from 'react-redux';
import {
    ReportSelectors,
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import TqmsSummaryByCategoryReport from '../../components/reports/TqmsSummaryByCategoryReport';
import actions from '../../actions/tqmsSummaryByCategoryReportActions';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.tqmsSummaryByCategoryReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    error: getItemErrorDetailMessage(state, reportTypes.tqmsSummaryByCategoryReport.item),
    options: getOptions(ownProps)
});

const initialise = ({ options }) => dispatch => {
    dispatch(actions.fetchReport(options));
};

const mapDispatchToProps = { initialise };

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(TqmsSummaryByCategoryReport));
