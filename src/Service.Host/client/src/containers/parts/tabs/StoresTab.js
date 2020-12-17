import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import StoresTab from '../../../components/parts/tabs/StoresTab';
import tqmsCategoriesSelectors from '../../../selectors/tqmsCategoriesSelectors';
import tqmsCategoriesActions from '../../../actions/tqmsCategoriesActions';

const mapStateToProps = state => ({
    tqmsCategories: tqmsCategoriesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(tqmsCategoriesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(StoresTab));
