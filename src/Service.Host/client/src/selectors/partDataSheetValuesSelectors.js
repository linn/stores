export const getResistorConstructionValues = state => {
    const items = state.partDataSheetValues?.items;
    if (!items) {
        return [];
    }

    return items.filter(i => i.attributeSet === 'RES' && i.field === 'CONSTRUCTION');
};

export const getLoading = state => {
    const storeItems = state.partDataSheetValues;
    if (!storeItems) {
        return null;
    }

    return storeItems.loading;
};
