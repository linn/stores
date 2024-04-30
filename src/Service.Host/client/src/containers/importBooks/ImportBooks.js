import { connect } from 'react-redux';
import ImportBooksSearch from '../../components/importBooks/ImportBooksSearch';
import importBooksActions from '../../actions/importBooksActions';
import importBooksSelectors from '../../selectors/importBooksSelectors';
import { getPrivileges } from '../../selectors/userSelectors';

const mapStateToProps = state => ({
    items: importBooksSelectors.getSearchItems(state),
    loading: importBooksSelectors.getSearchLoading(state),
    privileges: getPrivileges(state)
});

const mapDispatchToProps = {
    fetchItems: importBooksActions.searchWithOptions,
    clearSearch: importBooksActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(ImportBooksSearch);
