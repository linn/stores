import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import {
    ReportTable,
    Loading,
    ExportButton,
    Typeahead
} from '@linn-it/linn-form-components-library';
import Button from '@material-ui/core/Button';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function TriggerLevelsForAStoragePlaceReport({
    reportData,
    loading,
    fetchReport,
    config,
    storagePlacesSearchResults,
    storagePlacesSearchLoading,
    searchStoragePlaces
}) {
    const [searchTerm, setSearchTerm] = useState('');
    return (
        <Page>
            <Grid container space={3}>
                <Grid item xs={12}>
                    <Typeahead
                        onSelect={newValue => setSearchTerm(newValue.name)}
                        label="Onto Location"
                        modal
                        openModalOnClick={false}
                        handleFieldChange={(_, newValue) => {
                            setSearchTerm(newValue);
                        }}
                        propertyName="ontoLocation"
                        items={storagePlacesSearchResults}
                        value={searchTerm}
                        loading={storagePlacesSearchLoading}
                        fetchItems={searchStoragePlaces}
                        links={false}
                        text
                        clearSearch={() => {}}
                        placeholder="Search Locations"
                        minimumSearchTermLength={3}
                    />
                </Grid>
                <Grid item xs={2}>
                    <Button
                        color="primary"
                        variant="contained"
                        onClick={() => fetchReport({ searchTerm })}
                    >
                        Run
                    </Button>
                </Grid>
                <Grid item xs={10} />

                <Grid item xs={12}>
                    {loading ? (
                        <Loading />
                    ) : (
                        reportData && (
                            <>
                                <Grid item xs={12}>
                                    <ExportButton
                                        href={`${config.appRoot}/inventory/storage-places/reports/stock-trigger-levels/export?searchTerm=${searchTerm}`}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <ReportTable
                                        reportData={reportData}
                                        showTotals={false}
                                        showTitle
                                        title={
                                            reportData ? reportData?.title.displayString : 'Loading'
                                        }
                                    />
                                </Grid>
                            </>
                        )
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

TriggerLevelsForAStoragePlaceReport.propTypes = {
    reportData: PropTypes.shape({
        title: PropTypes.shape({ displayString: PropTypes.string }),
        headers: PropTypes.shape(),
        results: PropTypes.arrayOf(PropTypes.shape)
    }),
    searchStoragePlaces: PropTypes.func.isRequired,
    storagePlacesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    storagePlacesSearchLoading: PropTypes.bool,
    loading: PropTypes.bool,
    fetchReport: PropTypes.func.isRequired,
    config: PropTypes.shape({ appRoot: PropTypes.string }).isRequired
};

TriggerLevelsForAStoragePlaceReport.defaultProps = {
    reportData: null,
    loading: false,
    storagePlacesSearchResults: [],
    storagePlacesSearchLoading: false
};
