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
import Page from '../containers/Page';

const useStyles = makeStyles({
    runButton: {
        float: 'right',
        width: '100%'
    }
});

function Wand({}) {
    const classes = useStyles();

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Wand" />
                </Grid>
            </Grid>
        </Page>
    );
}

Wand.propTypes = {};

Wand.defaultProps = {};

export default Wand;
