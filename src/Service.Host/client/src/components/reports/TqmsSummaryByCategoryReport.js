import React from 'react';
import Grid from '@material-ui/core/Grid';
import { MultiReportTable, Loading, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function TqmsSummaryByCategoryReport({ reportData, loading, error }) {
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
                            <MultiReportTable
                                reportData={reportData}
                                showTotals
                                placeholderRows={10}
                                placeholderColumns={3}
                                showRowTitles={false}
                                showTitle
                            />
                        )
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

TqmsSummaryByCategoryReport.propTypes = {
    reportData: PropTypes.arrayOf(
        PropTypes.shape({ title: PropTypes.shape({ displayString: PropTypes.string }) })
    ),
    loading: PropTypes.bool,
    error: PropTypes.string,
    options: PropTypes.shape({
        jobRef: PropTypes.string
    })
};

TqmsSummaryByCategoryReport.defaultProps = {
    reportData: null,
    loading: false,
    error: '',
    options: {}
};
