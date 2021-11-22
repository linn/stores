import React, { useEffect, useState, useCallback } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Title,
    DatePicker,
    Loading,
    SnackbarMessage,
    ErrorCard,
    Typeahead,
    SaveBackCancelButtons
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { DataGrid } from '@mui/x-data-grid';
import Button from '@material-ui/core/Button';
import moment from 'moment';
import Page from '../../containers/Page';

function DeptStockUtility({
    items,
    itemsLoading,
    departments,
    clearDepartmentsSearch,
    searchDepartments,
    departmentsLoading,
    updateStockLocator,
    createStockLocator,
    deleteStockLocator,
    stockLocatorLoading,
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
    const [prevStockLocators, setPrevStockLocators] = useState(null);
    const [stockLocators, setStockLocators] = useState([]);
    const [newRows, setNewRows] = useState([]);
    const [rows, setRows] = useState([]);
    useEffect(() => {
        if (items !== prevStockLocators) {
            if (items) {
                setPrevStockLocators(items);
                setStockLocators(items);
                setNewRows([]);
            }
        }
    }, [items, stockLocators, prevStockLocators, options]);

    useEffect(() => {
        setRows([...newRows, ...stockLocators]);
    }, [stockLocators, newRows]);

    const handleSelectRows = selected => {
        setRows(
            rows.map(r =>
                selected.includes(r.id) ? { ...r, selected: true } : { ...r, selected: false }
            )
        );
    };

    const handleFieldChange = useCallback((field, rowId, newValue) => {
        setRows(c =>
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

    const valueGetter = (params, fieldName, formatDate) => {
        let value = rows.find(x => Number(x.id) === Number(params.row.id))?.[fieldName];
        if (formatDate) {
            value = moment(value).format('DD/MM/YY');
        }
        return value;
    };

    const addNewRow = () =>
        setNewRows([
            ...newRows,
            { isNewRow: true, id: -1 - newRows.length, stockRotationDate: new Date() }
        ]);

    const deleteSelectedRows = () => {
        rows.filter(r => r.selected).forEach(s => deleteStockLocator(s.id, s));
    };

    const columns = [
        {
            headerName: 'Storage Place',
            field: 'storagePlaceName',
            editable: true,
            width: 200,
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
            headerName: 'Description',
            field: 'storagePlaceDescription',
            width: 200
        },
        {
            headerName: 'Batch Ref',
            field: 'batchRef',
            width: 200,
            editable: true
        },
        {
            headerName: 'Batch Date',
            field: 'stockRotationDate',
            width: 200,
            editable: true,
            valueGetter: params => valueGetter(params, 'stockRotationDate', true),
            renderEditCell: params => (
                <DatePicker
                    label=""
                    value={new Date(valueGetter(params, 'stockRotationDate')) || null}
                    onChange={value => {
                        handleFieldChange('stockRotationDate', params.row.id, value);
                    }}
                />
            )
        },
        {
            headerName: 'Qty',
            field: 'quantity',
            width: 200,
            type: 'number',
            editable: true
        },
        {
            headerName: 'Remarks',
            field: 'remarks',
            width: 200,
            editable: true
        },
        {
            headerName: 'Audit Dept',
            field: 'auditDepartmentCode',
            editable: true,
            width: 200,
            valueGetter: params => valueGetter(params, 'auditDepartmentCode'),
            renderEditCell: params => (
                <Typeahead
                    onSelect={newValue => {
                        handleFieldChange('auditDepartmentCode', params.row.id, newValue.name);
                    }}
                    label=""
                    modal
                    items={departments}
                    value={valueGetter(params, 'auditDepartmentCode')}
                    loading={departmentsLoading}
                    fetchItems={searchDepartments}
                    links={false}
                    clearSearch={clearDepartmentsSearch}
                    placeholder=""
                />
            )
        }
    ];
    return (
        <Page>
            <SnackbarMessage
                visible={snackbarVisible}
                onClose={() => setSnackbarVisible(false)}
                message="Save Successful"
            />
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Departmental Pallets Utility" />
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard
                            errorMessage={itemError?.details?.errors?.[0] || itemError.statusText}
                        />
                    </Grid>
                )}
                <Grid item xs={12}>
                    <SaveBackCancelButtons
                        backClick={() => history.push('/inventory/dept-stock-parts')}
                        saveDisabled={!rows.some(x => x.edited)}
                        cancelClick={() => setRows(items)}
                        saveClick={() => {
                            rows.filter(x => x.edited && !x.isNewRow).forEach(s => {
                                updateStockLocator(s.id, s);
                            });
                            rows.filter(x => x.edited && x.isNewRow).forEach(s => {
                                const body = s;
                                if (!body.partNumber) {
                                    body.partNumber = stockLocators.find(
                                        l => l.partNumber
                                    ).partNumber;
                                }
                                createStockLocator(body);
                            });
                        }}
                    />
                </Grid>
                <Grid item xs={2}>
                    <Button onClick={addNewRow} variant="outlined">
                        Add Row
                    </Button>
                </Grid>
                <Grid item xs={8} />
                <Grid item xs={2}>
                    <Button variant="outlined" color="secondary" onClick={deleteSelectedRows}>
                        Delete Selected
                    </Button>
                </Grid>
                {itemsLoading || stockLocatorLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <Grid item xs={12}>
                        {stockLocators && (
                            <Grid item xs={12}>
                                <div style={{ width: '100%' }}>
                                    <DataGrid
                                        rows={rows}
                                        columns={columns}
                                        editMode="cell"
                                        onEditRowsModelChange={handleEditRowsModelChange}
                                        autoHeight
                                        density="comfortable"
                                        rowHeight={34}
                                        loading={false}
                                        hideFooter
                                        disableSelectionOnClick
                                        onSelectionModelChange={handleSelectRows}
                                        checkboxSelection
                                    />
                                </div>
                            </Grid>
                        )}
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

DeptStockUtility.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string,
            href: PropTypes.string
        })
    ),
    itemsLoading: PropTypes.bool,
    departments: PropTypes.arrayOf(PropTypes.shape({})),
    clearDepartmentsSearch: PropTypes.func.isRequired,
    searchDepartments: PropTypes.func.isRequired,
    departmentsLoading: PropTypes.bool,
    storagePlaces: PropTypes.arrayOf(PropTypes.shape({})),
    clearStoragePlacesSearch: PropTypes.func.isRequired,
    searchStoragePlaces: PropTypes.func.isRequired,
    storagePlacesLoading: PropTypes.bool,
    updateStockLocator: PropTypes.func.isRequired,
    createStockLocator: PropTypes.func.isRequired,
    deleteStockLocator: PropTypes.func.isRequired,
    stockLocatorLoading: PropTypes.bool,
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

DeptStockUtility.defaultProps = {
    itemsLoading: false,
    items: [],
    departments: [],
    departmentsLoading: false,
    storagePlacesLoading: false,
    storagePlaces: [],
    stockLocatorLoading: false,
    snackbarVisible: false,
    itemError: null
};

export default DeptStockUtility;
