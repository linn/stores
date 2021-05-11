import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import ConsignmentShipfiles from '../components/ConsignmentShipfiles';
import consignmentShipfilesActions from '../actions/consignmentShipfilesActions';
import consignmentShipfilesSelectors from '../selectors/consignmentShipfilesSelectors';
import * as processTypes from '../processTypes';
import shipfilesSendEmailsSelectors from '../selectors/shipfilesSendEmailsSelectors';
import shipfilesSendEmailsActions from '../actions/shipfilesSendEmailsActions';

const mapStateToProps = state => ({
    consignmentShipfiles: consignmentShipfilesSelectors.getItems(state),
    consignmentShipfilesLoading: consignmentShipfilesSelectors.getLoading(state),
    processedShipfiles: shipfilesSendEmailsSelectors.getData(state)?.processed,
    sendEmailsLoading: shipfilesSendEmailsSelectors.getWorking(state),
    itemError: getItemError(state, processTypes.shipfileSendEmails.item)
});

const initialise = () => dispatch => {
    dispatch(consignmentShipfilesActions.fetch());
    shipfilesSendEmailsActions.clearErrorsForItem();
};

const mapDispatchToProps = {
    initialise,
    sendEmails: shipfilesSendEmailsActions.requestProcessStart,
    clearErrors: shipfilesSendEmailsActions.clearErrorsForItem,
    clearData: shipfilesSendEmailsActions.clearProcessData
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ConsignmentShipfiles));
