import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import decrementRulesActions from '../../../actions/decrementRulesActions';
import partLibrariesSelectors from '../../../selectors/partLibrariesSelectors';
import partLibrariesActions from '../../../actions/partLibrariesActions';
import CadInfoTab from '../../../components/parts/tabs/CadInfoTab';
import partLibraryRefsSelectors from '../../../selectors/partLibraryRefsSelectors';
import partLibraryRefsActions from '../../../actions/partLibraryRefsActions';
import footprintRefOptionsSelectors from '../../../selectors/footprintRefOptionsSelectors';
import footprintRefOptionsActions from '../../../actions/footprintRefOptionsActions';

const mapStateToProps = state => ({
    partLibraries: partLibrariesSelectors.getItems(state),
    partLibraryRefs: partLibraryRefsSelectors.getItems(state),
    footprintRefOptions: footprintRefOptionsSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(partLibrariesActions.fetch());
    dispatch(decrementRulesActions.fetch());
    dispatch(partLibraryRefsActions.fetch());
    dispatch(footprintRefOptionsActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(CadInfoTab));
