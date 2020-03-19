import { connect } from 'react-redux';
import PartsSearch from '../../components/parts/PartsSearch';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';

const mapStateToProps = state => ({
    items: partsSelectors.getSearchItems(state),
    loading: partsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: partsActions.search,
    clearSearch: partsActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(PartsSearch);
