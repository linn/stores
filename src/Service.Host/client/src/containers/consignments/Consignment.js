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

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const initialise = ({ options }) => dispatch => {
    if (options.consignmentId) {
        dispatch(consignmentActions.fetch(options.consignmentId));
    }

    dispatch(consignmentsActions.fetch());
};

const mapStateToProps = (state, ownProps) => ({
    consignment: consignmentSelectors.getItem(state),
    loading: consignmentSelectors.getLoading(state),
    snackbarVisible: consignmentSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.consignment.item),
    userNumber: getUserNumber(state),
    requestErrors: getRequestErrors(state)?.filter(error => error.type !== 'FETCH_ERROR'),
    openConsignments: consignmentsSelectors.getItems(state),
    optionsLoading: consignmentsSelectors.getLoading(state),
    options: getOptions(ownProps),
    startingTab: getOptions(ownProps).consignmentId ? 1 : 0
});

const mapDispatchToProps = {
    initialise,
    addItem: consignmentActions.add,
    updateItem: consignmentActions.update,
    setEditStatus: consignmentActions.setEditStatus,
    setSnackbarVisible: consignmentActions.setSnackbarVisible,
    getConsignment: consignmentActions.fetch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Consignment));
