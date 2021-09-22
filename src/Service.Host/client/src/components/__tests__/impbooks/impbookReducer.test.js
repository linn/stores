import importBookReducer from '../../importBooks/ImportBookReducer';

const defaultImpBook = {
    id: null,
    dateCreated: new Date().toString(),
    parcelNumber: null,
    supplierId: '',
    foreignCurrency: 'N',
    currency: 'GBP',
    carrierId: '',
    OldArrivalPort: '',
    flightNumber: '',
    transportId: null,
    transportBillNumber: '',
    transactionId: null,
    deliveryTermCode: '',
    arrivalPort: '',
    lineVatTotal: null,
    hwb: '',
    supplierCostCurrency: '',
    transNature: '',
    arrivalDate: new Date().toString(),
    freightCharges: null,
    handlingCharge: null,
    clearanceCharge: null,
    cartage: null,
    duty: null,
    vat: null,
    misc: null,
    carriersInvTotal: null,
    carriersVatTotal: null,
    totalImportValue: null,
    pieces: null,
    weight: null,
    customsEntryCode: '',
    customsEntryCodeDate: new Date().toString(),
    linnDuty: null,
    linnVat: null,
    iprCpcNumber: null,
    eecgNumber: null,
    dateCancelled: null,
    cancelledBy: null,
    cancelledReason: '',
    carrierInvNumber: '',
    carrierInvDate: new Date().toString(),
    countryOfOrigin: '',
    fcName: '',
    vaxRef: '',
    storage: null,
    numCartons: null,
    numPallets: null,
    comments: '',
    exchangeCurrency: '',
    baseCurrency: '',
    periodNumber: null,
    createdBy: null,
    portCode: '',
    customsEntryCodePrefix: '',
    importBookInvoiceDetails: [],
    importBookOrderDetails: [],
    importBookPostEntries: []
};

describe('impbook reducer tests', () => {
    it('should return initial state', () => {
        expect(importBookReducer(undefined, {})).toEqual({
            impbook: { id: '' }
        });
    });

    it('should handle initialise', () => {
        expect(
            importBookReducer(
                {},
                {
                    type: 'initialise',
                    payload: defaultImpBook
                }
            )
        ).toEqual({
            impbook: { ...defaultImpBook },
            prevImpBook: { ...defaultImpBook }
        });
    });

    it('should handle simple field changes', () => {
        expect(
            importBookReducer(
                {},
                { type: 'fieldChange', fieldName: 'createdBy', payload: '12345678' }
            )
        ).toEqual({ impbook: { createdBy: '12345678' } });
    });

    it('should handle parcel change and update all fields', () => {
        expect(
            importBookReducer(
                {},
                {
                    type: 'parcelChange',
                    parcel: {
                        parcelNumber: 123,
                        weight: 11.5,
                        cartonCount: 4,
                        palletCount: 1,
                        supplierId: '3333',
                        carrierId: '4444',
                        dateReceived: '11/12/13',
                        consignmentNo: '12122321312'
                    }
                }
            )
        ).toEqual({
            impbook: {
                parcelNumber: 123,
                weight: 11.5,
                numCartons: 4,
                numPallets: 1,
                supplierId: '3333',
                carrierId: '4444',
                arrivalDate: '11/12/13',
                transportBillNumber: '12122321312'
            }
        });
    });

    it('should handle orderDetail add', () => {
        expect(
            importBookReducer(
                { impbook: { importBookOrderDetails: [{ lineNumber: 1 }, { lineNumber: 2 }] } },
                {
                    type: 'orderDetailAdd',
                    payload: {}
                }
            )
        ).toEqual({
            impbook: {
                importBookOrderDetails: [
                    {
                        lineNumber: 1
                    },
                    {
                        lineNumber: 2
                    },
                    {
                        lineNumber: 3
                    }
                ]
            }
        });
    });

    it('should handle orderDetail field change', () => {
        expect(
            importBookReducer(
                {
                    impbook: {
                        importBookOrderDetails: [
                            { lineNumber: 1 },
                            { lineNumber: 2 },
                            { lineNumber: 3 }
                        ]
                    }
                },
                {
                    type: 'orderDetailFieldChange',
                    payload: { lineNumber: 1, potato: 'blah' },
                    lineNumber: 1
                }
            )
        ).toEqual({
            impbook: {
                importBookOrderDetails: [
                    {
                        lineNumber: 2
                    },
                    {
                        lineNumber: 3
                    },
                    {
                        lineNumber: 1,
                        potato: 'blah'
                    }
                ]
            }
        });
    });

    it('should handle orderDetail remove', () => {
        expect(
            importBookReducer(
                {
                    impbook: {
                        importBookOrderDetails: [
                            { lineNumber: 1 },
                            { lineNumber: 2 },
                            { lineNumber: 3 }
                        ]
                    }
                },
                {
                    type: 'orderDetailRemove',
                    lineNumber: 2
                }
            )
        ).toEqual({
            impbook: {
                importBookOrderDetails: [
                    {
                        lineNumber: 1
                    },
                    {
                        lineNumber: 3
                    }
                ]
            }
        });
    });

    it('should handle invoiceDetailsUpdate', () => {
        expect(
            importBookReducer(
                {
                    impbook: {
                        importBookInvoiceDetails: [
                            { lineNumber: 1 },
                            { lineNumber: 2 },
                            { lineNumber: 3 }
                        ]
                    }
                },
                {
                    type: 'invoiceDetailsUpdate',
                    details: [{ lineNumber: 5 }, { lineNumber: 6 }, { lineNumber: 7 }]
                }
            )
        ).toEqual({
            impbook: {
                importBookInvoiceDetails: [{ lineNumber: 5 }, { lineNumber: 6 }, { lineNumber: 7 }]
            }
        });
    });

    it('should handle postEntriesUpdate', () => {
        expect(
            importBookReducer(
                {
                    impbook: {
                        importBookPostEntries: [
                            { lineNumber: 1 },
                            { lineNumber: 2 },
                            { lineNumber: 3 }
                        ]
                    }
                },
                {
                    type: 'postEntriesUpdate',
                    entries: [{ lineNumber: 5 }, { lineNumber: 6 }, { lineNumber: 7 }]
                }
            )
        ).toEqual({
            impbook: {
                importBookPostEntries: [{ lineNumber: 5 }, { lineNumber: 6 }, { lineNumber: 7 }]
            }
        });
    });
});
