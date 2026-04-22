import React from 'react';
import PropTypes from 'prop-types';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Close from '@material-ui/icons/Close';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(() => ({
    paper: {
        backgroundColor: '#f5f5f5',
        position: 'fixed',
        zIndex: 1000,
        paddingTop: '80px',
        width: '100%',
        overflow: 'auto',
        height: '100vh'
    },
    closeButton: {
        marginRight: '10px',
        marginTop: '10px',
        float: 'right',
        top: 0,
        right: 0
    },
    listItem: {
        paddingTop: 0,
        paddingBottom: 0,
        margin: 0
    },
    overline: {
        margin: 0,
        lineHeight: 1.8
    },
    showAllButton: {
        marginRight: '10px',
        marginBottom: '10px',
        float: 'right'
    }
}));

function Panel({ section, close }) {
    const { columns } = section;
    const classes = useStyles();
    return (
        <Paper className={classes.paper}>
            <Button onClick={close} color="secondary" className={classes.closeButton}>
                <Close />
            </Button>
            <Grid container>
                {columns.map((col, i) => (
                    //eslint-disable-next-line react/no-array-index-key
                    <Grid item xs={12} sm={6} md={4} lg={3} xl={3} key={i}>
                        {col.categories
                            .filter(e => e.items.filter(item => item.showInMenu).length > 0)
                            .map(category => (
                                <List key={category.title} dense>
                                    <ListItem className={classes.listItem}>
                                        <Typography variant="button" gutterBottom>
                                            {category.title.replace('&amp;', '&')}
                                        </Typography>
                                    </ListItem>
                                    {category.items.map(
                                        entry =>
                                            entry.showInMenu && (
                                                <a
                                                    href={entry.href}
                                                    key={entry.href}
                                                    style={{ textDecoration: 'none' }}
                                                >
                                                    <ListItem className={classes.listItem}>
                                                        <Typography
                                                            className={classes.overline}
                                                            variant="overline"
                                                            color="primary"
                                                        >
                                                            {entry.title}
                                                        </Typography>
                                                    </ListItem>
                                                </a>
                                            )
                                    )}
                                </List>
                            ))}
                    </Grid>
                ))}
                <Grid size={12}>
                    <a href={`/${section.id}`}>
                        <Button onClick={close} color="primary" className={classes.showAllButton}>
                            SHOW ALL OPTIONS...
                        </Button>
                    </a>
                </Grid>
            </Grid>
        </Paper>
    );
}

Panel.propTypes = {
    section: PropTypes.shape({
        id: PropTypes.oneOfType([PropTypes.number, PropTypes.string]),
        columns: PropTypes.arrayOf(PropTypes.shape({}))
    }).isRequired,
    close: PropTypes.func.isRequired
};

export default Panel;
