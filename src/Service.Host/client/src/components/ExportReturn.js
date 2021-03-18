import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import moment from 'moment';
import {
    Loading,
    InputField,
    Title,
    GroupEditTable,
    useGroupEditTable
} from '@linn-it/linn-form-components-library';
import Page from '../containers/Page';

const rsnColumns = [
    {
        id: 'rsnNumber',
        title: 'RSN Number',
        type: 'number',
        editable: false
    },
    {
        id: 'articleNumber',
        title: 'Article Number',
        type: 'text',
        editable: false
    },
    {
        id: 'description',
        title: 'Description',
        type: 'text',
        editable: false
    },
    {
        id: 'lineNo',
        title: 'Line Number',
        type: 'number',
        editable: false
    },
    {
        id: 'qty',
        title: 'Quantity',
        type: 'number',
        editable: false
    },
    {
        id: 'customsValue',
        title: 'Customs Value',
        type: 'number',
        editable: true
    },
    {
        id: 'baseCustomsValue',
        title: 'Base Customs Value',
        type: 'number',
        editable: true
    },
    {
        id: 'numCartons',
        title: 'Num Cartons',
        type: 'number',
        editable: true
    },
    {
        id: 'weight',
        title: 'Weight',
        type: 'number',
        editable: true
    },
    {
        id: 'width',
        title: 'Width',
        type: 'number',
        editable: true
    },
    {
        id: 'height',
        title: 'Height',
        type: 'number',
        editable: true
    },
    {
        id: 'depth',
        title: 'Depth',
        type: 'number',
        editable: true
    },
    {
        id: 'tariffId',
        title: 'Tariff ID',
        type: 'number',
        editable: false
    }
];

const rsnInvoiceColumns = [
    {
        id: 'rsnNumber',
        title: 'RSN Number',
        type: 'number',
        editable: false
    },
    {
        id: 'articleNumber',
        title: 'Article Number',
        type: 'text',
        editable: false
    },
    {
        id: 'description',
        title: 'Description',
        type: 'text',
        editable: false
    },
    {
        id: 'expInvDocumentType',
        title: 'Inv Doc Type',
        type: 'text',
        editable: false
    },
    {
        id: 'expInvDocumentNumber',
        title: 'Inv Doc Number',
        type: 'number',
        editable: false
    },
    {
        id: 'expInvDate',
        title: 'Invoice Date',
        type: 'date',
        editable: false
    }
];

