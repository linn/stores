import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    Dropdown
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function StartAllocation({
    editStatus,
    itemError,
    history,
    loading,
    addItem,
    setEditStatus,
    snackbarVisible,
    accountingCompanies
}) {
    const [allocationOptions, setAllocationOptions] = useState({ accountingCompany: 'LINN' });

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

    const accountingCompanyOptions = () => {
        return accountingCompanies
            ?.map(c => ({
                id: c.name,
                displayText: c.name
            }));
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
                            <Grid item xs={4}>
                                <Dropdown
                                    label="Company"
                                    propertyName="accountingCompany"
                                    items={accountingCompanyOptions()}
                                    fullWidth
                                    value={allocationOptions.accountingCompany}
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={8}>
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
    accountingCompanies: PropTypes.arrayOf(PropTypes.shape({})),
    snackbarVisible: PropTypes.bool,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired
};

StartAllocation.defaultProps = {
    snackbarVisible: false,
    addItem: null,
    loading: null,
    itemError: null,
    accountingCompanies: []
};

export default StartAllocation;
