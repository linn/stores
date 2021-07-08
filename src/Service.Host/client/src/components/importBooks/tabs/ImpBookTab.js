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
    SearchInputField
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';

function ImpBookTab({
    employees,
    suppliersSearchResults,
    suppliersSearchLoading,
    searchSuppliers,
    clearSuppliersSearch,
    allSuppliers,

    handleFieldChange,
    dateCreated,
    parcelNumber,
    supplierId,
    foreignCurrency,
    currency,
    carrier,
    OldArrivalPort,
    flightNumber,
    transportId,
    transportBillNumber,
    transactionId,
    deliveryTermCode,
    arrivalPort,
    lineVatTotal,
    hwb,
    supplierCostCurrency,
    transNature,
    arrivalDate,
    freightCharges,
    handlingCharge,
    clearanceCharge,
    cartage,
    duty,
    vat,
    misc,
    carriersInvTotal,
    carriersVatTotal,
    totalImportValue,
    pieces,
    weight,
    customsEntryCode,
    customsEntryCodeDate,
    linnDuty,
    linnVat,
    iprCpcNumber,
    eecgNumber,
    dateCancelled,
    cancelledBy,
    cancelledReason,
    carrierInvNumber,
    carrierInvDate,
    countryOfOrigin,
    fcName,
    vaxRef,
    storage,
    numCartons,
    numPallets,
    comments,
    exchangeRate,
    exchangeCurrency,
    baseCurrency,
    periodNumber,
    createdBy,
    portCode,
    customsEntryCodePrefix
}) {
    const [supplier, setSupplier] = useState({ id: -1, name: 'loading', country: 'loading' });

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

    const clearSupplier = () => {
        handleFieldChange('supplierId', '');
    };

    const handleSupplierChange = supplierParam => {
        handleFieldChange('supplierId', supplierParam.id);
    };

    const useStyles = makeStyles(theme => ({
        displayInline: {
            display: 'inline'
        },
        marginTop1: {
            marginTop: theme.spacing(1),
            display: 'inline-block',
            width: '2em'
        }
    }));
    const classes = useStyles();

    return (
        <>
            <Grid container spacing={1} item xs={6}>
                <Grid item xs={6}>
                    <SearchInputField
                        label="Date Created"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="dateCreated"
                        type="date"
                        value={dateCreated}
                        required
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
                    />
                </Grid>

                <Grid item xs={6}>
                    <InputField
                        label="Vax ref"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="vaxRef"
                        value={vaxRef}
                        required
                    />
                </Grid>
                <Grid item xs={6} />

                <Grid item xs={6}>
                    <InputField
                        label="Parcel Number"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="parcelNumber"
                        value={parcelNumber}
                        required
                    />
                </Grid>
                <Grid item xs={6}>
                    <LinkButton text="View Parcel" to={`/logistics/parcels/${parcelNumber}`} />
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
                        />
                    </div>
                    <div className={classes.marginTop1}>
                        <Tooltip title="Clear Supplier search">
                            <Button variant="outlined" onClick={clearSupplier}>
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
                    {/* TODO look up how this should actually be calculated and set, not sure eec member is based on eecgnumber */}
                    <InputField
                        label="EEC member"
                        value={eecgNumber ? 'Yes' : 'No'}
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
                        propertyName="foreignCurrency"
                        fullWidth
                        value={foreignCurrency}
                        label="Foreign Currency"
                        onChange={handleFieldChange}
                    />
                </Grid>

                <Grid item xs={4}>
                    {/* Todo change this to a search/typeahead and implement rest of required stuff
                    also should this be currency or foreign currency?
                     */}
                    <InputField
                        fullWidth
                        value={currency}
                        label="Currency"
                        maxLength={8}
                        onChange={handleFieldChange}
                        propertyName="currency"
                    />
                </Grid>

                <Grid item xs={4}>
                    {/* Todo implement exchange rate lookup - might need to be its own popup function */}
                    <InputField
                        fullWidth
                        value={exchangeRate}
                        label="Exchange Rate"
                        maxLength={8}
                        onChange={handleFieldChange}
                        propertyName="exchangeRate"
                    />
                </Grid>

                <Grid item xs={6}>
                    <InputField label="Total Import Value" value={totalImportValue} fullwidth />
                </Grid>

                {/* ,
    supplierId,
    foreignCurrency,
    currency,
    carrier,
    OldArrivalPort,
    flightNumber,
    transportId,
    transportBillNumber,
    transactionId,
    deliveryTermCode,
    arrivalPort,
    lineVatTotal,
    hwb,
    supplierCostCurrency,
    transNature,
    arrivalDate,

    
    carriersInvTotal,
    carriersVatTotal,
    totalImportValue,
    pieces,
    weight,
    customsEntryCode,
    customsEntryCodeDate,
    linnDuty,
    linnVat,
    iprCpcNumber,
    eecgNumber,
    dateCancelled,
    cancelledBy,
    cancelledReason,
    carrierInvNumber,
    carrierInvDate,
    countryOfOrigin,
    fcName,
    storage,
    numCartons,
    numPallets,
    comments,
    exchangeRate,
    exchangeCurrency,
    baseCurrency,
    periodNumber,
    ,
    portCode,
    customsEntryCodePrefix */}

                <Grid item xs={9} />
            </Grid>

            <Grid container spacing={1} item xs={1} />

            <Grid container spacing={1} item xs={5}>
                <Grid item xs={6}>
                    <InputField label="Freight Charges" value={freightCharges} fullwidth />
                </Grid>
                <Grid item xs={6}>
                    <InputField label="Handling Charge" value={handlingCharge} fullwidth />
                </Grid>
                <Grid item xs={6}>
                    <InputField label="Clearance Charge" value={clearanceCharge} fullwidth />
                </Grid>
                <Grid item xs={6}>
                    <InputField label="Cartage" value={cartage} fullwidth />
                </Grid>
                <Grid item xs={6}>
                    <InputField label="Storage" value={storage} fullwidth />
                </Grid>
                <Grid item xs={6}>
                    <InputField label="Duty" value={duty} fullwidth />
                </Grid>
                <Grid item xs={6}>
                    <InputField label="Vat" value={vat} fullwidth />
                </Grid>
                <Grid item xs={6}>
                    <InputField label="Misc" value={misc} fullwidth />
                </Grid>

                {/* <Grid item xs={6}>
                    <InputField label="Net total" value={netTotal} fullwidth />
                </Grid> */}
            </Grid>
        </>
    );
}

