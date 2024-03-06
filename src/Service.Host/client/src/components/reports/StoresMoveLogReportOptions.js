import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { DatePicker, Title, InputField, Typeahead } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function StoresMoveLogReportOptions({
    history,
    searchParts,
    clearPartsSearch,
    partsSearchLoading,
    partsSearchResults,
    stockPools,
    stockPoolsLoading,
    searchStockPools,
    clearStockPoolsSearch
}) {
    const defaultDate = new Date();

    const [reportOptions, setReportOptions] = useState({
        partNumber: '',
        from: defaultDate,
        to: defaultDate,
        location: '',
        transType: '',
        stockPool: ''
    });

    const handleFieldChange = (propertyName, newValue) => {
        setReportOptions({ ...reportOptions, [propertyName]: newValue });
    };

    const setFromDate = newDate => {
        handleFieldChange('from', newDate);
    };

    const setToDate = newDate => {
        handleFieldChange('to', newDate);
    };

    const handlePartSelect = part => {
        setReportOptions({
            ...reportOptions,
            partNumber: part.partNumber,
            partDescription: part.description
        });
    };

    const validateReportOptions = () => !!reportOptions.partNumber;

    const handleRunClick = () => {
        let searchString = `?partNumber=${
            reportOptions.partNumber
        }&from=${reportOptions.from.toISOString()}&to=${reportOptions.to.toISOString()}`;

        if (reportOptions.location) {
            searchString += `&location=${reportOptions.location}`;
        }

        if (reportOptions.transType) {
            searchString += `&transType=${reportOptions.transType}`;
        }

        if (reportOptions.stockPool) {
            searchString += `&stockPool=${reportOptions.stockPool}`;
        }

        history.push({
            pathname: '/inventory/reports/stores-move-log/report',
            search: searchString
        });
    };

    const getItems = () => {
        return partsSearchResults?.map(p => ({
            ...p,
            name: p.partNumber,
            description: p.description,
            id: p.partNumber
        }));
    };

    return (
        <Page>
            <Title text="Stores Move Log" />

            <Grid style={{ marginTop: 40 }} container spacing={3} justifyContent="center">
                <Grid item xs={4}>
                    <InputField
                        label="Part"
                        maxLength={14}
                        fullWidth
                        value={reportOptions.partNumber}
                        onChange={handleFieldChange}
                        propertyName="partNumber"
                        required
                    />
                </Grid>
                <Grid item xs={1}>
                    <Typeahead
                        items={getItems()}
                        fetchItems={searchParts}
                        clearSearch={clearPartsSearch}
                        loading={partsSearchLoading}
                        debounce={1000}
                        links={false}
                        modal
                        searchButtonOnly
                        onSelect={handlePartSelect}
                        label="Part"
                    />
                </Grid>
                <Grid item xs={7}>
                    <InputField
                        fullWidth
                        disabled
                        propertyName="description"
                        value={reportOptions.partDescription}
                        label="Description"
                    />
                </Grid>
                <Grid item xs={6}>
                    <DatePicker
                        label="From Date"
                        value={reportOptions.from.toString()}
                        onChange={setFromDate}
                    />
                </Grid>
                <Grid item xs={6}>
                    <DatePicker
                        label="To Date"
                        value={reportOptions.to.toString()}
                        minDate={reportOptions.from.toString()}
                        onChange={setToDate}
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        fullWidth
                        propertyName="location"
                        value={reportOptions.location}
                        label="Location"
                        onChange={handleFieldChange}
                    />
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        fullWidth
                        propertyName="transType"
                        value={reportOptions.transType}
                        label="Trans Type"
                        onChange={handleFieldChange}
                    />
                </Grid>
                <Grid item xs={2}>
                    <InputField
                        fullWidth
                        propertyName="stockPool"
                        value={reportOptions.stockPool}
                        label="Stock Pool"
                        onChange={handleFieldChange}
                    />
                </Grid>
                <Grid item xs={1}>
                    <Typeahead
                        items={stockPools}
                        fetchItems={searchStockPools}
                        modal
                        searchButtonOnly
                        links={false}
                        clearSearch={clearStockPoolsSearch}
                        loading={stockPoolsLoading}
                        label="Stock Pool"
                        title="Search Stock Pools"
                        value={reportOptions.stockPool}
                        onSelect={newValue =>
                            setReportOptions({
                                ...reportOptions,
                                stockPool: newValue.stockPoolCode
                            })
                        }
                        history={history}
                        debounce={1000}
                        minimumSearchTermLength={2}
                    />
                </Grid>
                <Grid item xs={3}>
                    <Button
                        color="primary"
                        variant="contained"
                        style={{ float: 'right' }}
                        onClick={handleRunClick}
                        disabled={!validateReportOptions()}
                    >
                        Run Report
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
}

StoresMoveLogReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired,
    partsSearchLoading: PropTypes.bool,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    stockPools: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string
        })
    ),
    stockPoolsLoading: PropTypes.bool,
    searchStockPools: PropTypes.func.isRequired,
    clearStockPoolsSearch: PropTypes.func.isRequired
};

StoresMoveLogReportOptions.defaultProps = {
    partsSearchLoading: false,
    partsSearchResults: [],
    stockPools: [],
    stockPoolsLoading: false
};
