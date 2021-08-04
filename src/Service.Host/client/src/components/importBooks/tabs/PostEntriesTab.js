import React from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { SingleEditTable } from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';

function PostEntriesTab({ postEntries, handlePostEntryChange }) {
    const updateRow = detail => {
        handlePostEntryChange(detail.lineNumber, detail);
    };

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

    const canEdit = () => {
        //todo check permissions if needed
        return true;
    };

    const columns = [
        {
            title: 'Reference',
            id: 'reference',
            type: 'text',
            editable: canEdit()
        },
        {
            title: 'Prefix',
            id: 'entryCodePrefix',
            type: 'text',
            editable: canEdit()
        },
        {
            title: 'Entry Code',
            id: 'entryCode',
            type: 'text',
            editable: canEdit()
        },
        {
            title: 'Entry Code Date',
            id: 'entryCodeDate',
            type: 'date',
            editable: canEdit()
        },
        {
            title: 'Duty',
            id: 'duty',
            type: 'number',
            editable: canEdit()
        },
        {
            title: 'Vat',
            id: 'vat',
            type: 'number',
            editable: canEdit()
        }
    ];

    return (
        <>
            <Grid container spacing={1} item xs={12} className={classes.gapAbove}>
                <SingleEditTable
                    columns={columns}
                    rows={postEntries ?? [{}]}
                    saveRow={updateRow}
                    // editable={!displayOnly}
                    //todo add createRow function
                    allowNewRowCreation={false}
                />
            </Grid>
        </>
    );
}

PostEntriesTab.propTypes = {
    postEntries: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    handlePostEntryChange: PropTypes.func.isRequired
};

PostEntriesTab.defaultProps = {};

export default PostEntriesTab;
