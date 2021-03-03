import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Grid from '@material-ui/core/Grid';
import { Loading, InputField, DatePicker, Title } from '@linn-it/linn-form-components-library';
import Page from '../containers/Page';
import ExportReturnDetail from './ExportReturnDetail';

export default function ExportReturn({ exportReturnLoading, exportReturn }) {
    const [item, setItem] = useState({});

    useEffect(() => {
        setItem(exportReturn);
    }, [exportReturn]);

    const handleFieldChange = (propertyName, newValue) => {
        setItem(o => ({ ...o, [propertyName]: newValue }));
    };

    return (
        <Page>
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
                                label="Return Number"
                                propertyName="returnId"
                                onChange={handleFieldChange}
                                type="number"
                                disabled
                                margin="dense"
                            />
                        </Grid>
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
                        <Grid item xs={4} />

                        {/* TODO get employee name */}
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={item.raisedBy}
                                label="Raised By"
                                propertyName="raisedBy"
                                onChange={handleFieldChange}
                                disabled
                            />
                        </Grid>
                        <Grid item xs={8} />

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
                                value={item.currency}
                                label="Currency"
                                propertyName="currency"
                                onChange={handleFieldChange}
                                margin="dense"
                            />
                        </Grid>
                        <Grid item xs={4} />

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
                            <DatePicker
                                label="Date Dispatched"
                                value={item.dateDispatched}
                                onChange={value => {
                                    handleFieldChange('dateDispatched', value);
                                }}
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

                        <Grid item xs={12}>
                            <Typography variant="subtitle1" gutterBottom>
                                RSNs
                            </Typography>
                        </Grid>
                        {item.exportReturnDetails?.map(detail => (
                            <Grid item xs={12} key={detail.rsnNumber}>
                                <ExportReturnDetail exportReturnDetail={detail} />
                            </Grid>
                        ))}

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
    exportReturn: PropTypes.shape({})
};

ExportReturn.defaultProps = {
    exportReturnLoading: false,
    exportReturn: {}
};
