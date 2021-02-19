import React, { useEffect, useState, useReducer } from 'react';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Tooltip from '@material-ui/core/Tooltip';
import {
    SearchInputField,
    LinkButton,
    useSearch,
    PaginatedTable,
    Loading,
    TypeaheadDialog,
    InputField
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import queryString from 'query-string';
import Page from '../../containers/Page';

function ParcelsSearch({
    items,
    fetchItems,
    loading,
    suppliers,
    suppliersSearchResults,
    suppliersSearchLoading,
    searchSuppliers,
    clearSuppliersSearch,
    carriersSearchResults,
    carriersSearchLoading,
    searchCarriers,
    clearCarriersSearch,
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

    const useStyles = makeStyles(theme => ({
        marginTop2: {
            marginTop: theme.spacing(2)
        },
        marginTop3: {
            marginTop: theme.spacing(3)
        },
        displayInline: {
            display: 'inline-block'
        }
    }));
    const classes = useStyles();

    function parcelReducer(state, action) {
        switch (action.type) {
            case 'updateSearchTerms': {
                const newSearchTerms = {
                    ...state.searchTerms,
                    [action.searchTermName]: action.newValue
                };
                if (action.carrierDisplayName) {
                    return {
                        ...state,
                        searchTerms: newSearchTerms,
                        stringifiedSearch: `&${queryString.stringify(newSearchTerms, '&', '=')}`,
                        carrierDisplayName: action.carrierDisplayName
                    };
                }
                if (action.supplierDisplayName) {
                    return {
                        ...state,
                        searchTerms: newSearchTerms,
                        stringifiedSearch: `&${queryString.stringify(newSearchTerms, '&', '=')}`,
                        supplierDisplayName: action.supplierDisplayName
                    };
                }
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
        },
        supplierDisplayName: '-',
        carrierDisplayName: '-'
    });

    const handleSearchTermChange = (propertyName, newValue) => {
        dispatch({ type: 'updateSearchTerms', searchTermName: propertyName, newValue });
    };

    const handleSupplierChange = supplier => {
        dispatch({
            type: 'updateSearchTerms',
            searchTermName: 'supplierIdSearchTerm',
            newValue: supplier.id,
            supplierDisplayName: `${supplier.id} - ${supplier.description}`
        });
    };

    const handleCarrierChange = carrier => {
        dispatch({
            type: 'updateSearchTerms',
            searchTermName: 'carrierIdSearchTerm',
            newValue: carrier.id,
            carrierDisplayName: `${carrier.id} - ${carrier.description}`
        });
    };

    const resetSuppliersSearchTerms = () => {
        dispatch({
            type: 'updateSearchTerms',
            searchTermName: 'supplierIdSearchTerm',
            newValue: '',
            supplierDisplayName: '-'
        });
    };

    const resetCarriersSearchTerms = () => {
        dispatch({
            type: 'updateSearchTerms',
            searchTermName: 'carrierIdSearchTerm',
            newValue: '',
            carrierDisplayName: '-'
        });
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
                  carrier: `${el.carrierId} - ${suppliers.find(x => x.id === el.carrierId)?.name}`,
                  dateCreated: el.dateCreated,
                  supplierInvoiceNo: el.supplierInvoiceNo,
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
        suppliers,
        applicationState
    ]);

    const columns = {
        parcelNumber: 'parcel number',
        supplier: 'supplier',
        carrier: 'carrier',
        dateCreated: 'date created',
        supplierInvoiceNo: 'supplier invoice number',
        consignmentNo: 'consignment number',
        comments: 'comments'
    };

    return (
        <Page>
            <Grid spacing={3} container justify="center">
                <Grid item xs={11} />
                <Grid item xs={1}>
                    {(allowedToCreate || true) && (
                        <LinkButton text="Create" to="/inventory/parcels/create" />
                    )}
                </Grid>
                <Grid item xs={2}>
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
                <Grid item xs={1} />

                <Grid item xs={1}>
                    <div className={classes.marginTop2}>
                        <TypeaheadDialog
                            title="Search for a supplier"
                            onSelect={handleSupplierChange}
                            searchItems={suppliersSearchResults}
                            loading={suppliersSearchLoading}
                            fetchItems={searchSuppliers}
                            clearSearch={() => clearSuppliersSearch}
                        />
                    </div>
                </Grid>
                <Grid item xs={2}>
                    <div className={classes.displayInline}>
                        <InputField
                            value={state.supplierDisplayName}
                            label="Supplier"
                            propertyName="supplier"
                            disabled
                        />
                    </div>
                </Grid>
                <Grid item xs={1}>
                    <div className={classes.marginTop3}>
                        <Tooltip title="Clear Supplier search">
                            <Button variant="outlined" onClick={resetSuppliersSearchTerms}>
                                X
                            </Button>
                        </Tooltip>
                    </div>
                </Grid>
                <Grid item xs={1} />

                <Grid item xs={1}>
                    <div className={classes.marginTop2}>
                        <TypeaheadDialog
                            title="Search for a Carrier"
                            onSelect={handleCarrierChange}
                            searchItems={carriersSearchResults}
                            loading={carriersSearchLoading}
                            fetchItems={searchCarriers}
                            clearSearch={() => clearCarriersSearch}
                        />
                    </div>
                </Grid>
                <Grid item xs={2}>
                    <div className={classes.displayInline}>
                        <InputField
                            value={state.carrierDisplayName}
                            label="Carrier"
                            propertyName="carrier"
                            disabled
                        />
                    </div>
                </Grid>
                <Grid item xs={1}>
                    <div className={classes.marginTop3}>
                        <Tooltip title="Clear Carrier search">
                            <Button variant="outlined" onClick={resetCarriersSearchTerms}>
                                X
                            </Button>
                        </Tooltip>
                    </div>
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

                <Grid item xs={3}>
                    <SearchInputField
                        label="Supplier Inv No"
                        fullWidth
                        onChange={handleSearchTermChange}
                        propertyName="supplierInvNoSearchTerm"
                        type="text"
                        value={state.searchTerms.supplierInvNoSearchTerm}
                    />
                </Grid>

                <Grid item xs={3}>
                    <SearchInputField
                        label="Consignment Number"
                        fullWidth
                        onChange={handleSearchTermChange}
                        propertyName="consignmentNoSearchTerm"
                        type="text"
                        value={state.searchTerms.consignmentNoSearchTerm}
                    />
                </Grid>

                <Grid item xs={4}>
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
    privileges: PropTypes.arrayOf(PropTypes.string),
    suppliersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.number,
            description: PropTypes.string
        })
    ),
    suppliersSearchLoading: PropTypes.bool,
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired,
    carriersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.number,
            description: PropTypes.string
        })
    ),
    carriersSearchLoading: PropTypes.bool,
    searchCarriers: PropTypes.func.isRequired,
    clearCarriersSearch: PropTypes.func.isRequired
};

ParcelsSearch.defaultProps = {
    loading: false,
    carriersSearchResults: [{ id: -1, name: '', description: '' }],
    suppliersSearchResults: [{ id: -1, name: '', description: '' }],
    suppliers: [{ id: -1, name: 'loading..' }],
    privileges: null,
    carriersSearchLoading: false,
    suppliersSearchLoading: false
};

export default ParcelsSearch;
