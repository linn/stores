import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Title, SingleEditTable, Loading, BackButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function StockLocator({ items, itemsLoading, history, options }) {
    const variableColumns = options?.batchRef
        ? [
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
              }
          ]
        : [
              {
                  title: 'Location Code',
                  id: 'locationName',
                  type: 'text',
                  editable: false
              },
              {
                  title: 'UOM',
                  id: 'partUnitOfMeasure',
                  type: 'text',
                  editable: false
              }
          ];

    const columns = [
        {
            title: 'Part',
            id: 'partNumber',
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
        ...variableColumns,
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
        }
    ];
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text={options?.batchRef ? 'Locator Batches' : 'Stock Locations'} />
                </Grid>
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
                                rows={items.map(i => ({
                                    ...i,
                                    id: i.id + i.batchRef + i.partNumber
                                }))}
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
    history: PropTypes.shape({ push: PropTypes.func }).isRequired
};

StockLocator.defaultProps = {
    items: [],
    itemsLoading: true
};

export default StockLocator;
