import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Title, SingleEditTable, Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function StockLocator({ items, itemsLoading, drillBackPath, history }) {
    const columns = [
        {
            title: 'Part',
            id: 'partNumber',
            type: 'text',
            editable: false
        },
        {
            title: '',
            id: 'drillBackButton',
            type: 'component',
            editable: false
        },
        {
            title: 'Qty At Location',
            id: 'quantityAtLocation',
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
            title: 'Remarks',
            id: 'remarks',
            type: 'text',
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
            id: 'stockPool',
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
            id: 'locationCode',
            type: 'text',
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
            id: 'batchDate',
            type: 'date',
            editable: false
        },
        {
            title: 'BudgetId',
            id: 'budgetId',
            type: 'text',
            editable: false
        },
        {
            title: 'Part',
            id: 'partPrice',
            type: 'number',
            editable: false
        },
        {
            title: 'Material',
            id: 'materialPrice',
            type: 'number',
            editable: false
        },
        {
            title: 'Labour',
            id: 'labourPrice',
            type: 'number',
            editable: false
        },
        {
            title: 'Overhead',
            id: 'overheadPrice',
            type: 'number',
            editable: false
        }
    ];
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Locator Prices" />
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
                                    drillBackButton: (
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
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    drillBackPath: PropTypes.shape({ path: PropTypes.string, search: PropTypes.string })
};

StockLocator.defaultProps = {
    items: [],
    itemsLoading: true,
    drillBackPath: null
};

export default StockLocator;
