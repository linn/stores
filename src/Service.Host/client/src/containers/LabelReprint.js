import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import printConsignmentLabelActions from '../actions/printConsignmentLabelActions';
import printConsignmentLabelSelectors from '../selectors/printConsignmentLabelSelectors';
import consignmentSelectors from '../selectors/consignmentSelectors';
import consignmentActions from '../actions/consignmentActions';
import addressesActions from '../actions/addressesActions';
import addressesSelectors from '../selectors/addressesSelectors';
import { getUserNumber } from '../selectors/userSelectors';
import LabelReprint from '../components/LabelReprint';

const mapStateToProps = state => ({
    userNumber: getUserNumber(state),
    printConsignmentLabelWorking: printConsignmentLabelSelectors.getWorking(state),
    printConsignmentLabelResult: printConsignmentLabelSelectors.getData(state),
    consignmentItem: consignmentSelectors.getItem(state),
    consignmentLoading: consignmentSelectors.getLoading(state),
    addressesSearchResults: addressesSelectors.getSearchItems(state),
    addressesSearchLoading: addressesSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    printConsignmentLabel: printConsignmentLabelActions.requestProcessStart,
    clearConsignmentLabelData: printConsignmentLabelActions.clearProcessData,
    getConsignment: consignmentActions.fetch,
    searchAddresses: addressesActions.search,
    clearAddresses: addressesActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(LabelReprint));
