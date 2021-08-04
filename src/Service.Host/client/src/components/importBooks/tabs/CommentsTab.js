import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown } from '@linn-it/linn-form-components-library';

function CommentsTab({
    handleFieldChange,
    comments,
    dateCancelled,
    cancelledBy,
    cancelledReason,
    employees
}) {
    return (
        <Grid container spacing={1} item xs={7}>
            <Grid item xs={12}>
                <InputField
                    label="Comments"
                    fullWidth
                    onChange={handleFieldChange}
                    propertyName="comments"
                    type="text"
                    rows={3}
                    value={comments}
                />
            </Grid>
            <Grid item xs={3}>
                <InputField
                    label="Date Cancelled"
                    fullWidth
                    onChange={handleFieldChange}
                    propertyName="dateCancelled"
                    type="date"
                    value={dateCancelled}
                    required
                />
            </Grid>
            <Grid item xs={3}>
                <Dropdown
                    items={employees.map(e => ({
                        displayText: `${e.fullName} (${e.id})`,
                        id: parseInt(e.id, 10)
                    }))}
                    propertyName="cancelledBy"
                    fullWidth
                    value={cancelledBy}
                    label="Cancelled by"
                    onChange={handleFieldChange}
                    type="number"
                />
            </Grid>

            <Grid item xs={12}>
                <InputField
                    label="Cancelled Reason"
                    fullWidth
                    onChange={handleFieldChange}
                    propertyName="cancelledReason"
                    type="text"
                    value={cancelledReason}
                />
            </Grid>
        </Grid>
    );
}

CommentsTab.propTypes = {
    comments: PropTypes.string.isRequired,
    handleFieldChange: PropTypes.func.isRequired,
    dateCancelled: PropTypes.string.isRequired,
    cancelledBy: PropTypes.number.isRequired,
    cancelledReason: PropTypes.string.isRequired,
    employees: PropTypes.arrayOf(
        PropTypes.shape({ id: PropTypes.string, fullName: PropTypes.string })
    ).isRequired
};

CommentsTab.defaultProps = {};

export default CommentsTab;
