import { connect } from 'react-redux';
import ManufacturersTab from '../../../../components/parts/mechPartSource/tabs/ManufacturersTab';
import manufacturersSelectors from '../../../../selectors/manufacturersSelectors';
import manufacturersActions from '../../../../actions/manufacturersActions';
import employeesSelectors from '../../../../selectors/employeesSelectors';
import employeesActions from '../../../../actions/employeesActions';

const mapStateToProps = state => ({
    loading: manufacturersSelectors.getLoading(state),
    manufacturersSearchResults: manufacturersSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.code, id: c.code, description: c.description })),
    manufacturersSearchLoading: manufacturersSelectors.getSearchLoading(state),
    employeesSearchResults: employeesSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.id.toString(), description: c.fullName, id: c.id })),
    employeesSearchLoading: employeesSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchManufacturers: manufacturersActions.search,
    clearManufacturersSearch: manufacturersActions.clearSearch,
    searchEmployees: employeesActions.search,
    clearEmployeesSearch: employeesActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(ManufacturersTab);
