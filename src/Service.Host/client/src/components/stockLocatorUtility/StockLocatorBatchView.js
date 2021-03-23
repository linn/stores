import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Title, SingleEditTable, Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import queryString from 'query-string';
import Page from '../../containers/Page';

function StockLocatorBatchView({ items, itemsLoading, history, drillBackPath }) {
    // think: can drillBackPath End Up undefined?
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
            title: 'Batch Ref',
            id: 'batchRef',
            type: 'text',
            editable: false
        },
        {
            title: 'Batch Date',
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
                    <Title text="Locator Batches" />
                </Grid>
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
                                        <span>
                                            {' '}
                                            <button
                                                type="button"
                                                onClick={() => {
                                                    history.push(
                                                        `/inventory/stock-locator/locators/batches/details?${queryString.stringify(
                                                            {
                                                                partNumber: i.partNumber,
                                                                locationName: i.locationName,
                                                                palletNumber: i.palletNumber?.toString(),
                                                                state: i.state,
                                                                category: i.category?.toString(),
                                                                stockPool: i.stockPoolCode,
                                                                batchRef: i.batchRef,
                                                                stockRotationDate:
                                                                    i.stockRotationDate
                                                            }
                                                        )}`
                                                    );
                                                }}
                                            >
                                                +
                                            </button>
                                            <button
                                                type="button"
                                                disabled={!drillBackPath}
                                                onClick={() => {
                                                    history.push(
                                                        drillBackPath.path + drillBackPath.search
                                                    );
                                                }}
                                            >
                                                -
                                            </button>
                                        </span>
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

StockLocatorBatchView.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number])
        })
    ),
    options: PropTypes.shape({
        batchRef: PropTypes.string
    }).isRequired,
    itemsLoading: PropTypes.bool,
    history: PropTypes.shape({ goBack: PropTypes.func, push: PropTypes.func }).isRequired,
    drillBackPath: PropTypes.shape({ path: PropTypes.string, search: PropTypes.string })
};

StockLocatorBatchView.defaultProps = {
    items: [],
    itemsLoading: true,
    drillBackPath: null
};

export default StockLocatorBatchView;
