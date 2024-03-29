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
    const uri = `/logistics/import-books/ipr?fromDate=${encodeURIComponent(
        options.fromDate
    )}&toDate=${encodeURIComponent(options.toDate)}`;

    history.push(uri);
};

const ImpbookIprReport = ({ reportData, loading, history, options, config }) => (
    <>
        <Grid style={{ marginTop: 40 }} container spacing={3} justifyContent="center">
            <Grid item xs={12}>
                <BackButton backClick={() => handleBackClick(history, options)} />

                {!loading && reportData ? (
                    <ExportButton
                        href={`${config.appRoot}/logistics/import-books/ipr/report/export?fromDate=${options.fromDate}&toDate=${options.toDate}&iprResults=${options.iprResults}`}
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

ImpbookIprReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.string }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({
        fromDate: PropTypes.instanceOf(Date),
        toDate: PropTypes.instanceOf(Date),
        iprResults: PropTypes.bool
    }),
    config: PropTypes.shape({ appRoot: PropTypes.string }).isRequired
};

ImpbookIprReport.defaultProps = {
    reportData: {},
    options: {},
    loading: false
};

export default ImpbookIprReport;
