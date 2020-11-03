import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
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
