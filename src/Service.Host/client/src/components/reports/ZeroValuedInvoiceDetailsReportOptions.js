import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { DatePicker, Title, ExportButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';
import config from '../../config';

function ZeroValuedInvoiceDetailsReportOptions() {
    const defaultStartDate = new Date();
    defaultStartDate.setDate(defaultStartDate.getDate() - 90);
    const [fromDate, setFromDate] = useState(defaultStartDate);
    const [toDate, setToDate] = useState(new Date());

    return (
        <Page>
            <Title text="Zero Valued Items on Invoices" />
            <Grid style={{ marginTop: 40 }} container spacing={3} justifyContent="center">
                <Grid item xs={12}>
                    <Typography variant="h6" gutterBottom>
                        Choose a date range and click export to generate the report:
                    </Typography>
                </Grid>
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
                <Grid item xs={12}>
                    <ExportButton
                        href={`${config.appRoot}/logistics/zero-valued-invoiced-report?from=${fromDate}&to=${toDate}`}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

ZeroValuedInvoiceDetailsReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string,
        iprResults: PropTypes.bool
    }).isRequired
};

export default ZeroValuedInvoiceDetailsReportOptions;
