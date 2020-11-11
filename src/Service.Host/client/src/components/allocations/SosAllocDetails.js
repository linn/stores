import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { ErrorCard, EditableTable, SnackbarMessage } from '@linn-it/linn-form-components-library';
import Typography from '@material-ui/core/Typography';

function SosAllocDetails({ itemError, loading, index, items, updateDetail }) {
    const [internalError, setInternalError] = useState(null);
    const updateRow = allocDetail => {
        if (allocDetail.quantityToAllocate > allocDetail.maximumQuantityToAllocate) {
            setInternalError(
                `Cannot set quantity higher than ${allocDetail.maximumQuantityToAllocate}`
            );
        } else {
            setInternalError(null);
            updateDetail(allocDetail.id, allocDetail);
        }
    };

    const columns = [
        {
            title: 'Order No',
            id: 'orderNumber',
            type: 'text',
            editable: false
        },
        {
            title: 'Line',
            id: 'orderLine',
            type: 'text',
            editable: false
        },
        {
            title: 'Article Number',
            id: 'articleNumber',
            type: 'text',
            editable: false,
            required: true
        },
        {
            title: 'Qty To Allocate',
            id: 'quantityToAllocate',
            type: 'text',
            editable: true
        },
        {
            title: 'Allocated',
            id: 'quantityAllocated',
            type: 'text',
            editable: false
        }
    ];

    return (
        <Grid container spacing={3}>
            {!loading && items && items.length > 0 && (
                <>
                    <Grid item xs={12}>
                        <Typography variant="h6" gutterBottom>
                            {`Account ${items[index].accountId} Outlet ${items[index].outletNumber}`}
                        </Typography>
                    </Grid>
                    {itemError && (
                        <Grid item xs={12}>
                            <ErrorCard errorMessage={itemError.statusText} />
                        </Grid>
                    )}

                    <SnackbarMessage
                        visible={internalError}
                        autoHideDuration={50}
                        onClose={() => setInternalError(null)}
                        message={internalError}
                    />
                    <Grid item xs={12}>
                        <EditableTable columns={columns} rows={items} saveRow={updateRow} />
                    </Grid>
                </>
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
    items: PropTypes.arrayOf(
        PropTypes.shape({ accountId: PropTypes.number, outletNumber: PropTypes.number })
    ),
    index: PropTypes.number,
    loading: PropTypes.bool,
    snackbarVisible: PropTypes.bool,
    updateDetail: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

SosAllocDetails.defaultProps = {
    index: 0,
    loading: null,
    itemError: null,
    items: [],
    snackbarVisible: false
};

export default SosAllocDetails;
