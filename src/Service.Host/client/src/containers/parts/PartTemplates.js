import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import partTemplatesActions from '../../actions/partTemplatesActions';
import partTemplatesSelectors from '../../selectors/partTemplatesSelectors';
import PartTemplatesSearch from '../../components/parts/PartTemplatesSearch';
import { getPrivileges } from '../../selectors/userSelectors';

const mapStateToProps = state => ({
    items: partTemplatesSelectors.getItems(state),
    loading: partTemplatesSelectors.getLoading(state),
    privileges: getPrivileges(state)
});

const mapDispatchToProps = {
    fetchItems: partTemplatesActions.fetchByQueryString,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartTemplatesSearch));
