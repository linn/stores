import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Tooltip from '@material-ui/core/Tooltip';
import {
    InputField,
    Dropdown,
    Typeahead,
    LinkButton,
    SearchInputField,
    TableWithInlineEditing
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';

function ImpBookTab({
    employees,
    suppliersSearchResults,
    suppliersSearchLoading,
    searchSuppliers,
    clearSuppliersSearch,
    carriersSearchResults,
    carriersSearchLoading,
    searchCarriers,
    clearCarriersSearch,
    transportCodes,
    transactionCodes,
    deliveryTerms,
    ports,
    handleFieldChange,
    dateCreated,
    parcelNumber,
    supplierId,
    foreignCurrency,
    currency,
    carrierId,
    transportId,
    transportBillNumber,
    transactionId,
    deliveryTermCode,
    arrivalPort,
    arrivalDate,
    totalImportValue,
    weight,
    customsEntryCode,
    customsEntryCodeDate,
    linnDuty,
    linnVat,
    numCartons,
    numPallets,
    createdBy,
    customsEntryCodePrefix,
    allowedToEdit,
    currencies,
    handleUpdateInvoiceDetails,
    invoiceDetails,
    totalInvoiceValue,
    searchParcels,
    clearParcelsSearch,
    parcelsSearchResults,
    parcelsSearchLoading,
    handleParcelChange,
    supplierCountryValue,
    supplierNameValue,
    carrierNameValue,
    countryIsInEU,
    pva,
    exchangeRate
}) {
    const clearSupplier = () => {
        handleFieldChange('supplierId', '');
    };

    const clearCarrier = () => {
        handleFieldChange('carrierId', '');
    };

    const handleSupplierChange = supplierParam => {
        handleFieldChange('supplierId', supplierParam.id);
    };

    const handleCarrierChange = carrierParam => {
        handleFieldChange('carrierId', carrierParam.id);
    };

    const clearParcel = () => {
        handleFieldChange('parcelNumber', '');
    };

    const useStyles = makeStyles(theme => ({
        displayInline: {
            display: 'inline'
        },
        marginTop1: {
            marginTop: theme.spacing(1),
            display: 'inline-block',
            width: '2em'
        },
        gapAbove: {
            marginTop: theme.spacing(8)
        },
        negativeTopMargin: {
            marginTop: theme.spacing(-4)
        }
    }));
    const classes = useStyles();

    return (
        <>
            <Grid container spacing={1} item xs={7}>
                <Grid item xs={6}>
                    <SearchInputField
                        label="Date Created"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="dateCreated"
                        type="date"
                        value={dateCreated}
                        required
                        disabled={!allowedToEdit}
                        data-testid="dateCreated"
                    />
                </Grid>

                <Grid item xs={6}>
                    <Dropdown
                        items={employees.map(e => ({
                            displayText: `${e.fullName} (${e.id})`,
                            id: parseInt(e.id, 10)
                        }))}
                        propertyName="createdBy"
                        fullWidth
                        value={createdBy}
                        label="Created by"
                        onChange={handleFieldChange}
                        type="number"
                        disabled={!allowedToEdit}
                        data-testid="createdBy"
                    />
                </Grid>

                <Grid item xs={6}>
                    <Typeahead
                        label="Parcel Number"
                        propertyName="parcelNumber"
                        title="Search for a parcel"
                        onSelect={handleParcelChange}
                        items={parcelsSearchResults}
                        loading={parcelsSearchLoading}
                        fetchItems={searchParcels}
                        clearSearch={() => clearParcelsSearch}
                        value={parcelNumber?.toString()}
                        modal
                        links={false}
                        debounce={1000}
                        minimumSearchTermLength={2}
                        required
                        disabled={!allowedToEdit}
                        clearable
                        clearTooltipText="Clear Parcel Number"
                        onClear={clearParcel}
                    />
                </Grid>

                <Grid item xs={6}>
                    <LinkButton
                        text="View Parcel"
                        to={`/logistics/parcels/${parcelNumber}`}
                        tooltip="Right click to open in new tab"
                        external
                    />
                </Grid>

                <Grid item xs={6}>
                    <Typeahead
                        label="Supplier"
                        title="Search for a supplier"
                        onSelect={handleSupplierChange}
                        items={suppliersSearchResults}
                        loading={suppliersSearchLoading}
                        fetchItems={searchSuppliers}
                        clearSearch={() => clearSuppliersSearch}
                        value={`${supplierId} - ${supplierNameValue()}`}
                        modal
                        links={false}
                        debounce={1000}
                        minimumSearchTermLength={2}
                        required
                        disabled={!allowedToEdit}
                        clearable
                        clearTooltipText="Clear Supplier Number"
                        onClear={clearSupplier}
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="Supplier Country"
                        propertyName="supplierCountry"
                        value={supplierCountryValue()}
                        disabled
                        fullwidth
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="EC (EU) Member"
                        propertyName="euEcMember"
                        value={countryIsInEU()}
                        disabled
                        fullwidth
                    />
                </Grid>

                <Grid item xs={4}>
                    <Dropdown
                        items={[
                            { id: 'Y', displayText: 'Yes' },
                            { id: 'N', displayText: 'No' }
                        ]}
                        propertyName="pva"
                        fullWidth
                        value={pva}
                        label="PVA"
                        onChange={handleFieldChange}
                        required
                        disabled={!allowedToEdit}
                    />
                </Grid>

                <Grid item xs={4}>
                    <Dropdown
                        items={[
                            { id: 'Y', displayText: 'Yes' },
                            { id: 'N', displayText: 'No' }
                        ]}
                        propertyName="foreignCurrency"
                        fullWidth
                        value={foreignCurrency}
                        label="Foreign Currency"
                        onChange={handleFieldChange}
                        required
                        disabled={!allowedToEdit}
                    />
                </Grid>

                <Grid item xs={4}>
                    <Dropdown
                        items={currencies}
                        propertyName="currency"
                        fullWidth
                        value={currency}
                        label="Currency"
                        onChange={handleFieldChange}
                        disabled={!allowedToEdit}
                    />
                </Grid>
                <Grid item xs={6}>
                    <Tooltip
                        title="Shouldn't need to manually enter this, auto calculated from invoice details below"
                        placement="bottom-start"
                    >
                        <span>
                            <InputField
                                label="Total Import Value (GBP)"
                                value={totalImportValue}
                                onChange={handleFieldChange}
                                propertyName="totalImportValue"
                                fullwidth
                                type="number"
                                disabled={!allowedToEdit}
                                data-testid="totalImportValue"
                            />
                        </span>
                    </Tooltip>
                </Grid>
                <Grid item xs={1} />

                <Grid item xs={5}>
                    <InputField
                        label="Exchange Rate for date created"
                        value={exchangeRate}
                        onChange={handleFieldChange}
                        propertyName="exchangeRate"
                        fullwidth
                        type="number"
                        disabled
                    />
                </Grid>

                <Grid item xs={12} className={classes.gapAbove} data-testid="invoiceDetailsTable">
                    <TableWithInlineEditing
                        columnsInfo={[
                            {
                                title: 'Invoice Number',
                                key: 'invoiceNumber',
                                type: 'text'
                            },
                            {
                                title: 'Invoice Value',
                                key: 'invoiceValue',
                                type: 'number',
                                decimalPlaces: 2
                            }
                        ]}
                        content={invoiceDetails ?? [{}]}
                        updateContent={handleUpdateInvoiceDetails}
                        allowedToEdit={allowedToEdit}
                        allowedToCreate={allowedToEdit}
                        allowedToDelete={allowedToEdit}
                    />
                </Grid>
                <Grid item xs={6} />
                <Grid item xs={6}>
                    <InputField
                        label="Total Invoice Value (Currency)"
                        value={totalInvoiceValue}
                        onChange={handleFieldChange}
                        propertyName="totalInvoiceValue"
                        fullwidth
                        type="number"
                        maxLength={20}
                        decimalPlaces={2}
                        required
                        disabled
                    />
                </Grid>
                <Grid item xs={12} className={classes.gapAbove}>
                    <Typeahead
                        label="Carrier"
                        title="Search for a carrier"
                        onSelect={handleCarrierChange}
                        items={carriersSearchResults}
                        loading={carriersSearchLoading}
                        fetchItems={searchCarriers}
                        clearSearch={() => clearCarriersSearch}
                        value={`${carrierId} - ${carrierNameValue()}`}
                        modal
                        links={false}
                        debounce={1000}
                        minimumSearchTermLength={2}
                        required
                        disabled={!allowedToEdit}
                        clearable
                        clearTooltipText="Clear Carrier Number"
                        onClear={clearCarrier}
                    />
                </Grid>
                <Grid item xs={6}>
                    <Dropdown
                        items={transportCodes}
                        propertyName="transportId"
                        fullWidth
                        value={transportId}
                        label="Mode of Transport"
                        onChange={handleFieldChange}
                        disabled={!allowedToEdit}
                        type="number"
                        required
                    />
                </Grid>

                <Grid item xs={6}>
                    <InputField
                        label="Transport Bill Number"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="transportBillNumber"
                        value={transportBillNumber}
                        disabled={!allowedToEdit}
                    />
                </Grid>

                <Grid item xs={6}>
                    <Dropdown
                        items={transactionCodes}
                        propertyName="transactionId"
                        fullWidth
                        value={transactionId}
                        label="Transaction Code"
                        onChange={handleFieldChange}
                        disabled={!allowedToEdit}
                        type="number"
                        required
                    />
                </Grid>

                <Grid item xs={6}>
                    <Dropdown
                        items={deliveryTerms}
                        propertyName="deliveryTermCode"
                        fullWidth
                        value={deliveryTermCode}
                        label="Delivery Term Code"
                        onChange={handleFieldChange}
                        disabled={!allowedToEdit}
                        required
                    />
                </Grid>

                <Grid item xs={6}>
                    <Dropdown
                        items={ports}
                        propertyName="arrivalPort"
                        fullWidth
                        value={arrivalPort}
                        label="Arrival Port"
                        onChange={handleFieldChange}
                        disabled={!allowedToEdit}
                    />
                </Grid>

                <Grid item xs={6}>
                    <SearchInputField
                        label="Arrival Date"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="arrivalDate"
                        type="date"
                        value={arrivalDate}
                        disabled={!allowedToEdit}
                    />
                </Grid>
                <Grid item xs={9} />
            </Grid>

            <Grid container spacing={1} item xs={1} />

            <Grid container spacing={1} item xs={4}>
                <Grid item xs={6}>
                    <InputField
                        label="Number of Cartons"
                        value={numCartons}
                        onChange={handleFieldChange}
                        propertyName="numCartons"
                        fullwidth
                        type="number"
                        disabled={!allowedToEdit}
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        label="Number of Pallets"
                        value={numPallets}
                        onChange={handleFieldChange}
                        propertyName="numPallets"
                        fullwidth
                        type="number"
                        disabled={!allowedToEdit}
                    />
                </Grid>
                <Grid item xs={12}>
                    <InputField
                        label="Weight"
                        value={weight}
                        onChange={handleFieldChange}
                        propertyName="weight"
                        fullwidth
                        type="number"
                        maxLength={10}
                        decimalPlaces={2}
                        disabled={!allowedToEdit}
                    />
                </Grid>

                <Grid item xs={2}>
                    <InputField
                        label="Prefix"
                        value={customsEntryCodePrefix}
                        onChange={handleFieldChange}
                        propertyName="customsEntryCodePrefix"
                        fullwidth
                        disabled={!allowedToEdit}
                    />
                </Grid>
                <Grid item xs={10}>
                    <InputField
                        label="Customs Entry Code"
                        value={customsEntryCode}
                        onChange={handleFieldChange}
                        propertyName="customsEntryCode"
                        fullwidth
                        disabled={!allowedToEdit}
                    />
                </Grid>

                <Grid item xs={12}>
                    <SearchInputField
                        label="Customs Entry Date"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="customsEntryCodeDate"
                        type="date"
                        value={customsEntryCodeDate}
                        disabled={!allowedToEdit}
                    />
                </Grid>

                <Grid item xs={6}>
                    <InputField
                        label="Linn Duty"
                        value={linnDuty}
                        onChange={handleFieldChange}
                        propertyName="linnDuty"
                        fullwidth
                        type="number"
                        disabled={!allowedToEdit}
                    />
                </Grid>

                <Grid item xs={6}>
                    <InputField
                        label="Linn Vat"
                        value={linnVat}
                        onChange={handleFieldChange}
                        propertyName="linnVat"
                        fullwidth
                        type="number"
                        disabled={!allowedToEdit}
                    />
                </Grid>

                {/* empty grid items to force the stuff in the right hand column up 
                and stop it spreading to the full height of the left hand column. 
                Ain't pretty but working for now without spending ages on it - can 
                maybe do this with margin/css or something else at some point,
                but not sure it'd be reliable on all screens, will have a think */}
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
                <Grid item xs={12} />
            </Grid>
        </>
    );
}

ImpBookTab.propTypes = {
    employees: PropTypes.arrayOf(
        PropTypes.shape({ id: PropTypes.number, fullName: PropTypes.string })
    ),
    suppliersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({ id: PropTypes.number, name: PropTypes.string, country: PropTypes.string })
    ).isRequired,
    suppliersSearchLoading: PropTypes.bool,
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired,
    carriersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({ id: PropTypes.number, name: PropTypes.string, country: PropTypes.string })
    ).isRequired,
    carriersSearchLoading: PropTypes.bool,
    searchCarriers: PropTypes.func.isRequired,
    clearCarriersSearch: PropTypes.func.isRequired,
    transportCodes: PropTypes.arrayOf(
        PropTypes.shape({ transportId: PropTypes.number, description: PropTypes.string })
    ).isRequired,
    transactionCodes: PropTypes.arrayOf(
        PropTypes.shape({ transactionId: PropTypes.number, description: PropTypes.string })
    ).isRequired,
    deliveryTerms: PropTypes.arrayOf(
        PropTypes.shape({ deliveryTermCode: PropTypes.number, description: PropTypes.string })
    ).isRequired,
    ports: PropTypes.arrayOf(
        PropTypes.shape({ portCode: PropTypes.number, description: PropTypes.string })
    ).isRequired,
    handleFieldChange: PropTypes.func.isRequired,
    dateCreated: PropTypes.string.isRequired,
    parcelNumber: PropTypes.number,
    supplierId: PropTypes.number.isRequired,
    foreignCurrency: PropTypes.string.isRequired,
    currency: PropTypes.string,
    carrierId: PropTypes.number.isRequired,
    transportId: PropTypes.number,
    transportBillNumber: PropTypes.string,
    transactionId: PropTypes.number,
    deliveryTermCode: PropTypes.string.isRequired,
    arrivalPort: PropTypes.string,
    arrivalDate: PropTypes.string,
    totalImportValue: PropTypes.number,
    weight: PropTypes.number,
    customsEntryCode: PropTypes.string,
    customsEntryCodeDate: PropTypes.string,
    linnDuty: PropTypes.number,
    linnVat: PropTypes.number,
    numCartons: PropTypes.number,
    numPallets: PropTypes.number,
    createdBy: PropTypes.number,
    customsEntryCodePrefix: PropTypes.string,
    allowedToEdit: PropTypes.bool.isRequired,
    invoiceDetails: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    currencies: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    handleUpdateInvoiceDetails: PropTypes.func.isRequired,
    totalInvoiceValue: PropTypes.string,
    searchParcels: PropTypes.func.isRequired,
    clearParcelsSearch: PropTypes.func.isRequired,
    parcelsSearchResults: PropTypes.arrayOf(
        PropTypes.shape({ id: PropTypes.number, name: PropTypes.string })
    ).isRequired,
    parcelsSearchLoading: PropTypes.bool,
    handleParcelChange: PropTypes.func.isRequired,
    supplierCountryValue: PropTypes.func.isRequired,
    supplierNameValue: PropTypes.func.isRequired,
    carrierNameValue: PropTypes.func.isRequired,
    countryIsInEU: PropTypes.func.isRequired,
    pva: PropTypes.string,
    exchangeRate: PropTypes.number.isRequired
};

ImpBookTab.defaultProps = {
    employees: [{ id: -1, fullname: 'loading..' }],
    parcelNumber: null,
    currency: '',
    transportBillNumber: '',
    arrivalPort: '',
    arrivalDate: new Date().toString(),
    weight: null,
    customsEntryCode: '',
    customsEntryCodeDate: new Date().toString(),
    linnDuty: null,
    linnVat: null,
    numCartons: null,
    numPallets: null,
    createdBy: null,
    customsEntryCodePrefix: '',
    totalInvoiceValue: 0,
    parcelsSearchLoading: false,
    totalImportValue: 0,
    suppliersSearchLoading: false,
    carriersSearchLoading: false,
    transportId: 0,
    transactionId: 0,
    pva: ''
};

export default ImpBookTab;
