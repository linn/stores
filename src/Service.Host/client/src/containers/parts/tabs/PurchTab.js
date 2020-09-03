import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import PurchTab from '../../../components/parts/tabs/PurchTab';
import partActions from '../../../actions/partActions';
import partSelectors from '../../../selectors/partSelectors';
import unitsOfMeasureActions from '../../../actions/unitsOfMeasureActions';
import unitsOfMeasureSelectors from '../../../selectors/unitsOfMeasureSelectors';
import suppliersActions from '../../../actions/suppliersActions';
import suppliersSelectors from '../../../selectors/suppliersSelectors';
import partCategoriesActions from '../../../actions/partCategoriesActions';
import partCategoriesSelectors from '../../../selectors/partCategoriesSelectors';

const mapStateToProps = state => ({
    editStatus: partSelectors.getEditStatus(state),
    loading: partSelectors.getLoading(state),
    unitsOfMeasure: unitsOfMeasureSelectors.getItems(state),
    suppliersSearchResults: suppliersSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.id, description: c.name })),
    suppliersSearchLoading: suppliersSelectors.getSearchLoading(state),
    partCategories: partCategoriesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(unitsOfMeasureActions.fetch());
    dispatch(partCategoriesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: partActions.update,
    setEditStatus: partActions.setEditStatus,
    setSnackbarVisible: partActions.setSnackbarVisible,
    searchSuppliers: suppliersActions.search,
    clearSuppliersSearch: suppliersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PurchTab));
