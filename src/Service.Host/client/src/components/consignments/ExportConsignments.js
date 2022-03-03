import React, { useState, useEffect, useCallback } from 'react';
import moment from 'moment';
import {
    Loading,
    DatePicker,
    utilities,
    InputField,
    Dropdown,
    SaveBackCancelButtons,
    Title
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import { DataGrid } from '@mui/x-data-grid';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Page from '../../containers/Page';

function ExportConsignments({
    hubs,
    hubsLoading,
    options,
    requestErrors,
    loading,
    consignments,
    searchConsignments,
    updateConsignment
}) {
    const defaultStartDate = new Date();
    defaultStartDate.setDate(defaultStartDate.getDate() - 1);
    const [fromDate, setFromDate] = useState(
        options.fromDate ? new Date(options.fromDate) : defaultStartDate
    );
    const [toDate, setToDate] = useState(options.toDate ? new Date(options.toDate) : new Date());
    const [hubId, setHubId] = useState(null);
    const [masterCarrierRef, setMasterCarrierRef] = useState(null);
    const [rows, setRows] = useState([]);
    const [selectedRows, setSelectedRows] = useState([]);
    const [editing, setEditing] = useState(false);

    const hubOptions = () => {
        return utilities.sortEntityList(hubs, 'hubId')?.map(h => ({
            id: h.hubId,
            displayText: `${h.hubId} - ${h.description}`
        }));
    };

    const updateField = (fieldName, newValue) => {
        if (fieldName === 'hubId') {
            setHubId(newValue);
        }
    };

    const findConsignments = () => {
        const searchTerm = options.consignmentId ? options.consignmentId : null;
        if (hubId) {
            searchConsignments(
                searchTerm,
                `&from=${moment(fromDate).format('DD-MMM-YYYY')}&to=${moment(toDate).format(
                    'DD-MMM-YYYY'
                )}&hubId=${hubId}`
            );
        } else {
            searchConsignments(
                searchTerm,
                `&from=${moment(fromDate).format('DD-MMM-YYYY')}&to=${moment(toDate).format(
                    'DD-MMM-YYYY'
                )}`
            );
        }
    };

    const handleSelectRow = selected => {
        setSelectedRows(rows.filter(r => selected.includes(r.id)));
    };

    useEffect(() => {
        setRows(
            consignments.map(c => ({
                ...c,
                id: c.consignmentId
            }))
        );
    }, [consignments]);

    const handleSave = () => {
        rows.forEach(a => {
            if (a.updating) {
                updateConsignment(a.id, a);
            }
        });

        setEditing(false);
    };

    const handleCancel = () => {
        setRows(
            consignments.map(c => ({
                ...c,
                id: c.consignmentId
            }))
        );
        setEditing(false);
    };

    const columns = [
        { field: 'id', headerName: 'Cons Id', width: 120, disableColumnMenu: true },
        { field: 'customerName', headerName: 'Customer Name', width: 300 },
        { field: 'carrier', headerName: 'Carrier', width: 120 },
        { field: 'masterCarrierRef', headerName: 'Master Carrier Ref', editable: true, width: 250 },
        { field: 'carrierRef', headerName: 'Carrier Ref', editable: true, width: 250 },
        {
            field: 'customsEntryCodePrefix',
            headerName: 'Customs Entry Prefix',
            width: 140,
            editable: true,
            disableColumnMenu: true
        },
        {
            field: 'customsEntryCode',
            headerName: 'Code',
            width: 120,
            editable: true,
            disableColumnMenu: true
        },
        {
            field: 'customsEntryCodeDate',
            headerName: 'Date',
            type: 'date',
            width: 200,
            editable: true,
            disableColumnMenu: true
        }
    ];

    const updateRow = useCallback(
        (rowId, fieldName, newValue) => {
            setRows(rows =>
                rows.map(r =>
                    r.consignmentId === rowId
                        ? {
                              ...r,
                              [fieldName]: newValue,
                              updating: true
                          }
                        : r
                )
            );
        },
        [rows]
    );

    const multiSetMCarrierRef = () => {
        if (selectedRows) {
            selectedRows.forEach(r => updateRow(r.id, 'masterCarrierRef', masterCarrierRef));
            setEditing(true);
        }
    };

    const handleEditRowsModelChange = useCallback(
        model => {
            if (model && Object.keys(model)[0]) {
                setEditing(true);
                const key = parseInt(Object.keys(model)[0], 10);
                const key2 = Object.keys(model[key])[0];
                if (model && model[key] && model[key][key2] && model[key][key2].value) {
                    updateRow(key, key2, model[key][key2].value);
                }
            }
        },
        [updateRow]
    );

    return (
        <div className="pageContainer">
            <Page requestErrors={requestErrors} showRequestErrors width="xl">
                <Title text="Consignment Export Details" />
                <Grid container spacing={3} style={{ paddingTop: '30px' }}>
                    <Grid item xs={3}>
                        <DatePicker
                            label="From Date"
                            value={fromDate.toString()}
                            onChange={setFromDate}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <DatePicker
                            label="To Date"
                            value={toDate.toString()}
                            minDate={fromDate.toString()}
                            onChange={setToDate}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <Dropdown
                            propertyName="hubId"
                            label="Hub"
                            items={hubOptions()}
                            onChange={updateField}
                            value={hubId}
                            optionsLoading={hubsLoading}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <Button
                            style={{ marginTop: '20px' }}
                            onClick={() => findConsignments()}
                            variant="outlined"
                            color="primary"
                        >
                            Search
                        </Button>
                    </Grid>
                </Grid>
                <Grid container spacing={3} style={{ paddingTop: '30px' }}>
                    {loading ? (
                        <Loading />
                    ) : (
                        <>
                            {consignments?.length > 0 ? (
                                <>
                                    <Grid container spacing={3} style={{ paddingTop: '10px' }}>
                                        <Grid item xs={4}>
                                            <>
                                                <InputField
                                                    label="Master Carrier Ref"
                                                    placeholder="Master Carrier Ref"
                                                    propertyName="consignmentIdSelect"
                                                    value={masterCarrierRef}
                                                    onChange={(_, val) => setMasterCarrierRef(val)}
                                                />
                                                <Button
                                                    style={{ marginTop: '10px' }}
                                                    onClick={() => multiSetMCarrierRef()}
                                                    variant="outlined"
                                                    color="primary"
                                                >
                                                    Set
                                                </Button>
                                            </>
                                        </Grid>
                                    </Grid>
                                    <div style={{ width: '100%' }}>
                                        <DataGrid
                                            rows={rows}
                                            columns={columns}
                                            onEditRowsModelChange={handleEditRowsModelChange}
                                            checkboxSelection
                                            onSelectionModelChange={handleSelectRow}
                                            density="compact"
                                            autoHeight
                                            hideFooter
                                        />
                                    </div>
                                    <SaveBackCancelButtons
                                        saveDisabled={!editing}
                                        saveClick={handleSave}
                                        cancelClick={handleCancel}
                                        backClick={handleCancel}
                                    />
                                </>
                            ) : (
                                <>No Consignments</>
                            )}
                        </>
                    )}
                </Grid>
            </Page>
        </div>
    );
}

ExportConsignments.propTypes = {
    hubs: PropTypes.arrayOf(
        PropTypes.shape({ hubId: PropTypes.number, description: PropTypes.string })
    ),
    hubsLoading: PropTypes.bool,
    options: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string,
        consignmentId: PropTypes.string
    }),
    requestErrors: PropTypes.arrayOf(
        PropTypes.shape({ message: PropTypes.string, name: PropTypes.string })
    ),
    searchConsignments: PropTypes.func.isRequired,
    updateConsignment: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    consignments: PropTypes.arrayOf(PropTypes.shape({}))
};

ExportConsignments.defaultProps = {
    hubs: [],
    hubsLoading: false,
    options: null,
    requestErrors: null,
    consignments: [],
    loading: false
};

export default ExportConsignments;
