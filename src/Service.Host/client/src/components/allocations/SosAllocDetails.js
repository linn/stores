import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import {
    Loading,
    ErrorCard,
    EditableTable,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Typography from '@material-ui/core/Typography';

function SosAllocDetails({
    itemError,
    loading,
    items,
    updateDetail,
    header,
    displayOnly,
    pickItemsAllocation,
    pickItemsAllocationWorking,
    unpickItemsAllocation,
    unpickItemsAllocationWorking
}) {
    const [internalError, setInternalError] = useState(null);

    const editableColumns = [
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
        }
    ];

    const displayOnlyColumns = editableColumns.concat([
        {
            title: 'Allocated',
            id: 'quantityAllocated',
            type: 'text',
            editable: false
        },
        {
            title: 'Success',
            id: 'allocationSuccessful',
            type: 'text',
            editable: false
        },
        {
            title: 'Message',
            id: 'allocationMessage',
            type: 'text',
            editable: false
        }
    ]);

    const updateRow = allocDetail => {
        if (allocDetail.quantityToAllocate > allocDetail.maximumQuantityToAllocate) {
            setInternalError(
                `Cannot set quantity higher than ${allocDetail.maximumQuantityToAllocate}`
            );
        } else if (allocDetail.quantityToAllocate < 0) {
            setInternalError('Cannot set quantity to be less than 0');
        } else {
            setInternalError(null);
            updateDetail(allocDetail.id, allocDetail);
        }
    };

    const pickAll = () => {
        pickItemsAllocation({
            jobId: header.jobId,
            accountId: header.accountId,
            outletNumber: header.outletNumber
        });
    };

    const unpickAll = () => {
        unpickItemsAllocation({
            jobId: header.jobId,
            accountId: header.accountId,
            outletNumber: header.outletNumber
        });
    };

    return (
        <Grid container spacing={3}>
            {loading || pickItemsAllocationWorking || unpickItemsAllocationWorking ? (
                <Loading />
            ) : (
                <>
                    <Grid item xs={8}>
                        <Typography variant="h6">{header.outletName}</Typography>
                        <Typography variant="subtitle1">
                            {`Account Id: ${header.accountId} Outlet: ${
                                header.outletNumber
                            } Value To Allocate: ${Math.round(header.valueToAllocate, 0)} `}
                        </Typography>
                        <Typography variant="subtitle1">
                            {`Country: ${header.countryName} (${header.countryCode}) `}
                        </Typography>
                    </Grid>
                    <Grid item xs={4}>
                        <>
                            <Button onClick={() => pickAll()}>Pick All</Button>
                            <Button onClick={() => unpickAll()}>Unpick All</Button>
                        </>
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
                        <EditableTable
                            columns={displayOnly ? displayOnlyColumns : editableColumns}
                            rows={items}
                            saveRow={updateRow}
                            editable={!displayOnly}
                            allowNewRowCreation={false}
                        />
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
        PropTypes.shape({
            accountId: PropTypes.number,
            outletNumber: PropTypes.number
        })
    ),
    pickItemsAllocation: PropTypes.func.isRequired,
    pickItemsAllocationWorking: PropTypes.func.isRequired,
    unpickItemsAllocation: PropTypes.func.isRequired,
    unpickItemsAllocationWorking: PropTypes.func.isRequired,
    displayOnly: PropTypes.bool,
    loading: PropTypes.bool,
    updateDetail: PropTypes.func.isRequired,
    header: PropTypes.shape({
        jobId: PropTypes.number,
        accountId: PropTypes.number,
        outletNumber: PropTypes.number,
        outletName: PropTypes.string,
        valueToAllocate: PropTypes.number,
        countryCode: PropTypes.string,
        countryName: PropTypes.string
    })
};

SosAllocDetails.defaultProps = {
    loading: null,
    itemError: null,
    items: [],
    header: {},
    displayOnly: false
};

export default SosAllocDetails;
