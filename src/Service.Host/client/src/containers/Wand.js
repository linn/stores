import { connect } from 'react-redux';
import { initialiseOnMount, utilities } from '@linn-it/linn-form-components-library';
import Wand from '../components/Wand';
import wandConsignmentsActions from '../actions/wandConsignmentsActions';
import wandConsignmentsSelectors from '../selectors/wandConsignmentsSelectors';
import wandItemsActions from '../actions/wandItemsActions';
import unallocateConsignmentActions from '../actions/unallocateConsignmentActions';
import unallocateConsignmentLineActions from '../actions/unallocateConsignmentLineActions';
import wandItemsSelectors from '../selectors/wandItemsSelectors';
import { getUserNumber } from '../selectors/userSelectors';
import doWandItemSelectors from '../selectors/doWandItemSelectors';
import doWandItemActions from '../actions/doWandItemActions';
import unallocateConsignmentSelectors from '../selectors/unallocateConsignmentSelectors';
import unallocateConsignmentLineSelectors from '../selectors/unallocateConsignmentLineSelectors';

const mapStateToProps = state => ({
    loadingWandConsignments: wandConsignmentsSelectors.getLoading(state),
    wandConsignments: utilities.sortEntityList(
        wandConsignmentsSelectors.getItems(state),
        'consignmentId'
    ),
    items: wandItemsSelectors.getSearchItems(state),
    itemsLoading: wandItemsSelectors.getSearchLoading(state),
    userNumber: getUserNumber(state),
    doWandItemWorking: doWandItemSelectors.getWorking(state),
    wandResult: doWandItemSelectors.getData(state),
    unallocateConsignmentResult: unallocateConsignmentSelectors.getData(state),
    unallocateConsignmentLineResult: unallocateConsignmentLineSelectors.getData(state),
    unallocateConsignmentWorking: unallocateConsignmentSelectors.getWorking(state),
    unallocateConsignmentLineWorking: unallocateConsignmentLineSelectors.getWorking(state)
});

const initialise = () => dispatch => {
    dispatch(wandConsignmentsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    getItems: wandItemsActions.search,
    clearItems: wandItemsActions.clearSearch,
    doWandItem: doWandItemActions.requestProcessStart,
    unallocateConsignment: unallocateConsignmentActions.requestProcessStart,
    unallocateConsignmentLine: unallocateConsignmentLineActions.requestProcessStart,
    clearUnallocateConsignment: unallocateConsignmentActions.clearProcessData,
    clearUnallocateConsignmentLine: unallocateConsignmentLineActions.clearProcessData
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Wand));
