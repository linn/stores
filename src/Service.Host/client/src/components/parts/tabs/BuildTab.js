import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown, Typeahead, LinkButton } from '@linn-it/linn-form-components-library';

function BuildTab({
    appRoot,
    handleFieldChange,
    creating,
    linnProduced,
    sernosSequenceName,
    sernosSequenceDescription,
    sernosSequencesSearchResults,
    sernosSequencesSearchLoading,
    searchSernosSequences,
    clearSernosSequencesSearch,
    decrementRuleName,
    assemblyTechnologyName,
    bomType,
    bomId,
    bomVerifyFreqWeeks,
    optionSet,
    drawingReference,
    safetyCriticalPart,
    plannedSurplus,
    decrementRules,
    assemblyTechnologies,
    partNumber
}) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={4}>
                <Dropdown
                    label="Linn Produced Assembly"
                    propertyName="linnProduced"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={linnProduced}
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
                        handleFieldChange('sernosSequenceName', newValue);
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
                    disabled={!creating()}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={1}>
                <LinkButton
                    to="/parts/decrement-rules/create"
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
                    allowNoValue
                    disabled={!creating()}
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
                    to={`/purchasing/change-bom-type?partNumber=${partNumber}`}
                    text="Change Bom Type"
                    disabled={creating()}
                />
            </Grid>
            <Grid item xs={2}>
                {bomId && (
                    <LinkButton
                        to={`/purchasing/boms/bom-utility?bomName=${partNumber}`}
                        text="View Bom"
                    />
                )}
            </Grid>
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
            <Grid item xs={2}>
                <InputField
                    fullWidth
                    value={bomVerifyFreqWeeks}
                    type="number"
                    label="Bom Frequency (Weeks)"
                    onChange={handleFieldChange}
                    propertyName="bomVerifyFreqWeeks"
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
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={safetyCriticalPart}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={9} />
            <Grid item xs={3}>
                <Dropdown
                    label="planned Surplus?"
                    propertyName="plannedSurplus"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={plannedSurplus}
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
    creating: PropTypes.func.isRequired,
    linnProduced: PropTypes.string,
    decrementRuleName: PropTypes.string,
    assemblyTechnologyName: PropTypes.string,
    bomType: PropTypes.string,
    bomId: PropTypes.number,
    bomVerifyFreqWeeks: PropTypes.number,
    optionSet: PropTypes.string,
    drawingReference: PropTypes.string,
    safetyCriticalPart: PropTypes.string,
    plannedSurplus: PropTypes.string,
    sernosSequenceName: PropTypes.string,
    sernosSequenceDescription: PropTypes.string,
    sernosSequencesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    sernosSequencesSearchLoading: PropTypes.bool,
    searchSernosSequences: PropTypes.func.isRequired,
    clearSernosSequencesSearch: PropTypes.func.isRequired,
    decrementRules: PropTypes.arrayOf(PropTypes.shape({})),
    assemblyTechnologies: PropTypes.arrayOf(PropTypes.shape({})),
    partNumber: PropTypes.string
};

BuildTab.defaultProps = {
    linnProduced: null,
    decrementRuleName: null,
    assemblyTechnologyName: null,
    bomType: null,
    bomId: null,
    bomVerifyFreqWeeks: null,
    optionSet: null,
    drawingReference: null,
    safetyCriticalPart: null,
    plannedSurplus: null,
    sernosSequenceName: null,
    sernosSequenceDescription: null,
    sernosSequencesSearchResults: [],
    sernosSequencesSearchLoading: false,
    decrementRules: [],
    assemblyTechnologies: [],
    partNumber: null
};

export default BuildTab;
