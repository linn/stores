import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import partSourcesActions from '../../actions/partSourcesActions';
import PartSources from '../../components/parts/PartSources';
import partSourcesSelectors from '../../selectors/partSourcesSelectors';
import departmentsActions from '../../actions/departmentsActions';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import employeesActions from '../../actions/employeesActions';
import employeesSelectors from '../../selectors/employeesSelectors';

const mapStateToProps = state => ({
    items: partSourcesSelectors.getSearchItems(state),
    loading: partSourcesSelectors.getSearchLoading(state),
    projectDepartments: departmentsSelectors.getSearchItems(state),
    employees: employeesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(departmentsActions.searchWithOptions(null, '&projectDeptsOnly=True'));
    dispatch(employeesActions.fetch());
};

const mapDispatchToProps = {
    search: partSourcesActions.searchWithOptions,
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PartSources));
