import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import Wand from '../components/Wand';
import wandConsignmentsActions from '../actions/wandConsignmentsActions';
import wandConsignmentsSelectors from '../selectors/wandConsignmentsSelectors';

const mapStateToProps = state => ({
    loadingWandConsignments: wandConsignmentsSelectors.getLoading(state),
    wandConsignments: wandConsignmentsSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(wandConsignmentsActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Wand));
