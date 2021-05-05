import { connect } from 'react-redux';
import {
    initialiseOnMount,
    getItemErrorDetailMessage,
    getRequestErrors
} from '@linn-it/linn-form-components-library';
import StockMove from '../components/StockMove';
import partsActions from '../actions/partsActions';
import partsSelectors from '../selectors/partsSelectors';
import availableStockActions from '../actions/availableStockActions';
import availableStockSelectors from '../selectors/availableStockSelectors';
import * as processTypes from '../processTypes';
import doStockMoveSelectors from '../selectors/doStockMoveSelectors';
import doStockMoveActions from '../actions/doStockMoveActions';
import { getUserNumber } from '../selectors/userSelectors';
import reqMovesActions from '../actions/reqMovesActions';
import reqMovesSelectors from '../selectors/reqMovesSelectors';
import partStorageTypesActions from '../actions/partStorageTypesActions';
import partStorageTypesSelectors from '../selectors/partStorageTypesSelectors';
import storageLocationsActions from '../actions/storageLocationsActions';
import storageLocationsSelectors from '../selectors/storageLocationsSelectors';

const mapStateToProps = state => ({
    parts: partsSelectors.getSearchItems(state),
    partsLoading: partsSelectors.getSearchLoading(state),
    availableStock: availableStockSelectors.getSearchItems(state),
    availableStockLoading: availableStockSelectors.getSearchLoading(state),
    moveError: getItemErrorDetailMessage(state, processTypes.doStockMove.item),
    requestErrors: getRequestErrors(state)?.filter(error => error.type !== 'FETCH_ERROR'),
    moveResult: doStockMoveSelectors.getData(state),
    moveWorking: doStockMoveSelectors.getWorking(state),
    userNumber: getUserNumber(state),
    reqMoves: reqMovesSelectors.getItems(state),
    reqMovesLoading: reqMovesSelectors.getLoading(state),
    partStorageTypes: partStorageTypesSelectors.getSearchItems(state),
    partStorageTypesLoading: partStorageTypesSelectors.getSearchLoading(state),
    storageLocations: storageLocationsSelectors.getSearchItems(state),
    storageLocationsLoading: storageLocationsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchParts: partsActions.search,
    fetchPartsLookup: partsActions.searchWithOptions,
    clearPartsSearch: partsActions.clearSearch,
    fetchAvailableStock: availableStockActions.search,
    clearAvailableStock: availableStockActions.clearSearch,
    doMove: doStockMoveActions.requestProcessStart,
    clearMoveError: doStockMoveActions.clearErrorsForItem,
    fetchReqMoves: reqMovesActions.fetchById,
    clearMoveResult: doStockMoveActions.clearProcessData,
    fetchPartStorageTypes: partStorageTypesActions.search,
    clearPartStorageTypes: partStorageTypesActions.clearSearch,
    fetchStorageLocations: storageLocationsActions.search,
    clearStorageLocationsSearch: storageLocationsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StockMove));
