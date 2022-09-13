import React, { useState } from 'react';
import {
    Loading,
    ReportTable,
    ExportButton,
    DatePicker,
    Title
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Page from '../../containers/Page';

function EuCreditInvoicesReport({ reportData, loading, config, getReport }) {
    const defaultStartDate = new Date();
    defaultStartDate.setDate(defaultStartDate.getDate() - 90);
    const [fromDate, setFromDate] = useState(defaultStartDate);
    const [toDate, setToDate] = useState(new Date());

    return (
        <Page>
            <Grid style={{ marginTop: 40 }} container spacing={3} justifyContent="center">
                <Grid item xs={12}>
                    <Title text="EU Credit Invoices" />
                </Grid>
                <Grid item xs={12}>
                    <ExportButton
                        href={`${
                            config.appRoot
                        }/logistics/reports/eu-credit-invoices/export?from=${fromDate.toISOString()}&to=${toDate.toISOString()}`}
                    />
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
                <Grid item xs={6} />
                <Grid item xs={12}>
                    <Button
                        variant="contained"
                        onClick={() =>
                            getReport({
                                from: fromDate.toISOString(),
                                to: toDate.toISOString()
                            })
                        }
                    >
                        RUN
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    {loading && <Loading />}

                    {reportData && (
                        <ReportTable
                            reportData={reportData}
                            title={reportData.title}
                            showTitle
                            showTotals={false}
                            placeholderRows={4}
                            placeholderColumns={4}
                            showRowTitles={false}
                        />
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

EuCreditInvoicesReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.string }),
    loading: PropTypes.bool,
    getReport: PropTypes.func.isRequired,
    config: PropTypes.shape({ appRoot: PropTypes.string }).isRequired
};

EuCreditInvoicesReport.defaultProps = {
    reportData: {},
    loading: false
};

export default EuCreditInvoicesReport;
