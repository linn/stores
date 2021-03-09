import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    ReportTable,
    Loading,
    ErrorCard,
    SingleEditTable
} from '@linn-it/linn-form-components-library';
import Table from '@material-ui/core/Table';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import TableBody from '@material-ui/core/TableBody';
import { makeStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    printButton: {
        marginTop: theme.spacing(6),
        marginBottom: theme.spacing(2)
    },
    viewButton: {
        marginBottom: theme.spacing(2)
    },
    printTable: {
        width: '100%'
    }
}));

export default function StoragePlaceAuditReport({ reportData, loading, error }) {
    const classes = useStyles();

    const ViewLayout = () => (
        <Page>
            <Grid container spacing={3}>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                <Grid item xs={12}>
                    {loading ? (
                        <Loading />
                    ) : (
                        reportData && (
                            <ReportTable
                                reportData={reportData}
                                showTotals={false}
                                showTitle
                                title={reportData ? reportData?.title.displayString : 'Loading'}
                            />
                        )
                    )}
                </Grid>
            </Grid>
        </Page>
    );

    const PrintLayout = () => {
        const headers = reportData.headers.columnHeaders;

        const columns = headers.map(header => ({
            id: header,
            type: 'text',
            title: header
        }));

        columns.push({ id: 'counted', type: 'text', title: 'Counted' });
        columns.push({ id: 'adjusted', type: 'text', title: 'Adjusted' });

        const rows = reportData.results.map((result, i) => {
            const row = { id: i };

            headers.forEach((h, j) => {
                row[h] = result.values[j].textDisplayValue
                    ? result.values[j].textDisplayValue
                    : result.values[j].displayValue;
            });

            return row;
        });

        return (
            <>
                <SingleEditTable editable={false} columns={columns} rows={rows} />
                <Table className={classes.printTable}>
                    <TableHead>
                        <TableRow>
                            <TableCell>Audited By</TableCell>
                            <TableCell>Date</TableCell>
                            <TableCell>Requisition</TableCell>
                            <TableCell>Posted</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        <TableRow>
                            <TableCell />
                            <TableCell />
                            <TableCell />
                            <TableCell />
                        </TableRow>
                    </TableBody>
                </Table>
            </>
        );
    };

    return (
        <>
            {reportData && !loading && (
                <div className="print-landscape show-only-when-printing">
                    <PrintLayout reportData={reportData} />
                </div>
            )}
            <div className="hide-when-printing">
                <ViewLayout />
            </div>
        </>
    );
}

StoragePlaceAuditReport.propTypes = {
    reportData: PropTypes.shape({
        title: PropTypes.shape({ displayString: PropTypes.string }),
        headers: PropTypes.shape(),
        results: PropTypes.arrayOf(PropTypes.shape)
    }),
    loading: PropTypes.bool,
    error: PropTypes.string
};

StoragePlaceAuditReport.defaultProps = {
    reportData: null,
    loading: false,
    error: ''
};
