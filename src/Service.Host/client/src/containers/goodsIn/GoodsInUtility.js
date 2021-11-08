import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import validatePurchaseOrderActions from '../../actions/validatePurchaseOrderActions';
import GoodsInUtility from '../../components/goodsIn/GoodsInUtility';
import validatePurchaseOrderResultSelectors from '../../selectors/validatePurchaseOrderResultSelectors';
import storagePlacesSelectors from '../../selectors/storagePlacesSelectors';
import storagePlacesActions from '../../actions/storagePlacesActions';
import doBookInSelectors from '../../selectors/doBookInSelectors';
import doBookInActions from '../../actions/doBookInActions';
import validatePurchaseOrderBookInQtyResultActions from '../../actions/validatePurchaseOrderBookInQtyResultActions';
import validatePurchaseOrderBookInQtyResultSelectors from '../../selectors/validatePurchaseOrderBookInQtyResultSelectors';
import validateStorageTypeActions from '../../actions/validateStorageTypeActions';
import validateStorageTypeResultSelectors from '../../selectors/validateStorageTypeResultSelectors';
import { getUserNumber } from '../../selectors/userSelectors';
import loanDetailsActions from '../../actions/loanDetailsActions';
import loanDetailsSelectors from '../../selectors/loanDetailsSelectors';
import rsnAccessoriesActions from '../../actions/rsnAccessoriesActions';
import rsnAccessoriesSelectors from '../../selectors/rsnAccessoriesSelectors';
import rsnConditionsActions from '../../actions/rsnConditionsActions';
import rsnConditionsSelectors from '../../selectors/rsnConditionsSelectors';
import ValidateRsnActions from '../../actions/ValidateRsnActions';
import validateRsnResultSelectors from '../../selectors/validateRsnResultSelectors';

const mapStateToProps = (state, { match }) => ({
    validatePurchaseOrderResult: validatePurchaseOrderResultSelectors.getItem(state),
    validatePurchaseOrderResultLoading: validatePurchaseOrderResultSelectors.getLoading(state),
    storagePlacesSearchResults: storagePlacesSelectors
        .getSearchItems(state)
        .map(i => ({ ...i, id: i.name })),
    storagePlacesSearchLoading: storagePlacesSelectors.getSearchLoading(state),
    bookInResult: doBookInSelectors.getData(state),
    bookInResultLoading: doBookInSelectors.getWorking(state),
    validatePurchaseOrderBookInQtyResult: validatePurchaseOrderBookInQtyResultSelectors.getItem(
        state
    ),
    validatePurchaseOrderBookInQtyResultLoading: validatePurchaseOrderBookInQtyResultSelectors.getLoading(
        state
    ),
    userNumber: getUserNumber(state),
    validateStorageTypeResult: validateStorageTypeResultSelectors.getItem(state),
    validateStorageTypeResultLoading: validateStorageTypeResultSelectors.getLoading(state),
    match,
    loanDetails: loanDetailsSelectors.getItems(state),
    loanDetailsLoading: loanDetailsSelectors.getLoading(state),
    rsnConditions: rsnConditionsSelectors.getItems(state),
    rsnAccessories: rsnAccessoriesSelectors.getItems(state),
    rsnConditionsLoading: rsnConditionsSelectors.getLoading(state),
    rsnAccessoriesLoading: rsnAccessoriesSelectors.getLoading(state),
    validateRsnResult: validateRsnResultSelectors.getItem(state),
    validateRsnResultLoading: validateRsnResultSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(rsnAccessoriesActions.clearItems());
    dispatch(rsnConditionsActions.clearItems());
    dispatch(doBookInActions.clearProcessData());
    dispatch(loanDetailsActions.clearItem());
    dispatch(ValidateRsnActions.clearItems());
};

const mapDispatchToProps = {
    initialise,
    validatePurchaseOrder: validatePurchaseOrderActions.fetchById,
    searchStoragePlaces: storagePlacesActions.search,
    doBookIn: doBookInActions.requestProcessStart,
    validatePurchaseOrderBookInQty: validatePurchaseOrderBookInQtyResultActions.fetchByQueryString,
    validateStorageType: validateStorageTypeActions.fetchByQueryString,
    getLoanDetails: loanDetailsActions.fetchByQueryString,
    getRsnConditions: rsnConditionsActions.fetch,
    getRsnAccessories: rsnAccessoriesActions.fetch,
    validateRsn: ValidateRsnActions.fetchById
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(GoodsInUtility));
