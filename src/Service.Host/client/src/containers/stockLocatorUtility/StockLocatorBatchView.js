import { connect } from 'react-redux';
import {
    initialiseOnMount,
    getItemError,
    getPreviousPaths
} from '@linn-it/linn-form-components-library';
import StockLocatorBatchView from '../../components/stockLocatorUtility/StockLocatorBatchView';
import stockLocatorLocationsSelectors from '../../selectors/stockLocatorLocationsSelectors';
import stockLocatorLocationsActions from '../../actions/stockLocatorLocationsActions';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { location }) => ({
    items: stockLocatorLocationsSelectors.getSearchItems(state),
    itemsLoading: stockLocatorLocationsSelectors.getSearchLoading(state),
    options: location?.search,
    loading: stockLocatorLocationsSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.stockLocator.item),
    drillBackPath: getPreviousPaths(state)
        ?.filter(x => x.path === '/inventory/stock-locator/locators')
        .pop(),
    previousPaths: getPreviousPaths(state)
});

const initialise = ({ options, history, previousPaths }) => () => {
    if (!Object.values(options).some(x => x !== null && x !== '')) {
        // check for previous options
        const prevPath = previousPaths.filter(p => p.path?.endsWith('/batches')).pop();
        if (prevPath?.search) {
            history.push(prevPath.path + prevPath.search);
            return;
        }
        // else just go back to the search to get new options
        history.push('/inventory/stock-locator');
    }
};

const mapDispatchToProps = {
    initialise,
    fetchItems: stockLocatorLocationsActions.searchWithOptions
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(StockLocatorBatchView));
