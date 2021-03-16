import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, InputField } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { DataGrid } from '@material-ui/data-grid';
import Page from '../containers/Page';

function StockMove({
    parts,
    fetchParts,
    partsLoading,
    clearPartsSearch,
    availableStock,
    availableStockLoading,
    fetchAvailableStock
}) {
    const [partNumber, setPartNumber] = useState(null);

    const partResults = () => {
        return parts?.map(item => ({
            ...item,
            name: item.partNumber.toString(),
            description: item.description,
            href: item.href
        }));
    };

    const handleSelectRow = () => {};

    const handleOnSelect = selectedPart => {
        setPartNumber(selectedPart.partNumber);
        fetchAvailableStock(selectedPart.partNumber);
    };

    const handlePartChange = (_property, value) => {
        setPartNumber(value.toUpperCase());
    };

    const handleOnKeyPress = data => {
        if (data.keyCode === 13 || data.keyCode === 9) {
            fetchAvailableStock(partNumber);
        }
    };

    const displayAvailableStock = stock => {
        if (!stock) {
            return [];
        }

        return stock.map((s, i) => ({ id: i, ...s }));
    };

    const focusProp = { onKeyDown: handleOnKeyPress };
    const columns = [
        { field: 'quantityAvailable', headerName: 'Qty', width: 100 },
        { field: 'displayLocation', headerName: 'Location', width: 140 },
        { field: 'stockRotationDate', type: 'date', headerName: 'Stock Rot Date', width: 140 },
        { field: 'stockPoolCode', headerName: 'Stock Pool', width: 140 },
        { field: 'state', headerName: 'State', width: 140 }
    ];

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={3}>
                    <InputField
                        value={partNumber}
                        label="Part Number"
                        onChange={handlePartChange}
                        maxLength={14}
                        propertyName="partNumber"
                        textFieldProps={focusProp}
                    />
                </Grid>
                <Grid item xs={2}>
                    <Typeahead
                        items={partResults()}
                        fetchItems={fetchParts}
                        clearSearch={clearPartsSearch}
                        loading={partsLoading}
                        label="Search For Part Number"
                        debounce={1000}
                        links={false}
                        modal
                        onSelect={p => handleOnSelect(p)}
                        placeholder="Part Search"
                    />
                </Grid>
                <Grid item xs={7}>
                    <span>Stock</span>
                    <DataGrid
                        rows={displayAvailableStock(availableStock)}
                        columns={columns}
                        density="compact"
                        rowHeight={34}
                        loading={availableStockLoading}
                        hideFooter
                        autoHeight
                        onSelectionChange={handleSelectRow}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

StockMove.propTypes = {
    parts: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string,
            href: PropTypes.string
        })
    ).isRequired,
    partsLoading: PropTypes.bool,
    fetchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired,
    availableStock: PropTypes.arrayOf(PropTypes.shape({})),
    availableStockLoading: PropTypes.bool,
    fetchAvailableStock: PropTypes.func.isRequired
};

StockMove.defaultProps = {
    partsLoading: false,
    availableStock: [],
    availableStockLoading: false
};

export default StockMove;
