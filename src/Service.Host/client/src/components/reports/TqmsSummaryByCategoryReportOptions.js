import React, { useState, useEffect } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { Title, Dropdown, Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import moment from 'moment';
import Page from '../../containers/Page';

export default function TqmsSummaryByCategoryReportOptions({ history, jobRefs, jobRefsLoading }) {
    const [jobRef, setJobRef] = useState(null);
    const [runType, setRunType] = useState('Headings Only');

    useEffect(() => {
        if (jobRefs && jobRefs.length > 0) {
            setJobRef(jobRefs[0].jobRef);
        }
    }, [jobRefs]);

    const typeOfRunOptions = ['Headings Only', 'Show Categories'];

    const handleRunClick = () => {
        const searchString = `?jobref=${jobRef}&headingsOnly=${runType === 'Headings Only'}`;

        history.push({
            pathname: '/inventory/tqms-category-summary/report',
            search: searchString
        });
    };

    const jobRefOptions = () => {
        return jobRefs?.map(c => ({
            id: c.jobRef,
            displayText: `${c.jobRef} - ${moment(new Date(c.dateOfRun)).format('DD MMM YYYY')}`
        }));
    };

    const handleFieldChange = (_, newValue) => {
        setJobRef(newValue);
    };

    const handleRunTypeChange = (_, newValue) => {
        setRunType(newValue);
    };

    return (
        <Page>
            <Title text="TQMS Summary Report" />
            {jobRefsLoading ? (
                <Loading />
            ) : (
                <Grid style={{ marginTop: 40 }} container spacing={3} justifyContent="center">
                    <Grid item xs={12}>
                        <Dropdown
                            label="JobRef"
                            onChange={handleFieldChange}
                            propertyName="jobRef"
                            value={jobRef}
                            items={jobRefOptions()}
                            required
                            optionsLoading={jobRefsLoading}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <Dropdown
                            label="Run Type"
                            onChange={handleRunTypeChange}
                            propertyName="runType"
                            value={runType}
                            items={typeOfRunOptions}
                            required
                        />
                    </Grid>
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
            )}
        </Page>
    );
}

TqmsSummaryByCategoryReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    jobRefs: PropTypes.arrayOf(
        PropTypes.shape({ jobRef: PropTypes.string, dateOfRun: PropTypes.string })
    ).isRequired,
    jobRefsLoading: PropTypes.bool
};

TqmsSummaryByCategoryReportOptions.defaultProps = {
    jobRefsLoading: false
};
