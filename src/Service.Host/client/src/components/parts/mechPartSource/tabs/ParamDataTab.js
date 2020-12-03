import React, { useState } from 'react';
import { Decimal } from 'decimal.js';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { Dropdown, InputField } from '@linn-it/linn-form-components-library';

function ParamDataTab({
    resistance,
    resistanceUnits,
    handleFieldChange,
    partType,
    capacitorRippleCurrent,
    capacitance,
    capacitanceUnits,
    capacitorVoltageRating,
    capacitorPositiveTolerance,
    capacitorDialectric,
    packageName,
    capacitorPitch,
    capacitorWidth,
    capacitorHeight,
    capacitorDiameter,
    resistorTolerance,
    construction,
    resistorLength,
    resistorWidth,
    resistorHeight,
    resistorPowerRating,
    resistorVoltageRating,
    temperatureCoefficient,
    transistorType,
    transistorPolarity,
    transistorVoltage,
    transistorCurrent,
    icType,
    icFunction,
    libraryRef,
    footPrintRef,
    resistorConstructionValues,
    resistorPackageValues
}) {
    const ohmUnitMultipliers = {
        KΩ: 1000,
        MΩ: 1000000,
        Ω: 1
    };
    const faradUnitMultipliers = {
        F: 1,
        uF: 0.000001,
        nF: 0.000000001,
        pF: 0.000000000001
    };
    const resistorTemperatureCoefficients = [25, 50, 75, 100, 250, 500, 999];
    const divide = (a, b) => new Decimal(a).dividedBy(new Decimal(b));
    switch (partType) {
        case 'RES':
            return (
                <Grid container spacing={3}>
                    <Grid item xs={3}>
                        <InputField
                            value={divide(resistance, ohmUnitMultipliers[resistanceUnits])}
                            propertyName="resistance"
                            label="Resistance"
                            onChange={(propertyName, newValue) => {
                                handleFieldChange(
                                    propertyName,
                                    newValue * ohmUnitMultipliers[resistanceUnits]
                                );
                            }}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <Dropdown
                            items={Object.keys(ohmUnitMultipliers)}
                            value={resistanceUnits}
                            label="units"
                            propertyName="resistanceUnits"
                            onChange={handleFieldChange}
                            allowNoValue={false}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={resistance}
                            propertyName="resistance"
                            label="Resistance ACTUAL"
                            onChange={() => {}}
                            disabled
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={resistorConstructionValues?.map(v => ({
                                id: v.value,
                                displayText: v.description
                            }))}
                            label="Construction"
                            value={construction}
                            propertyName="construction"
                            onChange={handleFieldChange}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={resistorPackageValues?.map(v => ({
                                id: v.value,
                                displayText: v?.description
                                    ? `${v.value} - ${v.description}`
                                    : v.value
                            }))}
                            label="Package"
                            value={packageName}
                            propertyName="packageName"
                            onChange={handleFieldChange}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <InputField
                            value={resistorLength}
                            propertyName="resistorLength"
                            label="Length"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={resistorWidth}
                            propertyName="resistorWidth"
                            label="rWidth"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={resistorHeight}
                            propertyName="resistorHeight"
                            label="Height"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3} />
                    <Grid item xs={3}>
                        <InputField
                            value={resistorVoltageRating}
                            propertyName="resistorVoltageRating"
                            label="Voltage Rating (V)"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={resistorPowerRating}
                            propertyName="resistorPowerRating"
                            label="Power Rating (W)"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={6} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={resistorTemperatureCoefficients}
                            label="Temp Coeff"
                            value={temperatureCoefficient}
                            propertyName="temperatureCoefficient"
                            onChange={handleFieldChange}
                        />
                    </Grid>
                    <Grid item xs={9} />
                </Grid>
            );
        case 'CAP':
            return (
                <Grid container spacing={3}>
                    <Grid item xs={3}>
                        <InputField
                            value={divide(capacitance, faradUnitMultipliers[capacitanceUnits])}
                            propertyName="capacitance"
                            label="Capacitance"
                            onChange={(propertyName, newValue) => {
                                handleFieldChange(
                                    propertyName,
                                    newValue * faradUnitMultipliers[capacitanceUnits]
                                );
                            }}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <Dropdown
                            items={Object.keys(faradUnitMultipliers)}
                            value={capacitanceUnits}
                            label="units"
                            propertyName="capacitanceUnits"
                            onChange={handleFieldChange}
                            allowNoValue={false}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={capacitance}
                            propertyName="capacitance"
                            label="Capacitance ACTUAL"
                            onChange={() => {}}
                            disabled
                            type="number"
                        />
                    </Grid>
                    {/* <Grid item xs={3} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={resistorConstructionValues?.map(v => ({
                                id: v.value,
                                displayText: v.description
                            }))}
                            label="Construction"
                            value={construction}
                            propertyName="construction"
                            onChange={handleFieldChange}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={resistorPackageValues?.map(v => ({
                                id: v.value,
                                displayText: v?.description
                                    ? `${v.value} - ${v.description}`
                                    : v.value
                            }))}
                            label="Package"
                            value={packageName}
                            propertyName="packageName"
                            onChange={handleFieldChange}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <InputField
                            value={resistorLength}
                            propertyName="resistorLength"
                            label="Length"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={resistorWidth}
                            propertyName="resistorWidth"
                            label="rWidth"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={resistorHeight}
                            propertyName="resistorHeight"
                            label="Height"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3} />
                    <Grid item xs={3}>
                        <InputField
                            value={resistorVoltageRating}
                            propertyName="resistorVoltageRating"
                            label="Voltage Rating (V)"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={resistorPowerRating}
                            propertyName="resistorPowerRating"
                            label="Power Rating (W)"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={6} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={resistorTemperatureCoefficients}
                            label="Temp Coeff"
                            value={temperatureCoefficient}
                            propertyName="temperatureCoefficient"
                            onChange={handleFieldChange}
                        />
                    </Grid>
                    <Grid item xs={9} /> */}
                </Grid>
            );
        default:
            return <> No Part Type Selected. </>;
    }
}

