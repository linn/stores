import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import BuildTab from '../../../components/parts/tabs/BuildTab';
import sernosSequencesActions from '../../../actions/sernosSequencesActions';
import sernosSequencesSelectors from '../../../selectors/sernosSequencesSelectors';
import assemblyTechnologiesSelectors from '../../../selectors/assemblyTechnologiesSelectors';
import assemblyTechnologiesActions from '../../../actions/assemblyTechnologiesActions';
import decrementRulesSelectors from '../../../selectors/decrementRulesSelectors';
import decrementRulesActions from '../../../actions/decrementRulesActions';
import config from '../../../config';

const mapStateToProps = state => ({
    sernosSequencesSearchResults: sernosSequencesSelectors
        .getSearchItems(state)
        .map?.(s => ({ name: s.sequenceName, description: s.description })),
    sernosSequencesSearchLoading: sernosSequencesSelectors.getSearchLoading(state),
    assemblyTechnologies: assemblyTechnologiesSelectors.getItems(state),
    decrementRules: decrementRulesSelectors.getItems(state),
    appRoot: config.appRoot
});

const initialise = () => dispatch => {
    dispatch(assemblyTechnologiesActions.fetch());
    dispatch(decrementRulesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchSernosSequences: sernosSequencesActions.search,
    clearSernosSequencesSearch: sernosSequencesActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(BuildTab));
