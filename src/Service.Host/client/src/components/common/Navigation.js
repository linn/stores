import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import AppBar from '@material-ui/core/AppBar';
import { utilities } from '@linn-it/linn-form-components-library';
import ClickAwayListener from '@material-ui/core/ClickAwayListener';
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import Toolbar from '@material-ui/core/Toolbar';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Badge from '@material-ui/core/Badge';
import Button from '@material-ui/core/Button';
import { useSnackbar } from 'notistack';
import AccountCircle from '@material-ui/icons/AccountCircle';
import Search from '@material-ui/icons/Search';
import Notifications from '@material-ui/icons/Notifications';
import Panel from './Panel';
import SearchPanel from './SearchPanel';

const useStyles = makeStyles(theme => ({
    root: {
        width: '100%',
        zIndex: 10
    },
    tabLabel: {
        fontSize: theme.typography.fontSize,
        color: theme.palette.grey[200]
    },
    snackbarNew: {
        background: theme.palette.primary.dark,
        width: '800px'
    },
    snackbarSeen: {
        width: '800px'
    },
    panel: {
        position: 'relative'
    },
    menuButton: {
        marginLeft: -12,
        marginRight: 20
    },
    tab: {
        minWidth: '100px'
    },
    toolbar: {
        paddingLeft: 0,
        paddingRight: 0
    },
    tabs: {
        paddingLeft: 40
    },
    container: {
        width: '100%',
        margin: 0
    },
    appBar: {
        backgroundColor: theme.palette.grey[800],
        width: '100% !important',
        margin: 0
    },
    icons: {
        cursor: 'pointer',
        color: 'white'
    }
}));

