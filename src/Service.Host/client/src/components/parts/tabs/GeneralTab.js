import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import {
    SaveBackCancelButtons,
    InputField,
    Dropdown,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    Typeahead
    //DatePicker
} from '@linn-it/linn-form-components-library';

function GeneralTab({
    accountingCompany,
    handleFieldChange,
    accountingCompanies,
    rootProduct,
    searchRootProducts,
    rootProductsSearchResults,
    rootProductsSearchLoading,
    clearRootProductsSearch
}) {
    return (
        <Grid container spacing={3}>
            <Grid itemx xs={4}>
                <Dropdown
                    label="AccountingCompanies"
                    propertyName="accountingCompany"
                    items={accountingCompanies.map(c => ({
                        id: c.name,
                        displayText: c.description
                    }))}
                    fullWidth
                    allowNoValue
                    value={accountingCompany}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={4}>
                <Typeahead
                    onSelect={newValue => {
                        handleFieldChange('rootProduct', newValue.name);
                    }}
                    label="Root Product"
                    modal
                    items={rootProductsSearchResults}
                    value={rootProduct}
                    loading={rootProductsSearchLoading}
                    fetchItems={searchRootProducts}
                    links={false}
                    clearSearch={() => clearRootProductsSearch}
                    placeholder="Search Root Products"
                />
            </Grid>
            <Grid item xs={8} />
        </Grid>
    );
}

const accountingCompanyShape = PropTypes.shape({
    name: PropTypes.string,
    description: PropTypes.string
});

const rootProductsShape = PropTypes.shape({
    name: PropTypes.string,
    description: PropTypes.string
});

GeneralTab.propTypes = {
    accountingCompany: accountingCompanyShape,
    accountingCompanies: PropTypes.arrayOf(accountingCompanyShape),
    handleFieldChange: PropTypes.func.isRequired,
    rootProduct: rootProductsShape,
    searchRootProducts: PropTypes.func,
    rootProductsSearchResults: PropTypes.arrayOf(rootProductsShape),
    rootProductsSearchLoading: PropTypes.bool,
    clearRootProductsSearch: PropTypes.func
};

GeneralTab.defaultProps = {
    accountingCompany: null,
    accountingCompanies: null,
    rootProduct: null,
    rootProductsSearchResults: null,
    searchRootProducts: PropTypes.func,
    rootProductsSearchLoading: false,
    clearRootProductsSearch: () => {}
};

export default GeneralTab;
