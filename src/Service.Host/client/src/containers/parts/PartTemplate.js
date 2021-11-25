import { connect } from 'react-redux';
import {
    getItemError,
    initialiseOnMount,
    getPreviousPaths
} from '@linn-it/linn-form-components-library';
import PartTemplate from '../../components/parts/PartTemplate';
import partTemplateSelectors from '../../selectors/partTemplateSelectors';
import partTemplateActions from '../../actions/partTemplateActions';
import { getPrivileges, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';

const creating = match =>
    match?.url?.endsWith('/create') || match?.url?.endsWith('/parts-template');

const mapStateToProps = (state, { match }) => ({
    item: creating(match) ? null : partTemplateSelectors.getItem(state),
    itemId: creating(match) ? null : match.params.id,
    editStatus: creating(match) ? 'create' : partTemplateSelectors.getEditStatus(state),
    loading: partTemplateSelectors.getLoading(state),
    snackbarVisible: partTemplateSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.partTemplate.item),
    privileges: getPrivileges(state),
    userNumber: getUserNumber(state),
    previousPaths: getPreviousPaths(state)
});

const initialise = item => dispatch => {
    if (item.itemId) {
        dispatch(partTemplateActions.fetch(item.itemId));
    }
};

const mapDispatchToProps = {
    initialise,
    addItem: partTemplateActions.add,
    updateItem: partTemplateActions.update,
    setEditStatus: partTemplateActions.setEditStatus,
    setSnackbarVisible: partTemplateActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartTemplate));
