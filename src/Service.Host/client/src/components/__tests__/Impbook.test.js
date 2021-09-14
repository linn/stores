import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, screen } from '@testing-library/react';
import render from '../../test-utils';
import ImportBook from '../importBooks/ImportBook';
import {
    InputField,
    Dropdown,
    Typeahead,
    LinkButton,
    SearchInputField,
    TableWithInlineEditing
} from '@linn-it/linn-form-components-library';

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
const applicationState = {};

const item = {
    impbookNumber: 106111,
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
    importBookNos: [136890, 22222],
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

const privileges = ['potato.admin', 'import-books.admin'];

// inputfield getByLabelText


jest.mock('../importBooks/tabs/ImpBookTab', () => () => <p>Parcel Number</p>);

describe('When loading', () => {
    test('On loading, loads spinner', () => {
        const { getByRole } = render(
            <ImportBook
                editStatus="edit"
                applicationState={applicationState}
                history={history}
                addItem={addItem}
                updateItem={updateItem}
                setEditStatus={setEditStatus}
                setSnackbarVisible={setSnackbarVisible}
                privileges={privileges}
                loading
            />
        );

        expect(getByRole('progressbar')).toBeInTheDocument();
    });
});

test('On Create - page renders all fields and tab options without crashing...', () => {
    const { getByText, queryByText } = render(
        <ImportBook
            editStatus="create"
            applicationState={applicationState}
            history={history}
            addItem={addItem}
            updateItem={updateItem}
            setEditStatus={setEditStatus}
            setSnackbarVisible={setSnackbarVisible}
            privileges={privileges}
            loading={false}

            // userNumber={118}
            // searchCarriers={searchCarriers}
            // searchSuppliers={searchSuppliers}
            // clearCarriersSearch={clearCarriersSearch}
            // clearSuppliersSearch={clearSuppliersSearch}
        />
    );

    expect(getByText('Import Book Id')).toBeInTheDocument();
    // expect(getByText('Date Created')).toBeInTheDocument();
    // expect(getByText('Created by')).toBeInTheDocument();
    expect(getByText('Parcel Number')).toBeInTheDocument();
    // expect(getByText('Supplier')).toBeInTheDocument();
    // expect(getByText('Supplier Country')).toBeInTheDocument();
    // expect(getByText('Foreign Currency')).toBeInTheDocument();
    // expect(getByText('Currency')).toBeInTheDocument();
    // expect(getByText('Total Import Value')).toBeInTheDocument();
    // expect(getByText('Invoice Number')).toBeInTheDocument();
    // expect(getByText('Invoice Value')).toBeInTheDocument();
    // expect(getByText('Total Invoice Value')).toBeInTheDocument();
    // expect(getByText('Carrier')).toBeInTheDocument();
    // expect(getByText('Mode of Transport')).toBeInTheDocument();
    // expect(getByText('Transport Bill Number')).toBeInTheDocument();
    // expect(getByText('Transaction Code')).toBeInTheDocument();
    // expect(getByText('Delivery Term Code')).toBeInTheDocument();
    // expect(getByText('Arrival Port')).toBeInTheDocument();
    // expect(getByText('Arrival Date')).toBeInTheDocument();
    // expect(getByText('Number of cartons')).toBeInTheDocument();
    // expect(getByText('Number of pallets')).toBeInTheDocument();
    // expect(getByText('Weight')).toBeInTheDocument();
    // expect(getByText('Prefix')).toBeInTheDocument();
    // expect(getByText('Customs Entry Code')).toBeInTheDocument();
    // expect(getByText('Customs Entry Date')).toBeInTheDocument();
    // expect(getByText('Linn Duty')).toBeInTheDocument();
    // expect(getByText('Linn Vat')).toBeInTheDocument();

    // expect(getByText('Import Book')).toBeInTheDocument();
    // expect(getByText('Order Details')).toBeInTheDocument();
    // expect(getByText('Post Entries')).toBeInTheDocument();
    // expect(getByText('Comments')).toBeInTheDocument();
});

// test('On View -  page renders populated fields', () => {
//     const { getByText, getByDisplayValue } = render(
//         <ImportBook
//             item={item}
//             editStatus="view"
//             applicationState={applicationState}
//             fetchItems={fetchItems}
//             searchCarriers={searchCarriers}
//             searchSuppliers={searchSuppliers}
//             clearCarriersSearch={clearCarriersSearch}
//             clearSuppliersSearch={clearSuppliersSearch}
//             history={history}
//             suppliers={suppliers}
//             employees={employees}
//             addItem={addItem}
//             updateItem={updateItem}
//             setEditStatus={setEditStatus}
//             setSnackbarVisible={setSnackbarVisible}
//             userNumber={118}

//             itemId={106111}
//             item={item}
//             privileges={privileges}
//         />
//     );
//     expect(getByText('Parcel Number')).toBeInTheDocument();
//     expect(getByDisplayValue('52828')).toBeInTheDocument();

//     expect(getByText('Supplier')).toBeInTheDocument();
//     expect(getByDisplayValue(`${suppliers[0].id} - ${suppliers[0].name}`)).toBeInTheDocument();

//     expect(getByText('Supplier Country')).toBeInTheDocument();
//     expect(getByDisplayValue('DE')).toBeInTheDocument();

//     expect(getByText('Carrier')).toBeInTheDocument();
//     expect(getByDisplayValue(`${suppliers[0].id} - ${suppliers[0].name}`)).toBeInTheDocument();

//     expect(getByText('Date Created')).toBeInTheDocument();
//     expect(getByText('Date Received')).toBeInTheDocument();

//     expect(getByText('Supplier Invoice Number(s)')).toBeInTheDocument();
//     expect(getByDisplayValue('81107829')).toBeInTheDocument();

//     expect(getByText('Consignment Number')).toBeInTheDocument();
//     expect(getByDisplayValue(item.consignmentNo)).toBeInTheDocument();

//     expect(getByText('Number of cartons')).toBeInTheDocument();
//     expect(getByDisplayValue(item.cartonCount)).toBeInTheDocument();

//     expect(getByText('Number of pallets')).toBeInTheDocument();
//     expect(getByDisplayValue(item.palletCount)).toBeInTheDocument();

//     expect(getByText('Weight')).toBeInTheDocument();
//     expect(getByDisplayValue(item.weight)).toBeInTheDocument();

//     expect(getByText('Checked by')).toBeInTheDocument();
//     expect(getByDisplayValue('Adam C (33067)')).toBeInTheDocument();

//     expect(getByText('Comments')).toBeInTheDocument();
//     expect(getByDisplayValue(item.comments)).toBeInTheDocument();

//     expect(getByDisplayValue(item.importBookNos[0])).toBeInTheDocument();
//     expect(getByDisplayValue(item.importBookNos[1])).toBeInTheDocument();
// });
