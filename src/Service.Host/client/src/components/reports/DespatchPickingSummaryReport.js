import React from 'react';
import { Loading, ReportTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/styles';
import PropTypes from 'prop-types';

const useStyles = makeStyles(theme => ({
    grid: {
        marginTop: theme.spacing(4),
        paddingLeft: theme.spacing(1),
        paddingRight: theme.spacing(1)
    }
}));

function DespatchPickingSummaryReport({ reportData, loading }) {
    const classes = useStyles();
    return (
        <Grid className={classes.grid} container justify="center">
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
    );
}

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
