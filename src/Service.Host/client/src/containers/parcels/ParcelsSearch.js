// import { connect } from 'react-redux';
// import { TypeaheadDialog } from '@linn-it/linn-form-components-library';
// import parcelsActions from '../../actions/parcelsActions';
// import parcelsSelectors from '../../selectors/parcelsSelectors';

// const mapStateToProps = (state, { onSelect, title }) => ({
//     title,
//     onSelect,
//     searchItems: parcelsSelectors.getSearchItems(state).map(w => ({
//         ...w,
//         id: w.parcelNumber,
//         name: w.parcelNumber,
//         description: `${w.supplierName} - ${w.dateCreated}`
//     })),
//     loading: parcelsSelectors.getSearchLoading(state)
// });

// const mapDispatchToProps = {
//     fetchItems: parcelsActions.search,
//     clearSearch: parcelsActions.clearSearch
// };

// export default connect(mapStateToProps, mapDispatchToProps)(TypeaheadDialog);
