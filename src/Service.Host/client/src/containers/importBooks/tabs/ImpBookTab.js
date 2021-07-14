import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import ImpBookTab from '../../../components/importBooks/tabs/ImpBookTab';
import employeesActions from '../../../actions/employeesActions';
import employeesSelectors from '../../../selectors/employeesSelectors';
import suppliersApprovedCarrierActions from '../../../actions/suppliersApprovedCarrierActions';
import suppliersApprovedCarrierSelectors from '../../../selectors/suppliersApprovedCarrierSelectors';
import suppliersActions from '../../../actions/suppliersActions';
import suppliersSelectors from '../../../selectors/suppliersSelectors';
import transportCodesActions from '../../../actions/impbookTransportCodesActions';
import transportCodesSelectors from '../../../selectors/impbookTransportCodesSelectors';
import transactionCodesActions from '../../../actions/impbookTransactionCodesActions';
import transactionCodesSelectors from '../../../selectors/impbookTransactionCodesSelectors';
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
    supplierItem: suppliersSelectors.getItem(state),
    allSuppliers: suppliersSelectors.getItems(state),
    carriersSearchResults: suppliersApprovedCarrierSelectors.getSearchItems(state).map?.(c => ({
        id: c.id,
        name: c.id.toString(),
        description: c.name,
        country: c.countryCode
    })),
    carriersSearchLoading: suppliersApprovedCarrierSelectors.getSearchLoading(state),
    transportCodes: transportCodesSelectors.getItems(state),
    transactionCodes: transactionCodesSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(employeesActions.fetch());
    dispatch(suppliersActions.fetch());
    dispatch(transportCodesActions.fetch());
    dispatch(transactionCodesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchSuppliers: suppliersActions.search,
    clearSuppliersSearch: suppliersActions.clearSearch,
    getSupplier: suppliersActions.fetchById,
    searchCarriers: suppliersApprovedCarrierActions.search,
    clearCarriersSearch: suppliersApprovedCarrierActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ImpBookTab));
