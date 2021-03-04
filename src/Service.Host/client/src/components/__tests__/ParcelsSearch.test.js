import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../test-utils';
import ParcelsSearch from '../parcels/ParcelsSearch';

afterEach(cleanup);

const fetchItems = jest.fn();
const searchSuppliers = jest.fn();
const searchCarriers = jest.fn();
const clearSuppliersSearch = jest.fn();
const clearCarriersSearch = jest.fn();

const defaultProps = {
    items: [],
    applicationState: {},
    fetchItems,
    searchSuppliers,
    searchCarriers,
    clearCarriersSearch,
    clearSuppliersSearch,
    history: {
        push: jest.fn()
    }
};

test('Search page renders all search fields without crashing...', () => {
    const { getByText } = render(<ParcelsSearch {...defaultProps} />);
    expect(getByText('Parcels')).toBeInTheDocument();
    expect(getByText('Supplier')).toBeInTheDocument();
    expect(getByText('Carrier')).toBeInTheDocument();
    expect(getByText('Date Created')).toBeInTheDocument();
    expect(getByText('Supplier Inv No')).toBeInTheDocument();
    expect(getByText('Consignment Number')).toBeInTheDocument();
    expect(getByText('Comments')).toBeInTheDocument();
});

//this might change to check disabled if permissions change
test('create button is available', () => {
    const { getByText } = render(<ParcelsSearch {...defaultProps} />);
    expect(getByText('Create')).toBeInTheDocument();
});
