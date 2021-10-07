import { connect } from 'react-redux';
import {
    getItemError,
    initialiseOnMount,
    getRequestErrors
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import { getUserNumber } from '../../selectors/userSelectors';
import Consignment from '../../components/consignments/Consignment';
import consignmentSelectors from '../../selectors/consignmentSelectors';
import consignmentActions from '../../actions/consignmentActions';
import consignmentsSelectors from '../../selectors/consignmentsSelectors';
import consignmentsActions from '../../actions/consignmentsActions';
import * as itemTypes from '../../itemTypes';
import hubSelectors from '../../selectors/hubSelectors';
import hubActions from '../../actions/hubActions';
import hubsSelectors from '../../selectors/hubsSelectors';
import hubsActions from '../../actions/hubsActions';
import carrierSelectors from '../../selectors/carrierSelectors';
import carrierActions from '../../actions/carrierActions';
import carriersSelectors from '../../selectors/carriersSelectors';
import carriersActions from '../../actions/carriersActions';
import shippingTermSelectors from '../../selectors/shippingTermSelectors';
import shippingTermActions from '../../actions/shippingTermActions';
import shippingTermsSelectors from '../../selectors/shippingTermsSelectors';
import shippingTermsActions from '../../actions/shippingTermsActions';
import cartonTypesSelectors from '../../selectors/cartonTypesSelectors';
import cartonTypesActions from '../../actions/cartonTypesActions';
import printConsignmentLabelActions from '../../actions/printConsignmentLabelActions';
import printConsignmentLabelSelectors from '../../selectors/printConsignmentLabelSelectors';
import printConsignmentDocumentsActions from '../../actions/printConsignmentDocumentsActions';
import printConsignmentDocumentsSelectors from '../../selectors/printConsignmentDocumentsSelectors';
import consignmentPackingListSelectors from '../../selectors/consignmentPackingListSelectors';
import consignmentPackingListActions from '../../actions/consignmentPackingListActions';
import saveConsignmentDocumentsActions from '../../actions/saveConsignmentDocumentsActions';
import saveConsignmentDocumentsSelectors from '../../selectors/saveConsignmentDocumentsSelectors';

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    const { match } = ownProps;
    if (match?.params?.consignmentId) {
        return match.params;
    }

    return options;
};

const initialise = ({ options }) => dispatch => {
    if (options.consignmentId) {
        dispatch(consignmentActions.fetch(options.consignmentId));
    }

    dispatch(consignmentsActions.fetch());
    dispatch(hubsActions.fetch());
    dispatch(cartonTypesActions.fetch());
    dispatch(carriersActions.fetch());
    dispatch(shippingTermsActions.fetch());
};

const mapStateToProps = (state, ownProps) => ({
    item: consignmentSelectors.getItem(state),
    loading: consignmentSelectors.getLoading(state),
    snackbarVisible: consignmentSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.consignment.item),
    userNumber: getUserNumber(state),
    requestErrors: getRequestErrors(state)?.filter(error => error.type !== 'FETCH_ERROR'),
    openConsignments: consignmentsSelectors.getItems(state)?.map(c => ({
        id: c.consignmentId,
        displayText: `${c.consignmentId} ${c.customerName}`
    })),
    optionsLoading: consignmentsSelectors.getLoading(state),
    options: getOptions(ownProps),
    startingTab: getOptions(ownProps).consignmentId ? 2 : 0,
    editStatus: consignmentSelectors.getEditStatus(state),
    hub: hubSelectors.getItem(state),
    hubs: hubsSelectors.getItems(state),
    hubsLoading: hubsSelectors.getLoading(state),
    carrier: carrierSelectors.getItem(state),
    carriers: carriersSelectors.getItems(state),
    carriersLoading: carriersSelectors.getLoading(state),
    shippingTerm: shippingTermSelectors.getItem(state),
    shippingTerms: shippingTermsSelectors.getItems(state),
    shippingTermsLoading: shippingTermsSelectors.getLoading(state),
    cartonTypes: cartonTypesSelectors.getItems(state),
    printConsignmentLabelWorking: printConsignmentLabelSelectors.getWorking(state),
    printConsignmentLabelResult: printConsignmentLabelSelectors.getData(state),
    printDocumentsWorking: printConsignmentDocumentsSelectors.getWorking(state),
    printDocumentsResult: printConsignmentDocumentsSelectors.getData(state),
    consignmentPackingList: consignmentPackingListSelectors.getItem(state),
    consignmentPackingListLoading: consignmentPackingListSelectors.getLoading(state),
    cartonTypesSearchResults: cartonTypesSelectors.getSearchItems(state),
    cartonTypesSearchLoading: cartonTypesSelectors.getSearchLoading(state),
    saveDocumentsWorking: saveConsignmentDocumentsSelectors.getWorking(state),
    saveDocumentsResult: saveConsignmentDocumentsSelectors.getData(state)
});

const mapDispatchToProps = {
    initialise,
    addConsignment: consignmentActions.add,
    updateItem: consignmentActions.update,
    createConsignment: consignmentActions.create,
    clearConsignmentErrors: consignmentActions.clearErrorsForItem,
    setEditStatus: consignmentActions.setEditStatus,
    setSnackbarVisible: consignmentActions.setSnackbarVisible,
    getConsignment: consignmentActions.fetch,
    getHub: hubActions.fetchByHref,
    clearHub: hubActions.clearItem,
    getCarrier: carrierActions.fetchByHref,
    getShippingTerm: shippingTermActions.fetchByHref,
    clearShippingTerm: shippingTermActions.clearItem,
    printConsignmentLabel: printConsignmentLabelActions.requestProcessStart,
    clearConsignmentLabelData: printConsignmentLabelActions.clearProcessData,
    printDocuments: printConsignmentDocumentsActions.requestProcessStart,
    printDocumentsClearData: printConsignmentDocumentsActions.clearProcessData,
    getConsignmentPackingList: consignmentPackingListActions.fetchByPath,
    clearConsignmentPackingList: consignmentPackingListActions.clearItem,
    searchCartonTypes: cartonTypesActions.search,
    clearCartonTypesSearch: cartonTypesActions.clearSearch,
    saveDocuments: saveConsignmentDocumentsActions.requestProcessStart,
    saveDocumentsClearData: saveConsignmentDocumentsActions.clearProcessData
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Consignment));
