import { connect } from 'react-redux';
import {
    initialiseOnMount,
    getItemErrorDetailMessage
} from '@linn-it/linn-form-components-library';
import sosAllocHeadsActions from '../../actions/sosAllocHeadsActions';
import sosAllocHeadsSelectors from '../../selectors/sosAllocHeadsSelectors';
import SosAllocHeads from '../../components/allocations/SosAllocHeads';
import sosAllocDetailActions from '../../actions/sosAllocDetailActions';
import sosAllocDetailsActions from '../../actions/sosAllocDetailsActions';
import sosAllocDetailsSelectors from '../../selectors/sosAllocDetailsSelectors';
import finishAllocationActions from '../../actions/finishAllocationActions';
import * as processTypes from '../../processTypes';

const mapStateToProps = (state, { match }) => ({
    jobId: match.params.jobId,
    items: sosAllocHeadsSelectors.getSearchItems(state),
    loading: sosAllocHeadsSelectors.getSearchLoading(state),
    details: sosAllocDetailsSelectors.getSearchItems(state),
    detailsLoading: sosAllocDetailsSelectors.getSearchLoading(state),
    allocationError: getItemErrorDetailMessage(state, processTypes.finishAllocation.item)
});

const initialise = ({ jobId }) => dispatch => {
    dispatch(sosAllocHeadsActions.search(jobId));
    dispatch(finishAllocationActions.clearErrorsForItem());
    dispatch(sosAllocDetailsActions.searchWithOptions(null, `&jobId=${jobId}`));
};

const mapDispatchToProps = {
    initialise,
    updateDetail: sosAllocDetailActions.update,
    finishAllocation: finishAllocationActions.requestProcessStart
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(SosAllocHeads));
