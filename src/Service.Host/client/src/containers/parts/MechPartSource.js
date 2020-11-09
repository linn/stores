import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import mechPartSourceActions from '../../actions/mechPartSourceActions';
import mechPartSourceSelectors from '../../selectors/mechPartSourceSelectors';
import { getPrivileges, getUserName, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';
import MechPartSource from '../../components/parts/mechPartSource/MechPartSource';

const mapStateToProps = (state, { match, location }) => ({
    item: mechPartSourceSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: mechPartSourceSelectors.getEditStatus(state),
    loading: mechPartSourceSelectors.getLoading(state),
    snackbarVisible: mechPartSourceSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.mechPartSource.item),
    options: queryString.parse(location?.search)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(mechPartSourceActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: mechPartSourceActions.update,
    setEditStatus: mechPartSourceActions.setEditStatus,
    setSnackbarVisible: mechPartSourceActions.setSnackbarVisible,
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(MechPartSource));
