import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { DatePicker, Title, OnOffSwitch } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function ImpbookEuReportOptions({ history, prevOptions }) {
    const defaultStartDate = new Date();
    defaultStartDate.setDate(defaultStartDate.getDate() - 90);
    const [fromDate, setFromDate] = useState(
        prevOptions.fromDate ? new Date(prevOptions.fromDate) : defaultStartDate
    );
    const [toDate, setToDate] = useState(
        prevOptions.toDate ? new Date(prevOptions.toDate) : new Date()
    );
    const [euResults, setEuResults] = useState(
        prevOptions.euResults ? prevOptions.euResults : true
    );

    const handleClick = () =>
        history.push({
            pathname: `/logistics/import-books/eu/report`,
            search: `?fromDate=${fromDate.toISOString()}&toDate=${toDate.toISOString()}&euResults=${euResults}`
        });

    return (
        <Page>
            <Title text="EU Import Books" />
            <Grid style={{ marginTop: 40 }} container spacing={3} justifyContent="center">
                <Grid item xs={12}>
                    <Typography variant="h6" gutterBottom>
                        Choose a date range:
                    </Typography>
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        label="From Date"
                        value={fromDate.toString()}
                        onChange={setFromDate}
                    />
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        label="To Date"
                        value={toDate.toString()}
                        minDate={fromDate.toString()}
                        onChange={setToDate}
                    />
                </Grid>
                <Grid item xs={6}>
                    <OnOffSwitch
                        label="Show EU (off means show ROW)"
                        onChange={() => setEuResults(!euResults)}
                        propertyName="showIpr"
                        value={euResults}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Button
                        color="primary"
                        variant="contained"
                        disabled={!fromDate && !toDate}
                        onClick={handleClick}
                    >
                        Run Report
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
}

ImpbookEuReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string,
        euResults: PropTypes.bool
    }).isRequired
};

export default ImpbookEuReportOptions;
