import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import { InputField, DatePicker } from '@linn-it/linn-form-components-library';

export default function ExportReturnDetail({ exportReturnDetail }) {
    const [item, setItem] = useState({});

    useEffect(() => {
        setItem(exportReturnDetail);
    }, [exportReturnDetail]);

    const handleFieldChange = (propertyName, newValue) => {
        setItem(o => ({ ...o, [propertyName]: newValue }));
    };

    return (
        <Paper style={{ padding: 12 }}>
            <Grid container spacing={3}>
                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.rsnNumber}
                        label="RSN Number"
                        propertyName="detail.rsnNumber"
                        onChange={handleFieldChange}
                        type="number"
                        disabled
                        margin="dense"
                    />
                </Grid>
                <Grid item xs={8} />

                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.articleNumber}
                        label="Article Number"
                        propertyName="detail.articleNumber"
                        onChange={handleFieldChange}
                        disabled
                        margin="dense"
                    />
                </Grid>
                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.description}
                        label="Description"
                        propertyName="detail.description"
                        onChange={handleFieldChange}
                        disabled
                        margin="dense"
                    />
                </Grid>
                <Grid item xs={4} />

                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.quantity}
                        label="Quantity"
                        propertyName="detail.quantity"
                        onChange={handleFieldChange}
                        margin="dense"
                    />
                </Grid>
                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.lineNo}
                        label="Line Number"
                        propertyName="detail.lineNo"
                        onChange={handleFieldChange}
                        disabled
                        margin="dense"
                    />
                </Grid>
                <Grid item xs={4} />

                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.customsValue}
                        label="Customs Value"
                        propertyName="detail.customsValue"
                        onChange={handleFieldChange}
                        margin="dense"
                        type="number"
                    />
                </Grid>
                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.baseCustomsValue}
                        label="Base Customs Value"
                        propertyName="detail.baseCustomsValue"
                        onChange={handleFieldChange}
                        margin="dense"
                        type="number"
                    />
                </Grid>
                <Grid item xs={4} />

                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.tariffId}
                        label="Tariff ID"
                        propertyName="detail.tariffId"
                        onChange={handleFieldChange}
                        margin="dense"
                        type="number"
                    />
                </Grid>
                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.baseCustomsValue}
                        label="Base Customs Value"
                        propertyName="detail.baseCustomsValue"
                        onChange={handleFieldChange}
                        margin="dense"
                        type="number"
                    />
                </Grid>
                <Grid item xs={4} />

                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.expinvDocumentType}
                        label="Export Invoice Document Type"
                        propertyName="detail.expinvDocumentType"
                        onChange={handleFieldChange}
                        margin="dense"
                        type="number"
                    />
                </Grid>
                <Grid item xs={4}>
                    <InputField
                        fullWidth
                        value={item.expinvDocumentNumber}
                        label="Export Invoice Document Number"
                        propertyName="detail.expinvDocumentNumber"
                        onChange={handleFieldChange}
                        margin="dense"
                        type="number"
                    />
                </Grid>
                <Grid item xs={4} />

                <Grid item xs={4}>
                    <DatePicker
                        label="Export Invoice Date"
                        value={item.expinvDate}
                        onChange={value => {
                            handleFieldChange('expinvDate', value);
                        }}
                    />
                </Grid>
                <Grid item xs={8} />
            </Grid>
        </Paper>
    );
}

ExportReturnDetail.propTypes = {
    exportReturnDetail: PropTypes.shape({})
};

ExportReturnDetail.defaultProps = {
    exportReturnDetail: {}
};
