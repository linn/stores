import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import Decimal from 'decimal.js';
import { cleanup, fireEvent, screen } from '@testing-library/react';
import render from '../../test-utils';
import QcLabelPrintScreen from '../goodsIn/QcLabelPrintScreen';

const printLabels = jest.fn();

const props = {
    docType: 'PO',
    orderNumber: '123456',
    qcState: 'PASS',
    partNumber: 'PART',
    partDescription: 'DESC',
    qtyReceived: 1,
    unitOfMeasure: 'ONES',
    reqNumber: '4567890',
    qcInfo: 'INFO',
    printLabels,
    printLabelsResult: null,
    printLabelsLoading: false,
    kardexLocation: null
};
describe('When not Kardex Location', () => {
    beforeEach(() => {
        cleanup();
        // eslint-disable-next-line react/jsx-props-no-spreading
        render(<QcLabelPrintScreen {...props} qtyReceived={3} />);
    });

    test('should enable quantities accordion', () => {
        expect(screen.getByTestId('quantitiesExpansionPanel')).not.toHaveClass('Mui-disabled');
    });
    describe('When quantites accordion opened ', () => {
        beforeEach(() => {
            const expansionPanel = screen.getByTestId('quantitiesExpansionPanel');
            fireEvent.click(expansionPanel);
        });
        test('should display correct number of label lines', () => {
            expect(screen.getByLabelText('1')).toBeInTheDocument();
        });
    });

    describe('When numContainers changed', () => {
        beforeEach(() => {
            const input = screen.getByLabelText('# Containers');
            fireEvent.change(input, { target: { value: 5 } });
        });
        test('should display correct number of label lines', () => {
            expect(screen.getByLabelText('1')).toBeInTheDocument();
            expect(screen.getByLabelText('2')).toBeInTheDocument();
            expect(screen.getByLabelText('3')).toBeInTheDocument();
            expect(screen.getByLabelText('4')).toBeInTheDocument();
            expect(screen.getByLabelText('5')).toBeInTheDocument();
        });
    });

    describe('When qty changed for a line', () => {
        beforeEach(() => {
            const input = screen.getByLabelText('1');
            fireEvent.change(input, { target: { value: 3 } });
        });
        test('should update correct line', () => {
            expect(screen.getByLabelText('1').value).toBe('3');
        });
    });
});

describe('When printLabels button clicked', () => {
    beforeEach(() => {
        cleanup();
        // eslint-disable-next-line react/jsx-props-no-spreading
        render(<QcLabelPrintScreen {...props} />);
        const button = screen.getByText('Print');
        fireEvent.click(button);
    });

    test('should callPrintLabels with correct args', () => {
        expect(printLabels).toHaveBeenCalledWith(
            expect.objectContaining({
                deliveryRef: '',
                documentType: 'PO',
                kardexLocation: null,
                lines: expect.arrayContaining([
                    expect.objectContaining({ id: '0', qty: new Decimal(1) })
                ]),
                numberOfLabels: 1,
                numberOfLines: 1,
                orderNumber: '123456',
                partDescription: 'DESC',
                partNumber: 'PART',
                qcInformation: 'INFO',
                qcState: 'PASS',
                qty: 1,
                reqNumber: '4567890'
            })
        );
    });
});

describe('When printLabels loading', () => {
    beforeEach(() => {
        cleanup();
        // eslint-disable-next-line react/jsx-props-no-spreading
        render(<QcLabelPrintScreen {...props} printLabelsLoading />);
    });

    test('should display loading spinner', () => {
        expect(screen.getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('When printLabelsResult error', () => {
    beforeEach(() => {
        cleanup();
        render(
            <QcLabelPrintScreen
                // eslint-disable-next-line react/jsx-props-no-spreading
                {...props}
                printLabelsResult={{ success: false, message: 'ERROR' }}
            />
        );
    });

    test('should display error message', () => {
        expect(screen.getByText('ERROR')).toBeInTheDocument();
    });
});

describe('When qties do not add up', () => {
    beforeEach(() => {
        cleanup();
        // eslint-disable-next-line react/jsx-props-no-spreading
        render(<QcLabelPrintScreen {...props} />);
        const expansionPanel = screen.getByTestId('quantitiesExpansionPanel');
        fireEvent.click(expansionPanel);
        const input = screen.getByLabelText('1');
        fireEvent.change(input, { target: { value: 5 } });
    });

    test('should disable button', () => {
        const button = screen.getByRole('button', { name: 'Print' });
        expect(button).toBeDisabled();
    });
});
