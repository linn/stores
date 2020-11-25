import React, { useState } from 'react';
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
    capacitorVoltageRating,
    capacitorPositiveTolerance,
    capacitorDialectric,
    packageName,
    capacitorPitch,
    capacitorWidth,
    capacitorHeight,
    capacitorDiameter,
    capacitanceUnit,
    resistanceUnit,
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
    footPrintRef
}) {
    const ohmUnitMultipliers = {
        KΩ: 1000,
        MΩ: 1000000,
        Ω: 1
    };
    const faradUnitMultipliers = {
        uF: 0.000001,
        nF: 0.000000001,
        pF: 0.000000000001
    };
    if (!partType) {
        return <> No Part Type Selected. </>;
    }
    if (partType === 'RES') {
        return (
            <Grid container spacing={3}>
                <Grid item xs={3}>
                    <InputField
                        value={resistance / ohmUnitMultipliers[resistanceUnits]}
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
                    <InputField
                        value={resistorVoltageRating}
                        propertyName="resistorVoltageRating"
                        label="Voltage Rating (V)"
                        onChange={handleFieldChange}
                        type="number"
                    />
                </Grid>
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
    capacitorDialectric: PropTypes.number,
    packageName: PropTypes.string,
    capacitorPitch: PropTypes.number,
    capacitorWidth: PropTypes.number,
    capacitorHeight: PropTypes.number,
    capacitorDiameter: PropTypes.number,
    capacitanceUnit: PropTypes.number,
    resistanceUnit: PropTypes.number,
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
    footPrintRef: PropTypes.string
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
    capacitanceUnit: null,
    resistanceUnit: null,
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
    partType: null
};
export default ParamDataTab;
