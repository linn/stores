import React from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function TqmsSummaryByCategoryReportOptions({ history }) {
    const handleRunClick = () => {
        const searchString = `?jobref=AABCFB`;

        history.push({
            pathname: '/inventory/tqms-category-summary/report',
            search: searchString
        });
    };

    return (
        <Page>
            <Title text="TQMS Summary Report" />

            <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Button
                        color="primary"
                        variant="contained"
                        style={{ float: 'right' }}
                        onClick={handleRunClick}
                    >
                        Run Report
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
}

TqmsSummaryByCategoryReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired
};

TqmsSummaryByCategoryReportOptions.defaultProps = {};
