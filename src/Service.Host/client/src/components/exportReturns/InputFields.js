import React from 'react';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { InputField } from '@linn-it/linn-form-components-library';

export default function InputFields({ exportReturn, handleFieldChange, calculateDims }) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={exportReturn.carrierCode}
                    label="Carrier Code"
                    propertyName="carrierCode"
                    onChange={handleFieldChange}
                    margin="dense"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={exportReturn.carrierRef}
                    label="Carrier Reference"
                    propertyName="carrierRef"
                    onChange={handleFieldChange}
                    margin="dense"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={exportReturn.numPallets}
                    label="Num Pallets"
                    propertyName="numPallets"
                    onChange={handleFieldChange}
                    margin="dense"
                    type="number"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={exportReturn.numCartons}
                    label="Num Cartons"
                    propertyName="numCartons"
                    onChange={handleFieldChange}
                    margin="dense"
                    type="number"
                />
            </Grid>

            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={exportReturn.grossWeightKg}
                    label="Gross Weight"
                    propertyName="grossWeightKg"
                    onChange={handleFieldChange}
                    margin="dense"
                    type="number"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={exportReturn.grossDimsM3}
                    label="Gross Dims"
                    propertyName="grossDims"
                    onChange={handleFieldChange}
                    margin="dense"
                    type="number"
                />
            </Grid>

            <Grid item xs={12}>
                <Button variant="outlined" color="primary" onClick={() => calculateDims()}>
                    Calculate Dimensions from RSNs
                </Button>
            </Grid>
        </Grid>
    );
}

InputFields.propTypes = {
    exportReturn: PropTypes.shape({
        carrierCode: PropTypes.string,
        carrierRef: PropTypes.string,
        numPallets: PropTypes.number,
        numCartons: PropTypes.number,
        grossWeightKg: PropTypes.number,
        grossDimsM3: PropTypes.number
    }).isRequired,
    calculateDims: PropTypes.func.isRequired,
    handleFieldChange: PropTypes.func.isRequired
};
