import React, { useEffect, useRef, useState } from 'react';
import { useReactToPrint } from 'react-to-print';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { ErrorCard, InputField, LinkButton, Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';
import WhatToWandPrintOut from './WhatToWandPrintOut';

export default function WhatToWandReprint({
    whatToWandReport,
    fetch,
    loading,
    clear,
    error,
    clearErrors
}) {
    const componentRef = useRef();

    const [id, setId] = useState(null);

    const handlePrint = useReactToPrint({
        content: () => componentRef.current,
        onAfterPrint: () => clear()
    });

    useEffect(() => {
        if (whatToWandReport) {
            handlePrint();
        }
    }, [whatToWandReport, handlePrint]);

    if (!loading) {
        return (
            <Page>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <LinkButton text="Back to TPK screen" to="/logistics/tpk" />
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h6">Re-print Report for a Consignment </Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <InputField
                            label="Enter Consignment Number and click to print"
                            propertyName="consignment"
                            value={id}
                            onChange={(_, newVal) => setId(newVal)}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <Button
                            disabled={!id}
                            variant="contained"
                            color="primary"
                            onClick={() => {
                                clearErrors();
                                fetch(id);
                            }}
                        >
                            Print
                        </Button>
                    </Grid>
                    {error && (
                        <Grid item xs={12}>
                            <ErrorCard
                                errorMessage={error?.details?.errors?.[0] || error.statusText}
                            />
                        </Grid>
                    )}
                </Grid>
                {whatToWandReport && (
                    <WhatToWandPrintOut ref={componentRef} whatToWandReport={[whatToWandReport]} />
                )}
            </Page>
        );
    }
    return (
        <Page>
            <Grid item xs={12}>
                <Loading />
            </Grid>
        </Page>
    );
}

WhatToWandReprint.propTypes = {
    whatToWandReport: PropTypes.shape({}),
    error: PropTypes.shape({
        statusText: PropTypes.string,
        details: PropTypes.shape({ errors: PropTypes.arrayOf(PropTypes.string) })
    }),
    clearErrors: PropTypes.func.isRequired,
    fetch: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    clear: PropTypes.func.isRequired
};

WhatToWandReprint.defaultProps = {
    whatToWandReport: null,
    loading: false,
    error: null
};
