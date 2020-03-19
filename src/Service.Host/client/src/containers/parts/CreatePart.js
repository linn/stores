import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import partsActions from '../../actions/partsActions';
import Part from '../../components/parts/Part';
import partsSelectors from '../../selectors/partsSelectors';
import * as itemTypes from '../../itemTypes';

const mapStpartsToProps = state => ({
    item: {},
    editStatus: 'creparts',
    itemError: getItemError(state, itemTypes.part.item),
    loading: partsSelectors.getLoading(state),
    snackbarVisible: partsSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(partsActions.setEditStatus('creparts'));
    dispatch(partsActions.clearErrorsForItem());
};

const mapDispatchToProps = {
    initialise,
    addItem: partsActions.add,
    setEditStatus: partsActions.setEditStatus,
    setSnackbarVisible: partsActions.setSnackbarVisible
};

export default connect(mapStpartsToProps, mapDispatchToProps)(initialiseOnMount(Part));
