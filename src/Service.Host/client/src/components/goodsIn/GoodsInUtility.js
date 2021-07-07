import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { InputField, Typeahead } from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function GoodsInUtility({
    validatePurchaseOrder,
    validatePurchaseOrderResult,
    validatePurchaseOrderResultLoading,
    searchDemLocations,
    demLocationsSearchLoading,
    demLocationsSearchResults,
    searchStoragePlaces,
    storagePlacesSearchResults,
    storagePlacesSearchLoading,
    searchSalesArticles,
    salesArticlesSearchResults,
    salesArticlesSearchLoading
}) {
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
                        value={
                            validatePurchaseOrderResultLoading
                                ? 'loading'
                                : validatePurchaseOrderResult?.bookInMessage
                        }
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
                        textFieldProps={{
                            onBlur: () => validatePurchaseOrder(formData.purchaseOrderNumber)
                        }}
                    />
                </Grid>
                <Grid item xs={8} />
                <Grid item xs={4}>
                    <Typeahead
                        onSelect={newValue => {
                            handleFieldChange('demLocation', newValue.name);
                        }}
                        label="Dem Location"
                        modal
                        items={demLocationsSearchResults}
                        value={formData?.demLocation}
                        loading={demLocationsSearchLoading}
                        fetchItems={searchDemLocations}
                        links={false}
                        clearSearch={() => {}}
                        placeholder="Search Locations"
                    />
                </Grid>
                <Grid item xs={4}>
                    <Typeahead
                        onSelect={newValue => {
                            handleFieldChange('ontoLocation', newValue.name);
                        }}
                        label="Onto Location"
                        modal
                        items={storagePlacesSearchResults}
                        value={formData?.ontoLocation}
                        loading={storagePlacesSearchLoading}
                        fetchItems={searchStoragePlaces}
                        links={false}
                        clearSearch={() => {}}
                        placeholder="Search Locations"
                        minimumSearchTermLength={3}
                    />
                </Grid>
                <Grid item xs={4}>
                    <Typeahead
                        onSelect={newValue => {
                            handleFieldChange('salesArticle', newValue.name);
                        }}
                        label="Sales Article"
                        modal
                        items={salesArticlesSearchResults}
                        value={formData?.salesArticle}
                        loading={salesArticlesSearchLoading}
                        fetchItems={searchSalesArticles}
                        links={false}
                        clearSearch={() => {}}
                        placeholder="Search Articles"
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

GoodsInUtility.propTypes = {
    validatePurchaseOrderResult: PropTypes.shape({
        bookInMessage: PropTypes.string
    }),
    validatePurchaseOrderResultLoading: PropTypes.bool,
    searchDemLocations: PropTypes.func.isRequired,
    validatePurchaseOrder: PropTypes.func.isRequired,
    demLocationsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    demLocationsSearchLoading: PropTypes.bool,
    searchSalesArticles: PropTypes.func.isRequired,
    salesArticlesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    salesArticlesSearchLoading: PropTypes.bool,
    searchStoragePlaces: PropTypes.func.isRequired,
    storagePlacesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    storagePlacesSearchLoading: PropTypes.bool
};

GoodsInUtility.defaultProps = {
    validatePurchaseOrderResult: null,
    validatePurchaseOrderResultLoading: false,
    demLocationsSearchResults: [],
    demLocationsSearchLoading: false,
    salesArticlesSearchResults: [],
    salesArticlesSearchLoading: false,
    storagePlacesSearchResults: [],
    storagePlacesSearchLoading: false
};

export default GoodsInUtility;
