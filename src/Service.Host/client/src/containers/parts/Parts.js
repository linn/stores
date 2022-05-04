import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import PartsSearch from '../../components/parts/PartsSearch';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import { getPrivileges } from '../../selectors/userSelectors';
import partTemplatesActions from '../../actions/partTemplatesActions';
import partTemplatesSelectors from '../../selectors/partTemplatesSelectors';
import partTemplateSelectors from '../../selectors/partTemplateSelectors';
import productAnalysisCodesSelectors from '../../selectors/productAnalysisCodesSelectors';
import productAnalysisCodesActions from '../../actions/productAnalysisCodesActions';

const mapStateToProps = (state, { match }) => ({
    linkToSources: match?.url?.endsWith('/sources'),
    items: partsSelectors.getSearchItems(state),
    loading: partsSelectors.getSearchLoading(state),
    privileges: getPrivileges(state),
    partTemplates: partTemplatesSelectors.getItems(state),
    partTemplate: partTemplateSelectors.getItem(state),
    productAnalysisCodeSearchResults: productAnalysisCodesSelectors
        .getSearchItems(state, 100)
        .map(c => ({ ...c, id: c.productCode, name: c.productCode })),
    productAnalysisCodeSearchLoading: productAnalysisCodesSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    dispatch(partTemplatesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    fetchItems: partsActions.searchWithOptions,
    clearSearch: partsActions.clearSearch,
    searchProductAnalysisCodes: productAnalysisCodesActions.search,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartsSearch));
