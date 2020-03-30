import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import allocationActions from '../../actions/allocationActions';
import StartAllocation from '../../components/allocations/StartAllocation';
import allocationSelectors from '../../selectors/allocationSelectors';
import accountingCompaniesActions from '../../actions/accountingCompaniesActions';
import accountingCompaniesSelectors from '../../selectors/accountingCompaniesSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.allocation.item),
    loading: allocationSelectors.getLoading(state),
    snackbarVisible: allocationSelectors.getSnackbarVisible(state),
    accountingCompanies: accountingCompaniesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(allocationActions.clearErrorsForItem());
    dispatch(accountingCompaniesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: allocationActions.add,
    setEditStatus: allocationActions.setEditStatus,
    setSnackbarVisible: allocationActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StartAllocation));
