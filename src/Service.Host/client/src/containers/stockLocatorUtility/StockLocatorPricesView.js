import { connect } from 'react-redux';
import {
    initialiseOnMount,
    getItemError,
    getPreviousPaths
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import StockLocatorPricesView from '../../components/stockLocatorUtility/StockLocatorPricesView';
import stockLocatorPricesSelectors from '../../selectors/stockLocatorPricesSelectors';
import stockLocatorPricesActions from '../../actions/stockLocatorPricesActions';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { location }) => ({
    items: stockLocatorPricesSelectors.getSearchItems(state),
    itemsLoading: stockLocatorPricesSelectors.getSearchLoading(state),
    options: queryString.parse(location?.search),
    loading: stockLocatorPricesSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.stockLocatorPrices.item),
    previousPaths: getPreviousPaths(state)
});

const initialise = ({ options }) => dispatch => {
    dispatch(
        stockLocatorPricesActions.searchWithOptions(null, `&${queryString.stringify(options)}`)
    );
};

const mapDispatchToProps = {
    initialise,
    fetchItems: stockLocatorPricesActions.searchWithOptions
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(StockLocatorPricesView));
