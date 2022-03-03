/**
 * @jest-environment jest-environment-jsdom-sixteen
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen } from '@testing-library/react';
import render from '../../../test-utils';
import ExportConsignments from '../../consignments/ExportConsignments';

const hubs = [
    {
        hubId: 1,
        description: 'Hubby McHubFace'
    }
]

const items = [
    {
        consignmentId: 1,
        customerName: 'Euro Hifi',
        carrier: 'DHL',
        carrierRef: null,
        masterCarrierRef: null,
        customsEntryCodePrefix: null,
        customsEntryCode: null,
        customsEntryCodeDate: null
    },
    {
        consignmentId: 2,
        customerName: 'Audio Berlin',
        carrier: 'DHL',
        carrierRef: null,
        masterCarrierRef: null,
        customsEntryCodePrefix: null,
        customsEntryCode: null,
        customsEntryCodeDate: null
    },
    {
        consignmentId: 3,
        customerName: 'Studio Riga',
        carrier: 'DHL',
        carrierRef: null,
        masterCarrierRef: null,
        customsEntryCodePrefix: null,
        customsEntryCode: null,
        customsEntryCodeDate: null
    }
];

const searchConsignments = jest.fn();
const updateConsignment = jest.fn();

test('Before search button pressed', () => {
    const { getByText, queryByText } = render(
        <ExportConsignments
            hubs={hubs}
            hubsLoading={false}
            options={null}
            requestErrors={null}
            loading={false}
            consignments={[]}
            searchConsignments={searchConsignments}
            updateConsignment={updateConsignment}
        />
    );
    expect(getByText('From Date')).toBeInTheDocument();
    expect(getByText('To Date')).toBeInTheDocument();
    expect(getByText('No Consignments')).toBeInTheDocument();
    expect(queryByText('Master Carrier Ref')).toBeNull();
});

test('With Search Results', () => {
    const { getByText, queryByText } = render(
        <ExportConsignments
            hubs={hubs}
            hubsLoading={false}
            options={null}
            requestErrors={null}
            loading={false}
            consignments={items}
            searchConsignments={searchConsignments}
            updateConsignment={updateConsignment}
        />
    );
    expect(getByText('From Date')).toBeInTheDocument();
    expect(getByText('To Date')).toBeInTheDocument();
    expect(queryByText('No Consignments')).toBeNull();
    expect(getByText('Master Carrier Ref')).toBeInTheDocument();
});
