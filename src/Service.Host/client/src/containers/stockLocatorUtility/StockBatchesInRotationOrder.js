import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';

import StockBatchesInRotationOrder from '../../components/stockLocatorUtility/StockBatchesInRotationOrder';
import stockBatchesInRotationOrderSelectors from '../../selectors/stockBatchesInRotationOrderSelectors';
import stockBatchesInRotationOrderActions from '../../actions/stockBatchesInRotationOrderActions';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { location }) => ({
    items: stockBatchesInRotationOrderSelectors.getSearchItems(state),
    options: queryString.parse(location?.search),
    loading: stockBatchesInRotationOrderSelectors.getSearchLoading(state),
    itemError: getItemError(state, itemTypes.stockLocatorPrices.item)
});

const initialise = ({ options }) => dispatch => {
    dispatch(
        stockBatchesInRotationOrderActions.searchWithOptions(
            null,
            `&${queryString.stringify(options)}`
        )
    );
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(StockBatchesInRotationOrder));
