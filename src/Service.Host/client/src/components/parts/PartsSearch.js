import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead, LinkButton, Dropdown } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function PartsSearch({
    items,
    fetchItems,
    loading,
    clearSearch,
    history,
    privileges,
    partTemplates,
    linkToSources
}) {
    const [template, setTemplate] = useState();

    const createUrl = () => {
        if (linkToSources) {
            return '/inventory/parts/sources/create';
        }
        return template
            ? `/inventory/parts/create?template=${template}`
            : '/inventory/parts/create';
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

    const canCreate = () => {
        if (!(privileges.length < 1)) {
            return privileges.some(priv => priv === 'part.admin');
        }
        return false;
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={7} />
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
                <Grid item xs={12}>
                    <Typeahead
                        items={searchItems()}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        resultLimit={100}
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
                        title="Part"
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
    linkToSources: PropTypes.bool
};

PartsSearch.defaultProps = {
    loading: false,
    partTemplates: [],
    linkToSources: false
};

export default PartsSearch;
