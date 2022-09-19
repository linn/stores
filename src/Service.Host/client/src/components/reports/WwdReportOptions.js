import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { Dropdown, Title, InputField, Typeahead } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function WwdReportOptions({
    history,
    searchParts,
    clearPartsSearch,
    partsSearchLoading,
    partsSearchResults
}) {
    const [reportOptions, setReportOptions] = useState({
        partNumber: '',
        quantity: 1,
        typeOfRun: 'SHORTAGES ONLY'
    });

    const typeOfRunOptions = ['ALL PARTS', 'SHORTAGES ONLY'];

    const handleFieldChange = (propertyName, newValue) => {
        setReportOptions({ ...reportOptions, [propertyName]: newValue });
    };

    const handlePartSelect = part => {
        setReportOptions({
            ...reportOptions,
            partNumber: part.partNumber,
            partDescription: part.description
        });
    };

    const validateReportOptions = () =>
        !!reportOptions.partNumber && !!reportOptions.typeOfRun && reportOptions.quantity >= 1;

    const handleRunClick = () => {
        let searchString = `?partNumber=${reportOptions.partNumber}&quantity=${reportOptions.quantity}&typeOfRun=${reportOptions.typeOfRun}`;

        if (reportOptions.workStationCode) {
            searchString += `&workStationCode=${reportOptions.workStationCode}`;
        }

        history.push({
            pathname: '/inventory/reports/what-will-decrement/report',
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
            <Title text="What Will Decrement Report" />

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
                <Grid item xs={4}>
                    <InputField
                        label="Quantity"
                        fullWidth
                        value={reportOptions.quantity}
                        onChange={handleFieldChange}
                        propertyName="quantity"
                        type="number"
                        required
                    />
                </Grid>
                <Grid item xs={8} />
                <Grid item xs={4}>
                    <Dropdown
                        label="Type of run"
                        onChange={handleFieldChange}
                        fullWidth
                        propertyName="typeOfRun"
                        value={reportOptions.typeOfRun}
                        items={typeOfRunOptions}
                        required
                    />
                </Grid>
                <Grid item xs={8} />
                <Grid item xs={4}>
                    <InputField
                        label="Workstation"
                        fullWidth
                        value={reportOptions.workstationCode}
                        onChange={handleFieldChange}
                        propertyName="workStationCode"
                    />
                </Grid>
                <Grid item xs={8} />
                <Grid item xs={12}>
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

WwdReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired,
    partsSearchLoading: PropTypes.bool,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({}))
};

WwdReportOptions.defaultProps = {
    partsSearchLoading: false,
    partsSearchResults: []
};
