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

const mapStateToProps = state => ({
    parts: partsSelectors.getSearchItems(state),
    partsLoading: partsSelectors.getSearchLoading(state),
    availableStock: availableStockSelectors.getSearchItems(state),
    availableStockLoading: availableStockSelectors.getSearchLoading(state),
    moveError: getItemErrorDetailMessage(state, processTypes.doStockMove.item),
    requestErrors: getRequestErrors(state)?.filter(error => error.type !== 'FETCH_ERROR'),
    moveResult: doStockMoveSelectors.getData(state),
    userNumber: getUserNumber(state)
});

const mapDispatchToProps = {
    fetchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    fetchAvailableStock: availableStockActions.search,
    doMove: doStockMoveActions.requestProcessStart,
    clearMoveError: doStockMoveActions.clearErrorsForItem
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StockMove));
