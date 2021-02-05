import React, { useEffect } from 'react';
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

function DespatchPickingSummaryReport({ reportData, loading, runOptions }) {
    useEffect(() => {
        if (!loading && reportData && runOptions && runOptions.print === 'true') {
            window.print();
        }
    }, [runOptions, loading, reportData]);

    const classes = useStyles();
    const date = new Date().toLocaleString('en-GB', {
        month: 'short',
        year: 'numeric',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });

    return (
        <div className="print-landscape">
            <Grid className={classes.grid} container justify="center">
                <Grid item xs={12}>
                    <span className="date-for-printing">{date}</span>
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
        </div>
    );
}

DespatchPickingSummaryReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.string }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    runOptions: PropTypes.shape({ print: PropTypes.string })
};

DespatchPickingSummaryReport.defaultProps = {
    reportData: {},
    runOptions: { print: 'false' },
    loading: false
};

export default DespatchPickingSummaryReport;
