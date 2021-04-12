import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Title,
    SingleEditTable,
    Loading,
    Dropdown,
    InputField,
    BackButton
} from '@linn-it/linn-form-components-library';
import Typography from '@material-ui/core/Typography';
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import PropTypes from 'prop-types';
import queryString from 'query-string';
import Page from '../../containers/Page';

function StockLocator({
    items,
    itemsLoading,
    history,
    quantities,
    quantitiesLoading,
    options,
    fetchItems
}) {
    const [selectedQuantities, setSelectQuantities] = useState();

    useEffect(() => {
        if (Object.values(queryString.parse(options)).some(x => x !== null && x !== '')) {
            fetchItems(null, `&${options}`);
        }
    }, [options, fetchItems]);

    useEffect(() => {
        if (quantities?.length > 0) {
            setSelectQuantities(quantities[0]);
        }
    }, [quantities]);

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
            id: 'component',
            type: 'component',
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
                    <Title text="Stock Locations" />
                </Grid>
                <Grid item xs={3}>
                    <BackButton
                        backClick={() => history.push('/inventory/stock-locator')}
                        text="back to search"
                    />
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
                                rows={items.map((i, index) => ({
                                    ...i,
                                    id: index,
                                    component: (
                                        <button
                                            type="button"
                                            onClick={() => {
                                                history.push(
                                                    `/inventory/stock-locator/locators/batches?${queryString.stringify(
                                                        {
                                                            partNumber: i.partNumber,
                                                            locationId: i.locationId,
                                                            palletNumber: i.palletNumber?.toString(),
                                                            state: i.state,
                                                            category: i.category?.toString(),
                                                            queryBatchView: true
                                                        }
                                                    )}`
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
                        {quantitiesLoading && <Loading />}
                        {quantities?.length && selectedQuantities && !quantitiesLoading && (
                            <Accordion>
                                <AccordionSummary
                                    expandIcon={<ExpandMoreIcon />}
                                    aria-controls="panel1a-content"
                                    id="panel1a-header"
                                >
                                    <Typography>Click here to show quantities</Typography>
                                </AccordionSummary>
                                <AccordionDetails>
                                    <Grid container spacing={3}>
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
                                                        quantities.find(
                                                            x => x.partNumber === newValue
                                                        )
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
                                    </Grid>
                                </AccordionDetails>
                            </Accordion>
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
    fetchItems: PropTypes.func.isRequired,
    options: PropTypes.string.isRequired,
    itemsLoading: PropTypes.bool,
    history: PropTypes.shape({ goBack: PropTypes.func, push: PropTypes.func }).isRequired,
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
