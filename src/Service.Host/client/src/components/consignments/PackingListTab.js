import React from 'react';
import { Loading } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';

function PackingListTab({ consignmentPackingList, consignmentPackingListLoading }) {
    return (
        <>
            <Grid container spacing={3} style={{ paddingTop: '30px' }}>
                {consignmentPackingListLoading || !consignmentPackingList ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <Grid item xs={12}>
                        <span>heeloo</span>
                        <span>{consignmentPackingList.consignmentId}</span>
                    </Grid>
                )}
            </Grid>
        </>
    );
}

PackingListTab.propTypes = {
    consignmentPackingList: PropTypes.arrayOf(PropTypes.shape({})),
    consignmentPackingListLoading: PropTypes.bool
};

PackingListTab.defaultProps = {
    consignmentPackingList: null,
    consignmentPackingListLoading: false
};

export default PackingListTab;
