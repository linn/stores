import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import Part from '../../components/parts/Part';
import partActions from '../../actions/partActions';
import partSelectors from '../../selectors/partSelectors';
import departmentsActions from '../../actions/departmentsActions';
import rootProductsActions from '../../actions/rootProductsActions';
import partCategoriesActions from '../../actions/partCategoriesActions';
import sernosSequencesActions from '../../actions/sernosSequencesActions';
import suppliersActions from '../../actions/suppliersActions';
import unitsOfMeasureActions from '../../actions/unitsOfMeasureActions';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import rootProductsSelectors from '../../selectors/rootProductsSelectors';
import partCategoriesSelectors from '../../selectors/partCategoriesSelectors';
import sernosSequencesSelectors from '../../selectors/sernosSequencesSelectors';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import unitsOfMeasureSelectors from '../../selectors/unitsOfMeasureSelectors';
import nominalActions from '../../actions/nominalActions';
import nominalSelectors from '../../selectors/nominalSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: partSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: partSelectors.getEditStatus(state),
    loading: partSelectors.getLoading(state),
    snackbarVisible: partSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.part.item),
    departments: departmentsSelectors.getItems(state),
    partCategoris: partCategoriesSelectors.getItems(state),
    rootProducts: rootProductsSelectors.getItems(state),
    sernosSequences: sernosSequencesSelectors.getItems(state),
    suppliers: suppliersSelectors.getItems(state),
    unitsOfMeasure: unitsOfMeasureSelectors.getItems(state),
    nominal: nominalSelectors.getItem(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(partActions.fetch(itemId));
    dispatch(departmentsActions.fetch());
    dispatch(partCategoriesActions.fetch());
    dispatch(rootProductsActions.fetch());
    dispatch(sernosSequencesActions.fetch());
    dispatch(suppliersActions.fetch());
    dispatch(unitsOfMeasureActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: partActions.update,
    setEditStatus: partActions.setEditStatus,
    setSnackbarVisible: partActions.setSnackbarVisible,
    fetchNominal: nominalActions.fetch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Part));
