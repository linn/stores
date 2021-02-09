import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Dropdown,
    Loading,
    Title,
    Typeahead,
    TypeaheadTable
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function StockViewerOptions({
    parts,
    partsLoading,
    searchParts,
    clearPartsSearch,
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
    const [partNumber, setPartNumber] = useState('');
    const [storageLocation, setStorageLocation] = useState('');
    const [stockPool, setStockPool] = useState('');
    const [batcheRef, setBatchRef] = useState('');
    const [inspectedState, setInspectedState] = useState('');

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
                    <Typeahead
                        items={parts}
                        fetchItems={searchParts}
                        modal
                        links={false}
                        clearSearch={clearPartsSearch}
                        loading={partsLoading}
                        label="Part Number"
                        title="Search Parts"
                        value={partNumber}
                        onSelect={newValue => setPartNumber(newValue.partNumber)}
                        history={history}
                        debounce={1000}
                        minimumSearchTermLength={2}
                    />
                </Grid>
                <Grid item xs={9} />

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
                        value={storageLocation}
                        onSelect={newValue => setStorageLocation(newValue.locationCode)}
                        history={history}
                        debounce={1000}
                        minimumSearchTermLength={2}
                    />
                </Grid>
                <Grid item xs={9} />
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
                        value={stockPool}
                        onSelect={newValue => setStockPool(newValue.stockPoolCode)}
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
                        value={batcheRef}
                        onSelect={newValue => {
                            setBatchRef(newValue.values[0].value);
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
                            value={inspectedState}
                            label="State"
                            propertyName="inspectedState"
                            fullWidth
                            onChange={(_propertyName, newValue) => setInspectedState(newValue)}
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
    parts: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string
        })
    ),
    partsLoading: PropTypes.bool,
    history: PropTypes.shape({}).isRequired,
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired,
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
    partsLoading: false,
    parts: [],
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
