import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import sosAllocHeadsActions from '../../actions/sosAllocHeadsActions';
import sosAllocHeadsSelectors from '../../selectors/sosAllocHeadsSelectors';
import SosAllocHeads from '../../components/allocations/SosAllocHeads';

const mapStateToProps = (state, { props }) => ({
    index: props.location.search,
    items: sosAllocHeadsSelectors.getSearchItems(state),
    loading: sosAllocHeadsSelectors.getSearchLoading(state)
});

export default connect(mapStateToProps, null)(initialiseOnMount(SosAllocDetails));
