import React from 'react';
import PropTypes from 'prop-types';
import Paper from '@material-ui/core/Paper';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import moment from 'moment';

export default function DisplayOnlyFields({ exportReturn }) {
    return (
        <Paper>
            <List dense>
                <ListItem>
                    <ListItemText primary="Return ID" secondary={exportReturn.returnId} />
                </ListItem>
                <ListItem>
                    <ListItemText
                        primary="Return for Credit"
                        secondary={exportReturn.returnForCredit}
                    />
                </ListItem>
                <ListItem>
                    <ListItemText
                        primary="Raised By"
                        secondary={`${exportReturn.raisedBy?.id} - ${exportReturn.raisedBy?.fullName}`}
                    />
                </ListItem>
                <ListItem>
                    <ListItemText
                        primary="Date Created"
                        secondary={moment(exportReturn.dateCreated).format('DD MMM YYYY')}
                    />
                </ListItem>
                {exportReturn.dateCancelled && (
                    <ListItem>
                        <ListItemText
                            primary="Date Cancelled"
                            secondary={exportReturn.dateCancelled}
                        />
                    </ListItem>
                )}
                <ListItem>
                    <ListItemText primary="Account" secondary={exportReturn.accountId} />
                </ListItem>
                <ListItem>
                    <ListItemText
                        primary="Outlet"
                        secondary={`${exportReturn.outletNumber} - ${exportReturn.salesOutlet?.name}`}
                    />
                </ListItem>
                <ListItem>
                    <ListItemText primary="Currency" secondary={exportReturn.currency} />
                </ListItem>
                <ListItem>
                    <ListItemText primary="Hub" secondary={`${exportReturn.hubId}`} />
                </ListItem>
                {exportReturn.dateDispatched && (
                    <ListItem>
                        <ListItemText
                            primary="Date Dispatched"
                            secondary={exportReturn.dateDispatched}
                        />
                    </ListItem>
                )}
                <ListItem>
                    <ListItemText primary="Terms" secondary={exportReturn.terms} />
                </ListItem>
            </List>
        </Paper>
    );
}

DisplayOnlyFields.propTypes = {
    exportReturn: PropTypes.shape({
        returnId: PropTypes.number,
        returnForCredit: PropTypes.string,
        raisedBy: PropTypes.shape({
            id: PropTypes.number,
            fullName: PropTypes.string
        }),
        dateCreated: PropTypes.string,
        dateCancelled: PropTypes.string,
        accountId: PropTypes.number,
        outletNumber: PropTypes.number,
        salesOutlet: PropTypes.shape({
            name: PropTypes.string
        }),
        currency: PropTypes.string,
        hubId: PropTypes.number,
        dateDispatched: PropTypes.string,
        terms: PropTypes.string
    }).isRequired
};
