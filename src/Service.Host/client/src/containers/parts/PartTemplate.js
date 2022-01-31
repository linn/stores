import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import assemblyTechnologiesSelectors from '../../selectors/assemblyTechnologiesSelectors';
import assemblyTechnologiesActions from '../../actions/assemblyTechnologiesActions';
import PartTemplate from '../../components/parts/PartTemplate';
import partTemplateSelectors from '../../selectors/partTemplateSelectors';
import partTemplateActions from '../../actions/partTemplateActions';
import partTemplateStateActions from '../../actions/partTemplateStateActions';
import productAnalysisCodesSelectors from '../../selectors/productAnalysisCodesSelectors';
import productAnalysisCodesActions from '../../actions/productAnalysisCodesActions';
import { getPrivileges, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';

const creating = match =>
    match?.url?.endsWith('/create') || match?.url?.endsWith('/parts-template');

const mapStateToProps = (state, { match }) => ({
    item: creating(match) ? null : partTemplateSelectors.getItem(state),
    itemId: creating(match) ? null : match.params.id,
    editStatus: creating(match) ? 'create' : partTemplateSelectors.getEditStatus(state),
    loading: partTemplateSelectors.getLoading(state),
    snackbarVisible: partTemplateSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.partTemplate.item),
    privileges: getPrivileges(state),
    userNumber: getUserNumber(state),
    productAnalysisCodeSearchResults: productAnalysisCodesSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.productCode, description: c.description })),
    productAnalysisCodesSearchLoading: productAnalysisCodesSelectors.getSearchLoading(state),
    assemblyTechnologies: assemblyTechnologiesSelectors.getItems(state),
    applicationState: partTemplateSelectors.getApplicationState(state)
});

const initialise = item => dispatch => {
    if (item.itemId) {
        dispatch(partTemplateActions.fetch(item.itemId));
    }
    dispatch(partTemplateStateActions.fetchState());
    dispatch(assemblyTechnologiesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: partTemplateActions.add,
    updateItem: partTemplateActions.update,
    setEditStatus: partTemplateActions.setEditStatus,
    setSnackbarVisible: partTemplateActions.setSnackbarVisible,
    searchProductAnalysisCodes: productAnalysisCodesActions.search,
    clearProductAnalysisCodesSearch: productAnalysisCodesActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartTemplate));
