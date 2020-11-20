import { connect } from 'react-redux';
import ManufacturersTab from '../../../../components/parts/mechPartSource/tabs/ManufacturersTab';
import manufacturersSelectors from '../../../../selectors/manufacturersSelectors';
import manufacturersActions from '../../../../actions/manufacturersActions';

const mapStateToProps = state => ({
    loading: manufacturersSelectors.getLoading(state),
    manufacturersSearchResults: manufacturersSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.code, description: c.description })),
    manufacturersSearchLoading: manufacturersSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchManufacturers: manufacturersActions.search,
    clearManufacturersSearch: manufacturersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(ManufacturersTab);
