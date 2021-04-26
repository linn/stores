import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import TqmsSummaryByCategoryReportOptions from '../../components/reports/TqmsSummaryByCategoryReportOptions';

const mapStateToProps = () => ({});

const initialise = () => () => {};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withRouter(initialiseOnMount(TqmsSummaryByCategoryReportOptions)));
