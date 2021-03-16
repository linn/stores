import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import StockMove from '../components/StockMove';
import partsActions from '../actions/partsActions';
import partsSelectors from '../selectors/partsSelectors';
import availableStockActions from '../actions/availableStockActions';
import availableStockSelectors from '../selectors/availableStockSelectors';

const mapStateToProps = state => ({
    parts: partsSelectors.getSearchItems(state),
    partsLoading: partsSelectors.getSearchLoading(state),
    availableStock: availableStockSelectors.getSearchItems(state),
    availableStockLoading: availableStockSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    fetchAvailableStock: availableStockActions.search
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StockMove));
