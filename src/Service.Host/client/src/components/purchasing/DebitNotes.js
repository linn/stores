import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    Title,
    SingleEditTable,
    Loading,
    SnackbarMessage,
    ErrorCard,
    BackButton
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function DebitNoteUtility({
    items,
    itemsLoading,
    itemsError,
    updateDebitNote,
    updateDebitNoteError,
    debitNoteLoading,
    snackbarVisible,
    setSnackbarVisible
}) {
    //const [prevDebitNotes, setPrevDebitNotes] = useState(null);
    const [debitNotes, setDebitNotes] = useState([]);

    // useEffect(() => {
    //     if (items !== prevDebitNotes) {
    //         if (items) {
    //             setPrevDebitNotes(items);
    //             setDebitNotes(items);
    //         }
    //     }
    // }, [items, debitNotes, prevDebitNotes, options]);

    const handleValueChange = (item, propertyName, newValue) => {
        setDebitNotes(s =>
            s.map(row => (row.id === item.id ? { ...row, [propertyName]: newValue } : row))
        );
    };

    const columns = [
        // {
        //     title: 'Storage Place',
        //     id: 'storagePlaceName',
        //     type: 'search',
        //     editable: true,
        //     search: searchStoragePlaces,
        //     clearSearch: clearStoragePlacesSearch,
        //     searchResults: storagePlaces,
        //     searchLoading: storagePlacesLoading,
        //     selectSearchResult: selectStoragePlaceSearchResult,
        //     searchTitle: 'Search Storage Places',
        //     minimumSearchTermLength: 3,
        //     required: true
        // },
        {
            title: '#',
            id: 'noteNumber',
            type: 'text',
            editable: false
        },
        // {
        //     title: 'Batch Ref',
        //     id: 'batchRef',
        //     type: 'text',
        //     required: false,
        //     editable: true
        // },
        // {
        //     title: 'Batch Date',
        //     id: 'stockRotationDate',
        //     type: 'date',
        //     editable: true
        // },
        // {
        //     title: 'Qty',
        //     id: 'quantity',
        //     type: 'number',
        //     editable: true
        // },
        // {
        //     title: 'Remarks',
        //     id: 'remarks',
        //     textFieldRows: 3,
        //     type: 'text',
        //     editable: true
        // },
        // {
        //     title: 'Audit Dept',
        //     id: 'auditDepartmentCode',
        //     type: 'search',
        //     editable: true,
        //     search: searchDepartments,
        //     clearSearch: clearDepartmentsSearch,
        //     searchResults: departments,
        //     searchLoading: departmentsLoading,
        //     selectSearchResult: selectDepartmentsSearchResult,
        //     searchTitle: 'Search Departments',
        //     minimumSearchTermLength: 3,
        //     required: false
        // }
    ];
    return (
        <Page>
            <SnackbarMessage
                visible={snackbarVisible}
                onClose={() => setSnackbarVisible(false)}
                message="Save Successful"
            />
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Open Debit Notes" />
                </Grid>
                {/* {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard
                            errorMessage={itemError?.details?.errors?.[0] || itemError.statusText}
                        />
                    </Grid>
                )} */}
                {itemsLoading || debitNoteLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <Grid item xs={12}>
                        {debitNotes && (
                            <SingleEditTable
                                newRowPosition="top"
                                columns={columns}
                                rows={items}
                                saveRow={item => {
                                    const body = item;
                                    if (!body.partNumber) {
                                        body.partNumber = items?.find(
                                            l => l.partNumber
                                        ).partNumber;
                                    }
                                    updateDebitNote(body.id, body);
                                }}
                                updateRow={(item, _setItem, propertyName, newValue) => {
                                    handleValueChange(item, propertyName, newValue);
                                }}
                                editable
                                allowNewRowCreations={false}
                            />
                        )}
                    </Grid>
                )}
                {/* <Grid item xs={12}>
                    <BackButton backClick={() => history.push('/inventory/dept-stock-parts')} />
                </Grid> */}
            </Grid>
        </Page>
    );
}

DebitNoteUtility.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            
        })
    ),
    itemsLoading: PropTypes.bool,
    debitNoteLoading: PropTypes.bool,
    options: PropTypes.shape({ partNumber: PropTypes.string }).isRequired,
    snackbarVisible: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        item: PropTypes.string,
        details: PropTypes.shape({
            errors: PropTypes.arrayOf(PropTypes.shape({}))
        })
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired
};

DebitNoteUtility.defaultProps = {
    itemsLoading: false,
    items: [],
    debitNoteLoading: false,
    snackbarVisible: false,
    itemError: null
};

export default DebitNoteUtility;
