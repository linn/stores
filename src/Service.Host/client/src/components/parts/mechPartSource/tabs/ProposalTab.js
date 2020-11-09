import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField } from '@linn-it/linn-form-components-library';

function ProposalTab({ handleFieldChange, notes }) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={4}>
                <InputField
                    onChange={handleFieldChange}
                    propertyName="notes"
                    value={notes}
                    label="Notes"
                    rows={4}
                />
            </Grid>
        </Grid>
    );
}

ProposalTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    notes: PropTypes.string
};

ProposalTab.defaultProps = {
    notes: null
};

export default ProposalTab;
