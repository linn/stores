import React, { useEffect, useState, useReducer } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    SearchInputField,
    LinkButton,
    useSearch,
    PaginatedTable,
    Loading,
    Dropdown
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import queryString from 'query-string';
import Page from '../../containers/Page';

function ParcelsSearch({
    items,
    fetchItems,
    loading,
    suppliers,
    carriers,
    applicationState,
    history,
    privileges
}) {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });

    const [rowsToDisplay, setRowsToDisplay] = useState([]);

    const allowedToCreate = () => {
        if (!(privileges.length < 1)) {
            return privileges.some(priv => priv === 'parcel.admin');
        }
        return false;
    };

    function parcelReducer(state, action) {
        switch (action.type) {
            case 'updateSearchTerms': {
                const newSearchTerms = {
                    ...state.searchTerms,
                    [action.searchTermName]: action.newValue
                };
                return {
                    ...state,
                    searchTerms: newSearchTerms,
                    stringifiedSearch: `&${queryString.stringify(newSearchTerms, '&', '=')}`
                };
            }
            default:
                return state;
        }
    }

    const [state, dispatch] = useReducer(parcelReducer, {
        stringifiedSearch: '',
        searchTerms: {
            parcelNumberSearchTerm: '',
            supplierIdSearchTerm: '',
            carrierIdSearchTerm: '',
            dateCreatedSearchTerm: '',
            supplierInvNoSearchTerm: '',
            consignmentNoSearchTerm: '',
            commentsSearchTerm: ''
        }
    });

    const handleSearchTermChange = (propertyName, newValue) => {
        dispatch({ type: 'updateSearchTerms', searchTermName: propertyName, newValue });
    };

    useSearch(fetchItems, state.stringifiedSearch, null, 'searchTerm');

    const handleRowLinkClick = href => history.push(href);

    useEffect(() => {
        const compare = (field, orderAscending) => (a, b) => {
            if (!field) {
                return 0;
            }

            if (a[field] < b[field]) {
                return orderAscending ? -1 : 1;
            }

            if (a[field] > b[field]) {
                return orderAscending ? 1 : -1;
            }

            return 0;
        };
        const rows = items
            ? items.map(el => ({
                  parcelNumber: el.parcelNumber,
                  supplier: `${el.supplierId} - ${
                      suppliers.find(x => x.id === el.supplierId)?.name
                  }`,
                  carrier: `${el.carrierId} - ${carriers.find(x => x.id === el.carrierId)?.name}`,
                  dateCreated: el.dateCreated,
                  supplerInvNo: el.supplerInvNo,
                  consignmentNo: el.consignmentNo,
                  comments: el.comments,
                  id: el.parcelNumber,
                  links: el.links
              }))
            : [];

        if (!rows || rows.length === 0) {
            setRowsToDisplay([]);
        } else {
            setRowsToDisplay(
                rows
                    .sort(compare(pageOptions.orderBy, pageOptions.orderAscending))
                    .slice(
                        pageOptions.currentPage * pageOptions.rowsPerPage,
                        pageOptions.currentPage * pageOptions.rowsPerPage + pageOptions.rowsPerPage
                    )
            );
        }
    }, [
        pageOptions.currentPage,
        pageOptions.rowsPerPage,
        pageOptions.orderBy,
        pageOptions.orderAscending,
        items,
        carriers,
        suppliers,
        applicationState
    ]);

    const columns = {
        parcelNumber: 'parcel number',
        supplier: 'supplier',
        carrier: 'carrier',
        dateCreated: 'date created',
        supplierInvNo: 'supplier invoice number',
        consignmentNo: 'consignment number',
        comments: 'comments'
    };

    return (
        <Page>
            <Grid spacing={3} container justify="center">
                <Grid item xs={11} />
                <Grid item xs={1}>
                    {/* todo: add if allowedToCreate properly, still needs pulled in from backend */}
                    {(allowedToCreate || true) && (
                        <LinkButton text="Create" to="/inventory/parcels/create" />
                    )}
                </Grid>
                <Grid item xs={1}>
                    <SearchInputField
                        label="Parcels"
                        fullWidth
                        placeholder="search.."
                        onChange={handleSearchTermChange}
                        propertyName="parcelNumberSearchTerm"
                        type="text"
                        value={state.searchTerms.parcelNumberSearchTerm}
                    />
                </Grid>

                <Grid item xs={2}>
                    <Dropdown
                        onChange={handleSearchTermChange}
                        items={suppliers.map(s => ({
                            ...s,
                            id: s.id,
                            displayText: `${s.name} ( ${s.id} ), ${s.countryCode}`
                        }))}
                        value={state.searchTerms.supplierIdSearchTerm}
                        propertyName="supplierIdSearchTerm"
                        required
                        fullWidth
                        label="Supplier"
                        allowNoValue
                    />
                </Grid>

                <Grid item xs={2}>
                    <Dropdown
                        onChange={handleSearchTermChange}
                        items={carriers.map(s => ({
                            ...s,
                            id: s.id,
                            displayText: `${s.name} ( ${s.id} )`
                        }))}
                        value={state.searchTerms.carrierIdSearchTerm}
                        propertyName="carrierIdSearchTerm"
                        required
                        fullWidth
                        label="Carrier"
                        allowNoValue
                    />
                </Grid>

                <Grid item xs={2}>
                    <SearchInputField
                        label="Date Created"
                        fullWidth
                        placeholder="search.."
                        onChange={handleSearchTermChange}
                        propertyName="dateCreatedSearchTerm"
                        type="date"
                        value={state.searchTerms.dateCreatedSearchTerm}
                    />
                </Grid>

                <Grid item xs={1}>
                    <SearchInputField
                        label="Supplier Inv No"
                        fullWidth
                        onChange={handleSearchTermChange}
                        propertyName="supplierInvNoSearchTerm"
                        type="text"
                        value={state.searchTerms.supplierInvNoSearchTerm}
                    />
                </Grid>

                <Grid item xs={2}>
                    <SearchInputField
                        label="Consignment Number"
                        fullWidth
                        onChange={handleSearchTermChange}
                        propertyName="consignmentNoSearchTerm"
                        type="text"
                        value={state.searchTerms.consignmentNoSearchTerm}
                    />
                </Grid>

                <Grid item xs={2}>
                    <SearchInputField
                        label="Comments"
                        fullWidth
                        onChange={handleSearchTermChange}
                        propertyName="commentsSearchTerm"
                        type="text"
                        value={state.searchTerms.commentsSearchTerm}
                    />
                </Grid>

                <Grid item xs={12}>
                    {loading ? (
                        <Loading />
                    ) : (
                        <>
                            {rowsToDisplay.length > 0 && (
                                <PaginatedTable
                                    columns={columns}
                                    handleRowLinkClick={handleRowLinkClick}
                                    rows={rowsToDisplay}
                                    pageOptions={pageOptions}
                                    setPageOptions={setPageOptions}
                                    totalItemCount={items ? items.length : 0}
                                    expandable={false}
                                />
                            )}
                        </>
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

ParcelsSearch.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            parcelNumber: PropTypes.number
        })
    ).isRequired,
    carriers: PropTypes.arrayOf(
        PropTypes.shape({
            carrierCode: PropTypes.string,
            organisationId: PropTypes.number
        })
    ),
    suppliers: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string
        })
    ),
    applicationState: PropTypes.shape().isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string)
};

ParcelsSearch.defaultProps = {
    loading: false,
    carriers: [{ carrierCode: 'loading..', organisationId: -1 }],
    suppliers: [{ id: -1, name: 'loading..' }],
    privileges: null
};

export default ParcelsSearch;
