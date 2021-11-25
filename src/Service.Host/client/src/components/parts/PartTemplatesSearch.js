import React, { useEffect, useState, useReducer } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    SearchInputField,
    LinkButton,
    useSearch,
    PaginatedTable,
    Loading
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import queryString from 'query-string';
import Page from '../../containers/Page';

function PartTemplatesSearch({ items, fetchItems, loading, history, privileges }) {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });

    const [rowsToDisplay, setRowsToDisplay] = useState([]);

    const allowedToCreate = () => {
        return privileges?.some(priv => priv === 'part.admin');
    };

    const useStyles = makeStyles(theme => ({
        marginTop1: {
            marginTop: theme.spacing(1),
            display: 'inline-block',
            width: '2em'
        },
        displayInline: {
            display: 'inline'
        }
    }));
    const classes = useStyles();

    function partTemplateReducer(state, action) {
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

    const [state, dispatch] = useReducer(partTemplateReducer, {
        stringifiedSearch: '',
        searchTerms: {
            partTemplateNumberSearchTerm: ''
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
                  partRoot: el.partRoot,
                  description: el.description,
                  hasDataSheet: el.hasDataSheet,
                  hasNumberSequence: el.hasNumberSequence,
                  nextNumber: el.nextNumber,
                  allowVariants: el.allowVariants,
                  variants: el.variants,
                  accountingCompany: el.accountingCompany,
                  productCode: el.productCode,
                  stockControlled: el.stockControlled,
                  linnProduced: el.linnProduced,
                  rmfgCode: el.rmfgCode,
                  bomType: el.bomType,
                  assemblyTechnology: el.assemblyTechnology,
                  allowPartCreation: el.allowPartCreation,
                  paretoCode: el.paretoCode
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
        items
    ]);

    const columns = {
        partRoot: 'Part Root',
        description: 'Description',
        hasDataSheet: 'Has Data Sheet',
        hasNumberSequence: 'Has Number Sequence',
        nextNumber: 'Next Number',
        allowVariants: 'Allow Variants',
        variants: 'Variants',
        accountingCompany: 'Accounting Company',
        productCode: 'Product Code',
        stockControlled: 'Stock Controlled',
        linnProduced: 'Linn Produced',
        rmfgCode: 'RM FG Code',
        bomType: 'BOM Type',
        assemblyTechnology: 'Assembly Technology',
        allowPartCreation: 'Allow Part Creation',
        paretoCode: 'Pareto Code'
    };

    return (
        <Page>
            <Grid spacing={3} container justifyContent="center">
                <Grid item xs={11} />
                <Grid item xs={1}>
                    <LinkButton
                        text="Create"
                        to="/inventory/part-templates/create"
                        disabled={!allowedToCreate()}
                    />
                </Grid>
                <Grid item xs={3}>
                    <SearchInputField
                        label="Part Template Part Root Search"
                        fullWidth
                        placeholder="search.."
                        onChange={handleSearchTermChange}
                        propertyName="partRoot"
                        type="text"
                        value={state.searchTerms.partTemplateNumberSearchTerm}
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

PartTemplatesSearch.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            partTemplateNumber: PropTypes.number
        })
    ).isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired
};

PartTemplatesSearch.defaultProps = {
    loading: false
};

export default PartTemplatesSearch;
