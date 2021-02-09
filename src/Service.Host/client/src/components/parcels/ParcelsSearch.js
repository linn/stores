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
import { makeStyles } from '@material-ui/styles';
import PropTypes from 'prop-types';

const useStyles = makeStyles(theme => ({
    grid: {
        marginTop: theme.spacing(4),
        paddingLeft: theme.spacing(1),
        paddingRight: theme.spacing(1)
    }
}));

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

    const classes = useStyles();

    const [rowsToDisplay, setRowsToDisplay] = useState([]);
    const [allowedToCreate, setAllowedToCreate] = useState(false);
    // const [snackbarVisible, setSnackbarVisible] = useState(editStatus === 'deleted');

    const [searchTerm, setSearchTerm] = useState(null);
    const [parcelNumberSearch, setParcelNumberSearch] = useState('');
    const [supplierIdSearch, setSupplierIdSearch] = useState('');
    const [carrierIdSearch, setCarrierIdSearch] = useState('');
    const [dateCreatedSearch, setDateCreatedSearch] = useState(null);
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
                  supplier: `${el.supplierId} - ${el.supplierName}`,
                  carrier: `${el.carrierId} - ${el.carrierName}`,
                  dateCreated: el.dateCreated,
                  supplerInvNo: el.supplerInvNo,
                  consignmentNo: el.consignmentNo,
                  comments: el.comments,
                  id: el.parcelNumber,
                  href: el.links.find(l => l.rel === 'self')?.href
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
        supplerInvNo: 'suppler invoice number',
        consignmentNo: 'consignment number',
        comments: 'comments'
    };

    // setAllowedToCreate(utilities.getHref(applicationState, 'edit') !== null);

    return (
        <Grid className={classes.grid} spacing={3} container justify="center">
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
                <SearchInputField
                    label="Supplier"
                    fullWidth
                    onChange={handleSupplierSearchChange}
                    propertyName="supplierSearchTerm"
                    type="text"
                    value={supplierIdSearch}
                    debounce={5000}
                />
            </Grid>

            <Grid item xs={2}>
                <SearchInputField
                    label="Carrier"
                    fullWidth
                    onChange={handleCarrierSearchChange}
                    propertyName="carrierSearchTerm"
                    type="text"
                    value={carrierIdSearch}
                    debounce={5000}
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
