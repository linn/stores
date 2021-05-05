import React, { useState, useRef, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Typeahead,
    InputField,
    SnackbarMessage,
    ErrorCard,
    utilities,
    Loading
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { DataGrid } from '@material-ui/data-grid';
import Button from '@material-ui/core/Button';
import Tooltip from '@material-ui/core/Tooltip';
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
    userNumber,
    reqMoves,
    fetchReqMoves,
    reqMovesLoading,
    clearAvailableStock,
    moveWorking,
    clearMoveResult,
    partStorageTypes,
    partStorageTypesLoading,
    fetchPartStorageTypes,
    clearPartStorageTypes,
    storageLocations,
    storageLocationsLoading,
    fetchStorageLocations,
    clearStorageLocationsSearch,
    fetchPartsLookup
}) {
    const [moveDetails, setMoveDetails] = useState({ userNumber });
    const [selectedRow, setSelectedRow] = useState(null);
    const [selectedPartStorageRow, setSelectedPartStorageRow] = useState(null);
    const [alert, setAlert] = useState({ message: ' ', visible: false });
    const [partDescription, setPartDescription] = useState(null);

    const toInput = useRef(null);
    const partNumberInput = useRef(null);

    useEffect(() => {
        clearMoveError();
    }, [moveDetails, clearMoveError]);

    useEffect(() => {
        if (moveResult && moveResult.success && moveResult.links) {
            const reqHref = utilities.getHref(moveResult, 'requisition');
            const reqNumber = reqHref.split('/').pop();
            fetchReqMoves(reqNumber);
            setMoveDetails({ reqNumber });
            setPartDescription(null);
            setSelectedRow(null);
            setSelectedPartStorageRow(null);
            partNumberInput.current.focus();
        }
    }, [moveResult, fetchReqMoves]);

    useEffect(() => {
        if (parts && parts.length === 1) {
            setPartDescription(parts[0].description);
        }
    }, [parts, setPartDescription]);

    const partResults = () => {
        return parts?.map(item => ({
            ...item,
            name: item.partNumber.toString(),
            description: item.description,
            href: item.href
        }));
    };

    const locationResults = () => {
        return storageLocations?.map(item => ({
            ...item,
            name: item.locationCode,
            href: null
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
            fromStockRotationDate: row.stockRotationDate
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
        clearAvailableStock();
        clearPartStorageTypes();
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

    const handlePartStorageButtonClick = () => {
        if (selectedPartStorageRow) {
            setMoveDetails({ ...moveDetails, storageType: selectedPartStorageRow.storageType });
        }
    };

    const handleSelectRow = row => {
        setSelectedRow(availableStock[row.rowIds[0]]);

        if (!moveDetails.from) {
            setFromDetailsFromAvailableStock(availableStock[row.rowIds[0]]);
        }
    };

    const handleSelectPartStorageRow = selected => {
        const row = partStorageTypes.find(p => p.id.toString() === selected.rowIds[0]);
        setSelectedPartStorageRow(row);

        if (!moveDetails.storageType) {
            setMoveDetails({
                ...moveDetails,
                storageType: row.storageType
            });
        }
    };

    const handleOnSelect = selectedPart => {
        setMoveDetails({ ...moveDetails, partNumber: selectedPart.partNumber });

        setPartDescription(selectedPart.description);
        fetchAvailableStock(selectedPart.partNumber);
        fetchPartStorageTypes(selectedPart.partNumber);
        clearMoveResult();
        clearMoveError();
    };

    const handleOnSelectLocation = loc => {
        setMoveDetails({ ...moveDetails, toLocationId: loc.id, to: loc.locationCode });
    };

    const handleFieldChange = (property, value) => {
        if (property === 'from') {
            setMoveDetails({
                ...moveDetails,
                from: value.toUpperCase(),
                fromPalletNumber: null,
                fromLocationId: null,
                fromLocationCode: null
            });
        } else if (property === 'to') {
            setMoveDetails({
                ...moveDetails,
                to: value.toUpperCase(),
                toPalletNumber: null,
                toLocationId: null,
                toLocationCode: null
            });
        } else {
            setMoveDetails({ ...moveDetails, [property]: value.toUpperCase() });
        }
    };

    const handleFieldNumberChange = (property, value) => {
        setMoveDetails({ ...moveDetails, [property]: value });
    };

    const handleOnKeyPress = data => {
        if (data.keyCode === 13 || data.keyCode === 9) {
            fetchAvailableStock(moveDetails.partNumber);
            fetchPartStorageTypes(moveDetails.partNumber);
            clearMoveResult();
            clearMoveError();
            fetchPartsLookup(moveDetails.partNumber, '&exactOnly=true');
        }
    };

    const displayAvailableStock = stock => {
        if (!stock) {
            return [];
        }

        return stock.map((s, i) => ({ ...s, id: i }));
    };

    const displayMoves = moves => {
        if (!moves) {
            return [];
        }

        return moves.map((m, i) => ({ ...m, id: i }));
    };

    const columns = [
        { field: 'quantityAvailable', headerName: 'Qty', width: 100 },
        { field: 'displayLocation', headerName: 'Location', width: 140 },
        {
            field: 'stockRotationDate',
            type: 'date',
            headerName: 'Stock Rot Date',
            width: 140,
            valueGetter: params =>
                params.row.stockRotationDate
                    ? moment(params.row.stockRotationDate).format('DD MMM YYYY')
                    : null
        },
        { field: 'stockPoolCode', headerName: 'Stock Pool', width: 140 },
        { field: 'state', headerName: 'State', width: 140 }
    ];

    const partStorageColumns = [
        { field: 'storageType', headerName: 'Type', width: 120 },
        { field: 'maximum', headerName: 'Max', width: 100 },
        { field: 'increment', headerName: 'Incr', width: 100 },
        { field: 'preference', headerName: 'Pref', width: 80 }
    ];

    const moveColumns = [
        { field: 'reqNumber', headerName: 'Req', width: 100 },
        { field: 'lineNumber', headerName: 'Line', width: 100, hide: true },
        { field: 'moveSeq', headerName: 'Seq', width: 100, hide: true },
        { field: 'partNumber', headerName: 'Part', width: 140 },
        { field: 'moveQuantity', headerName: 'Qty', width: 100 },
        { field: 'fromPalletNumber', headerName: 'From Pallet', width: 140 },
        { field: 'fromLocationCode', headerName: 'From Loc', width: 140 },
        { field: 'toPalletNumber', headerName: 'To Pallet', width: 140 },
        { field: 'toLocationCode', headerName: 'To Loc', width: 140 },
        { field: 'remarks', headerName: 'Remarks', width: 150 }
    ];

    const partProp = { inputRef: partNumberInput, onKeyDown: handleOnKeyPress };

    return (
        <Page requestErrors={requestErrors} showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={2} />
                <Grid item xs={6}>
                    <span>Stock</span>
                    <div style={{ height: 190, width: '100%' }}>
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
                <Grid item xs={4}>
                    <span>Storage Types</span>
                    <div style={{ height: 190, width: '100%' }}>
                        <DataGrid
                            rows={partStorageTypes}
                            columns={partStorageColumns}
                            density="compact"
                            rowHeight={34}
                            loading={partStorageTypesLoading}
                            hideFooter
                            onSelectionChange={handleSelectPartStorageRow}
                        />
                    </div>
                </Grid>
                <Grid item xs={2} />
                <Grid item xs={6}>
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
                <Grid item xs={4}>
                    <Button
                        className="hide-when-printing"
                        variant="contained"
                        onClick={handlePartStorageButtonClick}
                    >
                        Storage Type
                    </Button>
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        value={moveDetails.partNumber}
                        label="Part Number"
                        onChange={handleFieldChange}
                        maxLength={14}
                        autoFocus
                        propertyName="partNumber"
                        textFieldProps={partProp}
                    />
                    <Typeahead
                        items={partResults()}
                        fetchItems={fetchParts}
                        clearSearch={clearPartsSearch}
                        loading={partsLoading}
                        debounce={1000}
                        links={false}
                        modal
                        searchButtonOnly
                        onSelect={p => handleOnSelect(p)}
                        label="Search For Part Number"
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
                <Grid item xs={1}>
                    <InputField
                        value={moveDetails.quantity}
                        label="Qty"
                        type="number"
                        onChange={handleFieldNumberChange}
                        propertyName="quantity"
                    />
                </Grid>
                <Grid item xs={1} />
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
                <Grid item xs={1} />
                <Grid item xs={3}>
                    <Tooltip title={partDescription}>
                        <span>
                            <InputField
                                value={partDescription}
                                label="Part Description"
                                disabled
                                fullWidth
                                propertyName="partDescription"
                            />
                        </span>
                    </Tooltip>
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        value={moveDetails.to}
                        label="To"
                        onChange={handleFieldChange}
                        maxLength={16}
                        propertyName="to"
                        textFieldProps={{ inputRef: toInput }}
                    />
                    <Typeahead
                        items={locationResults()}
                        fetchItems={fetchStorageLocations}
                        clearSearch={clearStorageLocationsSearch}
                        loading={storageLocationsLoading}
                        debounce={1000}
                        links={false}
                        modal
                        searchButtonOnly
                        onSelect={p => handleOnSelectLocation(p)}
                        label="Search For Stock Location"
                    />
                </Grid>
                <Grid item xs={2}>
                    <InputField
                        value={moveDetails.storageType}
                        label="Storage Type"
                        onChange={handleFieldChange}
                        maxLength={4}
                        propertyName="storageType"
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        value={moveDetails.toStockRotationDate}
                        label="To Rot Date"
                        onChange={handleFieldChange}
                        maxLength={16}
                        type="date"
                        propertyName="toStockRotationDate"
                    />
                </Grid>
                <Grid item xs={1}>
                    <Button
                        style={{ marginTop: '22px' }}
                        className="hide-when-printing"
                        variant="contained"
                        onClick={handleMoveClick}
                        disabled={!saveEnabled() || moveWorking}
                    >
                        Move
                    </Button>
                    {moveWorking && <Loading />}
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
                            rows={displayMoves(reqMoves)}
                            columns={moveColumns}
                            density="compact"
                            rowHeight={34}
                            loading={reqMovesLoading}
                            hideFooter
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
    fetchPartsLookup: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired,
    availableStock: PropTypes.arrayOf(PropTypes.shape({})),
    availableStockLoading: PropTypes.bool,
    fetchAvailableStock: PropTypes.func.isRequired,
    moveError: PropTypes.string,
    moveResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string,
        links: PropTypes.arrayOf(PropTypes.shape({}))
    }),
    doMove: PropTypes.func.isRequired,
    clearMoveError: PropTypes.func.isRequired,
    requestErrors: PropTypes.arrayOf(
        PropTypes.shape({ message: PropTypes.string, name: PropTypes.string })
    ),
    userNumber: PropTypes.number.isRequired,
    reqMoves: PropTypes.arrayOf(PropTypes.shape({})),
    reqMovesLoading: PropTypes.bool,
    fetchReqMoves: PropTypes.func.isRequired,
    clearAvailableStock: PropTypes.func.isRequired,
    moveWorking: PropTypes.bool,
    clearMoveResult: PropTypes.func.isRequired,
    partStorageTypes: PropTypes.arrayOf(PropTypes.shape({})),
    partStorageTypesLoading: PropTypes.bool,
    fetchPartStorageTypes: PropTypes.func.isRequired,
    clearPartStorageTypes: PropTypes.func.isRequired,
    storageLocations: PropTypes.arrayOf(PropTypes.shape({})),
    storageLocationsLoading: PropTypes.bool,
    fetchStorageLocations: PropTypes.func.isRequired,
    clearStorageLocationsSearch: PropTypes.func.isRequired
};

StockMove.defaultProps = {
    partsLoading: false,
    availableStock: [],
    availableStockLoading: false,
    moveError: null,
    moveResult: {
        message: null,
        success: true,
        links: null
    },
    requestErrors: null,
    reqMovesLoading: false,
    reqMoves: null,
    moveWorking: false,
    partStorageTypes: [],
    partStorageTypesLoading: false,
    storageLocations: [],
    storageLocationsLoading: false
};

export default StockMove;
