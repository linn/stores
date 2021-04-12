import { connect } from 'react-redux';
import ExportRsns from '../components/exportReturns/ExportRsns';
import salesOutletsActions from '../actions/salesOutletsActions';
import salesOutletsSelectors from '../selectors/salesOutletsSelectors';
import salesAccountsActions from '../actions/salesAccountsActions';
import salesAccountsSelectors from '../selectors/salesAccountsSelectors';
import exportRsnsActions from '../actions/exportRsnsActions';
import exportRsnsSelectors from '../selectors/exportRsnsSelectors';
import exportReturnActions from '../actions/exportReturnActions';
import exportReturnSelectors from '../selectors/exportReturnSelectors';

const mapStateToProps = state => ({
    salesOutletsSearchResults: salesOutletsSelectors.getSearchItems(state),
    salesOutletsSearchLoading: salesOutletsSelectors.getSearchLoading(state),
    salesAccountsSearchResults: salesAccountsSelectors.getSearchItems(state),
    salesAccountsSearchLoading: salesAccountsSelectors.getSearchLoading(state),
    rsnsSearchResults: exportRsnsSelectors.getSearchItems(state),
    rsnsSearchLoading: exportRsnsSelectors.getSearchLoading(state),
    exportReturnLoading: exportReturnSelectors.getLoading(state)
});

const mapDispatchToProps = {
    searchSalesOutlets: salesOutletsActions.search,
    clearSalesOutletsSearch: salesOutletsActions.clearSearch,
    searchSalesAccounts: salesAccountsActions.search,
    clearSalesAccountsSearch: salesAccountsActions.clearSearch,
    searchRsns: exportRsnsActions.searchWithOptions,
    clearRsnsSearch: exportRsnsActions.clearSearch,
    createExportReturn: exportReturnActions.add
};

export default connect(mapStateToProps, mapDispatchToProps)(ExportRsns);
