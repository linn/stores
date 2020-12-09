import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, DatePicker, Dropdown } from '@linn-it/linn-form-components-library';

function QualityRequirementsTab({
    handleFieldChange,
    drawingsPackage,
    drawingsPackageAvailable,
    drawingsPackageDate,
    drawingFile,
    checklistCreated,
    checklistAvailable,
    checklistDate,
    packingRequired,
    packingAvailable,
    packingDate,
    productKnowledge,
    productKnowledgeAvailable,
    productKnowledgeDate,
    testEquipment,
    testEquipmentAvailable,
    testEquipmentDate,
    approvedReferenceStandards,
    approvedReferencesAvailable,
    approvedReferencesDate,
    processEvaluation,
    processEvaluationAvailable,
    processEvaluationDate
}) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={7}>
                <InputField
                    value={drawingsPackage}
                    propertyName="drawingsPackage"
                    fullWidth
                    onChange={handleFieldChange}
                    label="Drawings Package"
                />
            </Grid>
            <Grid item xs={2}>
                <Dropdown
                    label="Available"
                    propertyName="drawingsPackageAvailable"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={drawingsPackageAvailable}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={drawingsPackageDate}
                    onChange={value => {
                        handleFieldChange('drawingsPackageDate', value);
                    }}
                />
            </Grid>
            <Grid item xs={12}>
                <InputField
                    value={drawingFile}
                    propertyName="drawingFile"
                    fullWidth
                    onChange={handleFieldChange}
                    label="Drawing File"
                />
            </Grid>
            <Grid item xs={7}>
                <InputField
                    value={checklistCreated}
                    propertyName="checklistCreated"
                    fullWidth
                    onChange={handleFieldChange}
                    label="Checklist Created"
                />
            </Grid>
            <Grid item xs={2}>
                <Dropdown
                    label="Available"
                    propertyName="checklistAvailable"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={checklistAvailable}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={checklistDate}
                    onChange={value => {
                        handleFieldChange('checklistDate', value);
                    }}
                />
            </Grid>

            <Grid item xs={7}>
                <InputField
                    value={packingRequired}
                    propertyName="packingRequired"
                    fullWidth
                    onChange={handleFieldChange}
                    label="Packing Required"
                />
            </Grid>
            <Grid item xs={2}>
                <Dropdown
                    label="Available"
                    propertyName="packingAvailable"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={packingAvailable}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={packingDate}
                    onChange={value => {
                        handleFieldChange('packingDate', value);
                    }}
                />
            </Grid>

            <Grid item xs={7}>
                <InputField
                    value={productKnowledge}
                    propertyName="productKnowledge"
                    fullWidth
                    onChange={handleFieldChange}
                    label="Product Knowledge"
                />
            </Grid>
            <Grid item xs={2}>
                <Dropdown
                    label="Available"
                    propertyName="productKnowledgeAvailable"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={productKnowledgeAvailable}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={productKnowledgeDate}
                    onChange={value => {
                        handleFieldChange('productKnowledgeDate', value);
                    }}
                />
            </Grid>

            <Grid item xs={7}>
                <InputField
                    value={testEquipment}
                    propertyName="testEquipment"
                    fullWidth
                    onChange={handleFieldChange}
                    label="Test Equipment"
                />
            </Grid>
            <Grid item xs={2}>
                <Dropdown
                    label="Available"
                    propertyName="testEquipmentAvailable"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={testEquipmentAvailable}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={testEquipmentDate}
                    onChange={value => {
                        handleFieldChange('testEquipmentDate', value);
                    }}
                />
            </Grid>

            <Grid item xs={7}>
                <InputField
                    value={approvedReferenceStandards}
                    propertyName="approvedReferenceStandards"
                    fullWidth
                    onChange={handleFieldChange}
                    label="Approved Reference Standards"
                />
            </Grid>
            <Grid item xs={2}>
                <Dropdown
                    label="Available"
                    propertyName="approvedReferencesAvailable"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={approvedReferencesAvailable}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={approvedReferencesDate}
                    onChange={value => {
                        handleFieldChange('approvedReferencesDate', value);
                    }}
                />
            </Grid>

            <Grid item xs={7}>
                <InputField
                    value={processEvaluation}
                    propertyName="processEvaluation"
                    fullWidth
                    onChange={handleFieldChange}
                    label="Process Evaluation"
                />
            </Grid>
            <Grid item xs={2}>
                <Dropdown
                    label="Available"
                    propertyName="processEvaluationAvailable"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={processEvaluationAvailable}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={processEvaluationDate}
                    onChange={value => {
                        handleFieldChange('processEvaluationDate', value);
                    }}
                />
            </Grid>
        </Grid>
    );
}

QualityRequirementsTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    drawingsPackage: PropTypes.string,
    drawingsPackageAvailable: PropTypes.string,
    drawingsPackageDate: PropTypes.string,
    drawingFile: PropTypes.string,
    checklistCreated: PropTypes.string,
    checklistAvailable: PropTypes.string,
    checklistDate: PropTypes.string,
    packingRequired: PropTypes.string,
    packingAvailable: PropTypes.string,
    packingDate: PropTypes.string,
    productKnowledge: PropTypes.string,
    productKnowledgeAvailable: PropTypes.string,
    productKnowledgeDate: PropTypes.string,
    testEquipment: PropTypes.string,
    testEquipmentAvailable: PropTypes.string,
    testEquipmentDate: PropTypes.string,
    approvedReferenceStandards: PropTypes.string,
    approvedReferencesAvailable: PropTypes.string,
    approvedReferencesDate: PropTypes.string,
    processEvaluation: PropTypes.string,
    processEvaluationAvailable: PropTypes.string,
    processEvaluationDate: PropTypes.string
};

QualityRequirementsTab.defaultProps = {
    drawingsPackage: null,
    drawingsPackageAvailable: null,
    drawingsPackageDate: null,
    drawingFile: null,
    checklistCreated: null,
    checklistAvailable: null,
    checklistDate: null,
    packingRequired: null,
    packingAvailable: null,
    packingDate: null,
    productKnowledge: null,
    productKnowledgeAvailable: null,
    productKnowledgeDate: null,
    testEquipment: null,
    testEquipmentAvailable: null,
    testEquipmentDate: null,
    approvedReferenceStandards: null,
    approvedReferencesAvailable: null,
    approvedReferencesDate: null,
    processEvaluation: null,
    processEvaluationAvailable: null,
    processEvaluationDate: null
};

export default QualityRequirementsTab;
