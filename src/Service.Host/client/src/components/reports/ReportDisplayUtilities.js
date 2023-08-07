import React, { Fragment } from 'react';
import numeral from 'numeral';
import Button from '@material-ui/core/Button';
import Link from '@material-ui/core/Link';
import Modal from '@material-ui/core/Modal';
import { Link as RouterLink } from 'react-router-dom';
import makeStyles from '@material-ui/styles/makeStyles';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';

const useStyles = makeStyles(theme => ({
    link: {
        textDecoration: 'none',
        color: theme.palette.primary.main,
        padding: 0,
        '&:hover': {
            cursor: 'pointer',
            textDecoration: 'underline',
            background: 'transparent'
        }
    }
}));

function LinkOrAnchor({ hasExternalLinks, text, href }) {
    const classes = useStyles();

    if (hasExternalLinks) {
        return (
            <a style={{ textDecoration: 'none' }} href={href}>
                <Button size="small" className={classes.link}>
                    {text}
                </Button>
            </a>
        );
    }

    return (
        <Link className={classes.link} component={RouterLink} to={href}>
            {' '}
            {text}{' '}
        </Link>
    );
}

export const format = (i, prefix, suffix, decimalPlaces) => {
    let decimalPlaceTemplate;

    if (!decimalPlaces || decimalPlaces === '0') {
        decimalPlaceTemplate = '';
    } else {
        decimalPlaceTemplate = `.${Array(decimalPlaces + 1).join('0')}`;
    }

    if (i || i === 0) {
        return (prefix || '') + numeral(i).format(`0,0${decimalPlaceTemplate}`) + (suffix || '');
    }

    return null;
};

export const setDrilldown = (item, hasExternalLinks) => {
    let displayItem;
    let href;
    let externalLink;
    if (item && item.displayString) {
        displayItem = item.displayString;
        if (item.drillDowns.length > 0) {
            href = item.drillDowns[0].href;
            externalLink = item.drillDowns[0].externalLink;
        }
    } else {
        displayItem = item;
    }
    const text = displayItem;
    if (href) {
        return LinkOrAnchor({ hasExternalLinks: externalLink ?? hasExternalLinks, text, href });
    }

    return displayItem;
};

export const formatTitle = (title, showTitle, loading, error, helpText) => {
    if (error) {
        return <strong>Error</strong>;
    }

    if (!showTitle) {
        return '';
    }

    let displayTitle;
    if (title && title.displayString) {
        displayTitle = title.displayString;
    } else {
        displayTitle = title;
    }

    if (loading) {
        return <Typography variant="subtitle1">{`${displayTitle} (loading)`}</Typography>;
    }

    return (
        <>
            <div>
                <Typography variant="subtitle1">{setDrilldown(title)}</Typography>
            </div>
            {helpText ? (
                <div>
                    <Modal
                        aria-labelledby="simple-modal-title"
                        aria-describedby="simple-modal-description"
                    >
                        {helpText}
                    </Modal>
                </div>
            ) : (
                ''
            )}
        </>
    );
};

export const setValueDrilldown = (value, hasExternalLinks) => {
    let displayItem;
    if (value && (value.displayValue || value.displayValue === 0)) {
        if (value.drillDowns && value.drillDowns.length > 0) {
            const text = format(
                value.displayValue,
                value.prefix,
                value.suffix,
                value.decimalPlaces
            );
            const { href, externalLink } = value.drillDowns[0];
            displayItem = LinkOrAnchor({
                hasExternalLinks: externalLink ?? hasExternalLinks,
                href,
                text
            });
        } else {
            displayItem = format(
                value.displayValue,
                value.prefix,
                value.suffix,
                value.decimalPlaces
            );
        }
    } else {
        displayItem = null;
    }

    return displayItem;
};

export const setTextValueDrilldown = (value, hasExternalLinks) => {
    let displayItem;
    if (value && value.textDisplayValue) {
        if (value.drillDowns && value.drillDowns.length > 0) {
            const text = value.textDisplayValue;
            const { href, externalLink } = value.drillDowns[0];
            displayItem = LinkOrAnchor({
                hasExternalLinks: externalLink ?? hasExternalLinks,
                text,
                href
            });
        } else {
            displayItem = value.textDisplayValue;
        }
    } else {
        displayItem = null;
    }

    return displayItem;
};

export const formatHeading = (title, showTitle, loading, error) => {
    if (!showTitle) {
        return false;
    }

    if (error) {
        return <strong>Error</strong>;
    }

    let displayTitle;
    if (title && title.displayString) {
        displayTitle = title.displayString;
    } else {
        displayTitle = title;
    }

    if (loading) {
        return <h5>{`${displayTitle} (loading)`}</h5>;
    }

    return <strong>{displayTitle}</strong>;
};

export const displayError = message => <h5 className="error-message">{message}</h5>;

LinkOrAnchor.propTypes = {
    hasExternalLinks: PropTypes.bool,
    text: PropTypes.string.isRequired,
    href: PropTypes.string.isRequired
};

LinkOrAnchor.defaultProps = {
    hasExternalLinks: false
};
