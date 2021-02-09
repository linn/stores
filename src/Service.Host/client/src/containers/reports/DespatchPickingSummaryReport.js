import { connect } from 'react-redux';
import { ReportSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import DespatchPickingSummaryReport from '../../components/reports/DespatchPickingSummaryReport';
import actions from '../../actions/despatchPickingSummaryReportActions';
import config from '../../config';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.despatchPickingSummaryReport.item);

const mapStateToProps = (state, ownProps) => ({
    runOptions: queryString.parse(ownProps.location.search),
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    config
});

const initialise = ({ options }) => dispatch => {
    dispatch(actions.fetchReport(options));
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(DespatchPickingSummaryReport));
