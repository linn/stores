import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import Part from '../../components/parts/Part';
import partActions from '../../actions/partActions';
import partSelectors from '../../selectors/partSelectors';
import accountingCompaniesActions from '../../actions/accountingCompaniesActions';
import departmentsActions from '../../actions/departmentsActions';
import rootProductsActions from '../../actions/rootProductsActions';
import partCategoriesActions from '../../actions/partCategoriesActions';
import sernosSequencesActions from '../../actions/sernosSequencesActions';
import suppliersActions from '../../actions/suppliersActions';
import accountingCompaniesSelectors from '../../selectors/accountingCompaniesSelectors';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import rootProductsSelectors from '../../selectors/rootProductsSelectors';
import partCategoriesSelectors from '../../selectors/partCategoriesSelectors';
import sernosSequencesSelectors from '../../selectors/sernosSequencesSelectors';
import suppliersSelectors from '../../selectors/suppliersSelectors';
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
    accountingCompanies: accountingCompaniesSelectors.getItems(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(partActions.fetch(itemId));
    dispatch(departmentsActions.fetch());
    dispatch(partCategoriesActions.fetch());
    dispatch(rootProductsActions.fetch());
    dispatch(sernosSequencesActions.fetch());
    dispatch(suppliersActions.fetch());
    dispatch(accountingCompaniesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: partActions.update,
    setEditStatus: partActions.setEditStatus,
    setSnackbarVisible: partActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Part));
