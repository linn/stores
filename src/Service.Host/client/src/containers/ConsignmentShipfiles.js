import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import ConsignmentShipfiles from '../components/ConsignmentShipfiles';
import consignmentShipfilesActions from '../actions/consignmentShipfilesActions';
import consignmentShipfilesSelectors from '../selectors/consignmentShipfilesSelectors';
import * as processTypes from '../processTypes';

const mapStateToProps = state => ({
    consignmentShipfiles: consignmentShipfilesSelectors.getItems(state),
    consignmentShipfilesLoading: consignmentShipfilesSelectors.getLoading(state),
    //itemError: getItemError(state, processTypes.consignmentShipfiles.item),
    //shipfilesSent: consignmentShipfilesSelectors.getData(state)?.transferred
});

const initialise = () => dispatch => {
    dispatch(consignmentShipfilesActions.fetch());
    //consignmentShipfilesActions.clearErrorsForItem();
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ConsignmentShipfiles));
