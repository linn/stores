import { Grid, Typography } from '@material-ui/core';
import React from 'react';
import PropTypes from 'prop-types';

export default class WhatToWandPrintout extends React.PureComponent {
    render() {
        const { whatToWandReport } = this.props;

        return (
            <div className="show-only-when-printing">
                <style>
                    {`@media print {
                @page { size: portrait; margin: 75px; }
              }`}
                </style>

                {whatToWandReport.map(c => {
                    const rows = c.lines.map(l => ({
                        ...l,
                        id: `${l.orderNumber - l.orderLine}`
                    }));
                    return (
                        <Grid spacing={3} style={{ breakAfter: 'always !important' }}>
                            <Grid item xs={12}>
                                <Typography align="center" variant="h2">
                                    {c.type}
                                </Typography>
                            </Grid>
                            <Grid item xs={12}>
                                <Typography align="center" variant="h4">
                                    {c.account.accountName}
                                </Typography>
                            </Grid>
                            <Grid item xs={1} />
                            <Grid item xs={11}>
                                <Typography variant="h2">{c.consignment.consignmentId}</Typography>
                            </Grid>
                            <Grid item xs={1} />
                            <Grid item xs={12}>
                                Account: {c.account.accountId} - {c.account.accountName}
                            </Grid>

                            <Grid item xs={12}>
                                Delivery Address: {c.consignment.addressId}
                            </Grid>
                            <Grid item xs={12}>
                                Country: {c.consignment.country}
                            </Grid>
                            <Grid item xs={5} />
                            <Grid item xs={1} />
                            <Grid item xs={12}>
                                <table style={{ width: '100%', borderSpacing: '0 20px' }}>
                                    <tr style={{ textAlign: 'left', marginTop: '20px' }}>
                                        <th style={{ width: '15%' }}>Order</th>
                                        <th style={{ width: '5%' }}>Line</th>
                                        <th style={{ width: '15%' }}>Article</th>
                                        <th style={{ width: '20%' }}>Invoice Desc.</th>
                                        <th style={{ width: '15%' }}>Mains Lead</th>
                                        <th style={{ width: '5%' }}>Kitted</th>
                                        <th style={{ width: '5%' }}>Ordered</th>
                                        <th style={{ width: '5%' }}>SIF</th>
                                    </tr>
                                    <tbody>
                                        {rows.map(r => (
                                            <tr style={{ paddingTop: '10px' }}>
                                                <td>{r.orderNumber}</td>
                                                <td>{r.orderLine}</td>
                                                <td>{r.articleNumber}</td>
                                                <td>{r.invoiceDescription}</td>
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
                                        c.consignment.currencyCode
                                    }): ${c.consignment.totalNettValue.toFixed(2)}`}
                                </Typography>
                            </Grid>
                            <Grid item xs={1} />
                        </Grid>
                    );
                })}
            </div>
        );
    }
}

WhatToWandPrintout.propTypes = {
    whatToWandReport: PropTypes.arrayOf(
        PropTypes.shape({
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
        })
    ).isRequired
};
