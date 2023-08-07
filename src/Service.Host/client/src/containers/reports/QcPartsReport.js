import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import QcPartsReport from '../../components/reports/QcPartsReport';
import actions from '../../actions/qcPartsReportActions';
import * as reportTypes from '../../reportTypes';
import config from '../../config';

const mapStateToProps = state => ({
    reportData: state[reportTypes.qcPartsReport.item].data,
    loading: state[reportTypes.qcPartsReport.item].loading,
    config
});

const initialise = () => dispatch => {
    dispatch(actions.fetchReport());
};

const mapDispatchToProps = { initialise };

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(QcPartsReport));
