import { connect } from 'react-redux';
import ParcelsSearch from '../../components/parcels/ParcelsSearch';
import parcelsActions from '../../actions/parcelsActions';
import parcelsSelectors from '../../selectors/parcelsSelectors';

const mapStateToProps = state => ({
    items: parcelsSelectors.getSearchItems(state),
    loading: parcelsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: parcelsActions.search,
    clearSearch: parcelsActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(ParcelsSearch);
