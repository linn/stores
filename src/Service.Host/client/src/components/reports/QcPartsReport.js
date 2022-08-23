import React from 'react';
import { Loading, ReportTable, ExportButton } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const QcPartsReport = ({ reportData, loading, config }) => (
    <Page>
        <Grid style={{ marginTop: 40 }} container spacing={3} justifyContent="center">
            <Grid item xs={12}>
                {!loading && reportData && (
                    <ExportButton href={`${config.appRoot}/inventory/reports/qc-parts/export`} />
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
                        showRowTitles={false}
                    />
                )}
            </Grid>
        </Grid>
    </Page>
);

QcPartsReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.string }),
    loading: PropTypes.bool,
    config: PropTypes.shape({ appRoot: PropTypes.string }).isRequired
};

QcPartsReport.defaultProps = {
    reportData: {},
    loading: false
};

export default QcPartsReport;
