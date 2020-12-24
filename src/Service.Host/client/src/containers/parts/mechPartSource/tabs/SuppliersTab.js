import { connect } from 'react-redux';
import SuppliersTab from '../../../../components/parts/mechPartSource/tabs/SuppliersTab';
import suppliersSelectors from '../../../../selectors/suppliersSelectors';
import suppliersActions from '../../../../actions/suppliersActions';

const mapStateToProps = state => ({
    loading: suppliersSelectors.getLoading(state),
    suppliersSearchResults: suppliersSelectors
        .getSearchItems(state)
        .map(c => ({ id: c.id, name: c.id.toString(), description: c.name })),
    suppliersSearchLoading: suppliersSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchSuppliers: suppliersActions.search,
    clearSuppliersSearch: suppliersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(SuppliersTab);
