import { connect } from 'react-redux';
import DeptStockParts from '../../components/DeptStockUtility/DeptStockParts';
import deptStockPartsSelectors from '../../selectors/deptStockPartsSelectors';
import deptStockPartsActions from '../../actions/deptStockPartsActions';
import departmentsActions from '../../actions/departmentsActions';

const mapStateToProps = state => ({
    items: deptStockPartsSelectors.getSearchItems(state).map(i => ({
        ...i,
        name: i.partNumber,
        href: i.links.find(l => l.rel === 'stock-locators')?.href
    })),
    itemsLoading: deptStockPartsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: deptStockPartsActions.search,
    searchDepartments: departmentsActions.search,
    clearSearch: deptStockPartsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(DeptStockParts);
