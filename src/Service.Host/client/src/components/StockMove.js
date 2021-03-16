import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, InputField } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { DataGrid } from '@material-ui/data-grid';
import Page from '../containers/Page';

function StockMove({ parts, fetchParts, partsLoading, clearPartsSearch }) {
    const [partNumber, setPartNumber] = useState(null);

    const partResults = () => {
        return parts?.map(item => ({
            ...item,
            name: item.partNumber.toString(),
            description: item.description,
            href: item.href
        }));
    };

    const getStock = () => {};

    const handleSelectRow = () => {};

    const handleOnSelect = selectedPart => {
        setPartNumber(selectedPart.partNumber);
        getStock();
    };

    const handlePartChange = (_property, value) => {
        setPartNumber(value.toUpperCase());
    };

    const handleOnKeyPress = data => {
        if (data.keyCode === 13 || data.keyCode === 9) {
            getStock();
        }
    };

    const focusProp = { onKeyDown: handleOnKeyPress };
    const columns = [
        {
            field: 'partNumber',
            headerName: 'Article No',
            width: 140
        },
        { field: 'quantity', headerName: 'Qty', width: 100 }
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
                <Grid item xs={3}>
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
                <Grid item xs={6}>
                    <span>Stock</span>
                    <DataGrid
                        rows={[{ id: 2, quantity: 5 }]}
                        columns={columns}
                        density="compact"
                        rowHeight={34}
                        // loading={itemsLoading}
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
    clearPartsSearch: PropTypes.func.isRequired
};

StockMove.defaultProps = {
    partsLoading: false
};

export default StockMove;
