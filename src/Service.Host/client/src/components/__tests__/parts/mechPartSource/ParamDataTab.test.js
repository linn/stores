import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup, screen } from '@testing-library/react';
import render from '../../../../test-utils';
import ParamDataTab from '../../../parts/mechPartSource/tabs/ParamDataTab';

const handleFieldChange = jest.fn();
const creating = res => () => res;

describe('When part type RES...', () => {
    let component;
    beforeEach(() => {
        cleanup();
        component = (
            <ParamDataTab
                creating={creating(true)}
                partType="RES"
                handleFieldChange={handleFieldChange}
            />
        );
        render(component);
    });

    test('should render resistor specific fields', () => {
        expect(screen.getByLabelText('Resistance')).toBeInTheDocument();
    });

    describe('When units Ω...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="RES"
                    handleFieldChange={handleFieldChange}
                    resistance={3300}
                    resistanceUnits="Ω"
                />
            );
            render(component);
        });
        test('should correctly render resistance value with units', () => {
            expect(screen.getByLabelText('Resistance').value).toBe('3300');
            expect(screen.getByLabelText('units').value).toBe('Ω');
        });
    });

    describe('When units KΩ and reistance divides by 1000...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="RES"
                    handleFieldChange={handleFieldChange}
                    resistance={3000}
                    resistanceUnits="KΩ"
                />
            );
            render(component);
        });
        test('should correctly render resistance value with units', () => {
            expect(screen.getByLabelText('Resistance').value).toBe('3');
            expect(screen.getByLabelText('units').value).toBe('KΩ');
        });
    });

    describe('When units KΩ and reistance does not divide by 1000...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="RES"
                    handleFieldChange={handleFieldChange}
                    resistance={3300}
                    resistanceUnits="KΩ"
                />
            );
            render(component);
        });
        test('should correctly render resistance value with units', () => {
            expect(screen.getByLabelText('Resistance').value).toBe('3.3');
            expect(screen.getByLabelText('units').value).toBe('KΩ');
        });
    });

    describe('When units MΩ and reistance divides by 1000000...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="RES"
                    handleFieldChange={handleFieldChange}
                    resistance={3000000}
                    resistanceUnits="MΩ"
                />
            );
            render(component);
        });
        test('should correctly render resistance value with units', () => {
            expect(screen.getByLabelText('Resistance').value).toBe('3');
            expect(screen.getByLabelText('units').value).toBe('MΩ');
        });
    });

    describe('When units KΩ and reistance does not divide by 1000000...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="RES"
                    handleFieldChange={handleFieldChange}
                    resistance={3300000}
                    resistanceUnits="MΩ"
                />
            );
            render(component);
        });
        test('should correctly render resistance value with units', () => {
            expect(screen.getByLabelText('Resistance').value).toBe('3.3');
            expect(screen.getByLabelText('units').value).toBe('MΩ');
        });
    });

    describe('When resistance  0...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="RES"
                    handleFieldChange={handleFieldChange}
                    resistance={0}
                    resistanceUnits="MΩ"
                />
            );
            render(component);
        });
        test('should correctly render resistance value with units', () => {
            expect(screen.getByLabelText('Resistance').value).toBe('0');
            expect(screen.getByLabelText('units').value).toBe('MΩ');
        });
    });
});

describe('When part type CAP...', () => {
    let component;
    beforeEach(() => {
        cleanup();
        component = (
            <ParamDataTab
                creating={creating(true)}
                partType="CAP"
                handleFieldChange={handleFieldChange}
            />
        );
        render(component);
    });

    test('should render capacitor specific fields', () => {
        expect(screen.getByLabelText('Capacitance')).toBeInTheDocument();
    });

    describe('When units uF and capacitance multiple of 0.000001...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="CAP"
                    handleFieldChange={handleFieldChange}
                    capacitance={0.000006}
                    capacitanceUnits="uF"
                />
            );
            render(component);
        });
        test('should correctly render capacitacne value with units', () => {
            expect(screen.getByLabelText('Capacitance').value).toBe('6');
            expect(screen.getByLabelText('units').value).toBe('uF');
        });
    });

    describe('When units uF and capacitance not multiple of 0.000001...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="CAP"
                    handleFieldChange={handleFieldChange}
                    capacitance={0.0000065}
                    capacitanceUnits="uF"
                />
            );
            render(component);
        });
        test('should correctly render capacitacne value with units', () => {
            expect(screen.getByLabelText('Capacitance').value).toBe('6.5');
            expect(screen.getByLabelText('units').value).toBe('uF');
        });
    });

    describe('When units nF and capacitance multiple of 0.000001...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="CAP"
                    handleFieldChange={handleFieldChange}
                    capacitance={0.000000006}
                    capacitanceUnits="nF"
                />
            );
            render(component);
        });
        test('should correctly render capacitacne value with units', () => {
            expect(screen.getByLabelText('Capacitance').value).toBe('6');
            expect(screen.getByLabelText('units').value).toBe('nF');
        });
    });

    describe('When units nF and capacitance not multiple of 0.000001...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="CAP"
                    handleFieldChange={handleFieldChange}
                    capacitance={0.0000000065}
                    capacitanceUnits="nF"
                />
            );
            render(component);
        });
        test('should correctly render capacitacne value with units', () => {
            expect(screen.getByLabelText('Capacitance').value).toBe('6.5');
            expect(screen.getByLabelText('units').value).toBe('nF');
        });
    });

    describe('When units pF and capacitance multiple of 0.000000000001...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="CAP"
                    handleFieldChange={handleFieldChange}
                    capacitance={0.000000000006}
                    capacitanceUnits="pF"
                />
            );
            render(component);
        });
        test('should correctly render capacitacne value with units', () => {
            expect(screen.getByLabelText('Capacitance').value).toBe('6');
            expect(screen.getByLabelText('units').value).toBe('pF');
        });
    });

    describe('When units nF and capacitance not multiple of 0.000000001...', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="CAP"
                    handleFieldChange={handleFieldChange}
                    capacitance={0.0000000000065}
                    capacitanceUnits="pF"
                />
            );
            render(component);
        });
        test('should correctly render capacitacne value with units', () => {
            expect(screen.getByLabelText('Capacitance').value).toBe('6.5');
            expect(screen.getByLabelText('units').value).toBe('pF');
        });
    });

    describe('When capacitance 0', () => {
        beforeEach(() => {
            cleanup();
            component = (
                <ParamDataTab
                    creating={creating(true)}
                    partType="CAP"
                    handleFieldChange={handleFieldChange}
                    capacitance={0}
                    capacitanceUnits="pF"
                />
            );
            render(component);
        });
        test('should correctly render capacitance value with units', () => {
            expect(screen.getByLabelText('Capacitance').value).toBe('0');
        });
    });
});

describe('When part type TRAN...', () => {
    let component;
    beforeEach(() => {
        cleanup();

        component = (
            <ParamDataTab
                creating={creating(true)}
                partType="TRAN"
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
        cleanup();

        component = (
            <ParamDataTab
                creating={creating(true)}
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
