import { connect } from 'react-redux';
import demLocationsActions from '../../actions/demLocationsActions';
import validatePurchaseOrderActions from '../../actions/validatePurchaseOrderActions';
import GoodsInUtility from '../../components/goodsIn/GoodsInUtility';
import demLocationsSelectors from '../../selectors/demLocationsSelectors';
import validatePurchaseOrderResultSelectors from '../../selectors/validatePurchaseOrderResultSelectors';
import storagePlacesSelectors from '../../selectors/storagePlacesSelectors';
import storagePlacesActions from '../../actions/storagePlacesActions';
import salesArticlesSelectors from '../../selectors/salesArticlesSelectors';
import salesArticlesActions from '../../actions/salesArticlesActions';

const mapStateToProps = state => ({
    validatePurchaseOrderResult: validatePurchaseOrderResultSelectors.getItem(state),
    demLocationsSearchResults: demLocationsSelectors
        .getSearchItems(state)
        .map(c => ({ id: c.id, name: c.locationCode, description: c.description })),
    demLocationsSearchLoading: demLocationsSelectors.getSearchLoading(state),
    storagePlacesSearchResults: storagePlacesSelectors.getSearchItems(state),
    storagePlacesSearchLoading: storagePlacesSelectors.getSearchLoading(state),
    salesArticlesSearchResults: salesArticlesSelectors
        .getSearchItems(state)
        .map(c => ({ ...c, name: c.articleNumber })),
    salesArticlesSearchLoading: salesArticlesSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    validatePurchaseOrder: validatePurchaseOrderActions.fetchById,
    searchDemLocations: demLocationsActions.search,
    searchStoragePlaces: storagePlacesActions.search,
    searchSalesArticles: salesArticlesActions.search
};

export default connect(mapStateToProps, mapDispatchToProps)(GoodsInUtility);
