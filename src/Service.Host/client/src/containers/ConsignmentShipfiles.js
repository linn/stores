import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import ConsignmentShipfiles from '../components/ConsignmentShipfiles';
import consignmentShipfileActions from '../actions/consignmentShipfileActions';
import consignmentShipfilesActions from '../actions/consignmentShipfilesActions';
import consignmentShipfileSelectors from '../selectors/consignmentShipfileSelectors';
import consignmentShipfilesSelectors from '../selectors/consignmentShipfilesSelectors';
import * as processTypes from '../processTypes';
import * as itemTypes from '../itemTypes';
import shipfilesSendEmailsSelectors from '../selectors/shipfilesSendEmailsSelectors';
import shipfilesSendEmailsActions from '../actions/shipfilesSendEmailsActions';

const mapStateToProps = state => ({
    consignmentShipfiles: consignmentShipfilesSelectors.getItems(state),
    consignmentShipfilesLoading: consignmentShipfilesSelectors.getLoading(state),
    processedShipfiles: shipfilesSendEmailsSelectors.getData(state),
    sendEmailsLoading: shipfilesSendEmailsSelectors.getWorking(state),
    processError: getItemError(state, processTypes.shipfilesSendEmails.item),
    itemError: getItemError(state, itemTypes.consignmentShipfile.item),
    deleteLoading: consignmentShipfileSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(consignmentShipfilesActions.fetch());
    shipfilesSendEmailsActions.clearErrorsForItem();
};

const mapDispatchToProps = {
    initialise,
    fetchShipfiles: consignmentShipfilesActions.fetch,
    sendEmails: shipfilesSendEmailsActions.requestProcessStart,
    clearProcessErrors: shipfilesSendEmailsActions.clearErrorsForItem,
    clearItemErrors: consignmentShipfileActions.clearErrorsForItem,
    clearData: shipfilesSendEmailsActions.clearProcessData,
    deleteShipfile: consignmentShipfileActions.delete,
    addShipfile: consignmentShipfileActions.add
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ConsignmentShipfiles));
