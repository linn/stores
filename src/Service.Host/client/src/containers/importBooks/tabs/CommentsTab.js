import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import CommentsTab from '../../../components/importBooks/tabs/CommentsTab';
import employeesActions from '../../../actions/employeesActions';
import employeesSelectors from '../../../selectors/employeesSelectors';

const mapStateToProps = state => ({
    employees: employeesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(employeesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(CommentsTab));