ImpBookTab.propTypes = {
    employees: PropTypes.arrayOf(PropTypes.shape({})),
    suppliersSearchResults: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    suppliersSearchLoading: PropTypes.bool.isRequired,
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired,
    supplierItem: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string,
            country: PropTypes.string
        })
    ),
    getSupplier: PropTypes.func.isRequired,

    handleFieldChange: PropTypes.func.isRequired,
    dateCreated: PropTypes.string.isRequired,
    parcelNumber: PropTypes.number,
    supplierId: PropTypes.number.isRequired,
    foreignCurrency: PropTypes.string.isRequired,
    currency: PropTypes.string,
    carrier: PropTypes.number.isRequired,
    OldArrivalPort: PropTypes.string,
    flightNumber: PropTypes.string,
    transportId: PropTypes.number.isRequired,
    transportBillNumber: PropTypes.string,
    transactionId: PropTypes.number.isRequired,
    deliveryTermCode: PropTypes.string.isRequired,
    arrivalPort: PropTypes.string,
    lineVatTotal: PropTypes.number,
    hwb: PropTypes.string,
    supplierCostCurrency: PropTypes.string,
    transNature: PropTypes.string,
    arrivalDate: PropTypes.string,
    freightCharges: PropTypes.number,
    handlingCharge: PropTypes.number,
    clearanceCharge: PropTypes.number,
    cartage: PropTypes.number,
    duty: PropTypes.number,
    vat: PropTypes.number,
    misc: PropTypes.number,
    carriersInvTotal: PropTypes.number,
    carriersVatTotal: PropTypes.number,
    totalImportValue: PropTypes.number.isRequired,
    pieces: PropTypes.number,
    weight: PropTypes.number,
    customsEntryCode: PropTypes.string,
    customsEntryCodeDate: PropTypes.string,
    linnDuty: PropTypes.number,
    linnVat: PropTypes.number,
    iprCpcNumber: PropTypes.number,
    eecgNumber: PropTypes.number,
    dateCancelled: PropTypes.string,
    cancelledBy: PropTypes.number,
    cancelledReason: PropTypes.string,
    carrierInvNumber: PropTypes.string,
    carrierInvDate: PropTypes.string,
    countryOfOrigin: PropTypes.string,
    fcName: PropTypes.string,
    vaxRef: PropTypes.string,
    storage: PropTypes.number,
    numCartons: PropTypes.number,
    numPallets: PropTypes.number,
    comments: PropTypes.string,
    exchangeRate: PropTypes.number,
    exchangeCurrency: PropTypes.string,
    baseCurrency: PropTypes.string,
    periodNumber: PropTypes.number,
    createdBy: PropTypes.number,
    portCode: PropTypes.string,
    customsEntryCodePrefix: ''
};

ImpBookTab.defaultProps = {
    employees: [{ id: '-1', fullname: 'loading..' }],
    supplierItem: { id: 0, name: '', country: '' },
    parcelNumber: null,
    currency: '',
    OldArrivalPort: '',
    flightNumber: '',
    transportBillNumber: '',
    arrivalPort: '',
    lineVatTotal: null,
    hwb: '',
    supplierCostCurrency: '',
    transNature: '',
    arrivalDate: new Date(),
    freightCharges: null,
    handlingCharge: null,
    clearanceCharge: null,
    cartage: null,
    duty: null,
    vat: null,
    misc: null,
    carriersInvTotal: null,
    carriersVatTotal: null,
    pieces: null,
    weight: null,
    customsEntryCode: '',
    customsEntryCodeDate: new Date(),
    linnDuty: null,
    linnVat: null,
    iprCpcNumber: null,
    eecgNumber: null,
    dateCancelled: null,
    cancelledBy: null,
    cancelledReason: '',
    carrierInvNumber: '',
    carrierInvDate: new Date(),
    countryOfOrigin: '',
    fcName: '',
    vaxRef: '',
    storage: null,
    numCartons: null,
    numPallets: null,
    comments: '',
    exchangeRate: null,
    exchangeCurrency: '',
    baseCurrency: '',
    periodNumber: null,
    createdBy: null,
    portCode: '',
    customsEntryCodePrefix: ''
};

export default ImpBookTab;
