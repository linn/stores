import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import DeptStockUtility from '../../components/DeptStockUtility/DeptStockUtility';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import departmentsActions from '../../actions/departmentsActions';
import storagePlacesSelectors from '../../selectors/storagePlacesSelectors';
import storagePlacesActions from '../../actions/storagePlacesActions';
import stockLocatorsActions from '../../actions/stockLocatorsActions';
import stockLocatorsSelectors from '../../selectors/stockLocatorsSelectors';
import stockLocatorActions from '../../actions/stockLocatorActions';
import stockLocatorSelectors from '../../selectors/stockLocatorSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { location }) => ({
    items: stockLocatorsSelectors.getSearchItems(state),
    itemsLoading: stockLocatorsSelectors.getSearchLoading(state),
    departments: departmentsSelectors
        .getSearchItems(state)
        .map(i => ({ ...i, name: i.departmentCode, id: i.departmentCode })),
    departmentsLoading: departmentsSelectors.getSearchLoading(state),
    storagePlaces: storagePlacesSelectors.getSearchItems(state).map(i => ({ ...i, id: i.name })),
    storagePlacesLoading: storagePlacesSelectors.getSearchLoading(state),
    options: queryString.parse(location?.search),
    stockLocatorLoading: stockLocatorSelectors.getLoading(state),
    snackbarVisible: stockLocatorSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.stockLocator.item)
});

const initialise = ({ options }) => dispatch => {
    dispatch(stockLocatorsActions.searchWithOptions(null, `&partId=${options.partId}`));
};

const mapDispatchToProps = {
    initialise,
    searchDepartments: departmentsActions.search,
    clearDepartmentsSearch: departmentsActions.clearSearch,
    clearStoragePlacesSearch: storagePlacesActions.clearSearch,
    searchStoragePlaces: storagePlacesActions.search,
    updateStockLocator: stockLocatorActions.update,
    createStockLocator: stockLocatorActions.add,
    deleteStockLocator: stockLocatorActions.delete,
    setSnackbarVisible: stockLocatorActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(DeptStockUtility));
