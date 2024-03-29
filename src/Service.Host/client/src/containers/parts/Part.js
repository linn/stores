import { connect } from 'react-redux';
import {
    getItemError,
    initialiseOnMount,
    getPreviousPaths
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import Part from '../../components/parts/Part';
import partActions from '../../actions/partActions';
import partSelectors from '../../selectors/partSelectors';
import departmentsActions from '../../actions/departmentsActions';
import rootProductsActions from '../../actions/rootProductsActions';
import sernosSequencesActions from '../../actions/sernosSequencesActions';
import suppliersActions from '../../actions/suppliersActions';
import unitsOfMeasureActions from '../../actions/unitsOfMeasureActions';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import rootProductsSelectors from '../../selectors/rootProductsSelectors';
import sernosSequencesSelectors from '../../selectors/sernosSequencesSelectors';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import unitsOfMeasureSelectors from '../../selectors/unitsOfMeasureSelectors';
import partTemplatesActions from '../../actions/partTemplatesActions';
import partTemplatesSelectors from '../../selectors/partTemplatesSelectors';
import { getPrivileges, getUserName, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';
import partLiveTestSelectors from '../../selectors/partLiveTestSelectors';
import partLiveTestActions from '../../actions/partLiveTestActions';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import bomStandardPricesSelectors from '../../selectors/bomStandardPricesSelectors';
import bomStandardPricesActions from '../../actions/bomStandardPricesActions';

const creating = match => match?.url?.endsWith('/create');

const mapStateToProps = (state, { match, location }) => ({
    copy: !!queryString.parse(location.search.split('?')[1])?.copy,
    item: creating(match) ? null : partSelectors.getItem(state),
    itemId: creating(match) ? null : match.params.id,
    editStatus: creating(match) ? 'create' : partSelectors.getEditStatus(state),
    loading: partSelectors.getLoading(state),
    snackbarVisible: partSelectors.getSnackbarVisible(state),
    options: queryString.parse(location?.search),
    itemError: getItemError(state, itemTypes.part.item),
    departments: departmentsSelectors.getItems(state),
    rootProducts: rootProductsSelectors.getItems(state),
    sernosSequences: sernosSequencesSelectors.getItems(state),
    suppliers: suppliersSelectors.getItems(state),
    unitsOfMeasure: unitsOfMeasureSelectors.getItems(state),
    privileges: getPrivileges(state),
    userName: getUserName(state),
    userNumber: getUserNumber(state),
    templateName: queryString.parse(location?.search)?.template,
    partTemplates: partTemplatesSelectors.getItems(state),
    liveTest: creating(match) ? null : partLiveTestSelectors.getItem(state),
    partsSearchResults: partsSelectors.getSearchItems(state),
    previousPaths: getPreviousPaths(state),
    bomStandardPrices: bomStandardPricesSelectors.getItem(state),
    bomStandardPricesLoading: bomStandardPricesSelectors.getLoading(state)
});

const mapDispatchToProps = dispatch => {
    return {
        initialise: ({ itemId }) => {
            if (itemId) {
                dispatch(partActions.fetch(itemId));
                dispatch(partLiveTestActions.fetch(itemId));
            }
            dispatch(departmentsActions.fetch());
            dispatch(rootProductsActions.fetch());
            dispatch(sernosSequencesActions.fetch());
            dispatch(suppliersActions.fetch());
            dispatch(unitsOfMeasureActions.fetch());
            dispatch(partTemplatesActions.fetch());
        },
        fetchParts: searchTerm => dispatch(partsActions.search(searchTerm)),
        addItem: item => dispatch(partActions.add(item)),
        updateItem: (itemId, item) => dispatch(partActions.update(itemId, item)),
        setEditStatus: status => dispatch(partActions.setEditStatus(status)),
        setSnackbarVisible: () => dispatch(partActions.setSnackbarVisible()),
        fetchLiveTest: itemId => dispatch(partLiveTestActions.fetch(itemId)),
        clearErrors: () => dispatch(partActions.clearErrorsForItem()),
        refreshPart: itemId => {
            dispatch(partActions.fetch(itemId));
            dispatch(partLiveTestActions.fetch(itemId));
        },
        clearBomStandardPrices: () => dispatch(bomStandardPricesActions.clearItem())
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Part));
