import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
    //DatePicker
} from '@linn-it/linn-form-components-library';

function GeneralTab({ accountingCompany, handleFieldChange }) {
    return (
        <Grid container spacing={3}>
            <Grid itemx xs={4}>
                <InputField
                    value={accountingCompany}
                    label="Company"
                    propertyName="accountingCompany"
                    onChange={handleFieldChange}
                />
            </Grid>
        </Grid>
    );
}

GeneralTab.propTypes = {};

GeneralTab.defaultProps = {};

export default GeneralTab;
