import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import ImpBookTab from '../../../components/importBooks/tabs/ImpBookTab';
import employeesActions from '../../../actions/employeesActions';
import employeesSelectors from '../../../selectors/employeesSelectors';
import suppliersSelectors from '../../../selectors/suppliersSelectors';
import suppliersActions from '../../../actions/suppliersActions';
import config from '../../../config';

const mapStateToProps = state => ({
    suppliersSearchResults: suppliersSelectors.getSearchItems(state).map?.(c => ({
        id: c.id,
        name: c.id.toString(),
        description: c.name,
        country: c.countryCode
    })),
    suppliersSearchLoading: suppliersSelectors.getSearchLoading(state),
    employees: employeesSelectors.getItems(state),
    appRoot: config.appRoot,
    supplierItem: suppliersSelectors.getItem(state)
});

const initialise = () => dispatch => {
    dispatch(employeesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchSuppliers: suppliersActions.search,
    clearSuppliersSearch: suppliersActions.clearSearch,
    getSupplier: suppliersActions.fetchById
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ImpBookTab));
