import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Dropdown, LinkButton } from '@linn-it/linn-form-components-library';

import config from '../../../config';

function PurchTab({
    handleFieldChange,
    unitsOfMeasure,
    ourUnitOfMeasure,
    preferredSupplier,
    preferredSupplierName,
    currency,
    currencyUnitPrice,
    baseUnitPrice,
    materialPrice,
    labourPrice,
    costingPrice,
    orderHold,
    nonForecastRequirement,
    oneOffRequirement,
    sparesRequirement,
    ignoreWorkstationStock,
    imdsIdNumber,
    imdsWeight,
    links
}) {
    return (
        <Grid container spacing={3}>
            <Grid item xs={4}>
                <Dropdown
                    label="Unit Of Measure"
                    propertyName="ourUnitOfMeasure"
                    items={unitsOfMeasure.map(u => u.unit)}
                    fullWidth
                    allowNoValue
                    value={ourUnitOfMeasure}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={preferredSupplier}
                    label="Preferred Supplier"
                    disabled
                    onChange={() => {}}
                    propertyName="preferredSupplier"
                />
            </Grid>
            <Grid item xs={5}>
                <InputField
                    fullWidth
                    value={preferredSupplierName}
                    label="Name"
                    disabled
                    onChange={handleFieldChange}
                    propertyName="preferredSupplierName"
                />
            </Grid>
            <Grid item xs={3}>
                {links.find(l => l.rel === 'part-supplier') && (
                    <LinkButton
                        text="Part Suppliers"
                        newTab
                        external
                        to={`${config.proxyRoot}${links.find(l => l.rel === 'part-supplier').href}`}
                    />
                )}
            </Grid>
            {links.find(l => l.rel === 'mechanical-sourcing-sheet') && (
                <>
                    <Grid item xs={3}>
                        <LinkButton
                            text="Edit Manufacturers"
                            to={`${
                                links.find(l => l.rel === 'mechanical-sourcing-sheet').href
                            }?tab=manufacturers`}
                        />
                    </Grid>
                    <Grid item xs={9} />
                </>
            )}
            <Grid item xs={2}>
                <InputField
                    fullWidth
                    value={currency}
                    label="Currency"
                    disabled
                    onChange={handleFieldChange}
                    propertyName="currency"
                />
            </Grid>
            <Grid item xs={10} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={currencyUnitPrice}
                    label="Currency Unit Price"
                    disabled
                    onChange={handleFieldChange}
                    type="number"
                    propertyName="currencyUnitPrice"
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={nonForecastRequirement}
                    label="Non Forecast Requirement"
                    onChange={handleFieldChange}
                    type="number"
                    propertyName="nonForecastRequirement"
                />
            </Grid>
            <Grid item xs={4} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={baseUnitPrice}
                    label="Base Unit Price"
                    disabled
                    onChange={handleFieldChange}
                    type="number"
                    propertyName="baseUnitPrice"
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={oneOffRequirement}
                    label="One Off Requirement"
                    type="number"
                    onChange={handleFieldChange}
                    propertyName="oneOffRequirement"
                />
            </Grid>
            <Grid item xs={4} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={materialPrice}
                    label="Material Price"
                    type="number"
                    disabled
                    onChange={handleFieldChange}
                    propertyName="materialPrice"
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={sparesRequirement}
                    label="Spares Requirement"
                    type="number"
                    onChange={handleFieldChange}
                    propertyName="sparesRequirement"
                />
            </Grid>
            <Grid item xs={4} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={labourPrice}
                    label="Labour Price"
                    type="number"
                    disabled
                    onChange={handleFieldChange}
                    propertyName="labourPrice"
                />
            </Grid>
            <Grid item xs={4}>
                <LinkButton
                    to="/parts/change-labour"
                    text="Change Labour"
                    tooltip="Coming soon - still on Oracle Forms"
                    disabled
                />
            </Grid>
            <Grid item xs={4} />
            <Grid item xs={4}>
                <Dropdown
                    label="Ignore Workstation Stock?"
                    propertyName="ignoreWorkstationStock"
                    items={['Y', '']}
                    fullWidth
                    allowNoValue={false}
                    value={ignoreWorkstationStock}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={imdsIdNumber}
                    label="IMDS Id"
                    type="number"
                    onChange={handleFieldChange}
                    propertyName="imdsIdNumber"
                />
            </Grid>
            <Grid item xs={4} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={costingPrice}
                    label="Material Price"
                    type="number"
                    onChange={handleFieldChange}
                    propertyName="costingPrice"
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    value={imdsWeight}
                    label="IMDS Weight"
                    type="number"
                    onChange={handleFieldChange}
                    propertyName="imdsWeight"
                />
            </Grid>
            <Grid item xs={4} />
            <Grid item xs={4}>
                <Dropdown
                    label="Order Hold"
                    propertyName="orderHold"
                    items={['Y', 'N']}
                    fullWidth
                    allowNoValue={false}
                    value={orderHold}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8} />
        </Grid>
    );
}

const unitOfMeasureShape = PropTypes.shape({
    unit: PropTypes.string
});

PurchTab.propTypes = {
    handleFieldChange: PropTypes.func.isRequired,
    unitsOfMeasure: PropTypes.arrayOf(unitOfMeasureShape),
    ourUnitOfMeasure: PropTypes.string,
    preferredSupplier: PropTypes.string,
    preferredSupplierName: PropTypes.string,
    currency: PropTypes.string,
    currencyUnitPrice: PropTypes.number,
    baseUnitPrice: PropTypes.number,
    materialPrice: PropTypes.number,
    labourPrice: PropTypes.number,
    costingPrice: PropTypes.number,
    orderHold: PropTypes.string,
    nonForecastRequirement: PropTypes.number,
    oneOffRequirement: PropTypes.number,
    sparesRequirement: PropTypes.number,
    ignoreWorkstationStock: PropTypes.string,
    imdsIdNumber: PropTypes.number,
    imdsWeight: PropTypes.number,
    links: PropTypes.arrayOf(PropTypes.shape({ href: PropTypes.string, rel: PropTypes.string }))
};

PurchTab.defaultProps = {
    links: [],
    unitsOfMeasure: [],
    ourUnitOfMeasure: null,
    preferredSupplier: null,
    preferredSupplierName: null,
    currency: null,
    currencyUnitPrice: null,
    baseUnitPrice: null,
    materialPrice: null,
    labourPrice: null,
    costingPrice: null,
    orderHold: null,
    nonForecastRequirement: null,
    oneOffRequirement: null,
    sparesRequirement: null,
    ignoreWorkstationStock: null,
    imdsIdNumber: null,
    imdsWeight: null
};

export default PurchTab;
