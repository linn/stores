export default function reducer(state, action) {
    switch (action.type) {
        // TODO refactor to SetExportReturn and updateExportReturn maybe?
        case 'setExportReturn':
            if (action.payload?.propertyName) {
                return {
                    ...state,
                    exportReturn: {
                        ...state.exportReturn,
                        [action.payload.propertyName]: action.payload.newValue
                    },
                    editing: true
                };
            }
            return {
                ...state,
                exportReturn: {
                    ...action.payload,
                    exportReturnDetails: action.payload?.exportReturnDetails
                        ? action.payload.exportReturnDetails.map(detail => ({
                              ...detail,
                              id: detail.rsnNumber
                          }))
                        : null
                }
            };
        case 'setEditing':
            return {
                ...state,
                editing: action.payload
            };
        case 'setTab':
            return {
                ...state,
                tab: action.payload
            };
        case 'setDimensions':
            return {
                ...state,
                exportReturn: {
                    ...state.exportReturn,
                    numCartons: action.payload.numCartons,
                    grossWeightKg: action.payload.grossWeight,
                    grossDimsM3: action.payload.grossDims
                },
                editing: true
            };
        default:
            return state;
    }
}
