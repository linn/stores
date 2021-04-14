import { Grid, Typography } from '@material-ui/core';
import React from 'react';
import PropTypes from 'prop-types';

export default class WhatToWandPrintout extends React.PureComponent {
    render() {
        const { whatToWandReport } = this.props;
        const rows = whatToWandReport.lines.map(l => ({
            ...l,
            id: `${l.orderNumber - l.orderLine}`
        }));
        return (
            <div className="show-only-when-printing">
                <style>
                    {`@media print {
                @page { size: landscape; }
              }`}
                </style>
                <Grid container spacing={3}>
                    <Grid item xs={1} />

                    <Grid item xs={3}>
                        What to Wand
                    </Grid>
                    <Grid item xs={5} />
                    <Grid item xs={3}>
                        {new Date().toLocaleString()}
                    </Grid>
                    <Grid item xs={12}>
                        <Typography align="center" variant="h2">
                            {whatToWandReport.type}
                        </Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography align="center" variant="h4">
                            {whatToWandReport.account.accountName}
                        </Typography>
                    </Grid>
                    <Grid item xs={1} />

                    <Grid item xs={11}>
                        <Typography variant="h2">
                            {whatToWandReport.consignment.consignmentId}
                        </Typography>
                    </Grid>
                    <Grid item xs={1} />
                    <Grid item xs={2}>
                        Account:
                    </Grid>
                    <Grid item xs={2}>
                        {whatToWandReport.account.accountId}
                    </Grid>
                    <Grid item xs={2}>
                        {whatToWandReport.account.accountName}
                    </Grid>
                    <Grid item xs={5} />
                    <Grid item xs={1} />
                    <Grid item xs={2}>
                        Delivery Address:
                    </Grid>
                    <Grid item xs={2}>
                        {whatToWandReport.consignment.addressId}
                    </Grid>
                    <Grid item xs={2}>
                        {/* {whatToWandReport.account.accountName} */}
                    </Grid>
                    <Grid item xs={5} />
                    <Grid item xs={1} />
                    <Grid item xs={2}>
                        Country:
                    </Grid>
                    <Grid item xs={4}>
                        {whatToWandReport.consignment.country}
                    </Grid>
                    <Grid item xs={5} />
                    <Grid item xs={1} />
                    <Grid item xs={10}>
                        <table style={{ width: '100%', borderSpacing: '0 20px' }}>
                            <thead style={{ textAlign: 'left', marginTop: '20px' }}>
                                <tr>
                                    <th style={{ width: '15%' }}>Order</th>
                                    <th style={{ width: '5%' }}>Line</th>
                                    <th style={{ width: '15%' }}>Article</th>
                                    <th style={{ width: '20%' }}>Invoice Desc.</th>
                                    <th style={{ width: '15%' }}>Manual</th>
                                    <th style={{ width: '15%' }}>Mains Lead</th>
                                    <th style={{ width: '5%' }}>Kitted</th>
                                    <th style={{ width: '5%' }}>Ordered</th>
                                    <th style={{ width: '5%' }}>SIF</th>
                                </tr>
                            </thead>
                            <tbody>
                                {rows.map(r => (
                                    <tr style={{ paddingTop: '10px' }}>
                                        <td>{r.orderNumber}</td>
                                        <td>{r.orderLine}</td>
                                        <td>{r.articleNumber}</td>
                                        <td>{r.invoiceDescription}</td>
                                        <td>{r.manual}</td>
                                        <td>{r.mainsLead}</td>
                                        <td>{r.kitted}</td>
                                        <td>{r.ordered}</td>
                                        <td>{r.sif}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </Grid>
                    <Grid item xs={1} />
                    <Grid item xs={1} />
                    <Grid item xs={10}>
                        <Typography align="center" variant="h6">
                            {`Total Nett Value Of Consignment (${
                                whatToWandReport.consignment.currencyCode
                            }): ${whatToWandReport.consignment.totalNettValue.toFixed(2)}`}
                        </Typography>
                    </Grid>
                    <Grid item xs={1} />
                </Grid>
            </div>
        );
    }
}

WhatToWandPrintout.propTypes = {
    whatToWandReport: PropTypes.shape({
        consignment: PropTypes.shape({
            currencyCode: PropTypes.string,
            totalNettValue: PropTypes.number,
            country: PropTypes.string,
            consignmentId: PropTypes.number,
            addressId: PropTypes.number
        }),
        lines: PropTypes.arrayOf(PropTypes.shape({})),
        type: PropTypes.string,
        account: PropTypes.shape({ accountName: PropTypes.string, accountId: PropTypes.number })
    }).isRequired
};
