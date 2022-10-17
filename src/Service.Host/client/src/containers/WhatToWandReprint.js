import { connect } from 'react-redux';
import whatToWandReprintActions from '../actions/whatToWandReprintActions';
import whatToWandReprintSelectors from '../selectors/whatToWandReprintSelectors';
import WhatToWandReprint from '../components/tpk/WhatToWandReprint';

const mapStateToProps = state => ({
    whatToWandReport: whatToWandReprintSelectors.getItem(state),
    loading: whatToWandReprintSelectors.getLoading(state)
});

const mapDispatchToProps = {
    fetch: whatToWandReprintActions.fetch,
    clear: whatToWandReprintActions.clearItem
};

export default connect(mapStateToProps, mapDispatchToProps)(WhatToWandReprint);
