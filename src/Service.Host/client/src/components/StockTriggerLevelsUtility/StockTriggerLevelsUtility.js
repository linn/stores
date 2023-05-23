import React, { useEffect, useState, useCallback } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Loading,
    SnackbarMessage,
    ErrorCard,
    Typeahead,
    SaveBackCancelButtons
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { DataGrid } from '@mui/x-data-grid';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Page from '../../containers/Page';

function StockTriggerLevelsUtility({
    items,
    searchParts,
    partsSearchResults,
    partsLoading,
    clearPartsSearch,
    updateStockTriggerLevel,
    createStockTriggerLevel,
    deleteStockTriggerLevel,
    stockTriggerLevelsLoading,
    storagePlaces,
    clearStoragePlacesSearch,
    searchStoragePlaces,
    storagePlacesLoading,
    options,
    snackbarVisible,
    setSnackbarVisible,
    itemError,
    history
}) {
    const [prevStockTriggerLevel, setPrevStockTriggerLevel] = useState(null);
    const [stockTriggerLevel, setStockTriggerLevel] = useState([]);

    const [searchResults, setSearchResults] = useState([]);
    const [hasSearched, setHasSearched] = useState(false);

    useEffect(() => {
        if (items !== prevStockTriggerLevel) {
            if (items) {
                setPrevStockTriggerLevel(items);
                setStockTriggerLevel(items);
            }
        }
    }, [items, stockTriggerLevel, prevStockTriggerLevel, options]);

    useEffect(() => {
        if (hasSearched) {
            setSearchResults(items);
        }
    }, [items, hasSearched]);

    const handleSelectRows = selected => {
        setStockTriggerLevel(
            stockTriggerLevel.map(r =>
                selected.includes(r.id) ? { ...r, selected: true } : { ...r, selected: false }
            )
        );
    };

    const handleFieldChange = useCallback((field, rowId, newValue) => {
        setStockTriggerLevel(c =>
            c.map(s =>
                Number(s.id) === Number(rowId) ? { ...s, [field]: newValue, edited: true } : s
            )
        );
    }, []);

    const handleEditRowsModelChange = useCallback(
        model => {
            if (Object.keys(model).length > 0) {
                const key = Object.keys(model)[0];

                const field = Object.keys(model[key])?.[0];
                const { value } = model[key][field];
                handleFieldChange(field, key, value);
            }
        },
        [handleFieldChange]
    );

    const valueGetter = (params, fieldName) => {
        const value = stockTriggerLevel.find(x => Number(x.id) === Number(params.row.id))?.[
            fieldName
        ];
        return value;
    };

    const addNewRow = () =>
        setStockTriggerLevel([
            ...stockTriggerLevel,
            {
                isNewRow: true,
                edited: true,
                id: -stockTriggerLevel.length
            }
        ]);

    const deleteSelectedRows = () => {
        stockTriggerLevel.filter(r => r.selected).forEach(s => deleteStockTriggerLevel(s.id, s));
    };

    const columns = [
        {
            headerName: 'Storage Place',
            field: 'storagePlaceName',
            editable: true,
            width: 150,
            valueGetter: params => valueGetter(params, 'storagePlaceName'),
            renderEditCell: params => (
                <Typeahead
                    onSelect={newValue => {
                        handleFieldChange('storagePlaceName', params.row.id, newValue.name);
                        handleFieldChange(
                            'storagePlaceDescription',
                            params.row.id,
                            newValue.description
                        );
                        handleFieldChange('palletNumber', params.row.id, newValue.palletNumber);
                        handleFieldChange('locationId', params.row.id, newValue.locationId);
                        handleFieldChange('triggerLevel', params.row.id, newValue.triggerLevel);
                        handleFieldChange('maxCapacity', params.row.id, newValue.kanbanSize);
                    }}
                    label=""
                    modal
                    items={storagePlaces}
                    value={valueGetter(params, 'storagePlaceName')}
                    loading={storagePlacesLoading}
                    fetchItems={searchStoragePlaces}
                    links={false}
                    clearSearch={() => clearStoragePlacesSearch}
                    placeholder=""
                />
            )
        },
        {
            headerName: 'Part',
            field: 'partNumber',
            editable: true,
            width: 150,
            valueGetter: params => valueGetter(params, 'partNumber'),
            renderEditCell: params => (
                <Typeahead
                    onSelect={newValue => {
                        handleFieldChange('partNumber', params.row.id, newValue.name);
                    }}
                    modal
                    items={partsSearchResults}
                    value={valueGetter(params, 'partNumber')}
                    fetchItems={searchParts}
                    links={false}
                    clearSearch={() => clearPartsSearch}
                    placeholder=""
                    loading={partsLoading}
                    resultLimit={500}
                />
            )
        },
        {
            headerName: 'Trigger Level',
            field: 'triggerLevel',
            width: 200,
            type: 'number',
            valueGetter: params => valueGetter(params, 'triggerLevel'),
            editable: true
        },
        {
            headerName: 'Maximum Capacity',
            field: 'maxCapacity',
            width: 200,
            type: 'number',
            valueGetter: params => valueGetter(params, 'maxCapacity'),
            editable: true
        },
        {
            headerName: 'Kanban Size',
            field: 'kanbanSize',
            width: 200,
            type: 'number',
            valueGetter: params => valueGetter(params, 'kanbanSize'),
            editable: true
        },
        {
            headerName: 'Description',
            field: 'storagePlaceDescription',
            width: 400
        }
    ];

    const saveBackCancelButtons = () => (
        <Grid item xs={12}>
            <SaveBackCancelButtons
                backClick={() => history.push('/inventory/stock-trigger-levels/')}
                saveDisabled={!stockTriggerLevel.some(x => x.edited)}
                cancelClick={() => setStockTriggerLevel(items)}
                saveClick={() => {
                    stockTriggerLevel
                        .filter(x => x.edited && !x.isNewRow)
                        .forEach(s => {
                            updateStockTriggerLevel(s.id, s);
                        });
                    stockTriggerLevel
                        .filter(x => x.edited && x.isNewRow)
                        .forEach(s => {
                            const body = s;
                            if (!body.partNumber) {
                                body.partNumber = stockTriggerLevel.find(
                                    l => l.partNumber
                                ).partNumber;
                            }
                            createStockTriggerLevel(body);
                        });
                }}
            />
        </Grid>
    );
    return (
        <Page>
            <SnackbarMessage
                visible={snackbarVisible}
                onClose={() => setSnackbarVisible(false)}
                message="Save Successful"
            />
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {stockTriggerLevelsLoading ? (
                        <Loading />
                    ) : (
                        <Typeahead
                            items={searchResults.slice(0, 10)}
                            fetchItems={searchTerm => {
                                setHasSearched(true);
                                setSearchResults(
                                    items?.filter(i =>
                                        i.locationId.includes(searchTerm?.toUpperCase())
                                    )
                                );
                            }}
                            clearSearch={() => {}}
                            loading={stockTriggerLevelsLoading}
                            title="Stock Trigger Levels"
                            history={history}
                            minimumSearchTermLength={2}
                            debounce={1}
                        />
                    )}
                </Grid>
                <Grid item xs={12}>
                    <Typography variant="subtitle1">
                        Double click a cell to start editing or use the buttons at the bottom of the
                        page to add or delete a row.
                    </Typography>
                    <Typography variant="subtitle2">
                        The table currently only supports adding/updating/deleting one row at a
                        time.
                    </Typography>
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard
                            errorMessage={itemError?.details?.errors?.[0] || itemError.statusText}
                        />
                    </Grid>
                )}
                {saveBackCancelButtons()}
                {stockTriggerLevelsLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        <Grid item xs={12}>
                            {stockTriggerLevel && (
                                <Grid item xs={12}>
                                    <div style={{ width: '100%' }}>
                                        <DataGrid
                                            rows={stockTriggerLevel}
                                            columns={columns}
                                            editMode="cell"
                                            onEditRowsModelChange={handleEditRowsModelChange}
                                            autoHeight
                                            columnBuffer={7}
                                            density="comfortable"
                                            rowHeight={34}
                                            loading={false}
                                            hideFooter
                                            disableSelectionOnClick
                                            onSelectionModelChange={handleSelectRows}
                                            checkboxSelection
                                            isRowSelectable={params =>
                                                !stockTriggerLevel
                                                    .filter(s => s.id !== params.row.id)
                                                    .some(x => x.selected)
                                            }
                                            isCellEditable={params =>
                                                (!stockTriggerLevel.some(x => x.edited) &&
                                                    !stockTriggerLevel.some(x => x.selected)) ||
                                                params.row.edited
                                            }
                                        />
                                    </div>
                                </Grid>
                            )}
                        </Grid>
                        <Grid item xs={2}>
                            <Button
                                variant="outlined"
                                color="secondary"
                                disabled={
                                    !stockTriggerLevel.some(x => x.selected) ||
                                    stockTriggerLevel.some(x => x.edited)
                                }
                                onClick={deleteSelectedRows}
                            >
                                Delete Selected
                            </Button>
                        </Grid>
                        <Grid item xs={2}>
                            <Button
                                onClick={addNewRow}
                                variant="outlined"
                                disabled={stockTriggerLevel.some(x => x.edited)}
                            >
                                Add Row
                            </Button>
                        </Grid>
                        <Grid item xs={8} />
                        {saveBackCancelButtons()}
                    </>
                )}
            </Grid>
        </Page>
    );
}

