import React from 'react';
import {
    Loading,
    ReportTable,
    BackButton,
    ExportButton
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';

const handleBackClick = (history, options) => {
    const uri = `/logistics/import-books/eu?fromDate=${encodeURIComponent(
        options.fromDate
    )}&toDate=${encodeURIComponent(options.toDate)}`;

    history.push(uri);
};

const ImpbookEuReport = ({ reportData, loading, history, options, config }) => (
    <>
        <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
            <Grid item xs={12}>
                <BackButton backClick={() => handleBackClick(history, options)} />
            </Grid>
            <Grid item xs={12}>
                {!loading && reportData ? (
                    <ExportButton
                        href={`${config.appRoot}/logistics/import-books/eu/report/export?fromDate=${options.fromDate}&toDate=${options.toDate}&euResults=${options.euResults}`}
                    />
                ) : (
                    ''
                )}
            </Grid>
            <Grid item xs={12}>
                {loading || !reportData ? (
                    <Loading />
                ) : (
                    <ReportTable
                        reportData={reportData}
                        title={reportData.title}
                        showTitle
                        showTotals={false}
                        placeholderRows={4}
                        placeholderColumns={4}
                        showRowTitles
                    />
                )}
            </Grid>
            <Grid item xs={12}>
                <BackButton backClick={() => handleBackClick(history, options)} />
            </Grid>
        </Grid>
    </>
);

ImpbookEuReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.string }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({
        fromDate: PropTypes.instanceOf(Date),
        toDate: PropTypes.instanceOf(Date),
        euResults: PropTypes.bool
    }),
    config: PropTypes.shape({ appRoot: PropTypes.string }).isRequired
};

ImpbookEuReport.defaultProps = {
    reportData: {},
    options: {},
    loading: false
};

export default ImpbookEuReport;
