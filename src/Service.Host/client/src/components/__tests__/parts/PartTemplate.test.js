import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, screen } from '@testing-library/react';
import render from '../../../test-utils';
import PartTemplate from '../../parts/PartTemplate';

afterEach(cleanup);

const addItem = jest.fn();
const updateItem = jest.fn();
const setEditStatus = jest.fn();
const setSnackbarVisible = jest.fn();
const searchProductAnalysisCodes = jest.fn();
const clearProductAnalysisCodesSearch = jest.fn();
const history = {
    push: jest.fn()
};

const item = {
    partRoot: 'REACT',
    description: 'A test',
    hasDataSheet: 'Y',
    hasNumberSequence: 'Y',
    nextNumber: '123',
    allowVariants: 'Y',
    variants: 'Integration UNIT',
    accountingCompany: 'LINN',
    productCode: 'MECHANICAL',
    stockControlled: 'Y',
    linnProduced: 'Y',
    rmfgCode: 'B',
    bomType: 'A',
    assemblyTechnology: 'TH',
    allowPartCreation: 'Y',
    paretoCode: 'E',
    links: { href: '', rel: 'self' }
};

describe('On Create', () => {
    beforeEach(() => {
        render(
            <PartTemplate
                editStatus="create"
                searchProductAnalysisCodes={searchProductAnalysisCodes}
                clearProductAnalysisCodesSearch={clearProductAnalysisCodesSearch}
                setSnackbarVisible={setSnackbarVisible}
                history={history}
                addItem={addItem}
                updateItem={updateItem}
                setEditStatus={setEditStatus}
                privileges={['part.admin']}
                itemId={1}
            />
        );
    });

    test('page renders all fields without crashing...', () => {
        expect(screen.queryByText('PartRoot')).toBeNull();
    });

    test('save button should be disabled', () => {
        expect(screen.getByRole('button', { name: 'Save' })).toHaveClass('Mui-disabled');
    });

    test('Should Render Title', () => {
        expect(screen.getByText('Create New Part Template')).toBeInTheDocument();
    });
});

describe('On View', () => {
    beforeEach(() => {
        render(
            <PartTemplate
                editStatus="view"
                searchProductAnalysisCodes={searchProductAnalysisCodes}
                clearProductAnalysisCodesSearch={clearProductAnalysisCodesSearch}
                setSnackbarVisible={setSnackbarVisible}
                history={history}
                addItem={addItem}
                updateItem={updateItem}
                setEditStatus={setEditStatus}
                item={item}
                privileges={['part.admin']}
            />
        );
    });

    test('Page renders populated fields', () => {
        expect(screen.getAllByText('Part Root')[0]).toBeInTheDocument();
        expect(screen.getByDisplayValue('REACT')).toBeInTheDocument();

        expect(screen.getAllByText('Description')[0]).toBeInTheDocument();
        expect(screen.getByDisplayValue('A test')).toBeInTheDocument();

        expect(screen.getByText('Seq Next Number')).toBeInTheDocument();
        expect(screen.getByDisplayValue('123')).toBeInTheDocument();

        expect(screen.getByText('Variants')).toBeInTheDocument();
        expect(screen.getByDisplayValue('Integration UNIT')).toBeInTheDocument();

    });
});

describe('When have no permissions/privileges', () => {
    beforeEach(() => {
        render(
            <PartTemplate
                editStatus="view"
                searchProductAnalysisCodes={searchProductAnalysisCodes}
                clearProductAnalysisCodesSearch={clearProductAnalysisCodesSearch}
                setSnackbarVisible={setSnackbarVisible}
                history={history}
                addItem={addItem}
                updateItem={updateItem}
                setEditStatus={setEditStatus}
                item={item}
                privileges={[]}
            />
        );
    });
    test('cannot edit fields', () => {
        expect(screen.getByDisplayValue('REACT')).toBeDisabled();
        expect(screen.getByDisplayValue('123')).toBeDisabled();
        expect(screen.getByDisplayValue('MECHANICAL')).toBeDisabled();
        expect(screen.getByDisplayValue('A test')).toBeDisabled();
        expect(screen.getByDisplayValue('Integration UNIT')).toBeDisabled();
    });
});
