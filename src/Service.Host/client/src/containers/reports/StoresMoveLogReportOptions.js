import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import StoresMoveLogReportOptions from '../../components/reports/StoresMoveLogReportOptions';

const mapStateToProps = state => ({
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber }))
});

const initialise = () => dispatch => {
    dispatch(partsActions.clearSearch());
};

const mapDispatchToProps = {
    initialise,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withRouter(initialiseOnMount(StoresMoveLogReportOptions)));
