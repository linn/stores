import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    SearchInputField,
    LinkButton,
    useSearch,
    PaginatedTable,
    Loading,
    utilities,
    Dropdown
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function ParcelsSearch({
    items,
    fetchItems,
    loading,
    suppliers,
    carriers,
    applicationState,
    history
}) {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });

    const [rowsToDisplay, setRowsToDisplay] = useState([]);
    const [allowedToCreate, setAllowedToCreate] = useState(false);
    // const [snackbarVisible, setSnackbarVisible] = useState(editStatus === 'deleted');

    const [searchTerm, setSearchTerm] = useState(null);
    const [parcelNumberSearch, setParcelNumberSearch] = useState('');
    const [supplierIdSearch, setSupplierIdSearch] = useState('');
    const [carrierIdSearch, setCarrierIdSearch] = useState('');
    const [dateCreatedSearch, setDateCreatedSearch] = useState('');
    const [supplierInvNoSearch, setSupplierInvNoSearch] = useState('');
    const [consignmentNoSearch, setConsignmentNoSearch] = useState('');
    const [commentsSearch, setCommentsSearch] = useState('');

    useSearch(fetchItems, searchTerm, null, 'searchTerm');

    const handleRowLinkClick = href => history.push(href);

    const handleParcelNumberSearchChange = (...args) => {
        setParcelNumberSearch(args[1]);
        setSearchTerm(
            `${args[1]}&supplierIdSearchTerm=${supplierIdSearch}&carrierIdSearchTerm=${carrierIdSearch}&dateCreatedSearchTerm=${dateCreatedSearch}&supplierInvNoSearchTerm=${supplierInvNoSearch}&consignmentNoSearchTerm=${consignmentNoSearch}&commentsSearchTerm=${commentsSearch}`
        );
    };

    const handleSupplierSearchChange = (...args) => {
        setSupplierIdSearch(args[1]);
        setSearchTerm(
            `${parcelNumberSearch}&supplierIdSearchTerm=${args[1]}&carrierIdSearchTerm=${carrierIdSearch}&dateCreatedSearchTerm=${dateCreatedSearch}&supplierInvNoSearchTerm=${supplierInvNoSearch}&consignmentNoSearchTerm=${consignmentNoSearch}&commentsSearchTerm=${commentsSearch}`
        );
    };

    const handleCarrierSearchChange = (...args) => {
        setCarrierIdSearch(args[1]);
        setSearchTerm(
            `${parcelNumberSearch}&supplierIdSearchTerm=${supplierIdSearch}&carrierIdSearchTerm=${args[1]}&dateCreatedSearchTerm=${dateCreatedSearch}&supplierInvNoSearchTerm=${supplierInvNoSearch}&consignmentNoSearchTerm=${consignmentNoSearch}&commentsSearchTerm=${commentsSearch}`
        );
    };

    const handleDateSearchChange = (...args) => {
        setDateCreatedSearch(args[1]);
        setSearchTerm(
            `${parcelNumberSearch}&supplierIdSearchTerm=${supplierIdSearch}&carrierIdSearchTerm=${carrierIdSearch}&dateCreatedSearchTerm=${args[1]}&supplierInvNoSearchTerm=${supplierInvNoSearch}&consignmentNoSearchTerm=${consignmentNoSearch}&commentsSearchTerm=${commentsSearch}`
        );
    };

    const handleSupplierInvSearchChange = (...args) => {
        setSupplierInvNoSearch(args[1]);
        setSearchTerm(
            `${parcelNumberSearch}&supplierIdSearchTerm=${supplierIdSearch}&carrierIdSearchTerm=${carrierIdSearch}&dateCreatedSearchTerm=${dateCreatedSearch}&supplierInvNoSearchTerm=${args[1]}&consignmentNoSearchTerm=${consignmentNoSearch}&commentsSearchTerm=${commentsSearch}`
        );
    };

    const handleConsignmentNoSearchChange = (...args) => {
        setConsignmentNoSearch(args[1]);
        setSearchTerm(
            `${parcelNumberSearch}&supplierIdSearchTerm=${supplierIdSearch}&carrierIdSearchTerm=${carrierIdSearch}&dateCreatedSearchTerm=${dateCreatedSearch}&supplierInvNoSearchTerm=${supplierInvNoSearch}&consignmentNoSearchTerm=${args[1]}&commentsSearchTerm=${commentsSearch}`
        );
    };

    const handleCommentSearchChange = (...args) => {
        setCommentsSearch(args[1]);
        setSearchTerm(
            `${parcelNumberSearch}&supplierIdSearchTerm=${supplierIdSearch}&carrierIdSearchTerm=${carrierIdSearch}&dateCreatedSearchTerm=${dateCreatedSearch}&supplierInvNoSearchTerm=${supplierInvNoSearch}&consignmentNoSearchTerm=${consignmentNoSearch}&commentsSearchTerm=${args[1]}`
        );
    };

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
                  carrier: `${el.carrierId} - ${
                      carriers.find(x => x.organisationId === el.carrierId)?.carrierCode
                  }`,
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

        setAllowedToCreate(utilities.getHref(applicationState, 'edit') !== null);
    }, [
        pageOptions.currentPage,
        pageOptions.rowsPerPage,
        pageOptions.orderBy,
        pageOptions.orderAscending,
        items,
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

    // setAllowedToCreate(utilities.getHref(applicationState, 'edit') !== null);

    return (
        <Page>
            <Grid spacing={3} container justify="center">
                <Grid item xs={11} />
                <Grid item xs={1}>
                    <LinkButton text="Create" to="/inventory/parcels/create" />
                </Grid>
                <Grid item xs={1}>
                    <SearchInputField
                        label="Parcels"
                        fullWidth
                        placeholder="search.."
                        onChange={handleParcelNumberSearchChange}
                        propertyName="parcelSearchTerm"
                        type="text"
                        value={parcelNumberSearch}
                        debounce={5000}
                    />
                </Grid>

                <Grid item xs={2}>
                    <Dropdown
                        onChange={handleSupplierSearchChange}
                        items={suppliers
                            .map(s => ({
                                ...s,
                                id: s.id,
                                displayText: `${s.name} ( ${s.id} )`
                            }))
                            .sort((a, b) => (a.id > b.id ? 1 : -1))}
                        value={supplierIdSearch}
                        propertyName="supplierIdSearch"
                        required
                        fullWidth
                        label="Supplier"
                        allowNoValue
                    />
                </Grid>

                <Grid item xs={2}>
                    <Dropdown
                        onChange={handleCarrierSearchChange}
                        items={carriers
                            .map(s => ({
                                ...s,
                                id: s.carrierCode,
                                displayText: `${s.carrierCode} ( ${s.organisationId} )`
                            }))
                            .sort((a, b) => (a.id > b.id ? 1 : -1))}
                        value={carrierIdSearch}
                        propertyName="carrierIdSearch"
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
                        onChange={handleDateSearchChange}
                        propertyName="dateCreatedSearchTerm"
                        type="date"
                        value={dateCreatedSearch}
                        debounce={5000}
                    />
                </Grid>

                <Grid item xs={1}>
                    <SearchInputField
                        label="Supplier Inv No"
                        fullWidth
                        onChange={handleSupplierInvSearchChange}
                        propertyName="supplierInvNoSearchTerm"
                        type="text"
                        value={supplierInvNoSearch}
                        debounce={5000}
                    />
                </Grid>

                <Grid item xs={2}>
                    <SearchInputField
                        label="Consignment Number"
                        fullWidth
                        onChange={handleConsignmentNoSearchChange}
                        propertyName="consingmentNoSearchTerm"
                        type="text"
                        value={consignmentNoSearch}
                        debounce={5000}
                    />
                </Grid>

                <Grid item xs={2}>
                    <SearchInputField
                        label="Comments"
                        fullWidth
                        onChange={handleCommentSearchChange}
                        propertyName="commentSearchTerm"
                        type="text"
                        value={commentsSearch}
                        debounce={5000}
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
            parcelNumber: PropTypes.int
        })
    ).isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired
};

ParcelsSearch.defaultProps = {
    loading: false
};

export default ParcelsSearch;
