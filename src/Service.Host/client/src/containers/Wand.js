import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import Wand from '../components/Wand';
import wandConsignmentsActions from '../actions/wandConsignmentsActions';
import wandConsignmentsSelectors from '../selectors/wandConsignmentsSelectors';
import wandItemsActions from '../actions/wandItemsActions';
import wandItemsSelectors from '../selectors/wandItemsSelectors';
import { getUserNumber } from '../selectors/userSelectors';
import doWandItemSelectors from '../selectors/doWandItemSelectors';
import doWandItemActions from '../actions/doWandItemActions';

const mapStateToProps = state => ({
    loadingWandConsignments: wandConsignmentsSelectors.getLoading(state),
    wandConsignments: wandConsignmentsSelectors.getItems(state),
    items: wandItemsSelectors.getSearchItems(state),
    itemsLoading: wandItemsSelectors.getSearchLoading(state),
    userNumber: getUserNumber(state),
    doWandItemWorking: doWandItemSelectors.getWorking(state),
    doWandItemMessage: doWandItemSelectors.getMessageText(state)
});

const initialise = () => dispatch => {
    dispatch(wandConsignmentsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    getItems: wandItemsActions.search,
    clearItems: wandItemsActions.clearSearch,
    doWandItem: doWandItemActions.requestProcessStart
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Wand));
