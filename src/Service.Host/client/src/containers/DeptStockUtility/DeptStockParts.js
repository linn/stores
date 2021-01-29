import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import DeptStockParts from '../../components/DeptStockUtility/DeptStockParts';
import deptStockPartsSelectors from '../../selectors/deptStockPartsSelectors';
import deptStockPartsActions from '../../actions/deptStockPartsActions';

const mapStateToProps = state => ({
    items: deptStockPartsSelectors.getSearchItems(state).map(i => ({
        ...i,
        name: i.partNumber,
        href: i.links.find(l => l.rel === 'stock-locators')?.href
    })),
    itemsLoading: deptStockPartsSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    dispatch(deptStockPartsActions.search());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(DeptStockParts));
