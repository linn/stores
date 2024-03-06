import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import StoresMoveLogReportOptions from '../../components/reports/StoresMoveLogReportOptions';
import stockPoolsActions from '../../actions/stockPoolsActions';
import stockPoolsSelectors from '../../selectors/stockPoolsSelectors';

const mapStateToProps = state => ({
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    stockPools: stockPoolsSelectors.getSearchItems(state, 100).map(i => ({
        ...i,
        id: i.id,
        name: i.stockPoolCode
    })),
    stockPoolsLoading: stockPoolsSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    dispatch(partsActions.clearSearch());
};

const mapDispatchToProps = {
    initialise,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    searchStockPools: stockPoolsActions.search,
    clearStockPoolsSearch: stockPoolsActions.clearSearch
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withRouter(initialiseOnMount(StoresMoveLogReportOptions)));
