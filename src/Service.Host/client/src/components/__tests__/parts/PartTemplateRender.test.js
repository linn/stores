import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, screen } from '@testing-library/react';
import render from '../../../test-utils';
import PartTemplateSearch from '../../parts/PartTemplatesSearch';

const privileges = ['part.admin'];

const partTemplate = [
    {
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
        paretoCode: 'E'
    }
];

const defaultRender = props =>
    render(
        // eslint-disable-next-line react/jsx-props-no-spreading
        <PartTemplateSearch privileges={privileges} partTemplates={[partTemplate]} {...props} />
    );

afterEach(() => cleanup());

describe('When creating...', () => {
    beforeEach(() => defaultRender({ editStatus: 'create' }));

    test('Should Render Title', () => {
        expect(screen.getByText('Part Template Utility')).toBeInTheDocument();
    });

    test('Should render Create button', () => {
        expect(screen.getByText('Create')).toBeInTheDocument();
    });

    test('Should render View button', () => {
        expect(screen.getByText('View')).toBeInTheDocument();
    });
});
