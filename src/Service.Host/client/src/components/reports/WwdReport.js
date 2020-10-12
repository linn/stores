import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { ReportTable, Loading, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function WwdReport({ reportData, loading, error, options }) {
    return (
        <Page>
            <Grid container spacing={3}>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                <Grid item xs={12}>
                    <Typography variant="subtitle2">{`What Will Decrement Report - Part Number: ${
                        options.partNumber
                    }, Quantity: ${options.quantity}, Type of Run: ${options.typeOfRun}${
                        options.workStationCode ? options.wortkStationCode : ''
                    }`}</Typography>
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
                            />
                        )
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

WwdReport.propTypes = {
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    error: PropTypes.string,
    options: PropTypes.shape({})
};

WwdReport.defaultProps = {
    reportData: null,
    loading: false,
    error: '',
    options: {}
};
