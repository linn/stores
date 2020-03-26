import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown, Typeahead } from '@linn-it/linn-form-components-library';

function GeneralTab({
    accountingCompany,
    handleFieldChange,
    accountingCompanies,
    productAnalysisCodeSearchResults,
    productAnalysisCodesSearchLoading,
    searchProductAnalysisCodes,
    clearProductAnalysisCodesSearch,
    productAnalysisCode,
    productAnalysisCodeDescription,
    rootProduct,
    searchRootProducts,
    rootProductsSearchResults,
    rootProductsSearchLoading,
    clearRootProductsSearch,
    department,
    departmentDescription,
    departmentsSearchResults,
    departmentsSearchLoading,
    searchDepartments,
    clearDepartmentsSearch,
    handleDepartmentChange,
    handleProductAnalysisCodeChange,
    handleAccountingCompanyChange,
    paretoCode
}) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={4}>
                <Dropdown
                    label="Accounting Company"
                    propertyName="accountingCompany"
                    items={accountingCompanies.map(c => ({
                        id: c.name,
                        displayText: c.description
                    }))}
                    fullWidth
                    allowNoValue
                    value={accountingCompany}
                    onChange={(_, newValue) => {
                        handleAccountingCompanyChange(newValue);
                    }}
                />
            </Grid>
            <Grid item xs={2}>
                <InputField
                    fullWidth
                    value={paretoCode}
                    label="Pareto Code"
                    disabled
                    onChange={handleFieldChange}
                    propertyName="paretoCode"
                />
            </Grid>
            <Grid item xs={6} />
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
            <Grid item xs={4}>
                <Typeahead
                    onSelect={newValue => {
                        handleDepartmentChange(newValue);
                    }}
                    label="Department"
                    modal
                    items={departmentsSearchResults}
                    value={department}
                    loading={departmentsSearchLoading}
                    fetchItems={searchDepartments}
                    links={false}
                    clearSearch={() => clearDepartmentsSearch}
                    placeholder="Search Code or Description"
                />
            </Grid>
            <Grid item xs={8}>
                <InputField
                    fullWidth
                    value={departmentDescription}
                    label="Description"
                    disabled
                    onChange={handleFieldChange}
                    propertyName="departmentDescription"
                />
            </Grid>
            <Grid item xs={4}>
                <Typeahead
                    onSelect={newValue => {
                        handleProductAnalysisCodeChange(newValue);
                    }}
                    label="Product Analysis Code"
                    modal
                    items={productAnalysisCodeSearchResults}
                    value={productAnalysisCode}
                    loading={productAnalysisCodesSearchLoading}
                    fetchItems={searchProductAnalysisCodes}
                    links={false}
                    clearSearch={() => clearProductAnalysisCodesSearch}
                    placeholder="Search Codes"
                />
            </Grid>
            <Grid item xs={8}>
                <InputField
                    fullWidth
                    value={productAnalysisCodeDescription}
                    label="Description"
                    disabled
                    onChange={handleFieldChange}
                    propertyName="ProductAnalysisCodeDescription"
                />
            </Grid>
        </Grid>
    );
}

const accountingCompanyShape = PropTypes.shape({
    name: PropTypes.string,
    description: PropTypes.string
});

const departmentShape = PropTypes.shape({
    departmentCode: PropTypes.string,
    description: PropTypes.string
});

const rootProductShape = PropTypes.shape({
    name: PropTypes.string,
    description: PropTypes.string
});

const productAnalysisCodeShape = PropTypes.shape({
    productCode: PropTypes.string,
    description: PropTypes.string
});

GeneralTab.propTypes = {
    accountingCompany: accountingCompanyShape,
    accountingCompanies: PropTypes.arrayOf(accountingCompanyShape),
    handleFieldChange: PropTypes.func.isRequired,
    rootProduct: rootProductShape,
    searchRootProducts: PropTypes.func,
    rootProductsSearchResults: PropTypes.arrayOf(rootProductShape),
    rootProductsSearchLoading: PropTypes.bool,
    clearRootProductsSearch: PropTypes.func.isRequired,
    productAnalysisCode: productAnalysisCodeShape,
    productAnalysisCodeSearchResults: PropTypes.arrayOf(productAnalysisCodeShape),
    productAnalysisCodesSearchLoading: PropTypes.bool,
    productAnalysisCodeDescription: PropTypes.string,
    department: departmentShape,
    departmentDescription: PropTypes.string,
    departmentsSearchResults: PropTypes.arrayOf(departmentShape),
    departmentsSearchLoading: PropTypes.bool,
    paretoCode: PropTypes.string,
    searchDepartments: PropTypes.func.isRequired,
    clearDepartmentsSearch: PropTypes.func.isRequired,
    handleDepartmentChange: PropTypes.func.isRequired,
    handleProductAnalysisCodeChange: PropTypes.func.isRequired,
    handleAccountingCompanyChange: PropTypes.func.isRequired,
    searchProductAnalysisCodes: PropTypes.func.isRequired,
    clearProductAnalysisCodesSearch: PropTypes.func.isRequired
};

GeneralTab.defaultProps = {
    accountingCompany: null,
    accountingCompanies: null,
    rootProduct: null,
    rootProductsSearchResults: null,
    searchRootProducts: PropTypes.func,
    rootProductsSearchLoading: false,
    productAnalysisCode: null,
    productAnalysisCodeSearchResults: [],
    productAnalysisCodesSearchLoading: false,
    productAnalysisCodeDescription: null,
    department: null,
    departmentDescription: null,
    departmentsSearchResults: [],
    departmentsSearchLoading: false,
    paretoCode: null
};

export default GeneralTab;
