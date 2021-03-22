import React from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Title,
    SingleEditTable,
    Loading,
    BackButton,
    smartGoBack
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function StockLocator({ items, itemsLoading, history, previousPaths }) {
    const columns = [
        {
            title: 'Part',
            id: 'partNumber',
            type: 'text',
            editable: false
        },
        {
            title: '',
            id: 'drillDownButton',
            type: 'component',
            editable: false
        },
        {
            title: 'Qty At Location',
            id: 'quantity',
            type: 'number',
            editable: false
        },
        {
            title: 'Qty Allocated',
            id: 'quantityAllocated',
            type: 'number',
            editable: false
        },
        {
            title: 'Prices Ref',
            id: 'batchRef',
            type: 'text',
            editable: false
        },
        {
            title: 'Prices Date',
            id: 'stockRotationDate',
            type: 'date',
            editable: false
        },
        {
            title: 'State',
            id: 'state',
            type: 'text',
            editable: false
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
            title: 'Location Code',
            id: 'locationName',
            type: 'text',
            editable: false
        }
    ];
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Locator Priceses" />
                </Grid>
                <Grid item xs={3}>
                    <BackButton backClick={() => smartGoBack(previousPaths, history.goBack)} />
                </Grid>
                <Grid item xs={9} />
                {itemsLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        {items && (
                            <SingleEditTable
                                newRowPosition="top"
                                columns={columns}
                                rows={items.map(i => ({
                                    ...i,
                                    id: i.id + i.batchRef + i.partNumber,
                                    drillDownButton: (
                                        <button
                                            type="button"
                                            onClick={() => {
                                                console.log('ok');
                                            }}
                                        >
                                            +
                                        </button>
                                    )
                                }))}
                                allowNewRowCreation={false}
                                editable={false}
                                allowNewRowCreations
                            />
                        )}
                    </>
                )}
            </Grid>
        </Page>
    );
}

StockLocator.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number])
        })
    ),
    options: PropTypes.shape({
        batchRef: PropTypes.string
    }).isRequired,
    itemsLoading: PropTypes.bool,
    history: PropTypes.shape({ goBack: PropTypes.func }).isRequired,
    previousPaths: PropTypes.arrayOf(PropTypes.string)
};

StockLocator.defaultProps = {
    items: [],
    itemsLoading: true,
    previousPaths: []
};

export default StockLocator;
