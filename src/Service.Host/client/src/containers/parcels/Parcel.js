import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import Parcel from '../../components/parcels/Parcel';
import parcelSelectors from '../../selectors/parcelSelectors';
import parcelActions from '../../actions/parcelActions';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import employeesSelectors from '../../selectors/employeesSelectors';
import suppliersActions from '../../actions/suppliersActions';
import suppliersApprovedCarrierActions from '../../actions/suppliersApprovedCarrierActions';
import suppliersApprovedCarrierSelectors from '../../selectors/suppliersApprovedCarrierSelectors';
import employeesActions from '../../actions/employeesActions';
import { getPrivileges, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';

const creating = match =>
    match?.url?.endsWith('/create') || match?.url?.endsWith('/goods-in-utility');

const mapStateToProps = (state, { match }) => ({
    item: creating(match) ? null : parcelSelectors.getItem(state),
    itemId: creating(match) ? null : match.params.id,
    editStatus: creating(match) ? 'create' : parcelSelectors.getEditStatus(state),
    loading: parcelSelectors.getLoading(state),
    snackbarVisible: parcelSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.parcel.item),
    suppliersSearchResults: suppliersSelectors.getSearchItems(state).map(c => ({
        id: c.id,
        name: c.id.toString(),
        description: c.name,
        country: c.countryCode
    })),
    suppliersSearchLoading: suppliersSelectors.getSearchLoading(state),
    carriersSearchResults: suppliersApprovedCarrierSelectors
        .getSearchItems(state)
        .map(c => ({ id: c.id, name: c.id.toString(), description: c.name })),
    carriersSearchLoading: suppliersApprovedCarrierSelectors.getSearchLoading(state),
    employees: employeesSelectors.getItems(state),
    privileges: getPrivileges(state),
    suppliers: suppliersSelectors.getItems(state),
    userNumber: getUserNumber(state)
});

const initialise = item => dispatch => {
    if (item.itemId) {
        dispatch(parcelActions.fetch(item.itemId));
    }
    dispatch(suppliersActions.fetch());
    dispatch(employeesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: parcelActions.add,
    updateItem: parcelActions.update,
    setEditStatus: parcelActions.setEditStatus,
    setSnackbarVisible: parcelActions.setSnackbarVisible,
    searchSuppliers: suppliersActions.search,
    clearSuppliersSearch: suppliersActions.clearSearch,
    searchCarriers: suppliersApprovedCarrierActions.search,
    clearCarriersSearch: suppliersApprovedCarrierActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Parcel));
