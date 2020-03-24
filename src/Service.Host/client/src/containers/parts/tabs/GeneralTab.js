import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import GeneralTab from '../../../components/parts/tabs/GeneralTab';
import partActions from '../../../actions/partActions';
import partSelectors from '../../../selectors/partSelectors';
import accountingCompaniesActions from '../../../actions/accountingCompaniesActions';
// import departmentsActions from '../../actions/departmentsActions';
// import rootProductsActions from '../../actions/rootProductsActions';
// import partCategoriesActions from '../../actions/partCategoriesActions';
// import sernosSequencesActions from '../../actions/sernosSequencesActions';
// import suppliersActions from '../../actions/suppliersActions';
// import unitsOfMeasureActions from '../../actions/unitsOfMeasureActions';
import accountingCompaniesSelectors from '../../../selectors/accountingCompaniesSelectors';
import departmentsSelectors from '../../../selectors/departmentsSelectors';
import rootProductsSelectors from '../../../selectors/rootProductsSelectors';
import partCategoriesSelectors from '../../../selectors/partCategoriesSelectors';
import sernosSequencesSelectors from '../../../selectors/sernosSequencesSelectors';
import suppliersSelectors from '../../../selectors/suppliersSelectors';
import unitsOfMeasureSelectors from '../../../selectors/unitsOfMeasureSelectors';
import rootProductsActions from '../../../actions/rootProductsActions';
// import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, ownProps) => ({
    accountingComapny: ownProps.accountingComapny,
    editStatus: partSelectors.getEditStatus(state),
    loading: partSelectors.getLoading(state),
    departments: departmentsSelectors.getItems(state),
    partCategoris: partCategoriesSelectors.getItems(state),
    rootProducts: rootProductsSelectors.getItems(state),
    sernosSequences: sernosSequencesSelectors.getItems(state),
    suppliers: suppliersSelectors.getItems(state),
    unitsOfMeasure: unitsOfMeasureSelectors.getItems(state),
    accountingCompanies: accountingCompaniesSelectors.getItems(state),
    rootProductsSearchResults: rootProductsSelectors.getSearchItems(state),
    rootProductsSearchLoading: rootProductsSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    // dispatch(partActions.fetch(itemId));
    // dispatch(departmentsActions.fetch());
    // dispatch(partCategoriesActions.fetch());
    // dispatch(rootProductsActions.fetch());
    // dispatch(sernosSequencesActions.fetch());
    // dispatch(suppliersActions.fetch());
    // dispatch(unitsOfMeasureActions.fetch());
    dispatch(accountingCompaniesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: partActions.update,
    setEditStatus: partActions.setEditStatus,
    setSnackbarVisible: partActions.setSnackbarVisible,
    searchRootProducts: rootProductsActions.search,
    clearRootProductsSearch: rootProductsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(GeneralTab));
