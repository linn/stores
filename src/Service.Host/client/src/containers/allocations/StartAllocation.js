import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import allocationActions from '../../actions/allocationActions';
import StartAllocation from '../../components/allocations/StartAllocation';
import allocationSelectors from '../../selectors/allocationSelectors';
import accountingCompaniesActions from '../../actions/accountingCompaniesActions';
import accountingCompaniesSelectors from '../../selectors/accountingCompaniesSelectors';
import * as itemTypes from '../../itemTypes';
import stockPoolsActions from '../../actions/stockPoolsActions';
import stockPoolsSelectors from '../../selectors/stockPoolsSelectors';
import despatchLocationsActions from '../../actions/despatchLocationsActions';
import despatchLocationsSelectors from '../../selectors/despatchLocationsSelectors';
import countriesActions from '../../actions/countriesActions';
import countriesSelectors from '../../selectors/countriesSelectors';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.allocation.item),
    loading:
        allocationSelectors.getLoading(state) ||
        stockPoolsSelectors.getLoading(state) ||
        despatchLocationsSelectors.getLoading(state) ||
        accountingCompaniesSelectors.getLoading(state) ||
        countriesSelectors.getLoading(state),
    accountingCompanies: accountingCompaniesSelectors.getItems(state),
    stockPools: stockPoolsSelectors.getItems(state),
    despatchLocations: despatchLocationsSelectors.getItems(state),
    countries: countriesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(allocationActions.clearErrorsForItem());
    dispatch(accountingCompaniesActions.fetch());
    dispatch(stockPoolsActions.fetch());
    dispatch(despatchLocationsActions.fetch());
    dispatch(countriesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: allocationActions.add,
    setEditStatus: allocationActions.setEditStatus
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StartAllocation));
