import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import TqmsSummaryByCategoryReportOptions from '../../components/reports/TqmsSummaryByCategoryReportOptions';
import tqmsJobRefsActions from '../../actions/tqmsJobRefsActions';
import tqmsJobRefsSelectors from '../../selectors/tqmsJobRefsSelectors';

const mapStateToProps = state => ({
    jobRefs: tqmsJobRefsSelectors.getItems(state),
    loading: tqmsJobRefsSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(tqmsJobRefsActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withRouter(initialiseOnMount(TqmsSummaryByCategoryReportOptions)));
