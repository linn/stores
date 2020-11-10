import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    Title,
    ErrorCard
} from '@linn-it/linn-form-components-library';

function SosAllocDetails({
    itemError,
    loading,
    index,
    items
}) {
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
                {!loading && items && (
                    <Grid item xs={12}>
                       {items.map(i => (<span>{i.id}</span>)) }
                    </Grid>
                )}
            </Grid>
    );
}

SosAllocDetails.propTypes = {
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    items: PropTypes.shape({}),
    index: PropTypes.number,
    loading: PropTypes.bool
};

SosAllocDetails.defaultProps = {
    index: 0,
    loading: null,
    itemError: null,
    items: []
};

export default SosAllocDetails;
