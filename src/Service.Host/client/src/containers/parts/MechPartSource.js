import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import mechPartSourceActions from '../../actions/mechPartSourceActions';
import mechPartSourceSelectors from '../../selectors/mechPartSourceSelectors';
import { getPrivileges, getUserName, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';
import MechPartSource from '../../components/parts/MechPartSource';

const mapStateToProps = (state, { match }) => ({
    item: mechPartSourceSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: mechPartSourceSelectors.getEditStatus(state),
    loading: mechPartSourceSelectors.getLoading(state),
    snackbarVisible: mechPartSourceSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.mechPartSource.item)
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
