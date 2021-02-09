import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';

import ParcelsSearch from '../../components/parcels/ParcelsSearch';
import parcelsActions from '../../actions/parcelsActions';
import parcelsSelectors from '../../selectors/parcelsSelectors';
import suppliersActions from '../../actions/suppliersActions';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import carriersActions from '../../actions/carriersActions';
import carriersSelectors from '../../selectors/carriersSelectors';

const mapStateToProps = state => ({
    items: parcelsSelectors.getItems(state),
    loading: parcelsSelectors.getLoading(state),
    applicationState: parcelsSelectors.getApplicationState(state),
    suppliers: suppliersSelectors.getItems(state),
    carriers: carriersSelectors.getItems(state)
    // editStatus: parcelSelectors.getEditStatus(state)
});

const initialise = () => dispatch => {
    dispatch(carriersActions.fetch());
    dispatch(suppliersActions.fetch());

    // dispatch(productionTriggerLevelsStateActions.fetchState());
};

const mapDispatchToProps = {
    initialise,
    fetchItems: parcelsActions.fetchByQueryString,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ParcelsSearch));

// dispatch(parcelsActions.fetch());
