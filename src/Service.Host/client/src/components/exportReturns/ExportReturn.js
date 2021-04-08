import React, { useEffect, useReducer } from 'react';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import {
    Loading,
    InputField,
    Title,
    GroupEditTable,
    SnackbarMessage,
    ErrorCard,
    useGroupEditTable
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';
import InputFields from './InputFields';
import DisplayOnlyFields from './DisplayOnlyFields';
import reducer from '../../reducers/componentReducers/exportReturnReducer';

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

export default function ExportReturn({
    exportReturnLoading,
    exportReturn,
    updateExportReturn,
    makeIntercompanyInvoicesMessageVisible,
    makeIntercompanyInvoicesMessageText,
    makeIntercompanyInvoicesErrorMessage,
    makeIntercompanyInvoicesWorking,
    makeIntercompanyInvoices,
    clearMakeIntercompanyInvoicesErrors,
    setMakeIntercompanyInvoicesMessageVisible
}) {
    const [state, dispatch] = useReducer(reducer, {
        exportReturn: null,
        exportReturnDetails: null,
        tab: 0,
        editing: false
    });

    const {
        data: exportReturnDetails,
        addRow,
        updateRow,
        removeRow,
        resetRow,
        setEditing: setTableEditing,
        setTableValid,
        setRowToBeDeleted,
        setRowToBeSaved
    } = useGroupEditTable({
        rows: state.exportReturn?.exportReturnDetails
    });

    useEffect(() => {
        if (exportReturn) {
            dispatch({ type: 'setExportReturn', payload: exportReturn });
        } else {
            dispatch({ type: 'setExportReturn', payload: null });
        }
    }, [exportReturn]);

    const handleFieldChange = (propertyName, newValue) => {
        dispatch({ type: 'setExportReturn', payload: { propertyName, newValue } });
    };

    const handleSaveClick = () => {
        updateExportReturn(state.exportReturn.returnId, {
            ...state.exportReturn,
            exportReturnDetails
        });
    };

    const handleCancelClick = () => {
        updateExportReturn(state.exportReturn.returnId, {
            ...state.exportReturn,
            dateCancelled: new Date().toISOString()
        });
    };

    const handleTabChange = (_event, value) => {
        dispatch({ type: 'setTab', payload: value });
    };

    const handleExportReturnDetailEditClick = (id, editing) => {
        setTableEditing(id, editing);
        dispatch({ type: 'setEditing', payload: editing });
    };

    const handleMakeIntercompanyInvoicesClick = () => {
        clearMakeIntercompanyInvoicesErrors();
        makeIntercompanyInvoices({
            returnId: state.exportReturn.returnId
        });
    };

    const calculateDims = () => {
        let numCartons = 0;
        let grossWeight = 0;
        let grossDims = 0;

        exportReturnDetails.forEach(detail => {
            if (detail.numCartons !== null) {
                numCartons += detail.numCartons;
            }

            if (detail.qty !== null && detail.weight !== null) {
                grossWeight += detail.qty * detail.weight;
            }

            if (
                detail.width !== null &&
                detail.height !== null &&
                detail.depth !== null &&
                detail.qty !== null
            ) {
                grossDims += detail.qty * ((detail.width * detail.height * detail.depth) / 1000000);
            }
        });

        dispatch({ type: 'setDimensions', payload: { numCartons, grossWeight, grossDims } });
    };

    return (
        <Page width="xl">
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Export Return" />
                </Grid>
                <SnackbarMessage
                    visible={makeIntercompanyInvoicesMessageVisible}
                    message={makeIntercompanyInvoicesMessageText}
                    onClose={() => setMakeIntercompanyInvoicesMessageVisible(false)}
                />
                {makeIntercompanyInvoicesErrorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={makeIntercompanyInvoicesErrorMessage} />
                    </Grid>
                )}
                {(exportReturnLoading || makeIntercompanyInvoicesWorking) && (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                )}
                {state.exportReturn?.returnId &&
                    !exportReturnLoading &&
                    !makeIntercompanyInvoicesWorking && (
                        <>
                            <Grid item xs={6}>
                                <InputFields
                                    exportReturn={state.exportReturn}
                                    handleFieldChange={handleFieldChange}
                                    calculateDims={calculateDims}
                                />
                            </Grid>
                            <Grid item xs={6}>
                                <DisplayOnlyFields exportReturn={state.exportReturn} />
                            </Grid>
                            <Grid item xs={12}>
                                <Tabs
                                    value={state.tab}
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

                            {state.tab === 0 && exportReturnDetails?.length && (
                                <Grid item xs={12}>
                                    <GroupEditTable
                                        columns={rsnColumns}
                                        rows={exportReturnDetails}
                                        allowNewRowCreation={false}
                                        updateRow={updateRow}
                                        resetRow={resetRow}
                                        handleEditClick={handleExportReturnDetailEditClick}
                                        setRowToBeDeleted={setRowToBeDeleted}
                                        setRowToBeSaved={setRowToBeSaved}
                                    />
                                </Grid>
                            )}

                            {state.tab === 1 && (
                                <Grid item xs={12}>
                                    <GroupEditTable
                                        columns={rsnInvoiceColumns}
                                        rows={exportReturnDetails}
                                        editable={false}
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

                            {state.tab === 2 && (
                                <>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            value={state.exportReturn.exportCustomsEntryCode}
                                            label="Code"
                                            propertyName="exportCustomsCode"
                                            onChange={handleFieldChange}
                                            margin="dense"
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            value={state.exportReturn.exportCustomsEntryDate}
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

                            <Grid item xs={4}>
                                <Button
                                    variant="outlined"
                                    color="primary"
                                    disabled={!state.editing}
                                    onClick={handleSaveClick}
                                >
                                    Save
                                </Button>
                            </Grid>
                            <Grid item xs={4}>
                                <Button
                                    variant="outlined"
                                    color="primary"
                                    onClick={handleMakeIntercompanyInvoicesClick}
                                >
                                    Make Intercompany Invoices
                                </Button>
                            </Grid>
                            <Grid item xs={4}>
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
    }),
    updateExportReturn: PropTypes.func.isRequired,
    makeIntercompanyInvoicesMessageVisible: PropTypes.bool,
    makeIntercompanyInvoicesMessageText: PropTypes.string,
    makeIntercompanyInvoicesErrorMessage: PropTypes.string,
    makeIntercompanyInvoicesWorking: PropTypes.bool,
    makeIntercompanyInvoices: PropTypes.func.isRequired,
    clearMakeIntercompanyInvoicesErrors: PropTypes.func.isRequired,
    setMakeIntercompanyInvoicesMessageVisible: PropTypes.func.isRequired
};

ExportReturn.defaultProps = {
    exportReturnLoading: false,
    exportReturn: {},
    makeIntercompanyInvoicesMessageVisible: false,
    makeIntercompanyInvoicesMessageText: '',
    makeIntercompanyInvoicesErrorMessage: '',
    makeIntercompanyInvoicesWorking: false
};
