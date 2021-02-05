import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';

import ParcelsSearch from '../../components/parcels/ParcelsSearch';
import parcelsActions from '../../actions/parcelsActions';
import parcelsSelectors from '../../selectors/parcelsSelectors';

const mapStateToProps = state => ({
    items: parcelsSelectors.getSearchItems(state),
    loading: parcelsSelectors.getSearchLoading(state),
    applicationState: parcelsSelectors.getApplicationState(state),
    // editStatus: parcelSelectors.getEditStatus(state)
});

const initialise = () => dispatch => {
    dispatch(parcelsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    fetchItems: parcelsActions.search,
    clearSearch: parcelsActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ParcelsSearch));
