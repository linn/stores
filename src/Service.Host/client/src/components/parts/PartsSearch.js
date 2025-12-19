import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, LinkButton, Dropdown, InputField } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Accordion from '@material-ui/core/Accordion';
import Button from '@material-ui/core/Button';
import makeStyles from '@material-ui/styles/makeStyles';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Typography from '@material-ui/core/Typography';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    button: {
        marginLeft: theme.spacing(1),
        marginTop: theme.spacing(3)
    },
    a: {
        textDecoration: 'none'
    }
}));

function PartsSearch({
    items,
    fetchItems,
    loading,
    clearSearch,
    history,
    privileges,
    partTemplates,
    linkToSources,
    productAnalysisCodeSearchResults,
    productAnalysisCodeSearchLoading,
    searchProductAnalysisCodes
}) {
    const classes = useStyles();

    const [template, setTemplate] = useState();

    const [expanded, setExpanded] = useState(false);

    const [options, setOptions] = useState({
        partNumber: '',
        description: '',
        productAnalysisCode: '',
        manufacturersPartNumber: ''
    });

    const handleOptionsChange = (propertyName, newValue) =>
        setOptions({ ...options, [propertyName]: newValue });

    const createUrl = () => {
        if (linkToSources) {
            return '/parts/sources/create';
        }
        return template ? `/parts/create?template=${template}` : '/parts/create';
    };
    const searchItems = () => {
        const result = linkToSources
            ? items.filter(i => i.links.find(l => l.rel === 'mechanical-sourcing-sheet'))
            : items;

        return result?.map(item => ({
            ...item,
            name: item.partNumber.toString(),
            description: item.description,
            href: linkToSources
                ? item.links.find(l => l.rel === 'mechanical-sourcing-sheet')?.href
                : item.href
        }));
    };

    useEffect(() => {
        document.title = 'Search Parts';
    }, []);

    const canCreate = () => {
        if (!(privileges.length < 1)) {
            return privileges.some(priv => priv === 'part.admin');
        }
        return false;
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={7}>
                    <Typography variant="h3">
                        {linkToSources ? 'Search Part Source Sheets' : 'Search Parts'}
                    </Typography>
                </Grid>
                <Grid item xs={3}>
                    {!linkToSources && (
                        <Dropdown
                            label="Template"
                            propertyName="partTemplate"
                            items={partTemplates
                                .filter(p => p.allowPartCreation === 'Y')
                                .map(t => ({
                                    id: t.partRoot,
                                    displayText: `${t.partRoot} - ${t.description}`
                                }))}
                            fullWidth
                            allowNoValue
                            value={template}
                            onChange={(_, newValue) => {
                                setTemplate(newValue);
                            }}
                        />
                    )}
                </Grid>
                <Grid item xs={1}>
                    <LinkButton
                        text="Create"
                        to={createUrl()}
                        disabled={!canCreate()}
                        tooltip={canCreate() ? null : 'You are not authorised to create parts.'}
                    />
                </Grid>
                <Grid item xs={1} />
                {!linkToSources && (
                    <Grid item xs={12}>
                        <Accordion expanded={expanded}>
                            <AccordionSummary
                                onClick={() => setExpanded(!expanded)}
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-content"
                                id="panel1a-header"
                            >
                                <Typography>Advanced Search options</Typography>
                            </AccordionSummary>
                            <AccordionDetails>
                                <Grid container spacing={2}>
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
                                            value={options.partNumber}
                                            label="Part Number"
                                            propertyName="partNumber"
                                            onChange={handleOptionsChange}
                                            helperText="* can be used as a wildcard on all fields"
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
                                            value={options.description}
                                            label="Description"
                                            propertyName="description"
                                            onChange={handleOptionsChange}
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
                                            value={options.manufacturersPartNumber}
                                            label="Manuf Part Number"
                                            propertyName="manufacturersPartNumber"
                                            onChange={handleOptionsChange}
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Typeahead
                                            items={productAnalysisCodeSearchResults}
                                            fetchItems={searchProductAnalysisCodes}
                                            modal
                                            openModalOnClick={false}
                                            links={false}
                                            clearSearch={() => {}}
                                            loading={productAnalysisCodeSearchLoading}
                                            label="Product Analysis Code"
                                            title="Search Codes"
                                            value={options.productAnalysisCode}
                                            handleFieldChange={(_, newValue) => {
                                                setOptions({
                                                    ...options,
                                                    productAnalysisCode: newValue
                                                });
                                            }}
                                            onSelect={newValue =>
                                                setOptions({
                                                    ...options,
                                                    productAnalysisCode: newValue.id
                                                })
                                            }
                                            history={history}
                                            debounce={1000}
                                            minimumSearchTermLength={2}
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Button
                                            variant="outlined"
                                            color="primary"
                                            disabled={
                                                !options.partNumber &&
                                                !options.description &&
                                                !options.productAnalysisCode &&
                                                !options.manufacturersPartNumber
                                            }
                                            className={classes.button}
                                            onClick={() =>
                                                fetchItems(
                                                    '',
                                                    `&partNumberSearchTerm=${options.partNumber}&descriptionSearchTerm=${options.description}&productAnalysisCodeSearchTerm=${options.productAnalysisCode}&manufacturersPartNumberSearchTerm=${options.manufacturersPartNumber}`
                                                )
                                            }
                                        >
                                            Go
                                        </Button>
                                    </Grid>
                                </Grid>
                            </AccordionDetails>
                        </Accordion>
                    </Grid>
                )}
                <Grid item xs={12}>
                    <Typeahead
                        items={searchItems()}
                        fetchItems={searchTerm => fetchItems(searchTerm, '')}
                        clearSearch={clearSearch}
                        resultLimit={500}
                        priorityFunction={(item, searchTerm) => {
                            let count = 0;
                            for (let i = 0; i < searchTerm.length; i += 1) {
                                if (i === item.partNumber.length) {
                                    break;
                                }
                                if (
                                    item.partNumber.toUpperCase()[i] === searchTerm.toUpperCase()[i]
                                ) {
                                    count += 1;
                                }
                            }
                            return count;
                        }}
                        loading={loading}
                        history={history}
                        debounce={1000}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

PartsSearch.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string,
            href: PropTypes.string
        })
    ).isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired,
    partTemplates: PropTypes.arrayOf(PropTypes.shape({})),
    linkToSources: PropTypes.bool,
    productAnalysisCodeSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number])
        })
    ),
    productAnalysisCodeSearchLoading: PropTypes.bool,
    searchProductAnalysisCodes: PropTypes.func.isRequired
};

PartsSearch.defaultProps = {
    loading: false,
    partTemplates: [],
    linkToSources: false,
    productAnalysisCodeSearchResults: [],
    productAnalysisCodeSearchLoading: false
};

export default PartsSearch;
