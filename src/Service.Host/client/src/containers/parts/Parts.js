import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import PartsSearch from '../../components/parts/PartsSearch';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import { getPrivileges } from '../../selectors/userSelectors';
import partTemplatesActions from '../../actions/partTemplatesActions';
import partTemplatesSelectors from '../../selectors/partTemplatesSelectors';

const mapStateToProps = (state, { match }) => ({
    linkToSources: match?.url?.endsWith('/sources'),
    items: partsSelectors.getSearchItems(state),
    loading: partsSelectors.getSearchLoading(state),
    privileges: getPrivileges(state),
    partTemplates: partTemplatesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(partTemplatesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    fetchItems: partsActions.search,
    clearSearch: partsActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartsSearch));
