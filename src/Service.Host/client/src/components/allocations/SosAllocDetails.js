import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Divider from '@material-ui/core/Divider';

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

function SosAllocDetails({
    editStatus,
    itemError,
    history,
    loading,
    snackbarVisible,
    index,
    items,
    setSnackbarVisible
}) {
    const [selectedIndex, setSelectedIndex] = useState(0);

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';

    return (
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text={` Allocation  ${index}` } />
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                )}
                {!loading && (
                    <Grid item xs={12}>
                       ok
                    </Grid>
                )}
            </Grid>
    );
}

SosAllocDetails.propTypes = {
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

SosAllocDetails.defaultProps = {
    snackbarVisible: false,
    addItem: null,
    loading: null,
    itemError: null,
    items: []
};

export default SosAllocDetails;