StockTriggerLevelsUtility.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            partNumber: PropTypes.string,
            locationId: PropTypes.number,
            triggerLevel: PropTypes.number,
            maxCapacity: PropTypes.number,
            palletNumber: PropTypes.number,
            kanbanSize: PropTypes.number
        })
    ),
    searchParts: PropTypes.func.isRequired,
    partsLoading: PropTypes.bool,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    clearPartsSearch: PropTypes.func.isRequired,
    storagePlaces: PropTypes.arrayOf(PropTypes.shape({})),
    clearStoragePlacesSearch: PropTypes.func.isRequired,
    searchStoragePlaces: PropTypes.func.isRequired,
    storagePlacesLoading: PropTypes.bool,
    updateStockTriggerLevel: PropTypes.func.isRequired,
    createStockTriggerLevel: PropTypes.func.isRequired,
    deleteStockTriggerLevel: PropTypes.func.isRequired,
    stockTriggerLevelsLoading: PropTypes.bool,
    options: PropTypes.shape({ partNumber: PropTypes.string }).isRequired,
    snackbarVisible: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        item: PropTypes.string,
        details: PropTypes.shape({
            errors: PropTypes.arrayOf(PropTypes.shape({}))
        })
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired
};

StockTriggerLevelsUtility.defaultProps = {
    items: [],
    partsLoading: false,
    partsSearchResults: [],
    storagePlacesLoading: false,
    storagePlaces: [],
    stockTriggerLevelsLoading: false,
    snackbarVisible: false,
    itemError: null
};

export default StockTriggerLevelsUtility;
