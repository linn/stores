import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../test-utils';
import Parcel from '../parcels/Parcel';

afterEach(cleanup);

const fetchItems = jest.fn();
const addItem = jest.fn();
const updateItem = jest.fn();
const setEditStatus = jest.fn();
const setSnackbarVisible = jest.fn();
const searchSuppliers = jest.fn();
const searchCarriers = jest.fn();
const clearSuppliersSearch = jest.fn();
const clearCarriersSearch = jest.fn();
const history = {
    push: jest.fn()
};

const item = {
    parcelNumber: 52828,
    supplierId: 29696,
    countryCode: 'DE',
    carrierId: 8239,
    dateCreated: '2019-09-29T00:00:00.0000000',
    dateReceived: '2019-10-13T00:00:00.0000000',
    supplierInvoiceNo: '81107829',
    consignmentNo: '3879 8707 205',
    cartonCount: 3,
    palletCount: 0,
    weight: 15,
    checkedById: '33067',
    comments: 'TVS + PACKING MATERIAL',
    importBookNo: 136890,
    links: { href: '/logistics/parcels/52828', rel: 'self' }
};

const suppliers = [
    { id: 29696, name: 'IGF INVOICE FINANCE LTD', countryCode: 'DE', approvedCarrier: 'N' },
    { id: 2222, name: 'VIRGIN RETAIL LTD', countryCode: 'GB', approvedCarrier: 'N' },
    { id: 8239, name: 'SERCO-RYAN LTD', countryCode: 'FR', approvedCarrier: 'Y' },
    { id: 3333, name: 'PALLETOWER (GB) LTD', countryCode: 'GB', approvedCarrier: 'Y' }
];

const employees = [
    {
        fullName: 'Adam C',
        id: 33067
    },
    {
        fullName: '118 person',
        id: 118
    },
    {
        fullName: 'no person',
        id: -1
    }
];

test('On Create - page renders all fields without crashing...', () => {
    const { getByText, queryByText } = render(
        <Parcel
            editStatus="create"
            searchCarriers={searchCarriers}
            searchSuppliers={searchSuppliers}
            clearCarriersSearch={clearCarriersSearch}
            clearSuppliersSearch={clearSuppliersSearch}
            history={history}
            addItem={addItem}
            updateItem={updateItem}
            setEditStatus={setEditStatus}
            setSnackbarVisible={setSnackbarVisible}
            userNumber={118}
        />
    );

    expect(queryByText('Parcel Number')).toBeNull();
    expect(getByText('Supplier')).toBeInTheDocument();
    expect(getByText('Supplier Country')).toBeInTheDocument();
    expect(getByText('Carrier')).toBeInTheDocument();
    expect(getByText('Date Created')).toBeInTheDocument();
    expect(getByText('Date Received')).toBeInTheDocument();
    expect(getByText('Supplier Invoice Number(s)')).toBeInTheDocument();
    expect(getByText('Consignment Number')).toBeInTheDocument();
    expect(getByText('Number of cartons')).toBeInTheDocument();
    expect(getByText('Number of pallets')).toBeInTheDocument();
    expect(getByText('Weight')).toBeInTheDocument();
    expect(getByText('Checked by')).toBeInTheDocument();
    expect(getByText('Comments')).toBeInTheDocument();
});

test('On View -  page renders populated fields', () => {
    const { getByText, getByDisplayValue } = render(
        <Parcel
            item={item}
            editStatus="view"
            fetchItems={fetchItems}
            searchCarriers={searchCarriers}
            searchSuppliers={searchSuppliers}
            clearCarriersSearch={clearCarriersSearch}
            clearSuppliersSearch={clearSuppliersSearch}
            history={history}
            suppliers={suppliers}
            employees={employees}
            addItem={addItem}
            updateItem={updateItem}
            setEditStatus={setEditStatus}
            setSnackbarVisible={setSnackbarVisible}
            userNumber={118}
        />
    );
    expect(getByText('Parcel Number')).toBeInTheDocument();
    expect(getByDisplayValue('52828')).toBeInTheDocument();

    expect(getByText('Supplier')).toBeInTheDocument();
    expect(getByDisplayValue(`${suppliers[0].id} - ${suppliers[0].name}`)).toBeInTheDocument();

    expect(getByText('Supplier Country')).toBeInTheDocument();
    expect(getByDisplayValue('DE')).toBeInTheDocument();

    expect(getByText('Carrier')).toBeInTheDocument();
    expect(getByDisplayValue(`${suppliers[0].id} - ${suppliers[0].name}`)).toBeInTheDocument();

    expect(getByText('Date Created')).toBeInTheDocument();
    expect(getByText('Date Received')).toBeInTheDocument();

    expect(getByText('Supplier Invoice Number(s)')).toBeInTheDocument();
    expect(getByDisplayValue('81107829')).toBeInTheDocument();

    expect(getByText('Consignment Number')).toBeInTheDocument();
    expect(getByDisplayValue(item.consignmentNo)).toBeInTheDocument();

    expect(getByText('Number of cartons')).toBeInTheDocument();
    expect(getByDisplayValue(item.cartonCount)).toBeInTheDocument();

    expect(getByText('Number of pallets')).toBeInTheDocument();
    expect(getByDisplayValue(item.palletCount)).toBeInTheDocument();

    expect(getByText('Weight')).toBeInTheDocument();
    expect(getByDisplayValue(item.weight)).toBeInTheDocument();

    expect(getByText('Checked by')).toBeInTheDocument();
    expect(getByDisplayValue('Adam C (33067)')).toBeInTheDocument();

    expect(getByText('Comments')).toBeInTheDocument();
    expect(getByDisplayValue(item.comments)).toBeInTheDocument();

    expect(getByText('Import Book Number')).toBeInTheDocument();
    expect(getByDisplayValue(item.importBookNo)).toBeInTheDocument();
});
