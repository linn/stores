import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import ListItem from '@material-ui/core/ListItem';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Close from '@material-ui/icons/Close';
import { SearchInputField } from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(() => ({
    listItemText: {
        '&:first-child': {
            paddingLeft: 30,
            paddingTop: 0,
            paddingBottom: 0
        }
    },
    paper: {
        backgroundColor: '#f5f5f5',
        position: 'fixed',
        zIndex: 1000,
        paddingTop: '80px',
        width: '100%',
        overflow: 'auto',
        height: '100vh'
    },
    menuItems: {
        fontSize: '12px',
        lineHeight: 2
    },
    closeButton: {
        marginRight: '10px',
        marginTop: '10px',
        float: 'right',
        top: 0,
        right: 0
    },
    searchInputField: {
        float: 'right'
    }
}));

function SearchPanel({ menu, close }) {
    const [searchTerm, setSearchTerm] = useState();
    const classes = useStyles();

    const menuEntries = menu
        .map(s => s.columns)
        .flat()
        .map(c => c.categories)
        .flat()
        .map(i => i.items)
        .flat();

    const uniqueEntries = Object.values(
        menuEntries.reduce((uniques, entry) => {
            if (!uniques[entry.href]) {
                return { ...uniques, [entry.href]: entry };
            }
            return uniques;
        }, {})
    );

    const handleFieldChange = (propertyName, newValue) => {
        setSearchTerm(newValue);
    };
    return (
        <Paper className={classes.paper}>
            <Button onClick={close} color="secondary" className={classes.closeButton}>
                <Close />
            </Button>
            <Grid container>
                <Grid item xs={12} sm={6} md={4} lg={3} xl={3} justify-content="flex-end">
                    <SearchInputField
                        value={searchTerm}
                        onChange={handleFieldChange}
                        textFieldProps={{
                            autoFocus: true
                        }}
                        placeholder="start typing..."
                    />
                    {searchTerm?.length > 1 &&
                        uniqueEntries
                            .filter(
                                e =>
                                    e.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
                                    e.href.toLowerCase().includes(searchTerm.toLowerCase())
                            )
                            .map(entry => (
                                <React.Fragment key={entry.href}>
                                    <a href={entry.href} style={{ textDecoration: 'none' }}>
                                        <ListItem classes={{ root: classes.listItemText }} button>
                                            <Typography
                                                variant="overline"
                                                classes={{
                                                    overline: classes.menuItems
                                                }}
                                                color="primary"
                                            >
                                                {entry.title}
                                            </Typography>
                                        </ListItem>
                                    </a>
                                </React.Fragment>
                            ))}
                </Grid>
            </Grid>
        </Paper>
    );
}

SearchPanel.propTypes = {
    close: PropTypes.func.isRequired,
    menu: PropTypes.arrayOf(PropTypes.shape({})).isRequired
};

export default SearchPanel;
