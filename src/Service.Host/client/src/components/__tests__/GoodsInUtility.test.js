/**
 * @jest-environment jest-environment-jsdom-sixteen
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen, waitFor } from '@testing-library/react';
import render from '../../test-utils';
import GoodsInUtility from '../goodsIn/GoodsInUtility';

const validatePurchaseOrder = jest.fn();
const searchDemLocations = jest.fn();
const searchStoragePlaces = jest.fn();
const storagePlacesSearchResults = [{ name: 'LOC', id: 1 }];
const searchSalesArticles = jest.fn();
const doBookIn = jest.fn();
const validatePurchaseOrderBookInQty = jest.fn();
const validateStorageType = jest.fn();
const match = { url: '/goods-in-utility' };
const userNumber = 33087;

const defaultRender = props =>
    render(
        <GoodsInUtility
            history={{ push: jest.fn() }}
            validatePurchaseOrder={validatePurchaseOrder}
            searchDemLocations={searchDemLocations}
            demLocationsSearchResults={[]}
            searchStoragePlaces={searchStoragePlaces}
            storagePlacesSearchResults={storagePlacesSearchResults}
            searchSalesArticles={searchSalesArticles}
            salesArticlesSearchResults={[]}
            doBookIn={doBookIn}
            validatePurchaseOrderBookInQty={validatePurchaseOrderBookInQty}
            userNumber={userNumber}
            validateStorageType={validateStorageType}
            match={match}
            //eslint-disable-next-line react/jsx-props-no-spreading
            {...props}
        />
    );

afterEach(cleanup);

describe('On initial load', () => {
    beforeEach(() => {
        defaultRender();
    });

    test('Component renders without crashing', () => {
        expect(screen.getByText('Goods In Utility')).toBeInTheDocument();
    });

    test('Buttons should be disabled', () => {
        expect(screen.getByRole('button', { name: 'Add Line' })).toHaveClass('Mui-disabled');
        expect(screen.getByRole('button', { name: 'Book In' })).toHaveClass('Mui-disabled');
    });
});

describe('When Order Number Entered', () => {
    afterEach(() => {
        validatePurchaseOrder.mockClear();
    });

    beforeAll(() => {
        defaultRender();
    });

    test('Should call validation function onBlur if orderNumber input', () => {
        const orderNumberField = screen.getByLabelText('Order Number');
        orderNumberField.focus();
        fireEvent.change(orderNumberField, { target: { value: 123456 } });
        orderNumberField.blur();

        expect(validatePurchaseOrder).toHaveBeenCalledWith(123456);
    });

    test('Should not call validation function onBlur if orderNumber blank', () => {
        defaultRender();
        const orderNumberField = screen.getByLabelText('Order Number');
        orderNumberField.focus();
        fireEvent.change(orderNumberField, { target: { value: '' } });
        orderNumberField.blur();

        expect(validatePurchaseOrder).not.toHaveBeenCalled();
    });

    describe('When validatePurchaseOrderResult loading', () => {
        beforeEach(() => {
            defaultRender({
                validatePurchaseOrderResultLoading: true
            });
        });

        test('Should Show loading text', () => {
            expect(screen.getByDisplayValue('loading')).toBeInTheDocument();
        });

        test('Should disable orderNumber Order Input', () => {
            expect(screen.getByRole('spinbutton', { name: 'Order Number' })).toHaveClass(
                'Mui-disabled'
            );
        });

        test('Should disable Qty Input text', () => {
            expect(screen.getByRole('spinbutton', { name: 'Qty' })).toHaveClass('Mui-disabled');
        });
    });

    describe('When validatePurchaseOrderResult', () => {
        beforeEach(() => {
            defaultRender({
                validatePurchaseOrderResult: {
                    message: null,
                    orderNumber: 123456,
                    partNumber: 'A PART',
                    documentType: 'PO'
                }
            });
        });

        test('Should Open Purchase Order Book In Section', () => {
            expect(screen.getByDisplayValue('A PART')).toBeInTheDocument();
        });
    });

    describe('When validatePurchaseOrderResult errors', () => {
        beforeEach(() => {
            defaultRender({
                validatePurchaseOrderResult: {
                    message: 'Not found'
                }
            });
        });

        test('Should Show Message text', () => {
            expect(screen.getByDisplayValue('Not found')).toBeInTheDocument();
        });

        test('Should disable Add Line button', () => {
            expect(screen.getByRole('button', { name: 'Add Line' })).toBeInTheDocument();
        });

        test('Should disable Book In button', () => {
            expect(screen.getByRole('button', { name: 'Book In' })).toBeInTheDocument();
        });
    });
});

describe('When Qty Entered', () => {
    afterEach(() => {
        validatePurchaseOrderBookInQty.mockClear();
    });

    beforeEach(() => {
        defaultRender({
            validatePurchaseOrderResult: {
                message: null,
                orderNumber: 123456,
                partNumber: 'A PART',
                documentType: 'PO'
            }
        });
        // enter an order number so we are able tocenter a qty
        const orderNumberField = screen.getByLabelText('Order Number');
        fireEvent.change(orderNumberField, { target: { value: 123456 } });
    });

    test('Should call validation function onBlur if qty input', () => {
        const qtyField = screen.getByLabelText('Qty');
        qtyField.focus();
        fireEvent.change(qtyField, { target: { value: 1 } });
        qtyField.blur();

        expect(validatePurchaseOrderBookInQty).toHaveBeenCalledWith(
            'qty=1&orderLine=1&orderNumber',
            123456
        );
    });

    test('Should not call validation function onBlur if qty blank', () => {
        const qtyField = screen.getByLabelText('Qty');
        qtyField.focus();
        fireEvent.change(qtyField, { target: { value: '' } });
        qtyField.blur();

        expect(validatePurchaseOrderBookInQty).not.toHaveBeenCalled();
    });

    test('Should not call validation function onBlur if qty 0', () => {
        const qtyField = screen.getByLabelText('Qty');
        qtyField.focus();
        fireEvent.change(qtyField, { target: { value: 0 } });
        qtyField.blur();

        expect(validatePurchaseOrderBookInQty).not.toHaveBeenCalled();
    });

    describe('When validatePurchaseOrderBookInQty loading', () => {
        test('Should Show loading text', () => {
            defaultRender({
                validatePurchaseOrderBookInQtyResultLoading: true
            });
            expect(screen.getByDisplayValue('loading')).toBeInTheDocument();
        });
    });

    describe('When validatePurchaseOrderBookInQty result has error message', () => {
        beforeEach(() => {
            defaultRender({
                validatePurchaseOrderResult: {
                    message: 'Order is overbooked'
                }
            });
        });

        test('Should Show Message text', () => {
            expect(screen.getByDisplayValue('Order is overbooked')).toBeInTheDocument();
        });
    });
});

describe('when Book In button clicked', () => {
    beforeAll(async () => {
        defaultRender({
            validatePurchaseOrderResult: {
                message: null,
                orderNumber: 123456,
                partNumber: 'A PART',
                documentType: 'PO'
            }
        });
        const orderNumberField = screen.getByLabelText('Order Number');
        fireEvent.change(orderNumberField, { target: { value: 123456 } });
        const qtyField = screen.getByLabelText('Qty');
        fireEvent.change(qtyField, { target: { value: 1 } });

        // open the location search
        const locationField = screen.getByLabelText('Onto Location');
        fireEvent.click(locationField);

        // select a result to close the dialog
        const searchResult = await screen.findByText('LOC');
        fireEvent.click(searchResult);
    });

    test('should call doBookIn', async () => {
        const button = await screen.findByText('Book In');
        fireEvent.click(button);

        expect(doBookIn).toHaveBeenCalledWith(
            expect.objectContaining({
                partNumber: 'A PART',
                orderNumber: 123456,
                ontoLocation: 'LOC',
                qty: 1
            })
        );
    });

    describe('when bookInResultLoading', () => {
        beforeAll(() => {
            defaultRender({ bookInResultLoading: true });
        });

        test('Should Show loading text', () => {
            expect(screen.getByDisplayValue('loading')).toBeInTheDocument();
        });
    });

    describe('when bookInResult returns succesfully and not createParcel', () => {
        beforeEach(() => {
            defaultRender({
                bookInResult: {
                    success: true,
                    message: 'Book In Succesful!',
                    createParcel: false,
                    reqNumber: 500123,
                    lines: [
                        { id: 1, transactionType: 'SOME-TRANS-TYPE' },
                        { id: 2, transactionType: 'OTHER-TRANS-TYPE' }
                    ]
                }
            });
        });

        test('Should show message', () => {
            expect(screen.getByDisplayValue('Book In Succesful!')).toBeInTheDocument();
        });

        test('Should add lines to table', () => {
            expect(screen.getByText('SOME-TRANS-TYPE')).toBeInTheDocument();
            expect(screen.getByText('OTHER-TRANS-TYPE')).toBeInTheDocument();
        });

        test('Should open label print dialog', () => {
            expect(screen.getByText('Label Details')).toBeInTheDocument();
            expect(screen.getByDisplayValue('500123')).toBeInTheDocument();
        });

        test('Should not open parcel dialog', () => {
            expect(screen.queryByText('Create Parcel')).not.toBeInTheDocument();
        });
    });
});

describe('when bookInResult returns succesfully and create parcel', () => {
    beforeAll(() => {
        defaultRender({
            bookInResult: {
                success: true,
                message: 'Book In Succesful!',
                createParcel: true,
                supplierId: 123,
                parcelComments: 'I need a parcel',
                reqNumber: 500123,
                lines: [
                    { id: 1, transactionType: 'SOME-TRANS-TYPE' },
                    { id: 2, transactionType: 'OTHER-TRANS-TYPE' }
                ]
            }
        });
    });

    test('Should open parcel dialog', async () => {
        await waitFor(() => expect(screen.getByText('Create Parcel')).toBeInTheDocument());
    });
});
