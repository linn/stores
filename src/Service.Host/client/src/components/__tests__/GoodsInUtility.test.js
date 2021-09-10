import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen } from '@testing-library/react';
import render from '../../test-utils';
import GoodsInUtility from '../goodsIn/GoodsInUtility';

const validatePurchaseOrder = jest.fn();
// const validatePurchaseOrderResultLoading = null;
const searchDemLocations = jest.fn();
// const demLocationsSearchLoading = null;
// const demLocationsSearchResults = null;
const searchStoragePlaces = jest.fn();
// const storagePlacesSearchResults = null;
// const storagePlacesSearchLoading = null;
const searchSalesArticles = jest.fn();
// const salesArticlesSearchResults = null;
// const salesArticlesSearchLoading = null;
// const bookInResult = null;
// const bookInResultLoading = null;
const doBookIn = jest.fn();
// const validatePurchaseOrderBookInQtyResult = null;
const validatePurchaseOrderBookInQty = jest.fn();
// const validatePurchaseOrderBookInQtyResultLoading = null;
// const userNumber = null;
const validateStorageType = jest.fn();
// const validateStorageTypeResult = null;
// const validateStorageTypeResultLoading = null;
const match = { url: '/goods-in-utility' };
const userNumber = 33087;

const defaultRender = props =>
    render(
        <GoodsInUtility
            validatePurchaseOrder={validatePurchaseOrder}
            searchDemLocations={searchDemLocations}
            demLocationsSearchResults={[]}
            searchStoragePlaces={searchStoragePlaces}
            storagePlacesSearchResults={[]}
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
});
