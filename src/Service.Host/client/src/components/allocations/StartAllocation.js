import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function StartAllocation({
    editStatus,
    itemError,
    history,
    loading,
    addItem,
    setEditStatus,
    snackbarVisible
}) {
    const [allocationOptions, setAllocationOptions] = useState({});

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';

    const handleSaveClick = () => {
        if (creating()) {
            addItem(allocationOptions);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
    };

    const handleBackClick = () => {
        history.push('/logistics/allocations');
    };

    const handleFieldChange = (propertyName, newValue) => {
        setAllocationOptions({ ...allocationOptions, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Allocation Options" />
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                )}
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                        <>
                            <Grid item xs={8}>
                                <InputField
                                    fullWidth
                                    value={allocationOptions.description}
                                    label="Description"
                                    maxLength={10}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="description"
                                />
                            </Grid>
                            <Grid itemx xs={4}>
                                <InputField
                                    value={allocationOptions.accountingCompany}
                                    label="Company"
                                    propertyName="accountingCompany"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing()}
                                    saveClick={handleSaveClick}
                                    cancelClick={handleCancelClick}
                                    backClick={handleBackClick}
                                />
                            </Grid>
                        </>
                )}
            </Grid>
        </Page>
    );
}

StartAllocation.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    snackbarVisible: PropTypes.bool,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired
};

StartAllocation.defaultProps = {
    snackbarVisible: false,
    addItem: null,
    loading: null,
    itemError: null
};

export default StartAllocation;
