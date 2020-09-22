import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import partActions from '../../actions/partActions';
import CreatePart from '../../components/parts/CreatePart';
import partSelectors from '../../selectors/partSelectors';
import accountingCompaniesActions from '../../actions/accountingCompaniesActions';
import accountingCompaniesSelectors from '../../selectors/accountingCompaniesSelectors';
import { getPrivileges, getUserName, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    snackbarVisible: partSelectors.getSnackbarVisible(state),
    accountingCompanies: accountingCompaniesSelectors.getItems(state),
    privileges: getPrivileges(state),
    userName: getUserName(state),
    userNumber: getUserNumber(state),
    itemError: getItemError(state, itemTypes.part.item),
    loading: partSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(accountingCompaniesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: partActions.add,
    setEditStatus: partActions.setEditStatus,
    setSnackbarVisible: partActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(CreatePart));
