import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import partTemplatesActions from '../../actions/partTemplatesActions';
import partTemplatesSelectors from '../../selectors/partTemplatesSelectors';
import partTemplateSelectors from '../../selectors/partTemplateSelectors';
import PartTemplatesSearch from '../../components/parts/PartTemplatesSearch';
import { getPrivileges } from '../../selectors/userSelectors';

const mapStateToProps = (state, { match }) => ({
    items: partTemplatesSelectors.getItems(state),
    loading: partTemplatesSelectors.getLoading(state),
    linkToSources: match?.url?.endsWith('/sources'),
    privileges: getPrivileges(state),
    partTemplates: partTemplatesSelectors.getItems(state),
    partTemplate: partTemplateSelectors.getItem(state)
});

const initialise = () => dispatch => {
    dispatch(partTemplatesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    fetchItems: partTemplatesActions.fetchByQueryString,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartTemplatesSearch));
