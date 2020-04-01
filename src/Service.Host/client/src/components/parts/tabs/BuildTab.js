import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown, Typeahead, DatePicker } from '@linn-it/linn-form-components-library';

function BuildTab({
    handleFieldChange,
    linnProduced,
    sernosSequenceName,
    sernosSequenceDescription,
    handleSernosSequenceChange,
    sernosSequencesSearchResults,
    sernosSequencesSearchLoading,
    searchSernosSequences,
    clearSernosSequencesSearch,
    decrementRule,
    assemblyTechnology,
    bomType,
    bomId,
    optionSet,
    drawingReference,
    safetyCriticalPart,
    plannedSurplus
}) {
    const convertToYOrNString = booleanValue => {
        if (booleanValue === '' || booleanValue === null) {
            return null;
        }
        return booleanValue ? 'Yes' : 'No';
    };
    return (
        <Grid container spacing={3}>
            <Grid item xs={4}>
                <Dropdown
                    label="Linn"
                    propertyName="linnProduced"
                    items={['Yes', 'No']}
                    fullWidth
                    allowNoValue
                    value={convertToYOrNString(linnProduced)}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid ite xs={8} />
            <Grid item xs={4}>
                <Typeahead
                    onSelect={newValue => {
                        handleSernosSequenceChange(newValue);
                    }}
                    label="Sernos Sequence"
                    modal
                    items={sernosSequencesSearchResults}
                    value={sernosSequenceName}
                    loading={sernosSequencesSearchLoading}
                    fetchItems={searchSernosSequences}
                    links={false}
                    clearSearch={() => clearSernosSequencesSearch}
                    placeholder="Search Sequences"
                />
            </Grid>
            <Grid item xs={8}>
                <InputField
                    fullWidth
                    value={sernosSequenceDescription}
                    label="Description"
                    disabled
                    onChange={handleFieldChange}
                    propertyName="ProductAnalysisCodeDescription"
                />
            </Grid>
            {/* <Grid item xs={2}>
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
                <InputField
                    fullWidth
                    value={nominal}
                    label="Nominal"
                    onChange={handleFieldChange}
                    propertyName="nominal"
                />
            </Grid>
            <Grid item xs={8}>
                <InputField
                    fullWidth
                    value={nominalDescription}
                    label="Description"
                    disabled
                    onChange={handleFieldChange}
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
            <Grid item xs={3}>
                <Dropdown
                    label="Stores Controlled?"
                    propertyName="stockControlled"
                    items={['Yes', 'No']}
                    fullWidth
                    allowNoValue={false}
                    value={convertToYOrNString(stockControlled)}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={9} />

            <Grid item xs={3}>
                <Dropdown
                    label="Safety Critical?"
                    propertyName="safetyCriticalPart"
                    items={['Yes', 'No']}
                    fullWidth
                    allowNoValue
                    value={convertToYOrNString(safetyCriticalPart)}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <Dropdown
                    label="Performance Critical?"
                    propertyName="performanceCriticalPart"
                    items={['Yes', 'No']}
                    fullWidth
                    allowNoValue
                    value={convertToYOrNString(performanceCriticalPart)}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />

            <Grid item xs={3}>
                <Dropdown
                    label="EMC Critical?"
                    propertyName="emcCriticalPart"
                    items={['Yes', 'No']}
                    fullWidth
                    allowNoValue
                    value={convertToYOrNString(emcCriticalPart)}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <Dropdown
                    label="Single Source?"
                    propertyName="singleSourcePart"
                    items={['Yes', 'No']}
                    fullWidth
                    allowNoValue
                    value={convertToYOrNString(singleSourcePart)}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />

            <Grid item xs={3}>
                <Dropdown
                    label="CCC Critical?"
                    propertyName="cccCriticalPart"
                    items={['Yes', 'No']}
                    allowNoValue={false}
                    fullWidth
                    value={convertToYOrNString(cccCriticalPart)}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <Dropdown
                    label="Approved  PSU?"
                    propertyName="psuPart"
                    allowNoValue={false}
                    items={['Yes', 'No']}
                    fullWidth
                    value={convertToYOrNString(psuPart)}
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
            </Grid> */}
        </Grid>
    );
}

BuildTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    linnProduced: PropTypes.bool,
    decrementRule: PropTypes.string,
    assemblyTechnology: PropTypes.string,
    bomType: PropTypes.string,
    bomId: PropTypes.number,
    optionSet: PropTypes.string,
    drawingReference: PropTypes.string,
    safetyCriticalPart: PropTypes.bool,
    plannedSurplus: PropTypes.bool,
    sernosSequenceName: PropTypes.string,
    sernosSequenceDescription: PropTypes.string,
    handleSernosSequenceChange: PropTypes.func.isRequired,
    sernosSequencesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    sernosSequencesSearchLoading: PropTypes.bool,
    searchSernosSequences: PropTypes.func.isRequired,
    clearSernosSequencesSearch: PropTypes.func.isRequired
};

BuildTab.defaultProps = {
    linnProduced: null,
    decrementRule: null,
    assemblyTechnology: null,
    bomType: null,
    bomId: null,
    optionSet: null,
    drawingReference: null,
    safetyCriticalPart: null,
    plannedSurplus: null,
    sernosSequenceName: null,
    sernosSequenceDescription: null,
    sernosSequencesSearchResults: [],
    sernosSequencesSearchLoading: false
};

export default BuildTab;
