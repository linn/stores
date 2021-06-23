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

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const initialise = ({ options }) => dispatch => {
    if (options.consignmentId) {
        dispatch(consignmentActions.fetch(options.consignmentId));
    }

    dispatch(consignmentsActions.fetch());
    dispatch(hubsActions.fetch());
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
    openConsignments: consignmentsSelectors.getItems(state),
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
    shippingTermsLoading: shippingTermsSelectors.getLoading(state)
});

const mapDispatchToProps = {
    initialise,
    addItem: consignmentActions.add,
    updateItem: consignmentActions.update,
    clearConsignmentErrors: consignmentActions.clearErrorsForItem,
    setEditStatus: consignmentActions.setEditStatus,
    setSnackbarVisible: consignmentActions.setSnackbarVisible,
    getConsignment: consignmentActions.fetch,
    getHub: hubActions.fetchByHref,
    clearHub: hubActions.clearItem,
    getCarrier: carrierActions.fetchByHref,
    getShippingTerm: shippingTermActions.fetchByHref,
    clearShippingTerm: shippingTermActions.clearItem
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Consignment));
