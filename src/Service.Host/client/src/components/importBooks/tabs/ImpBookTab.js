import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Tooltip from '@material-ui/core/Tooltip';
import Grid from '@material-ui/core/Grid';
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
    allSuppliers,
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
    countries,
    currencies,
    handleUpdateInvoiceDetails,
    invoiceDetails,
    totalInvoiceValue,
    searchParcels,
    clearParcelsSearch,
    parcelsSearchResults,
    parcelsSearchLoading
}) {
    const [localSuppliers, setLocalSuppliers] = useState([{}]);

    useEffect(() => {
        if (allSuppliers) {
            setLocalSuppliers([...allSuppliers]);
        }
    }, [allSuppliers]);

    const supplierCountryValue = () => {
        if (localSuppliers.length && supplierId) {
            const tempSupplier = localSuppliers.find(x => x.id === supplierId);
            if (!tempSupplier) {
                return '-';
            }
            return tempSupplier.countryCode;
        }
        if (!supplierId) {
            return '';
        }

        return 'loading..';
    };

    const supplierNameValue = () => {
        if (localSuppliers.length && supplierId) {
            const tempSupplier = localSuppliers.find(x => x.id === supplierId);
            if (!tempSupplier) {
                return 'undefined supplier';
            }
            return tempSupplier.name;
        }
        if (!supplierId) {
            return '';
        }
        return 'loading..';
    };

    const carrierNameValue = () => {
        if (localSuppliers.length && carrierId) {
            const tempCarrier = localSuppliers.find(x => x.id === carrierId);
            if (!tempCarrier) {
                return 'undefined carrier';
            }
            return tempCarrier.name;
        }
        if (!carrierId) {
            return '';
        }

        return 'loading..';
    };

    const countryIsInEU = () => {
        if (localSuppliers.length && supplierId) {
            const tempSupplier = localSuppliers.find(x => x.id === supplierId);
            if (!tempSupplier) {
                return '';
            }
            const country = countries.find(x => x.countryCode === tempSupplier.countryCode);
            return country?.eCMember;
        }
        if (!supplierId) {
            return '';
        }
        return 'loading..';
    };

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

    const handleParcelChange = parcel => {
        handleFieldChange('parcelNumber', parcel.parcelNumber);
        //todo autopopulate all parcel fields
        //weight, etc
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
                    />
                </Grid>

                <Grid item xs={6}>
                    <div className={classes.displayInline}>
                        <Typeahead
                            label="Parcel Number"
                            title="Search for a parcel"
                            onSelect={handleParcelChange}
                            items={parcelsSearchResults}
                            loading={parcelsSearchLoading}
                            fetchItems={searchParcels}
                            clearSearch={() => clearParcelsSearch}
                            value={parcelNumber}
                            modal
                            links={false}
                            // history={history}
                            debounce={1000}
                            minimumSearchTermLength={2}
                            required
                            disabled={!allowedToEdit}
                        />
                    </div>
                    <div className={classes.marginTop1}>
                        <Tooltip title="Clear Parcel search">
                            <Button
                                variant="outlined"
                                onClick={clearParcel}
                                disabled={!allowedToEdit}
                            >
                                X
                            </Button>
                        </Tooltip>
                    </div>
                </Grid>

                <Grid item xs={6}>
                    <LinkButton
                        text="View Parcel"
                        to={`/logistics/parcels/${parcelNumber}`}
                        external
                    />
                </Grid>

                <Grid item xs={6}>
                    <div className={classes.displayInline}>
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
                            // history={history}
                            debounce={1000}
                            minimumSearchTermLength={2}
                            required
                            disabled={!allowedToEdit}
                        />
                    </div>
                    <div className={classes.marginTop1}>
                        <Tooltip title="Clear Supplier search">
                            <Button
                                variant="outlined"
                                onClick={clearSupplier}
                                disabled={!allowedToEdit}
                            >
                                X
                            </Button>
                        </Tooltip>
                    </div>
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="Supplier Country"
                        value={supplierCountryValue()}
                        disabled
                        fullwidth
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField label="EC (EU) member" value={countryIsInEU()} disabled fullwidth />
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
                        propertyName="Currency"
                        fullWidth
                        value={currency}
                        label="Currency"
                        onChange={handleFieldChange}
                        disabled={!allowedToEdit}
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        label="Total Import Value"
                        value={totalImportValue}
                        onChange={handleFieldChange}
                        propertyName="totalImportValue"
                        fullwidth
                        disabled={!allowedToEdit}
                    />
                </Grid>
                <Grid item xs={2} />

                <Grid item xs={12} className={classes.gapAbove}>
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
                        label="Total Invoice Value"
                        value={totalInvoiceValue}
                        onChange={handleFieldChange}
                        propertyName="totalImportValue"
                        fullwidth
                        type="number"
                        maxLength={20}
                        decimalPlaces={2}
                        disabled
                    />
                </Grid>
                <Grid item xs={12} className={classes.gapAbove}>
                    <div className={classes.displayInline}>
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
                        />
                    </div>
                    <div className={classes.marginTop1}>
                        <Tooltip title="Clear Supplier search">
                            <Button
                                variant="outlined"
                                onClick={clearCarrier}
                                disabled={!allowedToEdit}
                            >
                                X
                            </Button>
                        </Tooltip>
                    </div>
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
    employees: PropTypes.arrayOf(PropTypes.shape({})),
    suppliersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({ id: PropTypes.number, name: PropTypes.string, country: PropTypes.string })
    ).isRequired,
    suppliersSearchLoading: PropTypes.bool.isRequired,
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired,
    allSuppliers: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string,
            country: PropTypes.string
        })
    ),
    carriersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({ id: PropTypes.number, name: PropTypes.string, country: PropTypes.string })
    ).isRequired,
    carriersSearchLoading: PropTypes.bool.isRequired,
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
    transportId: PropTypes.number.isRequired,
    transportBillNumber: PropTypes.string,
    transactionId: PropTypes.number.isRequired,
    deliveryTermCode: PropTypes.string.isRequired,
    arrivalPort: PropTypes.string,
    arrivalDate: PropTypes.string,
    totalImportValue: PropTypes.number.isRequired,
    weight: PropTypes.number,
    customsEntryCode: PropTypes.string,
    customsEntryCodeDate: PropTypes.string,
    linnDuty: PropTypes.number,
    linnVat: PropTypes.number,
    numCartons: PropTypes.number,
    numPallets: PropTypes.number,
    createdBy: PropTypes.number,
    customsEntryCodePrefix: '',
    allowedToEdit: PropTypes.bool.isRequired,
    countries: PropTypes.arrayOf(PropTypes.shape({})),
    invoiceDetails: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    currencies: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    handleUpdateInvoiceDetails: PropTypes.func.isRequired,
    totalInvoiceValue: PropTypes.number,
    searchParcels: PropTypes.func.isRequired,
    clearParcelsSearch: PropTypes.func.isRequired,
    parcelsSearchResults: PropTypes.shape({ id: PropTypes.number, name: PropTypes.string })
        .isRequired,
    parcelsSearchLoading: PropTypes.bool.isRequired
};

ImpBookTab.defaultProps = {
    employees: [{ id: '-1', fullname: 'loading..' }],
    allSuppliers: [{ id: 0, name: 'loading', country: 'loading' }],
    parcelNumber: null,
    currency: '',
    transportBillNumber: '',
    arrivalPort: '',
    arrivalDate: new Date(),
    weight: null,
    customsEntryCode: '',
    customsEntryCodeDate: new Date(),
    linnDuty: null,
    linnVat: null,
    numCartons: null,
    numPallets: null,
    createdBy: null,
    customsEntryCodePrefix: '',
    countries: [{ id: '-1', countryCode: 'loading..' }],
    totalInvoiceValue: 0
};

export default ImpBookTab;
