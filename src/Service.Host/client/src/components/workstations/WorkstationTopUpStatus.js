import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    Dropdown,
    utilities,
    DatePicker,
    OnOffSwitch
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

const useStyles = makeStyles({
    runButton: {
        float: 'right',
        width: '100%'
    }
});

function WorkstationTopUpStatus({ item, itemError, loading }) {
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid>
                    <span>hello</span>
                </Grid>
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
    item: PropTypes.shape({}),
    loading: PropTypes.bool
};

WorkstationTopUpStatus.defaultProps = {
    item: {},
    loading: null,
    itemError: null
};

export default WorkstationTopUpStatus;
