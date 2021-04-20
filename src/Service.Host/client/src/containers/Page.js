import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { Page } from '@linn-it/linn-form-components-library';
import config from '../config';

const mapStateToProps = (state, ownProps) => ({
    homeUrl: config.appRoot,
    width: ownProps.width
});

const mapDispatchToProps = {};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Page));
