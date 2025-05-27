import { connect } from 'react-redux';
import { initialiseOnMount, utilities } from '@linn-it/linn-form-components-library';
import ImpBookTab from '../../../components/importBooks/tabs/ImpBookTab';
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
import currenciesActions from '../../../actions/currenciesActions';
import currenciesSelectors from '../../../selectors/currenciesSelectors';
import parcelsByNumberActions from '../../../actions/parcelsByNumberActions';
import parcelsSelectors from '../../../selectors/parcelsSelectors';
import config from '../../../config';

const mapStateToProps = state => ({
    suppliersSearchResults: suppliersSelectors.getSearchItems(state).map?.(c => ({
        id: c.id,
        name: c.id.toString(),
        description: c.name,
        country: c.countryCode
    })),
    suppliersSearchLoading: suppliersSelectors.getSearchLoading(state),
    appRoot: config.appRoot,
    supplierItem: suppliersSelectors.getItem(state),
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
        displayText: `${e.description}`,
        id: parseInt(e.transactionId, 10)
    })),
    deliveryTerms: utilities
        .sortEntityList(impbookDeliveryTermsSelectors.getItems(state), 'sortOrder')
        ?.map(e => ({
            displayText: `${e.deliveryTermCode} (${e.description})`,
            id: e.deliveryTermCode
        })),
    ports: utilities.sortEntityList(portsSelectors.getItems(state), 'sortOrder')?.map(e => ({
        displayText: `${e.portCode} (${e.description})`,
        id: e.portCode
    })),
    currencies: currenciesSelectors.getItems(state)?.map(c => ({
        displayText: `${c.code} - ${c.name}`,
        id: c.code
    })),
    parcelsSearchResults: parcelsSelectors.getSearchItems(state).map?.(p => ({
        ...p,
        id: p.parcelNumber,
        name: p.parcelNumber.toString()
    })),
    parcelsSearchLoading: parcelsSelectors.getSearchLoading(state)
});

const initialise = props => dispatch => {
    if (
        !props.transportCodes?.length ||
        !props.transactionCodes?.length ||
        !props.deliveryTerms?.length ||
        !props.ports?.length
    ) {
        dispatch(transportCodesActions.fetch());
        dispatch(transactionCodesActions.fetch());
        dispatch(impbookDeliveryTermsActions.fetch());
        dispatch(portsActions.fetch());
        dispatch(currenciesActions.fetch());
    }
};

const mapDispatchToProps = {
    initialise,
    searchSuppliers: suppliersActions.search,
    clearSuppliersSearch: suppliersActions.clearSearch,
    getSupplier: suppliersActions.fetchById,
    searchCarriers: suppliersApprovedCarrierActions.search,
    clearCarriersSearch: suppliersApprovedCarrierActions.clearSearch,
    searchParcels: parcelsByNumberActions.search,
    clearParcelsSearch: parcelsByNumberActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ImpBookTab));
