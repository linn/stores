import React, { useEffect, useState, useCallback } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    ErrorCard,
    InputField,
    Loading,
    SaveBackCancelButtons,
    SnackbarMessage,
    Title,
    Typeahead
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { DataGrid } from '@mui/x-data-grid';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import makeStyles from '@material-ui/styles/makeStyles';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    button: {
        marginLeft: theme.spacing(1),
        marginTop: theme.spacing(3)
    },
    a: {
        textDecoration: 'none'
    }
}));

function StockTriggerLevelsUtility({
    items,
    searchParts,
    partsSearchResults,
    partsLoading,
    clearPartsSearch,
    stockTriggerLevels,
    searchStockTriggerLevels,
    updateStockTriggerLevel,
    createStockTriggerLevel,
    deleteStockTriggerLevel,
    stockTriggerLevelsLoading,
    stockTriggerLevelsSearchLoading,
    storagePlaces,
    clearStoragePlacesSearch,
    searchStoragePlaces,
    storagePlacesLoading,
    snackbarVisible,
    setSnackbarVisible,
    itemError,
    history
}) {
    const classes = useStyles();
    const [triggerLevelRows, setTriggerLevelRows] = useState([]);

    const [options, setOptions] = useState({
        partNumber: '',
        storagePlace: ''
    });

    const handleOptionsChange = (propertyName, newValue) =>
        setOptions({ ...options, [propertyName]: newValue });

    useEffect(() => {
        if (stockTriggerLevels?.length) {
            setTriggerLevelRows(
                stockTriggerLevels.map(i => ({
                    ...i,
                    id: i.id,
                    description: i.storageLocation?.description,
                    storagePlaceDescription: i.storageLocation?.description,
                    locationName: i.palletNumber || i.storageLocation?.locationCode
                }))
            );
        }
    }, [stockTriggerLevels]);

    useEffect(() => {
        if (snackbarVisible)
            searchStockTriggerLevels(
                '',
                `&partNumberSearchTerm=${options.partNumber}&storagePlaceSearchTerm=${options.storagePlace}`
            );
    }, [snackbarVisible, searchStockTriggerLevels, options]);

    const valueGetter = (params, fieldName) => {
        const value = triggerLevelRows.find(x => Number(x.id) === Number(params.row.id))?.[
            fieldName
        ];
        return value;
    };

    const handleSelectRows = selected => {
        setTriggerLevelRows(
            triggerLevelRows.map(r =>
                selected.includes(r.id) ? { ...r, selected: true } : { ...r, selected: false }
            )
        );
    };

    const handleFieldChange = useCallback((field, rowId, newValue) => {
        setTriggerLevelRows(c =>
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

    const addNewRow = () =>
        setTriggerLevelRows([
            ...triggerLevelRows,
            {
                isNewRow: true,
                edited: true,
                id: -triggerLevelRows.length
            }
        ]);

    const deleteSelectedRows = () => {
        triggerLevelRows.filter(r => r.selected).forEach(s => deleteStockTriggerLevel(s.id, null));
    };

    const columns = [
        {
            headerName: 'Storage Location',
            field: 'locationName',
            editable: true,
            width: 300,
            valueGetter: params => valueGetter(params, 'locationName'),
            renderEditCell: params => (
                <Typeahead
                    onSelect={newValue => {
                        handleFieldChange('locationName', params.row.id, newValue.name);
                        handleFieldChange(
                            'storagePlaceDescription',
                            params.row.id,
                            newValue.description
                        );
                        handleFieldChange('locationId', params.row.id, newValue.locationId);
                        handleFieldChange('palletNumber', params.row.id, newValue.palletNumber);
                    }}
                    label=""
                    modal
                    items={storagePlaces}
                    value={params.row.palletNumber || params.row.name}
                    loading={storagePlacesLoading}
                    fetchItems={searchStoragePlaces}
                    links={false}
                    clearSearch={() => clearStoragePlacesSearch}
                    placeholder=""
                    debounce={1000}
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
                        handleFieldChange('partNumber', params.row.id, newValue.partNumber);
                    }}
                    modal
                    items={partsSearchResults}
                    value={params.row.partNumber}
                    fetchItems={searchParts}
                    links={false}
                    clearSearch={() => clearPartsSearch}
                    placeholder=""
                    loading={partsLoading}
                    resultLimit={500}
                    debounce={1000}
                    minimumSearchTermLength={2}
                />
            )
        },
        {
            headerName: 'Trigger Level',
            field: 'triggerLevel',
            width: 200,
            type: 'number',
            editable: true
        },
        {
            headerName: 'Maximum Capacity',
            field: 'maxCapacity',
            width: 200,
            type: 'number',
            editable: true
        },
        {
            headerName: 'Kanban Size',
            field: 'kanbanSize',
            width: 200,
            type: 'number',
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
                saveDisabled={!triggerLevelRows.some(x => x.edited)}
                cancelClick={() => setTriggerLevelRows(items)}
                saveClick={() => {
                    triggerLevelRows
                        .filter(x => x.edited && !x.isNewRow)
                        .forEach(s => {
                            updateStockTriggerLevel(s.id, s);
                        });
                    triggerLevelRows
                        .filter(x => x.edited && x.isNewRow)
                        .forEach(s => {
                            const body = s;
                            if (!body.partNumber) {
                                body.id = triggerLevelRows.find(l => l.partNumber).id;
                            }
                            createStockTriggerLevel(body);
                        });
                }}
            />
        </Grid>
    );
    return (
        <Page>
            <Grid item xs={12}>
                <SnackbarMessage
                    visible={snackbarVisible}
                    onClose={() => setSnackbarVisible(false)}
                    message="Save Successful"
                />
            </Grid>
            <Grid container spacing={3}>
                <Title text="Stock Trigger Levels" />
                <Grid container spacing={3}>
                    <Grid item xs={3}>
                        <InputField
                            fullWidth
                            value={options.partNumber}
                            label="Part Number"
                            propertyName="partNumber"
                            onChange={handleOptionsChange}
                            helperText="* can be used as a wildcard on all fields. Leave blank for all."
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <InputField
                            fullWidth
                            value={options.storagePlace}
                            label="Storage Place"
                            propertyName="storagePlace"
                            onChange={handleOptionsChange}
                        />
                    </Grid>
                    <Grid item xs={2}>
                        <Button
                            variant="outlined"
                            color="primary"
                            className={classes.button}
                            onClick={() => {
                                searchStockTriggerLevels(
                                    '',
                                    `&partNumberSearchTerm=${options.partNumber}&storagePlaceSearchTerm=${options.storagePlace}`
                                );
                            }}
                        >
                            Go
                        </Button>
                    </Grid>
                </Grid>
                <Grid item xs={12}>
                    <Typography variant="subtitle1">
                        Double click a cell to start editing or use the buttons at the bottom of the
                        page to add or delete a row.
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
                {stockTriggerLevelsLoading || stockTriggerLevelsSearchLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        {triggerLevelRows && (
                            <Grid item xs={12}>
                                <div style={{ width: '100%' }}>
                                    <DataGrid
                                        rows={triggerLevelRows}
                                        columns={columns}
                                        editMode="cell"
                                        onEditRowsModelChange={handleEditRowsModelChange}
                                        autoHeight
                                        columnBuffer={8}
                                        density="comfortable"
                                        rowHeight={34}
                                        loading={false}
                                        disableSelectionOnClick
                                        onSelectionModelChange={handleSelectRows}
                                        checkboxSelection
                                        isRowSelectable={params =>
                                            !triggerLevelRows
                                                .filter(s => s.id !== params.row.id)
                                                .some(x => x.selected)
                                        }
                                        isCellEditable={params =>
                                            (!triggerLevelRows.some(x => x.edited) &&
                                                !triggerLevelRows.some(x => x.selected)) ||
                                            params.row.edited
                                        }
                                    />
                                </div>
                            </Grid>
                        )}
                        <Grid item xs={2}>
                            <Button
                                variant="outlined"
                                color="secondary"
                                disabled={
                                    !triggerLevelRows.some(x => x.selected) ||
                                    triggerLevelRows.some(x => x.edited)
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
                                disabled={triggerLevelRows.some(x => x.edited)}
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
            kanbanSize: PropTypes.number,
            storageLocation: PropTypes.shape({})
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
    stockTriggerLevels: PropTypes.arrayOf(PropTypes.shape({})),
    updateStockTriggerLevel: PropTypes.func.isRequired,
    createStockTriggerLevel: PropTypes.func.isRequired,
    deleteStockTriggerLevel: PropTypes.func.isRequired,
    searchStockTriggerLevels: PropTypes.func.isRequired,
    stockTriggerLevelsLoading: PropTypes.bool,
    stockTriggerLevelsSearchLoading: PropTypes.bool,
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
    stockTriggerLevels: [],
    stockTriggerLevelsLoading: false,
    stockTriggerLevelsSearchLoading: false,
    snackbarVisible: false,
    itemError: null
};

export default StockTriggerLevelsUtility;
