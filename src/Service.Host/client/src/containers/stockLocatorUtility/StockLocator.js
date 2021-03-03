import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import StockLocator from '../../components/stockLocatorUtility/StockLocator';
import stockLocatorLocationsSelectors from '../../selectors/stockLocatorLocationsSelectors';
import stockLocatorLocationsActions from '../../actions/stockLocatorLocationsActions';
import stockQuantitiesActions from '../../actions/stockQuantitiesActions';
import stockQuantitiesSelectors from '../../selectors/stockQuantitiesSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { location }) => ({
    items: stockLocatorLocationsSelectors.getSearchItems(state),
    itemsLoading: stockLocatorLocationsSelectors.getSearchLoading(state),
    options: queryString.parse(location?.search),
    loading: stockLocatorLocationsSelectors.getLoading(state),
    quantities: stockQuantitiesSelectors.getItem(state),
    quantitiesLoading: stockQuantitiesSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.stockLocator.item)
});

const initialise = ({ options }) => dispatch => {
    dispatch(
        stockLocatorLocationsActions.searchWithOptions(null, `&${queryString.stringify(options)}`)
    );
    if (options.partNumber) {
        dispatch(stockQuantitiesActions.fetchByQueryString('partNumber', options.partNumber));
    }
};

const mapDispatchToProps = {
    initialise,
    fetchItems: stockLocatorLocationsActions.searchWithOptions
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StockLocator));
