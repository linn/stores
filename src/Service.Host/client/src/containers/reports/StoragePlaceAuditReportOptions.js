import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import {
    initialiseOnMount,
    getItemErrorDetailMessage
} from '@linn-it/linn-form-components-library';
import auditLocationsActions from '../../actions/auditLocationsActions';
import auditLocationsSelectors from '../../selectors/auditLocationsSelectors';
import storagePlacesActions from '../../actions/storagePlacesActions';
import storagePlacesSelectors from '../../selectors/storagePlacesSelectors';
import createAuditReqsActions from '../../actions/createAuditReqsActions';
import createAuditReqsSelectors from '../../selectors/createAuditReqsSelectors';
import StoragePlaceAuditReportOptions from '../../components/reports/StoragePlaceAuditReportOptions';
import * as processTypes from '../../processTypes';

const mapStateToProps = state => ({
    storagePlacesSearchLoading: storagePlacesSelectors.getSearchLoading(state),
    storagePlacesSearchResults: storagePlacesSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.name, name: s.name, displayText: s.name })),
    auditLocationsSearchLoading: auditLocationsSelectors.getSearchLoading(state),
    auditLocationsSearchResults: auditLocationsSelectors
        .getSearchItems(state)
        .map(a => ({ ...a, id: a.storagePlace, name: a.storagePlace })),
    createAuditReqsMessageVisible: createAuditReqsSelectors.getMessageVisible(state),
    createAuditReqsMessageText: createAuditReqsSelectors.getMessageText(state),
    createAuditReqsErrorMessage: getItemErrorDetailMessage(
        state,
        processTypes.createAuditReqs.item
    ),
    createAuditReqsWorking: createAuditReqsSelectors.getWorking(state)
});

const initialise = () => dispatch => {
    dispatch(storagePlacesActions.clearSearch());
    dispatch(auditLocationsActions.clearSearch());
};

const mapDispatchToProps = {
    initialise,
    searchStoragePlaces: storagePlacesActions.search,
    clearStoragePlacesSearch: storagePlacesActions.clearSearch,
    searchAuditLocations: auditLocationsActions.search,
    clearAuditLocationsSearch: auditLocationsActions.clearSearch,
    createAuditReqs: createAuditReqsActions.requestProcessStart,
    clearAuditReqsErrors: createAuditReqsActions.clearErrorsForItem,
    setAuditReqsMessageVisible: createAuditReqsActions.setMessageVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withRouter(initialiseOnMount(StoragePlaceAuditReportOptions)));
