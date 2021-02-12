import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { Title, Dropdown } from '@linn-it/linn-form-components-library';
import Page from '../containers/Page';

const useStyles = makeStyles({
    runButton: {
        float: 'right',
        width: '100%'
    }
});

function Wand({ wandConsignments }) {
    const [consignmentId, setConsignmentId] = useState(null);

    const classes = useStyles();

    const handleConsignmentChange = (propertyName, newValue) => {
        setConsignmentId(newValue);
    };

    const consignmentOptions = () => {
        return wandConsignments?.map(c => ({
            id: c.consignmentId,
            displayText: `Consignment: ${c.consignmentId} Addressee: ${c.addressee} Country: ${
                c.countryCode
            } ${c.isDone ? c.isDone : ' '} `
        }));
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Wand" />
                </Grid>
                <Grid item xs={12}>
                    <Dropdown
                        label="Consignment"
                        propertyName="consignment"
                        items={consignmentOptions()}
                        value={consignmentId}
                        onChange={handleConsignmentChange}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

Wand.propTypes = {
    wandConsignments: PropTypes.arrayOf(PropTypes.shape({}))
};

Wand.defaultProps = {
    wandConsignments: []
};

export default Wand;
