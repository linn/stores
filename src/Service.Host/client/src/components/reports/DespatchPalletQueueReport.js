import React from 'react';
import { Loading, Title } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/styles';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';

const useStyles = makeStyles(theme => ({
    grid: {
        marginTop: theme.spacing(4),
        paddingLeft: theme.spacing(1),
        paddingRight: theme.spacing(1)
    }
}));

function DespatchPalletQueueReport({ reportData, loading }) {
    const classes = useStyles();
    const date = new Date().toLocaleString('en-GB', {
        month: 'short',
        year: 'numeric',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });

    return (
        <Grid className={classes.grid} container justify="center">
            <Grid item xs={12}>
                <Typography variant="h5" gutterBottom>
                    Despatch Pallet Queue With Warehouse Information
                </Typography>
                <span className="date-for-printing">{date}</span>
            </Grid>
            <Grid item xs={12}>
                {loading || !reportData ? <Loading /> : <span>running</span>}
            </Grid>
        </Grid>
    );
}

DespatchPalletQueueReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.string }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool
};

DespatchPalletQueueReport.defaultProps = {
    reportData: {},
    loading: false
};

export default DespatchPalletQueueReport;
