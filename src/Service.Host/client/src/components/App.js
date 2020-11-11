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
                <ListItem component={Link} to="/inventory/parts" button>
                    <Typography color="primary">Parts Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/inventory/parts/sources" button>
                    <Typography color="primary">Mech Part Sources Utility</Typography>
                </ListItem>
            </List>
        </Page>
    );
}

export default App;
