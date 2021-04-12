/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent } from '@testing-library/react';
import render from '../../test-utils';
import ExportReturn from '../exportReturns/ExportReturn';

const exportReturn = {
    carrierCode: 'DHL',
    returnId: 43,
    dateCreated: '2021-04-08T15:31:37.0000000',
    currency: 'EUR',
    accountId: 6480,
    hubId: 1,
    outletNumber: 63,
    dateDispatched: null,
    dateCancelled: null,
    carrierRef: null,
    terms: null,
    numPallets: 0,
    numCartons: 0,
    grossWeightKg: 0,
    grossDimsM3: 0,
    madeIntercompanyInvoices: 'Y',
    dateProcessed: '2021-04-08T15:32:24.0000000',
    returnForCredit: 'N',
    exportCustomsEntryCode: null,
    exportCustomsCodeDate: null,
    exportReturnDetails: [
        {
            returnId: 43,
            rsnNumber: 92544,
            articleNumber: 'PACK 1060/P',
            lineNo: 1,
            qty: 1,
            description: '3K Acoustic Array ARTIKULAT - Passive - Silver',
            customsValue: 421.28,
            baseCustomsValue: 280.85,
            tariffId: 192,
            expInvDocumentType: 'I',
            expInvDocumentNumber: 483493,
            expInvDate: '2007-05-10T14:43:15.0000000',
            numCartons: 1,
            weight: 12,
            width: 12,
            height: 12,
            depth: 12
        }
    ],
    raisedBy: {
        id: 3333,
        fullName: 'USER NAME'
    },
    salesOutlet: {
        accountId: 1111,
        outletNumber: 63,
        name: 'OUTLET NAME',
        salesCustomerId: 111,
        countryCode: 'ES',
        countryName: 'Spain',
        dateInvalid: null
    },
    links: [
        {
            href: '/inventory/exports/returns/43',
            rel: 'self'
        }
    ]
};

const defaultProps = {
    updateExportReturn: jest.fn(),
    makeIntercompanyInvoices: jest.fn(),
    clearMakeIntercompanyInvoicesErrors: jest.fn(),
    setMakeIntercompanyInvoicesMessageVisible: jest.fn()
};

describe('<ExportReturn />', () => {
    describe('when export return loading', () => {
        it('should show loading spinner', () => {
            const { getByRole } = render(<ExportReturn {...defaultProps} exportReturnLoading />);

            expect(getByRole('progressbar')).toBeInTheDocument();
        });
    });

    describe('when make intercompany invoices is processing', () => {
        it('should show loading spinner', () => {
            const { getByRole } = render(
                <ExportReturn {...defaultProps} makeIntercompanyInvoicesWorking />
            );

            expect(getByRole('progressbar')).toBeInTheDocument();
        });
    });

    describe('when intercompany error message is present', () => {
        it('should show error message', () => {
            const { getByText } = render(
                <ExportReturn
                    {...defaultProps}
                    makeIntercompanyInvoicesErrorMessage="ERROR MESSAGE"
                />
            );

            expect(getByText('ERROR MESSAGE')).toBeInTheDocument();
        });
    });

    describe('when loaded', () => {
        it('should render export return', () => {
            const { getByText } = render(
                <ExportReturn {...defaultProps} exportReturn={exportReturn} />
            );

            expect(getByText('63 - OUTLET NAME')).toBeInTheDocument();
        });

        it('should render export return details', () => {
            const { getByText } = render(
                <ExportReturn {...defaultProps} exportReturn={exportReturn} />
            );

            // console.log(debug(undefined, 300000));

            expect(getByText('PACK 1060/P')).toBeInTheDocument();
        });

        it('should render buttons', () => {
            const { getByText } = render(
                <ExportReturn {...defaultProps} exportReturn={exportReturn} />
            );

            expect(getByText('Save')).toBeInTheDocument();
            expect(getByText('Make Intercompany Invoices')).toBeInTheDocument();
            expect(getByText('Cancel Export Return')).toBeInTheDocument();
        });

        it('should render tabs with first tab selected', () => {
            const { getByRole, getByText } = render(
                <ExportReturn {...defaultProps} exportReturn={exportReturn} />
            );

            expect(getByRole('tablist')).toBeInTheDocument();
            // weight should only show on first tab
            expect(getByText('280.85')).toBeInTheDocument();
        });

        it('should change tab on click', () => {
            const { getAllByRole, getByText } = render(
                <ExportReturn {...defaultProps} exportReturn={exportReturn} />
            );

            const tabs = getAllByRole('tab');

            fireEvent.click(tabs[1]);

            // expInvDocumentNumber should only be in second tab
            expect(getByText('483493')).toBeInTheDocument();
        });

        it('should calculate correct dimensions', () => {
            const { getByDisplayValue, getByRole } = render(
                <ExportReturn {...defaultProps} exportReturn={exportReturn} />
            );

            const button = getByRole('button', { name: 'Calculate Dimensions from RSNs' });

            fireEvent.click(button);

            expect(getByDisplayValue('0.001728')).toBeInTheDocument();
        });

        it('should disable save button by default', () => {
            const { getByRole } = render(
                <ExportReturn {...defaultProps} exportReturn={exportReturn} />
            );

            const button = getByRole('button', { name: 'Save' });

            expect(button).toBeDisabled();
        });
    });
});
