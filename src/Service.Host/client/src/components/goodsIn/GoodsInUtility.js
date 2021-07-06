import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField } from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function GoodsInUtility({ validatePurchaseOrderResult }) {
    const [formData, setFormData] = useState({ purchaseOrderNumber: null });
    const handleFieldChange = (propertyName, newValue) => {
        setFormData({ ...formData, [propertyName]: newValue });
    };
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <InputField
                        fullWidth
                        disabled
                        value={validatePurchaseOrderResult?.bookInMessage}
                        label="Message"
                        propertyName="bookInMessage"
                    />
                </Grid>
                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={formData.purchaseOrderNumber}
                        label="PO Number"
                        propertyName="purchaseOrderNumber"
                        onChange={handleFieldChange}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

GoodsInUtility.propTypes = {
    validatePurchaseOrderResult: PropTypes.shape({
        bookInMessage: PropTypes.string
    })
};

GoodsInUtility.defaultProps = {
    validatePurchaseOrderResult: null
};

export default GoodsInUtility;
