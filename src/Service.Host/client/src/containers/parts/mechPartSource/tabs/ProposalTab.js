import { connect } from 'react-redux';
import ProposalTab from '../../../../components/parts/mechPartSource/tabs/ProposalTab';
import partsSelectors from '../../../../selectors/partsSelectors';
import partsActions from '../../../../actions/partsActions';
import departmentsActions from '../../../../actions/departmentsActions';
import departmentsSelectors from '../../../../selectors/departmentsSelectors';

const mapStateToProps = state => ({
    loading: partsSelectors.getLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.partNumber, description: c.description })),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    departmentsSearchResults: departmentsSelectors
        .getSearchItems(state)
        .filter(a => a.projectDepartment === 'Y')
        .map(c => ({ name: c.departmentCode, description: c.description })),
    departmentsSearchLoading: departmentsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    searchDepartments: departmentsActions.search,
    clearSearchDepartments: departmentsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(ProposalTab);
