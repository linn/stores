import React, { useState, useRef, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Typeahead,
    InputField,
    SnackbarMessage,
    ErrorCard
} from '@linn-it/linn-form-components-library';
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
    fetchAvailableStock,
    moveError,
    moveResult,
    doMove,
    clearMoveError,
    requestErrors,
    userNumber
}) {
    const [moveDetails, setMoveDetails] = useState({ userNumber });
    const [selectedRow, setSelectedRow] = useState(null);
    const [alert, setAlert] = useState({ message: ' ', visible: false });

    const toInput = useRef(null);

    useEffect(() => {
        clearMoveError();
    }, [moveDetails, clearMoveError]);

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
            fromLocationCode: row.locationCode,
            fromState: row.state,
            fromStockPoolCode: row.stockPoolCode,
            fromPalletNumber: row.palletNumber,
            fromLocationId: row.locationId,
            fromStockRotationDate: moment(row.stockRotationDate).format('DD MMM YYYY')
        });

        toInput.current.focus();
    };

    const showMessage = text => {
        setAlert({ message: text, visible: true });
    };

    const closeMessage = () => {
        setAlert({ message: ' ', visible: false });
    };

    const isKardex = loc =>
        loc.startsWith('E-K1') ||
        loc.startsWith('K1') ||
        loc.startsWith('E-K2') ||
        loc.startsWith('K2') ||
        loc.startsWith('E-K4') ||
        loc.startsWith('K4');

    const saveEnabled = () =>
        moveDetails.from && moveDetails.to && moveDetails.quantity && moveDetails.partNumber;

    const handleMoveClick = () => {
        clearMoveError();
        if (!saveEnabled) {
            showMessage('Please fill out all move fields');
            return;
        }
        if (isKardex(moveDetails.from) && isKardex(moveDetails.to)) {
            showMessage("You can't move from Kardex to Kardex");
            return;
        }

        doMove(moveDetails);
    };

    const setToDetailsFromAvailableStock = row => {
        setMoveDetails({
            ...moveDetails,
            to: row.displayLocation,
            toLocationCode: row.locationCode,
            toState: row.state,
            toStockPoolCode: row.stockPoolCode,
            toPalletNumber: row.palletNumber,
            toLocationId: row.locationId
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
        <Page requestErrors={requestErrors} showRequestErrors>
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
                        value={moveDetails.fromStockRotationDate}
                        label="St Rot Date"
                        onChange={handleFieldChange}
                        maxLength={16}
                        type="date"
                        propertyName="fromStockRotationDate"
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
                        disabled={!saveEnabled()}
                    >
                        Move
                    </Button>
                </Grid>
                {moveError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={moveError} />
                    </Grid>
                )}
                <Grid item xs={12}>
                    <SnackbarMessage
                        visible={alert.visible}
                        onClose={closeMessage}
                        message={alert.message}
                    />
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
    fetchAvailableStock: PropTypes.func.isRequired,
    moveError: PropTypes.string,
    moveResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    doMove: PropTypes.func.isRequired,
    clearMoveError: PropTypes.func.isRequired,
    requestErrors: PropTypes.arrayOf(
        PropTypes.shape({ message: PropTypes.string, name: PropTypes.string })
    ),
    userNumber: PropTypes.number.isRequired
};

StockMove.defaultProps = {
    partsLoading: false,
    availableStock: [],
    availableStockLoading: false,
    moveError: null,
    moveResult: {
        message: null,
        success: true
    },
    requestErrors: null
};

export default StockMove;
