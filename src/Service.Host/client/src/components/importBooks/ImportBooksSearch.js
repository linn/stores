import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import {
    Typeahead,
    LinkButton,
    DatePicker,
    InputField
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import makeStyles from '@material-ui/styles/makeStyles';
import Page from '../../containers/Page';

function ImportBooksSearch({ items, fetchItems, loading, clearSearch, history, privileges }) {
    const searchItems = () => {
        return items?.map(item => ({
            ...item,
            name: item.id.toString(),
            description: `created ${new Date(item.dateCreated).toLocaleDateString(
                'en-GB'
            )}. Customs entry: ${item.customsEntryCodePrefix} ${
                item.customsEntryCode
            }, entry date ${new Date(item.customsEntryCodeDate).toLocaleDateString('en-GB')}`,
            href: item.href
        }));
    };
    const [options, setOptions] = useState({ poNumber: null, rsnNumber: null });

    const useStyles = makeStyles(theme => ({
        button: {
            marginLeft: theme.spacing(1),
            marginTop: theme.spacing(3)
        },
        a: {
            textDecoration: 'none'
        }
    }));
    const classes = useStyles();

    const doSearch = searchTerm => {
        fetchItems(
            searchTerm,
            `&rsnNumber=${options.rsnNumber ?? ''}&poNumber=${options.poNumber ?? ''}&fromDate=${
                options.fromDate ? options.fromDate.toISOString() : ''
            }&toDate=${
                options.toDate ? options.toDate.toISOString() : ''
            }&customsEntryCodePrefix=${options.customsEntryCodePrefix ??
                ''}&customsEntryCode=${options.customsEntryCode ?? ''}&customsEntryDate=${
                options.customsEntryDate ? options.customsEntryDate.toISOString() : ''
            }`
        );
    };

    const canCreate = () => {
        return privileges?.some(priv => priv === 'import-books.admin');
    };

    const handleFieldChange = (propertyName, newValue) => {
        setOptions(o => ({ ...o, [propertyName]: newValue }));
    };
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={10}>
                    <Typography variant="h4">Search Import Books</Typography>
                </Grid>
                <Grid item xs={1}>
                    <LinkButton
                        text="Create"
                        to="/logistics/import-books/create"
                        disabled={!canCreate()}
                        tooltip={
                            canCreate() ? null : 'You are not authorised to create import books.'
                        }
                    />
                </Grid>
                <Grid item xs={1} />
                <Grid item xs={3}>
                    <DatePicker
                        label="From Date"
                        value={options.fromDate ? options.fromDate : null}
                        onChange={value => {
                            handleFieldChange('fromDate', value);
                        }}
                    />
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        label="To Date"
                        value={options.toDate ? options.toDate : null}
                        onChange={value => {
                            handleFieldChange('toDate', value);
                        }}
                    />
                </Grid>

                <Grid item xs={1}>
                    <InputField
                        label="Customs Prefix"
                        value={options.customsEntryCodePrefix}
                        onChange={handleFieldChange}
                        propertyName="customsEntryCodePrefix"
                        fullwidth
                    />
                </Grid>
                <Grid item xs={2}>
                    <InputField
                        label="Customs Entry Code"
                        value={options.customsEntryCode}
                        onChange={handleFieldChange}
                        propertyName="customsEntryCode"
                        fullwidth
                    />
                </Grid>
                <Grid item xs={2}>
                    <InputField
                        label="RSN Number"
                        value={options.rsnNumber}
                        onChange={handleFieldChange}
                        propertyName="rsnNumber"
                        fullwidth
                        type="number"
                    />
                </Grid>
                <Grid item xs={2}>
                    <InputField
                        label="PO Number"
                        value={options.poNumber}
                        onChange={handleFieldChange}
                        propertyName="poNumber"
                        fullwidth
                        type="number"
                    />
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        label="Customs Entry Date"
                        value={options.customsEntryDate ? options.customsEntryDate : null}
                        onChange={value => {
                            handleFieldChange('customsEntryDate', value);
                        }}
                    />
                </Grid>
                <Grid item xs={8} />
                <Grid item xs={4}>
                    <Button
                        className={classes.button}
                        variant="outlined"
                        onClick={() => doSearch('')}
                        color="primary"
                    >
                        Go
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    <Typeahead
                        items={searchItems()}
                        fetchItems={doSearch}
                        clearSearch={clearSearch}
                        loading={loading}
                        title=""
                        history={history}
                        debounce={1000}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

ImportBooksSearch.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            href: PropTypes.string
        })
    ).isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired
};

ImportBooksSearch.defaultProps = {
    loading: false
};

export default ImportBooksSearch;
