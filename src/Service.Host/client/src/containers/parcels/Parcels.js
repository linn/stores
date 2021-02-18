import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import parcelsActions from '../../actions/parcelsActions';
import parcelsSelectors from '../../selectors/parcelsSelectors';
import suppliersActions from '../../actions/suppliersActions';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import ParcelsSearch from '../../components/parcels/ParcelsSearch';
import { getPrivileges } from '../../selectors/userSelectors';

const mapStateToProps = state => ({
    items: parcelsSelectors.getItems(state),
    loading: parcelsSelectors.getLoading(state),
    applicationState: parcelsSelectors.getApplicationState(state),
    suppliers: suppliersSelectors.getItems(state).filter(x => x.countryCode !== 'GB'),
    carriers: suppliersSelectors.getItems(state).filter(x => x.approvedCarrier === 'Y'),
    privileges: getPrivileges(state)
    // editStatus: parcelSelectors.getEditStatus(state)
});

const initialise = () => dispatch => {
    dispatch(suppliersActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    fetchItems: parcelsActions.fetchByQueryString,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ParcelsSearch));