function Navigation({
    sections,
    loading,
    username,
    myStuff,
    seenNotifications,
    unseenNotifications,
    markNotificationSeen,
    handleSignOut
}) {
    const classes = useStyles();
    const [selected, setSelected] = useState(false);
    const [anchorEl, setAnchorEl] = useState();
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();

    if (loading) {
        return (
            <div className={classes.root}>
                <AppBar position="fixed" color="default">
                    <Toolbar classes={{ gutters: classes.toolbar }} />
                </AppBar>
                <div className={classes.offset} />
            </div>
        );
    }

    if (sections) {
        const menuIds = sections.map(item => item.id);

        const handleClick = event => {
            setAnchorEl(event.currentTarget);
        };
        const handleClose = () => {
            setAnchorEl();
        };

        const actions = (key, e) => (
            <>
                <Button
                    variant="contained"
                    onClick={() => {
                        window.location = utilities.getSelfHref(e);
                    }}
                >
                    View
                </Button>
                <Button
                    onClick={() => {
                        closeSnackbar(key);
                        localStorage.setItem(e.title, e.content);
                        markNotificationSeen(e);
                    }}
                >
                    Dismiss
                </Button>
            </>
        );

        const noNotifications = () => {
            if (!seenNotifications && !unseenNotifications) {
                return true;
            }
            return seenNotifications.length + unseenNotifications.length === 0;
        };

        const queueNotifications = () => {
            if (noNotifications()) {
                enqueueSnackbar('No notifications to show!', {
                    anchorOrigin: {
                        vertical: 'bottom',
                        horizontal: 'right'
                    },
                    variant: 'info',
                    preventDuplicate: true
                });
            } else {
                unseenNotifications.concat(seenNotifications).forEach((e, i) => {
                    setTimeout(() => {
                        enqueueSnackbar(`${e.title} ${e.content}`, {
                            anchorOrigin: {
                                vertical: 'bottom',
                                horizontal: 'right'
                            },
                            ContentProps: {
                                classes: {
                                    root: localStorage.getItem(e.title)
                                        ? classes.snackbarSeen
                                        : classes.snackbarNew
                                }
                            },
                            action: key => actions(key, e),
                            preventDuplicate: true
                        });
                    }, i * 200);
                });
            }
        };

        return (
            <>
                <ClickAwayListener onClickAway={() => setSelected(false)}>
                    <div className="hide-when-printing">
                        <div className={classes.root}>
                            <AppBar position="fixed" classes={{ root: classes.appBar }}>
                                <Toolbar classes={{ gutters: classes.toolbar }}>
                                    <Grid
                                        container
                                        alignItems="center"
                                        justifyContent="space-between"
                                        spacing={0}
                                        classes={{ container: classes.container }}
                                    >
                                        <Grid item xs={9}>
                                            <Tabs
                                                classes={{
                                                    root: classes.tabs
                                                }}
                                                value={selected}
                                                onChange={(event, value) => {
                                                    if (selected === value) {
                                                        setSelected(false);
                                                    } else {
                                                        setSelected(value);
                                                    }
                                                }}
                                                scrollButtons="auto"
                                                variant="scrollable"
                                                indicatorColor="primary"
                                                textColor="primary"
                                            >
                                                {sections.map(item => (
                                                    <Tab
                                                        id={item.id}
                                                        key={item.id}
                                                        classes={{ root: classes.tab }}
                                                        label={
                                                            <span className={classes.tabLabel}>
                                                                {item.title}
                                                            </span>
                                                        }
                                                        selected={false}
                                                    />
                                                ))}
                                            </Tabs>
                                        </Grid>
                                        <Grid item xs={1}>
                                            <Typography variant="h4">
                                                <AccountCircle
                                                    className={classes.icons}
                                                    aria-owns={anchorEl ? 'simple-menu' : undefined}
                                                    onClick={handleClick}
                                                    id={sections.length}
                                                    key={sections.length}
                                                />
                                            </Typography>
                                        </Grid>
                                        <Grid item xs={1}>
                                            <Typography variant="h4">
                                                <Badge
                                                    badgeContent={
                                                        unseenNotifications
                                                            ? unseenNotifications.length
                                                            : 0
                                                    }
                                                    color="primary"
                                                    variant="dot"
                                                >
                                                    <Notifications
                                                        className={classes.icons}
                                                        onClick={queueNotifications}
                                                    />
                                                </Badge>
                                            </Typography>
                                        </Grid>
                                        <Grid item xs={1}>
                                            <Typography variant="h4">
                                                <Search
                                                    className={classes.icons}
                                                    onClick={() => setSelected(sections.length)}
                                                />
                                            </Typography>
                                        </Grid>
                                        <Menu
                                            id="simple-menu"
                                            anchorEl={anchorEl}
                                            open={Boolean(anchorEl)}
                                            onClose={handleClose}
                                        >
                                            <MenuItem onClick={handleClose}>{username}</MenuItem>
                                            {username &&
                                                myStuff.groups.map(item => (
                                                    <span key={item.items[0].href}>
                                                        <a href={item.items[0].href}>
                                                            <MenuItem onClick={handleClose}>
                                                                {item.items[0].title}
                                                            </MenuItem>
                                                        </a>
                                                    </span>
                                                ))}
                                            {handleSignOut && (
                                                <MenuItem
                                                    style={{
                                                        color: 'blue',
                                                        textDecoration: 'underline',
                                                        cursor: 'pointer',
                                                        background: 'none'
                                                    }}
                                                    onClick={handleSignOut}
                                                >
                                                    Sign Out (Newer apps pages)
                                                </MenuItem>
                                            )}
                                        </Menu>
                                    </Grid>
                                </Toolbar>
                            </AppBar>
                            <div className={classes.offset} />
                            <Grid container item spacing={3}>
                                <Grid item />
                                {menuIds.map(
                                    (item, i) =>
                                        selected === i && (
                                            <Panel
                                                key={item}
                                                section={sections.find(e => e.id === item)}
                                                id={item}
                                                style={{ align: 'right' }}
                                                anchorEl={item.id}
                                                close={() => setSelected(false)}
                                            />
                                        )
                                )}
                                {selected === sections.length && (
                                    <SearchPanel menu={sections} close={() => setSelected(false)} />
                                )}
                            </Grid>
                        </div>
                    </div>
                </ClickAwayListener>
            </>
        );
    }
    return (
        <div className={classes.root}>
            <AppBar position="fixed" color="default">
                <Toolbar classes={{ gutters: classes.toolbar }} />
            </AppBar>
            <div className={classes.offset} />
        </div>
    );
}

Navigation.propTypes = {
    sections: PropTypes.arrayOf(PropTypes.shape({})),
    loading: PropTypes.bool,
    username: PropTypes.string,
    myStuff: PropTypes.shape({ groups: PropTypes.arrayOf(PropTypes.shape({})) }),
    seenNotifications: PropTypes.arrayOf(PropTypes.shape({})),
    unseenNotifications: PropTypes.arrayOf(PropTypes.shape({})),
    markNotificationSeen: PropTypes.func.isRequired,
    handleSignOut: PropTypes.func
};

Navigation.defaultProps = {
    sections: null,
    myStuff: null,
    seenNotifications: [],
    unseenNotifications: [],
    loading: false,
    username: '',
    handleSignOut: null
};

export default Navigation;
