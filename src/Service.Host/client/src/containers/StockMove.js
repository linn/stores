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
    reqMovesLoading: reqMovesSelectors.getLoading(state)
});

const mapDispatchToProps = {
    fetchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    fetchAvailableStock: availableStockActions.search,
    clearAvailableStock: availableStockActions.clearSearch,
    doMove: doStockMoveActions.requestProcessStart,
    clearMoveError: doStockMoveActions.clearErrorsForItem,
    fetchReqMoves: reqMovesActions.fetchById,
    clearMoveResult: doStockMoveActions.clearProcessData
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StockMove));
