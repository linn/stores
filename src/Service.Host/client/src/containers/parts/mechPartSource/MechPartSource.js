import { connect } from 'react-redux';
import {
    getItemError,
    initialiseOnMount,
    getPreviousPaths
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import mechPartSourceActions from '../../../actions/mechPartSourceActions';
import mechPartSourceSelectors from '../../../selectors/mechPartSourceSelectors';
import * as itemTypes from '../../../itemTypes';
import MechPartSource from '../../../components/parts/mechPartSource/MechPartSource';
import { getPrivileges, getUserName, getUserNumber } from '../../../selectors/userSelectors';

const creating = match => match?.url?.endsWith('/create');

const mapStateToProps = (state, { match, location }) => ({
    item: creating(match) ? null : mechPartSourceSelectors.getItem(state),
    itemId: creating(match) ? null : match.params.id,
    editStatus: creating(match) ? 'create' : mechPartSourceSelectors.getEditStatus(state),
    loading: mechPartSourceSelectors.getLoading(state),
    snackbarVisible: mechPartSourceSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.mechPartSource.item),
    options: queryString.parse(location?.search),
    privileges: getPrivileges(state),
    userName: getUserName(state),
    userNumber: getUserNumber(state),
    previousPaths: getPreviousPaths(state)
});

const initialise = ({ itemId }) => dispatch => {
    if (itemId) {
        dispatch(mechPartSourceActions.fetch(itemId));
    }
};

const mapDispatchToProps = {
    initialise,
    updateItem: mechPartSourceActions.update,
    setEditStatus: mechPartSourceActions.setEditStatus,
    setSnackbarVisible: mechPartSourceActions.setSnackbarVisible,
    addItem: mechPartSourceActions.add,
    clearErrors: mechPartSourceActions.clearErrorsForItem
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(MechPartSource));
