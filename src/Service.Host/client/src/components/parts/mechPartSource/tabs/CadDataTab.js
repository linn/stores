import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField } from '@linn-it/linn-form-components-library';

export default function CadDataTab({ handleFieldChange, libraryRef, footprintRef }) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={4}>
                <InputField
                    label="Library Ref"
                    value={libraryRef}
                    propertyName="libraryRef"
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={4}>
                <InputField
                    label="Footprint Ref"
                    value={footprintRef}
                    propertyName="footprintRef"
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8} />
        </Grid>
    );
}

CadDataTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    libraryRef: PropTypes.string,
    footprintRef: PropTypes.string
};

CadDataTab.defaultProps = {
    libraryRef: null,
    footprintRef: null
};
