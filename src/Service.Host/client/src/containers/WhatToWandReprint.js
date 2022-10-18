import { connect } from 'react-redux';
import { getItemError } from '@linn-it/linn-form-components-library';
import whatToWandReprintActions from '../actions/whatToWandReprintActions';
import whatToWandReprintSelectors from '../selectors/whatToWandReprintSelectors';
import * as itemTypes from '../itemTypes';
import WhatToWandReprint from '../components/tpk/WhatToWandReprint';

const mapStateToProps = state => ({
    whatToWandReport: whatToWandReprintSelectors.getItem(state),
    loading: whatToWandReprintSelectors.getLoading(state),
    error: getItemError(state, itemTypes.whatToWandReprint.item)
});

const mapDispatchToProps = {
    fetch: whatToWandReprintActions.fetch,
    clear: whatToWandReprintActions.clearItem,
    clearErrors: whatToWandReprintActions.clearErrorsForItem
};

export default connect(mapStateToProps, mapDispatchToProps)(WhatToWandReprint);
