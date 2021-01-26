import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown, Typeahead, LinkButton } from '@linn-it/linn-form-components-library';

function BuildTab({
    appRoot,
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
                    label="Linn Produced Assembly"
                    propertyName="linnProduced"
                    items={['Yes', 'No']}
                    fullWidth
                    allowNoValue
                    value={convertToYOrNString(linnProduced)}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4} />
            <Grid item xs={4}>
                <LinkButton
                    to={`${appRoot}/production/maintenance/production-trigger-levels/create`}
                    text="Trigger Levels"
                    external
                />
            </Grid>
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
            <Grid item xs={7}>
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
            <Grid item xs={1}>
                <LinkButton
                    to="/inventory/parts/decrement-rules/create"
                    text="Change"
                    tooltip="Coming soon - still on Oracle Forms"
                    disabled
                />
            </Grid>
            <Grid item xs={4} />
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
                    items={[
                        { id: 'C', displayText: 'Component' },
                        { id: 'A', displayText: 'Assembly' },
                        { id: 'P', displayText: 'Phantom' }
                    ]}
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
            <Grid item xs={2}>
                <LinkButton
                    to="/inventory/parts/change-bom-type"
                    text="Change Bom Type"
                    tooltip="Coming soon - still on Oracle Forms"
                    disabled
                />
            </Grid>
            <Grid item xs={2} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={optionSet}
                    label="option Set"
                    disabled
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
    appRoot: PropTypes.string.isRequired,
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
