import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import GeneralTab from '../../../components/parts/tabs/GeneralTab';
import accountingCompaniesActions from '../../../actions/accountingCompaniesActions';
import departmentsActions from '../../../actions/departmentsActions';
import accountingCompaniesSelectors from '../../../selectors/accountingCompaniesSelectors';
import departmentsSelectors from '../../../selectors/departmentsSelectors';
import rootProductsSelectors from '../../../selectors/rootProductsSelectors';
import productAnalysisCodesSelectors from '../../../selectors/productAnalysisCodesSelectors';
import sernosSequencesSelectors from '../../../selectors/sernosSequencesSelectors';
import suppliersSelectors from '../../../selectors/suppliersSelectors';
import rootProductsActions from '../../../actions/rootProductsActions';
import productAnalysisCodesActions from '../../../actions/productAnalysisCodesActions';
import nominalAccountsActions from '../../../actions/nominalAccountsActions';
import nominalAccountsSelectors from '../../../selectors/nominalAccountsSelectors';

const mapStateToProps = (state, ownProps) => ({
    accountingComapny: ownProps.accountingComapny,
    departmentsSearchResults: departmentsSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.departmentCode, description: c.description })),
    departmentsSearchLoading: departmentsSelectors.getSearchLoading(state),
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
    productAnalysisCodesSearchLoading: productAnalysisCodesSelectors.getSearchLoading(state),
    nominalAccountsSearchResults: nominalAccountsSelectors.getSearchItems(state, 50),
    nominalAccountsSearchLoading: nominalAccountsSelectors.getSearchLoading(state)
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
    clearSearchDepartments: departmentsActions.clearSearch,
    searchNominalAccounts: nominalAccountsActions.search,
    clearNominalAccountsSearch: nominalAccountsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(GeneralTab));
