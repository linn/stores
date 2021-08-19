import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { TableWithInlineEditing } from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';

function PostEntriesTab({ postEntries, updatePostEntries, allowedToEdit }) {
    const useStyles = makeStyles(theme => ({
        displayInline: {
            display: 'inline'
        },
        marginTop1: {
            marginTop: theme.spacing(1),
            display: 'inline-block',
            width: '2em'
        },
        gapAbove: {
            marginTop: theme.spacing(8)
        },
        negativeTopMargin: {
            marginTop: theme.spacing(-4)
        }
    }));
    const classes = useStyles();

    const columns = [
        {
            title: 'Reference',
            key: 'reference',
            type: 'text'
        },
        {
            title: 'Prefix',
            key: 'entryCodePrefix',
            type: 'text'
        },
        {
            title: 'Entry Code',
            key: 'entryCode',
            type: 'text'
        },
        {
            title: 'Entry Code Date',
            key: 'entryCodeDate',
            type: 'date'
        },
        {
            title: 'Duty',
            key: 'duty',
            type: 'number'
        },
        {
            title: 'Vat',
            key: 'vat',
            type: 'number'
        }
    ];

    return (
        <>
            <Grid container spacing={1} item xs={12} className={classes.gapAbove}>
                <TableWithInlineEditing
                    columnsInfo={columns}
                    content={postEntries ?? [{}]}
                    updateContent={updatePostEntries}
                    allowedToEdit={!allowedToEdit}
                    allowedToCreate={!allowedToEdit}
                    allowedToDelete={!allowedToEdit}
                />
            </Grid>
        </>
    );
}

PostEntriesTab.propTypes = {
    postEntries: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    updatePostEntries: PropTypes.func.isRequired,
    allowedToEdit: PropTypes.bool.isRequired
};

PostEntriesTab.defaultProps = {};

export default PostEntriesTab;
