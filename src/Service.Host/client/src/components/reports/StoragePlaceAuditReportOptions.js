import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import {
    Title,
    InputField,
    TypeaheadDialog,
    SelectedItemsList,
    Dropdown,
    SnackbarMessage,
    Loading
} from '@linn-it/linn-form-components-library';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function StoragePlaceAuditReportOptions({
    storagePlacesSearchLoading,
    storagePlacesSearchResults,
    auditLocationsSearchLoading,
    auditLocationsSearchResults,
    searchStoragePlaces,
    clearStoragePlacesSearch,
    searchAuditLocations,
    clearAuditLocationsSearch,
    history,
    createAuditReqsMessageVisible,
    createAuditReqsMessageText,
    createAuditReqs,
    clearAuditReqsErrors,
    setAuditReqsMessageVisible,
    createAuditReqsWorking
}) {
    const [options, setOptions] = useState({
        storagePlaces: [],
        auditLocation: ''
    });

    const [searchType, setSearchType] = useState('Storage Place');

    const handleStoragePlaceSelect = storagePlace => {
        setOptions({ ...options, storagePlaces: [...options.storagePlaces, storagePlace.id] });
    };

    const handleAuditLocationSelect = auditLocation => {
        setOptions({ ...options, auditLocation: auditLocation.storagePlace });
    };

    const handleSearchTypeChange = (_, newValue) => {
        setSearchType(newValue);
    };

    const validateReportOptions = () =>
        searchType === 'Storage Place' ? options.storagePlaces.length > 0 : !!options.auditLocation;

    const handleCreateAuditReqs = () => {
        clearAuditReqsErrors();

        if (searchType === 'Storage Place') {
            createAuditReqs({
                locationList: options.storagePlaces
            });
        } else {
            createAuditReqs({
                locationRange: options.auditLocation
            });
        }
    };

    const handleRunClick = () => {
        let search = '';

        if (searchType === 'Storage Place') {
            options.storagePlaces.forEach((s, i) => {
                search += i === 0 ? `?locationList=${s}` : `&locationList=${s}`;
            });
        } else {
            search = `?locationRange=${options.auditLocation}`;
        }

        history.push({
            pathname: '/inventory/reports/storage-place-audit/report',
            search
        });
    };

    return (
        <Page>
            <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Title text="Storage Place Audit Report" />
                </Grid>
                <SnackbarMessage
                    visible={createAuditReqsMessageVisible}
                    onClose={() => setAuditReqsMessageVisible(false)}
                    message={createAuditReqsMessageText}
                />
                {createAuditReqsWorking ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        <Grid item xs={12}>
                            <Dropdown
                                label="Search by Sorage Places or Kardex Shelf"
                                items={['Storage Place', 'Kardex Shelf']}
                                value={searchType}
                                onChange={handleSearchTypeChange}
                                allowNoValue={false}
                            />
                        </Grid>
                        {searchType === 'Storage Place' ? (
                            <>
                                <Grid item xs={3}>
                                    <Typography>Storage places to show on report</Typography>
                                </Grid>
                                <Grid item xs={1}>
                                    <TypeaheadDialog
                                        title="Search for Storage Place"
                                        onSelect={handleStoragePlaceSelect}
                                        searchItems={storagePlacesSearchResults}
                                        loading={storagePlacesSearchLoading}
                                        fetchItems={searchStoragePlaces}
                                        clearSearch={() => clearStoragePlacesSearch}
                                    />
                                </Grid>
                                <Grid item xs={8}>
                                    <SelectedItemsList
                                        title="Storage Places Selected"
                                        items={options.storagePlaces}
                                        removeItem={() => {}}
                                        maxHeight={300}
                                    />
                                </Grid>
                            </>
                        ) : (
                            <>
                                <Grid item xs={3}>
                                    <Typography>
                                        Search for location range (Kardex Shelf)
                                    </Typography>
                                </Grid>
                                <Grid item xs={1}>
                                    <TypeaheadDialog
                                        title="Search for location range"
                                        onSelect={handleAuditLocationSelect}
                                        searchItems={auditLocationsSearchResults}
                                        loading={auditLocationsSearchLoading}
                                        fetchItems={searchAuditLocations}
                                        clearSearch={() => clearAuditLocationsSearch}
                                    />
                                </Grid>
                                <Grid item xs={8}>
                                    <InputField
                                        label="Location range (Kardex Shelf)"
                                        fullWidth
                                        value={options.auditLocation}
                                        disabled
                                    />
                                </Grid>
                            </>
                        )}
                        <Grid item xs={12}>
                            <Button
                                color="primary"
                                variant="outlined"
                                onClick={handleCreateAuditReqs}
                                disabled={!validateReportOptions()}
                            >
                                Create Audit Reqs
                            </Button>
                            <Button
                                color="primary"
                                variant="contained"
                                style={{ float: 'right' }}
                                onClick={handleRunClick}
                                disabled={!validateReportOptions()}
                            >
                                Run Report
                            </Button>
                        </Grid>{' '}
                    </>
                )}
            </Grid>
        </Page>
    );
}

StoragePlaceAuditReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    storagePlacesSearchLoading: PropTypes.bool,
    storagePlacesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    auditLocationsSearchLoading: PropTypes.bool,
    auditLocationsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    searchStoragePlaces: PropTypes.func.isRequired,
    clearStoragePlacesSearch: PropTypes.func.isRequired,
    searchAuditLocations: PropTypes.func.isRequired,
    clearAuditLocationsSearch: PropTypes.func.isRequired,
    createAuditReqsMessageVisible: PropTypes.bool,
    createAuditReqsMessageText: PropTypes.string,
    createAuditReqs: PropTypes.func.isRequired,
    clearAuditReqsErrors: PropTypes.func.isRequired,
    setAuditReqsMessageVisible: PropTypes.func.isRequired,
    createAuditReqsWorking: PropTypes.bool
};

StoragePlaceAuditReportOptions.defaultProps = {
    storagePlacesSearchLoading: false,
    storagePlacesSearchResults: [],
    auditLocationsSearchLoading: false,
    auditLocationsSearchResults: [],
    createAuditReqsMessageVisible: false,
    createAuditReqsMessageText: '',
    createAuditReqsWorking: false
};
