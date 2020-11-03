import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import sosAllocHeadsActions from '../../actions/sosAllocHeadsActions';
import sosAllocHeadsSelectors from '../../selectors/sosAllocHeadsSelectors';
import SosAllocHeads from '../../components/allocations/SosAllocHeads';

const mapStateToProps = (state, { match }) => ({
    jobId: match.params.jobId,
    items: sosAllocHeadsSelectors.getSearchItems(state),
    loading: sosAllocHeadsSelectors.getSearchLoading(state)
});

const initialise = ( { jobId } ) => dispatch => {
    dispatch(sosAllocHeadsActions.search(jobId));
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(SosAllocHeads));
