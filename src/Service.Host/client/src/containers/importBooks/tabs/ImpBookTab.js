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
import impbookDeliveryTermsActions from '../../../actions/impbookDeliveryTermsActions';
import impbookDeliveryTermsSelectors from '../../../selectors/impbookDeliveryTermsSelectors';
import portsActions from '../../../actions/portsActions';
import portsSelectors from '../../../selectors/portsSelectors';
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
    transportCodes: transportCodesSelectors.getItems(state)?.map(e => ({
        displayText: `${e.transportId} (${e.description})`,
        id: parseInt(e.transportId, 10)
    })),
    transactionCodes: transactionCodesSelectors.getItems(state)?.map(e => ({
        displayText: `${e.transactionId}`,
        id: parseInt(e.transactionId, 10)
    })),
    deliveryTerms: impbookDeliveryTermsSelectors.getItems(state)?.map(e => ({
        displayText: `${e.deliveryTermCode} (${e.description})`,
        id: e.deliveryTermCode
    })),
    ports: portsSelectors.getItems(state)?.map(e => ({
        displayText: `${e.portCode} (${e.description})`,
        id: e.portCode
    }))
});

const initialise = () => dispatch => {
    dispatch(employeesActions.fetch());
    dispatch(suppliersActions.fetch());
    dispatch(transportCodesActions.fetch());
    dispatch(transactionCodesActions.fetch());
    dispatch(impbookDeliveryTermsActions.fetch());
    dispatch(portsActions.fetch());
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
