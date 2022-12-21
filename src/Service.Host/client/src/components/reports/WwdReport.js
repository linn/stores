import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { Loading, ErrorCard, BackButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import ReportTable from './ReportTable';

export default function WwdReport({ reportData, loading, error, options, history }) {
    return (
        <div className="zoomed-out-printing no-page">
            <Grid container spacing={3}>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                <Grid item xs={10}>
                    <Typography variant="subtitle2">{`What Will Decrement Report - Part Number: ${
                        options.partNumber
                    }, Quantity: ${options.quantity}, Type of Run: ${options.typeOfRun} ${
                        options.workStationCode ? `Workstation: ${options.workStationCode}` : ''
                    }`}</Typography>
                </Grid>
                <Grid item xs={2} className="hide-when-printing">
                    <BackButton
                        backClick={() => history.push('/inventory/reports/what-will-decrement')}
                    />
                </Grid>
                <Grid item xs={12}>
                    {loading ? (
                        <Loading />
                    ) : (
                        reportData && (
                            <ReportTable
                                reportData={reportData}
                                showTotals={false}
                                showTitle
                                title={reportData ? reportData?.title.displayString : 'Loading'}
                                showRowTitles
                            />
                        )
                    )}
                </Grid>
            </Grid>
        </div>
    );
}

WwdReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.shape({ displayString: PropTypes.string }) }),
    loading: PropTypes.bool,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    error: PropTypes.string,
    options: PropTypes.shape({
        partNumber: PropTypes.string,
        quantity: PropTypes.number,
        workStationCode: PropTypes.string,
        typeOfRun: PropTypes.string
    })
};

WwdReport.defaultProps = {
    reportData: null,
    loading: false,
    error: '',
    options: {}
};
