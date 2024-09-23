import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen } from '@testing-library/react';
import render from '../../../../test-utils';
import MechPartSource from '../../../parts/mechPartSource/MechPartSource';

const updateItem = jest.fn();
const addItem = jest.fn();
const setSnackbarVisible = jest.fn();
const history = { goBack: jest.fn() };
const setEditStatus = jest.fn();
const clearErrors = jest.fn();
const defaultRender = props =>
    render(
        <MechPartSource
            updateItem={updateItem}
            addItem={addItem}
            setSnackbarVisible={setSnackbarVisible}
            history={history}
            setEditStatus={setEditStatus}
            editStatus="view"
            clearErrors={clearErrors}
            //eslint-disable-next-line react/jsx-props-no-spreading
            {...props}
        />
    );

afterEach(() => cleanup());

describe('When creating...', () => {
    beforeEach(() => defaultRender({ editStatus: 'create' }));

    test('should render creating title', () => {
        expect(screen.getByText('Create Part Source Sheet')).toBeInTheDocument();
    });

    test('should have save button disabled before any data', () => {
        expect(screen.getByRole('button', { name: 'Save' })).toHaveClass('Mui-disabled');
    });

    describe('when required fields populated...', () => {
        beforeEach(() => {
            const assemblyDropdown = screen.getByLabelText('Assembly Type*');
            fireEvent.change(assemblyDropdown, { target: { value: 'TH' } });

            const mechElecDropdown = screen.getByLabelText('Mechanical Or Electrical*');
            fireEvent.change(mechElecDropdown, { target: { value: 'M' } });
        });

        test('should enable save button', () => {
            expect(screen.getByRole('button', { name: 'Save' })).not.toHaveClass('Mui-disabled');
        });

        test('should call addItem', () => {
            const saveButton = screen.getByRole('button', { name: 'Save' });
            fireEvent.click(saveButton);
            expect(addItem).toHaveBeenCalledWith(
                expect.objectContaining({ assemblyType: 'TH', mechanicalOrElectrical: 'M' })
            );
        });
    });

    describe('when not Electrical Part...', () => {
        beforeEach(() => {
            const mechElecDropdown = screen.getByLabelText('Mechanical Or Electrical*');
            fireEvent.change(mechElecDropdown, { target: { value: 'M' } });
        });

        test('should disable Param Data and DataSheets tabs', () => {
            const dataSheetsTab = screen.getByText('DataSheets');
            fireEvent.click(dataSheetsTab);
            expect(screen.queryByText('Path')).not.toBeInTheDocument();
            const ParamDataTab = screen.getByText('Param Data');
            fireEvent.click(ParamDataTab);
            expect(screen.queryByText('No Part Type Selected.')).not.toBeInTheDocument();
        });
    });

    describe('when Electrical Part...', () => {
        beforeEach(() => {
            const assemblyDropdown = screen.getByLabelText('Assembly Type*');
            fireEvent.change(assemblyDropdown, { target: { value: 'TH' } });
            const mechElecDropdown = screen.getByLabelText('Mechanical Or Electrical*');
            fireEvent.change(mechElecDropdown, { target: { value: 'E' } });
        });

        test('should show part type dropdown', () => {
            expect(screen.queryByLabelText('Part Type*')).toBeInTheDocument();
        });

        describe('when Part Type not entered...', () => {
            test('should disable save button', () => {
                expect(screen.getByRole('button', { name: 'Save' })).toHaveClass('Mui-disabled');
            });
        });

        describe('when Part Type entered...', () => {
            test('should enable save button', () => {
                const partTypeDropdown = screen.getByLabelText('Part Type*');
                fireEvent.change(partTypeDropdown, { target: { value: 'RES' } });
                expect(screen.getByRole('button', { name: 'Save' })).not.toHaveClass(
                    'Mui-disabled'
                );
            });
        });
    });
});

