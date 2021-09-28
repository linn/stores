import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ImportBook from '../../components/importBooks/ImportBook';
import importBookActions from '../../actions/importBookActions';
import importBooksActions from '../../actions/importBooksActions';
import importBookSelectors from '../../selectors/importBookSelectors';
import { getPrivileges, getUserName, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';
import suppliersActions from '../../actions/suppliersActions';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import countriesActions from '../../actions/countriesActions';
import countriesSelectors from '../../selectors/countriesSelectors';
import employeesActions from '../../actions/employeesActions';
import employeesSelectors from '../../selectors/employeesSelectors';

const creating = match => match?.url?.endsWith('/create');

const mapStateToProps = (state, { match }) => ({
    item: creating(match) ? null : importBookSelectors.getItem(state),
    itemId: creating(match) ? null : match.params.id,
    editStatus: creating(match) ? 'create' : importBookSelectors.getEditStatus(state),
    loading: importBookSelectors.getLoading(state),
    snackbarVisible: importBookSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.part.item),
    privileges: getPrivileges(state),
    userName: getUserName(state),
    userNumber: getUserNumber(state),
    allSuppliers: suppliersSelectors.getItems(state),
    countries: countriesSelectors.getItems(state),
    employees: employeesSelectors.getItems(state)
});

const mapDispatchToProps = dispatch => {
    return {
        initialise: ({ itemId }) => {
            if (itemId) {
                dispatch(importBookActions.fetch(itemId));
            }
            dispatch(suppliersActions.fetch());
            dispatch(countriesActions.fetch());
            dispatch(employeesActions.fetch());
        },
        fetchParts: searchTerm => dispatch(importBooksActions.search(searchTerm)),
        addItem: item => dispatch(importBookActions.add(item)),
        updateItem: (itemId, item) => dispatch(importBookActions.update(itemId, item)),
        setEditStatus: status => dispatch(importBookActions.setEditStatus(status)),
        setSnackbarVisible: () => dispatch(importBookActions.setSnackbarVisible())
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ImportBook));
