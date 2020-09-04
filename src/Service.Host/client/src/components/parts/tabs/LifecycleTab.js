import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    InputField,
    Dropdown,
    DatePicker,
    LinkButton
} from '@linn-it/linn-form-components-library';

function LifeCycleTab({
    handleFieldChange,
    dateCreated,
    createdByName,
    dateLive,
    madeLiveByName,
    phasedOutByName,
    reasonPhasedOut,
    scrapOrConvert,
    purchasingPhaseOutType,
    datePhasedOut,
    dateDesignObsolete
}) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={3}>
                <DatePicker
                    label="Date Created"
                    value={dateCreated}
                    onChange={value => {
                        handleFieldChange('dateCreated', value);
                    }}
                />
            </Grid>
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    value={createdByName}
                    label="Created By"
                    onChange={handleFieldChange}
                    propertyName="createdByName"
                />
            </Grid>
            <Grid item xs={6} />
            <Grid item xs={3}>
                <DatePicker
                    label="Date Live"
                    value={dateLive}
                    onChange={value => {
                        handleFieldChange('dateLive', value);
                    }}
                />
            </Grid>
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    value={madeLiveByName}
                    label="Made Live By"
                    onChange={handleFieldChange}
                    propertyName="madeLiveByName"
                />
            </Grid>
            <Grid item xs={3}>
                <LinkButton
                    to="/inventory/parts/make-live"
                    text="Make Live"
                    tooltip="Coming soon - still on Oracle Forms"
                    disabled
                />
            </Grid>
            <Grid item xs={3} />
            <Grid item xs={3}>
                <InputField
                    fullWidth
                    value={phasedOutByName}
                    label="Phased Out By"
                    onChange={handleFieldChange}
                    propertyName="phasedOutByName"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={reasonPhasedOut}
                    label="Reason Phased Out"
                    onChange={handleFieldChange}
                    propertyName="reasonPhasedOut"
                />
            </Grid>
            <Grid item xs={3} />
            <Grid item xs={4}>
                <Dropdown
                    label="Scrap Or Convertible"
                    propertyName="scrapOrConvert"
                    items={['SCRAP', 'CONVERT']}
                    fullWidth
                    allowNoValue
                    value={scrapOrConvert}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={4}>
                <Dropdown
                    label="Purch Obsolete Type"
                    propertyName="purchasingPhaseOutType"
                    items={['AVAILABLE', 'UNAVAILABLE']}
                    fullWidth
                    allowNoValue
                    value={purchasingPhaseOutType}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date Purch Phase Out"
                    value={datePhasedOut}
                    onChange={value => {
                        handleFieldChange('datePhasedOut', value);
                    }}
                />
            </Grid>
            <Grid item xs={3}>
                <DatePicker
                    label="Date Design Obsolete"
                    value={dateDesignObsolete}
                    onChange={value => {
                        handleFieldChange('dateDesignObsolete', value);
                    }}
                />
            </Grid>
            <Grid item xs={2} />
        </Grid>
    );
}

LifeCycleTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    dateCreated: PropTypes.string,
    createdByName: PropTypes.string,
    dateLive: PropTypes.string,
    madeLiveByName: PropTypes.string,
    phasedOutByName: PropTypes.string,
    reasonPhasedOut: PropTypes.string,
    scrapOrConvert: PropTypes.string,
    purchasingPhaseOutType: PropTypes.string,
    datePhasedOut: PropTypes.string,
    dateDesignObsolete: PropTypes.string
};

LifeCycleTab.defaultProps = {
    dateCreated: null,
    createdByName: null,
    dateLive: null,
    madeLiveByName: null,
    phasedOutByName: null,
    reasonPhasedOut: null,
    scrapOrConvert: null,
    purchasingPhaseOutType: null,
    datePhasedOut: null,
    dateDesignObsolete: null
};

export default LifeCycleTab;
