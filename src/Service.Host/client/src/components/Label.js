import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { Title } from '@linn-it/linn-form-components-library';
import Page from '../containers/Page';

function Label({ text }) {
    return (
        <div style={{ display: 'none' }} className="show-only-when-printing">
            <Page>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <Title text="Label" />
                        <Title text={text} />
                    </Grid>
                </Grid>
            </Page>
        </div>
    );
}

Label.propTypes = {
    text: PropTypes.string.isRequired
};

Label.defaultProps = {};

export default Label;
