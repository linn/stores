import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import makeStyles from '@material-ui/styles/makeStyles';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import { DataGrid } from '@mui/x-data-grid';
import Typography from '@material-ui/core/Typography';
import LinearProgress from '@material-ui/core/LinearProgress';
import {
    InputField,
    Typeahead,
    Dropdown,
    CheckboxWithLabel,
    Loading
} from '@linn-it/linn-form-components-library';
import QcLabelPrintScreen from '../../containers/goodsIn/QcLabelPrintScreen';
import Page from '../../containers/Page';
import Parcel from '../../containers/parcels/Parcel';
import LoanDetails from './LoanDetails';
import RsnDetails from './RsnDetails';

function GoodsInUtility({
    validatePurchaseOrder,
    validatePurchaseOrderResult,
    validatePurchaseOrderResultLoading,
    searchStoragePlaces,
    storagePlacesSearchResults,
    storagePlacesSearchLoading,
    bookInResult,
    bookInResultLoading,
    doBookIn,
    validatePurchaseOrderBookInQtyResult,
    validatePurchaseOrderBookInQty,
    validatePurchaseOrderBookInQtyResultLoading,
    userNumber,
    validateStorageType,
    validateStorageTypeResult,
    validateStorageTypeResultLoading,
    match,
    history,
    loanDetails,
    getLoanDetails,
    loanDetailsLoading,
    rsnConditions,
    rsnConditionsLoading,
    getRsnConditions,
    rsnAccessories,
    rsnAccessoriesLoading,
    getRsnAccessories,
    validateRsn,
    validateRsnResult,
    validateRsnResultLoading,
    clearPo,
    clearRsn,
    printRsnResult,
    printRsnLoading,
    printRsn,
    parcel
}) {
    const [formData, setFormData] = useState({
        orderNumber: null,
        thisBookIn: 0,
        dateReceived: new Date(),
        lines: []
    });

    const [message, setMessage] = useState({ error: false, text: '' });

    const [multipleBookIn, setMultipleBookIn] = useState(false);
    const [printRsnLabels, setPrintRsnLabels] = useState(false);

    const [lines, setLines] = useState([]);

    const [selectedRows, setSelectedRows] = useState([]);

    useEffect(() => {
        if (
            !validatePurchaseOrderResult &&
            !validateRsnResult &&
            !validatePurchaseOrderBookInQtyResult &&
            !validateStorageTypeResult
        ) {
            setMessage({ text: '', error: false });
        }
    }, [
        validatePurchaseOrderResult,
        validateRsnResult,
        validatePurchaseOrderBookInQtyResult,
        validateStorageTypeResult
    ]);

    const getMessageColour = () => {
        if (bookInResult?.success) {
            return 'limegreen';
        }
        if (message?.error) {
            return 'red';
        }
        return 'black';
    };

    const useStyles = makeStyles(theme => ({
        dialog: {
            margin: theme.spacing(6),
            minWidth: theme.spacing(70)
        },
        notchedOutline: {
            borderWidth: '3px',
            borderColor: `${getMessageColour()} !important`
        },
        padBottom: {
            paddingBottom: theme.spacing(6)
        }
    }));

    const handleFieldChange = (propertyName, newValue) => {
        setFormData({ ...formData, [propertyName]: newValue });
    };

    const [printDialogOpen, setPrintDialogOpen] = useState(false);

    const [qcDialogOpen, setQcDialogOpen] = useState(false);

    const [parcelDialogOpen, setParcelDialogOpen] = useState(false);

    const [loanDetailsDialogOpen, setLoanDetailsDialogOpen] = useState(false);

    const [rsnDetailsDialogOpen, setRsnDetailsDialogOpen] = useState(false);

    const [rsnConditionsString, setRsnConditionsString] = useState('');

    const [rsnAccessoriesString, setRsnAccessoriesString] = useState('');

    const [parcelNumber, setParcelNumber] = useState();

    useEffect(() => {
        if (parcel?.parcelNumber) {
            setParcelDialogOpen(false);
            setParcelNumber(parcel.parcelNumber);
        }
    }, [parcel]);

    const handleSelectLoanDetails = details => {
        setLoanDetailsDialogOpen(false);
        setLines(l => [
            ...l,
            ...details.map((detail, i) => ({
                id: l.length + i,
                articleNumber: detail.articleNumber,
                transactionType: 'L',
                dateCreated: new Date().toISOString(),
                location: formData?.ontoLocation.toUpperCase(),
                locationId: formData?.ontoLocationId,
                quantity: detail.return,
                serialNumber: detail.serialNumber,
                serialNumber2: details.serialNumber2,
                loanNumber: detail.loanNumber,
                loanLine: detail.line,
                createdBy: userNumber,
                state: detail.state
            }))
        ]);
    };

    const handleSelectRsnDetails = (accessories, conditions) => {
        setRsnDetailsDialogOpen(false);
        setRsnAccessoriesString(
            accessories?.length
                ? accessories.map(a => `${a.description}-${a.extraInfo}`).join(',')
                : 'No accessories'
        );
        setRsnConditionsString(
            conditions?.length
                ? conditions.map(a => `${a.description}-${a.extraInfo}`).join(',')
                : 'Good condition'
        );
        setLines(l => [
            {
                id: l.length + 1,
                articleNumber: validateRsnResult?.articleNumber,
                transactionType: 'R',
                dateCreated: new Date().toISOString(),
                location: formData?.ontoLocation?.toUpperCase(),
                locationId: formData?.ontoLocationId,
                quantity: validateRsnResult?.quantity,
                serialNumber: validateRsnResult?.serialNumber,
                createdBy: userNumber,
                rsnNumber: formData?.rsnNumber,
                state: validateRsnResult?.state
            }
        ]);
    };

    const [tab, setTab] = useState(0);

    const handleTabChange = (event, value) => {
        setTab(value);
    };

    useEffect(() => {
        if (validatePurchaseOrderResult) {
            setMessage({
                error: !!validatePurchaseOrderResult.message,
                text: validatePurchaseOrderResult.message
            });
            if (validatePurchaseOrderResult.qcPart === 'Yes') {
                setQcDialogOpen(true);
            }
        }
    }, [validatePurchaseOrderResult]);

    useEffect(() => {
        if (validateRsnResult) {
            setMessage({
                error: !!validateRsnResult.message,
                text: validateRsnResult.message
            });
        }
    }, [validateRsnResult]);

    useEffect(() => {
        if (loanDetails?.length > 0) {
            setLoanDetailsDialogOpen(true);
        } else {
            setLoanDetailsDialogOpen(false);
        }
    }, [loanDetails]);

    useEffect(() => {
        if (!validateRsnResult?.success) {
            setRsnDetailsDialogOpen(false);
        }
    }, [validateRsnResult]);

    useEffect(() => {
        if (validatePurchaseOrderBookInQtyResult) {
            if (validatePurchaseOrderBookInQtyResult.success) {
                setFormData(d => ({ ...d, numberOfLines: 1 }));
            }
            setMessage({
                error: !validatePurchaseOrderBookInQtyResult?.success,
                text: validatePurchaseOrderBookInQtyResult?.message
            });
        }
    }, [validatePurchaseOrderBookInQtyResult]);

    useEffect(() => {
        if (validateStorageTypeResult?.message) {
            setMessage({ error: true, text: validateStorageTypeResult?.message });
        } else {
            setFormData(d => ({ ...d, ontoLocation: validateStorageTypeResult?.locationCode }));
        }
    }, [validateStorageTypeResult]);

    useEffect(() => {
        if (bookInResult?.message) {
            setMessage({ error: !bookInResult.success, text: bookInResult.message });
        }
        if (bookInResult?.success && bookInResult.printLabels) {
            setPrintDialogOpen(true);
            setLines([]);
        }
        if (bookInResult?.createParcel) {
            setParcelDialogOpen(true);
        } else {
            setParcelDialogOpen(false);
        }

        if (['L', 'R'].includes(bookInResult?.transactionCode)) {
            setLines([]);
        }
    }, [bookInResult]);

    useEffect(() => {
        const total = lines.reduce((a, b) => a + (b.quantity || 0), 0);
        setFormData(d => ({ ...d, thisBookIn: total }));
    }, [lines]);

    const classes = useStyles();

    const getTransactionType = () => {
        if (formData.loanNumber) {
            return 'L';
        }
        if (formData.rsnNumber) {
            return 'R';
        }
        return validatePurchaseOrderResult.transactionType || 'O';
    };

    const tableColumns = [
        {
            headerName: 'id',
            field: 'id',
            width: 100,
            hide: true
        },
        {
            headerName: 'Trans Type',
            field: 'transactionType',
            width: 200
        },
        {
            headerName: 'Created',
            field: 'dateCreated',
            width: 100,
            hide: true
        },
        {
            headerName: 'By',
            field: 'createdBy',
            width: 100,
            hide: true
        },
        {
            headerName: 'Wandstring',
            field: 'wandstring',
            hide: true
        },
        {
            headerName: 'Article',
            field: 'articleNumber',
            width: 200
        },
        {
            headerName: 'LocId',
            field: 'locationId',
            width: 200,
            hide: true
        },
        {
            headerName: 'Loc',
            field: 'location',
            width: 200
        },
        {
            headerName: 'Qty',
            field: 'quantity',
            width: 100
        },
        {
            headerName: 'Serial',
            field: 'serialNumber',
            hide: true,
            width: 200
        },
        {
            headerName: 'Serial 2',
            field: 'serialNumber2',
            width: 200,
            hide: true
        },
        {
            headerName: 'Order',
            field: 'orderNumber',
            width: 200
        },
        {
            headerName: 'Line',
            field: 'orderLine',
            width: 200,
            hide: true
        },
        {
            headerName: 'S/Type',
            field: 'storageType',
            width: 200
        },
        {
            headerName: 'Manuf Part',
            field: 'manufacturersPartNumber',
            width: 200
        },
        {
            headerName: 'Loan',
            field: 'loanNumber',
            hide: true,
            width: 200
        },
        {
            headerName: 'Line',
            hide: true,
            field: 'loanLine',
            width: 200
        },
        {
            headerName: 'Rsn',
            hide: true,
            field: 'rsnNumber',
            width: 200
        },
        {
            headerName: 'State',
            field: 'state',
            hide: true,
            width: 200
        },
        {
            headerName: 'Description',
            field: 'description',
            width: 300,
            hide: true
        }
    ];

    const handleSelectRow = selected => {
        setSelectedRows(lines.filter(r => selected.includes(r.id)));
    };

    if (bookInResultLoading) {
        return (
            <Page>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                </Grid>
            </Page>
        );
    }

    return (
        <Page>
            <Grid container spacing={3}>
                <Dialog open={qcDialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setQcDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <Grid container spacing={3}>
                                <Grid item xs={12}>
                                    <Typography variant="h5" color="secondary">
                                        {`Note: ${validatePurchaseOrderResult?.partNumber} part is in QC`}
                                    </Typography>
                                </Grid>

                                <Grid item xs={4} />
                                <Grid item xs={4}>
                                    <Button
                                        onClick={() => setQcDialogOpen(false)}
                                        variant="contained"
                                    >
                                        Acknowledge and continue
                                    </Button>
                                </Grid>
                                <Grid item xs={4} />
                            </Grid>
                        </div>
                    </div>
                </Dialog>
                <Dialog open={printDialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setPrintDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <QcLabelPrintScreen
                                kardexLocation={bookInResult?.kardexLocation}
                                partNumber={bookInResult?.partNumber}
                                partDescription={bookInResult?.description}
                                reqNumber={bookInResult?.reqNumber}
                                orderNumber={bookInResult?.orderNumber}
                                qcState={bookInResult?.qcState}
                                qcInfo={bookInResult?.qcInfo}
                                docType={bookInResult?.docType}
                                unitOfMeasure={bookInResult?.unitOfMeasure}
                                qtyReceived={bookInResult?.qtyReceived}
                            />
                        </div>
                    </div>
                </Dialog>
                <Dialog open={parcelDialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setParcelDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <Parcel
                                comments={bookInResult?.parcelComments}
                                supplierId={bookInResult?.supplierId || ''}
                                match={match}
                                inDialogBox
                                closeDialog={() => setParcelDialogOpen(false)}
                                history={history}
                            />
                        </div>
                    </div>
                </Dialog>
                <Dialog open={loanDetailsDialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setLoanDetailsDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <LoanDetails
                                loanDetails={loanDetails?.map(d => ({
                                    ...d,
                                    id: d.line,
                                    return: d.qtyOnLoan,
                                    selected: false
                                }))}
                                onConfirm={handleSelectLoanDetails}
                            />
                        </div>
                    </div>
                </Dialog>
                <Dialog open={rsnDetailsDialogOpen} fullWidth maxWidth="md">
                    <div>
                        <IconButton
                            className={classes.pullRight}
                            aria-label="Close"
                            onClick={() => setRsnDetailsDialogOpen(false)}
                        >
                            <CloseIcon />
                        </IconButton>
                        <div className={classes.dialog}>
                            <RsnDetails
                                rsnAccessories={rsnAccessories?.map(d => ({
                                    ...d,
                                    id: d.code,
                                    extraInfo: ''
                                }))}
                                rsnConditions={rsnConditions?.map(d => ({
                                    ...d,
                                    id: d.code,
                                    extraInfo: ''
                                }))}
                                onConfirm={handleSelectRsnDetails}
                            />
                        </div>
                    </div>
                </Dialog>
                <Grid item xs={6}>
                    <Typography variant="h3"> Goods In Utility </Typography>
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        fullWidth
                        disabled
                        textFieldProps={{
                            InputProps: {
                                classes: {
                                    notchedOutline: classes.notchedOutline
                                }
                            }
                        }}
                        error={message.error}
                        value={
                            validatePurchaseOrderResultLoading ||
                            validatePurchaseOrderBookInQtyResultLoading ||
                            bookInResultLoading ||
                            validateStorageTypeResultLoading ||
                            loanDetailsLoading
                                ? 'loading'
                                : message.text
                        }
                        label="Message"
                        propertyName="message"
                    />
                </Grid>
                {parcelNumber && (
                    <>
                        <Grid item xs={9} />
                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                disabled
                                textFieldProps={{
                                    InputProps: {
                                        classes: {
                                            notchedOutline: classes.notchedOutline
                                        }
                                    }
                                }}
                                value={parcelNumber}
                                label="Parcel Created"
                                onChange={() => {}}
                                propertyName="parcelNumber"
                            />
                        </Grid>
                    </>
                )}
                <Grid item xs={3}>
                    <Typeahead
                        onSelect={newValue =>
                            setFormData(d => ({
                                ...d,
                                ontoLocation: newValue.name,
                                ontoLocationId: newValue.locationId,
                                palletNumber: newValue.palletNumber
                            }))
                        }
                        label="Onto Location"
                        modal
                        openModalOnClick={false}
                        handleFieldChange={(_, newValue) => {
                            setFormData(d => ({ ...d, ontoLocation: newValue }));
                        }}
                        propertyName="ontoLocation"
                        items={storagePlacesSearchResults}
                        value={formData?.ontoLocation}
                        loading={storagePlacesSearchLoading}
                        fetchItems={searchStoragePlaces}
                        links={false}
                        text
                        clearSearch={() => {}}
                        placeholder="Search Locations"
                        minimumSearchTermLength={3}
                    />
                </Grid>
                <Grid item xs={2}>
                    <InputField
                        fullWidth
                        value={formData.storageType}
                        label="S/Type*"
                        disabled={
                            validatePurchaseOrderResult?.message !==
                                'New part - enter storage type or location' &&
                            validatePurchaseOrderResult?.storage === 'BB'
                        }
                        propertyName="storageType"
                        onChange={handleFieldChange}
                        textFieldProps={{
                            onKeyDown: data => {
                                if (
                                    formData.storageType &&
                                    (data.keyCode === 13 || data.keyCode === 9)
                                ) {
                                    validateStorageType(`storageType`, formData.storageType);
                                }
                            }
                        }}
                    />
                </Grid>
                <Grid item xs={1}>
                    {validateStorageTypeResultLoading && <Loading />}
                </Grid>
                <Grid item xs={6} />
                <Grid item xs={12}>
                    <Typography>
                        Note: press enter or tab on fields marked with a * to validate / fill
                        related fields
                    </Typography>
                </Grid>
                <Grid item xs={6}>
                    <Tabs
                        value={tab}
                        onChange={handleTabChange}
                        indicatorColor="primary"
                        textColor="primary"
                    >
                        <Tab label="PO" />
                        <Tab label="LOAN" />
                        <Tab label="RSN" />
                    </Tabs>
                </Grid>
                {tab === 0 && (
                    <Grid item xs={12}>
                        <Grid container spacing={3}>
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    type="number"
                                    value={formData.orderNumber}
                                    label="Order Number*"
                                    disabled={
                                        validatePurchaseOrderResultLoading ||
                                        formData?.rsnNumber ||
                                        formData?.loanNumber
                                    }
                                    propertyName="orderNumber"
                                    onChange={handleFieldChange}
                                    textFieldProps={{
                                        onKeyDown: data => {
                                            if (
                                                formData.orderNumber &&
                                                (data.keyCode === 13 || data.keyCode === 9)
                                            ) {
                                                validatePurchaseOrder(formData.orderNumber);
                                            }
                                        }
                                    }}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    fullWidth
                                    value={validatePurchaseOrderResult?.orderLine}
                                    label="Line"
                                    disabled
                                    propertyName="orderLine"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    value={formData.qty}
                                    label="Qty*"
                                    propertyName="qty"
                                    type="number"
                                    disabled={
                                        !validatePurchaseOrderResult ||
                                        !!validatePurchaseOrderResult?.message
                                    }
                                    textFieldProps={{
                                        onKeyDown: data => {
                                            if (
                                                formData?.qty &&
                                                (data.keyCode === 13 || data.keyCode === 9)
                                            ) {
                                                validatePurchaseOrderBookInQty(
                                                    `qty=${
                                                        formData.qty
                                                    }&orderLine=${1}&orderNumber`,
                                                    formData.orderNumber
                                                );
                                            }
                                        }
                                    }}
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                {validatePurchaseOrderResultLoading && <LinearProgress />}
                            </Grid>
                            <Grid item xs={1}>
                                {validatePurchaseOrderBookInQtyResultLoading && <LinearProgress />}
                            </Grid>
                            <Grid item xs={1} />

                            <Grid item xs={1}>
                                <InputField
                                    fullWidth
                                    value={validatePurchaseOrderResult?.documentType}
                                    label="Type"
                                    disabled
                                    propertyName="documentType"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={validatePurchaseOrderResult?.orderQty}
                                    label="Order Qty"
                                    disabled
                                    type="number"
                                    propertyName="orderQty"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={validatePurchaseOrderResult?.qtyBookedIn}
                                    label="Booked In"
                                    type="number"
                                    disabled
                                    propertyName="qtyBookedIn"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={formData?.thisBookIn}
                                    label="This Bookin"
                                    disabled
                                    type="number"
                                    propertyName="thisBookIn"
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    fullWidth
                                    value={validatePurchaseOrderResult?.storage}
                                    label="Storage"
                                    disabled
                                    propertyName="storage"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    fullWidth
                                    value={validatePurchaseOrderResult?.qcPart}
                                    label="QC"
                                    disabled
                                    propertyName="qcPart"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <Dropdown
                                    items={['STORES', 'QC', 'FAIL']}
                                    propertyName="state"
                                    fullWidth
                                    allowNoValue
                                    disabled
                                    label="State"
                                    onChange={handleFieldChange}
                                    type="state"
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <InputField
                                    fullWidth
                                    value={validatePurchaseOrderResult?.partNumber}
                                    label="Part"
                                    disabled
                                    propertyName="partNumber"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={5}>
                                <InputField
                                    fullWidth
                                    value={validatePurchaseOrderResult?.partDescription}
                                    label="Description"
                                    disabled
                                    propertyName="partDescription"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={validatePurchaseOrderResult?.orderUnitOfMeasure}
                                    label="UOM"
                                    disabled
                                    propertyName="orderUnitOfMeasure"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={validatePurchaseOrderResult?.manufacturersPartNumber}
                                    label="MFPN"
                                    disabled
                                    propertyName="manufacturersPartNumber"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <CheckboxWithLabel
                                    label="Multiple Book In?"
                                    checked={multipleBookIn}
                                    onChange={() => setMultipleBookIn(m => !m)}
                                />
                            </Grid>
                        </Grid>
                    </Grid>
                )}
                {tab === 1 && (
                    <Grid item xs={12}>
                        <Grid container spacing={3}>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={formData?.loanNumber}
                                    label="Loan Number*"
                                    propertyName="loanNumber"
                                    disabled={
                                        formData?.orderNumber ||
                                        formData?.rsnNumber ||
                                        !formData?.ontoLocation
                                    }
                                    onChange={handleFieldChange}
                                    textFieldProps={{
                                        onKeyDown: data => {
                                            if (
                                                formData?.loanNumber &&
                                                (data.keyCode === 13 || data.keyCode === 9)
                                            ) {
                                                getLoanDetails('loanNumber', formData.loanNumber);
                                            }
                                        }
                                    }}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                {loanDetailsLoading && <Loading />}
                            </Grid>
                            <Grid item xs={8} />
                        </Grid>
                    </Grid>
                )}
                {tab === 2 && (
                    <Grid item xs={12}>
                        <Grid container spacing={3}>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={formData?.rsnNumber}
                                    label="RSN Number*"
                                    propertyName="rsnNumber"
                                    disabled={formData?.orderNumber || formData?.loanNumber}
                                    onChange={handleFieldChange}
                                    textFieldProps={{
                                        onKeyDown: data => {
                                            if (
                                                formData?.rsnNumber &&
                                                (data.keyCode === 13 || data.keyCode === 9)
                                            ) {
                                                validateRsn(formData?.rsnNumber);
                                                getRsnAccessories();
                                                getRsnConditions();
                                                setRsnAccessoriesString('');
                                                setRsnConditionsString('');
                                            }
                                        }
                                    }}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                {(rsnAccessoriesLoading ||
                                    rsnConditionsLoading ||
                                    validateRsnResultLoading) && <Loading />}
                            </Grid>
                            <Grid item xs={6} />
                            <Grid item xs={2}>
                                {tab === 2 && (
                                    <Button
                                        variant="contained"
                                        color="secondary"
                                        disabled={!validateRsnResult}
                                        onClick={() => printRsn({ rsnNumber: formData.rsnNumber })}
                                    >
                                        Reprint RSN
                                    </Button>
                                )}
                            </Grid>
                            <Grid item xs={12}>
                                {printRsnLoading && <LinearProgress />}
                                {printRsnResult?.success && (
                                    <Typography variant="h6"> Print requested.</Typography>
                                )}
                            </Grid>
                            <Grid item xs={6}>
                                <InputField
                                    fullWidth
                                    disabled={
                                        !formData?.rsnNumber ||
                                        rsnAccessoriesLoading ||
                                        rsnConditionsLoading ||
                                        validateRsnResultLoading
                                    }
                                    textFieldProps={{
                                        onClick: () => setRsnDetailsDialogOpen(true)
                                    }}
                                    value={rsnAccessoriesString}
                                    label="Accessories"
                                    propertyName="rsnAccessoriesString"
                                    onChange={() => {}}
                                />
                            </Grid>
                            <Grid item xs={6}>
                                <InputField
                                    fullWidth
                                    value={rsnConditionsString}
                                    disabled={
                                        !formData?.rsnNumber ||
                                        rsnAccessoriesLoading ||
                                        rsnConditionsLoading ||
                                        validateRsnResultLoading
                                    }
                                    textFieldProps={{
                                        onClick: () => setRsnDetailsDialogOpen(true)
                                    }}
                                    label="Conditions"
                                    propertyName="rsnConditionsString"
                                    onChange={() => {}}
                                />
                            </Grid>
                            <Grid item xs={4}>
                                <InputField
                                    disabled
                                    fullWidth
                                    value={validateRsnResult?.articleNumber}
                                    label="Article"
                                    propertyName="rsnArticleNumber"
                                    onChange={() => {}}
                                />
                            </Grid>
                            <Grid item xs={4}>
                                <InputField
                                    disabled
                                    fullWidth
                                    value={validateRsnResult?.description}
                                    label="Desc"
                                    propertyName="rsnArticleDesc"
                                    onChange={() => {}}
                                />
                            </Grid>
                            <Grid item xs={4}>
                                <InputField
                                    disabled
                                    fullWidth
                                    value={validateRsnResult?.quantity}
                                    label="Quantity"
                                    propertyName="rsnQty"
                                    onChange={() => {}}
                                />
                            </Grid>
                            <Grid item xs={4}>
                                <InputField
                                    disabled
                                    fullWidth
                                    value={validateRsnResult?.state}
                                    label="State"
                                    propertyName="rsnState"
                                    onChange={() => {}}
                                />
                            </Grid>
                            <Grid item xs={4}>
                                <InputField
                                    disabled
                                    fullWidth
                                    value={validateRsnResult?.serialNumber}
                                    label="Serial"
                                    propertyName="rsnSerialNumber"
                                    onChange={() => {}}
                                />
                            </Grid>
                            <Grid item xs={4} />
                        </Grid>
                    </Grid>
                )}
                <Grid item xs={12}>
                    <div
                        style={{
                            width: '100%'
                        }}
                    >
                        <DataGrid
                            autoHeight
                            rows={lines}
                            columns={tableColumns}
                            density="standard"
                            rowHeight={34}
                            checkboxSelection
                            onSelectionModelChange={handleSelectRow}
                            hideFooter
                        />
                    </div>
                </Grid>
                <Grid item xs={12}>
                    <Button
                        variant="contained"
                        disabled={
                            !validatePurchaseOrderResult ||
                            !!validatePurchaseOrderResult?.message ||
                            !formData.ontoLocation ||
                            !formData.qty
                        }
                        onClick={() => {
                            setLines(l => [
                                ...l,
                                {
                                    id: l.length + 1,
                                    articleNumber: validatePurchaseOrderResult.partNumber,
                                    transactionType: validatePurchaseOrderResult.transactionType,
                                    dateCreated: new Date().toISOString(),
                                    locationId: formData.ontoLocationId,
                                    quantity: formData.qty,
                                    orderNumber: validatePurchaseOrderResult.orderNumber,
                                    state: validatePurchaseOrderResult.state,
                                    orderLine: validatePurchaseOrderResult.orderLine,
                                    storageType: formData.storageType,
                                    createdBy: userNumber,
                                    location: formData.ontoLocation.toUpperCase()
                                }
                            ]);
                            setFormData(d => ({ ...d, qty: 0, ontoLocation: '' }));
                        }}
                    >
                        Add Line
                    </Button>
                    <Button
                        variant="contained"
                        disabled={
                            (!validatePurchaseOrderResult && !lines.length) ||
                            !formData.ontoLocation ||
                            (!formData.qty && !lines.length)
                        }
                        onClick={() => {
                            setParcelNumber(null);
                            const row = {
                                articleNumber:
                                    validatePurchaseOrderResult?.partNumber ||
                                    validateRsnResult?.articleNumber,
                                transactionType: getTransactionType(),
                                dateCreated: new Date().toISOString(),
                                location: formData.ontoLocation.toUpperCase(),
                                locationId: formData.ontoLocationId,
                                quantity: formData.qty,
                                orderNumber: validatePurchaseOrderResult?.orderNumber,
                                state: validatePurchaseOrderResult?.state,
                                orderLine: validatePurchaseOrderResult?.orderLine,
                                storageType: formData.storageType,
                                createdBy: userNumber
                            };

                            doBookIn({
                                ...formData,
                                multipleBookIn,
                                printRsnLabels,
                                lines: getTransactionType() === 'O' ? [...lines, row] : lines,
                                createdBy: userNumber,
                                transactionType: getTransactionType(),
                                partNumber:
                                    validatePurchaseOrderResult?.partNumber ||
                                    validateRsnResult?.articleNumber,
                                manufacturersPartNumber:
                                    validatePurchaseOrderResult?.manufacturersPartNumber,
                                state: validatePurchaseOrderResult?.state,
                                rsnAccessories: rsnAccessoriesString,
                                condition: rsnConditionsString
                            });
                            setLines([]);
                            setFormData({
                                thisBookIn: 0,
                                dateReceived: new Date(),
                                lines: []
                            });
                            clearPo();
                            clearRsn();
                            setRsnAccessoriesString('');
                            setRsnConditionsString('');
                        }}
                    >
                        Book In
                    </Button>

                    {tab === 2 && (
                        <CheckboxWithLabel
                            label="Print RSN Label?"
                            checked={printRsnLabels}
                            onChange={() => setPrintRsnLabels(m => !m)}
                        />
                    )}
                    {(tab === 0 || tab === 2) && (
                        <Button
                            variant="contained"
                            color="secondary"
                            disabled={selectedRows.length < 1}
                            onClick={() => {
                                setLines(
                                    lines.filter(r => !selectedRows.map(x => x.id).includes(r.id))
                                );
                            }}
                        >
                            Clear Selected
                        </Button>
                    )}
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        fullWidth
                        value={formData.serialNumber}
                        label="Serial"
                        propertyName="serialNumber"
                        onChange={handleFieldChange}
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        fullWidth
                        value={formData.comments}
                        label="Comments"
                        propertyName="comments"
                        onChange={handleFieldChange}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

GoodsInUtility.propTypes = {
    validatePurchaseOrderResult: PropTypes.shape({
        bookInMessage: PropTypes.string,
        orderLine: PropTypes.number,
        state: PropTypes.string,
        orderNumber: PropTypes.number,
        documentType: PropTypes.string,
        orderQty: PropTypes.number,
        qtyBookedIn: PropTypes.number,
        storage: PropTypes.string,
        qcPart: PropTypes.string,
        partNumber: PropTypes.string,
        partDescription: PropTypes.string,
        orderUnitOfMeasure: PropTypes.string,
        manufacturersPartNumber: PropTypes.string,
        transactionType: PropTypes.string,
        message: PropTypes.string
    }),
    validateStorageType: PropTypes.func.isRequired,
    validateStorageTypeResult: PropTypes.shape({
        message: PropTypes.string,
        locationCode: PropTypes.string
    }),
    validateStorageTypeResultLoading: PropTypes.bool,
    validatePurchaseOrderResultLoading: PropTypes.bool,
    validatePurchaseOrder: PropTypes.func.isRequired,
    searchStoragePlaces: PropTypes.func.isRequired,
    storagePlacesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    storagePlacesSearchLoading: PropTypes.bool,
    bookInResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string,
        reqNumber: PropTypes.number,
        qcState: PropTypes.string,
        docType: PropTypes.string,
        unitOfMeasure: PropTypes.string,
        qtyReceived: PropTypes.number,
        qcInfo: PropTypes.string,
        kardexLocation: PropTypes.string,
        parcelComments: PropTypes.string,
        supplierId: PropTypes.number,
        createParcel: PropTypes.bool,
        lines: PropTypes.arrayOf(PropTypes.shape({})),
        printLabels: PropTypes.bool,
        transactionCode: PropTypes.string,
        partNumber: PropTypes.string,
        description: PropTypes.string,
        orderNumber: PropTypes.number
    }),
    bookInResultLoading: PropTypes.bool,
    doBookIn: PropTypes.func.isRequired,
    validatePurchaseOrderBookInQtyResult: PropTypes.shape({
        success: PropTypes.bool,
        message: PropTypes.string
    }),
    validatePurchaseOrderBookInQty: PropTypes.func.isRequired,
    validatePurchaseOrderBookInQtyResultLoading: PropTypes.bool,
    userNumber: PropTypes.number.isRequired,
    match: PropTypes.shape({}).isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loanDetails: PropTypes.arrayOf(PropTypes.shape({})),
    getLoanDetails: PropTypes.func.isRequired,
    loanDetailsLoading: PropTypes.bool,
    rsnConditions: PropTypes.arrayOf(PropTypes.shape({})),
    rsnConditionsLoading: PropTypes.bool,
    getRsnConditions: PropTypes.func.isRequired,
    rsnAccessories: PropTypes.arrayOf(PropTypes.shape({})),
    rsnAccessoriesLoading: PropTypes.bool,
    getRsnAccessories: PropTypes.func.isRequired,
    validateRsn: PropTypes.func.isRequired,
    validateRsnResult: PropTypes.shape({
        message: PropTypes.string,
        success: PropTypes.bool,
        articleNumber: PropTypes.string,
        description: PropTypes.string,
        state: PropTypes.string,
        quantity: PropTypes.number,
        serialNumber: PropTypes.number
    }),
    validateRsnResultLoading: PropTypes.bool,
    clearPo: PropTypes.func.isRequired,
    clearRsn: PropTypes.func.isRequired,
    printRsnResult: PropTypes.shape({ success: PropTypes.bool, message: PropTypes.string }),
    printRsnLoading: PropTypes.bool,
    printRsn: PropTypes.func.isRequired,
    parcel: PropTypes.shape({ parcelNumber: PropTypes.number })
};

GoodsInUtility.defaultProps = {
    bookInResult: null,
    bookInResultLoading: false,
    validatePurchaseOrderResult: null,
    validatePurchaseOrderResultLoading: false,
    validateStorageTypeResult: null,
    validateStorageTypeResultLoading: false,
    storagePlacesSearchResults: [],
    storagePlacesSearchLoading: false,
    validatePurchaseOrderBookInQtyResult: null,
    validatePurchaseOrderBookInQtyResultLoading: false,
    loanDetails: [],
    loanDetailsLoading: false.partNumber,
    rsnConditions: [],
    rsnConditionsLoading: false,
    rsnAccessories: [],
    rsnAccessoriesLoading: false,
    validateRsnResult: null,
    validateRsnResultLoading: false,
    printRsnResult: null,
    printRsnLoading: false,
    parcel: null
};

export default GoodsInUtility;
