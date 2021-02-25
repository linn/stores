import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown } from '@linn-it/linn-form-components-library';

function StoresTab({
    handleFieldChange,
    qcOnReceipt,
    qcInformation,
    ourInspectionWeeks,
    safetyWeeks,
    railMethod,
    minStockRail,
    maxStockRail,
    secondStageBoard,
    secondStageDescription,
    tqmsCategoryOverride,
    stockNotes,
    tqmsCategories
}) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={4}>
                <Dropdown
                    label="QC On Receipt"
                    propertyName="qcOnReceipt"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue={false}
                    value={qcOnReceipt}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8}>
                <InputField
                    fullWidth
                    value={qcInformation}
                    label="QC Info"
                    onChange={handleFieldChange}
                    propertyName="qcInformation"
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={ourInspectionWeeks}
                    type="number"
                    label="Our Inspection Weeks"
                    onChange={handleFieldChange}
                    propertyName="ourInspectionWeeks"
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={safetyWeeks}
                    type="number"
                    label="Safety Weeks"
                    onChange={handleFieldChange}
                    propertyName="safetyWeeks"
                />
            </Grid>
            <Grid item xs={4} />
            <Grid item xs={4}>
                <Dropdown
                    label="Rail Method"
                    propertyName="railMethod"
                    items={['MR9', 'SMM', 'POLICY', 'FIXED', 'OVERRIDE']}
                    fullWidth
                    allowNoValue
                    value={railMethod}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={minStockRail}
                    type="number"
                    label="Min Stock Rail"
                    onChange={handleFieldChange}
                    propertyName="minStockRail"
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={maxStockRail}
                    type="number"
                    label="Max Stock Rail"
                    onChange={handleFieldChange}
                    propertyName="maxStockRail"
                />
            </Grid>
            <Grid item xs={4}>
                <Dropdown
                    label="Second Stage Board"
                    propertyName="secondStageBoard"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue
                    value={secondStageBoard}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={secondStageDescription}
                    label="Second Stage Description"
                    onChange={handleFieldChange}
                    propertyName="secondStageDescription"
                />
            </Grid>
            <Grid item xs={4}>
                <Dropdown
                    label="TQMS Override"
                    propertyName="tqmsCategoryOverride"
                    items={tqmsCategories.map(c => ({ id: c.name, displayText: c.description }))}
                    fullWidth
                    allowNoValue
                    value={tqmsCategoryOverride}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={stockNotes}
                    label="Stock Notes"
                    required={!!tqmsCategoryOverride}
                    helperText={
                        tqmsCategoryOverride ? 'You must provide a reason to set an override' : ''
                    }
                    onChange={handleFieldChange}
                    propertyName="stockNotes"
                    rows={4}
                />
            </Grid>
        </Grid>
    );
}

StoresTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    qcOnReceipt: PropTypes.string,
    qcInformation: PropTypes.string,
    ourInspectionWeeks: PropTypes.number,
    safetyWeeks: PropTypes.number,
    railMethod: PropTypes.string,
    minStockRail: PropTypes.number,
    maxStockRail: PropTypes.number,
    secondStageBoard: PropTypes.string,
    secondStageDescription: PropTypes.string,
    tqmsCategoryOverride: PropTypes.string,
    stockNotes: PropTypes.string,
    tqmsCategories: PropTypes.arrayOf(
        PropTypes.shape({ name: PropTypes.string, description: PropTypes.string })
    )
};

StoresTab.defaultProps = {
    qcOnReceipt: null,
    qcInformation: null,
    ourInspectionWeeks: null,
    safetyWeeks: null,
    railMethod: null,
    minStockRail: null,
    maxStockRail: null,
    secondStageBoard: null,
    secondStageDescription: null,
    tqmsCategoryOverride: null,
    stockNotes: null,
    tqmsCategories: []
};

export default StoresTab;
