import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import workstationTopUpStatusActions from '../../actions/workstationTopUpStatusActions';
import * as itemTypes from '../../itemTypes';
import workstationTopUpStatusSelectors from '../../selectors/workstationTopUpStatusSelectors';
import WorkstationTopUpStatus from '../../components/workstations/WorkstationTopUpStatus';

const mapStateToProps = state => ({
    item: workstationTopUpStatusSelectors.getItem(state),
    itemError: getItemError(state, itemTypes.allocation.item),
    loading: workstationTopUpStatusSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(workstationTopUpStatusActions.clearErrorsForItem());
    dispatch(workstationTopUpStatusActions.fetchByHref(itemTypes.workstationTopUpStatus.uri));
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(WorkstationTopUpStatus));
