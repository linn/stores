import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import StockViewerOptions from '../../components/stockLocatorUtility/StockViewerOptions';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import storageLocationsActions from '../../actions/storageLocationsActions';
import storageLocationsSelectors from '../../selectors/storageLocationsSelectors';
import stockPoolsActions from '../../actions/stockPoolsActions';
import stockPoolsSelectors from '../../selectors/stockPoolsSelectors';
import inspectedStatesActions from '../../actions/inspectedStatesActions';
import inspectedStatesSelectors from '../../selectors/inspectedStatesSelectors';
import stockLocatorBatchesActions from '../../actions/stockLocatorBatchesActions';
import stockLocatorBatchesSelectors from '../../selectors/stockLocatorBatchesSelectors';

const mapStateToProps = state => ({
    parts: partsSelectors.getSearchItems(state, 100).map(i => ({
        ...i,
        name: i.partNumber
    })),
    partsLoading: partsSelectors.getSearchLoading(state),
    storageLocations: storageLocationsSelectors.getSearchItems(state, 100).map(i => ({
        ...i,
        id: i.id,
        name: i.locationCode
    })),
    storageLocationsLoading: storageLocationsSelectors.getSearchLoading(state),
    stockPools: stockPoolsSelectors.getSearchItems(state, 100).map(i => ({
        ...i,
        id: i.id,
        name: i.stockPoolCode
    })),
    stockPoolsLoading: stockPoolsSelectors.getSearchLoading(state),
    stockLocatorBatches: stockLocatorBatchesSelectors.getSearchItems(state, 100).map(i => ({
        ...i,
        id: i.id,
        name: i.batchRef
    })),
    stockLocatorBatchesLoading: stockLocatorBatchesSelectors.getSearchLoading(state),
    inspectedStates: inspectedStatesSelectors.getItems(state, 100).map(i => ({
        ...i,
        id: i.state,
        name: i.state
    })),
    inspectedStatesLoading: inspectedStatesSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(inspectedStatesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    searchStorageLocations: storageLocationsActions.search,
    clearStorageLocationsSearch: storageLocationsActions.clearSearch,
    searchStockPools: stockPoolsActions.search,
    clearStockPoolsSearch: stockPoolsActions.clearSearch,
    searchStockLocatorBatches: stockLocatorBatchesActions.search,
    clearStockLocatorBatchesSearch: stockLocatorBatchesActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StockViewerOptions));
