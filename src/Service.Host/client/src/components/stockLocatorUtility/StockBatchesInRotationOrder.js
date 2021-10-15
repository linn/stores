import React from 'react';
import Grid from '@material-ui/core/Grid';
import { BackButton, ErrorCard, Loading } from '@linn-it/linn-form-components-library';
import Typography from '@material-ui/core/Typography';
import { DataGrid } from '@mui/x-data-grid';
import PropTypes from 'prop-types';
import moment from 'moment';
import Page from '../../containers/Page';

function StockBatchesInRotationOrder({ items, loading, error, history }) {
    const parts = [...new Set(items.map(x => x.partNumber))].sort().map(p => ({
        partNumber: p,
        description: items.find(x => x.partNumber === p).partDescription
    }));

    const columns = [
        { field: 'id', headerName: 'id', width: 140, hide: true },
        { field: 'state', headerName: 'State', width: 150 },
        { field: 'stockPoolCode', headerName: 'Pool', width: 150 },
        { field: 'quantity', headerName: 'Qty', width: 100 },
        { field: 'quantityAllocated', headerName: 'Alloc', width: 150 },
        { field: 'locationName', headerName: 'Storage Place', width: 200 },
        { field: 'batchRef', headerName: 'Batch', width: 200 },
        { field: 'stockRotationDate', headerName: 'Date', width: 200 }
    ];

    if (error) {
        return (
            <Page>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error?.details?.errors?.[0] || error.statusText} />
                    </Grid>
                    <Grid item xs={2}>
                        <BackButton backClick={() => history.push('/inventory/stock-locator')} />
                    </Grid>
                    <Grid item xs={10} />
                </Grid>
            </Page>
        );
    }

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={2}>
                    <BackButton backClick={() => history.push('/inventory/stock-locator')} />
                </Grid>
                <Grid item xs={10} />
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        {parts.map(p => (
                            <>
                                <Grid item xs={12}>
                                    <Typography variant="h4">{p.partNumber}</Typography>
                                    <Typography variant="h6">{p.description}</Typography>
                                </Grid>
                                <Grid item xs={12}>
                                    <div style={{ width: '100%' }}>
                                        <DataGrid
                                            rows={items
                                                .filter(i => i.partNumber === p.partNumber)
                                                .map(i => ({
                                                    ...i,
                                                    stockRotationDate: moment(
                                                        i.stockRotationDate
                                                    ).format('DD-MMM-YYYY'),
                                                    locationName:
                                                        i.locationName || `P${i.palletNumber}`
                                                }))}
                                            columnBuffer={7}
                                            columns={columns}
                                            density="standard"
                                            rowHeight={34}
                                            autoHeight
                                            loading={false}
                                            hideFooter
                                        />
                                    </div>
                                </Grid>
                            </>
                        ))}
                    </>
                )}
            </Grid>
        </Page>
    );
}

StockBatchesInRotationOrder.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    loading: PropTypes.bool,
    error: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        item: PropTypes.string,
        details: PropTypes.shape({
            errors: PropTypes.arrayOf(PropTypes.shape({}))
        })
    })
};

StockBatchesInRotationOrder.defaultProps = {
    items: null,
    loading: false,
    error: null
};

export default StockBatchesInRotationOrder;
