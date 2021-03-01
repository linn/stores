/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../test-utils';
import ExportRsns from '../ExportRsns';

afterEach(cleanup);

const searchSalesAccounts = jest.fn();
const searchSalesOutlets = jest.fn();
const searchRsns = jest.fn();

const defaultProps = {
    salesOutletsSearchResults: [],
    salesAccountsSearchResults: [],
    rsnsSearchResults: [],
    salesAccountsSearchLoading: false,
    salesOutletsSearchLoading: false,
    rsnsSearchResultsLoading: false,
    searchSalesAccounts,
    searchSalesOutlets,
    searchRsns
};

const salesAccountsSearchResults = [{ accountId: 65140, accountName: 'acct1' }];

const salesOutletsSearchResults = [
    {
        accountId: 65140,
        outletNumber: 1,
        name: 'out1',
        country: 'US',
        countryName: 'United States Of America'
    },
    {
        accountId: 65140,
        outletNumber: 2,
        name: 'out2',
        country: 'US',
        countryName: 'United States Of America'
    }
];

const rsnsSearchResults = [
    {
        rsnNumber: 138588,
        reasonCodeAlleged: 'INSP',
        dateEntered: '2020-02-11T00:00:00.0000000',
        quantity: 2,
        articleNumber: '320 AMP MOD',
        accountId: 65140,
        outletNumber: 1,
        outletName: 'ACCENT ON MUSIC',
        country: 'US',
        countryName: 'United States Of America',
        accountType: 'RETAILER',
        href: null
    },
    {
        rsnNumber: 140269,
        reasonCodeAlleged: 'RFR',
        dateEntered: '2020-12-29T00:00:00.0000000',
        quantity: 1,
        articleNumber: 'MAJIK DSI',
        accountId: 65140,
        outletNumber: 2,
        outletName: 'ACCENT ON MUSIC',
        country: 'US',
        countryName: 'United States Of America',
        accountType: 'RETAILER',
        href: null
    },
    {
        rsnNumber: 138822,
        reasonCodeAlleged: 'RFC',
        dateEntered: '2020-03-18T00:00:00.0000000',
        quantity: 1,
        articleNumber: 'PCAS 959/A/PT',
        accountId: 65140,
        outletNumber: 2,
        outletName: 'ACCENT ON MUSIC',
        country: 'US',
        countryName: 'United States Of America',
        accountType: 'RETAILER',
        href: null
    }
];

