import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import StockMove from '../components/StockMove';
import partsActions from '../actions/partsActions';
import partsSelectors from '../selectors/partsSelectors';

const mapStateToProps = state => ({
    parts: partsSelectors.getSearchItems(state),
    partsLoading: partsSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {};

const mapDispatchToProps = {
    initialise,
    fetchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StockMove));
