import React, { useState, useEffect }  from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { Link } from 'react-router-dom';
import { Loading, Title, utilities } from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

const useStyles = makeStyles({
    nounderline: {
        textDecoration: 'none'
    }
});

function WorkstationTopUpStatus({ item, loading }) {
    const classes = useStyles();

    const [startHref, setStartHref] = useState(null);

    useEffect(() => {
        setStartHref(utilities.getHref(item, 'start-top-up'))
    }, [item, setStartHref]);

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={2} />
                <Grid item xs={8}>
                    <Title text="Workstation Top Up" />
                </Grid>
                <Grid item xs={2}>
                    <Link className={classes.nounderline} to={startHref}>
                        <Button disabled={!startHref} color="secondary" variant="contained">
                            Start New Run
                        </Button>
                    </Link>
                </Grid>
                {loading || !item ? (
                    <Loading />
                ) : (
                    <>
                        <Grid item xs={2} />
                        <Grid item xs={10}>
                            <Typography variant="subtitle1">
                                Last Trigger Run : {item.productionTriggerRunJobRef}{' '}
                                {item.productionTriggerRunMessage}
                            </Typography>
                        </Grid>
                        <Grid item xs={2} />
                        <Grid item xs={10}>
                            <Typography variant="subtitle1">
                                Last Workstation Run : {item.workstationTopUpJobRef}{' '}
                                {item.workstationTopUpMessage}
                            </Typography>
                        </Grid>
                        <Grid item xs={2} />
                        <Grid item xs={10}>
                            <Typography variant="h4">{item.statusMessage}</Typography>
                        </Grid>
                    </>
                )}
            </Grid>
        </Page>
    );
}

WorkstationTopUpStatus.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    item: PropTypes.shape({
        productionTriggerRunJobRef: PropTypes.string,
        productionTriggerRunMessage: PropTypes.string,
        workstationTopUpJobRef: PropTypes.string,
        workstationTopUpMessage: PropTypes.string,
        statusMessage: PropTypes.string
    }),
    loading: PropTypes.bool
};

WorkstationTopUpStatus.defaultProps = {
    item: null,
    loading: null,
    itemError: null
};

export default WorkstationTopUpStatus;
