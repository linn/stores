import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import Grid from '@material-ui/core/Grid';
import { DataGrid } from '@material-ui/data-grid';
import { Title, Dropdown, Loading, InputField } from '@linn-it/linn-form-components-library';
import Page from '../containers/Page';

const useStyles = makeStyles({
    runButton: {
        float: 'right',
        width: '100%'
    }
});

function Wand({ wandConsignments, loadingWandConsignments, getItems, items, itemsLoading }) {
    const [consignmentId, setConsignmentId] = useState(null);
    const [wandAction, setwandAction] = useState('W');

    const classes = useStyles();

    const handleConsignmentChange = (_propertyName, newValue) => {
        setConsignmentId(newValue);
        getItems(newValue);
    };

    const handlewandActionChange = (_propertyName, newValue) => {
        setwandAction(newValue);
    };

    const consignmentOptions = () => {
        return wandConsignments?.map(c => ({
            id: c.consignmentId,
            displayText: `Consignment: ${c.consignmentId} Addressee: ${c.addressee} Country: ${
                c.countryCode
            } ${c.isDone ? c.isDone : ' '} `
        }));
    };

    const getDetailRows = details => {
        if (!details) {
            return [];
        }

        return details.map((d, i) => ({ id: i, ...d }));
    };

    const columns = [
        { field: 'partNumber', headerName: 'Article No', width: 140 },
        { field: 'quantity', headerName: 'Qty', width: 100 },
        { field: 'quantityScanned', headerName: 'Scanned', width: 120 },
        { field: 'partDescription', headerName: 'Description', width: 230 },
        { field: 'orderNumber', headerName: 'Order', width: 100 },
        { field: 'orderLine', headerName: 'Line', width: 80 },
        { field: 'linnBarCode', headerName: 'Bar Code', width: 120, hide: true }
    ];

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Wand" />
                </Grid>
            </Grid>
            {loadingWandConsignments ? (
                <Loading />
            ) : (
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <Dropdown
                            label="Consignment"
                            propertyName="consignment"
                            items={consignmentOptions()}
                            value={consignmentId}
                            onChange={handleConsignmentChange}
                        />
                    </Grid>
                    <Grid item xs={2}>
                        <Dropdown
                            label="Action"
                            propertyName="wandAction"
                            items={[
                                { id: 'W', displayText: 'Wand' },
                                { id: 'U', displayText: 'Unwand' }
                            ]}
                            value={wandAction}
                            onChange={handlewandActionChange}
                        />
                    </Grid>
                    <Grid item xs={8}>
                        <InputField fullWidth autoFocus label="Wand String" />
                    </Grid>
                    <Grid item xs={2} />
                    <Grid item xs={12}>
                        <div style={{ display: 'flex', height: 600 }}>
                            <div style={{ flexGrow: 1 }}>
                                <DataGrid
                                    rows={getDetailRows(items)}
                                    columns={columns}
                                    density="compact"
                                    autoHeight
                                    loading={itemsLoading}
                                    hideFooter
                                />
                            </div>
                        </div>
                    </Grid>
                </Grid>
            )}
        </Page>
    );
}

Wand.propTypes = {
    wandConsignments: PropTypes.arrayOf(PropTypes.shape({})),
    loadingWandConsignments: PropTypes.bool,
    getItems: PropTypes.func.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    itemsLoading: PropTypes.bool
};

Wand.defaultProps = {
    wandConsignments: [],
    loadingWandConsignments: false,
    items: [],
    itemsLoading: false
};

export default Wand;
