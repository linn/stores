import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import { LinkButton, Dropdown, utilities } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';
import Page from '../../containers/Page';

function PartTemplateSearch({ applicationState, partTemplates }) {
    const [template, setTemplate] = useState();

    const createUrl = () => {
        return `/inventory/part-templates/create`;
    };
    const viewUrl = () => {
        return `/inventory/part-templates/${template}`;
    };
    const [allowedToCreate, setAllowedToCreate] = useState(false);

    useEffect(() => {
        setAllowedToCreate(utilities.getHref(applicationState, 'create') !== null);
    }, [applicationState]);

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={7}>
                    <Typography variant="h3">Part Template Utility</Typography>
                </Grid>
                <Grid item xs={1}>
                    <LinkButton
                        text="Create"
                        to={createUrl()}
                        disabled={!allowedToCreate}
                        tooltip={
                            allowedToCreate
                                ? null
                                : 'You are not authorised to create part templates.'
                        }
                    />
                </Grid>
                <Grid item xs={12} />
                <Grid item xs={3}>
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
                </Grid>
                <Grid item xs={1}>
                    <LinkButton
                        label="View"
                        text="View"
                        to={viewUrl()}
                        disabled={!allowedToCreate}
                        tooltip={
                            allowedToCreate
                                ? null
                                : 'You are not authorised to create part templates.'
                        }
                    />
                </Grid>
                <Grid item xs={1} />
            </Grid>
        </Page>
    );
}

PartTemplateSearch.propTypes = {
    history: PropTypes.shape({}).isRequired,
    partTemplates: PropTypes.arrayOf(PropTypes.shape({})),
    applicationState: PropTypes.shape({ links: PropTypes.arrayOf(PropTypes.shape({})) })
};

PartTemplateSearch.defaultProps = {
    partTemplates: [],
    applicationState: null
};

export default PartTemplateSearch;
