import { connect } from 'react-redux';
import {
    initialiseOnMount,
    getItemError,
    getPreviousPaths
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import StockLocatorBatchView from '../../components/stockLocatorUtility/StockLocatorBatchView';
import stockLocatorLocationsSelectors from '../../selectors/stockLocatorLocationsSelectors';
import stockLocatorLocationsActions from '../../actions/stockLocatorLocationsActions';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { location }) => ({
    items: stockLocatorLocationsSelectors.getSearchItems(state),
    itemsLoading: stockLocatorLocationsSelectors.getSearchLoading(state),
    options: queryString.parse(location?.search),
    loading: stockLocatorLocationsSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.stockLocator.item),
    previousPaths: getPreviousPaths(state)
});

const initialise = ({ options }) => dispatch => {
    dispatch(
        stockLocatorLocationsActions.searchWithOptions(null, `&${queryString.stringify(options)}`)
    );
};

const mapDispatchToProps = {
    initialise,
    fetchItems: stockLocatorLocationsActions.searchWithOptions
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(StockLocatorBatchView));
