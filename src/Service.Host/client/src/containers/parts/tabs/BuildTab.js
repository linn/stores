import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import BuildTab from '../../../components/parts/tabs/BuildTab';
import sernosSequencesActions from '../../../actions/sernosSequencesActions';
import sernosSequencesSelectors from '../../../selectors/sernosSequencesSelectors';

const mapStateToProps = (state, ownProps) => ({
    sernosSequencesSearchResults: sernosSequencesSelectors
        .getSearchItems(state)
        .map?.(s => ({ name: s.sequenceName, description: s.description })),
    sernosSequencesSearchLoading: sernosSequencesSelectors.getSearchLoading(state)

    // departmentsSearchResults: departmentsSelectors
    //     .getSearchItems(state)
    //     .map(c => ({ name: c.departmentCode, description: c.description }))
});

const initialise = () => dispatch => {
    //dispatch(productAnalysisCodesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchSernosSequences: sernosSequencesActions.search,
    clearSernosSequencesSearch: sernosSequencesActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(BuildTab));
