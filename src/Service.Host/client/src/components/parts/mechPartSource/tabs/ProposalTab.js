import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, DatePicker, Dropdown, Typeahead } from '@linn-it/linn-form-components-library';

function ProposalTab({
    handleFieldChange,
    partType,
    notes,
    proposedBy,
    proposedByName,
    dateEntered,
    mechanicalOrElectrical,
    safetyCritical,
    performanceCritical,
    emcCritical,
    singleSource,
    safetyDataDirectory,
    estimatedVolume,
    samplesRequired,
    sampleQuantity,
    dateSamplesRequired,
    rohsReplace,
    linnPartNumber,
    linnPartDescription,
    handleLinnPartChange,
    searchParts,
    partsSearchResults,
    partsSearchLoading,
    clearPartsSearch,
    assemblyType,
    description,
    projectCode,
    projectName,
    departmentsSearchResults,
    departmentsSearchLoading,
    searchDepartments,
    clearSearchDepartments,
    handleProjectChange
}) {
    const electricalPartTypes = [
        'CAP',
        'CONN',
        'CRYS',
        'FAN',
        'IC',
        'IND',
        'MCAS',
        'MISS',
        'PCB',
        'RES',
        'SWRL',
        'TRAN',
        'DIO',
        'PROT',
        'ANT',
        'MOD'
    ];

    const mechanicalPartTypes = [
        'RECORD',
        'MCP',
        'BOX',
        'GRILL',
        'LBL',
        'MECH',
        'CAB',
        'MOLD',
        'RAW',
        'SPKR',
        'TAPE',
        'GLUE',
        'WOOD'
    ];
    return (
        <Grid container spacing={3}>
            <Grid item xs={4}>
                <InputField
                    value={proposedBy}
                    propertyName="proposedBy"
                    disabled
                    fullWidth
                    onChange={handleFieldChange}
                    label="Proposed By"
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    value={proposedByName}
                    propertyName="proposedByName"
                    disabled
                    fullWidth
                    onChange={handleFieldChange}
                    label="Name"
                />
            </Grid>
            <Grid item xs={4}>
                <DatePicker
                    label="Date Entered"
                    value={dateEntered}
                    onChange={value => {
                        handleFieldChange('dateEntered', value);
                    }}
                    disabled
                />
            </Grid>
            <Grid item xs={3}>
                <Dropdown
                    label="Mechanical Or Electrical*"
                    propertyName="mechanicalOrElectrical"
                    items={['M', 'E']}
                    fullWidth
                    value={mechanicalOrElectrical}
                    onChange={handleFieldChange}
                />
            </Grid>
            {mechanicalOrElectrical === 'E' ? (
                <Grid item xs={3}>
                    <Dropdown
                        label="Part Type*"
                        propertyName="partType"
                        items={electricalPartTypes}
                        fullWidth
                        value={partType}
                        onChange={handleFieldChange}
                    />
                </Grid>
            ) : (
                <Grid item xs={3}>
                    <Dropdown
                        label="Part Type*"
                        propertyName="partType"
                        items={mechanicalPartTypes}
                        fullWidth
                        value={partType}
                        onChange={handleFieldChange}
                    />
                </Grid>
            )}
            <Grid item xs={6}>
                <InputField
                    onChange={handleFieldChange}
                    propertyName="description"
                    fullWidth
                    value={description}
                    label="description"
                />
            </Grid>

            <Grid item xs={3}>
                <Dropdown
                    label="Safety Critical"
                    propertyName="safetyCritical"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={safetyCritical}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={9} />
            <Grid item xs={3}>
                <Dropdown
                    label="Performance Critical"
                    propertyName="performanceCritical"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={performanceCritical}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={9} />

            <Grid item xs={3}>
                <Dropdown
                    label="EMC Critical"
                    propertyName="emcCritical"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={emcCritical}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={9} />

            <Grid item xs={3}>
                <Dropdown
                    label="Single Source"
                    propertyName="singleSource"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={singleSource}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={9} />
            <Grid item xs={8}>
                <InputField
                    onChange={handleFieldChange}
                    propertyName="safetyDataDirectory"
                    fullWidth
                    value={safetyDataDirectory}
                    label="SafetyDataDirectory"
                />
            </Grid>
            <Grid item xs={12} />
            <Grid item xs={2}>
                <InputField
                    onChange={handleFieldChange}
                    propertyName="estimatedVolume"
                    fullWidth
                    type="number"
                    value={estimatedVolume}
                    label="Estimated Volume"
                />
            </Grid>
            <Grid item xs={10} />
            <Grid item xs={3}>
                <Dropdown
                    label="Samples Required*"
                    propertyName="samplesRequired"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue={false}
                    value={samplesRequired}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={2}>
                <InputField
                    onChange={handleFieldChange}
                    propertyName="sampleQuantity"
                    fullWidth
                    type="number"
                    value={sampleQuantity}
                    label="Sample Quantity"
                />
            </Grid>
            <Grid item xs={4}>
                <DatePicker
                    label="Date Samples Required"
                    value={dateSamplesRequired}
                    onChange={value => {
                        handleFieldChange('dateSamplesRequired', value);
                    }}
                />
            </Grid>
            <Grid item xs={3} />
            <Grid item xs={3}>
                <Dropdown
                    label="Replacement For Part"
                    propertyName="rohsReplace"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={rohsReplace}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <Typeahead
                    onSelect={newValue => {
                        handleLinnPartChange(newValue);
                    }}
                    label="Linn Part Number for Replaced Part"
                    modal
                    items={partsSearchResults}
                    value={linnPartNumber}
                    loading={partsSearchLoading}
                    fetchItems={searchParts}
                    links={false}
                    clearSearch={() => clearPartsSearch}
                    placeholder="Search Codes"
                />
            </Grid>
            <Grid item xs={5}>
                <InputField
                    onChange={handleFieldChange}
                    propertyName="linnPartDescription"
                    fullWidth
                    disabled
                    value={linnPartDescription}
                    label="Description"
                    rows={4}
                />
            </Grid>
            <Grid item xs={3}>
                <Dropdown
                    label="Assembly Type*"
                    propertyName="assemblyType"
                    items={['TH', 'SM']}
                    fullWidth
                    allowNoValue
                    value={assemblyType}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={9} />
            <Grid item xs={3}>
                <Typeahead
                    onSelect={newValue => {
                        handleProjectChange(newValue);
                    }}
                    label="Project Code"
                    modal
                    items={departmentsSearchResults}
                    value={projectCode}
                    propertyName="projectCode"
                    loading={departmentsSearchLoading}
                    fetchItems={searchDepartments}
                    links={false}
                    clearSearch={clearSearchDepartments}
                    required
                    placeholder="Search for project code"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    onChange={handleFieldChange}
                    propertyName="projectName"
                    fullWidth
                    disabled
                    value={projectName}
                    label="Project Name"
                />
            </Grid>
            <Grid item xs={3} />
            <Grid item xs={8}>
                <InputField
                    onChange={handleFieldChange}
                    propertyName="notes"
                    fullWidth
                    value={notes}
                    label="Notes"
                    rows={4}
                />
            </Grid>
            <Grid item xs={4} />
        </Grid>
    );
}

ProposalTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    notes: PropTypes.string,
    partType: PropTypes.string,
    proposedBy: PropTypes.number,
    proposedByName: PropTypes.string,
    dateEntered: PropTypes.string,
    mechanicalOrElectrical: PropTypes.string,
    safetyCritical: PropTypes.string,
    performanceCritical: PropTypes.string,
    emcCritical: PropTypes.string,
    singleSource: PropTypes.string,
    safetyDataDirectory: PropTypes.string,
    estimatedVolume: PropTypes.number,
    samplesRequired: PropTypes.string,
    sampleQuantity: PropTypes.number,
    dateSamplesRequired: PropTypes.string,
    rohsReplace: PropTypes.string,
    linnPartNumber: PropTypes.string,
    linnPartDescription: PropTypes.string,
    assemblyType: PropTypes.string,
    projectCode: PropTypes.string,
    projectName: PropTypes.string,
    handleLinnPartChange: PropTypes.func.isRequired,
    searchParts: PropTypes.func.isRequired,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    partsSearchLoading: PropTypes.bool,
    clearPartsSearch: PropTypes.func.isRequired,
    description: PropTypes.string,
    departmentsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    departmentsSearchLoading: PropTypes.bool,
    searchDepartments: PropTypes.func.isRequired,
    clearSearchDepartments: PropTypes.func.isRequired,
    handleProjectChange: PropTypes.func.isRequired
};

ProposalTab.defaultProps = {
    notes: null,
    partType: null,
    proposedBy: null,
    proposedByName: null,
    dateEntered: null,
    mechanicalOrElectrical: null,
    safetyCritical: null,
    performanceCritical: null,
    emcCritical: null,
    singleSource: null,
    safetyDataDirectory: null,
    estimatedVolume: null,
    samplesRequired: null,
    sampleQuantity: null,
    dateSamplesRequired: null,
    rohsReplace: null,
    projectCode: null,
    projectName: null,
    linnPartNumber: null,
    linnPartDescription: null,
    assemblyType: null,
    partsSearchResults: [],
    partsSearchLoading: false,
    description: null,
    departmentsSearchResults: [],
    departmentsSearchLoading: false
};

export default ProposalTab;
