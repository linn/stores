import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import partTemplatesActions from '../../actions/partTemplatesActions';
import partTemplatesStateActions from '../../actions/partTemplatesStateActions';
import partTemplatesSelectors from '../../selectors/partTemplatesSelectors';
import partTemplateSelectors from '../../selectors/partTemplateSelectors';
import PartTemplatesSearch from '../../components/parts/PartTemplatesSearch';
import { getPrivileges } from '../../selectors/userSelectors';

const mapStateToProps = state => ({
    items: partTemplatesSelectors.getItems(state),
    loading: partTemplatesSelectors.getLoading(state),
    privileges: getPrivileges(state),
    partTemplates: partTemplatesSelectors.getItems(state),
    partTemplate: partTemplateSelectors.getItem(state),
    applicationState: partTemplatesSelectors.getApplicationState(state)
});

const initialise = () => dispatch => {
    dispatch(partTemplatesActions.fetch());
    dispatch(partTemplatesStateActions.fetchState());
};

const mapDispatchToProps = {
    initialise,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartTemplatesSearch));
