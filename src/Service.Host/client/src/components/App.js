﻿import React from 'react';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import { Link } from 'react-router-dom';
import Typography from '@material-ui/core/Typography';
import Page from '../containers/Page';

function App() {
    return (
        <Page>
            <Typography variant="h6">Stores</Typography>
            <List>
                <ListItem component={Link} to="/logistics/allocations" button>
                    <Typography color="primary">Start Allocation</Typography>
                </ListItem>
                <ListItem component={Link} to="/logistics/workstations/top-up" button>
                    <Typography color="primary">Workstation Run</Typography>
                </ListItem>
                <ListItem component={Link} to="/logistics/parcels" button>
                    <Typography color="primary">Parcel Ut</Typography>
                </ListItem>
                <ListItem component={Link} to="/inventory/parts" button>
                    <Typography color="primary">Parts Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/inventory/parts/sources" button>
                    <Typography color="primary">Mech Part Sources Utility</Typography>
                </ListItem>
            </List>
            <Typography variant="h6">Reports</Typography>
            <List>
                <ListItem component={Link} to="/inventory/reports/storage-place-audit" button>
                    <Typography color="primary">Storage Place Audit Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/inventory/reports/what-will-decrement" button>
                    <Typography color="primary">What Will Decrement Report</Typography>
                </ListItem>
            </List>
        </Page>
    );
}

export default App;
