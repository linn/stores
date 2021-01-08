import React from 'react';
import Grid from '@material-ui/core/Grid';
import { ReportTable, Loading, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function StoragePlaceAuditReport({ reportData, loading, error }) {
    return (
        <Page>
            <Grid container spacing={3}>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
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

StoragePlaceAuditReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.shape({ displayString: PropTypes.string }) }),
    loading: PropTypes.bool,
    error: PropTypes.string
};

StoragePlaceAuditReport.defaultProps = {
    reportData: null,
    loading: false,
    error: ''
};
