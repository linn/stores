import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { InputField } from '@linn-it/linn-form-components-library';

function QcLabelPrintScreen({
    bookInLocation,
    palletNumber,
    docType,
    orderNumber,
    orderLine,
    qcState,
    partNumber,
    partDescription,
    qtyReceived,
    unitOfMeasure,
    deliveryRef,
    testedBy,
    initials,
    dateBooked,
    qcInfo,
    numberOfContainers,
    storagePlace,
    transactionCode
}) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={12}>
                <Typography variant="h5" gutterBottom>
                    Label Details
                </Typography>
            </Grid>
            <Grid item xs={12}>
                <Typography variant="h5" gutterBottom>
                    {storagePlace?.siteCode}
                </Typography>
            </Grid>
            <Grid item xs={12}>
                <InputField
                    fullWidth
                    disabled
                    value={partNumber}
                    label="Part"
                    propertyName="partNumber"
                />
            </Grid>
            <Grid item xs={12}>
                <InputField
                    fullWidth
                    disabled
                    value={qcInfo}
                    label="QC Info"
                    propertyName="qcInfo"
                />
            </Grid>
        </Grid>
    );
}

export default QcLabelPrintScreen;
