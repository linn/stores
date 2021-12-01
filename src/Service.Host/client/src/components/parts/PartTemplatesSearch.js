import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { LinkButton, Dropdown } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import makeStyles from '@material-ui/styles/makeStyles';
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

function PartTemplateSearch({ privileges, partTemplates, linkToSources }) {
    const classes = useStyles();

    const [template, setTemplate] = useState();

    const createUrl = () => {
        if (linkToSources) {
            return '/inventory/part-templates/create';
        }
        return template ? `/inventory/part-templates/create` : '/inventory/part-templates/create';
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
            </Grid>
        </Page>
    );
}

PartTemplateSearch.propTypes = {
    history: PropTypes.shape({}).isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired,
    partTemplates: PropTypes.arrayOf(PropTypes.shape({})),
    linkToSources: PropTypes.bool
};

PartTemplateSearch.defaultProps = {
    partTemplates: [],
    linkToSources: false
};

export default PartTemplateSearch;
