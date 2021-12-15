import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { LinkButton, Dropdown } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';
import Page from '../../containers/Page';

function PartTemplateSearch({ privileges, partTemplates }) {
    const [template, setTemplate] = useState();

    const createUrl = () => {
        return `/inventory/part-templates/create`;
    };
    const viewUrl = () => {
        return `/inventory/part-templates/${template}`;
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
                <Grid item xs={7}>
                    <Typography variant="h3">Part Template Utility</Typography>
                </Grid>
                <Grid item xs={1}>
                    <LinkButton
                        text="Create"
                        to={createUrl()}
                        disabled={!canCreate()}
                        tooltip={
                            canCreate() ? null : 'You are not authorised to create part templates.'
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
                        text="View"
                        to={viewUrl()}
                        disabled={!canCreate()}
                        tooltip={
                            canCreate() ? null : 'You are not authorised to create part templates.'
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
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired,
    partTemplates: PropTypes.arrayOf(PropTypes.shape({}))
};

PartTemplateSearch.defaultProps = {
    partTemplates: []
};

export default PartTemplateSearch;
