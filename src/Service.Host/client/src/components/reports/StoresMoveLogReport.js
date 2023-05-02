import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import {
    Loading,
    ErrorCard,
    BackButton,
    ExportButton,
    ReportTable
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import moment from 'moment';

export default function StoresMoveLogReport({
    reportData,
    loading,
    error,
    options,
    history,
    config
}) {
    return (
        <div className="zoomed-out-printing no-page">
            <Grid container spacing={3}>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                <Grid item xs={8}>
                    <Typography variant="subtitle2">{`Stores Move Log - Part Number: ${
                        options.partNumber
                    } from ${moment(options.from).format('DD MMM YY')} to ${moment(
                        options.to
                    ).format('DD MMM YY')}`}</Typography>
                </Grid>
                <Grid item xs={2} className="hide-when-printing">
                    <ExportButton
                        href={`${config.appRoot}/inventory/reports/stores-move-log/export?partNumber=${options.partNumber}&from=${options.from}&to=${options.to}`}
                    />
                </Grid>
                <Grid item xs={2} className="hide-when-printing">
                    <BackButton
                        backClick={() => history.push('/inventory/reports/stores-move-log')}
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
                                showRowTitles={false}
                            />
                        )
                    )}
                </Grid>
            </Grid>
        </div>
    );
}

StoresMoveLogReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.shape({ displayString: PropTypes.string }) }),
    loading: PropTypes.bool,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    error: PropTypes.string,
    options: PropTypes.shape({
        partNumber: PropTypes.string,
        from: PropTypes.string,
        to: PropTypes.string
    }),
    config: PropTypes.shape({ appRoot: PropTypes.string }).isRequired
};

StoresMoveLogReport.defaultProps = {
    reportData: null,
    loading: false,
    error: '',
    options: {}
};
