import React from 'react';
import Chip from '@material-ui/core/Chip';

export default function reducer(state, action) {
    switch (action.type) {
        case 'clearSearchResults':
            return { ...state, searchResults: [] };

        case 'receiveSalesAccountsSearchResults':
            return {
                ...state,
                searchResults: [
                    ...state.searchResults,
                    ...action.payload.map(account => ({
                        ...account,
                        id: account.accountId,
                        type: 'account',
                        name: account.accountName,
                        description: (
                            <>
                                {account.accountId}{' '}
                                <Chip
                                    size="small"
                                    label="Account"
                                    color="primary"
                                    variant="outlined"
                                />
                            </>
                        )
                    }))
                ]
            };

        case 'receiveSalesOutletsSearchResults':
            return {
                ...state,
                searchResults: [
                    ...state.searchResults,
                    ...action.payload.map(outlet => ({
                        ...outlet,
                        type: 'outlet',
                        id: `${outlet.accountId} / ${outlet.outletNumber}`,
                        description: (
                            <>
                                {`${outlet.accountId} / ${outlet.outletNumber}`}{' '}
                                <Chip
                                    size="small"
                                    label="Outlet"
                                    color="primary"
                                    variant="outlined"
                                />
                            </>
                        )
                    }))
                ]
            };

        case 'receiveRsnsSearchResults':
            return { ...state, rsns: action.payload.map(rsn => ({ ...rsn, selected: false })) };

        case 'selectAccount':
            return {
                ...state,
                selectedAccount: action.payload
            };

        case 'selectRsn':
            return {
                ...state,
                rsns: state.rsns.map(rsn =>
                    rsn.rsnNumber === action.payload.rsnNumber
                        ? { ...rsn, selected: !rsn.selected }
                        : rsn
                )
            };

        case 'setBelgiumShipping':
            return {
                ...state,
                belgiumShipping: !state.belgiumShipping
            };

        default:
            return state;
    }
}
