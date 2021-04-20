import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import exportReturnActions from '../actions/exportReturnActions';
import exportReturnSelectors from '../selectors/exportReturnSelectors';
import makeIntercompanyInvoicesActions from '../actions/makeIntercompanyInvoicesActions';
import makeIntercompanyInvoicesSelectors from '../selectors/makeIntercompanyInvoicesSelectors';
import * as processTypes from '../processTypes';
import ExportReturn from '../components/exportReturns/ExportReturn';
import interCompanyInvoicesActions from '../actions/interCompanyInvoicesActions';
import interCompanyInvoicesSelectors from '../selectors/interCompanyInvoicesSelectors';

const mapStateToProps = (state, { match }) => ({
    exportReturnId: match.params.id,
    exportReturnLoading: exportReturnSelectors.getLoading(state),
    exportReturn: exportReturnSelectors.getItem(state),
    makeIntercompanyInvoicesMessageVisible: makeIntercompanyInvoicesSelectors.getMessageVisible(
        state
    ),
    makeIntercompanyInvoicesMessageText: makeIntercompanyInvoicesSelectors.getMessageText(state),
    makeIntercompanyInvoicesErrorMessage: getItemErrorDetailMessage(
        state,
        processTypes.makeIntercompanyInvoices.item
    ),
    makeIntercompanyInvoicesWorking: makeIntercompanyInvoicesSelectors.getWorking(state),
    interCompanyInvoicesLoading: interCompanyInvoicesSelectors.getSearchLoading(state),
    interCompanyInvoices: interCompanyInvoicesSelectors.getSearchItems(state)
});

const initialise = ({ exportReturnId }) => dispatch => {
    dispatch(exportReturnActions.fetch(exportReturnId));
};

const mapDispatchToProps = {
    initialise,
    updateExportReturn: exportReturnActions.update,
    makeIntercompanyInvoices: makeIntercompanyInvoicesActions.requestProcessStart,
    clearMakeIntercompanyInvoicesErrors: makeIntercompanyInvoicesActions.clearErrorsForItem,
    setMakeIntercompanyInvoicesMessageVisible: makeIntercompanyInvoicesActions.setMessageVisible,
    searchInterCompanyInvoices: interCompanyInvoicesActions.search
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ExportReturn));
