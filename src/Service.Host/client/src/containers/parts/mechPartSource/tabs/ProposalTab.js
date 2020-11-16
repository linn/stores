import { connect } from 'react-redux';
import ProposalTab from '../../../../components/parts/mechPartSource/tabs/ProposalTab';
import partsSelectors from '../../../../selectors/partsSelectors';
import partsActions from '../../../../actions/partsActions';

const mapStateToProps = state => ({
    loading: partsSelectors.getLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.partNumber, description: c.description })),
    partsSearchLoading: partsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(ProposalTab);
