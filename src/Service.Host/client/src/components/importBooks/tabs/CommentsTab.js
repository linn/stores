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
    employees,
    allowedToEdit
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
                    disabled={!allowedToEdit}
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
                    disabled={!allowedToEdit}
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
                    disabled={!allowedToEdit}
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
                    disabled={!allowedToEdit}
                />
            </Grid>
        </Grid>
    );
}

CommentsTab.propTypes = {
    comments: PropTypes.string,
    handleFieldChange: PropTypes.func.isRequired,
    dateCancelled: PropTypes.string,
    cancelledBy: PropTypes.number,
    cancelledReason: PropTypes.string,
    employees: PropTypes.arrayOf(
        PropTypes.shape({ id: PropTypes.number, fullName: PropTypes.string })
    ),
    allowedToEdit: PropTypes.func.isRequired
};

CommentsTab.defaultProps = {
    employees: [{ id: -1, fullname: 'loading..' }],
    comments: '',
    dateCancelled: '',
    cancelledBy: '',
    cancelledReason: ''
};

export default CommentsTab;
