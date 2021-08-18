const initialState = { impbook: { id: '' } };

export default function importBookReducer(state = initialState, action) {
    switch (action.type) {
        case 'initialise':
            return { ...state, impbook: action.payload, prevImpBook: action.payload };
        case 'fieldChange':
            if (action.fieldName === 'something with multiple bits') {
                return {
                    ...state,
                    impbook: {
                        ...state.impbook,
                        sernosSequenceName: action.payload.name,
                        sernosSequenceDescription: action.payload.description
                    }
                };
            }
            return {
                ...state,
                impbook: { ...state.impbook, [action.fieldName]: action.payload }
            };
        case 'orderDetailFieldChange':
            return {
                ...state,
                impbook: {
                    ...state.impbook,
                    importBookOrderDetails: [
                        ...state.impbook.importBookOrderDetails.filter(
                            x => x.lineNumber !== action.lineNumber
                        ),
                        action.payload
                    ]
                }
            };
        case 'orderDetailAdd':
            return {
                ...state,
                impbook: {
                    ...state.impbook,
                    importBookOrderDetails: [
                        ...state.impbook.importBookOrderDetails,
                        {
                            lineNumber:
                                Math.max([
                                    state.impbook.importBookOrderDetails?.map(x => {
                                        return x.lineNumber;
                                    })
                                ]) + 1
                        }
                    ]
                }
            };
        case 'orderDetailRemove':
            return {
                ...state,
                impbook: {
                    ...state.impbook,
                    importBookOrderDetails: [
                        ...state.impbook.importBookOrderDetails.filter(
                            x => x.lineNumber !== action.lineNumber
                        )
                    ]
                }
            };
        case 'postEntryFieldChange':
            return {
                ...state,
                impbook: {
                    ...state.impbook,
                    importBookPostEntries: [
                        ...state.impbook.importBookPostEntries.filter(
                            x => x.lineNumber !== action.lineId
                        ),
                        action.payload
                    ]
                }
            };
        case 'postEntryAdd':
            return {
                ...state,
                impbook: {
                    ...state.impbook,
                    importBookPostEntries: [...state.impbook.importBookPostEntries, action.payload]
                }
            };
        case 'postEntryRemove':
            return {
                ...state,
                impbook: {
                    ...state.impbook,
                    importBookPostEntries: [
                        ...state.impbook.importBookPostEntries.filter(
                            x => x.lineNumber !== action.lineId
                        )
                    ]
                }
            };
        default:
            return state;
    }
}
