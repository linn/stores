import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import ImportBooksSearch from '../../components/importBooks/ImportBooksSearch';
import importBooksActions from '../../actions/importBooksActions';
import importBooksSelectors from '../../selectors/importBooksSelectors';
import { getPrivileges } from '../../selectors/userSelectors';

const mapStateToProps = state => ({
    items: importBooksSelectors.getSearchItems(state),
    loading: importBooksSelectors.getSearchLoading(state),
    privileges: getPrivileges(state)
});

const initialise = () => dispatch => {
    dispatch(importBooksActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    fetchItems: importBooksActions.search,
    clearSearch: importBooksActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ImportBooksSearch));
