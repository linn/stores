import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import OrderDetailsTab from '../../../components/importBooks/tabs/OrderDetailsTab';
import cpcNumbersActions from '../../../actions/impbookCpcNumbersActions';
import cpcNumbersSelectors from '../../../selectors/impbookCpcNumbersSelectors';
import rsnsActions from '../../../actions/rsnsActions';
import rsnsSelectors from '../../../selectors/rsnsSelectors';
import loansActions from '../../../actions/loansActions';
import loansSelectors from '../../../selectors/loansSelectors';
import purchaseOrdersActions from '../../../actions/purchaseOrdersActions';
import purchaseOrdersSelectors from '../../../selectors/purchaseOrdersSelectors';

const mapStateToProps = state => ({
    cpcNumbers: cpcNumbersSelectors.getItems(state)?.map(x => ({
        displayText: `${x.cpcNumber === 13 ? `${x.cpcNumber} (IPR)` : x.cpcNumber} - ${
            x.description
        }`,
        id: parseInt(x.cpcNumber, 10)
    })),
    rsnsSearchResults: rsnsSelectors.getSearchItems(state).map?.(r => ({
        id: r.rsnNumber,
        name: r.rsnNumber.toString(),
        description: r.invoiceDescription,
        quantity: r.quantity,
        tariffCode: r.tariffCode,
        weight: r.weight
    })),
    rsnsSearchLoading: rsnsSelectors.getSearchLoading(state),
    purchaseOrdersSearchResults: purchaseOrdersSelectors.getSearchItems(state).map?.(p => ({
        id: p.orderNumber,
        name: p.orderNumber.toString(),
        description: p.suppliersDesignation,
        supplierId: p.supplierId,
        tariffCode: p.tariffCode,
        lineNumber: p.lineNumber
    })),
    purchaseOrdersSearchLoading: purchaseOrdersSelectors.getSearchLoading(state),
    loansSearchResults: loansSelectors.getSearchItems(state).map?.(l => ({
        id: l.loanNumber,
        name: l.loanNumber.toString(),
        description: l.loanNumber
    })),
    loansSearchLoading: loansSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    dispatch(cpcNumbersActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchRsns: rsnsActions.search,
    clearRsnsSearch: rsnsActions.clearSearch,
    searchLoans: loansActions.search,
    clearLoansSearch: loansActions.clearSearch,
    searchPurchaseOrders: purchaseOrdersActions.search,
    clearPurchaseOrdersSearch: purchaseOrdersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(OrderDetailsTab));
