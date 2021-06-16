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
        default:
            return state;
    }
}
