import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import DeptStockUtility from '../components/DeptStockUtility';
import deptStockPartsSelectors from '../selectors/deptStockPartsSelectors';
import deptStockPartsActions from '../actions/deptStockPartsActions';
import departmentsSelectors from '../selectors/departmentsSelectors';
import departmentsActions from '../actions/departmentsActions';
import storagePlacesSelectors from '../selectors/storagePlacesSelectors';
import storagePlacesActions from '../actions/storagePlacesActions';

const mapStateToProps = state => ({
    items: deptStockPartsSelectors.getSearchItems(state).map(i => ({ ...i, name: i.partNumber })),
    itemsLoading: deptStockPartsSelectors.getSearchLoading(state),
    departments: departmentsSelectors
        .getSearchItems(state)
        .map(i => ({ ...i, name: i.departmentCode, id: i.departmentCode })),
    departmentsLoading: departmentsSelectors.getSearchLoading(state),
    storagePlaces: storagePlacesSelectors.getSearchItems(state).map(i => ({ ...i, id: i.name })),
    storagePlacesLoading: storagePlacesSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {};

const mapDispatchToProps = {
    initialise,
    fetchItems: deptStockPartsActions.search,
    searchDepartments: departmentsActions.search,
    clearSearch: deptStockPartsActions.clearSearch,
    clearDepartmentsSearch: departmentsActions.clearSearch,
    clearStoragePlacesSearch: storagePlacesActions.clearSearch,
    searchStoragePlaces: storagePlacesActions.search
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(DeptStockUtility));
