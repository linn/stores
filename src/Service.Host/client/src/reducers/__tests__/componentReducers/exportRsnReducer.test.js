import exportRsnReducer from '../../componentReducers/exportRsnReducer';

describe('exportRsnReducer', () => {
    describe('when clearing search results', () => {
        it('should clear search results', () => {
            const state = {
                searchResults: [{ id: 1 }, { id: 2 }]
            };

            const expected = { searchResults: [] };

            const action = { type: 'clearSearchResults' };

            expect(exportRsnReducer(state, action)).toEqual(expected);
        });
    });

    // describe('when receiving sales account search results', () => {
    //     it('should add results to empty search results', () => {
    //         const state = {
    //             searchResults: []
    //         };

    //         const expected = {
    //             searchResults: [
    //                 {
    //                     id: 1,
    //                     type: 'account',
    //                     name: 'name1',
    //                     description: `<React.Fragment>
    //                   1
    //                   <ForwardRef(WithStyles)
    //                     color="primary"
    //                     label="Account"
    //                     size="small"
    //                     variant="outlined"
    //                   />
    //                 </React.Fragment>`
    //                 },
    //                 {
    //                     id: 2,
    //                     type: 'account',
    //                     name: 'name2',
    //                     description: `<React.Fragment>
    //                 2
    //                 <ForwardRef(WithStyles)
    //                   color="primary"
    //                   label="Account"
    //                   size="small"
    //                   variant="outlined"
    //                 />
    //               </React.Fragment>`
    //                 }
    //             ]
    //         };

    //         const action = {
    //             type: 'receiveSalesAccountsSearchResults',
    //             payload: [
    //                 { accountName: 'name1', accountId: 1 },
    //                 { accountName: 'name2', accountId: 2 }
    //             ]
    //         };

    //         expect(exportRsnReducer(state, action)).toEqual(expected);
    //     });
    // });
});
