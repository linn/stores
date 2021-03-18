import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Button from '@material-ui/core/Button';
import KeyboardArrowDown from '@material-ui/icons/KeyboardArrowDown';
import KeyboardArrowUp from '@material-ui/icons/KeyboardArrowUp';
import Divider from '@material-ui/core/Divider';
import LinearProgress from '@material-ui/core/LinearProgress';
import { makeStyles } from '@material-ui/core/styles';
import { Loading, Title, utilities, ErrorCard } from '@linn-it/linn-form-components-library';
import { Link } from 'react-router-dom';
import Typography from '@material-ui/core/Typography';
import Tooltip from '@material-ui/core/Tooltip';
import SosAllocDetails from './SosAllocDetails';

import Page from '../../containers/Page';

function SosAllocHeads({
    jobId,
    loading,
    items,
    details,
    detailsLoading,
    updateDetail,
    finishAllocation,
    allocationError,
    finishAllocationWorking,
    initialise,
    pickItemsAllocation,
    pickItemsAllocationWorking,
    unpickItemsAllocation,
    unpickItemsAllocationWorking,
    clearAllocationError
}) {
    const [selectedIndex, setSelectedIndex] = useState(0);
    const [selectedDetails, setSelectedDetails] = useState([]);
    const [progress, setProgress] = useState(50);
    const [alloctionHasFinished, setAllocationHasFinished] = useState(false);
    const [allocationHasStarted, setAllocationHasStarted] = useState(false);

    useEffect(() => {
        if (items.length > 0 && details.length > 0) {
            setSelectedDetails(
                details.filter(
                    d =>
                        d.accountId === items[selectedIndex].accountId &&
                        d.outletNumber === items[selectedIndex].outletNumber
                )
            );
        }
    }, [selectedIndex, items, details, setSelectedDetails]);

    useEffect(() => {
        setAllocationHasFinished(
            details.length > 0 && details.some(detail => detail.allocationSuccessful)
        );
    }, [details, items, jobId, setAllocationHasFinished]);

    useEffect(() => {
        if (allocationHasStarted && !finishAllocationWorking) {
            setAllocationHasStarted(false);
            initialise({ jobId });
        } else {
            setAllocationHasStarted(finishAllocationWorking);
        }
    }, [finishAllocationWorking, jobId, setAllocationHasStarted, allocationHasStarted, initialise]);

    useEffect(() => {
        if (selectedIndex === 0) {
            setProgress(0);
        } else if (selectedIndex === items.length - 1) {
            setProgress(100);
        } else {
            setProgress((selectedIndex / (items.length - 1)) * 100);
        }
    }, [setProgress, selectedIndex, items]);

    const useStyles = makeStyles({
        progress: {
            top: '50%'
        },
        nounderline: {
            textDecoration: 'none',
            paddingRight: 10
        }
    });

    const doFinishAllocation = () => {
        clearAllocationError();
        finishAllocation({ jobId });
    };

    const classes = useStyles();

    const nextOutlet = () => {
        if (selectedIndex < items.length - 1) {
            setSelectedIndex(selectedIndex + 1);
        }
    };

    const previousOutlet = () => {
        if (selectedIndex > 0) {
            setSelectedIndex(selectedIndex - 1);
        }
    };
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={2} />
                <Grid item xs={3}>
                    <Title text="Allocation" />
                </Grid>
                <Grid item xs={5}>
                    <Link
                        className={classes.nounderline}
                        to="/logistics/allocations/despatch-picking-summary?print=true"
                    >
                        <Tooltip title="Print Despatch Picking Summary">
                            <Button variant="outlined">Run DPS</Button>
                        </Tooltip>
                    </Link>
                    <Link
                        className={classes.nounderline}
                        to="/logistics/allocations/despatch-pallet-queue"
                    >
                        <Tooltip title="Despatch Pallet Queue (Upper)">
                            <Button variant="outlined">Pallet Queue</Button>
                        </Tooltip>
                    </Link>
                    <Link className={classes.nounderline} to="/logistics/allocations">
                        <Tooltip title="Return To Allocation Options Page">
                            <Button variant="outlined">Start New Run</Button>
                        </Tooltip>
                    </Link>
                </Grid>
                <Grid item xs={2}>
                    <Tooltip title="Allocate selected items for despatch">
                        <Button
                            color="secondary"
                            variant="contained"
                            onClick={doFinishAllocation}
                            disabled={alloctionHasFinished}
                        >
                            Allocate
                        </Button>
                    </Tooltip>
                </Grid>
                <Grid item xs={12}>
                    {allocationError && <ErrorCard errorMessage={`${allocationError}`} />}
                </Grid>
                {(loading || finishAllocationWorking) && (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                )}
            </Grid>
            <Grid container spacing={3}>
                {!loading && !finishAllocationWorking && (
                    <>
                        <Grid item xs={2} style={{ paddingRight: '20px' }}>
                            <Grid container>
                                <Grid item xs={12}>
                                    <Button
                                        onClick={previousOutlet}
                                        disabled={selectedIndex === 0}
                                        startIcon={<KeyboardArrowUp />}
                                    >
                                        Previous
                                    </Button>
                                </Grid>
                                <Grid item xs={12}>
                                    <LinearProgress
                                        variant="determinate"
                                        value={progress}
                                        className={classes.progress}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <Button
                                        onClick={nextOutlet}
                                        disabled={selectedIndex === items.length - 1}
                                        startIcon={<KeyboardArrowDown />}
                                    >
                                        Next
                                    </Button>
                                </Grid>
                            </Grid>
                            <List component="nav" style={{ paddingTop: '20px' }}>
                                <Divider />
                                {items.map((item, i) => (
                                    <>
                                        <ListItem
                                            button
                                            selected={selectedIndex === i}
                                            onClick={() => setSelectedIndex(i)}
                                        >
                                            <ListItemText
                                                primaryTypographyProps={{
                                                    style: {
                                                        fontSize: '0.9rem'
                                                    }
                                                }}
                                                primary={item.outletName}
                                            />
                                        </ListItem>
                                        <Divider />
                                    </>
                                ))}
                            </List>
                        </Grid>
                        <Grid item xs={10}>
                            <>
                                {items.length ? (
                                    <SosAllocDetails
                                        header={items[selectedIndex]}
                                        updateDetail={updateDetail}
                                        loading={detailsLoading}
                                        displayOnly={alloctionHasFinished}
                                        items={utilities.sortEntityList(
                                            selectedDetails,
                                            'orderNumber'
                                        )}
                                        pickItemsAllocation={pickItemsAllocation}
                                        pickItemsAllocationWorking={pickItemsAllocationWorking}
                                        unpickItemsAllocation={unpickItemsAllocation}
                                        unpickItemsAllocationWorking={unpickItemsAllocationWorking}
                                    />
                                ) : (
                                    <Typography variant="h6" gutterBottom>
                                        There were no items found to be allocated.
                                    </Typography>
                                )}
                            </>
                        </Grid>
                    </>
                )}
            </Grid>
        </Page>
    );
}

SosAllocHeads.propTypes = {
    jobId: PropTypes.number.isRequired,
    loading: PropTypes.bool,
    detailsLoading: PropTypes.bool,
    items: PropTypes.arrayOf(
        PropTypes.shape({
            accountId: PropTypes.number,
            outletNumber: PropTypes.number,
            outletName: PropTypes.string
        })
    ),
    details: PropTypes.arrayOf(PropTypes.shape({})),
    updateDetail: PropTypes.func.isRequired,
    finishAllocation: PropTypes.func.isRequired,
    allocationError: PropTypes.string,
    finishAllocationWorking: PropTypes.bool,
    initialise: PropTypes.func.isRequired,
    pickItemsAllocation: PropTypes.func.isRequired,
    pickItemsAllocationWorking: PropTypes.bool,
    unpickItemsAllocation: PropTypes.func.isRequired,
    unpickItemsAllocationWorking: PropTypes.bool,
    clearAllocationError: PropTypes.func.isRequired
};

SosAllocHeads.defaultProps = {
    loading: null,
    items: [],
    details: [],
    detailsLoading: null,
    allocationError: null,
    finishAllocationWorking: false,
    pickItemsAllocationWorking: false,
    unpickItemsAllocationWorking: false
};

export default SosAllocHeads;
