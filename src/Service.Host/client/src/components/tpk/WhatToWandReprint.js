import React, { useEffect, useRef, useState } from 'react';
import { useReactToPrint } from 'react-to-print';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import { InputField, Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';
import WhatToWandPrintOut from './WhatToWandPrintOut';

export default function WhatToWandReprint({ whatToWandReport, fetch, loading, clear }) {
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
                <Grid item xs={12}>
                    <InputField
                        propertyName="consignment"
                        value={id}
                        onChange={(_, newVal) => setId(newVal)}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Button onClick={() => fetch(id)}>Print</Button>
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
    fetch: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    clear: PropTypes.func.isRequired
};

WhatToWandReprint.defaultProps = {
    whatToWandReport: null,
    loading: false
};
