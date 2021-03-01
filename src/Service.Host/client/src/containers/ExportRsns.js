import { connect } from 'react-redux';
import ExportRsns from '../components/ExportRsns';
import salesOutletsActions from '../actions/salesOutletsActions';
import salesOutletsSelectors from '../selectors/salesOutletsSelectors';
import salesAccountsActions from '../actions/salesAccountsActions';
import salesAccountsSelectors from '../selectors/salesAccountsSelectors';
import exportRsnsActions from '../actions/exportRsnsActions';
import exportRsnsSelectors from '../selectors/exportRsnsSelectors';
import makeExportReturnActions from '../actions/makeExportReturnActions';
import makeExportReturnSelectors from '../selectors/makeExportReturnSelectors';

const mapStateToProps = state => ({
    salesOutletsSearchResults: salesOutletsSelectors.getSearchItems(state),
    salesOutletsSearchLoading: salesOutletsSelectors.getSearchLoading(state),
    salesAccountsSearchResults: salesAccountsSelectors.getSearchItems(state),
    salesAccountsSearchLoading: salesAccountsSelectors.getSearchLoading(state),
    rsnsSearchResults: exportRsnsSelectors.getSearchItems(state),
    rsnsSearchLoading: exportRsnsSelectors.getSearchLoading(state),
    makeExportReturnLoading: makeExportReturnSelectors.getWorking(state)
});

const mapDispatchToProps = {
    searchSalesOutlets: salesOutletsActions.search,
    clearSalesOutletsSearch: salesOutletsActions.clearSearch,
    searchSalesAccounts: salesAccountsActions.search,
    clearSalesAccountsSearch: salesAccountsActions.clearSearch,
    searchRsns: exportRsnsActions.searchWithOptions,
    clearRsnsSearch: exportRsnsActions.clearSearch,
    makeExportReturnProcess: makeExportReturnActions.requestProcessStart
};

export default connect(mapStateToProps, mapDispatchToProps)(ExportRsns);
