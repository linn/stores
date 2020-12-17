import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, DatePicker } from '@linn-it/linn-form-components-library';

export default function VerificationTab({
    handleFieldChange,
    partCreatedBy,
    partCreatedByName,
    partCreatedDate,
    verifiedBy,
    verifiedByName,
    verifiedDate,
    qualityVerifiedBy,
    qualityVerifiedByName,
    qualityVerifiedDate,
    mcitVerifiedBy,
    mcitVerifiedByName,
    mcitVerifiedDate,
    applyTCodeBy,
    applyTCodeName,
    applyTCodeDate,
    removeTCodeBy,
    removeTCodeName,
    removeTCodeDate,
    cancelledBy,
    cancelledByName,
    dateCancelled
}) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={2}>
                <InputField
                    label="Part Created By"
                    value={partCreatedBy}
                    propertyName="partCreatedBy"
                    disabled
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    label="Name"
                    value={partCreatedByName}
                    propertyName="partCreatedByName"
                    fullWidth
                    disabled
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={partCreatedDate}
                    onChange={value => {
                        handleFieldChange('partCreatedDate', value);
                    }}
                />
            </Grid>
            <Grid item xs={3} />

            <Grid item xs={2}>
                <InputField
                    label="Purchasing"
                    value={verifiedBy}
                    propertyName="verifiedBy"
                    disabled
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    label="Name"
                    value={verifiedByName}
                    propertyName="verifiedByName"
                    fullWidth
                    disabled
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={verifiedDate}
                    onChange={value => {
                        handleFieldChange('verifiedDate', value);
                    }}
                />
            </Grid>
            <Grid item xs={3} />

            <Grid item xs={2}>
                <InputField
                    label="Quality"
                    value={qualityVerifiedBy}
                    propertyName="qualityVerifiedBy"
                    disabled
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    label="Name"
                    value={qualityVerifiedByName}
                    propertyName="qualityVerifiedByName"
                    fullWidth
                    disabled
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={qualityVerifiedDate}
                    onChange={value => {
                        handleFieldChange('qualityVerifiedDate', value);
                    }}
                />
            </Grid>
            <Grid item xs={3} />

            <Grid item xs={2}>
                <InputField
                    label="Design"
                    value={mcitVerifiedBy}
                    propertyName="mcitVerifiedBy"
                    disabled
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    label="Name"
                    value={mcitVerifiedByName}
                    disabled
                    propertyName="mcitVerifiedByName"
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={mcitVerifiedDate}
                    onChange={value => {
                        handleFieldChange('mcitVerifiedDate', value);
                    }}
                />
            </Grid>
            <Grid item xs={3} />

            <Grid item xs={2}>
                <InputField
                    label="Apply T Code"
                    value={applyTCodeBy}
                    propertyName="applyTCodeBy"
                    disabled
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    label="Name"
                    value={applyTCodeName}
                    propertyName="applyTCodeName"
                    disabled
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={applyTCodeDate}
                    onChange={value => {
                        handleFieldChange('applyTCodeDate', value);
                    }}
                />
            </Grid>
            <Grid item xs={3} />

            <Grid item xs={2}>
                <InputField
                    label="Remove T Code"
                    value={removeTCodeBy}
                    propertyName="removeTCodeBy"
                    disabled
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    label="Name"
                    value={removeTCodeName}
                    propertyName="removeTCodeName"
                    fullWidth
                    disabled
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={removeTCodeDate}
                    onChange={value => {
                        handleFieldChange('removeTCodeDate', value);
                    }}
                />
            </Grid>
            <Grid item xs={3} />

            <Grid item xs={2}>
                <InputField
                    label="Cancelled By"
                    value={cancelledBy}
                    propertyName="cancelledBy"
                    disabled
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    label="Name"
                    value={cancelledByName}
                    disabled
                    propertyName="cancelledByName"
                    fullWidth
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date"
                    value={dateCancelled}
                    onChange={value => {
                        handleFieldChange('dateCancelled', value);
                    }}
                />
            </Grid>
            <Grid item xs={3} />
        </Grid>
    );
}

VerificationTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    partCreatedBy: PropTypes.number,
    partCreatedByName: PropTypes.string,
    partCreatedDate: PropTypes.string,
    verifiedBy: PropTypes.number,
    verifiedByName: PropTypes.string,
    verifiedDate: PropTypes.string,
    qualityVerifiedBy: PropTypes.number,
    qualityVerifiedByName: PropTypes.string,
    qualityVerifiedDate: PropTypes.string,
    mcitVerifiedBy: PropTypes.number,
    mcitVerifiedByName: PropTypes.string,
    mcitVerifiedDate: PropTypes.string,
    applyTCodeBy: PropTypes.number,
    applyTCodeName: PropTypes.string,
    applyTCodeDate: PropTypes.string,
    removeTCodeBy: PropTypes.number,
    removeTCodeName: PropTypes.string,
    removeTCodeDate: PropTypes.string,
    cancelledBy: PropTypes.number,
    cancelledByName: PropTypes.string,
    dateCancelled: PropTypes.string
};

VerificationTab.defaultProps = {
    partCreatedBy: null,
    partCreatedByName: null,
    partCreatedDate: null,
    verifiedBy: null,
    verifiedByName: null,
    verifiedDate: null,
    qualityVerifiedBy: null,
    qualityVerifiedByName: null,
    qualityVerifiedDate: null,
    mcitVerifiedBy: null,
    mcitVerifiedByName: null,
    mcitVerifiedDate: null,
    applyTCodeBy: null,
    applyTCodeName: null,
    applyTCodeDate: null,
    removeTCodeBy: null,
    removeTCodeName: null,
    removeTCodeDate: null,
    cancelledBy: null,
    cancelledByName: null,
    dateCancelled: null
};
