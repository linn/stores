import { connect } from 'react-redux';
import {
    getItemError,
    initialiseOnMount,
    getRequestErrors
} from '@linn-it/linn-form-components-library';
import { getUserNumber } from '../../selectors/userSelectors';
import Consignment from '../../components/consignments/Consignment';
import consignmentSelectors from '../../selectors/consignmentSelectors';
import consignmentActions from '../../actions/consignmentActions';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    consignment: consignmentSelectors.getItem(state),
    loading: consignmentSelectors.getLoading(state),
    snackbarVisible: consignmentSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.consignment.item),
    userNumber: getUserNumber(state),
    requestErrors: getRequestErrors(state)?.filter(error => error.type !== 'FETCH_ERROR')
});

const mapDispatchToProps = {
    addItem: consignmentActions.add,
    updateItem: consignmentActions.update,
    setEditStatus: consignmentActions.setEditStatus,
    setSnackbarVisible: consignmentActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Consignment));
