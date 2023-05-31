import { connect } from 'react-redux';
import { getItemError } from '@linn-it/linn-form-components-library';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import StockTriggerLevelsUtility from '../../components/StockTriggerLevelsUtility/StockTriggerLevelsUtility';
import stockTriggerLevelActions from '../../actions/stockTriggerLevelActions';
import stockTriggerLevelsActions from '../../actions/stockTriggerLevelsActions';
import stockTriggerLevelSelectors from '../../selectors/stockTriggerLevelSelectors';
import stockTriggerLevelsSelectors from '../../selectors/stockTriggerLevelsSelectors';
import storagePlacesSelectors from '../../selectors/storagePlacesSelectors';
import storagePlacesActions from '../../actions/storagePlacesActions';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    parts: partsSelectors.getSearchItems(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    partsLoading: partsSelectors.getSearchLoading(state),
    stockTriggerLevels: stockTriggerLevelsSelectors.getSearchItems(state),
    stockTriggerLevelsSearchLoading: stockTriggerLevelsSelectors.getLoading(state),
    storagePlaces: storagePlacesSelectors.getSearchItems(state).map(i => ({ ...i, id: i.name })),
    storagePlacesLoading: storagePlacesSelectors.getSearchLoading(state),
    itemError: getItemError(state, itemTypes.stockTriggerLevels.item),
    snackbarVisible: stockTriggerLevelSelectors.getSnackbarVisible(state)
});

const mapDispatchToProps = {
    clearStoragePlacesSearch: storagePlacesActions.clearSearch,
    searchStoragePlaces: storagePlacesActions.search,
    clearPartsSearch: partsActions.clearSearch,
    searchParts: partsActions.search,
    searchStockTriggerLevels: stockTriggerLevelsActions.searchWithOptions,
    clearStockTriggerLevels: stockTriggerLevelsActions.clearSearch,
    updateStockTriggerLevel: stockTriggerLevelActions.update,
    createStockTriggerLevel: stockTriggerLevelActions.add,
    deleteStockTriggerLevel: stockTriggerLevelActions.delete,
    setSnackbarVisible: stockTriggerLevelActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(StockTriggerLevelsUtility);
