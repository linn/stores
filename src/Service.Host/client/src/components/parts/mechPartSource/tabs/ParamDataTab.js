import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { Dropdown, InputField } from '@linn-it/linn-form-components-library';

function ParamDataTab({ paramData, handleFieldChange }) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={3}>
                <InputField
                    value={paramData?.resistance}
                    propertyName="resistance"
                    label="Resistance"
                    onChange={handleFieldChange}
                    type="number"
                />
                <Dropdown />
            </Grid>
        </Grid>
    );
}

ParamDataTab.propTypes = {
    paramData: PropTypes.shape({ resistance: PropTypes.number }),
    handleFieldChange: PropTypes.func.isRequired
};

ParamDataTab.defaultProps = {
    paramData: { resistance: null }
};
export default ParamDataTab;
