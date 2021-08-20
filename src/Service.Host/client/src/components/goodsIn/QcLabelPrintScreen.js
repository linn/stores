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
    useEffect(() => {
        if (transactionCode) {
            if (docType === 'WO') {
                // hide document line field?
            } else {
                // show document line field?
            }
        }
    }, [transactionCode]);
    return (
        <Grid container spacing={3}>
            <Grid item xs={12}>
                <Typography variant="h5" gutterBottom>
                    Label Details
                </Typography>
            </Grid>
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    disabled
                    value={docType}
                    label="Document Type"
                    propertyName="docType"
                />
            </Grid>
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    disabled
                    value={docType}
                    label="Order Number"
                    propertyName="orderNumber"
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    disabled
                    value={qcState}
                    label="QC State"
                    propertyName="orderNumber"
                />
            </Grid>
            <Grid item xs={9} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    disabled
                    value={partNumber}
                    label="Part"
                    propertyName="partNumber"
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    disabled
                    value={partDescription}
                    label="Part Description"
                    propertyName="partDescription"
                />
            </Grid>
            <Grid item xs={4} />
            <Grid item xs={2}>
                <InputField
                    fullWidth
                    disabled
                    value={qtyReceived}
                    label="Qty Received"
                    propertyName="qtyReceived"
                />
            </Grid>
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    disabled
                    value={unitOfMeasure}
                    label="Unit of Measure"
                    propertyName="unitOfMeasure"
                />
            </Grid>
            <Grid item xs={7} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    disabled
                    value={deliveryRef}
                    label="Delivery Ref"
                    propertyName="deliveryRef"
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    disabled
                    value={qcInfo}
                    label="QC Info"
                    propertyName="qcInfo"
                />
            </Grid>
            <Grid item xs={6} />
        </Grid>
    );
}

export default QcLabelPrintScreen;
