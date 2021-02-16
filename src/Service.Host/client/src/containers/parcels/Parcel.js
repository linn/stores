import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import Parcel from '../../components/parcels/Parcel';
import parcelSelectors from '../../selectors/parcelSelectors';
import parcelActions from '../../actions/parcelActions';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import carriersSelectors from '../../selectors/carriersSelectors';
import employeesSelectors from '../../selectors/employeesSelectors';
import suppliersActions from '../../actions/suppliersActions';
import carriersActions from '../../actions/carriersActions';
import employeesActions from '../../actions/employeesActions';
import { getPrivileges } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';

const creating = match => match?.url?.endsWith('/create');

const mapStateToProps = (state, { match }) => ({
    item: creating(match) ? null : parcelSelectors.getItem(state),
    itemId: creating(match) ? null : match.params.id,
    editStatus: creating(match) ? 'create' : parcelSelectors.getEditStatus(state),
    loading: parcelSelectors.getLoading(state),
    snackbarVisible: parcelSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.part.item),
    suppliers: suppliersSelectors.getItems(state),
    carriers: carriersSelectors.getItems(state),
    employees: employeesSelectors.getItems(state),
    privileges: getPrivileges(state)
});

const initialise = itemId => dispatch => {
    if (itemId) {
        dispatch(parcelActions.fetch(itemId));
    }
    dispatch(suppliersActions.fetch());
    dispatch(carriersActions.fetch());
    dispatch(employeesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: parcelActions.add,
    updateItem: parcelActions.update,
    setEditStatus: parcelActions.setEditStatus,
    setSnackbarVisible: parcelActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Parcel));
