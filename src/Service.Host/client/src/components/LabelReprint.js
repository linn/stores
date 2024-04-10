import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import {
    Title,
    Dropdown,
    InputField,
    Loading,
    Typeahead
} from '@linn-it/linn-form-components-library';
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
    getConsignment,
    searchAddresses,
    clearAddresses,
    addressesSearchResults,
    addressesSearchLoading
}) {
    const [labelOptions, setLabelOptions] = useState({
        labelType: 'Carton',
        consignmentId: null,
        firstItem: null,
        lastItem: null,
        numberOfCopies: 1,
        addressId: null,
        line1: null,
        line2: null,
        line3: null,
        line4: null,
        line5: null
    });

    const [addressInfo, setAddressInfo] = useState({
        addressee: null,
        addressee2: null,
        line1: null,
        line2: null,
        line3: null,
        line4: null
    });

    const handleFieldChange = (propertyName, newValue) => {
        setLabelOptions({ ...labelOptions, [propertyName]: newValue });
        if (propertyName === 'consignmentId' && newValue > 10000) {
            getConsignment(newValue);
        }
    };

    const handleLabelTypeChange = (_propertyName, newValue) => {
        clearConsignmentLabelData();
        if (newValue === 'Address') {
            setLabelOptions({
                ...labelOptions,
                consignmentId: null,
                firstItem: null,
                lastItem: null,
                labelType: newValue,
                line1: null,
                line2: null,
                line3: null,
                line4: null,
                line5: null
            });
        } else if (newValue === 'General') {
            setLabelOptions({
                ...labelOptions,
                consignmentId: null,
                firstItem: null,
                lastItem: null,
                labelType: newValue,
                addressId: null
            });
        } else {
            setLabelOptions({
                ...labelOptions,
                labelType: newValue,
                addressId: null,
                line1: null,
                line2: null,
                line3: null,
                line4: null,
                line5: null
            });
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

    const addressesSearchResultsList = () => {
        return addressesSearchResults?.map(address => ({
            ...address,
            name: address.id,
            description: address.addressee,
            id: address.id
        }));
    };

    const handleOnSelectAddress = selectedAddress => {
        handleFieldChange('addressId', selectedAddress.id);
        setAddressInfo(selectedAddress);
    };

    const doPrintLabel = () => {
        printConsignmentLabel({
            labelType: labelOptions.labelType,
            consignmentId: labelOptions.consignmentId,
            firstItem: labelOptions.firstItem,
            lastItem: labelOptions.lastItem,
            addressId: labelOptions.addressId,
            line1: labelOptions.line1,
            line2: labelOptions.line2,
            line3: labelOptions.line3,
            line4: labelOptions.line4,
            line5: labelOptions.line5,
            numberOfCopies: labelOptions.numberOfCopies,
            userNumber
        });
    };

    const DoNotBreakdownLabel = () => {
        setLabelOptions({
            ...labelOptions,
            line1: 'DO NOT BREAKDOWN',
            line2: 'DO NOT REMOVE',
            line3: 'BOXED',
            line4: null,
            line5: null
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
                            { id: 'General', displayText: 'General Label' },
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
                        <Typeahead
                            label="Address Id"
                            items={addressesSearchResultsList()}
                            placeholder="Address Id"
                            fetchItems={name => searchAddresses(name)}
                            clearSearch={clearAddresses}
                            loading={addressesSearchLoading}
                            debounce={1000}
                            links={false}
                            modal
                            onSelect={p => handleOnSelectAddress(p)}
                            value={labelOptions.addressId}
                        />
                        <Typography>
                            {`${addressInfo.addressee ? addressInfo.addressee : ''}\n
                          ${addressInfo.addressee2 ? addressInfo.addressee2 : ''}\n
                          ${addressInfo.line1 ? addressInfo.line1 : ''}\n
                          ${addressInfo.line2 ? addressInfo.line2 : ''}\n
                          ${addressInfo.line3 ? addressInfo.line3 : ''}\n
                          ${addressInfo.line4 ? addressInfo.line1 : ''}`}
                        </Typography>
                        <br />
                        <InputField
                            label="Additional Info"
                            placeholder="line 1"
                            fullWidth
                            value={labelOptions.line1}
                            onChange={handleFieldChange}
                            propertyName="line1"
                        />
                        <InputField
                            placeholder="line 2"
                            fullWidth
                            value={labelOptions.line2}
                            onChange={handleFieldChange}
                            propertyName="line2"
                        />
                    </Grid>
                )}

                {labelOptions.labelType === 'General' && (
                    <Grid item xs={6}>
                        <InputField
                            placeholder="line 1"
                            fullWidth
                            value={labelOptions.line1}
                            onChange={handleFieldChange}
                            propertyName="line1"
                        />
                        <InputField
                            placeholder="line 2"
                            fullWidth
                            value={labelOptions.line2}
                            onChange={handleFieldChange}
                            propertyName="line2"
                        />
                        <InputField
                            placeholder="line 3"
                            fullWidth
                            value={labelOptions.line3}
                            onChange={handleFieldChange}
                            propertyName="line3"
                        />
                        <InputField
                            placeholder="line 4"
                            fullWidth
                            value={labelOptions.line4}
                            onChange={handleFieldChange}
                            propertyName="line4"
                        />
                        <InputField
                            placeholder="line 5"
                            fullWidth
                            value={labelOptions.line5}
                            onChange={handleFieldChange}
                            propertyName="line5"
                        />
                        <Button onClick={DoNotBreakdownLabel} variant="text">
                            Do not breakdown label
                        </Button>
                    </Grid>
                )}

                <Grid item xs={6} />
                <Grid item xs={4}>
                    <Button
                        style={{ marginTop: '20px', marginBottom: '40px' }}
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
    getConsignment: PropTypes.func.isRequired,
    searchAddresses: PropTypes.func.isRequired,
    clearAddresses: PropTypes.func.isRequired,
    addressesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    addressesSearchLoading: PropTypes.bool
};

LabelReprint.defaultProps = {
    printConsignmentLabelWorking: false,
    printConsignmentLabelResult: null,
    consignmentItem: null,
    consignmentLoading: false,
    addressesSearchResults: [],
    addressesSearchLoading: false
};
