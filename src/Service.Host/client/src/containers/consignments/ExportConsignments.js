import { connect } from 'react-redux';
import { initialiseOnMount, getRequestErrors } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import ExportConsignments from '../../components/consignments/ExportConsignments';
import consignmentsSelectors from '../../selectors/consignmentsSelectors';
import consignmentsActions from '../../actions/consignmentsActions';
import consignmentActions from '../../actions/consignmentActions';
import hubsSelectors from '../../selectors/hubsSelectors';
import hubsActions from '../../actions/hubsActions';

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options || {};
};

const mapStateToProps = (state, ownProps) => ({
    hubs: hubsSelectors.getItems(state),
    hubsLoading: hubsSelectors.getLoading(state),
    options: getOptions(ownProps),
    requestErrors: getRequestErrors(state)?.filter(error => error.type !== 'FETCH_ERROR'),
    loading: consignmentsSelectors.getSearchLoading(state),
    consignments: consignmentsSelectors.getSearchItems(state)
});

const initialise = ({ options }) => dispatch => {
    dispatch(hubsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchConsignments: consignmentsActions.searchWithOptions,
    updateConsignment: consignmentActions.update
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ExportConsignments));
