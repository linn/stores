import React from 'react';
import { Loading } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import { DataGrid } from '@material-ui/data-grid';
import TableCell from '@material-ui/core/TableCell';
import Button from '@material-ui/core/Button';
import TableRow from '@material-ui/core/TableRow';
import { makeStyles } from '@material-ui/styles';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';

const useStyles = makeStyles(theme => ({
    grid: {
        marginTop: theme.spacing(4),
        paddingLeft: theme.spacing(1),
        paddingRight: theme.spacing(1)
    },
    tableCell: {
        borderBottom: 'none'
    }
}));

function DespatchPalletQueueReport({ reportData, loading }) {
    const classes = useStyles();
    const date = new Date().toLocaleString('en-GB', {
        month: 'short',
        year: 'numeric',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });

    const columns = [
        { field: 'palletNumber', headerName: 'Pallet No', width: 130 },
        { field: 'pickingSequence', headerName: 'Picking Seq', width: 130 }
    ];

    const getDetailRows = details => {
        if (!details) {
            return [];
        }

        return details.map((d, i) => ({ id: i, ...d }));
    };

    return (
        <Grid className={classes.grid} container justify="center">
            <Grid item xs={12}>
                <Typography variant="h5" gutterBottom>
                    Despatch Pallet Queue With Warehouse Information
                </Typography>
                <span className="date-for-printing">{date}</span>
            </Grid>
            {loading || !reportData ? (
                <Loading />
            ) : (
                <>
                    <Grid item xs={6} style={{ paddingTop: 20, paddingBottom: 20 }}>
                        <Table size="small">
                            <TableBody>
                                <TableRow key="totalRow">
                                    <TableCell className={classes.tableCell}>
                                        Total number of pallets in queue
                                    </TableCell>
                                    <TableCell className={classes.tableCell}>
                                        {reportData.totalNumberOfPallets}
                                    </TableCell>
                                    <TableCell className={classes.tableCell} />
                                </TableRow>
                                <TableRow key="movableRow">
                                    <TableCell className={classes.tableCell}>
                                        Total number of pallets to come out
                                    </TableCell>
                                    <TableCell className={classes.tableCell}>
                                        {reportData.numberOfPalletsToMove}
                                    </TableCell>
                                    <TableCell className={classes.tableCell}>
                                        <Button>Pick Them All</Button>
                                    </TableCell>
                                </TableRow>
                            </TableBody>
                        </Table>
                    </Grid>
                    <Grid item xs={6} />
                    <Grid item xs={12}>
                        <div style={{ width: 300 }}>
                            <DataGrid
                                rows={getDetailRows(reportData.despatchPalletQueueDetails)}
                                columns={columns}
                                density="compact"
                                autoHeight
                                hideFooter
                            />
                        </div>
                    </Grid>
                </>
            )}
        </Grid>
    );
}

DespatchPalletQueueReport.propTypes = {
    reportData: PropTypes.shape({
        totalNumberOfPallets: PropTypes.number,
        numberOfPalletsToMove: PropTypes.number,
        despatchPalletQueueDetails: PropTypes.arrayOf(PropTypes.shape({}))
    }),
    loading: PropTypes.bool
};

DespatchPalletQueueReport.defaultProps = {
    reportData: null,
    loading: false
};

export default DespatchPalletQueueReport;