describe('When creating Capacitor...', () => {
    test('should call addItem with capacitance', () => {
        jest.clearAllMocks();

        defaultRender({ editStatus: 'create' });
        const assemblyDropdown = screen.getByLabelText('Assembly Type*');
        fireEvent.change(assemblyDropdown, { target: { value: 'TH' } });

        const mechElecDropdown = screen.getByLabelText('Mechanical Or Electrical*');
        fireEvent.change(mechElecDropdown, { target: { value: 'E' } });
        const partTypeDropdown = screen.getByLabelText('Part Type*');
        fireEvent.change(partTypeDropdown, { target: { value: 'CAP' } });

        const ParamDataTab = screen.getByText('Param Data');
        fireEvent.click(ParamDataTab);

        const unitsDropdown = screen.getByLabelText('units');
        fireEvent.change(unitsDropdown, { target: { value: 'uF' } });

        const capacitanceInput = screen.getByLabelText('Capacitance');
        fireEvent.change(capacitanceInput, { target: { value: '68' } });

        const saveButton = screen.getByRole('button', { name: 'Save' });
        fireEvent.click(saveButton);
        expect(addItem).toHaveBeenCalledWith(
            expect.objectContaining({
                assemblyType: 'TH',
                mechanicalOrElectrical: 'E',
                capacitance: '0.0000680000000' // 68 micro farads
            })
        );
    });
});

describe('When viewing...', () => {
    beforeEach(() => {
        cleanup();
        defaultRender({
            item: {
                id: 1,
                partNumber: 'PART',
                mechanicalOrElectrical: 'E',
                partType: 'RES',
                assemblyType: 'SM',
                samplesRequired: 'N'
            },
            editStatus: 'view'
        });
    });

    test('should render viewing title', () => {
        expect(screen.getByText('Part Source Sheet Details')).toBeInTheDocument();
    });

    test('save button should be disabled', () => {
        expect(screen.getByRole('button', { name: 'Save' })).toHaveClass('Mui-disabled');
    });

    describe('When field changed...', () => {
        beforeEach(() => {
            setEditStatus.mockClear();
            const samplesReqDropdown = screen.getByLabelText('Samples Required*');
            fireEvent.change(samplesReqDropdown, { target: { value: 'Y' } });
        });

        test('should setEditStatus', () => expect(setEditStatus).toHaveBeenCalledWith('edit'));
    });
});

describe('When editing...', () => {
    beforeEach(() => {
        cleanup();
        defaultRender({
            itemId: 1,
            item: {
                id: 1,
                partNumber: 'PART',
                mechanicalOrElectrical: 'E',
                partType: 'RES',
                assemblyType: 'SM',
                samplesRequired: 'Y'
            },
            editStatus: 'edit'
        });
    });

    test('should call updateItem when save clicked', () => {
        const saveButton = screen.getByRole('button', { name: 'Save' });
        fireEvent.click(saveButton);
        expect(updateItem).toHaveBeenCalledWith(
            1,
            expect.objectContaining({
                id: 1,
                partNumber: 'PART',
                mechanicalOrElectrical: 'E',
                partType: 'RES',
                assemblyType: 'SM',
                samplesRequired: 'Y'
            })
        );
    });
});

