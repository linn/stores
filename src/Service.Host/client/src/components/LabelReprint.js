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
    clearConsignmentLabelData
}) {
    const [labelOptions, setLabelOptions] = useState({
        labelType: 'Carton',
        consignmentId: null,
        firstItem: null,
        lastItem: null,
        numberOfCopies: 1
    });

    const handleFieldChange = (propertyName, newValue) => {
        setLabelOptions({ ...labelOptions, [propertyName]: newValue });
    };

    const handleLabelTypeChange = (_propertyName, newValue) => {
        clearConsignmentLabelData();
        setLabelOptions({ ...labelOptions, labelType: newValue });
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
                            { id: 'Pallet', displayText: 'Pallet Label' }
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
                <Grid item xs={3}>
                    <InputField
                        label="Consignment Id"
                        fullWidth
                        value={labelOptions.consignmentId}
                        onChange={handleFieldChange}
                        propertyName="consignmentId"
                        type="number"
                    />
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
    clearConsignmentLabelData: PropTypes.func.isRequired
};

LabelReprint.defaultProps = {
    printConsignmentLabelWorking: false,
    printConsignmentLabelResult: null
};
