import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen } from '@testing-library/react';
import render from '../../../test-utils';

import DeptStockUtility from '../../DeptStockUtility/DeptStockUtility';

const clearDepartmentsSearch = jest.fn();
const searchDepartments = jest.fn();
const updateStockLocator = jest.fn();
const createStockLocator = jest.fn();
const deleteStockLocator = jest.fn();
const searchStoragePlaces = jest.fn();
const push = jest.fn();

const defaultProps = {
    items: [
        {
            id: 1,
            storagePlaceName: 'PLACE A',
            storagePlaceDescription: 'A PLACE',
            batchRef: 'REFA',
            stockRotationDate: new Date().toISOString,
            quantity: 1,
            remarks: 'REMARKABLE',
            auditDepartmentCode: '00001234',
            partNumber: 'EMPTY'
        },
        {
            id: 2,
            storagePlaceName: 'PLACE B',
            storagePlaceDescription: 'B PLACE',
            batchRef: 'REFB',
            stockRotationDate: new Date().toISOString,
            quantity: 1,
            remarks: 'REMARKABLE',
            auditDepartmentCode: '00001234',
            partNumber: 'EMPTY'
        }
    ],
    itemsLoading: false,
    departments: [],
    clearDepartmentsSearch,
    searchDepartments,
    departmentsLoading: false,
    updateStockLocator,
    createStockLocator,
    deleteStockLocator,
    searchStoragePlaces,
    stockLocatorLoading: false,
    storagePlaces: [
        {
            id: 1,
            name: 'P123',
            description: 'LINN PALLET STORE',
            locationId: null,
            palletNumber: 123
        }
    ],
    clearStoragePlacesSearch: null,
    storagePlacesLoading: null,
    options: { partNumber: 'EMPTY' },
    snackbarVisible: false,
    setSnackbarVisible: jest.fn(),
    itemError: null,
    history: { push }
};

//eslint-disable-next-line react/jsx-props-no-spreading
const renderWith = props => render(<DeptStockUtility {...defaultProps} {...props} />);

describe('When items...', () => {
    beforeEach(() => {
        cleanup();
        renderWith({});
    });
    test('Should render initial state', () => {
        expect(screen.getByText('Departmental Pallets Utility')).toBeInTheDocument();
        const delButton = screen.getByRole('button', { name: 'Delete Selected' });
        expect(delButton).toBeDisabled();
        const saveButtons = screen.getAllByRole('button', { name: 'Save' });
        saveButtons.forEach(b => expect(b).toBeDisabled());
        const rows = screen.getAllByRole('row');
        expect(rows).toHaveLength(3); // column headers count as a row
    });
});

describe('When loading...', () => {
    beforeEach(() => {
        cleanup();
        renderWith({ itemsLoading: true });
    });
    test('Should render spinner', () => {
        expect(screen.getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('When row selected...', () => {
    beforeEach(() => {
        cleanup();
        renderWith({});
        const firstCheckbox = screen.getAllByRole('checkbox')[1];
        fireEvent.click(firstCheckbox);
    });

    test('Should enable delete', () => {
        const button = screen.getByRole('button', { name: 'Delete Selected' });
        expect(
            screen.getByRole('row', {
                name: 'Select Row checkbox PLACE A A PLACE REFA Invalid date 1 REMARKABLE 00001234'
            })
        ).toHaveClass('Mui-selected');
        expect(button).not.toBeDisabled();
    });

    test('Should not allow other row selection', () => {
        // the one we selected should be enabled so we can unselect
        expect(screen.getAllByRole('checkbox')[1]).not.toBeDisabled();

        // the other should be disabled
        expect(screen.getAllByRole('checkbox')[2]).toBeDisabled();
    });

    test('Should delete selected row when button clicked', () => {
        const delButton = screen.getByRole('button', { name: 'Delete Selected' });
        fireEvent.click(delButton);
        expect(deleteStockLocator).toHaveBeenCalledTimes(1);
        expect(deleteStockLocator).toHaveBeenCalledWith(
            1,
            expect.objectContaining({
                id: 1
            })
        );
    });
});

describe('When row edited...', () => {
    beforeEach(() => {
        cleanup();
        renderWith({});
        const cell = screen.getByRole('cell', { name: 'REFA' });
        fireEvent.doubleClick(cell);
    });

    test('Should enable Save', () => {
        const button = screen.getAllByRole('button', { name: 'Save' })[0];
        expect(button).not.toBeDisabled();
    });

    test('Should allow edit of this row', () => {
        expect(screen.getByRole('cell', { name: 'REFA' })).toHaveClass('MuiDataGrid-cell--editing');
    });

    test('Should not allow edit of other rows', () => {
        const cell = screen.getByRole('cell', { name: 'REFB' });
        fireEvent.doubleClick(cell);
        expect(screen.getByRole('cell', { name: 'REFB' })).not.toHaveClass(
            'MuiDataGrid-cell--editing'
        );
    });

    test('Should update edited row when button clicked', () => {
        const saveButton = screen.getAllByRole('button', { name: 'Save' })[0];
        fireEvent.click(saveButton);
        expect(updateStockLocator).toHaveBeenCalledTimes(1);
        expect(updateStockLocator).toHaveBeenCalledWith(
            1,
            expect.objectContaining({
                id: 1
            })
        );
    });
});

describe('When adding a row', () => {
    beforeEach(() => {
        cleanup();
        renderWith({});
        const button = screen.getByRole('button', { name: 'Add Row' });
        fireEvent.click(button);
    });

    test('Should add row', () => {
        const rows = screen.getAllByRole('row');
        expect(rows).toHaveLength(4);
    });

    test('Should allow edit of this row', () => {
        const cell = screen.getAllByRole('cell', { name: '' })[0]; // an empty cell on the new row
        fireEvent.doubleClick(cell);
        expect(cell).toHaveClass('MuiDataGrid-cell--editing');
    });

    test('Should not allow edit of other rows', () => {
        const cell = screen.getByRole('cell', { name: 'REFB' });
        fireEvent.doubleClick(cell);
        expect(screen.getByRole('cell', { name: 'REFB' })).not.toHaveClass(
            'MuiDataGrid-cell--editing'
        );
    });

    test('Should add  new row when button clicked', () => {
        const saveButton = screen.getAllByRole('button', { name: 'Save' })[0];
        fireEvent.click(saveButton);
        expect(createStockLocator).toHaveBeenCalledTimes(1);
        expect(createStockLocator).toHaveBeenCalledWith(
            expect.objectContaining({
                id: -2
            })
        );
    });
});
