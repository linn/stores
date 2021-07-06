const initialState = { impbook: { id: '' } };

export default function importBookReducer(state = initialState, action) {
    switch (action.type) {
        case 'initialise':
            return { ...state, impbook: action.payload, prevImpbook: action.payload };
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
        default:
            return state;
    }
}
