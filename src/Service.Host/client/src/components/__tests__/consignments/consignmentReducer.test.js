import consignmentReducer from '../../consignments/consignmentReducer';

describe('consignmentReducer tests', () => {
    it('should initialise on load', () => {
        expect(
            consignmentReducer(
                {},
                {
                    type: 'initialise',
                    payload: {
                        consignmentNumber: 123,
                        status: 'L'
                    }
                }
            )
        ).toEqual({
            consignment: {
                consignmentNumber: 123,
                status: 'L'
            },
            originalConsignment: {
                consignmentNumber: 123,
                status: 'L'
            }
        });
    });

    it('should create empty consignment', () => {
        expect(
            consignmentReducer(
                {},
                {
                    type: 'create',
                    payload: null
                }
            )
        ).toEqual({
            consignment: {
                despatchLocationCode: 'LINN',
                pallets: [],
                items: [],
                shippingMethod: 'S'
            },
            originalConsignment: null
        });
    });

    it('should reset', () => {
        const state = {
            consignment: { consignmentId: 123, hubId: 4 },
            originalConsignment: { consignmentId: 123, hubId: 5 }
        };
        const expectedState = {
            consignment: { consignmentId: 123, hubId: 5 },
            originalConsignment: { consignmentId: 123, hubId: 5 }
        };

        expect(
            consignmentReducer(state, {
                type: 'reset',
                payload: null
            })
        ).toEqual(expectedState);
    });

    it('should update field', () => {
        const state = { consignment: { consignmentId: 123, hubId: 4 } };
        const expectedState = { consignment: { consignmentId: 123, hubId: 5 } };
        expect(
            consignmentReducer(state, { type: 'updateField', fieldName: 'hubId', payload: 5 })
        ).toEqual(expectedState);
    });
});
