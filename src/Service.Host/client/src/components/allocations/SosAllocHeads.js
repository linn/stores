import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Divider from '@material-ui/core/Divider';
import SosAllocDetails from './SosAllocDetails';

import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    Dropdown,
    SnackbarMessage,
    utilities,
    DatePicker,
    OnOffSwitch
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function SosAllocHeads({
    editStatus,
    itemError,
    history,
    loading,
    snackbarVisible,
    items,
    setSnackbarVisible
}) {
    const [selectedIndex, setSelectedIndex] = useState(0);

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Allocation" />
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                )}
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        <SnackbarMessage
                            visible={snackbarVisible}
                            onClose={() => setSnackbarVisible(false)}
                            message="Allocation Successful"
                        />
                    </>
                )}
            </Grid>
            <Grid container spacing={3}>
                {!loading && (
                    <>
                    <Grid item xs={2}>
                        <List component="nav">
                                {items.map((item, i) => (<><ListItem
                                    button
                                    selected={selectedIndex === i}
                                    onClick={() => setSelectedIndex(i)} 
                                >
                                    <ListItemText primaryTypographyProps={{ style: {
                                            fontSize: "0.9rem"
                                        }
                                    }} secondary={`Value ${item.valueToAllocate} `} primary={`Account Id ${item.accountId} Outlet ${item.outletNumber}`}  />
                                </ListItem>
                                    <Divider /> </>) )}
                        </List>
                    </Grid>
                        <Grid item xs={10}>
                            <Grid container spacing={3} style={{ paddingTop: "12px" }}>

                                <Grid item xs={6}>
                        here 
                                    <SosAllocDetails index={selectedIndex} />
                        </Grid>
                        <Grid item xs={6}>
                            big list 2
                        </Grid>
                        <Grid item xs={6}>
                            big list 4
                        </Grid>
                        <Grid item xs={6}>
                            big list 4
                        </Grid></Grid>
                    </Grid>
                    </>
                )} 
            </Grid>
        </Page>
    );
}

SosAllocHeads.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    snackbarVisible: PropTypes.bool,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

SosAllocHeads.defaultProps = {
    snackbarVisible: false,
    addItem: null,
    loading: null,
    itemError: null,
    items: []
};

export default SosAllocHeads;
