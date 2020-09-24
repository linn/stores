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
    partTemplates
}) {
    const searchItems = items.map(item => ({
        ...item,
        name: item.partNumber.toString(),
        description: item.description
    }));

    const [template, setTemplate] = useState();

    const canCreate = () => {
        if (!(privileges.length < 1)) {
            return privileges.some(priv => priv === 'part.admin');
        }
        return false;
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={8} />
                <Grid item xs={3}>
                    <Dropdown
                        label="Template"
                        propertyName="partTemplate"
                        items={partTemplates
                            .filter(p => p.allowPartCreation === 'Y')
                            .map(t => ({
                                id: t.partRoot,
                                displayText: t.description
                            }))}
                        fullWidth
                        allowNoValue
                        value={template}
                        onChange={(_, newValue) => {
                            setTemplate(newValue);
                        }}
                    />
                </Grid>
                <Grid item xs={1}>
                    <LinkButton
                        text="Create"
                        to={`/inventory/parts/create?template=${template}`}
                        disabled={!canCreate()}
                        tooltip={canCreate() ? null : 'You are not authorised to create parts.'}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Typeahead
                        items={searchItems}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title="Part"
                        history={history}
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
    partTemplates: PropTypes.arrayOf(PropTypes.shape({}))
};

PartsSearch.defaultProps = {
    loading: false,
    partTemplates: []
};

export default PartsSearch;
