import { connect } from 'react-redux';
import {
    ReportSelectors,
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import config from '../../config';
import StoresMoveLogReport from '../../components/reports/StoresMoveLogReport';
import actions from '../../actions/storesMoveLogReportActions';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.storesMoveLogReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => ({
    reportData: reportSelectors.getReportData(state),
    loading: reportSelectors.getReportLoading(state),
    error: getItemErrorDetailMessage(state, reportTypes.storesMoveLogReport.item),
    options: getOptions(ownProps),
    config
});

const initialise = ({ options }) => dispatch => {
    dispatch(actions.fetchReport(options));
};

const mapDispatchToProps = { initialise };

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StoresMoveLogReport));
