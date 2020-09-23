import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import partActions from '../../actions/partActions';
import CreatePart from '../../components/parts/CreatePart';
import partSelectors from '../../selectors/partSelectors';
import accountingCompaniesActions from '../../actions/accountingCompaniesActions';
import accountingCompaniesSelectors from '../../selectors/accountingCompaniesSelectors';
import { getPrivileges, getUserName, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import suppliersActions from '../../actions/suppliersActions';
import suppliersSelectors from '../../selectors/suppliersSelectors';

const mapStateToProps = state => ({
    item: {
        partNumber: '',
        description: '',
        accountingCompany: 'LINN',
        psuPart: 'No',
        stockControlled: 'Yes',
        cccCriticalPart: 'No',
        safetyCriticalPart: 'No',
        paretoCode: 'U',
        createdBy: getUserNumber(state),
        dateCreated: new Date(),
        railMethod: 'POLICY',
        preferredSupplier: 4415,
        preferredSupplierName: 'Linn Products Ltd',
        qcInformation: ''
    },
    snackbarVisible: partSelectors.getSnackbarVisible(state),
    accountingCompanies: accountingCompaniesSelectors.getItems(state),
    privileges: getPrivileges(state),
    userName: getUserName(state),
    itemError: getItemError(state, itemTypes.part.item),
    loading: partSelectors.getLoading(state),
    searchItems: partsSelectors.getSearchItems(state),
    searchLoading: partsSelectors.getSearchLoading(state),
    suppliersSearchResults: suppliersSelectors
        .getSearchItems(state)
        .map(c => ({ name: c.id, description: c.name })),
    suppliersSearchLoading: suppliersSelectors.getSearchLoading(state)
});

const initialise = () => dispatch => {
    dispatch(accountingCompaniesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: partActions.add,
    setEditStatus: partActions.setEditStatus,
    setSnackbarVisible: partActions.setSnackbarVisible,
    fetchItems: partsActions.search,
    clearSearch: partsActions.clearSearch,
    searchSuppliers: suppliersActions.search,
    clearSuppliersSearch: suppliersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(CreatePart));
