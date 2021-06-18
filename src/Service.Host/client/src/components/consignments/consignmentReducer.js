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
        case 'createPallet': {
            const newPallets = state.consignment.pallets.slice();
            const nextPalletNumber = state.consignment.pallets
                ? state.consignment.pallets.length + 1
                : 1;
            newPallets.push({ palletNumber: nextPalletNumber });
            return {
                ...state,
                consignment: { ...state.consignment, pallets: newPallets }
            };
        }
        case 'updatePallets': {
            const newPallets = action.payload;
            if (newPallets?.length > 0) {
                newPallets[0].palletNumber = 1;
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
