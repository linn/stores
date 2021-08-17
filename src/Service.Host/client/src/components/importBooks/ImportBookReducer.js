const initialState = { impbook: { id: '' } };

export default function importBookReducer(state = initialState, action) {
    switch (action.type) {
        case 'initialise':
            return { ...state, ...action.payload, prevImpBook: action.payload };
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
                [action.fieldName]: action.payload
            };
        case 'orderDetailFieldChange':
            return {
                ...state,
                importBookOrderDetails: [
                    ...state.importBookOrderDetails.filter(x => x.lineNumber !== action.lineId),
                    action.payload
                ]
            };
        case 'postEntryFieldChange':
            return {
                ...state,
                importBookPostEntries: [
                    ...state.importBookPostEntries.filter(x => x.lineNumber !== action.lineId),
                    action.payload
                ]
            };
        default:
            return state;
    }
}
