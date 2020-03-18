import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import Part from '../../components/parts/Part';
import partActions from '../../actions/partActions';
import partSelectors from '../../selectors/partSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: partSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: partSelectors.getEditStatus(state),
    loading: partSelectors.getLoading(state),
    snackbarVisible: partSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.part.item)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(partActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: partActions.update,
    setEditStatus: partActions.setEditStatus,
    setSnackbarVisible: partActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Part));
