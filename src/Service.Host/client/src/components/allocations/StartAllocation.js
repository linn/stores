import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    Dropdown,
    SnackbarMessage,
    utilities
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
    accountingCompanies,
    setSnackbarVisible,
    stockPools,
    despatchLocations,
    countries
}) {
    const [allocationOptions, setAllocationOptions] = useState({
        accountingCompany: 'LINN',
        despatchLocation: 'LINN',
        stockPool: 'LINN'
    });

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';

    const handleSaveClick = () => {
        if (creating()) {
            addItem(allocationOptions);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {};

    const handleBackClick = () => {
        history.push('/logistics/allocations');
    };

    const handleFieldChange = (propertyName, newValue) => {
        setAllocationOptions({ ...allocationOptions, [propertyName]: newValue });
    };

    const accountingCompanyOptions = () => {
        return accountingCompanies?.map(c => ({
            id: c.name,
            displayText: c.name
        }));
    };

    const stockPoolOptions = () => {
        return stockPools?.map(c => ({
            id: c.stockPoolCode,
            displayText: c.stockPoolCode
        }));
    };

    const despatchLocationOptions = () => {
        return despatchLocations?.map(c => ({
            id: c.locationCode,
            displayText: c.locationCode
        }));
    };

    const countryOptions = () => {
        return utilities.sortEntityList(countries, 'displayName')?.map(c => ({
            id: c.countryCode,
            displayText: c.displayName
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
                        <SnackbarMessage
                            visible={snackbarVisible}
                            onClose={() => setSnackbarVisible(false)}
                            message="Allocation Successful"
                        />
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
                        <Grid item xs={4}>
                            <Dropdown
                                label="Stock Pool"
                                propertyName="stockPool"
                                items={stockPoolOptions()}
                                fullWidth
                                value={allocationOptions.stockPool}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={4} />
                        <Grid item xs={4}>
                            <Dropdown
                                label="Despatch Location"
                                propertyName="despatchLocation"
                                items={despatchLocationOptions()}
                                fullWidth
                                value={allocationOptions.despatchLocation}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <Dropdown
                                label="Country"
                                propertyName="countryCode"
                                items={countryOptions()}
                                fullWidth
                                value={allocationOptions.countryCode}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={4} />
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
    countries: PropTypes.arrayOf(PropTypes.shape({})),
    despatchLocations: PropTypes.arrayOf(PropTypes.shape({})),
    stockPools: PropTypes.arrayOf(PropTypes.shape({})),
    snackbarVisible: PropTypes.bool,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

StartAllocation.defaultProps = {
    snackbarVisible: false,
    addItem: null,
    loading: null,
    itemError: null,
    accountingCompanies: [],
    stockPools: [],
    countries: [],
    despatchLocations: []
};

export default StartAllocation;
