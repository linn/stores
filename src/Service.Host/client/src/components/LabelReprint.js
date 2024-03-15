import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { Title, Dropdown, InputField, Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../containers/Page';

export default function LabelReprint({
    userNumber,
    printConsignmentLabelWorking,
    printConsignmentLabelResult,
    printConsignmentLabel,
    clearConsignmentLabelData,
    consignmentItem,
    consignmentLoading,
    getConsignment
}) {
    const [labelOptions, setLabelOptions] = useState({
        labelType: 'Carton',
        consignmentId: null,
        firstItem: null,
        lastItem: null,
        numberOfCopies: 1,
        addressId: null
    });

    const handleFieldChange = (propertyName, newValue) => {
        setLabelOptions({ ...labelOptions, [propertyName]: newValue });
        if (propertyName === 'consignmentId' && newValue > 10000) {
            getConsignment(newValue);
        }
    };

    const handleLabelTypeChange = (_propertyName, newValue) => {
        clearConsignmentLabelData();
        if (labelOptions.labelType === 'Address') {
            setLabelOptions({
                ...labelOptions,
                consignmentId: null,
                firstItem: null,
                lastItem: null,
                labelType: newValue
            });
        } else {
            setLabelOptions({ ...labelOptions, labelType: newValue });
        }
    };

    const itemLabel = itemType => {
        if (labelOptions.labelType === 'Carton') {
            return `${itemType} Carton Number`;
        }
        if (labelOptions.labelType === 'Pallet') {
            return `${itemType} Pallet Number`;
        }
        return null;
    };

    const hasConsignmentFields = () => {
        return labelOptions.labelType === 'Carton' || labelOptions.labelType === 'Pallet';
    };

    const doPrintLabel = () => {
        printConsignmentLabel({
            labelType: labelOptions.labelType,
            consignmentId: labelOptions.consignmentId,
            firstItem: labelOptions.firstItem,
            lastItem: labelOptions.lastItem,
            numberOfCopies: labelOptions.numberOfCopies,
            userNumber
        });
    };

    return (
        <Page>
            <Title text="Label Reprint" />

            <Grid style={{ marginTop: 40 }} container spacing={3}>
                <Grid item xs={4}>
                    <Dropdown
                        label="Label Type"
                        propertyName="labelType"
                        items={[
                            { id: 'Carton', displayText: 'Carton Label' },
                            { id: 'Pallet', displayText: 'Pallet Label' },
                            { id: 'Address', displayText: 'Address Label' }
                        ]}
                        value={labelOptions.labelType}
                        onChange={handleLabelTypeChange}
                        allowNoValue={false}
                    />
                </Grid>
                <Grid item xs={4}>
                    <InputField
                        label="Copies"
                        value={labelOptions.numberOfCopies}
                        onChange={handleFieldChange}
                        propertyName="numberOfCopies"
                        type="number"
                    />
                </Grid>
                <Grid item xs={4} />
                {hasConsignmentFields() && (
                    <>
                        <Grid item xs={6}>
                            <InputField
                                label="Consignment Id"
                                fullWidth
                                value={labelOptions.consignmentId}
                                onChange={handleFieldChange}
                                propertyName="consignmentId"
                                type="number"
                            />
                            {consignmentLoading ? (
                                <Loading />
                            ) : (
                                <Typography>
                                    {consignmentItem?.address.id}{' '}
                                    {consignmentItem?.address.displayAddress}
                                </Typography>
                            )}
                        </Grid>
                        <Grid item xs={3}>
                            <InputField
                                label={itemLabel('From')}
                                fullWidth
                                value={labelOptions.firstItem}
                                onChange={handleFieldChange}
                                propertyName="firstItem"
                                type="number"
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <InputField
                                label={itemLabel('To')}
                                fullWidth
                                value={labelOptions.lastItem}
                                onChange={handleFieldChange}
                                propertyName="lastItem"
                                type="number"
                            />
                        </Grid>
                    </>
                )}

                {labelOptions.labelType === 'Address' && (
                    <Grid item xs={6}>
                        <InputField
                            label="Address"
                            fullWidth
                            value={labelOptions.labelType}
                            onChange={handleFieldChange}
                            propertyName="addressId"
                            type="number"
                        />
                    </Grid>
                )}

                <Grid item xs={6} />
                <Grid item xs={4}>
                    <Button
                        style={{ marginTop: '30px', marginBottom: '40px' }}
                        onClick={doPrintLabel}
                        variant="contained"
                        color="primary"
                    >
                        Print
                    </Button>
                    {printConsignmentLabelWorking ? (
                        <Loading />
                    ) : (
                        <Typography variant="h6">{printConsignmentLabelResult?.message}</Typography>
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

LabelReprint.propTypes = {
    userNumber: PropTypes.number.isRequired,
    printConsignmentLabelWorking: PropTypes.bool,
    printConsignmentLabelResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    printConsignmentLabel: PropTypes.func.isRequired,
    clearConsignmentLabelData: PropTypes.func.isRequired,
    consignmentItem: PropTypes.shape({
        consignmentId: PropTypes.number,
        address: PropTypes.shape({ id: PropTypes.number, displayAddress: PropTypes.string })
    }),
    consignmentLoading: PropTypes.bool,
    getConsignment: PropTypes.func.isRequired
};

LabelReprint.defaultProps = {
    printConsignmentLabelWorking: false,
    printConsignmentLabelResult: null,
    consignmentItem: null,
    consignmentLoading: false
};