describe('<ExportRsns>', () => {
    describe('when rendered', () => {
        it('should render typeahead', () => {
            const { getByText, getByRole } = render(<ExportRsns {...defaultProps} />);

            expect(getByText('Search for Sales Outlet or Account')).toBeInTheDocument();
            expect(getByRole('button')).toBeInTheDocument();
        });
    });

    describe('when viewing search results', () => {
        it('should show results with labels', () => {
            const { getByText, queryAllByText, getByRole } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);

            expect(getByText('Account')).toBeInTheDocument();
            expect(queryAllByText('Outlet')).toHaveLength(2);
        });

        it('should format account / outlet numbers', () => {
            const { getByText, getByRole } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);

            expect(getByText('65140')).toBeInTheDocument();
            expect(getByText('65140 / 1')).toBeInTheDocument();
            expect(getByText('65140 / 2')).toBeInTheDocument();
        });

        it('should display names', () => {
            const { getByText, getByRole } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);

            expect(getByText('acct1')).toBeInTheDocument();
            expect(getByText('out1')).toBeInTheDocument();
            expect(getByText('out2')).toBeInTheDocument();
        });
    });

    describe('when selecting sales account', () => {
        it('should display sales account data', () => {
            const { getByText, queryByText, getByRole } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('acct1'));

            expect(getByText('acct1')).toBeInTheDocument();
            expect(getByText('65140')).toBeInTheDocument();

            expect(queryByText('out1')).not.toBeInTheDocument();
            expect(queryByText('out2')).not.toBeInTheDocument();
        });

        it('should search rsns', () => {
            const { getByText, getByRole } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('acct1'));

            expect(searchRsns).toHaveBeenCalled();
        });
    });

    describe('when selecting sales outlet', () => {
        it('should display sales outlet data', () => {
            const { getByText, queryByText, getByRole } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('out1'));

            expect(getByText('out1')).toBeInTheDocument();
            expect(getByText('65140 / 1')).toBeInTheDocument();

            expect(queryByText('acct1')).not.toBeInTheDocument();
            expect(queryByText('out2')).not.toBeInTheDocument();
        });

        it('should search rsns', () => {
            const { getByText, getByRole } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('out1'));

            expect(searchRsns).toHaveBeenCalled();
        });
    });

    describe('when rsns search loading', () => {
        it('should display loading spinner', async () => {
            const { getByText, getByRole, rerender, getByTestId } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('acct1'));

            rerender(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                    rsnsSearchLoading
                />
            );

            expect(getByTestId('rsnsLoading')).toBeInTheDocument();
        });
    });

    describe('when rsns have loaded', () => {
        it('should show rsn count', () => {
            const { getByText, getByRole, rerender } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('acct1'));

            rerender(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                    rsnsSearchResults={rsnsSearchResults}
                />
            );

            expect(getByText('3')).toBeInTheDocument();
        });

        it('should format oldest rsn date and show in account and rsn tables', () => {
            const { getByText, getAllByText, getByRole, rerender } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('acct1'));

            rerender(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                    rsnsSearchResults={rsnsSearchResults}
                />
            );

            expect(getAllByText('11 Feb 2020')).toHaveLength(2);
        });

        it('should show all rsns', () => {
            const { getByText, getByRole, rerender } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('acct1'));

            rerender(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                    rsnsSearchResults={rsnsSearchResults}
                />
            );

            expect(getByText('138588')).toBeInTheDocument();
            expect(getByText('140269')).toBeInTheDocument();
            expect(getByText('138822')).toBeInTheDocument();
        });

        describe('when outlet selected', () => {
            it('should show outlet country', () => {
                const { getByText, getByRole, rerender } = render(
                    <ExportRsns
                        {...defaultProps}
                        salesAccountsSearchResults={salesAccountsSearchResults}
                        salesOutletsSearchResults={salesOutletsSearchResults}
                    />
                );

                const typeaheadButton = getByRole('button');

                fireEvent.click(typeaheadButton);
                fireEvent.click(getByText('out2'));

                rerender(
                    <ExportRsns
                        {...defaultProps}
                        salesAccountsSearchResults={salesAccountsSearchResults}
                        salesOutletsSearchResults={salesOutletsSearchResults}
                        rsnsSearchResults={rsnsSearchResults}
                    />
                );

                expect(getByText('United States Of America')).toBeInTheDocument();
            });
        });

        it('should format rsn dates', () => {
            const { getByText, getAllByText, getByRole, rerender } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('acct1'));

            rerender(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                    rsnsSearchResults={rsnsSearchResults}
                />
            );

            expect(getByText('18 Mar 2020')).toBeInTheDocument();
            expect(getByText('29 Dec 2020')).toBeInTheDocument();
            expect(getAllByText('11 Feb 2020')).toHaveLength(2);
        });

        it('should disable export button when no rsn selected', () => {
            const { getAllByText, getByText, getByRole, rerender } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('acct1'));

            rerender(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                    rsnsSearchResults={rsnsSearchResults}
                />
            );

            const exportButton = getAllByText('Make Export Return')[1].closest('button');

            expect(exportButton).toBeDisabled();
        });

        it('should enable export button when rsn is selected', () => {
            const { getAllByText, getByText, getByRole, getAllByTestId, rerender } = render(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                />
            );

            const typeaheadButton = getByRole('button');

            fireEvent.click(typeaheadButton);
            fireEvent.click(getByText('acct1'));

            rerender(
                <ExportRsns
                    {...defaultProps}
                    salesAccountsSearchResults={salesAccountsSearchResults}
                    salesOutletsSearchResults={salesOutletsSearchResults}
                    rsnsSearchResults={rsnsSearchResults}
                />
            );

            const checkbox = getAllByTestId('rsnCheckbox')[0].querySelector(
                'input[type="checkbox"]'
            );

            expect(checkbox).toHaveProperty('checked', false);

            fireEvent.click(checkbox);

            const exportButton = getAllByText('Make Export Return')[1].closest('button');

            expect(exportButton).toBeEnabled();
        });
    });
});