describe('When loading...', () => {
    beforeEach(() => defaultRender({ loading: true }));

    test('should render loading spinner', () => {
        expect(screen.getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('When clicking tabs...', () => {
    beforeEach(() => {
        defaultRender({ editStatus: 'create' });
    });

    test('should render proposal tab', () => {
        const tab = screen.getByText('Proposal');
        fireEvent.click(tab);
        expect(screen.getByText('Proposed By')).toBeInTheDocument();
    });

    test('should render Datasheets tab', () => {
        const tab = screen.getByText('DataSheets');
        fireEvent.click(tab);
        expect(screen.getByText('Path')).toBeInTheDocument();
    });

    test('should render Quality Requirements tab', () => {
        const tab = screen.getByText('Quality Requirements');
        fireEvent.click(tab);
        expect(screen.getByText('Drawings Package')).toBeInTheDocument();
    });

    test('should render Suppliers tab', () => {
        const tab = screen.getByText('Suppliers');
        fireEvent.click(tab);
        expect(screen.getByText('Supplier')).toBeInTheDocument();
        expect(screen.getByText('Name')).toBeInTheDocument();
        expect(screen.getByText('Part Number')).toBeInTheDocument();
    });

    test('should render Manufacturers tab', () => {
        const tab = screen.getByText('Manufacturers');
        fireEvent.click(tab);
        expect(screen.getByText('Preference')).toBeInTheDocument();
        expect(screen.getByText('Manufacturer')).toBeInTheDocument();
    });

    test('should render Param Data tab', () => {
        const tab = screen.getByText('Param Data');
        fireEvent.click(tab);
        expect(screen.getByText('No Part Type Selected.')).toBeInTheDocument();
    });

    test('should render Cad Data tab', () => {
        const tab = screen.getByText('Cad Data');
        fireEvent.click(tab);
        expect(screen.getByText('Library Ref')).toBeInTheDocument();
        expect(screen.getByText('Footprint Ref')).toBeInTheDocument();
    });

    test('should render Quotes tab', () => {
        const tab = screen.getByText('Quotes');
        fireEvent.click(tab);
        expect(screen.getByText('Configuration')).toBeInTheDocument();
        expect(screen.getByText('Life Expectancy Part')).toBeInTheDocument();
    });

    test('should render Usages tab', () => {
        const tab = screen.getByText('Usages');
        fireEvent.click(tab);
        expect(screen.getByText('Product')).toBeInTheDocument();
        expect(screen.getByText('Quantity Per Assembly')).toBeInTheDocument();
    });

    test('should render Verification tab', () => {
        const tab = screen.getByText('Verification');
        fireEvent.click(tab);
        expect(screen.getByText('Part Created By')).toBeInTheDocument();
        expect(screen.getByText('Purchasing')).toBeInTheDocument();
        expect(screen.getByText('Quality')).toBeInTheDocument();
    });
});

describe('When url specifies tab...', () => {
    test('should render Proposal tab', () => {
        defaultRender({ editStatus: 'create', options: { tab: 'proposal' } });
        expect(screen.getByText('Proposed By')).toBeInTheDocument();
    });

    test('should render Datasheets tab', () => {
        defaultRender({ editStatus: 'create', options: { tab: 'dataSheets' } });
        expect(screen.getByText('Path')).toBeInTheDocument();
    });

    test('should render Quality Requirements tab', () => {
        defaultRender({ editStatus: 'create', options: { tab: 'qualityRequirements' } });
        expect(screen.getByText('Drawings Package')).toBeInTheDocument();
    });

    test('should render Suppliers tab', () => {
        defaultRender({ editStatus: 'create', options: { tab: 'suppliers' } });
        expect(screen.getByText('Supplier')).toBeInTheDocument();
        expect(screen.getByText('Name')).toBeInTheDocument();
        expect(screen.getByText('Part Number')).toBeInTheDocument();
    });

    test('should render Manufacturers tab', () => {
        defaultRender({ editStatus: 'create', options: { tab: 'manufacturers' } });
        expect(screen.getByText('Preference')).toBeInTheDocument();
        expect(screen.getByText('Manufacturer')).toBeInTheDocument();
    });

    test('should render Param Data tab', () => {
        defaultRender({ editStatus: 'create', options: { tab: 'paramData' } });
        expect(screen.getByText('No Part Type Selected.')).toBeInTheDocument();
    });

    test('should render Cad Data tab', () => {
        defaultRender({ editStatus: 'create', options: { tab: 'cadData' } });
        expect(screen.getByText('Library Ref')).toBeInTheDocument();
        expect(screen.getByText('Footprint Ref')).toBeInTheDocument();
    });

    test('should render Quotes tab', () => {
        defaultRender({ editStatus: 'create', options: { tab: 'quotes' } });
        expect(screen.getByText('Configuration')).toBeInTheDocument();
        expect(screen.getByText('Life Expectancy Part')).toBeInTheDocument();
    });

    test('should render Usages tab', () => {
        defaultRender({ editStatus: 'create', options: { tab: 'usages' } });
        expect(screen.getByText('Product')).toBeInTheDocument();
        expect(screen.getByText('Quantity Per Assembly')).toBeInTheDocument();
    });

    test('should render Verification tab', () => {
        defaultRender({ editStatus: 'create', options: { tab: 'verification' } });
        expect(screen.getByText('Part Created By')).toBeInTheDocument();
        expect(screen.getByText('Purchasing')).toBeInTheDocument();
        expect(screen.getByText('Quality')).toBeInTheDocument();
    });
});

describe('When rkmCode...', () => {
    beforeEach(() => {
        cleanup();
        defaultRender({
            options: { tab: 'paramData' },
            item: {
                id: 1,
                partNumber: 'PART',
                mechanicalOrElectrical: 'E',
                partType: 'RES',
                assemblyType: 'SM',
                samplesRequired: 'N',
                rkmCode: '3M1'
            },
            editStatus: 'view'
        });
    });

    test('should pass correct resistance units to Param Data tab', () => {
        expect(screen.getByLabelText('units').value).toBe('MΩ');
    });
});
