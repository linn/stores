import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import sosAllocHeadsActions from '../../actions/sosAllocHeadsActions';
import sosAllocHeadsSelectors from '../../selectors/sosAllocHeadsSelectors';
import SosAllocHeads from '../../components/allocations/SosAllocHeads';
import sosAllocDetailsActions from '../../actions/sosAllocDetailsActions';
import sosAllocDetailsSelectors from '../../selectors/sosAllocDetailsSelectors';

const mapStateToProps = (state, { match }) => ({
    jobId: match.params.jobId,
    items: sosAllocHeadsSelectors.getSearchItems(state),
    loading: sosAllocHeadsSelectors.getSearchLoading(state),
    details: sosAllocDetailsSelectors.getSearchItems(state),
    detailsLoading: sosAllocDetailsSelectors.getSearchLoading(state)

});

const initialise = ( { jobId } ) => dispatch => {
    dispatch(sosAllocHeadsActions.search(jobId));
    dispatch(sosAllocDetailsActions.searchWithOptions(null, `&jobId=${jobId}`));
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(SosAllocHeads));
