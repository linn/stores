import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown } from '@linn-it/linn-form-components-library';
import Typography from '@material-ui/core/Typography';

function CadInfoTab({
    handleFieldChange,
    partLibraries,
    libraryName,
    libraryRef,
    footprintRef1,
    footprintRef2,
    footprintRef3,
    theirPartNumber,
    datasheetPath,
    altiumType,
    altiumValue,
    altiumValueRkm,
    construction,
    temperatureCoefficient,
    resistorTolerance,
    device,
    dielectric,
    capNegativeTolerance,
    capPositiveTolerance,
    capVoltageRating,
    frequency,
    frequencyLabel,
    partLibraryRefs,
    footprintRefOptions
}) {
    const [libraryRefOption, setLibraryRefOption] = useState();
    const [footprintRefOption, setFootPrintRefOption] = useState();

    return (
        <Grid container spacing={3}>
            <Grid item xs={12}>
                <Typography variant="subtitle1">
                    The information on this tab informs the values that propogate through to the
                    libraries for use in Altium. The fields will differ depending on the Library
                    Name, which ultimately decides which library this part will appear in
                </Typography>
            </Grid>
            <Grid item xs={6}>
                <Dropdown
                    label="Library Name"
                    propertyName="libraryName"
                    items={partLibraries?.map(l => l.libraryName)}
                    fullWidth
                    allowNoValue
                    value={libraryName}
                    helperText="Select a library to make this part appear in the corresponding library in Altium (if the part has not been phased out)"
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />

            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={libraryRef}
                    label="Library Ref"
                    onChange={handleFieldChange}
                    helperText="enter a new value, or pick one from the dropdown to the right"
                    propertyName="libraryRef"
                />
            </Grid>
            <Grid item xs={6}>
                {libraryName && (
                    <Dropdown
                        label="Library Ref options"
                        propertyName="libraryRefOption"
                        items={partLibraryRefs
                            ?.filter(x => x.libraryName === libraryName || x.libraryName === 'All')
                            .map(l => l.libraryRef)}
                        fullWidth
                        allowNoValue
                        value={libraryRefOption}
                        onChange={(_, newValue) => {
                            handleFieldChange('libraryRef', newValue);
                            setLibraryRefOption(newValue);
                        }}
                    />
                )}
            </Grid>

            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={footprintRef1}
                    label="Footprint Ref 1"
                    onChange={handleFieldChange}
                    propertyName="footprintRef1"
                />
            </Grid>
            <Grid item xs={6}>
                {libraryName && (
                    <Dropdown
                        label="Footprint Ref options"
                        propertyName="footprintRefOption"
                        items={footprintRefOptions
                            ?.filter(x => x.libraryName === libraryName || x.libraryName === 'All')
                            .map(l => `${l.ref1},${l.ref2},${l.ref3}`)}
                        fullWidth
                        allowNoValue
                        helperText="You can enter your own values for footprint refs, or just pick a default from the list"
                        value={footprintRefOption}
                        onChange={(_, newValue) => {
                            const parts = newValue.split(',');

                            handleFieldChange('footprintRef1', parts[0]);
                            handleFieldChange('footprintRef2', parts[1]);
                            handleFieldChange('footprintRef3', parts[2]);

                            setFootPrintRefOption(newValue);
                        }}
                    />
                )}
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={footprintRef2}
                    label="Footprint Ref 2"
                    onChange={handleFieldChange}
                    propertyName="footprintRef2"
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={footprintRef3}
                    label="Footprint Ref 3"
                    onChange={handleFieldChange}
                    propertyName="footprintRef3"
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={theirPartNumber}
                    label="Their Part Number"
                    onChange={handleFieldChange}
                    propertyName="theirPartNumber"
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={12}>
                <InputField
                    fullWidth
                    value={datasheetPath}
                    label="Datasheet Path"
                    onChange={handleFieldChange}
                    propertyName="datasheetPath"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={altiumType}
                    label="Type"
                    onChange={handleFieldChange}
                    propertyName="altiumType"
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={device}
                    label="Device"
                    onChange={handleFieldChange}
                    propertyName="device"
                />
            </Grid>
            <Grid item xs={6} />
            {libraryName === 'Resistors' && (
                <>
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={resistorTolerance}
                            label="Tolerance"
                            onChange={handleFieldChange}
                            propertyName="resistorTolerance"
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={construction}
                            label="Construction"
                            onChange={handleFieldChange}
                            propertyName="construction"
                        />
                    </Grid>
                </>
            )}
            {(libraryName === 'Resistors' || libraryName === 'Capacitors') && (
                <>
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={temperatureCoefficient}
                            label="Temp Coeff"
                            onChange={handleFieldChange}
                            propertyName="temperatureCoefficient"
                        />
                    </Grid>
                    <Grid item xs={6} />
                </>
            )}
            {libraryName === 'Capacitors' && (
                <>
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={dielectric}
                            label="Dielectric"
                            onChange={handleFieldChange}
                            propertyName="dielectric"
                        />
                    </Grid>
                    <Grid item xs={6} />
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={capVoltageRating}
                            label="Voltage Rating"
                            onChange={handleFieldChange}
                            propertyName="capVoltageRating"
                        />
                    </Grid>
                    <Grid item xs={6} />
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={capPositiveTolerance}
                            label="Positive Tolerance"
                            onChange={handleFieldChange}
                            propertyName="capPositiveTolerance"
                        />
                    </Grid>
                    <Grid item xs={6} />
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={capNegativeTolerance}
                            label="Negative Tolerance"
                            onChange={handleFieldChange}
                            propertyName="capNegativeTolerance"
                        />
                    </Grid>
                    <Grid item xs={6} />
                </>
            )}
            {libraryName === 'Crystals' && (
                <>
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={frequency}
                            label="Frequency"
                            onChange={handleFieldChange}
                            propertyName="frequency"
                        />
                    </Grid>
                    <Grid item xs={6} />
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={frequencyLabel}
                            label="Frequency Label"
                            onChange={handleFieldChange}
                            propertyName="frequencyLabel"
                        />
                    </Grid>
                    <Grid item xs={6} />
                </>
            )}
            {libraryName === 'Resistors' ||
            libraryName === 'Capacitors' ||
            libraryName === 'Inductors & Transformers' ? (
                <>
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={altiumValue}
                            label="Search Value"
                            onChange={handleFieldChange}
                            propertyName="altiumValue"
                        />
                    </Grid>
                    <Grid item xs={6} />
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={altiumValueRkm}
                            label="Value"
                            onChange={handleFieldChange}
                            propertyName="altiumValueRkm"
                        />
                    </Grid>
                    <Grid item xs={6} />
                </>
            ) : (
                <>
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={altiumValue}
                            label="Value"
                            onChange={handleFieldChange}
                            propertyName="altiumValue"
                        />
                    </Grid>
                    <Grid item xs={6} />
                </>
            )}
        </Grid>
    );
}

CadInfoTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    libraryName: PropTypes.string,
    libraryRef: PropTypes.string,
    footprintRef1: PropTypes.string,
    footprintRef2: PropTypes.string,
    footprintRef3: PropTypes.string,
    partLibraries: PropTypes.arrayOf(PropTypes.shape({})),
    theirPartNumber: PropTypes.string,
    datasheetPath: PropTypes.string,
    altiumType: PropTypes.string,
    altiumValue: PropTypes.string,
    altiumValueRkm: PropTypes.string,
    construction: PropTypes.string,
    temperatureCoefficient: PropTypes.string,
    resistorTolerance: PropTypes.string,
    device: PropTypes.string,
    dielectric: PropTypes.string,
    capNegativeTolerance: PropTypes.string,
    capPositiveTolerance: PropTypes.string,
    capVoltageRating: PropTypes.string,
    frequency: PropTypes.string,
    frequencyLabel: PropTypes.string,
    partLibraryRefs: PropTypes.arrayOf(PropTypes.shape({})),
    footprintRefOptions: PropTypes.arrayOf(PropTypes.shape({}))
};

CadInfoTab.defaultProps = {
    partLibraries: [],
    libraryName: null,
    libraryRef: null,
    footprintRef1: null,
    footprintRef2: null,
    footprintRef3: null,
    theirPartNumber: null,
    datasheetPath: null,
    altiumType: null,
    altiumValue: null,
    altiumValueRkm: null,
    construction: null,
    temperatureCoefficient: null,
    resistorTolerance: null,
    device: null,
    dielectric: null,
    capNegativeTolerance: null,
    capPositiveTolerance: null,
    capVoltageRating: null,
    frequency: null,
    frequencyLabel: null,
    partLibraryRefs: [],
    footprintRefOptions: []
};

export default CadInfoTab;
