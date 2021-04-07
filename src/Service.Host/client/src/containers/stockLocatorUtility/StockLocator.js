import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import queryString from 'query-string';
import {
    initialiseOnMount,
    getItemError,
    getPreviousPaths
} from '@linn-it/linn-form-components-library';
import StockLocator from '../../components/stockLocatorUtility/StockLocator';
import stockLocatorLocationsSelectors from '../../selectors/stockLocatorLocationsSelectors';
import stockLocatorLocationsActions from '../../actions/stockLocatorLocationsActions';
import stockQuantitiesActions from '../../actions/stockQuantitiesActions';
import stockQuantitiesSelectors from '../../selectors/stockQuantitiesSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { location }) => ({
    items: stockLocatorLocationsSelectors.getSearchItems(state),
    itemsLoading: stockLocatorLocationsSelectors.getSearchLoading(state),
    options: location?.search.replace('?', ''),
    loading: stockLocatorLocationsSelectors.getLoading(state),
    quantities: stockQuantitiesSelectors.getItem(state),
    quantitiesLoading: stockQuantitiesSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.stockLocator.item),
    previousPaths: getPreviousPaths(state)
});

const initialise = ({ options, history, previousPaths }) => dispatch => {
    // if options are empty
    if (!Object.values(options).some(x => x !== null && x !== '')) {
        // check for previous options
        const prevPath = previousPaths.filter(p => p.path?.endsWith('/locators')).pop();
        if (prevPath?.search) {
            history.push(prevPath.path + prevPath.search);
            return;
        }
        // else just go back to the search to get new options
        history.push('/inventory/stock-locator');
        return;
    }
    const parsedOptions = queryString.parse(options);
    if (parsedOptions.partNumber) {
        dispatch(stockQuantitiesActions.fetchByQueryString('partNumber', parsedOptions.partNumber));
    }
};

const mapDispatchToProps = {
    initialise,
    fetchItems: stockLocatorLocationsActions.searchWithOptions
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withRouter(initialiseOnMount(StockLocator)));
