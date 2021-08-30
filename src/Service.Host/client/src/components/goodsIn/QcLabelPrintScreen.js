import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import { InputField } from '@linn-it/linn-form-components-library';

function QcLabelPrintScreen({
    docType,
    orderNumber,
    qcState,
    partNumber,
    partDescription,
    qtyReceived,
    unitOfMeasure,
    reqNumber,
    qcInfo,
    printLabels
}) {
    const [deliveryRef, setDeliveryRef] = useState('');
    const [numContainers, setNumContainers] = useState(qtyReceived);

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
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    disabled
                    value={docType}
                    label="Req Number"
                    propertyName="reqNumber"
                />
            </Grid>
            <Grid item xs={3} />
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
            <Grid item xs={8}>
                <InputField
                    fullWidth
                    disabled
                    value={partDescription}
                    label="Part Description"
                    propertyName="description"
                />
            </Grid>
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
                    value={deliveryRef}
                    onChange={(_, newValue) => setDeliveryRef(newValue)}
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
            <Grid item xs={2}>
                <InputField
                    fullWidth
                    value={numContainers}
                    onChange={(_, newValue) => setNumContainers(newValue)}
                    label="# Containers"
                    propertyName="numberOfContainers"
                />
            </Grid>
            <Grid item xs={10} />

            <Grid item xs={2}>
                <Button
                    variant="contained"
                    color="primary"
                    onClick={() =>
                        printLabels({
                            documentType: docType,
                            partNumber,
                            partDescription,
                            deliveryRef,
                            qcInformation: qcInfo,
                            qty: qtyReceived,
                            orderNumber,
                            numberOfLabels: numContainers,
                            numberOfLines: numContainers,
                            qcState,
                            reqNumber,
                            lines: []
                        })
                    }
                >
                    Print
                </Button>
            </Grid>
            <Grid item xs={10} />
        </Grid>
    );
}

QcLabelPrintScreen.propTypes = {
    docType: PropTypes.string,
    orderNumber: PropTypes.number,
    qcState: PropTypes.string,
    partNumber: PropTypes.string,
    partDescription: PropTypes.string,
    qtyReceived: PropTypes.number,
    unitOfMeasure: PropTypes.string,
    reqNumber: PropTypes.number,
    qcInfo: PropTypes.string,
    printLabels: PropTypes.func.isRequired
};

QcLabelPrintScreen.defaultProps = {
    docType: null,
    orderNumber: null,
    qcState: null,
    partNumber: null,
    partDescription: null,
    qtyReceived: null,
    unitOfMeasure: null,
    reqNumber: null,
    qcInfo: null
};

export default QcLabelPrintScreen;
