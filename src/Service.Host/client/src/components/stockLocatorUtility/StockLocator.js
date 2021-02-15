import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Title,
    SingleEditTable,
    Loading,
    SnackbarMessage,
    ErrorCard,
    BackButton
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function StockLocator({ items, itemsLoading, history }) {
    const columns = [
        {
            title: 'Part',
            id: 'partNumber',
            type: 'text',
            editable: false
        },
        {
            title: 'UOM',
            id: 'partUnitOfMeasure',
            type: 'text',
            editable: false
        },
        {
            title: 'Qty At Location',
            id: 'quantity',
            type: 'number',
            editable: false
        },
        {
            title: '',
            id: 'button',
            type: 'text',
            editable: false
        },
        {
            title: 'Qty Allocated',
            id: 'quantityAllocated',
            type: 'number',
            editable: false
        },
        {
            title: 'State',
            id: 'state',
            type: 'text',
            editable: false
        },
        {
            title: 'Qty To Pick',
            id: 'quantityToPick',
            type: 'number',
            editable: false // true?
        },
        {
            title: 'Stock Pool',
            id: 'stockPoolCode',
            type: 'text',
            editable: false
        },
        {
            title: 'Pallet',
            id: 'palletNumber',
            type: 'text',
            editable: false
        },
        {
            title: 'Location',
            id: 'locationName',
            type: 'text',
            editable: false
        },
        {
            title: 'Trigger Level',
            id: 'triggerLevel',
            type: 'number',
            editable: false // true?
        },
        {
            title: 'Max Capacity',
            id: 'maxCapacity',
            type: 'number',
            editable: false // true?
        }
    ];
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Stock Locations" />
                </Grid>
                {/* {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard
                            errorMessage={itemError?.details?.errors?.[0] || itemError.statusText}
                        />
                    </Grid>
                )} */}
                {itemsLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <Grid item xs={12}>
                        {items && (
                            <SingleEditTable
                                newRowPosition="top"
                                columns={columns}
                                rows={items}
                                allowNewRowCreation={false}
                                editable
                                allowNewRowCreations
                            />
                        )}
                    </Grid>
                )}
                <Grid item xs={12}>
                    <BackButton backClick={() => history.push('/inventory/stock-viewer')} />
                </Grid>
            </Grid>
        </Page>
    );
}

export default StockLocator;
