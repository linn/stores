import { connect } from 'react-redux';
import suppliersSelectors from '../../../../selectors/suppliersSelectors';
import suppliersActions from '../../../../actions/suppliersActions';
import manufacturersSelectors from '../../../../selectors/manufacturersSelectors';
import manufacturersActions from '../../../../actions/manufacturersActions';
import PurchasingQuotesTab from '../../../../components/parts/mechPartSource/tabs/PurchasingQuotesTab';

const mapStateToProps = state => ({
    suppliersLoading: suppliersSelectors.getLoading(state),
    suppliersSearchResults: suppliersSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.id.toString(), id: c.id, description: c.name })),
    suppliersSearchLoading: suppliersSelectors.getSearchLoading(state),
    manufacturersLoading: manufacturersSelectors.getLoading(state),
    manufacturersSearchResults: manufacturersSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.code, id: c.code, description: c.name })),
    manufacturersSearchLoading: manufacturersSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchSuppliers: suppliersActions.search,
    clearSuppliersSearch: suppliersActions.clearSearch,
    searchManufacturers: manufacturersActions.search,
    clearManufacturersSearch: manufacturersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(PurchasingQuotesTab);
