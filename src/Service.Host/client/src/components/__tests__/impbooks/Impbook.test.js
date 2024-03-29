import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, screen, fireEvent } from '@testing-library/react';
import { within } from '@testing-library/dom';
import render from '../../../test-utils';
import ImportBook from '../../importBooks/ImportBook';

afterEach(cleanup);

const addItem = jest.fn();
const updateItem = jest.fn();
const setEditStatus = jest.fn();
const setSnackbarVisible = jest.fn();
const getExchangeRatesForDate = jest.fn();

const history = {
    push: jest.fn()
};
const applicationState = {};

const item = {
    id: 106111,
    parcelNumber: 52828,
    supplierId: '29696',
    carrierId: '8239',
    dateCreated: '2019-09-29T00:00:00.0000000',
    arrivalDate: '2019-10-13T00:00:00.0000000',
    createdBy: 33067,
    comments: 'TVS + PACKING MATERIAL',
    foreignCurrency: 'N',
    currency: 'GBP',
    transportId: 1234,
    transportBillNumber: '3879 8707 205',
    transactionId: 111,
    deliveryTermCode: '',
    arrivalPort: 'GLA',
    customsEntryCodePrefix: '011',
    customsEntryCode: '160674K',
    customsEntryCodeDate: new Date().toString(),
    linnDuty: 120,
    linnVat: 600,
    iprCpcNumber: null,
    eecgNumber: null,
    dateCancelled: null,
    cancelledBy: null,
    cancelledReason: '',
    countryOfOrigin: '',
    numCartons: 3,
    numPallets: 0,
    weight: 15,
    exchangeCurrency: 'GBP',
    baseCurrency: 'GBP',
    periodNumber: null,
    invoiceDate: '11/12/13',
    pva: 'Y',
    importBookInvoiceDetails: [
        { invoiceNumber: 123, invoiceValue: 1400 },
        { invoiceNumber: 124, invoiceValue: 99.01 }
    ],
    importBookOrderDetails: [
        {
            lineType: 'PO',
            orderValue: 998,
            vatValue: 294.99,
            weight: 5.5,
            dutyValue: 10.9,
            lineNumber: 1
        },
        {
            lineNumber: 3,
            lineType: 'PO',
            orderValue: 2,
            vatValue: 295.99,
            weight: 3,
            dutyValue: 0.1
        }
    ],
    importBookPostEntries: null,
    totalImportValue: 1000
};

const cpcNumbers = () => [
    {
        id: 1,
        displayText: '51 00 00'
    },
    {
        id: 2,
        displayText: '51 00 01'
    },
    {
        id: 3,
        displayText: '40 00 00'
    },
    {
        id: 4,
        displayText: '40 00 60'
    },
    {
        id: 5,
        displayText: '40 00 63'
    },
    {
        id: 6,
        displayText: 'EC Countries'
    },
    {
        id: 7,
        displayText: '40 00 09 - SAMPLES ONLY'
    },
    {
        id: 8,
        displayText: 'Low Value Shipment'
    },
    {
        id: 9,
        displayText: '40 51 00 - IPR to Free'
    },
    {
        id: 10,
        displayText: '40 00 58'
    },
    {
        id: 11,
        displayText: '61 23 F01'
    },
    {
        id: 12,
        displayText: '40 00 000'
    },
    {
        id: 13,
        displayText: '51 00 000'
    }
];

