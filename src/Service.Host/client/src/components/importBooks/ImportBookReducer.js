const initialState = { impbook: { id: '' } };

export default function importBookReducer(state = initialState, action) {
    switch (action.type) {
        case 'initialise':
            return { ...state, impbook: action.payload, prevImpBook: action.payload };
        case 'fieldChange':
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
        case 'postEntriesUpdate':
            return {
                ...state,
                impbook: {
                    ...state.impbook,
                    importBookPostEntries: action.entries
                }
            };
        default:
            return state;
    }
}
