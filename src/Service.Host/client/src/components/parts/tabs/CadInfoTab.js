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
    footprintRef3
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
                    helperText="Select a library to make this part appear in the corresponding library in Altium (if the part is LIVE)"
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
    partLibraries: PropTypes.arrayOf(PropTypes.shape({}))
};

CadInfoTab.defaultProps = {
    partLibraries: [],
    libraryName: null,
    libraryRef: null,
    footprintRef1: null,
    footprintRef2: null,
    footprintRef3: null
};

export default CadInfoTab;
