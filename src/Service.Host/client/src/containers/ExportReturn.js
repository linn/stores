import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import exportReturnActions from '../actions/exportReturnActions';
import exportReturnSelectors from '../selectors/exportReturnSelectors';
import ExportReturn from '../components/ExportReturn';

const mapStateToProps = (state, { match }) => ({
    exportReturnId: match.params.id,
    exportReturnLoading: exportReturnSelectors.getLoading(state),
    exportReturn: exportReturnSelectors.getItem(state)
});

const initialise = ({ exportReturnId }) => dispatch => {
    dispatch(exportReturnActions.fetch(exportReturnId));
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ExportReturn));
