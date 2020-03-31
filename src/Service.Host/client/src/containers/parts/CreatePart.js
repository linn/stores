import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import partsActions from '../../actions/partsActions';
import Part from '../../components/parts/Part';
import partsSelectors from '../../selectors/partsSelectors';
import accountingCompaniesActions from '../../actions/accountingCompaniesActions';
import departmentsActions from '../../actions/departmentsActions';
import rootProductsActions from '../../actions/rootProductsActions';
import partCategoriesActions from '../../actions/partCategoriesActions';
import sernosSequencesActions from '../../actions/sernosSequencesActions';
import suppliersActions from '../../actions/suppliersActions';
import unitsOfMeasureActions from '../../actions/unitsOfMeasureActions';
import accountingCompaniesSelectors from '../../selectors/accountingCompaniesSelectors';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import rootProductsSelectors from '../../selectors/rootProductsSelectors';
import partCategoriesSelectors from '../../selectors/partCategoriesSelectors';
import sernosSequencesSelectors from '../../selectors/sernosSequencesSelectors';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import unitsOfMeasureSelectors from '../../selectors/unitsOfMeasureSelectors';
import nominalActions from '../../actions/nominalActions';
import nominalSelectors from '../../selectors/nominalSelectors';
import * as itemTypes from '../../itemTypes';

const mapStpartsToProps = state => ({
    item: {},
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.part.item),
    loading: partsSelectors.getLoading(state),
    snackbarVisible: partsSelectors.getSnackbarVisible(state),
    accountingCompanies: accountingCompaniesSelectors.getItems(state),
    departments: departmentsSelectors.getItems(state),
    partCategoris: partCategoriesSelectors.getItems(state),
    rootProducts: rootProductsSelectors.getItems(state),
    sernosSequences: sernosSequencesSelectors.getItems(state),
    suppliers: suppliersSelectors.getItems(state),
    unitsOfMeasure: unitsOfMeasureSelectors.getItems(state),
    nominal: nominalSelectors.getItem(state)
});

const initialise = () => dispatch => {
    dispatch(partsActions.clearErrorsForItem());
    dispatch(accountingCompaniesActions.fetch());
    dispatch(departmentsActions.fetch());
    dispatch(partCategoriesActions.fetch());
    dispatch(rootProductsActions.fetch());
    dispatch(sernosSequencesActions.fetch());
    dispatch(suppliersActions.fetch());
    dispatch(unitsOfMeasureActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: partsActions.add,
    setEditStatus: partsActions.setEditStatus,
    setSnackbarVisible: partsActions.setSnackbarVisible,
    fetchNominal: nominalActions.fetch
};

export default connect(mapStpartsToProps, mapDispatchToProps)(initialiseOnMount(Part));
