import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import rootProductsActions from '../../../../actions/rootProductsActions';
import rootProductsSelectors from '../../../../selectors/rootProductsSelectors';
import UsagesTab from '../../../../components/parts/mechPartSource/tabs/UsagesTab';

const mapStateToProps = state => ({
    rootProductsSearchResults: rootProductsSelectors.getSearchItems(state),
    rootProductsSearchLoading: rootProductsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchRootProducts: rootProductsActions.search,
    clearRootProductsSearch: rootProductsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(UsagesTab));
