import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import partActions from '../../actions/partActions';
import CreatePart from '../../components/parts/CreatePart';
import partSelectors from '../../selectors/partSelectors';
import accountingCompaniesActions from '../../actions/accountingCompaniesActions';
import accountingCompaniesSelectors from '../../selectors/accountingCompaniesSelectors';
import { getPrivileges, getUserName, getUserNumber } from '../../selectors/userSelectors';

const mapStpartsToProps = state => ({
    item: {},
    editStatus: partSelectors.getEditStatus(state),
    snackbarVisible: partSelectors.getSnackbarVisible(state),
    accountingCompanies: accountingCompaniesSelectors.getItems(state),
    privileges: getPrivileges(state),
    userName: getUserName(state),
    userNumber: getUserNumber(state),
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

export default connect(mapStpartsToProps, mapDispatchToProps)(initialiseOnMount(CreatePart));
