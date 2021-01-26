export const getResistorConstructionValues = state => {
    const items = state.partDataSheetValues?.items;
    if (!items) {
        return [];
    }

    return items.filter(i => i.attributeSet === 'RES' && i.field === 'CONSTRUCTION');
};

export const getResistorPackageValues = state => {
    const items = state.partDataSheetValues?.items;
    if (!items) {
        return [];
    }

    return items.filter(i => i.attributeSet === 'RES' && i.field === 'PACKAGE');
};

export const getCapacitorPackageValues = state => {
    const items = state.partDataSheetValues?.items;
    if (!items) {
        return [];
    }

    return items.filter(i => i.attributeSet === 'CAP' && i.field === 'PACKAGE');
};

export const getTransistorPackageValues = state => {
    const items = state.partDataSheetValues?.items;
    if (!items) {
        return [];
    }

    return items.filter(i => i.attributeSet === 'TRAN' && i.field === 'PACKAGE');
};

export const getIcPackageValues = state => {
    const items = state.partDataSheetValues?.items;
    if (!items) {
        return [];
    }

    return items.filter(i => i.attributeSet === 'IC' && i.field === 'PACKAGE');
};

export const getTransistorPolarityValues = state => {
    const items = state.partDataSheetValues?.items;
    if (!items) {
        return [];
    }

    return items.filter(i => i.attributeSet === 'TRAN' && i.field === 'POLARITY');
};

export const getCapacitorDielectricValues = state => {
    const items = state.partDataSheetValues?.items;
    if (!items) {
        return [];
    }

    return items.filter(i => i.attributeSet === 'CAP' && i.field === 'DIELECTRIC');
};

export const getLoading = state => {
    const storeItems = state.partDataSheetValues;
    if (!storeItems) {
        return null;
    }

    return storeItems.loading;
};
