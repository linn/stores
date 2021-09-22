const initialState = { part: { partNumber: '' } };

export default function partReducer(state = initialState, action) {
    switch (action.type) {
        case 'initialise':
            return { ...state, part: action.payload, prevPart: action.payload };
        case 'fieldChange':
            if (action.fieldName === 'partNumber') {
                return {
                    ...state,
                    part: {
                        ...state.part,
                        partNumber: action.payload.trim()
                    }
                };
            }
            if (action.fieldName === 'rawOrFinished') {
                if (action.payload === 'F') {
                    return {
                        ...state,
                        part: {
                            ...state.part,
                            rawOrFinished: 'F',
                            nominalAccount: 564,
                            nominal: '0000000417',
                            nominalDescription: 'TOTAL COST OF SALES',
                            department: '0000002106',
                            departmentDescription: 'GROSS PROFIT'
                        }
                    };
                }
                return {
                    ...state,
                    part: {
                        ...state.part,
                        rawOrFinished: action.payload
                    }
                };
            }
            if (action.fieldName === 'nominalAccount') {
                return {
                    ...state,
                    part: {
                        ...state.part,
                        nominalAccount: action.payload.id,
                        nominal: action.payload.values[0].value,
                        nominalDescription: action.payload.values[1].value,
                        department: action.payload.values[2].value,
                        departmentDescription: action.payload.values[3].value
                    }
                };
            }
            if (action.fieldName === 'productAnalysisCode') {
                return {
                    ...state,
                    part: {
                        ...state.part,
                        productAnalysisCode: action.payload.name,
                        productAnalysisCodeDescription: action.payload.description
                    }
                };
            }
            if (action.fieldName === 'accountingCompany') {
                const updated =
                    action.payload === 'RECORDS'
                        ? {
                              ...state.part,
                              accountingCompany: action.payload,
                              paretoCode: 'R',
                              bomType: 'C',
                              linnProduced: 'N',
                              qcOnReceipt: 'N'
                          }
                        : { ...state.part, accountingCompany: action.payload, paretoCode: 'U' };
                return {
                    ...state,
                    part: updated
                };
            }
            if (action.fieldName === 'sernosSequenceName') {
                return {
                    ...state,
                    part: {
                        ...state.part,
                        sernosSequenceName: action.payload.name,
                        sernosSequenceDescription: action.payload.description
                    }
                };
            }
            if (action.fieldName === 'linnProduced') {
                if (action.payload === 'Y') {
                    return {
                        ...state,
                        part: {
                            ...state.part,
                            linnProduced: action.payload,
                            sernosSequenceName: 'SERIAL 1',
                            sernosSequenceDescription: 'MASTER SERIAL NUMBER RECORDS.'
                        }
                    };
                }
                return {
                    ...state,
                    part: {
                        ...state.part,
                        linnProduced: action.payload
                    }
                };
            }
            if (action.fieldName === 'preferredSupplier') {
                return {
                    ...state,
                    part: {
                        ...state.part,
                        preferredSupplier: action.payload.name,
                        preferredSupplierName: action.payload.description
                    }
                };
            }
            return {
                ...state,
                part: { ...state.part, [action.fieldName]: action.payload }
            };
        default:
            return state;
    }
}
