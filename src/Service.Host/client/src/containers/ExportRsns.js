import { connect } from 'react-redux';
import ExportRsns from '../components/ExportRsns';
import salesOutletsActions from '../actions/salesOutletsActions';
import salesOutletsSelectors from '../selectors/salesOutletsSelectors';
import salesAccountsActions from '../actions/salesAccountsActions';
import salesAccountsSelectors from '../selectors/salesAccountsSelectors';
import rsnsActions from '../actions/rsnsActions';
import rsnsSelectors from '../selectors/rsnsSelectors';

const mapStateToProps = state => ({
    salesOutletsSearchResults: salesOutletsSelectors.getSearchItems(state),
    salesOutletsSearchLoading: salesOutletsSelectors.getSearchLoading(state),
    salesAccountsSearchResults: salesAccountsSelectors.getSearchItems(state),
    salesAccountsSearchLoading: salesAccountsSelectors.getSearchLoading(state),
    rsnsSearchResults: rsnsSelectors.getSearchItems(state),
    rsnsSearchLoading: rsnsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchSalesOutlets: salesOutletsActions.search,
    clearSalesOutletsSearch: salesOutletsActions.clearSearch,
    searchSalesAccounts: salesAccountsActions.search,
    clearSalesAccountsSearch: salesAccountsActions.clearSearch,
    searchRsns: rsnsActions.searchWithOptions,
    clearRsnsSearch: rsnsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(ExportRsns);
