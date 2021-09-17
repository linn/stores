const initialState = {};

const defaultNewConsignment = {
    despatchLocationCode: 'LINN',
    pallets: [],
    items: []
};

const getItemType = itemTypeDisplay => {
    switch (itemTypeDisplay) {
        case 'Loose Item':
            return 'I';
        case 'Sealed Box':
            return 'S';
        case 'Open Carton':
            return 'C';
        default:
            return itemTypeDisplay;
    }
};

export default function consignmentReducer(state = initialState, action) {
    switch (action.type) {
        case 'create': {
            return {
                consignment: defaultNewConsignment,
                originalConsignment: null
            };
        }
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
        case 'updateItems': {
            let newItems = [];
            if (action.payload?.length > 0) {
                newItems = action.payload.map(item => {
                    return {
                        ...item,
                        consignmentId: state.consignment.consignmentId,
                        itemType: getItemType(item.itemTypeDisplay)
                    };
                });
            } else {
                newItems = [];
            }

            return {
                ...state,
                consignment: { ...state.consignment, items: newItems }
            };
        }
        default:
            return state;
    }
}
