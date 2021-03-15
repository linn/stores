import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import {
    Loading,
    InputField,
    DatePicker,
    Title,
    GroupEditTable,
    useGroupEditTable
} from '@linn-it/linn-form-components-library';
import Page from '../containers/Page';

const columns = [
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
        id: 'description',
        title: 'Description',
        type: 'text',
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
        id: 'tariffId',
        title: 'Tariff ID',
        type: 'number',
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
    // const [editing, setEditing] = useState(false);
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

    const calculateDims = () => {
        console.log('calculate dims');
    };

    const handleTabChange = (event, value) => {
        setTab(value);
    };

    return (
        <Page width="xl">
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Export Return" />
                </Grid>
                {exportReturnLoading && <Loading />}
                {item && (
                    <>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.returnId}
                                label="Return ID"
                                propertyName="returnId"
                                onChange={handleFieldChange}
                                type="number"
                                disabled
                                margin="dense"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.returnForCredit}
                                label="Return For Credit"
                                propertyName="returnForCredit"
                                onChange={handleFieldChange}
                                type="number"
                                disabled
                                margin="dense"
                            />
                        </Grid>
                        <Grid item xs={4} />

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.raisedBy?.id}
                                label="Raised By"
                                propertyName="raisedBy"
                                onChange={handleFieldChange}
                                disabled
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.raisedBy?.fullName}
                                label="Raised By"
                                propertyName="raisedBy"
                                onChange={handleFieldChange}
                                disabled
                            />
                        </Grid>
                        <Grid item xs={4} />

                        <Grid item xs={4}>
                            <DatePicker
                                label="Date Created"
                                value={item.dateCreated}
                                onChange={value => {
                                    handleFieldChange('dateCreated', value);
                                }}
                                disabled
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <DatePicker
                                label="Date Cancelled"
                                value={item.dateCancelled}
                                onChange={value => {
                                    handleFieldChange('dateCancelled', value);
                                }}
                            />
                        </Grid>
                        <Grid item xs={4} />

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.accountId}
                                label="Account Number"
                                propertyName="accountId"
                                onChange={handleFieldChange}
                                type="number"
                                margin="dense"
                                disabled
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.outletNumber}
                                label="Outlet Number"
                                propertyName="outletNumber"
                                onChange={handleFieldChange}
                                type="number"
                                margin="dense"
                                disabled
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.salesOutlet.name}
                                label="Name"
                                propertyName="outletName"
                                onChange={handleFieldChange}
                                margin="dense"
                                disabled
                            />
                        </Grid>

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.currency}
                                label="Currency"
                                propertyName="currency"
                                onChange={handleFieldChange}
                                margin="dense"
                            />
                        </Grid>
                        <Grid item xs={8} />

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.carrierCode}
                                label="Carrier Code"
                                propertyName="carrierCode"
                                onChange={handleFieldChange}
                                margin="dense"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.carrierRef}
                                label="Carrier Reference"
                                propertyName="carrierRef"
                                onChange={handleFieldChange}
                                margin="dense"
                            />
                        </Grid>
                        <Grid item xs={4} />

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.hubId}
                                label="Hub"
                                propertyName="hubId"
                                onChange={handleFieldChange}
                                type="number"
                                margin="dense"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.hubName}
                                label="Name"
                                propertyName="hubName"
                                onChange={handleFieldChange}
                                type="number"
                                margin="dense"
                            />
                        </Grid>
                        <Grid item xs={4} />

                        <Grid item xs={4}>
                            <DatePicker
                                label="Date Dispatched"
                                value={item.dateDispatched}
                                onChange={value => {
                                    handleFieldChange('dateDispatched', value);
                                }}
                            />
                        </Grid>
                        <Grid item xs={8} />

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.terms}
                                label="Terms"
                                propertyName="terms"
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={8} />

                        <Grid item xs={4}>
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
                        <Grid item xs={4}>
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
                        <Grid item xs={4} />

                        <Grid item xs={4}>
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
                        <Grid item xs={4}>
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
                        <Grid item xs={4} />

                        <Grid item xs={12}>
                            <Button
                                variant="outlined"
                                color="primary"
                                onClick={() => calculateDims()}
                            >
                                Calculate Dimensions from RSNs
                            </Button>
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
                                <Tab label="Inter Company Invoices" />
                                <Tab label="Export Customs Entry" />
                            </Tabs>
                        </Grid>

                        {tab === 0 && exportReturnDetails.length && (
                            <Grid item xs={12}>
                                <GroupEditTable
                                    columns={columns}
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
                            <>
                                <Grid item xs={4}>
                                    <Button
                                        variant="outlined"
                                        color="primary"
                                        onClick={() => {
                                            console.log('generate invoices');
                                        }}
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

                        {tab === 2 && (
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

                        <Grid item xs={12}>
                            <Button variant="outlined" color="primary" disabled onClick={() => {}}>
                                Update
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
