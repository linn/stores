import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { Title, Typeahead } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function StockViewerOptions({ parts, partsLoading, searchParts, clearPartsSearch, history }) {
    const [partNumber, setPartNumber] = useState('');

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Stock Viewer" />
                </Grid>
                <Grid item xs={12}>
                    <Typeahead
                        items={parts}
                        fetchItems={searchParts}
                        modal
                        links={false}
                        clearSearch={clearPartsSearch}
                        loading={partsLoading}
                        label="Part Number"
                        title="Search Parts"
                        value={partNumber}
                        onSelect={newValue => setPartNumber(newValue.partNumber)}
                        history={history}
                        debounce={1000}
                        minimumSearchTermLength={2}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

StockViewerOptions.propTypes = {
    parts: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string,
            href: PropTypes.string
        })
    ).isRequired,
    partsLoading: PropTypes.bool,
    history: PropTypes.shape({}).isRequired,
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired
};

StockViewerOptions.defaultProps = {
    partsLoading: false
};

export default StockViewerOptions;
