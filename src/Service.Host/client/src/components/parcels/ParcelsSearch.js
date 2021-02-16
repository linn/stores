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

    const [searchTerm, setSearchTerm] = useState(null);

    const [searchTerms, setSearchTerms] = useState({
        parcelNumberSearchTerm: '',
        supplierIdSearchTerm: '',
        carrierIdSearchTerm: '',
        dateCreatedSearchTerm: '',
        supplierInvNoSearchTerm: '',
        consignmentNoSearchTerm: '',
        commentsSearchTerm: ''
    });
    useSearch(fetchItems, searchTerm, null, 'searchTerm', null, 1500, 1);

    const handleRowLinkClick = href => history.push(href);

    const handleSearchTermChange = (propertyName, newValue) => {
        setSearchTerms({ ...searchTerms, [propertyName]: newValue });
        setSearchTerm(queryString.stringify(searchTerms, '&&', '=');
        // setTimeout(() => {
        let stringifiedquery = queryString.stringify(searchTerms, '&&', '=');
        console.log(stringifiedquery);
        console.log("property name is " + propertyName);
        const queryBeforeParam = stringifiedquery.split(`${propertyName}=`)[0];
        const paramToEndOfQuery = stringifiedquery.split(`${propertyName}=`)[1];
        const newlyUpdatedParam = paramToEndOfQuery.split('&')[0];
        const restOfQuery = paramToEndOfQuery.split('&')[1];

        console.log(
            `before is ${queryBeforeParam}, newlyupdated is ${newlyUpdatedParam}, rest of query is ${restOfQuery}`
        );
        if (newlyUpdatedParam !== newValue) {
            stringifiedquery = `${queryBeforeParam}${propertyName}${newValue}&&${restOfQuery}`;
        }
        console.log(stringifiedquery);
        setSearchTerm(stringifiedquery);
        // }, 500);
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
                        value={searchTerms.parcelNumberSearchTerm}
                    />
                </Grid>

                <Grid item xs={2}>
                    <Dropdown
                        onChange={handleSearchTermChange}
                        items={suppliers
                            .map(s => ({
                                ...s,
                                id: s.id,
                                displayText: `${s.name} ( ${s.id} )`
                            }))
                            .sort((a, b) => (a.id > b.id ? 1 : -1))}
                        value={searchTerms.supplierIdSearchTerm}
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
                        items={carriers
                            .map(s => ({
                                ...s,
                                id: s.carrierCode,
                                displayText: `${s.carrierCode} ( ${s.organisationId} )`
                            }))
                            .sort((a, b) => (a.id > b.id ? 1 : -1))}
                        value={searchTerms.carrierIdSearchTerm}
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
                        value={searchTerms.dateCreatedSearchTerm}
                    />
                </Grid>

                <Grid item xs={1}>
                    <SearchInputField
                        label="Supplier Inv No"
                        fullWidth
                        onChange={handleSearchTermChange}
                        propertyName="supplierInvNoSearchTerm"
                        type="text"
                        value={searchTerms.supplierInvNoSearchTerm}
                    />
                </Grid>

                <Grid item xs={2}>
                    <SearchInputField
                        label="Consignment Number"
                        fullWidth
                        onChange={handleSearchTermChange}
                        propertyName="consignmentNoSearchTerm"
                        type="text"
                        value={searchTerms.consignmentNoSearchTerm}
                    />
                </Grid>

                <Grid item xs={2}>
                    <SearchInputField
                        label="Comments"
                        fullWidth
                        onChange={handleSearchTermChange}
                        propertyName="commentSearchTerm"
                        type="text"
                        value={searchTerms.commentSearchTerm}
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
