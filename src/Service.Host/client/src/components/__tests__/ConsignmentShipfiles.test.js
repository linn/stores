/**
 * @jest-environment jest-environment-jsdom-sixteen
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen } from '@testing-library/react';
import render from '../../test-utils';
import ConsignmentShipfiles from '../ConsignmentShipfiles';

const sendEmails = jest.fn();
const deleteShipfile = jest.fn();
const clearErrors = jest.fn();

const defaultRender = props =>
    render(
        <ConsignmentShipfiles
            sendEmails={sendEmails}
            deleteShipfile={deleteShipfile}
            clearErrors={clearErrors}
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
        expect(screen.getByText('Send Shipfile Emails')).toBeInTheDocument();
    });

    test('should show no rows message', () => {
        expect(screen.getByText('No rows')).toBeInTheDocument();
    });

    test('buttons should be disabled', () => {
        expect(screen.getByRole('button', { name: 'Send Selected' })).toHaveClass('Mui-disabled');
        expect(screen.getByRole('button', { name: 'Test Selected' })).toHaveClass('Mui-disabled');
        expect(screen.getByRole('button', { name: 'Delete Selected' })).toHaveClass('Mui-disabled');
    });
});

describe('When Shipfiles Loading...', () => {
    beforeEach(() => {
        defaultRender({ consignmentShipfilesLoading: true });
    });

    test('should show loading spinner', () => {
        expect(screen.getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('When Shipfiles arrive...', () => {
    beforeEach(() => {
        defaultRender({
            consignmentShipfiles: [
                {
                    id: 1,
                    consignmentId: 1,
                    dateClosed: '08/06/2003',
                    customerName: 'CUSTOMER ONE',
                    invoiceNumbers: '123456',
                    status: ''
                },
                {
                    id: 2,
                    consignmentId: 2,
                    dateClosed: '08/09/2003',
                    customerName: 'CUSTOMER TWO',
                    invoiceNumbers: '567890',
                    status: ''
                }
            ]
        });
    });

    test('should show shipfiles in table', () => {
        expect(screen.getByText('08/06/2003')).toBeInTheDocument();
        expect(screen.getByText('08/09/2003')).toBeInTheDocument();
    });
});

describe('When Sending Emails...', () => {
    beforeEach(() => {
        sendEmails.mockClear();
        defaultRender({
            consignmentShipfiles: [
                {
                    id: 1,
                    consignmentId: 1,
                    dateClosed: '08/06/2003',
                    customerName: 'CUSTOMER ONE',
                    invoiceNumbers: '123456',
                    status: ''
                },
                {
                    id: 2,
                    consignmentId: 2,
                    dateClosed: '08/09/2003',
                    customerName: 'CUSTOMER TWO',
                    invoiceNumbers: '567890',
                    status: ''
                }
            ]
        });

        const checkboxes = screen.getAllByRole('checkbox');

        checkboxes.forEach((c, i) => i > 0 && fireEvent.click(c));

        const sendButton = screen.getByRole('button', { name: 'Send Selected' });

        fireEvent.click(sendButton);
    });

    test('should clear errors', async () => {
        expect(clearErrors).toHaveBeenCalled();
    });

    test('should call sendEmails', async () => {
        expect(sendEmails).toHaveBeenCalledWith(
            expect.objectContaining({
                shipfiles: expect.arrayContaining([
                    expect.objectContaining({
                        id: 1,
                        consignmentId: 1,
                        dateClosed: '08/06/2003',
                        customerName: 'CUSTOMER ONE',
                        invoiceNumbers: '123456',
                        status: ''
                    })
                ])
            })
        );
        expect(sendEmails).toHaveBeenCalledWith(
            expect.objectContaining({
                shipfiles: expect.arrayContaining([
                    expect.objectContaining({
                        id: 2,
                        consignmentId: 2,
                        dateClosed: '08/09/2003',
                        customerName: 'CUSTOMER TWO',
                        invoiceNumbers: '567890',
                        status: ''
                    })
                ])
            })
        );
    });
});

// describe('When Sent Emails...', () => {
//     beforeEach(() => {
//         sendEmails.mockClear();
//         defaultRender({
//             consignmentShipfiles: [
//                 {
//                     id: 1,
//                     consignmentId: 1,
//                     dateClosed: '08/06/2003',
//                     customerName: 'CUSTOMER ONE',
//                     invoiceNumbers: '123456',
//                     status: ''
//                 },
//                 {
//                     id: 2,
//                     consignmentId: 2,
//                     dateClosed: '08/09/2003',
//                     customerName: 'CUSTOMER TWO',
//                     invoiceNumbers: '567890',
//                     status: ''
//                 }
//             ],
//             processedShipfiles: [
//                 {
//                     id: 1,
//                     consignmentId: 1,
//                     dateClosed: '08/06/2003',
//                     customerName: 'CUSTOMER ONE',
//                     invoiceNumbers: '123456',
//                     status: 'Email Sent!'
//                 },
//                 {
//                     id: 2,
//                     consignmentId: 2,
//                     dateClosed: '08/09/2003',
//                     customerName: 'CUSTOMER TWO',
//                     invoiceNumbers: '567890',
//                     status: 'No Contact details found.'
//                 }
//             ]
//         });
//     });
// });
