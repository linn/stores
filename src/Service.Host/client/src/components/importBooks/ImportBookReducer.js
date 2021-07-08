const initialState = { impbook: { id: '' } };

export default function importBookReducer(state = initialState, action) {
    switch (action.type) {
        case 'initialise':
            console.log('setting state in init func');
            return { ...state, ...action.payload, prevImpBook: action.payload };
        case 'fieldChange':
            console.log(
                `setting state in field change func - ${action.fieldName}, ${action.payload}`
            );
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
        case 'orderDetailsFieldChange':
            console.log(
                `setting state in orderDetails field change func - ${action.fieldName}, ${action.payload}`
            );
            return {
                ...state,
                orderDetails: { ...state.orderDetails, [action.fieldName]: action.payload }
            };
        default:
            return state;
    }
}
