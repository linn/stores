import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import partsActions from '../../actions/partsActions';
import Part from '../../components/parts/Part';
import partsSelectors from '../../selectors/partsSelectors';
import departmentsActions from '../../actions/departmentsActions';
import rootProductsActions from '../../actions/rootProductsActions';
import partCategoriesActions from '../../actions/partCategoriesActions';
import sernosSequencesActions from '../../actions/sernosSequencesActions';
import suppliersActions from '../../actions/suppliersActions';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import rootProductsSelectors from '../../selectors/rootProductsSelectors';
import partCategoriesSelectors from '../../selectors/partCategoriesSelectors';
import sernosSequencesSelectors from '../../selectors/sernosSequencesSelectors';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import * as itemTypes from '../../itemTypes';

const mapStpartsToProps = state => ({
    item: {},
    editStatus: 'creparts',
    itemError: getItemError(state, itemTypes.part.item),
    loading: partsSelectors.getLoading(state),
    snackbarVisible: partsSelectors.getSnackbarVisible(state),
    departments: departmentsSelectors.getItems(state),
    partCategoris: partCategoriesSelectors.getItems(state),
    rootProducts: rootProductsSelectors.getItems(state),
    sernosSequences: sernosSequencesSelectors.getItems(state),
    suppliers: suppliersSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(partsActions.setEditStatus('creparts'));
    dispatch(partsActions.clearErrorsForItem());
    dispatch(departmentsActions.fetch());
    dispatch(partCategoriesActions.fetch());
    dispatch(rootProductsActions.fetch());
    dispatch(sernosSequencesActions.fetch());
    dispatch(suppliersActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: partsActions.add,
    setEditStatus: partsActions.setEditStatus,
    setSnackbarVisible: partsActions.setSnackbarVisible
};

export default connect(mapStpartsToProps, mapDispatchToProps)(initialiseOnMount(Part));
