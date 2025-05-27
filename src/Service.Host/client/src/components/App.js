import React from 'react';
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
                <ListItem component={Link} to="/logistics/wand" button>
                    <Typography color="primary">Wand</Typography>
                </ListItem>
                <ListItem component={Link} to="/parts" button>
                    <Typography color="primary">Parts Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/parts/sources" button>
                    <Typography color="primary">Mech Part Sources Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/inventory/move-stock" button>
                    <Typography color="primary">Stock Move</Typography>
                </ListItem>
                <ListItem component={Link} to="/inventory/stock-locator" button>
                    <Typography color="primary">Stock Locator Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/logistics/parcels" button>
                    <Typography color="primary">Parcel Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/logistics/import-books" button>
                    <Typography color="primary">Impbook Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/logistics/consignments" button>
                    <Typography color="primary">Consignment Utility</Typography>
                </ListItem>
                <ListItem component={Link} to="/logistics/labels-reprint" button>
                    <Typography color="primary">Despatch Label Reprint</Typography>
                </ListItem>
            </List>
            <Typography variant="h6">Reports</Typography>
            <List>
                <ListItem component={Link} to="/inventory/reports/what-will-decrement" button>
                    <Typography color="primary">What Will Decrement Report</Typography>
                </ListItem>
                <ListItem
                    component={Link}
                    to="/logistics/allocations/despatch-picking-summary"
                    button
                >
                    <Typography color="primary">Despatch Picking Summary Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/inventory/tqms-category-summary" button>
                    <Typography color="primary">TQMS Summary Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/logistics/import-books/ipr" button>
                    <Typography color="primary">IPR Import Books Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/logistics/import-books/eu" button>
                    <Typography color="primary">EU Import Books Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/inventory/reports/stores-move-log" button>
                    <Typography color="primary">Stores Move Log Report</Typography>
                </ListItem>
                <ListItem component={Link} to="/inventory/reports/stock-locators-report" button>
                    <Typography color="primary">Stock Locator Report</Typography>
                </ListItem>
            </List>
        </Page>
    );
}

export default App;
