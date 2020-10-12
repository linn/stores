import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import GeneralTab from '../../../components/parts/tabs/GeneralTab';
import accountingCompaniesActions from '../../../actions/accountingCompaniesActions';
import departmentsActions from '../../../actions/departmentsActions';
import accountingCompaniesSelectors from '../../../selectors/accountingCompaniesSelectors';
import departmentsSelectors from '../../../selectors/departmentsSelectors';
import rootProductsSelectors from '../../../selectors/rootProductsSelectors';
import partCategoriesSelectors from '../../../selectors/partCategoriesSelectors';
import productAnalysisCodesSelectors from '../../../selectors/productAnalysisCodesSelectors';
import sernosSequencesSelectors from '../../../selectors/sernosSequencesSelectors';
import suppliersSelectors from '../../../selectors/suppliersSelectors';
import rootProductsActions from '../../../actions/rootProductsActions';
import productAnalysisCodesActions from '../../../actions/productAnalysisCodesActions';

const mapStateToProps = (state, ownProps) => ({
    accountingComapny: ownProps.accountingComapny,
    departmentsSearchResults: departmentsSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.departmentCode, description: c.description })),
    departmentsSearchLoading: departmentsSelectors.getSearchLoading(state),
    partCategories: partCategoriesSelectors.getItems(state),
    rootProducts: rootProductsSelectors.getItems(state),
    sernosSequences: sernosSequencesSelectors.getItems(state),
    suppliers: suppliersSelectors.getItems(state),
    accountingCompanies: accountingCompaniesSelectors.getItems(state),
    rootProductsSearchResults: rootProductsSelectors.getSearchItems(state),
    rootProductsSearchLoading: rootProductsSelectors.getSearchLoading(state),
    productAnalysisCode: ownProps.productAnalysisCode,
    productAnalysisCodeDescription: ownProps.productAnalysisCodeDescription,
    productAnalysisCodeSearchResults: productAnalysisCodesSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.productCode, description: c.description })),
    productAnalysisCodesSearchLoading: productAnalysisCodesSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    dispatch(productAnalysisCodesActions.fetch());
    dispatch(accountingCompaniesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchRootProducts: rootProductsActions.search,
    clearRootProductsSearch: rootProductsActions.clearSearch,
    searchProductAnalysisCodes: productAnalysisCodesActions.search,
    clearSearchProductAnalysisCodes: productAnalysisCodesActions.clearSearch,
    searchDepartments: departmentsActions.search,
    clearSearchDepartments: departmentsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(GeneralTab));
