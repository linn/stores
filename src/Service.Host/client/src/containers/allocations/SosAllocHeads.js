import { connect } from 'react-redux';
import {
    initialiseOnMount,
    getItemErrorDetailMessage,
    utilities
} from '@linn-it/linn-form-components-library';
import sosAllocHeadsActions from '../../actions/sosAllocHeadsActions';
import sosAllocHeadsSelectors from '../../selectors/sosAllocHeadsSelectors';
import SosAllocHeads from '../../components/allocations/SosAllocHeads';
import sosAllocDetailActions from '../../actions/sosAllocDetailActions';
import sosAllocDetailsActions from '../../actions/sosAllocDetailsActions';
import sosAllocDetailsSelectors from '../../selectors/sosAllocDetailsSelectors';
import finishAllocationActions from '../../actions/finishAllocationActions';
import finishAllocationSelectors from '../../selectors/finishAllocationSelectors';
import * as processTypes from '../../processTypes';
import pickItemsAllocationActions from '../../actions/pickItemsAllocationActions';
import pickItemsAllocationSelectors from '../../selectors/pickItemsAllocationSelectors';
import unpickItemsAllocationActions from '../../actions/unpickItemsAllocationActions';
import unpickItemsAllocationSelectors from '../../selectors/unpickItemsAllocationSelectors';

const mapStateToProps = (state, { match }) => ({
    jobId: match.params.jobId,
    items: utilities.sortEntityList(sosAllocHeadsSelectors.getSearchItems(state), 'outletName'),
    loading: sosAllocHeadsSelectors.getSearchLoading(state),
    details: sosAllocDetailsSelectors.getSearchItems(state),
    detailsLoading: sosAllocDetailsSelectors.getSearchLoading(state),
    allocationError: getItemErrorDetailMessage(state, processTypes.finishAllocation.item),
    finishAllocationWorking: finishAllocationSelectors.getWorking(state),
    pickItemsAllocationWorking: pickItemsAllocationSelectors.getWorking(state),
    unpickItemsAllocationWorking: unpickItemsAllocationSelectors.getWorking(state)
});

const initialise = ({ jobId }) => dispatch => {
    dispatch(sosAllocHeadsActions.search(jobId));
    dispatch(sosAllocDetailsActions.searchWithOptions(null, `&jobId=${jobId}`));
};

const mapDispatchToProps = {
    initialise,
    updateDetail: sosAllocDetailActions.update,
    finishAllocation: finishAllocationActions.requestProcessStart,
    pickItemsAllocation: pickItemsAllocationActions.requestProcessStart,
    unpickItemsAllocation: unpickItemsAllocationActions.requestProcessStart,
    clearAllocationError: finishAllocationActions.clearErrorsForItem
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(SosAllocHeads));
