const initialState = {};

export default function consignmentReducer(state = initialState, action) {
    switch (action.type) {
        case 'initialise': {
            return {
                consignment: action.payload,
                originalConsignment: action.payload
            };
        }
        case 'reset': {
            return {
                consignment: state.originalConsignment,
                originalConsignment: state.originalConsignment
            };
        }
        case 'updateField':
            return {
                ...state,
                consignment: { ...state.consignment, [action.fieldName]: action.payload }
            };
        case 'updatePallets': {
            let newPallets = [];
            if (action.payload?.length > 0) {
                newPallets = action.payload.map(item => {
                    return { ...item, consignmentId: state.consignment.consignmentId };
                });
            } else {
                newPallets = [];
            }

            return {
                ...state,
                consignment: { ...state.consignment, pallets: newPallets }
            };
        }
        default:
            return state;
    }
}
