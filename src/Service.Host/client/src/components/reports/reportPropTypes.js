import PropTypes from 'prop-types';

const headersType = PropTypes.shape({
    rowHeader: PropTypes.string.isRequired,
    columnHeaders: PropTypes.arrayOf(PropTypes.string).isRequired,
    varianceColumns: PropTypes.arrayOf(PropTypes.number).isRequired
});

export const resultDetailsType = PropTypes.shape({
    rowTitle: PropTypes.shape({}).isRequired,
    rowSortOrder: PropTypes.number,
    values: PropTypes.arrayOf(PropTypes.object).isRequired
});

export const reportResultType = PropTypes.shape({
    title: PropTypes.shape({}),
    resultType: PropTypes.string,
    reportValueType: PropTypes.string,
    headers: headersType,
    results: PropTypes.arrayOf(resultDetailsType).isRequired,
    totals: resultDetailsType,
    message: PropTypes.string
});
