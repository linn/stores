import React from 'react';
import { Loading, ReportTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const DespatchPickingSummaryReport = ({ reportData, loading }) => (
    <Page>
        <Grid container spacing={3} justify="center">
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
                        showRowTitles={false}
                    />
                )}
            </Grid>
        </Grid>
    </Page>
);

DespatchPickingSummaryReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.string }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

DespatchPickingSummaryReport.defaultProps = {
    reportData: {},
    options: {},
    loading: false
};

export default DespatchPickingSummaryReport;
