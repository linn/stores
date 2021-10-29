/**
 * @jest-environment jest-environment-jsdom-sixteen
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen } from '@testing-library/react';
import render from '../../../test-utils';
import Consignment from '../../consignments/Consignment';

const getConsignment = jest.fn();
const setEditStatus = jest.fn();
const getHub = jest.fn();
const clearHub = jest.fn();
const getCarrier = jest.fn();
const updateItem = jest.fn();
const getShippingTerm = jest.fn();
const clearShippingTerm = jest.fn();
const clearConsignmentErrors = jest.fn();
const userNumber = 12345;
const printConsignmentLabel = jest.fn();
const clearConsignmentLabelData = jest.fn();
const printDocuments = jest.fn();
const printDocumentsClearData = jest.fn();
const getConsignmentPackingList = jest.fn();
const clearConsignmentPackingList = jest.fn();
const createConsignment = jest.fn();
const addConsignment = jest.fn();
const openConsignments = [
    { id: 1, displayText: 'c1' },
    { id: 2, displayText: 'c2' }
];
const getSalesOutlets = jest.fn();

const item = {
    salesAccountId: 21,
    consignmentId: 101101,
    status: 'L',
    terms: 'DDP',
    shippingMethod: 'S',
    carrier: 'Bus',
    customerName: 'Fred',
    customsEntryCodePrefix: 'Prefix 1',
    customsEntryCode: 'Entry Code 1',
    despatchLocationCode: 'LINN1',
    hubId: 1,
    pallets: [
        {
            weight: 56.56,
            height: 140,
            depth: 180,
            width: 200,
            palletNumber: 4
        }
    ],
    items: [
        {
            itemNumber: 1,
            itemType: 'S',
            serialNumber: 1543097,
            quantity: 0.5,
            maybeHalfAPair: 'N',
            weight: 26.75,
            height: 40,
            depth: 108,
            width: 60,
            containerType: 'MAJIK140',
            containerNumber: 1,
            palletNumber: null,
            orderNumber: 607355,
            orderLine: 1234,
            itemBaseWeight: null,
            itemDescription: 'MAJIK 140/WH',
            rsnNumber: null
        }
    ]
};

const defaultRender = props =>
    render(
        <Consignment
            item={null}
            history={{ push: jest.fn() }}
            getConsignment={getConsignment}
            setEditStatus={setEditStatus}
            getHub={getHub}
            clearHub={clearHub}
            userNumber={userNumber}
            getCarrier={getCarrier}
            updateItem={updateItem}
            getShippingTerm={getShippingTerm}
            clearShippingTerm={clearShippingTerm}
            clearConsignmentErrors={clearConsignmentErrors}
            printConsignmentLabel={printConsignmentLabel}
            clearConsignmentLabelData={clearConsignmentLabelData}
            printDocuments={printDocuments}
            printDocumentsClearData={printDocumentsClearData}
            getConsignmentPackingList={getConsignmentPackingList}
            clearConsignmentPackingList={clearConsignmentPackingList}
            createConsignment={createConsignment}
            addConsignment={addConsignment}
            openConsignments={openConsignments}
            getSalesOutlets={getSalesOutlets}
            //eslint-disable-next-line react/jsx-props-no-spreading
            {...props}
        />
    );

afterEach(() => cleanup());

describe('On initial load...', () => {
    beforeEach(() => {
        defaultRender({ startingTab: 0 });
    });

    test('should show title', () => {
        expect(screen.getByText('Consignment')).toBeInTheDocument();
    });

    test('should show drop down and options', () => {
        expect(screen.getByText('Select open consignment')).toBeInTheDocument();
        expect(screen.getByText('c1')).toBeInTheDocument();
        expect(screen.getByText('c2')).toBeInTheDocument();
    });

    test('actions should be disabled', () => {
        expect(screen.getByRole('button', { name: 'Edit' })).toHaveClass('Mui-disabled');
        expect(screen.getByRole('button', { name: 'Close Consignment' })).toHaveClass(
            'Mui-disabled'
        );
    });

    test('should show select consignment', () => {
        expect(screen.getByLabelText('Select Consignment By Id')).toBeInTheDocument();
        expect(screen.getByRole('button', { name: 'Show Consignment' })).toBeInTheDocument();
    });
});

describe('should render detail of selected consignment', () => {
    beforeEach(() => {
        defaultRender({
            item,
            carrier: { name: 'Anne Bus Co' },
            hub: { description: 'Dieppe' }
        });

        const details = screen.getByText('Details');
        fireEvent.click(details);
    });

    test('should show details', () => {
        expect(screen.getByText('101101 Fred')).toBeInTheDocument();
        expect(screen.getByText('21 Fred')).toBeInTheDocument();
        expect(screen.getByText('Status')).toBeInTheDocument();
        expect(screen.getByText('Open')).toBeInTheDocument();
        expect(screen.getByText('Freight')).toBeInTheDocument();
        expect(screen.getByText('Surface')).toBeInTheDocument();
        expect(screen.getByText('Carrier')).toBeInTheDocument();
        expect(screen.getByText('LINN1')).toBeInTheDocument();
        expect(screen.getByText('1 - Dieppe')).toBeInTheDocument();
        expect(screen.getByText('Bus - Anne Bus Co')).toBeInTheDocument();
        expect(screen.getByText('Prefix 1 Entry Code 1')).toBeInTheDocument();
    });
});

describe('should render items and pallets of selected consignment', () => {
    beforeEach(() => {
        defaultRender({
            item
        });

        const tab = screen.getByText('Items');
        fireEvent.click(tab);
    });

    test('should show item', () => {
        expect(screen.getByText('Sealed Box')).toBeInTheDocument();
        expect(screen.getByText('MAJIK 140/WH')).toBeInTheDocument();
        expect(screen.getByText('MAJIK140')).toBeInTheDocument();
        const lineNumberAndBoxNumber = screen.getAllByText('1');
        expect(lineNumberAndBoxNumber[0]).toBeInTheDocument();
        expect(lineNumberAndBoxNumber[1]).toBeInTheDocument();
        expect(screen.getByText('1234')).toBeInTheDocument();
        expect(screen.getByText('607355')).toBeInTheDocument();
        expect(screen.getByText('0.5')).toBeInTheDocument();
        expect(screen.getByText('40')).toBeInTheDocument();
        expect(screen.getByText('26.75')).toBeInTheDocument();
        expect(screen.getByText('108')).toBeInTheDocument();
        expect(screen.getByText('60')).toBeInTheDocument();
    });

    test('should show pallet', () => {
        expect(screen.getByText('56.56')).toBeInTheDocument();
        expect(screen.getByText('140')).toBeInTheDocument();
        expect(screen.getByText('180')).toBeInTheDocument();
        expect(screen.getByText('200')).toBeInTheDocument();
        expect(screen.getByText('4')).toBeInTheDocument();
    });

    test('should show no document numbers', () => {
        const tab = screen.getByText('Documents');
        fireEvent.click(tab);

        expect(screen.getByText('Invoices')).toBeInTheDocument();
        expect(screen.getByText('No Invoices')).toBeInTheDocument();
        expect(screen.getByText('Export Books')).toBeInTheDocument();
        expect(screen.getByText('No Export Books')).toBeInTheDocument();
    });
});

describe('should render documents of closed consignment', () => {
    beforeEach(() => {
        defaultRender({
            item: {
                ...item,
                invoices: [{ documentNumber: 123456 }, { documentNumber: 789012 }],
                exportBooks: [{ exportId: 888999 }]
            }
        });

        const tab = screen.getByText('Documents');
        fireEvent.click(tab);
    });

    test('should show document numbers', () => {
        expect(screen.getByText('Invoices')).toBeInTheDocument();
        expect(screen.getByText('123456')).toBeInTheDocument();
        expect(screen.getByText('789012')).toBeInTheDocument();
        expect(screen.getByText('Export Books')).toBeInTheDocument();
        expect(screen.getByText('888999')).toBeInTheDocument();
    });
});

describe('should change status when clicking edit', () => {
    beforeEach(() => {
        defaultRender({
            item
        });

        const editButton = screen.getByRole('button', { name: 'Edit' });
        fireEvent.click(editButton);
    });

    test('should set status to edit', () => {
        expect(setEditStatus).toHaveBeenCalledWith('edit');
    });
});

describe('should update and save consignment status', () => {
    beforeEach(() => {
        defaultRender({
            item,
            hubs: [
                { hubId: 1, description: 'hub1' },
                { hubId: 2, description: 'hub2' }
            ],
            hub: { hubId: 1, description: 'hub1' },
            editStatus: 'edit'
        });

        const details = screen.getByText('Details');
        fireEvent.click(details);

        const a = screen.getByDisplayValue('1 - hub1');
        fireEvent.change(a, { target: { value: 2 } });

        const saveButton = screen.getByRole('button', { name: 'Save' });
        fireEvent.click(saveButton);
    });

    afterEach(() => {
        updateItem.mockClear();
    });

    test('should call update with new values', () => {
        expect(updateItem).toHaveBeenCalledWith(
            101101,
            expect.objectContaining({
                consignmentId: 101101,
                hubId: '2'
            })
        );
    });
});

describe('should close consignment', () => {
    beforeEach(() => {
        defaultRender({
            item
        });

        const closeButton = screen.getByRole('button', { name: 'Close Consignment' });
        fireEvent.click(closeButton);
    });

    afterEach(() => {
        updateItem.mockClear();
    });

    test('should call update with close details', () => {
        expect(updateItem).toHaveBeenCalledWith(
            101101,
            expect.objectContaining({
                status: 'C',
                closedById: userNumber
            })
        );
    });
});

describe('When terms different to that of outlets on consignment', () => {
    beforeEach(() => {
        defaultRender({
            item,
            hubs: [
                { hubId: 1, description: 'hub1' },
                { hubId: 2, description: 'hub2' }
            ],
            hub: { hubId: 1, description: 'hub1' },
            editStatus: 'edit',
            salesOutlets: [{ dispatchTerms: 'DIFFERENT TERMS', name: 'TEST OUTLET' }]
        });

        const details = screen.getByText('Details');
        fireEvent.click(details);

        // const a = screen.getByDisplayValue('1 - hub1');
        // fireEvent.change(a, { target: { value: 2 } });

        const saveButton = screen.getByRole('button', { name: 'Save' });
        fireEvent.click(saveButton);
    });

    test('should not call update ', () => {
        expect(updateItem).not.toHaveBeenCalled();
    });

    test('should show warning with details', () => {
        expect(screen.getByText('Warning')).toBeInTheDocument();
        expect(
            screen.getByText('Some outlets do not match chosen shipping terms: DDP')
        ).toBeInTheDocument();
        expect(screen.getByText('TEST OUTLET - DIFFERENT TERMS,')).toBeInTheDocument();
    });
});
