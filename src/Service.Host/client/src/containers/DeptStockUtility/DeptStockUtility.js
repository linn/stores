import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import DeptStockUtility from '../../components/DeptStockUtility/DeptStockUtility';
import deptStockPartsActions from '../../actions/deptStockPartsActions';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import departmentsActions from '../../actions/departmentsActions';
import storagePlacesSelectors from '../../selectors/storagePlacesSelectors';
import storagePlacesActions from '../../actions/storagePlacesActions';
import deptStockPartsSelectors from '../../selectors/deptStockPartsSelectors';

const mapStateToProps = (state, { location }) => ({
    items: deptStockPartsSelectors.getItems(state),
    departments: departmentsSelectors
        .getSearchItems(state)
        .map(i => ({ ...i, name: i.departmentCode, id: i.departmentCode })),
    departmentsLoading: departmentsSelectors.getSearchLoading(state),
    storagePlaces: storagePlacesSelectors.getSearchItems(state).map(i => ({ ...i, id: i.name })),
    storagePlacesLoading: storagePlacesSelectors.getSearchLoading(state),
    options: queryString.parse(location?.search)
});

const initialise = ({ options }) => dispatch => {
    dispatch(deptStockPartsActions.search(options.partNumber));
};

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
