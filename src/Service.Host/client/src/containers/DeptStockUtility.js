import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import DeptStockUtility from '../components/DeptStockUtility';
import deptStockPartsSelectors from '../selectors/deptStockPartsSelectors';
import deptStockPartsActions from '../actions/deptStockPartsActions';

const mapStateToProps = state => ({
    items: deptStockPartsSelectors.getSearchItems(state).map(i => ({ ...i, name: i.partNumber })),
    loading: deptStockPartsSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {};

const mapDispatchToProps = {
    initialise,
    fetchItems: deptStockPartsActions.search,
    clearSearch: deptStockPartsActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(DeptStockUtility));
