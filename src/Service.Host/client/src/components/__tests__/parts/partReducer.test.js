import partReducer from '../../parts/partReducer';

describe('partReducer tests', () => {
    it('should return initial state', () => {
        expect(partReducer(undefined, {})).toEqual({ part: { partNumber: '' } });
    });

    it('should handle initialise', () => {
        expect(
            partReducer(
                {},
                {
                    type: 'initialise',
                    payload: {
                        partNumber: '',
                        description: '',
                        accountingCompany: 'LINN',
                        psuPart: false,
                        stockControlled: true,
                        cccCriticalPart: false,
                        safetyCriticalPart: false,
                        paretoCode: 'U',
                        createdBy: 1,
                        createdByName: 'Name',
                        railMethod: 'POLICY',
                        preferredSupplier: 4415,
                        preferredSupplierName: 'Linn Products Ltd',
                        qcInformation: '',
                        qcOnReceipt: false,
                        orderHold: false
                    }
                }
            )
        ).toEqual({
            part: {
                partNumber: '',
                description: '',
                accountingCompany: 'LINN',
                psuPart: false,
                stockControlled: true,
                cccCriticalPart: false,
                safetyCriticalPart: false,
                paretoCode: 'U',
                createdBy: 1,
                createdByName: 'Name',
                railMethod: 'POLICY',
                preferredSupplier: 4415,
                preferredSupplierName: 'Linn Products Ltd',
                qcInformation: '',
                qcOnReceipt: false,
                orderHold: false
            },
            prevPart: {
                partNumber: '',
                description: '',
                accountingCompany: 'LINN',
                psuPart: false,
                stockControlled: true,
                cccCriticalPart: false,
                safetyCriticalPart: false,
                paretoCode: 'U',
                createdBy: 1,
                createdByName: 'Name',
                railMethod: 'POLICY',
                preferredSupplier: 4415,
                preferredSupplierName: 'Linn Products Ltd',
                qcInformation: '',
                qcOnReceipt: false,
                orderHold: false
            }
        });
    });

    it('should handle simple field changes', () => {
        expect(
            partReducer({}, { type: 'fieldChange', fieldName: 'partNumber', payload: ' PART' })
        ).toEqual({ part: { partNumber: 'PART' } });
    });

    it('should trim leading/trailing spaces from partNumber', () => {
        expect(
            partReducer({}, { type: 'fieldChange', fieldName: 'partNumber', payload: ' PART' })
        ).toEqual({ part: { partNumber: 'PART' } });
    });

    it('should handle rawOrFinished change', () => {
        expect(
            partReducer({}, { type: 'fieldChange', fieldName: 'rawOrFinished', payload: 'F' })
        ).toEqual({
            part: {
                rawOrFinished: 'F',
                nominalAccount: 564,
                nominal: '0000000417',
                nominalDescription: 'TOTAL COST OF SALES',
                department: '0000002106',
                departmentDescription: 'GROSS PROFIT'
            }
        });
    });

    it('should handle nominalAccount change', () => {
        expect(
            partReducer(
                {},
                {
                    type: 'fieldChange',
                    fieldName: 'nominalAccount',
                    payload: {
                        id: 1,
                        values: [
                            { value: 'name' },
                            { value: 'description' },
                            { value: 'department' },
                            { value: 'department description' }
                        ]
                    }
                }
            )
        ).toEqual({
            part: {
                nominalAccount: 1,
                nominal: 'name',
                nominalDescription: 'description',
                department: 'department',
                departmentDescription: 'department description'
            }
        });
    });

    it('should handle productAnalysisCode change', () => {
        expect(
            partReducer(
                {},
                {
                    type: 'fieldChange',
                    fieldName: 'productAnalysisCode',
                    payload: {
                        name: 'CODE',
                        description: 'Description'
                    }
                }
            )
        ).toEqual({
            part: {
                productAnalysisCode: 'CODE',
                productAnalysisCodeDescription: 'Description'
            }
        });
    });

    it('should handle accountingCompany Change', () => {
        expect(
            partReducer(
                {},
                {
                    type: 'fieldChange',
                    fieldName: 'accountingCompany',
                    payload: 'RECORDS'
                }
            )
        ).toEqual({
            part: {
                accountingCompany: 'RECORDS',
                paretoCode: 'R',
                bomType: 'C',
                linnProduced: 'N',
                qcOnReceipt: 'N'
            }
        });
        expect(
            partReducer(
                {},
                {
                    type: 'fieldChange',
                    fieldName: 'accountingCompany',
                    payload: 'LINN'
                }
            )
        ).toEqual({
            part: {
                accountingCompany: 'LINN',
                paretoCode: 'U'
            }
        });
    });
    it('should handle sernosSequence Change', () => {
        expect(
            partReducer(
                {},
                {
                    type: 'fieldChange',
                    fieldName: 'sernosSequenceName',
                    payload: { name: 'name', description: 'description' }
                }
            )
        ).toEqual({
            part: {
                sernosSequenceName: 'name',
                sernosSequenceDescription: 'description'
            }
        });
    });
    it('should handle linnProduced Change', () => {
        expect(
            partReducer(
                {},
                {
                    type: 'fieldChange',
                    fieldName: 'linnProduced',
                    payload: 'Y'
                }
            )
        ).toEqual({
            part: {
                linnProduced: 'Y',
                sernosSequenceName: 'SERIAL 1',
                sernosSequenceDescription: 'MASTER SERIAL NUMBER RECORDS.'
            }
        });
        expect(
            partReducer(
                {},
                {
                    type: 'fieldChange',
                    fieldName: 'linnProduced',
                    payload: 'N'
                }
            )
        ).toEqual({
            part: {
                linnProduced: 'N'
            }
        });
    });
    it('should handle preferredSupplier Change', () => {
        expect(
            partReducer(
                {},
                {
                    type: 'fieldChange',
                    fieldName: 'preferredSupplier',
                    payload: { name: 'id', description: 'name' }
                }
            )
        ).toEqual({
            part: {
                preferredSupplier: 'id',
                preferredSupplierName: 'name'
            }
        });
    });
});