export default function ExportReturn({ exportReturnLoading, exportReturn }) {
    const [item, setItem] = useState(null);
    const [exportReturnDetails, setExportReturnDetails] = useState([]);
    const [tab, setTab] = useState(0);

    const {
        data,
        addRow,
        updateRow,
        removeRow,
        resetRow,
        setEditing: setTableEditing,
        setTableValid,
        setRowToBeDeleted,
        setRowToBeSaved
    } = useGroupEditTable({
        rows: exportReturnDetails
    });

    useEffect(() => {
        setItem(exportReturn);

        if (exportReturn?.exportReturnDetails) {
            setExportReturnDetails(
                exportReturn.exportReturnDetails.map(detail => ({
                    ...detail,
                    id: detail.rsnNumber
                }))
            );
        }
    }, [exportReturn]);

    const handleFieldChange = (propertyName, newValue) => {
        setItem(o => ({ ...o, [propertyName]: newValue }));
    };

    const handleTabChange = (event, value) => {
        setTab(value);
    };

    const calculateDims = () => {
        console.log('calculate dims');
    };

    const handleSaveClick = () => {
        console.log('save all the stuff');
    };

    const handleCancelClick = () => {
        console.log('cancel export return');
    };

    const handleGenerateInvoices = () => {
        console.log('generate invoices');
    };

    const DisplayOnlyFields = () => (
        <Paper>
            <List dense>
                <ListItem>
                    <ListItemText primary="Return ID" secondary={item.returnId} />
                </ListItem>
                <ListItem>
                    <ListItemText primary="Return for Credit" secondary={item.returnForCredit} />
                </ListItem>
                <ListItem>
                    <ListItemText
                        primary="Rasied By"
                        secondary={`${item.raisedBy.id} - ${item.raisedBy.fullName}`}
                    />
                </ListItem>
                <ListItem>
                    <ListItemText
                        primary="Date Created"
                        secondary={moment(item.dateCreated).format('DD MMM YYYY')}
                    />
                </ListItem>
                {item.dateCancelled && (
                    <ListItem>
                        <ListItemText primary="Date Cancelled" secondary={item.dateCancelled} />
                    </ListItem>
                )}
                <ListItem>
                    <ListItemText primary="Account" secondary={item.accountId} />
                </ListItem>
                <ListItem>
                    <ListItemText
                        primary="Outlet"
                        secondary={`${item.outletNumber} - ${item.salesOutlet.name}`}
                    />
                </ListItem>
                <ListItem>
                    <ListItemText primary="Currency" secondary={item.currency} />
                </ListItem>
                <ListItem>
                    <ListItemText
                        primary="Hub"
                        secondary={`${item.hubId}${item.hubName && `- ${item.hubName}`}`}
                    />
                </ListItem>
                {item.dateDispatched && (
                    <ListItem>
                        <ListItemText primary="Date Dispatched" secondary={item.dateDispatched} />
                    </ListItem>
                )}
                <ListItem>
                    <ListItemText primary="Terms" secondary={item.terms} />
                </ListItem>
            </List>
        </Paper>
    );

    const InputFields = () => (
        <Grid container spacing={3}>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={item.carrierCode}
                    label="Carrier Code"
                    propertyName="carrierCode"
                    onChange={handleFieldChange}
                    margin="dense"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={item.carrierRef}
                    label="Carrier Reference"
                    propertyName="carrierRef"
                    onChange={handleFieldChange}
                    margin="dense"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={item.numPallets}
                    label="Num Pallets"
                    propertyName="numPallets"
                    onChange={handleFieldChange}
                    margin="dense"
                    type="number"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={item.numCartons}
                    label="Num Cartons"
                    propertyName="numCartons"
                    onChange={handleFieldChange}
                    margin="dense"
                    type="number"
                />
            </Grid>

            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={item.grossWeight}
                    label="Gross Weight"
                    propertyName="grossWeight"
                    onChange={handleFieldChange}
                    margin="dense"
                    type="number"
                />
            </Grid>
            <Grid item xs={6}>
                <InputField
                    fullWidth
                    value={item.grossDims}
                    label="Gross Dims"
                    propertyName="grossDims"
                    onChange={handleFieldChange}
                    margin="dense"
                    type="number"
                />
            </Grid>

            <Grid item xs={12}>
                <Button variant="outlined" color="primary" onClick={() => calculateDims()}>
                    Calculate Dimensions from RSNs
                </Button>
            </Grid>
        </Grid>
    );

    return (
        <Page width="xl">
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Export Return" />
                </Grid>
                {exportReturnLoading && <Loading />}
                {item && (
                    <>
                        <Grid item xs={6}>
                            <InputFields />
                        </Grid>
                        <Grid item xs={6}>
                            <DisplayOnlyFields />
                        </Grid>
                        <Grid item xs={12}>
                            <Tabs
                                value={tab}
                                onChange={handleTabChange}
                                indicatorColor="primary"
                                textColor="primary"
                                style={{ paddingBottom: '40px' }}
                            >
                                <Tab label="RSNs" />
                                <Tab label="RSN Invoice Details" />
                                <Tab label="Inter Company Invoices" />
                                <Tab label="Export Customs Entry" />
                            </Tabs>
                        </Grid>

                        {tab === 0 && exportReturnDetails.length && (
                            <Grid item xs={12}>
                                <GroupEditTable
                                    columns={rsnColumns}
                                    rows={data}
                                    allowNewRowCreation={false}
                                    updateRow={updateRow}
                                    addRow={addRow}
                                    removeRow={removeRow}
                                    resetRow={resetRow}
                                    handleEditClick={setTableEditing}
                                    tableValid={setTableValid}
                                    setRowToBeDeleted={setRowToBeDeleted}
                                    setRowToBeSaved={setRowToBeSaved}
                                />
                            </Grid>
                        )}

                        {tab === 1 && (
                            <Grid item xs={12}>
                                <GroupEditTable
                                    columns={rsnInvoiceColumns}
                                    rows={data}
                                    allowNewRowCreation={false}
                                    updateRow={updateRow}
                                    addRow={addRow}
                                    removeRow={removeRow}
                                    resetRow={resetRow}
                                    handleEditClick={setTableEditing}
                                    tableValid={setTableValid}
                                    setRowToBeDeleted={setRowToBeDeleted}
                                    setRowToBeSaved={setRowToBeSaved}
                                />
                            </Grid>
                        )}

                        {tab === 2 && (
                            <>
                                <Grid item xs={4}>
                                    <Button
                                        variant="outlined"
                                        color="primary"
                                        onClick={handleGenerateInvoices}
                                    >
                                        Generate Invoices
                                    </Button>
                                </Grid>
                                <Grid item xs={8} />
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={item.interDocNumber}
                                        label="Inter Doc Number"
                                        propertyName="interDocNumber"
                                        onChange={handleFieldChange}
                                        margin="dense"
                                        type="number"
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={item.interDocType}
                                        label="Inter Doc Type"
                                        propertyName="interDocType"
                                        onChange={handleFieldChange}
                                        margin="dense"
                                    />
                                </Grid>
                                <Grid item xs={4} />
                            </>
                        )}

                        {tab === 3 && (
                            <>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={item.exportCustomsCode}
                                        label="Code"
                                        propertyName="exportCustomsCode"
                                        onChange={handleFieldChange}
                                        margin="dense"
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={item.exportCustomsCode}
                                        label="Date"
                                        propertyName="exportCustomsCode"
                                        onChange={handleFieldChange}
                                        margin="dense"
                                        type="date"
                                    />
                                </Grid>
                                <Grid item xs={4} />
                            </>
                        )}

                        <Grid item xs={2}>
                            <Button
                                variant="outlined"
                                color="primary"
                                disabled
                                onClick={handleSaveClick}
                            >
                                Save
                            </Button>
                        </Grid>
                        <Grid item xs={8}>
                            <Button
                                variant="outlined"
                                color="secondary"
                                onClick={handleCancelClick}
                            >
                                Cancel Export Return
                            </Button>
                        </Grid>
                    </>
                )}
            </Grid>
        </Page>
    );
}

ExportReturn.propTypes = {
    exportReturnLoading: PropTypes.bool,
    exportReturn: PropTypes.shape({
        exportReturnDetails: PropTypes.arrayOf(PropTypes.shape({}))
    })
};

ExportReturn.defaultProps = {
    exportReturnLoading: false,
    exportReturn: {}
};
