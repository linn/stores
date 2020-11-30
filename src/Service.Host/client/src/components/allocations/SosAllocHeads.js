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
    allocationError
}) {
    const [selectedIndex, setSelectedIndex] = useState(0);
    const [selectedDetails, setSelectedDetails] = useState([]);
    const [progress, setProgress] = useState(50);

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
        }
    });

    const doFinishAllocation = () => {
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
                <Grid item xs={8}>
                    <Title text="Allocation" />
                </Grid>
                <Grid item xs={2}>
                    <Button variant="outlined" onClick={doFinishAllocation}>
                        Allocate
                    </Button>
                </Grid>
                {loading && (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                )}
            </Grid>
            <Grid container spacing={3}>
                {!loading && (
                    <>
                        <Grid item xs={2}>
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
                                                secondary={`Value ${item.valueToAllocate} `}
                                                primary={`Account Id ${item.accountId} Outlet ${item.outletNumber}`}
                                            />
                                        </ListItem>
                                        <Divider />
                                    </>
                                ))}
                            </List>
                        </Grid>
                        <Grid item xs={10}>
                            <>
                                {allocationError && (
                                    <ErrorCard errorMessage={`${allocationError}`} />
                                )}
                                <SosAllocDetails
                                    header={items[selectedIndex]}
                                    updateDetail={updateDetail}
                                    loading={detailsLoading}
                                    items={utilities.sortEntityList(selectedDetails, 'orderNumber')}
                                />
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
        PropTypes.shape({ accountId: PropTypes.number, outletNumber: PropTypes.number })
    ),
    details: PropTypes.arrayOf(PropTypes.shape({})),
    updateDetail: PropTypes.func.isRequired,
    finishAllocation: PropTypes.func.isRequired,
    allocationError: PropTypes.string
};

SosAllocHeads.defaultProps = {
    loading: null,
    items: [],
    details: [],
    detailsLoading: null,
    allocationError: null
};

export default SosAllocHeads;
