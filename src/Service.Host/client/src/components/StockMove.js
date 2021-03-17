import React, { useState, useRef } from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, InputField } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { DataGrid } from '@material-ui/data-grid';
import Button from '@material-ui/core/Button';
import moment from 'moment';
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
    const [moveDetails, setMoveDetails] = useState({});
    const [selectedRow, setSelectedRow] = useState(null);

    const toInput = useRef(null);

    const partResults = () => {
        return parts?.map(item => ({
            ...item,
            name: item.partNumber.toString(),
            description: item.description,
            href: item.href
        }));
    };

    const setFromDetailsFromAvailableStock = row => {
        setMoveDetails({
            ...moveDetails,
            from: row.displayLocation,
            locationCode: row.locationCode,
            state: row.state,
            stockPoolCode: row.stockPoolCode,
            palletNumber: row.palletNumber,
            locationId: row.locationId,
            stockRotationDate: moment(row.stockRotationDate).format('DD MMM YYYY')
        });

        toInput.current.focus();
    };

    const setToDetailsFromAvailableStock = row => {
        setMoveDetails({
            ...moveDetails,
            to: row.displayLocation
        });

        toInput.current.focus();
    };

    const handleFromButtonClick = () => {
        if (selectedRow) {
            setFromDetailsFromAvailableStock(selectedRow);
        }
    };

    const handleToButtonClick = () => {
        if (selectedRow) {
            setToDetailsFromAvailableStock(selectedRow);
        }
    };

    const handleSelectRow = row => {
        setSelectedRow(availableStock[row.rowIds[0]]);

        if (!moveDetails.from) {
            setFromDetailsFromAvailableStock(availableStock[row.rowIds[0]]);
        }
    };

    const handleOnSelect = selectedPart => {
        setMoveDetails({ partNumber: selectedPart.partNumber, ...moveDetails });
        fetchAvailableStock(selectedPart.partNumber);
    };

    const handleFieldChange = (property, value) => {
        setMoveDetails({ ...moveDetails, [property]: value.toUpperCase() });
    };

    const handleFieldNumberChange = (property, value) => {
        setMoveDetails({ ...moveDetails, [property]: value });
    };

    const handleOnKeyPress = data => {
        if (data.keyCode === 13 || data.keyCode === 9) {
            fetchAvailableStock(moveDetails.partNumber);
        }
    };

    const displayAvailableStock = stock => {
        if (!stock) {
            return [];
        }

        return stock.map((s, i) => ({ id: i, ...s }));
    };

    const handleMoveClick = () => {};

    const onKeyDownProp = { onKeyDown: handleOnKeyPress };

    const columns = [
        { field: 'quantityAvailable', headerName: 'Qty', width: 100 },
        { field: 'displayLocation', headerName: 'Location', width: 140 },
        {
            field: 'stockRotationDate',
            type: 'date',
            headerName: 'Stock Rot Date',
            width: 140,
            valueGetter: params => moment(params.row.stockRotationDate).format('DD MMM YYYY')
        },
        { field: 'stockPoolCode', headerName: 'Stock Pool', width: 140 },
        { field: 'state', headerName: 'State', width: 140 }
    ];

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={5} />
                <Grid item xs={7}>
                    <span>Stock</span>
                    <div style={{ height: 180, width: '100%' }}>
                        <DataGrid
                            rows={displayAvailableStock(availableStock)}
                            columns={columns}
                            density="compact"
                            rowHeight={34}
                            loading={availableStockLoading}
                            hideFooter
                            onSelectionChange={handleSelectRow}
                        />
                    </div>
                </Grid>
                <Grid item xs={5}>
                    <Typeahead
                        items={partResults()}
                        fetchItems={fetchParts}
                        clearSearch={clearPartsSearch}
                        loading={partsLoading}
                        debounce={1000}
                        links={false}
                        modal
                        onSelect={p => handleOnSelect(p)}
                        placeholder="Search For Part Number"
                    />
                </Grid>
                <Grid item xs={7}>
                    <Button
                        className="hide-when-printing"
                        variant="contained"
                        onClick={handleFromButtonClick}
                    >
                        From
                    </Button>
                    <Button
                        className="hide-when-printing"
                        variant="contained"
                        onClick={handleToButtonClick}
                    >
                        To
                    </Button>
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        value={moveDetails.partNumber}
                        label="Part Number"
                        onChange={handleFieldChange}
                        maxLength={14}
                        propertyName="partNumber"
                        textFieldProps={onKeyDownProp}
                    />
                </Grid>
                <Grid item xs={1}>
                    <InputField
                        value={moveDetails.quantity}
                        label="Qty"
                        type="number"
                        onChange={handleFieldNumberChange}
                        propertyName="quantity"
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        value={moveDetails.from}
                        label="From"
                        onChange={handleFieldChange}
                        maxLength={16}
                        propertyName="from"
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        value={moveDetails.stockRotationDate}
                        label="St Rot Date"
                        onChange={handleFieldChange}
                        maxLength={16}
                        type="date"
                        propertyName="stockRotationDate"
                    />
                </Grid>
                <Grid item xs={2} />
                <Grid item xs={4} />
                <Grid item xs={3}>
                    <InputField
                        value={moveDetails.to}
                        label="To"
                        onChange={handleFieldChange}
                        maxLength={16}
                        propertyName="to"
                        textFieldProps={{ inputRef: toInput }}
                    />
                </Grid>
                <Grid item xs={5}>
                    <Button
                        style={{ marginTop: '22px' }}
                        className="hide-when-printing"
                        variant="contained"
                        onClick={handleMoveClick}
                    >
                        Move
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    <div style={{ height: 300, width: '100%' }}>
                        <DataGrid
                            rows={displayAvailableStock(availableStock)}
                            columns={columns}
                            density="compact"
                            rowHeight={34}
                            loading={availableStockLoading}
                            hideFooter
                            onSelectionChange={handleSelectRow}
                        />
                    </div>
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
