import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import StockTriggerLevelsUtility from '../../components/StockTriggerLevelsUtility/StockTriggerLevelsUtility';
import stockTriggerLevelActions from '../../actions/stockTriggerLevelActions';
import stockTriggerLevelsActions from '../../actions/stockTriggerLevelsActions';
import stockTriggerLevelsSelectors from '../../selectors/stockTriggerLevelsSelectors';
import storagePlacesSelectors from '../../selectors/storagePlacesSelectors';
import storagePlacesActions from '../../actions/storagePlacesActions';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { location }) => ({
    parts: partsSelectors.getSearchItems(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    partsLoading: partsSelectors.getSearchLoading(state),
    stockTriggerLevelsSearchResults: stockTriggerLevelsSelectors.getItems(state).map(i => ({
        ...i,
        name: i.id,
        href: i.links.find(l => l.rel === 'stock-trigger-levels')?.href
    })),
    stockTriggerLevelsSearchLoading: stockTriggerLevelsSelectors.getLoading(state),
    storagePlaces: storagePlacesSelectors.getSearchItems(state).map(i => ({ ...i, id: i.name })),
    storagePlacesLoading: storagePlacesSelectors.getSearchLoading(state),
    options: queryString.parse(location?.search),
    stockTriggerLevelsLoading: stockTriggerLevelsSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.stockTriggerLevels.item)
});

const initialise = props => dispatch => {
    dispatch(stockTriggerLevelsActions.clearSearch());
    if (!props.items || props.items.length === 0) {
        dispatch(stockTriggerLevelsActions.fetch());
    }
};

const mapDispatchToProps = {
    initialise,
    clearStoragePlacesSearch: storagePlacesActions.clearSearch,
    searchStoragePlaces: storagePlacesActions.search,
    clearPartsSearch: partsActions.clearSearch,
    searchParts: partsActions.search,
    updateStockTriggerLevel: stockTriggerLevelActions.update,
    createStockTriggerLevel: stockTriggerLevelActions.add,
    deleteStockTriggerLevel: stockTriggerLevelActions.delete
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(StockTriggerLevelsUtility));
