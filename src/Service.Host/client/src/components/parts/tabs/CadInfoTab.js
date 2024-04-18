import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown } from '@linn-it/linn-form-components-library';

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
    resistorTolerance
}) {
    return (
        <Grid container spacing={3}>
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
                    propertyName="libraryRef"
                />
            </Grid>
            <Grid item xs={6} />

            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={footprintRef1}
                    label="Footprint Ref 1"
                    onChange={handleFieldChange}
                    propertyName="footprintRef1"
                />
            </Grid>
            <Grid item xs={6} />
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
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={datasheetPath}
                    label="Datasheet Path"
                    onChange={handleFieldChange}
                    propertyName="datasheetPath"
                />
            </Grid>
            <Grid item xs={6} />
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
                    <Grid item xs={6} />
                </>
            )}
            {(libraryName === 'Resistors' || libraryName === 'Capacitors') && (
                <>
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={construction}
                            label="Construction"
                            onChange={handleFieldChange}
                            propertyName="construction"
                        />
                    </Grid>
                    <Grid item xs={6} />
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
            {libraryName === 'Resistors' ||
            libraryName === 'Capacitors' ||
            libraryName === 'Inductors & Transformers' ? (
                <>
                    <Grid item xs={6}>
                        <InputField
                            fullWidth
                            value={construction}
                            label="Construction"
                            onChange={handleFieldChange}
                            propertyName="construction"
                        />
                    </Grid>
                    <Grid item xs={6} />
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
    resistorTolerance: PropTypes.string
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
    resistorTolerance: null
};

export default CadInfoTab;
