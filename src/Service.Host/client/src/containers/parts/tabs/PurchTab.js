import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import PurchTab from '../../../components/parts/tabs/PurchTab';
import partSelectors from '../../../selectors/partSelectors';
import unitsOfMeasureActions from '../../../actions/unitsOfMeasureActions';
import unitsOfMeasureSelectors from '../../../selectors/unitsOfMeasureSelectors';
import suppliersActions from '../../../actions/suppliersActions';
import suppliersSelectors from '../../../selectors/suppliersSelectors';

const mapStateToProps = state => ({
    editStatus: partSelectors.getEditStatus(state),
    loading: partSelectors.getLoading(state),
    unitsOfMeasure: unitsOfMeasureSelectors.getItems(state),
    suppliersSearchResults: suppliersSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.id, description: c.name })),
    suppliersSearchLoading: suppliersSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    dispatch(unitsOfMeasureActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchSuppliers: suppliersActions.search,
    clearSuppliersSearch: suppliersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PurchTab));