ParamDataTab.propTypes = {
    partType: PropTypes.string,
    resistance: PropTypes.number,
    handleFieldChange: PropTypes.func.isRequired,
    resistanceUnits: PropTypes.string,
    capacitorRippleCurrent: PropTypes.number,
    capacitance: PropTypes.number,
    capacitorVoltageRating: PropTypes.number,
    capacitorPositiveTolerance: PropTypes.number,
    capacitorDialectric: PropTypes.number,
    packageName: PropTypes.string,
    capacitorPitch: PropTypes.number,
    capacitorWidth: PropTypes.number,
    capacitorHeight: PropTypes.number,
    capacitorDiameter: PropTypes.number,
    resistorTolerance: PropTypes.number,
    construction: PropTypes.string,
    resistorLength: PropTypes.number,
    resistorWidth: PropTypes.number,
    resistorHeight: PropTypes.number,
    resistorPowerRating: PropTypes.number,
    resistorVoltageRating: PropTypes.number,
    temperatureCoefficient: PropTypes.number,
    transistorType: PropTypes.string,
    transistorPolarity: PropTypes.string,
    transistorVoltage: PropTypes.number,
    transistorCurrent: PropTypes.number,
    icType: PropTypes.string,
    icFunction: PropTypes.string,
    libraryRef: PropTypes.string,
    footPrintRef: PropTypes.string,
    resistorConstructionValues: PropTypes.arrayOf(PropTypes.shape({}))
};

ParamDataTab.defaultProps = {
    resistance: null,
    resistanceUnits: null, // ohms
    capacitorRippleCurrent: null,
    capacitance: null,
    capacitorVoltageRating: null,
    capacitorPositiveTolerance: null,
    capacitorDialectric: null,
    packageName: null,
    capacitorPitch: null,
    capacitorWidth: null,
    capacitorHeight: null,
    capacitorDiameter: null,
    capacitanceUnits: null,
    resistorTolerance: null,
    construction: null,
    resistorLength: null,
    resistorWidth: null,
    resistorHeight: null,
    resistorPowerRating: null,
    resistorVoltageRating: null,
    temperatureCoefficient: null,
    transistorType: null,
    transistorPolarity: null,
    transistorVoltage: null,
    transistorCurrent: null,
    icType: null,
    icFunction: null,
    libraryRef: null,
    footPrintRef: null,
    partType: null,
    resistorConstructionValues: []
};
export default ParamDataTab;
