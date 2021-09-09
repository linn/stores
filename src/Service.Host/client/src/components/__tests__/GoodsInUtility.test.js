import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, waitFor } from '@testing-library/react';
import render from '../../test-utils';
import GoodsInUtility from '../goodsIn/GoodsInUtility';

afterEach(cleanup);

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
            {...props}
        />
    );

describe('On initial load', () => {
    test('Component renders without crashing', () => {
        const { getByText } = defaultRender();
        expect(getByText('Goods In Utility')).toBeInTheDocument();
    });
});

describe('When Order Number Entered', () => {
    test('Should call validation function onBlur', () => {
        const { getByLabelText } = defaultRender();
        const orderNumberField = getByLabelText('Order Number');
        orderNumberField.focus();
        fireEvent.change(orderNumberField, { target: { value: '123456' } });
        orderNumberField.blur();

        expect(validatePurchaseOrder).toHaveBeenCalledWith('123456');
    });
});

describe('When validatePurchaseOrderResultComesBackWithSuccess', () => {
    test('Should Open Purchase Order Book In Section', () => {
        const { getByDisplayValue } = defaultRender({
            validatePurchaseOrderResult: {
                message: null,
                orderNumber: 123456,
                partNumber: 'A PART',
                documentType: 'PO'
            }
        });
        expect(getByDisplayValue('A PART')).toBeInTheDocument();
    });
});
