/**
 * @jest-environment jest-environment-jsdom-sixteen
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen } from '@testing-library/react';
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

afterEach(() => cleanup());

describe('On initial load...', () => {
    beforeEach(() => {
        defaultRender();
    });

    test('component renders without crashing', () => {
        expect(screen.getByText('Goods In Utility')).toBeInTheDocument();
    });

    test('inputs should be disabled', () => {
        expect(screen.getByRole('button', { name: 'Add Line' })).toHaveClass('Mui-disabled');
        expect(screen.getByRole('button', { name: 'Book In' })).toHaveClass('Mui-disabled');
        expect(screen.getByLabelText('S/Type')).toHaveClass('Mui-disabled');
        expect(screen.getByLabelText('Qty')).toHaveClass('Mui-disabled');
    });
});

describe('When order number entered...', () => {
    afterEach(() => {
        validatePurchaseOrder.mockClear();
    });

    test('should call validation function onBlur if orderNumber input', () => {
        defaultRender();
        const orderNumberField = screen.getByLabelText('Order Number');
        orderNumberField.focus();
        fireEvent.change(orderNumberField, { target: { value: 123456 } });
        orderNumberField.blur();

        expect(validatePurchaseOrder).toHaveBeenCalledWith(123456);
    });

    test('should not call validation function onBlur if orderNumber blank', () => {
        defaultRender();
        const orderNumberField = screen.getByLabelText('Order Number');
        orderNumberField.focus();
        fireEvent.change(orderNumberField, { target: { value: '' } });
        orderNumberField.blur();

        expect(validatePurchaseOrder).not.toHaveBeenCalled();
    });

    describe('When validatePurchaseOrderResult loading...', () => {
        beforeEach(() => {
            defaultRender({
                validatePurchaseOrderResultLoading: true
            });
        });

        test('should show loading text', () => {
            expect(screen.getByDisplayValue('loading')).toBeInTheDocument();
        });

        test('should disable orderNumber input', () => {
            expect(screen.getByRole('spinbutton', { name: 'Order Number' })).toHaveClass(
                'Mui-disabled'
            );
        });

        test('should disable qty input', () => {
            expect(screen.getByRole('spinbutton', { name: 'Qty' })).toHaveClass('Mui-disabled');
        });
    });

    describe('When validatePurchaseOrderResult...', () => {
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

        test('should open purchase order book in section', () => {
            expect(screen.getByDisplayValue('A PART')).toBeInTheDocument();
        });

        describe('When new part...', () => {
            beforeEach(() => {
                defaultRender({
                    validatePurchaseOrderResult: {
                        orderNumber: 123456,
                        partNumber: 'A PART',
                        documentType: 'PO',
                        message: 'New part - enter storage type or location'
                    }
                });
            });

            test('should enable storageType Field', () => {
                expect(screen.getByLabelText('S/Type')).not.toHaveClass('Mui-disabled');
            });
        });
    });

    describe('When Storage is BB...', () => {
        beforeEach(() => {
            defaultRender({
                validatePurchaseOrderResult: {
                    storage: 'BB',
                    orderNumber: 123456,
                    partNumber: 'A PART',
                    documentType: 'PO'
                }
            });
        });

        test('should disable storageType field', () => {
            expect(screen.getByLabelText('S/Type')).toHaveClass('Mui-disabled');
        });
    });

    describe('When validatePurchaseOrderResult error...', () => {
        beforeEach(() => {
            defaultRender({
                validatePurchaseOrderResult: {
                    message: 'Not found'
                }
            });
        });

        test('should show message text', () => {
            expect(screen.getByDisplayValue('Not found')).toBeInTheDocument();
        });

        test('should disable add Line button', () => {
            expect(screen.getByRole('button', { name: 'Add Line' })).toBeInTheDocument();
        });

        test('should disable book in button', () => {
            expect(screen.getByRole('button', { name: 'Book In' })).toBeInTheDocument();
        });
    });
});

describe('When storage type entered', () => {
    afterEach(() => {
        validateStorageType.mockClear();
    });

    test('should call validation function onBlur if storageType input', () => {
        defaultRender({
            validatePurchaseOrderResult: {
                orderNumber: 123456,
                partNumber: 'A PART',
                documentType: 'PO',
                message: 'New part - enter storage type or location'
            }
        });
        const storageTypeField = screen.getByLabelText('S/Type');
        storageTypeField.focus();
        fireEvent.change(storageTypeField, { target: { value: 'K1' } });
        storageTypeField.blur();
        expect(validateStorageType).toHaveBeenCalledWith('storageType', 'K1');
    });

    test('should not call validation function onBlur if storageType blank', () => {
        defaultRender({
            validatePurchaseOrderResult: {
                orderNumber: 123456,
                partNumber: 'A PART',
                documentType: 'PO',
                message: 'New part - enter storage type or location'
            }
        });
        const storageTypeField = screen.getByLabelText('S/Type');
        storageTypeField.focus();
        fireEvent.change(storageTypeField, { target: { value: '' } });
        storageTypeField.blur();
        expect(validateStorageType).not.toHaveBeenCalled();
    });

    describe('When validateStorageTypeResult loading...', () => {
        beforeEach(() => {
            defaultRender({
                validateStorageTypeResultLoading: true
            });
        });

        test('should show loading text', () => {
            expect(screen.getByDisplayValue('loading')).toBeInTheDocument();
        });
    });

    describe('When validateStorageTypeResult returns...', () => {
        beforeEach(() => {
            defaultRender({
                validateStorageTypeResult: { message: 'Storage Type Valid' }
            });
        });

        test('should show message text', () => {
            expect(screen.getByDisplayValue('Storage Type Valid')).toBeInTheDocument();
        });
    });
});

describe('When qty Entered...', () => {
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
        // enter an order number so we are able to enter a qty
        const orderNumberField = screen.getByLabelText('Order Number');
        fireEvent.change(orderNumberField, { target: { value: 123456 } });
    });

    test('should call validation function onBlur if qty input', () => {
        const qtyField = screen.getByLabelText('Qty');
        qtyField.focus();
        fireEvent.change(qtyField, { target: { value: 1 } });
        qtyField.blur();

        expect(validatePurchaseOrderBookInQty).toHaveBeenCalledWith(
            'qty=1&orderLine=1&orderNumber',
            123456
        );
    });

    test('should not call validation function onBlur if qty blank', () => {
        const qtyField = screen.getByLabelText('Qty');
        qtyField.focus();
        fireEvent.change(qtyField, { target: { value: '' } });
        qtyField.blur();

        expect(validatePurchaseOrderBookInQty).not.toHaveBeenCalled();
    });

    test('should not call validation function onBlur if qty 0', () => {
        const qtyField = screen.getByLabelText('Qty');
        qtyField.focus();
        fireEvent.change(qtyField, { target: { value: 0 } });
        qtyField.blur();

        expect(validatePurchaseOrderBookInQty).not.toHaveBeenCalled();
    });

    describe('When validatePurchaseOrderBookInQty loading...', () => {
        test('should show loading text', () => {
            defaultRender({
                validatePurchaseOrderBookInQtyResultLoading: true
            });
            expect(screen.getByDisplayValue('loading')).toBeInTheDocument();
        });
    });

    describe('When validatePurchaseOrderBookInQty result has error message...', () => {
        beforeEach(() => {
            defaultRender({
                validatePurchaseOrderResult: {
                    message: 'Order is overbooked'
                }
            });
        });

        test('should show message text', () => {
            expect(screen.getByDisplayValue('Order is overbooked')).toBeInTheDocument();
        });
    });
});

describe('When book in button clicked', () => {
    beforeEach(() => {
        defaultRender({
            validatePurchaseOrderResult: {
                message: null,
                orderNumber: 123456,
                partNumber: 'A PART',
                documentType: 'PO'
            }
        });

        afterEach(() => cleanup());

        const orderNumberField = screen.getByLabelText('Order Number');
        fireEvent.change(orderNumberField, { target: { value: 123456 } });
        const qtyField = screen.getByLabelText('Qty');
        fireEvent.change(qtyField, { target: { value: 1 } });

        // open the location search
        const locationField = screen.getByLabelText('Onto Location');
        fireEvent.click(locationField);

        // select a result to close the dialog
        const searchResult = screen.getByText('LOC');
        fireEvent.click(searchResult);
    });

    test('should call doBookIn', () => {
        const button = screen.getByText('Book In');
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

    describe('When multipleBookIn checkbox selected...', () => {
        test('should call doBookIn with multipeBookIn flag', async () => {
            // click the checkbox
            const checkboxes = await screen.findAllByRole('checkbox');

            // doesn't work
            //const checkboxes = screen.getAllByRole('checkbox');

            fireEvent.click(checkboxes[0]);
            const button = await screen.findByText('Book In');
            fireEvent.click(button);

            expect(doBookIn).toHaveBeenCalledWith(
                expect.objectContaining({
                    partNumber: 'A PART',
                    orderNumber: 123456,
                    ontoLocation: 'LOC',
                    multipleBookIn: true,
                    qty: 1
                })
            );
        });
    });

    describe('When bookInResultLoading...', () => {
        test('should show loading text', () => {
            defaultRender({ bookInResultLoading: true });

            expect(screen.getByDisplayValue('loading')).toBeInTheDocument();
        });
    });

    describe('When bookInResult returns succesfully...', () => {
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

        test('should show message', () => {
            expect(screen.getByDisplayValue('Book In Succesful!')).toBeInTheDocument();
        });

        test('should add lines to table', () => {
            expect(screen.getByText('SOME-TRANS-TYPE')).toBeInTheDocument();
            expect(screen.getByText('OTHER-TRANS-TYPE')).toBeInTheDocument();
        });

        test('should open label print dialog', () => {
            expect(screen.getByText('Label Details')).toBeInTheDocument();
            expect(screen.getByDisplayValue('500123')).toBeInTheDocument();
        });

        test('should not open parcel dialog', () => {
            expect(screen.queryByText('Create Parcel')).not.toBeInTheDocument();
        });
    });

    describe('When create parcel...', () => {
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

        test('should open parcel dialog', () => {
            expect(screen.getByText('Create Parcel')).toBeInTheDocument();
        });
    });

    describe('When not create parcel...', () => {
        beforeAll(() => {
            defaultRender({
                bookInResult: {
                    success: true,
                    message: 'Book In Succesful!',
                    createParcel: false,
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

        test('should not open parcel dialog', () => {
            expect(screen.queryByText('Create Parcel')).not.toBeInTheDocument();
        });
    });
});

describe('When adding multiple lines to a book in...', () => {
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

    test('should call doBookIn with lines', () => {
        const orderNumberField = screen.getByLabelText('Order Number');
        fireEvent.change(orderNumberField, { target: { value: 123456 } });
        const qtyField = screen.getByLabelText('Qty');
        fireEvent.change(qtyField, { target: { value: 1 } });

        // open the location search
        const locationField = screen.getByLabelText('Onto Location');
        fireEvent.click(locationField);

        // select a result to close the dialog
        const searchResult = screen.getByText('LOC');
        fireEvent.click(searchResult);
        const addLineButton = screen.getByText('Add Line');
        fireEvent.click(addLineButton);
        const doBookInButton = screen.getByText('Book In');
        fireEvent.click(doBookInButton);

        expect(doBookIn).toHaveBeenCalledWith(
            expect.objectContaining({
                lines: expect.arrayContaining([expect.objectContaining({ quantity: 1 })])
            })
        );
    });
});
