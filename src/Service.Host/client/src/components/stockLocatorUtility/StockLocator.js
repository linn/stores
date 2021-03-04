import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Title,
    SingleEditTable,
    Loading,
    Dropdown,
    InputField
} from '@linn-it/linn-form-components-library';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';
import queryString from 'query-string';
import Page from '../../containers/Page';

function StockLocator({ items, itemsLoading, fetchItems, options, quantities, quantitiesLoading }) {
    const [batchView, setBatchView] = useState(false);
    const [hasDrilledDown, setHasDrilledDown] = useState(false);
    const [selectedQuantities, setSelectQuantities] = useState();

    useEffect(() => {
        if (quantities?.length > 0) {
            setSelectQuantities(quantities[0]);
        }
    }, [quantities]);

    const variableColumns =
        options?.batchRef || batchView
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
            title: 'Expand',
            id: 'component',
            type: 'component',
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
                    <Title
                        text={
                            options?.batchRef || batchView ? 'Locator Batches' : 'Stock Locations'
                        }
                    />
                </Grid>
                {itemsLoading || quantitiesLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        {quantities?.length && selectedQuantities && !batchView && (
                            <>
                                <Grid item xs={3}>
                                    <Dropdown
                                        items={quantities?.map(v => ({
                                            id: v.partNumber,
                                            displayText: v.partNumber
                                        }))}
                                        value={selectedQuantities.partNumber}
                                        label="Show Summaries For Part"
                                        propertyName="part"
                                        onChange={(_propertyName, newValue) =>
                                            setSelectQuantities(
                                                quantities.find(x => x.partNumber === newValue)
                                            )
                                        }
                                        allowNoValue={false}
                                    />
                                </Grid>
                                <Grid item xs={9} />
                                <Grid item xs={1}>
                                    <Typography variant="subtitle1" align="right">
                                        Main
                                    </Typography>
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        label="Good (Allocated)"
                                        propertyName="goodStock"
                                        value={`${selectedQuantities.goodStock} (${selectedQuantities.goodStockAllocated})`}
                                        disabled
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        label="Uninspected (Allocated)"
                                        propertyName="uninspectedStock"
                                        value={`${selectedQuantities.uninspectedStock} (${selectedQuantities.uninspectedStockAllocated})`}
                                        disabled
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        label="Faulty (Allocated)"
                                        propertyName="uninspectedStockAllocated"
                                        value={`${selectedQuantities.faultyStock} (${selectedQuantities.faultyStockAllocated})`}
                                        disabled
                                    />
                                </Grid>
                                <Grid item xs={2} />

                                <Grid item xs={1}>
                                    <Typography variant="subtitle1" align="right">
                                        Distributor
                                    </Typography>
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        label="Good (Allocated)"
                                        propertyName="distributorStock"
                                        value={`${selectedQuantities.distributorStock} (${selectedQuantities.distributorStockAllocated})`}
                                        disabled
                                    />
                                </Grid>
                                <Grid item xs={8} />
                                <Grid item xs={1}>
                                    <Typography variant="subtitle1" align="right">
                                        Other
                                    </Typography>
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        label="Good (Allocated)"
                                        propertyName="otherStock"
                                        value={`${selectedQuantities.otherStock} (${selectedQuantities.otherStockAllocated})`}
                                        disabled
                                    />
                                </Grid>
                                <Grid item xs={8} />
                            </>
                        )}
                        {items && (
                            <SingleEditTable
                                newRowPosition="top"
                                columns={columns}
                                rows={items.map(i => ({
                                    ...i,
                                    id: i.id + i.batchRef + i.partNumber,
                                    component: hasDrilledDown ? (
                                        <button
                                            type="button"
                                            onClick={() => {
                                                setBatchView(false);
                                                setHasDrilledDown(false);
                                                fetchItems(
                                                    null,
                                                    `&${queryString.stringify(options)}`
                                                );
                                            }}
                                        >
                                            -
                                        </button>
                                    ) : (
                                        <button
                                            type="button"
                                            onClick={() => {
                                                setBatchView(true);
                                                setHasDrilledDown(true);
                                                fetchItems(
                                                    null,
                                                    `&locationId=${i.id}&partNumber=${
                                                        i.partNumber
                                                    }&queryBatchView=${true}&batchRef=${
                                                        i.batchRef ? i.batchRef : ''
                                                    }`
                                                );
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
    fetchItems: PropTypes.func.isRequired,
    quantities: PropTypes.arrayOf(
        PropTypes.shape({
            partNumber: PropTypes.string,
            goodStock: PropTypes.number,
            goodStockAllocated: PropTypes.number,
            uninspectedStock: PropTypes.number,
            uninspectedStockAllocated: PropTypes.number,
            faultyStock: PropTypes.number,
            faultyStockAllocated: PropTypes.number,
            distributorStock: PropTypes.number,
            distributorStockAllocated: PropTypes.number,
            supplierStock: PropTypes.number,
            supplierStockAllocated: PropTypes.number,
            otherStock: PropTypes.number,
            otherStockAllocated: PropTypes.number
        })
    ),
    quantitiesLoading: PropTypes.bool
};

StockLocator.defaultProps = {
    items: [],
    itemsLoading: true,
    quantities: null,
    quantitiesLoading: false
};

export default StockLocator;
