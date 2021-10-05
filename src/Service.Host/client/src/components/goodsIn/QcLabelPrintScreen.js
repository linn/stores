import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { Decimal } from 'decimal.js';
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import { ErrorCard, InputField, Loading } from '@linn-it/linn-form-components-library';
import Tooltip from '@material-ui/core/Tooltip';

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
    printLabels,
    printLabelsResult,
    printLabelsLoading,
    kardexLocation
}) {
    const [deliveryRef, setDeliveryRef] = useState('');
    const [numContainers, setNumContainers] = useState(qtyReceived);
    const [labelLines, setLabelLines] = useState([]);
    const [labelLinesExpanded, setLabelLinesExpanded] = useState(false);

    const divide = (a, b) => (!a || !b ? null : new Decimal(a).dividedBy(new Decimal(b)));

    const qtiesInvalid = () =>
        Number(qtyReceived) !== labelLines.reduce((a, b) => Number(a) + Number(b.qty), 0);

    useEffect(() => {
        const lines = [];
        for (let index = 0; index < numContainers; index += 1) {
            lines.push({ id: index.toString(), qty: divide(qtyReceived, numContainers) });
        }

        setLabelLines(lines);
    }, [numContainers, qtyReceived]);

    const handleLabelLineQtyChange = (propertyName, newValue) => {
        const index = propertyName.replace('line ', '');
        setLabelLines(lines =>
            lines.map(line => {
                return line.id === index ? { id: index, qty: newValue } : line;
            })
        );
    };

    const printButton = () => (
        <Button
            variant="contained"
            color="primary"
            disabled={qtiesInvalid()}
            onClick={() =>
                printLabels({
                    documentType: docType,
                    kardexLocation,
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
                    lines: labelLines
                })
            }
        >
            Print
        </Button>
    );

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
                    value={orderNumber}
                    label="Order Number"
                    propertyName="orderNumber"
                />
            </Grid>
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    disabled
                    value={reqNumber}
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
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    disabled
                    value={kardexLocation}
                    label="Kardex Location"
                    propertyName="kardexLocation"
                />
            </Grid>
            <Grid item xs={9} />
            <Grid item xs={2}>
                <InputField
                    fullWidth
                    value={numContainers}
                    onChange={(_, newValue) => setNumContainers(newValue)}
                    label="# Containers"
                    type="number"
                    propertyName="numberOfContainers"
                />
            </Grid>
            <Grid item xs={10} />

            <Grid item xs={2}>
                {qtiesInvalid() ? (
                    <Tooltip title="Values entered don't add up to Qty Received">
                        <span>{printButton()} </span>
                    </Tooltip>
                ) : (
                    printButton()
                )}
            </Grid>

            <Grid item xs={10} />

            <Grid item xs={12}>
                <Accordion
                    expanded={labelLinesExpanded}
                    disabled={!!kardexLocation}
                    data-testid="quantitiesExpansionPanel"
                >
                    <AccordionSummary
                        onClick={() => setLabelLinesExpanded(!labelLinesExpanded)}
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1a-content"
                        id="panel1a-header"
                    >
                        <Typography>Set Qty Split Across Labels</Typography>
                    </AccordionSummary>
                    <AccordionDetails>
                        <Grid container spacing={3}>
                            <>
                                {numContainers &&
                                    labelLines.map(l => (
                                        <Fragment key={l.id}>
                                            <Grid item xs={2}>
                                                <InputField
                                                    fullWidth
                                                    value={l.qty}
                                                    onChange={(propertyName, newValue) =>
                                                        handleLabelLineQtyChange(
                                                            propertyName,
                                                            newValue
                                                        )
                                                    }
                                                    label={Number(l.id) + 1}
                                                    type="number"
                                                    propertyName={`line ${l.id}`}
                                                />
                                            </Grid>
                                            <Grid item xs={10} />
                                        </Fragment>
                                    ))}
                            </>
                        </Grid>
                    </AccordionDetails>
                </Accordion>
            </Grid>

            <Grid item xs={12}>
                {printLabelsResult?.success === false && (
                    <ErrorCard errorMessage={printLabelsResult?.message} />
                )}
                {printLabelsLoading && <Loading />}
            </Grid>
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
    printLabels: PropTypes.func.isRequired,
    printLabelsResult: PropTypes.shape({ message: PropTypes.string, success: PropTypes.bool }),
    printLabelsLoading: PropTypes.bool,
    kardexLocation: PropTypes.string
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
    qcInfo: null,
    printLabelsResult: null,
    printLabelsLoading: false,
    kardexLocation: null
};

export default QcLabelPrintScreen;
