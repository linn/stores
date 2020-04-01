import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown, Typeahead } from '@linn-it/linn-form-components-library';

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
    decrementRuleName,
    assemblyTechnologyName,
    bomType,
    bomId,
    optionSet,
    drawingReference,
    safetyCriticalPart,
    plannedSurplus,
    decrementRules,
    assemblyTechnologies
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
            <Grid item xs={6}>
                <Dropdown
                    label="Decrement Rule"
                    propertyName="decrementRuleName"
                    items={decrementRules.map(c => ({
                        id: c.rule,
                        displayText: c.description
                    }))}
                    fullWidth
                    allowNoValue
                    value={decrementRuleName}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={6}>
                <Dropdown
                    label="Assembly Technology"
                    propertyName="assemblyTechnologyName"
                    items={assemblyTechnologies.map(c => ({
                        id: c.name,
                        displayText: c.description
                    }))}
                    fullWidth
                    allowNoValue
                    value={assemblyTechnologyName}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={4}>
                <Dropdown
                    label="Bom type"
                    propertyName="bomType"
                    items={['Component', 'Assembly', 'Phantom']}
                    fullWidth
                    allowNoValue={false}
                    value={bomType}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={2}>
                <InputField
                    fullWidth
                    value={bomId}
                    type="number"
                    label="bomId"
                    onChange={handleFieldChange}
                    propertyName="bomId"
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={optionSet}
                    label="option Set"
                    onChange={handleFieldChange}
                    propertyName="optionSet"
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={drawingReference}
                    label="Drawing Reference"
                    onChange={handleFieldChange}
                    propertyName="drawingReference"
                />
            </Grid>
            <Grid item xs={6} />
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
            <Grid item xs={9} />
            <Grid item xs={3}>
                <Dropdown
                    label="planned Surplus?"
                    propertyName="plannedSurplus"
                    items={['Yes', 'No']}
                    fullWidth
                    allowNoValue
                    value={convertToYOrNString(plannedSurplus)}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={9} />
        </Grid>
    );
}

BuildTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    linnProduced: PropTypes.bool,
    decrementRuleName: PropTypes.string,
    assemblyTechnologyName: PropTypes.string,
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
    clearSernosSequencesSearch: PropTypes.func.isRequired,
    decrementRules: PropTypes.arrayOf(PropTypes.shape({})),
    assemblyTechnologies: PropTypes.arrayOf(PropTypes.shape({}))
};

BuildTab.defaultProps = {
    linnProduced: null,
    decrementRuleName: null,
    assemblyTechnologyName: null,
    bomType: null,
    bomId: null,
    optionSet: null,
    drawingReference: null,
    safetyCriticalPart: null,
    plannedSurplus: null,
    sernosSequenceName: null,
    sernosSequenceDescription: null,
    sernosSequencesSearchResults: [],
    sernosSequencesSearchLoading: false,
    decrementRules: [],
    assemblyTechnologies: []
};

export default BuildTab;
