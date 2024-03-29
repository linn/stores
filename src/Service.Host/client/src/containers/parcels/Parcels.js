import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import parcelsActions from '../../actions/parcelsActions';
import parcelsSelectors from '../../selectors/parcelsSelectors';
import suppliersActions from '../../actions/suppliersActions';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import suppliersApprovedCarrierActions from '../../actions/suppliersApprovedCarrierActions';
import suppliersApprovedCarrierSelectors from '../../selectors/suppliersApprovedCarrierSelectors';
import ParcelsSearch from '../../components/parcels/ParcelsSearch';
import { getPrivileges } from '../../selectors/userSelectors';

const mapStateToProps = state => ({
    items: parcelsSelectors.getItems(state),
    loading: parcelsSelectors.getLoading(state),
    suppliers: suppliersSelectors.getItems(state),
    privileges: getPrivileges(state),
    suppliersSearchResults: suppliersSelectors
        .getSearchItems(state)
        .map(c => ({ id: c.id, name: c.id.toString(), description: c.name })),
    suppliersSearchLoading: suppliersSelectors.getSearchLoading(state),
    carriersSearchResults: suppliersApprovedCarrierSelectors
        .getSearchItems(state)
        .map(c => ({ id: c.id, name: c.id.toString(), description: c.name })),
    carriersSearchLoading: suppliersApprovedCarrierSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    dispatch(suppliersActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    fetchItems: parcelsActions.fetchByQueryString,
    searchSuppliers: suppliersActions.search,
    clearSuppliersSearch: suppliersActions.clearSearch,
    searchCarriers: suppliersApprovedCarrierActions.search,
    clearCarriersSearch: suppliersApprovedCarrierActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ParcelsSearch));
