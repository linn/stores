import { connect } from 'react-redux';
import {
    ReportSelectors,
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import StockLocatorReport from '../../components/reports/StockLocatorReport';
import actions from '../../actions/stockLocatorReportActions';
import * as reportTypes from '../../reportTypes';
import config from '../../config';

const reportSelectors = new ReportSelectors(reportTypes.stockLocatorReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    error: getItemErrorDetailMessage(state, reportTypes.stockLocatorReport.item),
    options: getOptions(ownProps),
    config
});

const mapDispatchToProps = {
    getReport: actions.fetchReport
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StockLocatorReport));
