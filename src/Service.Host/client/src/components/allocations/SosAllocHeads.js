import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Divider from '@material-ui/core/Divider';
import { Loading, Title, ErrorCard, utilities } from '@linn-it/linn-form-components-library';
import SosAllocDetails from './SosAllocDetails';

import Page from '../../containers/Page';

function SosAllocHeads({ itemError, loading, items, details, detailsLoading, updateDetail }) {
    const [selectedIndex, setSelectedIndex] = useState(0);
    const [selectedDetails, setSelectedDetails] = useState([]);

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

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={2} />
                <Grid item xs={10}>
                    <Title text="Allocation" />
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                )}
                {loading && (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                )}
            </Grid>
            <Grid container spacing={3}>
                {!loading && !detailsLoading && (
                    <>
                        <Grid item xs={2}>
                            <List component="nav" style={{ paddingTop: '76px' }}>
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
                            <SosAllocDetails
                                index={selectedIndex}
                                updateDetail={updateDetail}
                                items={utilities.sortEntityList(selectedDetails, 'orderNumber')}
                            />
                        </Grid>
                    </>
                )}
            </Grid>
        </Page>
    );
}

SosAllocHeads.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    loading: PropTypes.bool,
    detailsLoading: PropTypes.bool,
    items: PropTypes.arrayOf(
        PropTypes.shape({ accountId: PropTypes.number, outletNumber: PropTypes.number })
    ),
    details: PropTypes.arrayOf(PropTypes.shape({})),
    updateDetail: PropTypes.func.isRequired
};

SosAllocHeads.defaultProps = {
    loading: null,
    itemError: null,
    items: [],
    details: [],
    detailsLoading: null
};

export default SosAllocHeads;
