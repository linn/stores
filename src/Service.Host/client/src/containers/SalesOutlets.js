import { connect } from 'react-redux';
import SalesOutlets from '../components/SalesOutlets';
import salesOutletsActions from '../actions/salesOutletsActions';
import salesOutletsSelectors from '../selectors/salesOutletsSelectors';

const mapStateToProps = state => ({
    searchResults: salesOutletsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: `${s.accountId}${s.outletNumber}`, displayText: s.name })),
    searchLoading: salesOutletsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: salesOutletsActions.search,
    clearSearch: salesOutletsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(SalesOutlets);
