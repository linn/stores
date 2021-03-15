import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { Page } from '@linn-it/linn-form-components-library';

const mapStateToProps = (state, ownProps) => ({
    width: ownProps.width
});

const mapDispatchToProps = {};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Page));
