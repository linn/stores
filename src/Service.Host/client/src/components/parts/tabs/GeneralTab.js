import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    InputField,
    Dropdown,
    Typeahead,
    DatePicker,
    TypeaheadTable
} from '@linn-it/linn-form-components-library';
import Link from '@material-ui/core/Link';

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
    nominal,
    nominalDescription,
    nominalAccountsSearchResults,
    nominalAccountsSearchLoading,
    searchNominalAccounts,
    clearNominalAccountsSearch,
    paretoCode,
    stockControlled,
    department,
    departmentDescription,
    safetyCriticalPart,
    safetyCriticalHelperText,
    performanceCriticalPart,
    emcCriticalPart,
    singleSourcePart,
    cccCriticalPart,
    psuPart,
    editStatus,
    safetyCertificateExpirationDate,
    safetyDataDirectory,
    rawOrFinished,
    salesArticleNumber,
    partNumber
}) {
    const nominalAccountsTable = {
        totalItemCount: nominalAccountsSearchResults.length,
        rows: nominalAccountsSearchResults?.map((item, i) => ({
            id: item.nominalAccountId,
            values: [
                { id: `${i}-0`, value: `${item.nominalCode}` },
                { id: `${i}-1`, value: `${item.description || ''}` },
                { id: `${i}-2`, value: `${item.departmentCode || ''}` },
                { id: `${i}-3`, value: `${item.departmentDescription || ''}` }
            ],
            links: item.links
        }))
    };

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
                    onChange={handleFieldChange}
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
                    propertyName="rootProduct"
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
                <Dropdown
                    label="Raw/Finished"
                    propertyName="rawOrFinished"
                    items={['R', 'F']}
                    fullWidth
                    allowNoValue
                    value={rawOrFinished}
                    disabled={editStatus !== 'create'}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8} />

            <Grid item xs={4}>
                <TypeaheadTable
                    table={nominalAccountsTable}
                    columnNames={['Nominal', 'Description', 'Dept', 'Name']}
                    fetchItems={searchNominalAccounts}
                    modal
                    placeholder="Search Nominal/Dept"
                    links={false}
                    clearSearch={clearNominalAccountsSearch}
                    loading={nominalAccountsSearchLoading}
                    label="Nominal"
                    title="Search Nominals"
                    propertyName="nominal"
                    value={nominal}
                    onSelect={newValue => handleFieldChange('nominalAccount', newValue)}
                    debounce={1000}
                    minimumSearchTermLength={2}
                />
            </Grid>
            <Grid item xs={8}>
                <InputField
                    fullWidth
                    value={nominalDescription}
                    label="Description"
                    disabled
                    onChange={handleFieldChange}
                    propertyName="nominalDescription"
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={department}
                    label="Dept"
                    onChange={handleFieldChange}
                    propertyName="department"
                    disabled
                />
            </Grid>
            <Grid item xs={8}>
                <InputField
                    fullWidth
                    value={departmentDescription}
                    label="Description"
                    disabled
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <Typeahead
                    onSelect={newValue => {
                        handleFieldChange('productAnalysisCode', newValue);
                    }}
                    label="Product Analysis Code"
                    modal
                    items={productAnalysisCodeSearchResults}
                    value={productAnalysisCode}
                    loading={productAnalysisCodesSearchLoading}
                    fetchItems={searchProductAnalysisCodes}
                    links={false}
                    disabled={!!salesArticleNumber}
                    clearSearch={clearProductAnalysisCodesSearch}
                    placeholder="Search Codes"
                    propertyName="productAnalysisCode"
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
            <Grid item xs={3}>
                <Dropdown
                    label="Stores Controlled?"
                    propertyName="stockControlled"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue={false}
                    value={stockControlled}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={9} />

            <Grid item xs={3}>
                <Dropdown
                    label="Safety Critical?"
                    propertyName="safetyCriticalPart"
                    items={['Y', 'N']}
                    fullWidth
                    helperText={safetyCriticalHelperText}
                    allowNoValue={false}
                    value={safetyCriticalPart}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <Dropdown
                    label="Performance Critical?"
                    propertyName="performanceCriticalPart"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={performanceCriticalPart}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />

            <Grid item xs={3}>
                <Dropdown
                    label="EMC Critical?"
                    propertyName="emcCriticalPart"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={emcCriticalPart}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <Dropdown
                    label="Single Source?"
                    propertyName="singleSourcePart"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={singleSourcePart}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />

            <Grid item xs={3}>
                <Dropdown
                    label="CCC Critical?"
                    propertyName="cccCriticalPart"
                    items={['Y', 'N']}
                    allowNoValue={false}
                    fullWidth
                    value={cccCriticalPart}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <Dropdown
                    label="Approved  PSU?"
                    propertyName="psuPart"
                    allowNoValue={false}
                    items={['Y', 'N']}
                    fullWidth
                    value={psuPart}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={3}>
                <DatePicker
                    label="Safety Certificate Expiry Date"
                    value={safetyCertificateExpirationDate}
                    onChange={value => {
                        handleFieldChange('safetyCertificateExpirationDate', value);
                    }}
                />
            </Grid>
            <Grid item xs={9}>
                <InputField
                    fullWidth
                    value={safetyDataDirectory}
                    label="EMC + Safety Data Directory"
                    onChange={handleFieldChange}
                    propertyName="safetyDataDirectory"
                />
            </Grid>
            <Grid item xs={12}>
                <Link href={`/production/boms/ShowBom.aspx?partNumber=${partNumber}`}>
                    View Bom
                </Link>
            </Grid>
        </Grid>
    );
}

