import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
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
import { getPrivileges, getUserName, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';

const creating = match => match?.url?.endsWith('/create');

const getOptions = ownProps => {
    const options = queryString.parse(ownProps?.location?.search);
    return options;
};

const mapStateToProps = (state, { match }, ownProps) => ({
    item: creating(match) ? null : partSelectors.getItem(state),
    itemId: creating(match) ? null : match.params.id,
    editStatus: creating(match) ? 'create' : partSelectors.getEditStatus(state),
    loading: partSelectors.getLoading(state),
    snackbarVisible: partSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.part.item),
    departments: departmentsSelectors.getItems(state),
    partCategoris: partCategoriesSelectors.getItems(state),
    rootProducts: rootProductsSelectors.getItems(state),
    sernosSequences: sernosSequencesSelectors.getItems(state),
    suppliers: suppliersSelectors.getItems(state),
    unitsOfMeasure: unitsOfMeasureSelectors.getItems(state),
    nominal: nominalSelectors.getItem(state),
    privileges: getPrivileges(state),
    userName: getUserName(state),
    userNumber: getUserNumber(state),
    options: getOptions(ownProps)
});

const mapDispatchToProps = dispatch => {
    return {
        initialise: ({ itemId }) => {
            if (itemId) {
                dispatch(partActions.fetch(itemId));
            }
            dispatch(departmentsActions.fetch());
            dispatch(partCategoriesActions.fetch());
            dispatch(rootProductsActions.fetch());
            dispatch(sernosSequencesActions.fetch());
            dispatch(suppliersActions.fetch());
            dispatch(unitsOfMeasureActions.fetch());
        },
        addItem: item => dispatch(partActions.add(item)),
        updateItem: (itemId, item) => dispatch(partActions.update(itemId, item)),
        setEditStatus: status => dispatch(partActions.setEditStatus(status)),
        setSnackbarVisible: () => dispatch(partActions.setSnackbarVisible()),
        fetchNominal: () => dispatch(nominalActions.fetch())
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Part));
