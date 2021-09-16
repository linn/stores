import React from 'react';
import { Loading } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import moment from 'moment';

function PackingListTab({ consignmentPackingList, consignmentPackingListLoading }) {
    const useStyles = makeStyles(() => ({
        multiLineText: {
            whiteSpace: 'pre-line',
            verticalAlign: 'top'
        },
        table: {
            minWidth: 650
        },
        noWrap: {
            whiteSpace: 'nowrap'
        }
    }));
    const classes = useStyles();

    const getItems = items => {
        if (!items) {
            return [];
        }

        return items.map(d => ({ id: d.itemNumber, ...d }));
    };

    const getPallets = pallets => {
        if (!pallets) {
            return [];
        }

        return pallets.map(d => ({ id: d.palletNumber, ...d }));
    };

    const showItems = items => {
        if (!items || items.length === 0) {
            return '';
        }

        return (
            <>
                <Grid item xs={10}>
                    <div>
                        <Table className={classes.table} size="small">
                            <TableHead>
                                <TableRow>
                                    <TableCell align="right">Box</TableCell>
                                    <TableCell>Description</TableCell>
                                    <TableCell>Weight</TableCell>
                                    <TableCell>Dimensions</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {getItems(items).map(row => (
                                    <TableRow key={row.id}>
                                        <TableCell align="right">{row.containerNumber}</TableCell>
                                        <TableCell>{row.description}</TableCell>
                                        <TableCell className={classes.noWrap}>
                                            {row.weight}
                                        </TableCell>
                                        <TableCell className={classes.noWrap}>
                                            {row.displayDimensions}
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </div>
                </Grid>
                <Grid item xs={2} />
            </>
        );
    };

    return (
        <>
            <Grid container spacing={3} style={{ paddingTop: '30px' }}>
                {consignmentPackingListLoading || !consignmentPackingList ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        <Grid item xs={12}>
                            <Typography variant="h5" align="left">
                                {`Packing List - Consignment ${consignmentPackingList.consignmentId}`}
                            </Typography>
                        </Grid>
                        <Grid item xs={4}>
                            <Typography variant="subtitle2">Date Despatched</Typography>
                            <Typography variant="body2">
                                {consignmentPackingList.despatchDate
                                    ? moment(consignmentPackingList.despatchDate).format(
                                          'DD MMM YYYY'
                                      )
                                    : 'Not yet dispatched'}
                            </Typography>
                        </Grid>
                        <Grid item xs={4}>
                            <Typography variant="subtitle2">Delivery Address</Typography>
                            <Typography variant="body2" className={classes.multiLineText}>
                                {consignmentPackingList.deliveryAddress}
                            </Typography>
                        </Grid>
                        <Grid item xs={4}>
                            <Typography variant="subtitle2">Sender Address</Typography>
                            <Typography variant="body2" className={classes.multiLineText}>
                                {consignmentPackingList.senderAddress}
                            </Typography>
                        </Grid>
                        {showItems(consignmentPackingList.items)}
                        <>
                            {getPallets(consignmentPackingList.pallets).map(pallet => (
                                <>
                                    <Grid item xs={12}>
                                        <Typography variant="h6">
                                            {`Pallet ${pallet.palletNumber} - Dimensions: ${pallet.displayDimensions} Weight: ${pallet.displayWeight}`}
                                        </Typography>
                                    </Grid>
                                    {showItems(pallet.items)}
                                </>
                            ))}
                        </>
                        <Grid item xs={3} />
                        <Grid item xs={6}>
                            <Typography variant="h6">{`Total Pallets: ${consignmentPackingList.numberOfPallets} Total Boxes: ${consignmentPackingList.numberOfItemsNotOnPallets}`}</Typography>
                        </Grid>
                        <Grid item xs={3} />
                        <Grid item xs={3} />
                        <Grid item xs={6}>
                            <Typography variant="h6">{`Total Gross Weight: ${consignmentPackingList.totalGrossWeight}`}</Typography>
                        </Grid>
                        <Grid item xs={3} />
                        <Grid item xs={3} />
                        <Grid item xs={6}>
                            <Typography variant="h6">{`Total Volume: ${consignmentPackingList.totalVolume}`}</Typography>
                        </Grid>
                        <Grid item xs={3} />
                    </>
                )}
            </Grid>
        </>
    );
}

PackingListTab.propTypes = {
    consignmentPackingList: PropTypes.shape({
        consignmentId: PropTypes.number,
        despatchDate: PropTypes.string,
        deliveryAddress: PropTypes.string,
        senderAddress: PropTypes.string,
        items: PropTypes.arrayOf(PropTypes.shape({})),
        pallets: PropTypes.arrayOf(PropTypes.shape({})),
        numberOfPallets: PropTypes.number,
        numberOfItemsNotOnPallets: PropTypes.number,
        totalGrossWeight: PropTypes.string,
        totalVolume: PropTypes.string
    }),
    consignmentPackingListLoading: PropTypes.bool
};

PackingListTab.defaultProps = {
    consignmentPackingList: null,
    consignmentPackingListLoading: false
};

export default PackingListTab;
