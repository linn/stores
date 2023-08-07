import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import BahnofKoenig from '../components/BahnhofKoenig';
import warehouseTaskSelectors from '../selectors/warehouseTaskSelectors';
import warehousePalletSelectors from '../selectors/warehousePalletSelectors';
import warehouseTaskActions from '../actions/warehouseTaskActions';
import warehousePalletActions from '../actions/warehousePalletActions';
import * as itemTypes from '../itemTypes';

const creating = match =>
    match?.url?.endsWith('/create') || match?.url?.includes('/goods-in-utility');

const mapStateToProps = (state, { match }) => ({
    item: creating(match) ? null : warehouseTaskSelectors.getItem(state),
    itemId: creating(match) ? null : match.params.id,
    palletLoc: warehousePalletSelectors.getItem(state),
    editStatus: 'create',
    loading: warehouseTaskSelectors.getLoading(state),
    snackbarVisible: warehouseTaskSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.warehouseTask.item)
});

const initialise = item => dispatch => {
    if (item.itemId) {
        dispatch(warehouseTaskActions.fetch(item.itemId));
    }
};

const mapDispatchToProps = {
    initialise,
    addItem: warehouseTaskActions.add,
    setEditStatus: warehouseTaskActions.setEditStatus,
    setSnackbarVisible: warehouseTaskActions.setSnackbarVisible,
    getPalletLoc: warehousePalletActions.fetch,
    queryPalletLoc: warehousePalletActions.fetchByQueryString,
    clearPalletLoc: warehousePalletActions.clearItem
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(BahnofKoenig));
