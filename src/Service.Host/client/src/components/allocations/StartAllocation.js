import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    Dropdown,
    utilities,
    DatePicker,
    OnOffSwitch
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

const useStyles = makeStyles({
    runButton: {
        float: 'right',
        width: '100%'
    }
});

function StartAllocation({
    editStatus,
    itemError,
    loading,
    addItem,
    setEditStatus,
    accountingCompanies,
    stockPools,
    despatchLocations,
    countries
}) {
    const [allocationOptions, setAllocationOptions] = useState({
        accountingCompany: 'LINN',
        despatchLocation: 'LINN',
        stockPool: 'LINN',
        excludeUnsuppliableLines: true,
        excludeOverCreditLimit: true,
        excludeOnHold: true,
        cutOffDate: new Date().toISOString()
    });

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const classes = useStyles();

    const handleSaveClick = () => {
        if (creating()) {
            addItem(allocationOptions);
            setEditStatus('view');
        }
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
                        <Grid item xs={4}>
                            <InputField
                                value={allocationOptions.articleNumber}
                                label="Article Number"
                                onChange={handleFieldChange}
                                propertyName="articleNumber"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                value={allocationOptions.accountId}
                                label="Account Id"
                                type="number"
                                onChange={handleFieldChange}
                                propertyName="accountId"
                            />
                        </Grid>
                        <Grid item xs={4} />
                        <Grid item xs={4}>
                            <OnOffSwitch
                                label="Exclude Unsuppliable Lines"
                                value={allocationOptions.excludeUnsuppliableLines}
                                onChange={() => {
                                    handleFieldChange(
                                        'excludeUnsuppliableLines',
                                        !allocationOptions.excludeUnsuppliableLines
                                    );
                                }}
                                propertyName="excludeUnsuppliableLines"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <OnOffSwitch
                                label="Exclude On Hold"
                                value={allocationOptions.excludeOnHold}
                                onChange={() => {
                                    handleFieldChange(
                                        'excludeOnHold',
                                        !allocationOptions.excludeOnHold
                                    );
                                }}
                                propertyName="excludeOnHold"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <OnOffSwitch
                                label="Exclude Over Credit Limit"
                                value={allocationOptions.excludeOverCreditLimit}
                                onChange={() => {
                                    handleFieldChange(
                                        'excludeOverCreditLimit',
                                        !allocationOptions.excludeOverCreditLimit
                                    );
                                }}
                                propertyName="excludeOverCreditLimit"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <DatePicker
                                label="Cut Off Date"
                                value={
                                    allocationOptions.cutOffDate
                                        ? allocationOptions.cutOffDate
                                        : null
                                }
                                onChange={value => {
                                    handleFieldChange('cutOffDate', value);
                                }}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={6} />
                        <Grid item xs={2}>
                            <Button
                                className={classes.runButton}
                                disabled={viewing()}
                                onClick={handleSaveClick}
                                variant="contained"
                                color="primary"
                            >
                                Run Allocation
                            </Button>
                        </Grid>
                        <Grid item xs={4} />
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
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired
};

StartAllocation.defaultProps = {
    addItem: null,
    loading: null,
    itemError: null,
    accountingCompanies: [],
    stockPools: [],
    countries: [],
    despatchLocations: []
};

export default StartAllocation;
