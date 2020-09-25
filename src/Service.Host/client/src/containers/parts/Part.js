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

const defaults = state => ({
    partNumber: '',
    description: '',
    accountingCompany: 'LINN',
    psuPart: 'No',
    stockControlled: 'Yes',
    cccCriticalPart: 'No',
    safetyCriticalPart: 'No',
    paretoCode: 'U',
    createdBy: getUserNumber(state),
    dateCreated: new Date(),
    railMethod: 'POLICY',
    preferredSupplier: 4415,
    preferredSupplierName: 'Linn Products Ltd',
    qcInformation: ''
});

const creating = match => match?.url?.endsWith('/create');

const getOptions = ownProps => {
    const options = queryString.parse(ownProps?.location?.search);
    return options;
};

const mapStateToProps = (state, { match }, ownProps) => ({
    item: creating(match) ? defaults(state) : partSelectors.getItem(state),
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

const initialise = ({ itemId }) => dispatch => {
    if (itemId) {
        dispatch(partActions.fetch(itemId));
    }
    dispatch(departmentsActions.fetch());
    dispatch(partCategoriesActions.fetch());
    dispatch(rootProductsActions.fetch());
    dispatch(sernosSequencesActions.fetch());
    dispatch(suppliersActions.fetch());
    dispatch(unitsOfMeasureActions.fetch());
};

const mapDispatchToProps = (_, { match }) => {
    return {
        initialise,
        saveItem: creating(match) ? partActions.add : partActions.update,
        setEditStatus: partActions.setEditStatus,
        setSnackbarVisible: partActions.setSnackbarVisible,
        fetchNominal: nominalActions.fetch
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Part));