const privileges = ['potato.admin', 'import-books.admin'];

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
                getExchangeRatesForDate={getExchangeRatesForDate}
                privileges={privileges}
                loading
            />
        );

        expect(getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('On Create', () => {
    beforeEach(() => {
        render(
            <ImportBook
                editStatus="create"
                applicationState={applicationState}
                history={history}
                addItem={addItem}
                updateItem={updateItem}
                setEditStatus={setEditStatus}
                setSnackbarVisible={setSnackbarVisible}
                getExchangeRatesForDate={getExchangeRatesForDate}
                privileges={privileges}
                loading={false}
                cpcNumbers={cpcNumbers()}
            />
        );
    });

    test('import id is just shown as new', () => {
        expect(screen.getByDisplayValue('New')).toBeInTheDocument();
    });

    test('page renders all fields and tab options without crashing...', () => {
        expect(screen.getByText('Import Book Id')).toBeInTheDocument();
        expect(screen.getByText('Date Created')).toBeInTheDocument();
        expect(screen.getByText('Created by')).toBeInTheDocument();
        expect(screen.getByText('Parcel Number')).toBeInTheDocument();
        expect(screen.getByText('Supplier')).toBeInTheDocument();
        expect(screen.getByText('Supplier Country')).toBeInTheDocument();
        expect(screen.getByText('Foreign Currency')).toBeInTheDocument();
        expect(screen.getByText('Currency')).toBeInTheDocument();
        expect(screen.getByText('Total Import Value (GBP)')).toBeInTheDocument();
        const invoiceDetailsTable = screen.getByTestId('invoiceDetailsTable');
        expect(within(invoiceDetailsTable).getByText('Invoice Number')).toBeInTheDocument();
        expect(screen.getByText('Invoice Value')).toBeInTheDocument();
        expect(screen.getByText('Total Invoice Value (Currency)')).toBeInTheDocument();
        expect(screen.getByText('Carrier')).toBeInTheDocument();
        expect(screen.getByText('Mode of Transport')).toBeInTheDocument();
        expect(screen.getByText('Transport Bill Number')).toBeInTheDocument();
        expect(screen.getByText('Transaction Code')).toBeInTheDocument();
        expect(screen.getByText('Delivery Term Code')).toBeInTheDocument();
        expect(screen.getByText('Arrival Port')).toBeInTheDocument();
        expect(screen.getByText('Arrival Date')).toBeInTheDocument();
        expect(screen.getByLabelText('Number of Cartons')).toBeInTheDocument();
        expect(screen.getByText('Number of Pallets')).toBeInTheDocument();
        expect(screen.getAllByText('Weight')[1]).toBeInTheDocument();
        expect(screen.getByText('Prefix')).toBeInTheDocument();
        expect(screen.getByText('Customs Entry Code')).toBeInTheDocument();
        expect(screen.getByText('Customs Entry Date')).toBeInTheDocument();
        expect(screen.getByText('Linn Duty')).toBeInTheDocument();
        expect(screen.getByText('Linn Vat')).toBeInTheDocument();
        expect(screen.getByText('Import Book')).toBeInTheDocument();
        expect(screen.getAllByText('Order Details')[1]).toBeInTheDocument();
        expect(screen.getByText('Post Entries')).toBeInTheDocument();
        expect(screen.getByText('Comments')).toBeInTheDocument();
    });

    test('can edit fields because have right permission', () => {
        expect(screen.getByLabelText('Total Import Value (GBP)')).not.toBeDisabled();
        expect(screen.getByLabelText('Parcel Number')).not.toBeDisabled();
        expect(screen.getByLabelText('Number of Cartons')).not.toBeDisabled();
        expect(screen.getByLabelText('Number of Pallets')).not.toBeDisabled();
        expect(screen.getByLabelText('Weight')).not.toBeDisabled();
    });

    test('but should never be able to edit certain fields', () => {
        expect(screen.getByLabelText('Supplier Country')).toBeDisabled();
        expect(screen.getByLabelText('EC (EU) Member')).toBeDisabled();
        expect(screen.getByLabelText('Total Invoice Value (Currency)')).toBeDisabled();
        expect(screen.getByLabelText('Import Book Id')).toBeDisabled();
    });
});

describe('When dont have right privilege', () => {
    beforeEach(() => {
        render(
            <ImportBook
                editStatus="create"
                applicationState={applicationState}
                history={history}
                addItem={addItem}
                updateItem={updateItem}
                setEditStatus={setEditStatus}
                setSnackbarVisible={setSnackbarVisible}
                getExchangeRatesForDate={getExchangeRatesForDate}
                privileges={['not-right.priv']}
                loading={false}
                cpcNumbers={cpcNumbers()}
            />
        );
    });

    test('cannot edit fields', () => {
        expect(screen.getByLabelText('Parcel Number')).toBeDisabled();
        expect(screen.getByLabelText('Total Import Value (GBP)')).toBeDisabled();
        expect(screen.getByLabelText('Total Invoice Value (Currency)')).toBeDisabled();
        expect(screen.getByLabelText('Number of Cartons')).toBeDisabled();
        expect(screen.getByLabelText('Number of Pallets')).toBeDisabled();
        expect(screen.getByLabelText('Weight')).toBeDisabled();
    });
});

describe('When editing', () => {
    beforeEach(() => {
        render(
            <ImportBook
                editStatus="edit"
                applicationState={applicationState}
                history={history}
                addItem={addItem}
                updateItem={updateItem}
                setEditStatus={setEditStatus}
                setSnackbarVisible={setSnackbarVisible}
                getExchangeRatesForDate={getExchangeRatesForDate}
                privileges={privileges}
                loading={false}
                item={item}
                cpcNumbers={cpcNumbers()}
                e
            />
        );
    });

    test('invoice values are shown', () => {
        const table = screen.getByTestId('invoiceDetailsTable');
        expect(within(table).getByText('99.01')).toBeInTheDocument();
        expect(within(table).getByText('1400')).toBeInTheDocument();
    });

    test('parcel no is displayed', () => {
        expect(screen.getByLabelText('Parcel Number')).toHaveDisplayValue(52828);
    });

    test('total invoice value is right', () => {
        expect(screen.getByLabelText('Total Invoice Value (Currency)')).toHaveDisplayValue(
            '1499.01'
        );
    });
});

describe('When clicking through to second tab', () => {
    beforeEach(() => {
        render(
            <ImportBook
                editStatus="edit"
                applicationState={applicationState}
                history={history}
                addItem={addItem}
                updateItem={updateItem}
                setEditStatus={setEditStatus}
                setSnackbarVisible={setSnackbarVisible}
                getExchangeRatesForDate={getExchangeRatesForDate}
                privileges={privileges}
                loading={false}
                item={item}
                cpcNumbers={cpcNumbers()}
            />
        );

        const orderDetailsTabButton = screen.getAllByText('Order Details')[1];
        fireEvent.click(orderDetailsTabButton);
    });

    test('Remaining total (based on import value) is correct', () => {
        expect(screen.getByLabelText('Remaining Total')).toHaveDisplayValue('0');
    });

    test('Remaining duty total is correct', () => {
        expect(screen.getByLabelText('Remaining Duty Total')).toHaveDisplayValue('109');
    });

    test('Remaining weight is correct', () => {
        expect(screen.getByLabelText('Remaining Weight')).toHaveDisplayValue('6.5');
    });
});
