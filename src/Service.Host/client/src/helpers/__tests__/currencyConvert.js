import currencyConvert from '../currencyConvert';

describe('when converting value with exchange rate', () => {
    test('should convert correctly', () => {
        const value = 1268.88;
        const exchangeRate = 2;

        const converted = currencyConvert(value, exchangeRate);
        expect(converted).toEqual('634.44');
    });

    test('should round half up correctly - 100.005 should become 100.01', () => {
        const value = 200.01;
        const exchangeRate = 2;

        const converted = currencyConvert(value, exchangeRate);
        expect(converted).toEqual('100.01');
    });

    test('should convert correctly with real 2004 euro exchange rate', () => {
        const value = 5555;
        const exchangeRate = 1.4508;

        const converted = currencyConvert(value, exchangeRate);
        expect(converted).toEqual('3828.92');
    });

    test('should convert correctly with real impbook example', () => {
        const value = 7044.39;
        const exchangeRate = 1.16919918273452;

        const converted = currencyConvert(value, exchangeRate);
        expect(converted).toEqual('6024.97');
    });
});
