import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../test-utils';
import ParcelsSearch from '../parcels/ParcelsSearch';

afterEach(cleanup);

const fetchItems = jest.fn();
const searchSuppliers = jest.fn();
const searchCarriers = jest.fn();
const clearSuppliersSearch = jest.fn();
const clearCarriersSearch = jest.fn();
const history = {
    push: jest.fn()
};
const applicationState = {};

const items = [
    {
        parcelNumber: 52828,
        supplierId: 29696,
        carrierId: 8239,
        dateCreated: '2019-09-29T00:00:00.0000000',
        supplierInvoiceNo: '9792',
        consignmentNo: '3879 8707 205',
        comments: 'TVS + PACKING MATERIAL',
        links: { href: '/logistics/parcels/52828', rel: 'self' }
    },
    {
        parcelNumber: 11111,
        supplierId: 2222,
        carrierId: 3333,
        dateCreated: '2014-01-01T00:00:00.0000000',
        supplierInvoiceNo: '4444',
        consignmentNo: '5555 5555 555',
        comments: 'PO738401',
        links: { href: '/logistics/parcels/11111', rel: 'self' }
    }
];

const suppliers = [
    { id: 29696, name: 'IGF INVOICE FINANCE LTD', countryCode: 'GB', approvedCarrier: 'N' },
    { id: 2222, name: 'VIRGIN RETAIL LTD', countryCode: 'GB', approvedCarrier: 'N' },
    { id: 8239, name: 'SERCO-RYAN LTD', countryCode: 'FR', approvedCarrier: 'Y' },
    { id: 3333, name: 'PALLETOWER (GB) LTD', countryCode: 'GB', approvedCarrier: 'Y' }
];

test('Search page renders all search fields without crashing...', () => {
    const { getByText } = render(
        <ParcelsSearch
            items={[]}
            applicationState={applicationState}
            fetchItems={fetchItems}
            searchCarriers={searchCarriers}
            searchSuppliers={searchSuppliers}
            clearCarriersSearch={clearCarriersSearch}
            clearSuppliersSearch={clearSuppliersSearch}
            history={history}
        />
    );
    expect(getByText('Parcels')).toBeInTheDocument();
    expect(getByText('Supplier')).toBeInTheDocument();
    expect(getByText('Carrier')).toBeInTheDocument();
    expect(getByText('Date Created')).toBeInTheDocument();
    expect(getByText('Supplier Inv No')).toBeInTheDocument();
    expect(getByText('Consignment Number')).toBeInTheDocument();
    expect(getByText('Comments')).toBeInTheDocument();
});

test('Search page renders search results + finds correct supplier and carrier names for eachr result', () => {
    const { getByText } = render(
        <ParcelsSearch
            items={items}
            applicationState={applicationState}
            fetchItems={fetchItems}
            searchCarriers={searchCarriers}
            searchSuppliers={searchSuppliers}
            clearCarriersSearch={clearCarriersSearch}
            clearSuppliersSearch={clearSuppliersSearch}
            history={history}
            suppliers={suppliers}
        />
    );
    expect(getByText(items[0].parcelNumber)).toBeInTheDocument();
    expect(getByText(items[1].parcelNumber)).toBeInTheDocument();

    //suppliers
    expect(getByText(`${suppliers[0].id} - ${suppliers[0].name}`)).toBeInTheDocument();
    expect(getByText(`${suppliers[1].id} - ${suppliers[1].name}`)).toBeInTheDocument();

    //carriers
    expect(getByText(`${suppliers[2].id} - ${suppliers[2].name}`)).toBeInTheDocument();
    expect(getByText(`${suppliers[3].id} - ${suppliers[3].name}`)).toBeInTheDocument();
});

test('Before suppliers have loaded, search page can still render search results + loading for carrier & supplier names', () => {
    const { getByText } = render(
        <ParcelsSearch
            items={items}
            applicationState={applicationState}
            fetchItems={fetchItems}
            searchCarriers={searchCarriers}
            searchSuppliers={searchSuppliers}
            clearCarriersSearch={clearCarriersSearch}
            clearSuppliersSearch={clearSuppliersSearch}
            history={history}
        />
    );
    expect(getByText(items[0].parcelNumber)).toBeInTheDocument();
    expect(getByText(items[1].parcelNumber)).toBeInTheDocument();

    // suppliers
    expect(getByText(`${suppliers[0].id} - loading..`)).toBeInTheDocument();
    expect(getByText(`${suppliers[1].id} - loading..`)).toBeInTheDocument();

    //carriers
    expect(getByText(`${suppliers[2].id} - loading..`)).toBeInTheDocument();
    expect(getByText(`${suppliers[3].id} - loading..`)).toBeInTheDocument();

});

//this might change to check disabled if permissions change
test('create button is available', () => {
    const { getByText } = render(
        <ParcelsSearch
            items={[]}
            applicationState={applicationState}
            fetchItems={fetchItems}
            searchCarriers={searchCarriers}
            searchSuppliers={searchSuppliers}
            clearCarriersSearch={clearCarriersSearch}
            clearSuppliersSearch={clearSuppliersSearch}
            history={history}
        />
    );
    expect(getByText('Create')).toBeInTheDocument();
});
