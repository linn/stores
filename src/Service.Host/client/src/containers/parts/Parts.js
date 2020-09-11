import { connect } from 'react-redux';
import PartsSearch from '../../components/parts/PartsSearch';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import { getPrivileges } from '../../selectors/userSelectors';

const mapStateToProps = state => ({
    items: partsSelectors.getSearchItems(state),
    loading: partsSelectors.getSearchLoading(state),
    privileges: getPrivileges(state)
});

const mapDispatchToProps = {
    fetchItems: partsActions.search,
    clearSearch: partsActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(PartsSearch);
