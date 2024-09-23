import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown } from '@linn-it/linn-form-components-library';

export default function CadDataTab({
    handleFieldChange,
    partLibraries,
    libraryName,
    libraryRef,
    footprintRef,
    footprintRef2,
    footprintRef3,
    partLibraryRefs
}) {
    const [libraryRefOption, setLibraryRefOption] = useState();
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
                    label="Library Ref"
                    value={libraryRef}
                    propertyName="libraryRef"
                    fullWidth
                    helperText="enter a new value, or pick one from the dropdown to the right"
                    onChange={handleFieldChange}
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
                    label="Footprint Ref"
                    value={footprintRef}
                    propertyName="footprintRef"
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={6}>
                <InputField
                    label="Footprint Ref 2"
                    value={footprintRef2}
                    propertyName="footprintRef2"
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={6}>
                <InputField
                    label="Footprint Ref 3"
                    value={footprintRef3}
                    propertyName="footprintRef3"
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={6} />
        </Grid>
    );
}

CadDataTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    libraryRef: PropTypes.string,
    libraryName: PropTypes.string,
    footprintRef: PropTypes.string,
    footprintRef2: PropTypes.string,
    footprintRef3: PropTypes.string,
    partLibraries: PropTypes.arrayOf(PropTypes.shape({})),
    partLibraryRefs: PropTypes.arrayOf(PropTypes.shape({}))
};

CadDataTab.defaultProps = {
    libraryRef: null,
    libraryName: null,
    footprintRef: null,
    footprintRef2: null,
    footprintRef3: null,
    partLibraries: [],
    partLibraryRefs: []
};