const accountingCompanyShape = PropTypes.shape({
    name: PropTypes.string,
    description: PropTypes.string
});

const nominalShape = PropTypes.shape({
    nominalCode: PropTypes.string,
    description: PropTypes.string,
    departmentCode: PropTypes.string,
    departmentDescription: PropTypes.string
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
    accountingCompany: PropTypes.string,
    accountingCompanies: PropTypes.arrayOf(accountingCompanyShape),
    handleFieldChange: PropTypes.func.isRequired,
    rootProduct: rootProductShape,
    searchRootProducts: PropTypes.func,
    rootProductsSearchResults: PropTypes.arrayOf(rootProductShape),
    rootProductsSearchLoading: PropTypes.bool,
    clearRootProductsSearch: PropTypes.func.isRequired,
    productAnalysisCode: PropTypes.string,
    productAnalysisCodeSearchResults: PropTypes.arrayOf(productAnalysisCodeShape),
    productAnalysisCodesSearchLoading: PropTypes.bool,
    productAnalysisCodeDescription: PropTypes.string,
    nominal: PropTypes.string,
    nominalDescription: PropTypes.string,
    nominalAccountsSearchResults: PropTypes.arrayOf(nominalShape),
    nominalAccountsSearchLoading: PropTypes.bool,
    paretoCode: PropTypes.string,
    searchNominalAccounts: PropTypes.func.isRequired,
    clearNominalAccountsSearch: PropTypes.func,
    searchProductAnalysisCodes: PropTypes.func.isRequired,
    clearProductAnalysisCodesSearch: PropTypes.func,
    stockControlled: PropTypes.string,
    safetyCriticalPart: PropTypes.string,
    performanceCriticalPart: PropTypes.string,
    emcCriticalPart: PropTypes.string,
    singleSourcePart: PropTypes.string,
    cccCriticalPart: PropTypes.string,
    psuPart: PropTypes.string,
    safetyCertificateExpirationDate: PropTypes.string,
    safetyDataDirectory: PropTypes.string,
    safetyCriticalHelperText: PropTypes.string,
    department: PropTypes.string,
    departmentDescription: PropTypes.string,
    rawOrFinished: PropTypes.string,
    editStatus: PropTypes.string,
    salesArticleNumber: PropTypes.string,
    partNumber: PropTypes.string
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
    nominal: null,
    nominalDescription: null,
    nominalAccountsSearchResults: [],
    nominalAccountsSearchLoading: false,
    paretoCode: null,
    stockControlled: null,
    safetyCriticalPart: null,
    performanceCriticalPart: null,
    emcCriticalPart: null,
    singleSourcePart: null,
    cccCriticalPart: null,
    psuPart: null,
    safetyCertificateExpirationDate: null,
    safetyDataDirectory: null,
    safetyCriticalHelperText: null,
    clearProductAnalysisCodesSearch: () => {},
    clearNominalAccountsSearch: () => {},
    department: null,
    departmentDescription: null,
    rawOrFinished: null,
    editStatus: null,
    salesArticleNumber: null,
    partNumber: null
};

export default GeneralTab;
