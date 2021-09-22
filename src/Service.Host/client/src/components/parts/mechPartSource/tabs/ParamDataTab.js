import React from 'react';
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
    capacitorNegativeTolerance,
    capacitorDielectric,
    capacitorPackageValues,
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
    transistorDeviceName,
    transistorPolarity,
    transistorVoltage,
    transistorCurrent,
    icType,
    icFunction,
    resistorConstructionValues,
    resistorPackageValues,
    capacitorDielectricValues,
    capacitorLength,
    transistorPackageValues,
    transistorPolarityValues,
    icPackageValues,
    creating
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
    const resistorTolerances = [0.1, 1, 2, 5, 10, 20, 50, 80];
    const divide = (a, b) => (!a || !b ? null : new Decimal(a).dividedBy(new Decimal(b)));
    const multiply = (a, b) => (!a || !b ? null : new Decimal(a).times(new Decimal(b)));

    switch (partType) {
        case 'RES':
            return (
                <Grid container spacing={3}>
                    <Grid item xs={3}>
                        <InputField
                            value={
                                resistance === 0
                                    ? 0.0
                                    : divide(resistance, ohmUnitMultipliers[resistanceUnits])
                            }
                            propertyName="resistance"
                            label="Resistance"
                            onChange={(propertyName, newValue) => {
                                if (newValue === 0) {
                                    handleFieldChange(propertyName, 0.0);
                                } else {
                                    handleFieldChange(
                                        propertyName,
                                        multiply(newValue, ohmUnitMultipliers[resistanceUnits])
                                    );
                                }
                            }}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <Dropdown
                            items={Object.keys(ohmUnitMultipliers)}
                            value={resistanceUnits}
                            label="units"
                            disabled={!creating()}
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
                            adornment="Ω"
                            onChange={() => {}}
                            disabled
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <Dropdown
                            items={resistorTolerances}
                            label="Tolerance"
                            value={resistorTolerance}
                            propertyName="resistorTolerance"
                            disabled={!creating()}
                            onChange={handleFieldChange}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <Dropdown
                            items={resistorConstructionValues?.map(v => ({
                                id: v.value,
                                displayText: v.description
                            }))}
                            label="Construction"
                            value={construction}
                            propertyName="construction"
                            disabled={!creating()}
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
                            disabled={!creating()}
                            onChange={handleFieldChange}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <InputField
                            value={resistorLength}
                            propertyName="resistorLength"
                            label="Length (mm)"
                            onChange={handleFieldChange}
                            disabled={!creating()}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={resistorWidth}
                            propertyName="resistorWidth"
                            label="Width (mm)"
                            onChange={handleFieldChange}
                            disabled={!creating()}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={resistorHeight}
                            propertyName="resistorHeight"
                            label="Height (mm)"
                            onChange={handleFieldChange}
                            disabled={!creating()}
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
                            disabled={!creating()}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={resistorPowerRating}
                            propertyName="resistorPowerRating"
                            label="Power Rating (W)"
                            onChange={handleFieldChange}
                            disabled={!creating()}
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
                            disabled={!creating()}
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
                            value={
                                capacitance === 0
                                    ? 0.0
                                    : divide(capacitance, faradUnitMultipliers[capacitanceUnits])
                            }
                            propertyName="capacitance"
                            label="Capacitance"
                            onChange={(propertyName, newValue) => {
                                handleFieldChange(
                                    propertyName,
                                    multiply(newValue, faradUnitMultipliers[capacitanceUnits])
                                );
                            }}
                            type="number"
                            disabled={!creating()}
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
                    <Grid item xs={3} />

                    <Grid item xs={3}>
                        <InputField
                            value={capacitorVoltageRating}
                            propertyName="capacitorVoltageRating"
                            label="Max Voltage (V)"
                            onChange={handleFieldChange}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={capacitorPositiveTolerance}
                            propertyName="capacitorPositiveTolerance"
                            label="+ve Tolerance"
                            onChange={handleFieldChange}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={capacitorNegativeTolerance}
                            propertyName="capacitorNegativeTolerance"
                            label="-ve Tolerance"
                            onChange={handleFieldChange}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={3} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={capacitorDielectricValues?.map(v => ({
                                id: v.value,
                                displayText: v.description
                            }))}
                            label="Dielectric"
                            value={capacitorDielectric}
                            propertyName="capacitorDielectric"
                            onChange={handleFieldChange}
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={capacitorPackageValues?.map(v => ({
                                id: v.value,
                                displayText: v?.description
                                    ? `${v.value} - ${v.description}`
                                    : v.value
                            }))}
                            label="Package"
                            value={packageName}
                            propertyName="packageName"
                            onChange={handleFieldChange}
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <InputField
                            value={capacitorPitch}
                            propertyName="capacitorPitch"
                            label="Pitch (mm)"
                            onChange={handleFieldChange}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={capacitorLength}
                            propertyName="capacitorLength"
                            label="Length (mm)"
                            onChange={handleFieldChange}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={capacitorWidth}
                            propertyName="capacitorWidth"
                            label="Width (mm)"
                            onChange={handleFieldChange}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={capacitorHeight}
                            propertyName="capacitorHeight"
                            label="Height (mm)"
                            onChange={handleFieldChange}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={capacitorDiameter}
                            propertyName="capacitorDiameter"
                            label="Diameter (mm)"
                            onChange={handleFieldChange}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={capacitorRippleCurrent}
                            propertyName="capacitorRippleCurrent"
                            label="Ripple Current (mA RMS)"
                            onChange={handleFieldChange}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={6} />
                </Grid>
            );
        case 'TRAN':
            return (
                <Grid container spacing={3}>
                    <Grid item xs={3}>
                        <InputField
                            value={transistorDeviceName}
                            propertyName="transistorDeviceName"
                            label="Device/Type"
                            onChange={handleFieldChange}
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={transistorPolarityValues?.map(v => ({
                                id: v.value,
                                displayText: v.description ? v.description : v.value
                            }))}
                            label="Dielectric"
                            value={transistorPolarity}
                            allowNoValue
                            propertyName="transistorPolarity"
                            onChange={handleFieldChange}
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <InputField
                            value={transistorVoltage}
                            propertyName="transistorVoltage"
                            label="Voltage (V)"
                            onChange={handleFieldChange}
                            type="number"
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            value={transistorCurrent}
                            propertyName="transistorCurrent"
                            label="Current (A)"
                            onChange={handleFieldChange}
                            type="number"
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={6} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={transistorPackageValues?.map(v => ({
                                id: v.value,
                                displayText: v?.description
                                    ? `${v.value} - ${v.description}`
                                    : v.value
                            }))}
                            label="Package"
                            allowNoValue
                            value={packageName}
                            propertyName="packageName"
                            onChange={handleFieldChange}
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={9} />
                </Grid>
            );
        case 'IC':
            return (
                <Grid container spacing={3}>
                    <Grid item xs={3}>
                        <InputField
                            value={icType}
                            propertyName="icType"
                            label="Type"
                            onChange={handleFieldChange}
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <InputField
                            value={icFunction}
                            propertyName="icFunction"
                            label="Function"
                            onChange={handleFieldChange}
                            disabled={!creating()}
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={3}>
                        <Dropdown
                            items={icPackageValues?.map(v => ({
                                id: v.value,
                                displayText: v?.description
                                    ? `${v.value} - ${v.description}`
                                    : v.value
                            }))}
                            label="Package"
                            allowNoValue
                            value={packageName}
                            propertyName="packageName"
                            onChange={handleFieldChange}
                            disabled={!creating()}
                        />
                    </Grid>

                    <Grid item xs={9} />
                </Grid>
            );
        default:
            return (
                <Grid item xs={12}>
                    No Part Type Selected.
                </Grid>
            );
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
    capacitorNegativeTolerance: PropTypes.number,
    capacitorDielectric: PropTypes.number,
    packageName: PropTypes.string,
    capacitorPitch: PropTypes.number,
    capacitorWidth: PropTypes.number,
    capacitorHeight: PropTypes.number,
    capacitorDiameter: PropTypes.number,
    capacitanceUnits: PropTypes.string,
    resistorTolerance: PropTypes.number,
    construction: PropTypes.string,
    resistorLength: PropTypes.number,
    resistorWidth: PropTypes.number,
    resistorHeight: PropTypes.number,
    resistorPowerRating: PropTypes.number,
    resistorVoltageRating: PropTypes.number,
    temperatureCoefficient: PropTypes.number,
    transistorDeviceName: PropTypes.string,
    transistorPolarity: PropTypes.string,
    transistorVoltage: PropTypes.number,
    transistorCurrent: PropTypes.number,
    icType: PropTypes.string,
    icFunction: PropTypes.string,
    resistorPackageValues: PropTypes.arrayOf(PropTypes.shape({})),
    capacitorDielectricValues: PropTypes.arrayOf(PropTypes.shape({})),
    capacitorLength: PropTypes.number,
    transistorPackageValues: PropTypes.arrayOf(PropTypes.shape({})),
    transistorPolarityValues: PropTypes.arrayOf(PropTypes.shape({})),
    icPackageValues: PropTypes.arrayOf(PropTypes.shape({})),
    capacitorPackageValues: PropTypes.arrayOf(PropTypes.shape({})),
    resistorConstructionValues: PropTypes.arrayOf(PropTypes.shape({})),
    creating: PropTypes.func.isRequired
};

ParamDataTab.defaultProps = {
    resistance: null,
    resistanceUnits: 'Ω',
    capacitorRippleCurrent: null,
    capacitance: null,
    capacitorVoltageRating: null,
    capacitorPositiveTolerance: null,
    capacitorNegativeTolerance: null,
    capacitorDielectric: null,
    packageName: null,
    capacitorPitch: null,
    capacitorWidth: null,
    capacitorHeight: null,
    capacitorDiameter: null,
    capacitanceUnits: 'uF',
    resistorTolerance: null,
    construction: null,
    resistorLength: null,
    resistorWidth: null,
    resistorHeight: null,
    resistorPowerRating: null,
    resistorVoltageRating: null,
    temperatureCoefficient: null,
    transistorDeviceName: null,
    transistorPolarity: null,
    transistorVoltage: null,
    transistorCurrent: null,
    icType: null,
    icFunction: null,
    resistorPackageValues: null,
    capacitorDielectricValues: null,
    capacitorLength: null,
    transistorPackageValues: null,
    transistorPolarityValues: null,
    icPackageValues: null,
    partType: null,
    resistorConstructionValues: [],
    capacitorPackageValues: []
};
export default ParamDataTab;
