import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    SearchInputField,
    LinkButton,
    useSearch,
    PaginatedTable,
    Loading,
    utilities
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function ParcelsSearch({ items, fetchItems, loading, clearSearch, applicationState, history }) {
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
    const [dateCreatedSearch, setDateCreatedSearch] = useState(null);
    const [supplerInvNoSearch, setSupplerInvNoSearch] = useState('');
    const [consignmentNoSearch, setConsignmentNoSearch] = useState('');
    const [commentsSearch, setcommentsSearch] = useState('');

    useSearch(fetchItems, searchTerm, null, 'searchTerm');

    const handleRowLinkClick = href => history.push(href);


    const handleParcelNumberSearchChange = (...args) => {
        setParcelNumberSearch(args[1]);
        setSearchTerm(
            `${args[1]}&supplierIdSearchTerm=${supplierIdSearch}&carrierIdSearchTerm=${carrierIdSearch}
            &dateCreatedSearchTerm=${dateCreatedSearch}&supplerInvNoSearchTerm=${supplerInvNoSearch}
            &consignmentNoSearchTerm=${consignmentNoSearch}&commentsSearchTerm=${commentsSearch}`
        );
    };

    // const handleSupplierIdSearchChange = (...args) => {
    //     setSupplierIdSearch(args[1]);
    //     setSearchTerm(
    //         `${parcelNumberSearch}&supplierIdSearchTerm=${args[1]}&carrierIdSearchTerm=${carrierIdSearch}
    //         &dateCreatedSearchTerm=${dateCreatedSearch}&supplerInvNoSearchTerm=${supplerInvNoSearch}
    //         &consignmentNoSearchTerm=${consignmentNoSearch}&commentsSearchTerm=${commentsSearch}`
    //     );
    // };

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
                  supplier: `${el.supplierId} - ${el.supplierName}`,
                  carrier: `${el.carrierId} - ${el.carrierName}`,
                  dateCreated: el.dateCreated,
                  comments: el.comments,
                  supplerInvNo: el.supplerInvNo,
                  consignmentNo: el.consignmentNo,
                  id: el.parcelNumber
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

        // setAllowedToCreate(utilities.getHref(applicationState, 'edit') !== null);
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
        comments: 'comments',
        supplerInvNo: 'suppler invoice number',
        consignmentNo: 'consignment number'
    };

    setAllowedToCreate(utilities.getHref(applicationState, 'edit') !== null);

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={7} />
                <Grid item xs={1}>
                    <LinkButton text="Create" to="/inventory/parcels/create" />
                </Grid>
                <Grid item xs={1} />
                <Grid item xs={12}>
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
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired
};

ParcelsSearch.defaultProps = {
    loading: false
};

export default ParcelsSearch;
