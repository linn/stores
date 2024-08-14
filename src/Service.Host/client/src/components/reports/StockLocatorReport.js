import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { Loading, ErrorCard, ExportButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import ReportTable from './ReportTable';

export default function StockLocatorReport({ reportData, loading, error, getReport, config }) {
    const handleRunClick = () => {
        getReport({ siteCode: 'SUPPLIERS' });
    };

    return (
        <div className="zoomed-out-printing no-page">
            <Grid container spacing={3}>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                <Grid item xs={4}>
                    <Typography variant="subtitle2">Stock At Suppliers Report</Typography>
                </Grid>
                <Grid item xs={2}>
                    <Button color="primary" variant="contained" onClick={handleRunClick}>
                        Run Report
                    </Button>
                </Grid>
                <Grid item xs={2}>
                    <ExportButton
                        href={`${config.appRoot}/inventory/reports/stock-locators-report/export?siteCode=SUPPLIERS`}
                    />
                </Grid>
                <Grid item xs={4} />
                <Grid item xs={8}>
                    {loading ? (
                        <Loading />
                    ) : (
                        reportData && (
                            <ReportTable
                                reportData={reportData}
                                showTotals={false}
                                showTitle
                                showRowCount
                                title={reportData ? reportData?.title.displayString : 'Loading'}
                                showRowTitles={false}
                            />
                        )
                    )}
                </Grid>
                <Grid item xs={4} />
            </Grid>
        </div>
    );
}

StockLocatorReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.shape({ displayString: PropTypes.string }) }),
    loading: PropTypes.bool,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    error: PropTypes.string,
    getReport: PropTypes.func.isRequired,
    config: PropTypes.shape({ appRoot: PropTypes.string }).isRequired
};

StockLocatorReport.defaultProps = {
    reportData: null,
    loading: false,
    error: ''
};
