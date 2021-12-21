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

test('On Create - page renders all fields without crashing...', () => {
    const { getByText, queryByText } = render(
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
    expect(queryByText('PartRoot')).toBeNull();
    expect(getByText('linnProduced')).toBeNull();
});

test('save button should be disabled', () => {
    expect(screen.getAllByRole('button', { name: 'save-button' })).toHaveClass('Mui-disabled')

});

test('Should Render Title', () => {
    expect(screen.getByDisplayValue('Part Template Details')).toBeInTheDocument();
});

test('On View -  page renders populated fields', () => {
    const { getByText, getByDisplayValue } = render(
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
    expect(getByText('Part Root')).toBeInTheDocument();
    expect(getByDisplayValue('REACT')).toBeInTheDocument();

    expect(getByText('Seq Next Number')).toBeInTheDocument();
    expect(getByDisplayValue('123')).toBeInTheDocument();
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
        expect(screen.getByText('Part Root')).not.toBeDisabled();
      //  screen.debug(undefined, Infinity);
    });
});
