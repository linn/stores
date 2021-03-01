import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { Loading, InputField, Page, DatePicker } from '@linn-it/linn-form-components-library';

export default function CreateRep25({ makeExportReturnLoading }) {
    const [options, setOptions] = useState({
        invoiceNumber: '',
        numberOfCartons: '',
        grossDimensions: '',
        carrierUsed: '',
        dateOfDispatch: '',
        shipmentWeight: '',
        transportVia: '',
        consignmentNumber: ''
    });

    const handleFieldChange = (propertyName, newValue) => {
        setOptions(o => ({ ...o, [propertyName]: newValue }));
    };

    return (
        <Page>
            <Grid container spacing={3}>
                {makeExportReturnLoading ? (
                    <Loading />
                ) : (
                    <>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={options.invoiceNumber}
                                label="Invoice Number"
                                propertyName="invoiceNumber"
                                onChange={handleFieldChange}
                                type="number"
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={options.numberOfCartons}
                                label="Number of Cartons"
                                propertyName="numberOfCartons"
                                onChange={handleFieldChange}
                                type="number"
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={options.grossDimensions}
                                label="Gross Dimensions"
                                propertyName="grossDimensions"
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={options.carrierUsed}
                                label="Carrier Used"
                                propertyName="carrierUsed"
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <DatePicker
                                label="Date of Dispatch"
                                value={options.dateOfDispatch}
                                onChange={value => {
                                    handleFieldChange('dateOfDispatch', value);
                                }}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={options.shipmentWeight}
                                label="Shipment Weight"
                                propertyName="shipmentWeight"
                                onChange={handleFieldChange}
                                type="number"
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={options.transportVia}
                                label="Transport via"
                                propertyName="transportVia"
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={options.consignmentNumber}
                                label="Consignment Number"
                                propertyName="consignmentNumber"
                                onChange={handleFieldChange}
                                type="number"
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={12}>
                            <Button variant="outlined" color="primary" onClick={() => {}}>
                                Create RSN Return
                            </Button>
                        </Grid>
                    </>
                )}
            </Grid>
        </Page>
    );
}

CreateRep25.propTypes = {
    makeExportReturnLoading: PropTypes.bool
};

CreateRep25.defaultProps = {
    makeExportReturnLoading: false
};
