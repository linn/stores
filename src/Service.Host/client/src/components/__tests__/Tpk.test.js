import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen } from '@testing-library/react';
import render from '../../test-utils';
import Tpk from '../tpk/Tpk';

const transferStock = jest.fn();
const clearData = jest.fn();
const clearErrors = jest.fn();
const clearUnpickErrors = jest.fn();
const clearUnallocateErrors = jest.fn();
const clearUnallocateData = jest.fn();
const clearUnpickData = jest.fn();
const refresh = jest.fn();

const defaultRender = props =>
    render(
        <Tpk
            transferStock={transferStock}
            clearData={clearData}
            clearErrors={clearErrors}
            clearUnpickErrors={clearUnpickErrors}
            clearUnallocateErrors={clearUnallocateErrors}
            clearUnallocateData={clearUnallocateData}
            clearUnpickData={clearUnpickData}
            refresh={refresh}
            // eslint-disable-next-line react/jsx-props-no-spreading
            {...props}
        />
    );

afterEach(() => cleanup());

describe('On initial load...', () => {
    beforeEach(() => defaultRender());

    test('component renders without crashing', () => {
        expect(screen.getByText('TPK')).toBeInTheDocument();
    });
});

describe('When transferable stock loading...', () => {
    beforeEach(() => defaultRender({ transferableStockLoading: true }));

    test('component renders loading spinner', () => {
        expect(screen.getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('When transferable stock arrives...', () => {
    beforeEach(() =>
        defaultRender({ transferableStock: [{ id: 1, quantity: 1, articleNumber: 'FIRST THING' }] })
    );

    test('datagird renders lines', () => {
        expect(screen.getByText('FIRST THING')).toBeInTheDocument();
    });
});

describe('When doing tpk...', () => {
    beforeEach(() => {
        defaultRender({
            transferableStock: [
                {
                    id: 1,
                    quantity: 1,
                    articleNumber: 'FIRST THING',
                    orderNumber: 1,
                    orderLine: 1,
                    fromLocation: 'LOC A'
                },
                {
                    id: 2,
                    quantity: 1,
                    articleNumber: 'SECOND THING',
                    orderNumber: 2,
                    orderLine: 1,
                    fromLocation: 'LOC A'
                }
            ]
        });
        const checkboxes = screen.getAllByRole('checkbox');

        checkboxes.forEach((c, i) => i > 0 && fireEvent.click(c));

        const sendButton = screen.getByRole('button', { name: 'Transfer' });

        fireEvent.click(sendButton);
    });

    test('transferStock is called with correct arguments', () => {
        expect(transferStock).toHaveBeenCalledWith(
            expect.objectContaining({
                stockToTransfer: expect.arrayContaining([
                    expect.objectContaining({
                        id: 'FIRST THING11LOC A',
                        articleNumber: 'FIRST THING'
                    }),
                    expect.objectContaining({
                        id: 'SECOND THING21LOC A',
                        articleNumber: 'SECOND THING'
                    })
                ])
            })
        );
    });
});

describe('When tpk loading...', () => {
    beforeEach(() => defaultRender({ tpkLoading: true }));

    test('component renders loading spinner', () => {
        expect(screen.getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('When tpk result...', () => {
    beforeEach(() =>
        defaultRender({
            transferableStock: [
                {
                    quantity: 1,
                    articleNumber: 'FIRST THING',
                    orderNumber: 1,
                    orderLine: 1,
                    fromLocation: 'LOC A'
                },
                {
                    quantity: 1,
                    articleNumber: 'SECOND THING',
                    orderNumber: 2,
                    orderLine: 1,
                    fromLocation: 'LOC A'
                }
            ],
            transferredStock: [
                {
                    quantity: 1,
                    articleNumber: 'FIRST THING',
                    orderNumber: 1,
                    orderLine: 1,
                    fromLocation: 'LOC A',
                    notes: '*TPKD*'
                },
                {
                    quantity: 1,
                    articleNumber: 'SECOND THING',
                    orderNumber: 2,
                    orderLine: 1,
                    fromLocation: 'LOC A',
                    notes: '*TPKD*'
                }
            ]
        })
    );

    test('Should updates notes in table', () => {
        expect(screen.getAllByText('*TPKD*').length).toBe(2);
    });
});

describe('When tpk error...', () => {
    beforeEach(() =>
        defaultRender({ itemError: { details: { errors: ['Something went wrong'] } } })
    );
    test('should display error message', () => {
        expect(screen.getByText('Something went wrong')).toBeInTheDocument();
    });
});

describe('When whatToWandReport...', () => {
    beforeEach(() =>
        defaultRender({
            whatToWandReport: [
                {
                    lines: [],
                    account: { accountName: 'something' },
                    consignment: { totalNettValue: 100.1234, currencyCode: 'GBP' }
                }
            ]
        })
    );
    test('should render currency code and rounded nett total', () => {
        expect(
            screen.getByText('Total Nett Value Of Consignment (GBP): 100.12')
        ).toBeInTheDocument();
    });
});
