import { connect } from 'react-redux';
import { initialiseOnMount, getItemError } from '@linn-it/linn-form-components-library';
import DebitNotes from '../../components/purchasing/DebitNotes';
import debitNotesActions from '../../actions/purchasing/debitNotesActions';
import debitNoteActions from '../../actions/purchasing/debitNoteActions';
import debitNoteSelectors from '../../selectors/purchasing/debitNoteSelectors';
import debitNotesSelectors from '../../selectors/purchasing/debitNotesSelectors';
import { debitNotes } from '../../itemTypes';

const mapStateToProps = state => ({
    items: debitNotesSelectors.getItems(state),
    itemsLoading: debitNotesSelectors.getLoading(state),
    snackbarVisible: debitNoteSelectors.getSnackbarVisible(state),
    itemsError: getItemError(state, debitNotes.item)
});

const initialise = () => dispatch => {
    dispatch(debitNotesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateDebitNote: debitNoteActions.update
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(DebitNotes));
