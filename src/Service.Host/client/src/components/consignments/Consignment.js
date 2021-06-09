import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function Consignment({ consignment, loading, requestErrors }) {
    return (
        <Page requestErrors={requestErrors} showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <span>Consignment</span>
                </Grid>
                {loading ? (
                    <Loading />
                ) : (
                    <Grid item xs={12}>
                        ok {consignment ? consignment.consignmentId : 'none'}
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

Consignment.propTypes = {
    consignment: PropTypes.shape({
        consignmentId: PropTypes.number
    }),
    loading: PropTypes.bool,
    requestErrors: PropTypes.arrayOf(
        PropTypes.shape({ message: PropTypes.string, name: PropTypes.string })
    )
};

Consignment.defaultProps = {
    consignment: null,
    loading: false,
    requestErrors: null
};

export default Consignment;
