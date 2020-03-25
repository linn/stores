import React from 'react';
import { Typography } from '@material-ui/core';
import Page from '../containers/Page';

function App() {
    return (
        <Page>
            <Typography variant="h6">Stores</Typography>
            <List>
                <ListItem component={Link} to="/logistics/allocations" button>
                    <Typography color="primary">Start Allocation</Typography>
                </ListItem>
            </List>
        </Page>
    );
}

export default App;
