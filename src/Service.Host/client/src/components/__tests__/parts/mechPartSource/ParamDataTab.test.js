import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, fireEvent, screen } from '@testing-library/react';
import render from '../../../../test-utils';
import ParamDataTab from '../../../parts/mechPartSource/tabs/ParamDataTab';

const handleFieldChange = jest.fn();
const creatingTrue = () => true;

// const defaultProps = { creating: true };

describe('When part type RES...', () => {
    let component;
    beforeEach(() => {
        component = (
            <ParamDataTab
                creating={creatingTrue}
                partType="RES"
                handleFieldChange={handleFieldChange}
            />
        );
        render(component);
    });

    test('should render resistor specific fields', () => {
        expect(screen.getByLabelText('Resistance')).toBeInTheDocument();
    });
});

describe('When part type CAP...', () => {
    let component;
    beforeEach(() => {
        component = (
            <ParamDataTab
                creating={creatingTrue}
                partType="CAP"
                handleFieldChange={handleFieldChange}
            />
        );
        render(component);
    });

    test('should render capacitor specific fields', () => {
        expect(screen.getByLabelText('Capacitance')).toBeInTheDocument();
    });
});

describe('When part type TRAN...', () => {
    let component;
    beforeEach(() => {
        component = (
            <ParamDataTab
                creating={creatingTrue}
                partType="CAP"
                handleFieldChange={handleFieldChange}
            />
        );
        render(component);
    });

    test('should render transistor specific fields', () => {
        expect(screen.getByLabelText('Dielectric')).toBeInTheDocument();
    });
});

describe('When part type IC...', () => {
    let component;
    beforeEach(() => {
        component = (
            <ParamDataTab
                creating={creatingTrue}
                partType="IC"
                handleFieldChange={handleFieldChange}
            />
        );
        render(component);
    });

    test('should render IC specific fields', () => {
        expect(screen.getByLabelText('Function')).toBeInTheDocument();
    });
});
