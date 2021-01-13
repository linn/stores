import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { Loading, Title, utilities } from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function WorkstationTopUpStatus({ item, loading, startTopUpRun, refreshStatus }) {
    const [startHref, setStartHref] = useState(null);

    useEffect(() => {
        setStartHref(utilities.getHref(item, 'start-top-up'));
    }, [item, setStartHref]);

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={1} />
                <Grid item xs={7}>
                    <Title text="Workstation Top Up" />
                </Grid>
                <Grid item xs={2}>
                    <Button onClick={() => refreshStatus()} variant="contained">
                        Refresh Status
                    </Button>
                </Grid>
                <Grid item xs={2}>
                    <Button
                        disabled={!startHref}
                        onClick={() => startTopUpRun({})}
                        color="secondary"
                        variant="contained"
                    >
                        Start New Run
                    </Button>
                </Grid>
                {loading || !item ? (
                    <Loading />
                ) : (
                    <>
                        <Grid item xs={1} />
                        <Grid item xs={11}>
                            <Typography variant="subtitle1">
                                Last Trigger Run : {item.productionTriggerRunJobRef}
                            </Typography>
                        </Grid>

                        <Grid item xs={1} />
                        <Grid item xs={11}>
                            <Typography variant="subtitle1">
                                {item.productionTriggerRunMessage}
                            </Typography>
                        </Grid>
                        <Grid item xs={1} />
                        <Grid item xs={11}>
                            <Typography variant="subtitle1">
                                Last Workstation Run : {item.workstationTopUpJobRef}
                            </Typography>
                        </Grid>
                        <Grid item xs={1} />
                        <Grid item xs={11}>
                            <Typography variant="subtitle1">
                                {item.workstationTopUpMessage}
                            </Typography>
                        </Grid>
                        <Grid item xs={1} />
                        <Grid item xs={11}>
                            <Typography variant="h6">{item.statusMessage}</Typography>
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
    loading: PropTypes.bool,
    startTopUpRun: PropTypes.func.isRequired,
    refreshStatus: PropTypes.func.isRequired
};

WorkstationTopUpStatus.defaultProps = {
    item: null,
    loading: null,
    itemError: null
};

export default WorkstationTopUpStatus;
