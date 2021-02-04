import { connect } from 'react-redux';
import StockViewerOptions from '../../components/stockLocatorUtility/StockViewerOptions';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
// import storageLocationsActions from '../../actions/storageLocationsActions';
// import storageLocationsSelectors from '../../selectors/storageLocationsSelectors';
// import stockPoolsActions from '../../actions/stockPoolsActions';
// import stockPoolsSelectors from '../../selectors/stockPoolsSelectors';
// import inspectedStatesActions from '../../actions/inspectedStatesActions';
// import inspectedStatesSeletors from '../../selectors/inspectedStatesSelectors';
// import stockLocatorBatchesActions from '../../actions/stockLocatorBatchesActions';
// import stockLocatorBatchesSelectors from '../../selectors/stockLocatorBatchesSelectors';

const mapStateToProps = state => ({
    parts: partsSelectors.getSearchItems(state).map(i => ({
        ...i,
        name: i.partNumber
    })),
    partsLoading: partsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(StockViewerOptions);
