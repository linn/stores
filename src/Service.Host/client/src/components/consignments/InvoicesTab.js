import React from 'react';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';

function InvoicesTab({ invoices, exportBooks, printDocuments }) {
    return (
        <>
            <Grid container spacing={3} style={{ paddingTop: '30px' }}>
                {invoices && invoices.length > 0 ? (
                    <>
                        <Grid item xs={2}>
                            <Typography variant="subtitle2">Invoices</Typography>
                        </Grid>
                        <Grid item xs={10}>
                            {invoices.map(i => (
                                <Typography variant="subtitle2">{i.documentNumber}</Typography>
                            ))}
                        </Grid>
                    </>
                ) : (
                    <>
                        <Grid item xs={2} />
                        <Grid item xs={10}>
                            <Typography variant="subtitle2">No Invoices</Typography>
                        </Grid>
                    </>
                )}
                {exportBooks && exportBooks.length > 0 ? (
                    <>
                        <Grid item xs={2}>
                            <Typography variant="subtitle2">Export Books</Typography>
                        </Grid>
                        <Grid item xs={10}>
                            {exportBooks.map(e => (
                                <Typography variant="subtitle2">{e.exportId}</Typography>
                            ))}
                        </Grid>
                    </>
                ) : (
                    <>
                        <Grid item xs={2} />
                        <Grid item xs={10}>
                            <Typography variant="subtitle2">No Export Books</Typography>
                        </Grid>
                    </>
                )}
                <>
                    <Grid item xs={2} />
                    <Grid item xs={10}>
                        <Button
                            style={{ marginTop: '40px' }}
                            onClick={printDocuments}
                            variant="outlined"
                            color="primary"
                            disabled={
                                (!invoices || invoices.length === 0) &&
                                (!exportBooks || exportBooks.length === 0)
                            }
                        >
                            Reprint Documents
                        </Button>
                    </Grid>
                </>
            </Grid>
        </>
    );
}

InvoicesTab.propTypes = {
    invoices: PropTypes.arrayOf(PropTypes.shape({})),
    exportBooks: PropTypes.arrayOf(PropTypes.shape({})),
    printDocuments: PropTypes.func.isRequired
};

InvoicesTab.defaultProps = {
    invoices: [],
    exportBooks: []
};

export default InvoicesTab;
