import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import decrementRulesActions from '../../../actions/decrementRulesActions';
import partLibrariesSelectors from '../../../selectors/partLibrariesSelectors';
import partLibrariesActions from '../../../actions/partLibrariesActions';
import CadInfoTab from '../../../components/parts/tabs/CadInfoTab';

const mapStateToProps = state => ({
    partLibraries: partLibrariesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(partLibrariesActions.fetch());
    dispatch(decrementRulesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(CadInfoTab));
