import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import partActions from '../../actions/partActions';
import CreatePart from '../../components/parts/CreatePart';
import partSelectors from '../../selectors/partSelectors';
import accountingCompaniesActions from '../../actions/accountingCompaniesActions';
import accountingCompaniesSelectors from '../../selectors/accountingCompaniesSelectors';
import { getPrivileges, getUserName, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';

const mapStateToProps = state => ({
    item: {},
    snackbarVisible: partSelectors.getSnackbarVisible(state),
    accountingCompanies: accountingCompaniesSelectors.getItems(state),
    privileges: getPrivileges(state),
    userName: getUserName(state),
    userNumber: getUserNumber(state),
    itemError: getItemError(state, itemTypes.part.item),
    loading: partSelectors.getLoading(state),
    searchItems: partsSelectors.getSearchItems(state),
    searchLoading: partsSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    dispatch(accountingCompaniesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: partActions.add,
    setEditStatus: partActions.setEditStatus,
    setSnackbarVisible: partActions.setSnackbarVisible,
    fetchItems: partsActions.search,
    clearSearch: partsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(CreatePart));
