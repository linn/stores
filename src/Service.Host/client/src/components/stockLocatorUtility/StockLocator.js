import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Title,
    SingleEditTable,
    Loading,
    Dropdown,
    ErrorCard,
    InputField,
    BackButton
} from '@linn-it/linn-form-components-library';
import Typography from '@material-ui/core/Typography';
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Dialog from '@material-ui/core/Dialog';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import { CSVLink } from 'react-csv';
import CloseIcon from '@material-ui/icons/Close';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';
import makeStyles from '@material-ui/styles/makeStyles';
import queryString from 'query-string';
import Page from '../../containers/Page';
import config from '../../config';

function StockLocator({
    items,
    itemsLoading,
    history,
    quantities,
    quantitiesLoading,
    options,
    fetchItems,
    moves,
    movesLoading,
    fetchMoves,
    clearMoves,
    itemError,
    clearErrors
}) {
    const [selectedQuantities, setSelectedQuantities] = useState();
    const [dialogOpen, setDialogOpen] = useState(false);

    const useStyles = makeStyles(theme => ({
        dialog: {
            margin: theme.spacing(6),
            width: '80%'
        }
    }));

    const classes = useStyles();

    useEffect(() => {
        if (Object.values(queryString.parse(options)).some(x => x !== null && x !== '')) {
            clearErrors();
            fetchItems(null, `&${options}`);
        }
    }, [options, fetchItems, clearErrors]);

    useEffect(() => {
        if (quantities?.length > 0 && items?.length > 0) {
            setSelectedQuantities(
                quantities.filter(q => items.map(i => i.partNumber).includes(q.partNumber))[0]
            );
        }
    }, [quantities, items]);

    const columns = [
        {
            title: 'Part',
            id: 'partLinkComponent',
            type: 'component',
            editable: false
        },
        {
            title: 'Desc',
            id: 'partDescription',
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
            id: 'drillDownButtonComponent',
            type: 'component',
            editable: false
        },
        {
            title: 'Qty Allocated',
            id: 'qtyAllocatedComponent',
            type: 'component',
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
        },
        {
            title: 'Desc',
            id: 'locationDescription',
            type: 'text',
            editable: false
        },
        {
            title: 'Trigger Lvl',
            id: 'triggerLevel',
            type: 'text',
            editable: false
        },
        {
            title: 'Max Cap',
            id: 'maxCapacity',
            type: 'text',
            editable: false
        }
    ];

    const moveColumns = [
        {
            title: 'Part',
            id: 'partNumber',
            type: 'text',
            editable: false
        },
        {
            title: 'Qty Allocated',
            id: 'qtyAllocated',
            type: 'number',
            editable: false
        },
        {
            title: 'Trans Code',
            id: 'transactionCode',
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
            title: 'Req',
            id: 'reqNumber',
            type: 'number',
            editable: false
        },
        {
            title: 'Line',
            id: 'lineNumber',
            type: 'number',
            editable: false
        },
        {
            title: 'Sequence',
            id: 'sequence',
            type: 'number',
            editable: false
        }
    ];
    const csvData = [
        columns.map(c => c.title),
        ...items.map(i =>
            columns.map(col => i[col.id === 'partLinkComponent' ? 'partNumber' : col.id])
        )
    ];
    return (
        <div className="print-landscape">
            <Page width="xl">
                <Grid container spacing={3}>
                    <Dialog open={dialogOpen} fullWidth maxWidth="lg">
                        <div>
                            <IconButton
                                className={classes.pullRight}
                                aria-label="Close"
                                onClick={() => {
                                    clearMoves();
                                    setDialogOpen(false);
                                }}
                            >
                                <CloseIcon />
                            </IconButton>
                            <div className={classes.dialog}>
                                <Grid container spacing={3}>
                                    <Grid item xs={12}>
                                        <Typography variant="h5" gutterBottom>
                                            Stock Moves
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12}>
                                        {movesLoading && <Loading />}
                                    </Grid>
                                    <Grid item xs={12}>
                                        {moves?.length ? (
                                            <SingleEditTable
                                                editable={false}
                                                allowNewRowCreation={false}
                                                newRowPosition="top"
                                                columns={moveColumns}
                                                rows={moves.map((i, index) => ({
                                                    ...i,
                                                    id: index
                                                }))}
                                            />
                                        ) : (
                                            <> </>
                                        )}
                                    </Grid>
                                </Grid>
                            </div>
                        </div>
                    </Dialog>
                    <Grid item xs={12}>
                        <Title text="Stock Locations" />
                    </Grid>
                    <Grid item xs={3} className="hide-when-printing">
                        <BackButton
                            backClick={() => history.push('/inventory/stock-locator')}
                            text="back to search"
                        />
                    </Grid>
                    <Grid item xs={9} />
                    <Grid item xs={12}>
                        {itemError && (
                            <ErrorCard
                                errorMessage={itemError.details?.message || itemError.statusText}
                            />
                        )}
                    </Grid>
                    {itemsLoading ? (
                        <Grid item xs={12}>
                            <Loading />
                        </Grid>
                    ) : (
                        <>
                            {items && (
                                <>
                                    <Grid item xs={10} />
                                    <Grid item xs={2}>
                                        <CSVLink data={csvData}>
                                            <Button variant="contained">Export</Button>
                                        </CSVLink>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <SingleEditTable
                                            newRowPosition="top"
                                            columns={columns}
                                            rows={items.map((i, index) => ({
                                                ...i,
                                                id: index,
                                                partLinkComponent: (
                                                    <>
                                                        <Link
                                                            to={
                                                                i.links.find(l => l.rel === 'part')
                                                                    ?.href
                                                            }
                                                        >
                                                            {i.partNumber}
                                                        </Link>
                                                        {'       '}
                                                        <button
                                                            type="button"
                                                            style={{ cursor: 'pointer' }}
                                                            onClick={() => {
                                                                window.open(
                                                                    `${config.appRoot}${
                                                                        i.links.find(
                                                                            l =>
                                                                                l.rel ===
                                                                                'part-used-on'
                                                                        )?.href
                                                                    }`,
                                                                    '_blank'
                                                                );
                                                            }}
                                                        >
                                                            +
                                                        </button>
                                                    </>
                                                ),
                                                drillDownButtonComponent: (
                                                    <button
                                                        type="button"
                                                        style={{ cursor: 'pointer' }}
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
                                                ),
                                                qtyAllocatedComponent: (
                                                    <>
                                                        {i.quantityAllocated && (
                                                            <button
                                                                style={{ cursor: 'pointer' }}
                                                                type="button"
                                                                onClick={() => {
                                                                    fetchMoves(
                                                                        i.partNumber,
                                                                        `&palletNumber=${i.palletNumber ||
                                                                            ''}&locationId=${i.locationId ||
                                                                            ''} `
                                                                    );
                                                                    setDialogOpen(true);
                                                                }}
                                                            >
                                                                {i.quantityAllocated}
                                                            </button>
                                                        )}
                                                    </>
                                                )
                                            }))}
                                            allowNewRowCreation={false}
                                            editable={false}
                                            allowNewRowCreations
                                        />
                                    </Grid>
                                </>
                            )}
                            <Grid item xs={12}>
                                <Accordion>
                                    <AccordionSummary
                                        expandIcon={<ExpandMoreIcon />}
                                        aria-controls="panel1a-content"
                                        id="panel1a-header"
                                    >
                                        <Typography className="hide-when-printing">
                                            Click here to show quantities
                                        </Typography>
                                    </AccordionSummary>
                                    <AccordionDetails>
                                        <Grid container spacing={3}>
                                            {quantitiesLoading && (
                                                <Grid item xs={12}>
                                                    <Loading />
                                                </Grid>
                                            )}
                                            {quantities?.length &&
                                                selectedQuantities &&
                                                !quantitiesLoading && (
                                                    <>
                                                        <Grid item xs={3}>
                                                            <Dropdown
                                                                items={quantities
                                                                    ?.filter(x =>
                                                                        items
                                                                            .map(i => i.partNumber)
                                                                            .includes(x.partNumber)
                                                                    )
                                                                    .map(v => ({
                                                                        id: v.partNumber,
                                                                        displayText: v.partNumber
                                                                    }))}
                                                                value={
                                                                    selectedQuantities.partNumber
                                                                }
                                                                label="Show Summaries For Part"
                                                                propertyName="part"
                                                                onChange={(
                                                                    _propertyName,
                                                                    newValue
                                                                ) =>
                                                                    setSelectedQuantities(
                                                                        quantities.find(
                                                                            x =>
                                                                                x.partNumber ===
                                                                                newValue
                                                                        )
                                                                    )
                                                                }
                                                                allowNoValue={false}
                                                            />
                                                        </Grid>
                                                        <Grid item xs={9} />
                                                        <Grid item xs={1}>
                                                            <Typography
                                                                variant="subtitle1"
                                                                align="right"
                                                            >
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
                                                            <Typography
                                                                variant="subtitle1"
                                                                align="right"
                                                            >
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
                                                            <Typography
                                                                variant="subtitle1"
                                                                align="right"
                                                            >
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
                                                        </Grid>{' '}
                                                    </>
                                                )}
                                            <Grid item xs={8} />
                                        </Grid>
                                    </AccordionDetails>
                                </Accordion>{' '}
                            </Grid>
                        </>
                    )}
                </Grid>
            </Page>
        </div>
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
    quantitiesLoading: PropTypes.bool,
    moves: PropTypes.arrayOf(PropTypes.shape({})),
    movesLoading: PropTypes.bool,
    fetchMoves: PropTypes.func.isRequired,
    clearMoves: PropTypes.func.isRequired,
    itemError: PropTypes.shape({
        statusText: PropTypes.string,
        details: PropTypes.shape({ message: PropTypes.string })
    }),
    clearErrors: PropTypes.func.isRequired
};

StockLocator.defaultProps = {
    items: [],
    itemsLoading: true,
    quantities: null,
    quantitiesLoading: false,
    moves: [],
    movesLoading: false,
    itemError: null
};

export default StockLocator;
