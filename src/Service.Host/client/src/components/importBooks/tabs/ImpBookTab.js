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
                    <InputField
                        label="Vax ref"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="vaxRef"
                        value={vaxRef}
                    />
                </Grid>
                <Grid item xs={6} />

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
                        label="Parcel Number"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="parcelNumber"
                        value={parcelNumber}
                    />
                </Grid>

                <Grid item xs={6}>
                    {/* todo might add new param to link button shared component to open this in a new tab */}
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
                    {/* TODO set this based on supplier country being an EC member*/}
                    <InputField
                        label="EC member"
                        // value={supplierECMember()}
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
                        required
                    />
                </Grid>

                <Grid item xs={4}>
                    {/* Todo change this to a search/typeahead and implement rest of required stuff
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
                    {/* Todo implement exchange rate lookup actions on rest of frontend, actions etc */}
                    <InputField
                        fullWidth
                        value={exchangeRate}
                        label="Exchange Rate"
                        maxLength={8}
                        onChange={handleFieldChange}
                        propertyName="exchangeRate"
                        disabled
                    />
                </Grid>

                <Grid item xs={6}>
                    <InputField label="Total Import Value" value={totalImportValue} fullwidth />
                </Grid>
                <Grid item xs={2} />

                <Grid item xs={4} className={classes.negativeTopMargin}>
                    <LinkButton
                        text="Exchange Rates"
                        // to={''}
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
                        />
                    </div>
                    <div className={classes.marginTop1}>
                        <Tooltip title="Clear Supplier search">
                            <Button variant="outlined" onClick={clearCarrier}>
                                X
                            </Button>
                        </Tooltip>
                    </div>
                </Grid>

                <Grid item xs={6}>
                    <InputField
                        label="Carrier Invoice No"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="carrierInvNumber"
                        value={carrierInvNumber}
                    />
                </Grid>

                <Grid item xs={6}>
                    <SearchInputField
                        label="carrier Invoice Date"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="carrierInvDate"
                        type="date"
                        value={carrierInvDate}
                    />
                </Grid>

                <Grid item xs={6}>
                    <Dropdown
                        items={transportCodes.map(e => ({
                            displayText: `${e.transportId} (${e.description})`,
                            id: parseInt(e.transportId, 10)
                        }))}
                        propertyName="transportId"
                        fullWidth
                        value={transportId}
                        label="Mode of Transport"
                        onChange={handleFieldChange}
                    />
                </Grid>

                <Grid item xs={6}>
                    <InputField
                        label="Transport Bill Number"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="transportBillNumber"
                        value={transportBillNumber}
                    />
                </Grid>

                <Grid item xs={6}>
                    <Dropdown
                        items={transactionCodes.map(e => ({
                            displayText: `${e.transactionId}`,
                            id: parseInt(e.transactionId, 10)
                        }))}
                        propertyName="transactionId"
                        fullWidth
                        value={transactionId}
                        label="Transaction Code"
                        onChange={handleFieldChange}
                    />
                </Grid>

                <Grid item xs={6}>
                    <Dropdown
                        items={deliveryTerms.map(e => ({
                            displayText: `${e.deliveryTermCode} (${e.description})`,
                            id: parseInt(e.deliveryTermCode, 10)
                        }))}
                        propertyName="deliveryTermCode"
                        fullWidth
                        value={deliveryTermCode}
                        label="Delivery Term Code"
                        onChange={handleFieldChange}
                    />
                </Grid>

                <Grid item xs={6}>
                    <Dropdown
                        items={ports.map(e => ({
                            displayText: `${e.portCode} (${e.description})`,
                            id: parseInt(e.portCode, 10)
                        }))}
                        propertyName="arrivalPort"
                        fullWidth
                        value={arrivalPort}
                        label="Arrival Port"
                        onChange={handleFieldChange}
                    />
                </Grid>

                <Grid item xs={6}>
                    <InputField
                        label="Flight Number"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="flightNumber"
                        value={flightNumber}
                    />
                </Grid>

                <Grid item xs={6}>
                    <InputField
                        label="HWB"
                        fullWidth
                        onChange={handleFieldChange}
                        propertyName="hwb"
                        value={hwb}
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
                    />
                </Grid>
                <Grid item xs={9} />
            </Grid>

            <Grid container spacing={1} item xs={1} />

            <Grid container spacing={1} item xs={4}>
                <Grid item xs={12}>
                    <InputField
                        label="Freight Charges"
                        value={freightCharges}
                        onChange={handleFieldChange}
                        type="number"
                        propertyName="freightCharges"
                        fullwidth
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        label="Handling Charge"
                        value={handlingCharge}
                        onChange={handleFieldChange}
                        type="number"
                        propertyName="handlingCharge"
                        fullwidth
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        label="Clearance Charge"
                        value={clearanceCharge}
                        onChange={handleFieldChange}
                        propertyName="clearanceCharge"
                        type="number"
                        fullwidth
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        label="Cartage"
                        value={cartage}
                        onChange={handleFieldChange}
                        type="number"
                        propertyName="cartage"
                        fullwidth
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        label="Storage"
                        value={storage}
                        onChange={handleFieldChange}
                        type="number"
                        propertyName="storage"
                        fullwidth
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        label="Duty"
                        value={duty}
                        onChange={handleFieldChange}
                        propertyName="duty"
                        type="number"
                        fullwidth
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        label="Vat"
                        value={vat}
                        onChange={handleFieldChange}
                        propertyName="vat"
                        type="number"
                        fullwidth
                    />
                </Grid>
                <Grid item xs={12}>
                    <InputField
                        label="Misc"
                        value={misc}
                        onChange={handleFieldChange}
                        propertyName="misc"
                        type="number"
                        fullwidth
                    />
                </Grid>

                <Grid item xs={4}>
                    <InputField
                        label="Net total (inv)"
                        value={carriersInvTotal}
                        onChange={handleFieldChange}
                        propertyName="carriersInvTotal"
                        fullwidth
                        type="number"
                    />
                </Grid>

                <Grid item xs={4}>
                    <InputField
                        label="Freight Vat"
                        value={carriersVatTotal}
                        onChange={handleFieldChange}
                        propertyName="carriersVatTotal"
                        fullwidth
                        type="number"
                    />
                </Grid>

                <Grid item xs={4}>
                    <InputField
                        label="Grand total"
                        value={carriersInvTotal + carriersVatTotal}
                        fullwidth
                        type="number"
                    />
                </Grid>

                <Grid item xs={4}>
                    <InputField
                        label="Number of Cartons"
                        value={numCartons}
                        onChange={handleFieldChange}
                        propertyName="numCartons"
                        fullwidth
                        type="number"
                    />
                </Grid>
                <Grid item xs={4}>
                    <InputField
                        label="Number of Pallets"
                        value={numPallets}
                        onChange={handleFieldChange}
                        propertyName="numPallets"
                        fullwidth
                        type="number"
                    />
                </Grid>
                <Grid item xs={4}>
                    <InputField
                        label="Weight"
                        value={weight}
                        onChange={handleFieldChange}
                        propertyName="weight"
                        fullwidth
                        type="number"
                    />
                </Grid>

                <Grid item xs={2}>
                    <InputField
                        label="Prefix"
                        value={customsEntryCodePrefix}
                        onChange={handleFieldChange}
                        propertyName="customsEntryCodePrefix"
                        fullwidth
                    />
                </Grid>
                <Grid item xs={10}>
                    <InputField
                        label="Customs Entry Code"
                        value={customsEntryCode}
                        onChange={handleFieldChange}
                        propertyName="customsEntryCode"
                        fullwidth
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
    allSuppliers: [{ id: 0, name: 'loading', country: 'loading' }],
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
