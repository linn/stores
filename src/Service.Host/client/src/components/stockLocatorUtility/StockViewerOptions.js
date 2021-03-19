import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import queryString from 'query-string';
import {
    Dropdown,
    InputField,
    LinkButton,
    Loading,
    Title,
    Typeahead,
    TypeaheadTable
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function StockViewerOptions({
    storageLocations,
    storageLocationsLoading,
    searchStorageLocations,
    clearStorageLocationsSearch,
    stockPools,
    stockPoolsLoading,
    searchStockPools,
    clearStockPoolsSearch,
    stockLocatorBatches,
    stockLocatorBatchesLoading,
    searchStockLocatorBatches,
    clearStockLocatorBatchesSearch,
    inspectedStates,
    inspectedStatesLoading,
    history
}) {
    const [options, setOptions] = useState({
        partNumber: '',
        storageLocation: '',
        locationId: '',
        stockPool: '',
        batchRef: '',
        state: ''
    });

    const table = {
        totalItemCount: stockLocatorBatches.length,
        rows: stockLocatorBatches?.map((item, i) => ({
            id: `${item.id}`,
            values: [
                { id: `${i}-0`, value: `${item.batchRef}` },
                { id: `${i}-1`, value: `${item.partNumber || ''}` },
                { id: `${i}-2`, value: `${item.palletNumber || ''}` },
                { id: `${i}-3`, value: `${item.storagePlaceName || ''}` },
                { id: `${i}-4`, value: `${item.stockRotationDate.split('T')[0] || ''}` }
            ],
            links: item.links
        }))
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Stock Viewer" />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="Part Number"
                        propertyName="partNumber"
                        onChange={(_, newValue) => setOptions({ ...options, partNumber: newValue })}
                        helperText="note: * can be used as a wildcard character"
                        value={options.partNumber}
                    />
                </Grid>
                <Grid item xs={2} />
                <Grid item xs={5}>
                    <LinkButton
                        text="VIEW STOCK LOCATORS"
                        disabled={
                            !options.batchRef &&
                            !options.partNumber &&
                            !(options.storageLocation || options.palletNumber)
                        }
                        to={`/inventory/stock-locator-utility?${queryString.stringify(options)}`}
                    />
                </Grid>
                <Grid item xs={2} />
                <Grid item xs={3}>
                    <InputField
                        label="Pallet Number"
                        type="number"
                        propertyName="palletNumber"
                        onChange={(_, newValue) =>
                            setOptions({ ...options, palletNumber: newValue })
                        }
                        value={options.palletNumber}
                    />
                </Grid>
                <Grid item xs={1}>
                    Or
                </Grid>
                <Grid item xs={3}>
                    <Typeahead
                        items={storageLocations}
                        fetchItems={searchStorageLocations}
                        modal
                        links={false}
                        clearSearch={clearStorageLocationsSearch}
                        loading={storageLocationsLoading}
                        label="Storage Location"
                        title="Search Storage Locations"
                        value={options.storageLocation}
                        onSelect={newValue =>
                            setOptions({
                                ...options,
                                storageLocation: newValue.locationCode,
                                locationId: newValue.id
                            })
                        }
                        history={history}
                        debounce={1000}
                        minimumSearchTermLength={2}
                    />
                </Grid>
                <Grid item xs={5} />
                <Grid item xs={3}>
                    <Typeahead
                        items={stockPools}
                        fetchItems={searchStockPools}
                        modal
                        links={false}
                        clearSearch={clearStockPoolsSearch}
                        loading={stockPoolsLoading}
                        label="Stock Pool"
                        title="Search Stock Pools"
                        value={options.stockPool}
                        onSelect={newValue =>
                            setOptions({ ...options, stockPool: newValue.stockPoolCode })
                        }
                        history={history}
                        debounce={1000}
                        minimumSearchTermLength={2}
                    />
                </Grid>
                <Grid item xs={9} />
                <Grid item xs={12}>
                    <TypeaheadTable
                        table={table}
                        columnNames={['Name', 'Part', 'Pallet', 'Location', 'Date']}
                        fetchItems={searchStockLocatorBatches}
                        modal
                        links={false}
                        clearSearch={clearStockLocatorBatchesSearch}
                        loading={stockLocatorBatchesLoading}
                        label="Batch"
                        title="Search Batch Refs"
                        value={options.batchRef}
                        onSelect={newValue => {
                            setOptions({ ...options, batchRef: newValue?.values?.[0].value });
                        }}
                        history={history}
                        debounce={1000}
                        minimumSearchTermLength={2}
                    />
                </Grid>
                <Grid item xs={3}>
                    {inspectedStatesLoading ? (
                        <Loading />
                    ) : (
                        <Dropdown
                            items={inspectedStates?.map(v => ({
                                id: v.state,
                                displayText: v.description
                            }))}
                            value={options.state}
                            label="State"
                            propertyName="state"
                            fullWidth
                            onChange={(_propertyName, newValue) =>
                                setOptions({ ...options, state: newValue })
                            }
                            allowNoValue
                        />
                    )}
                </Grid>
                <Grid item xs={9} />
            </Grid>
        </Page>
    );
}

StockViewerOptions.propTypes = {
    history: PropTypes.shape({}).isRequired,
    storageLocations: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string
        })
    ),
    storageLocationsLoading: PropTypes.bool,
    searchStorageLocations: PropTypes.func.isRequired,
    clearStorageLocationsSearch: PropTypes.func.isRequired,
    stockPools: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string
        })
    ),
    stockPoolsLoading: PropTypes.bool,
    searchStockPools: PropTypes.func.isRequired,
    clearStockPoolsSearch: PropTypes.func.isRequired,
    stockLocatorBatches: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string
        })
    ),
    stockLocatorBatchesLoading: PropTypes.bool,
    searchStockLocatorBatches: PropTypes.func.isRequired,
    clearStockLocatorBatchesSearch: PropTypes.func.isRequired,
    inspectedStates: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string
        })
    ),
    inspectedStatesLoading: PropTypes.bool
};

StockViewerOptions.defaultProps = {
    storageLocations: [],
    storageLocationsLoading: false,
    inspectedStates: [],
    inspectedStatesLoading: false,
    stockPools: [],
    stockPoolsLoading: false,
    stockLocatorBatches: [],
    stockLocatorBatchesLoading: false
};

export default StockViewerOptions;
