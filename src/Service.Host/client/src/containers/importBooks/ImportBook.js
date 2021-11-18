import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ImportBook from '../../components/importBooks/ImportBook';
import importBookActions from '../../actions/importBookActions';
import importBookSelectors from '../../selectors/importBookSelectors';
import { getPrivileges, getUserNumber } from '../../selectors/userSelectors';
import * as itemTypes from '../../itemTypes';
import suppliersActions from '../../actions/suppliersActions';
import suppliersSelectors from '../../selectors/suppliersSelectors';
import countriesActions from '../../actions/countriesActions';
import countriesSelectors from '../../selectors/countriesSelectors';
import employeesActions from '../../actions/employeesActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import exchangeRatesActions from '../../actions/exchangeRatesActions';
import exchangeRatesSelectors from '../../selectors/exchangeRatesSelectors';
import cpcNumbersActions from '../../actions/impbookCpcNumbersActions';
import cpcNumbersSelectors from '../../selectors/impbookCpcNumbersSelectors';

const creating = match => match?.url?.endsWith('/create');

const mapStateToProps = (state, { match }) => ({
    item: creating(match) ? null : importBookSelectors.getItem(state),
    itemId: creating(match) ? null : match.params.id,
    editStatus: creating(match) ? 'create' : importBookSelectors.getEditStatus(state),
    loading: importBookSelectors.getLoading(state),
    snackbarVisible: importBookSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.part.item),
    privileges: getPrivileges(state),
    userNumber: getUserNumber(state),
    allSuppliers: suppliersSelectors.getItems(state),
    countries: countriesSelectors.getItems(state),
    employees: employeesSelectors.getItems(state),
    exchangeRates: exchangeRatesSelectors.getSearchItems(state),
    cpcNumbers: cpcNumbersSelectors.getItems(state)?.map(x => ({
        displayText: `${x.cpcNumber === 13 ? `${x.cpcNumber} (IPR)` : x.cpcNumber} - ${
            x.description
        }`,
        id: parseInt(x.cpcNumber, 10)
    }))
});

const initialise = ({ itemId }) => dispatch => {
    if (itemId) {
        dispatch(importBookActions.fetch(itemId));
    }
    dispatch(suppliersActions.fetch());
    dispatch(countriesActions.fetch());
    dispatch(employeesActions.fetch());
    dispatch(cpcNumbersActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    addItem: item => importBookActions.add(item),
    updateItem: (itemId, item) => importBookActions.update(itemId, item),
    setEditStatus: status => importBookActions.setEditStatus(status),
    setSnackbarVisible: () => importBookActions.setSnackbarVisible(),
    getExchangeRatesForDate: exchangeRatesActions.search
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ImportBook));
