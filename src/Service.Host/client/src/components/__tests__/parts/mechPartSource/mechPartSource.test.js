/**
 * @jest-environment jest-environment-jsdom-sixteen
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen, waitFor } from '@testing-library/react';
import render from '../../../../test-utils';
import MechPartSource from '../../../parts/mechPartSource/MechPartSource';

const updateItem = jest.fn();
const addItem = jest.fn();
const setSnackbarVisible = jest.fn();
const history = { goBack: jest.fn() };
const setEditStatus = jest.fn();

const defaultRender = props =>
    render(
        <MechPartSource
            updateItem={updateItem}
            addItem={addItem}
            setSnackbarVisible={setSnackbarVisible}
            history={history}
            setEditStatus={setEditStatus}
            editStatus="view"
            //eslint-disable-next-line react/jsx-props-no-spreading
            {...props}
        />
    );

afterEach(() => cleanup());

describe('When creating...', () => {
    beforeEach(() => defaultRender({ editStatus: 'create' }));

    test('should render creating title', () => {
        expect(screen.getByText('Create Mech Part Source')).toBeInTheDocument();
    });

    test('should have save button disabled before any data', () => {
        expect(screen.getByRole('button', { name: 'Save' })).toHaveClass('Mui-disabled');
    });

    describe('when required fields populated...', () => {
        beforeEach(async () => {
            const assemblyTypeDropdownOption = await screen.findByRole('option', { name: 'TH' });
            fireEvent.click(assemblyTypeDropdownOption);

            const partTypeDropdownOption = await screen.findByRole('option', { name: 'RES' });
            fireEvent.click(partTypeDropdownOption);
        });

        test('should enable save button', () => {
            waitFor(() =>
                expect(screen.getByRole('button', { name: 'Save' })).not.toHaveClass('Mui-disabled')
            );
        });
    });
});

describe('When viewing...', () => {
    beforeEach(() => defaultRender({ item: {} }));

    test('should render viewing title', () => {
        expect(screen.getByText('Mech Part Source Details')).toBeInTheDocument();
    });
});
