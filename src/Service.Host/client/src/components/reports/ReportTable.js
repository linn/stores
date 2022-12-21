import React from 'react';
import makeStyles from '@material-ui/styles/makeStyles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';
import { Title, ErrorCard } from '@linn-it/linn-form-components-library';
import {
    formatTitle,
    setDrilldown,
    setValueDrilldown,
    setTextValueDrilldown
} from './ReportDisplayUtilities';
import { reportResultType } from './reportPropTypes';

const useStyles = makeStyles(() => ({
    subTotal: {
        fontWeight: 'bolder'
    },
    root: {
        width: '100%',
        overflow: 'auto',
        textAlign: 'center',
        pageBreakInside: 'avoid'
    },
    rootAllowsPageBreaks: {
        width: '100%',
        overflow: 'auto',
        textAlign: 'center'
    },
    numberField: {
        textAlign: 'right'
    },
    noWrap: {
        whiteSpace: 'nowrap'
    },
    smallCol: {
        width: '100px',
        overflow: 'auto'
    },
    mediumCol: {
        width: '200px',
        overflow: 'auto'
    },
    largeCol: {
        width: '300px',
        overflow: 'auto'
    },
    textHighlight: {
        fontWeight: 'bolder'
    }
}));

const setCellClasses = (
    classes,
    displayValue,
    textDisplayValue,
    rowType,
    varianceColumn,
    textColumn,
    totalColumn,
    allowWrap,
    defaultClasses,
    attributes
) => {
    let generatedClasses = '';
    if (rowType === 'Subtotal' || totalColumn) {
        generatedClasses += `${classes.subTotal} `;
    }

    if (!textColumn && !textDisplayValue) {
        generatedClasses += `${classes.numberField} `;
    }

    if (!allowWrap) {
        generatedClasses += `${classes.noWrap} `;
    }

    if (defaultClasses) {
        generatedClasses += `${defaultClasses} `;
    }

    if (attributes?.length) {
        if (attributes.some(a => a.attributeType === 'text-colour')) {
            generatedClasses += `${classes.textHighlight} `;
        }
    }
    return generatedClasses;
};

const getTextColour = attributes => {
    if (attributes?.length) {
        const textAttribute = attributes.find(a => a.attributeType === 'text-colour');
        return textAttribute?.attributeValue;
    }

    return null;
};

const setHeaderCellClasses = (
    classes,
    varianceColumn,
    textColumn,
    totalColumn,
    columnClass,
    defaultClasses
) => {
    let generatedClasses = '';
    if (!textColumn) {
        generatedClasses += `${classes.numberField} `;
    }

    if (columnClass === 'small') {
        generatedClasses += `${classes.smallCol} `;
    }

    if (columnClass === 'medium') {
        generatedClasses += `${classes.mediumCol} `;
    }

    if (columnClass === 'large') {
        generatedClasses += `${classes.largeCol} `;
    }

    if (defaultClasses) {
        generatedClasses += `${defaultClasses} `;
    }

    return generatedClasses;
};

const Results = ({
    reportData,
    classes,
    title,
    showTitle,
    showTotals,
    hasExternalLinks,
    showRowTitles,
    columnClasses,
    allowPageBreakInside,
    showRowCount
}) => (
    <Paper className={allowPageBreakInside ? classes.rootAllowsPageBreaks : classes.root}>
        <Title
            text={formatTitle(
                title,
                showTitle,
                !reportData,
                reportData && reportData.error,
                reportData ? reportData.reportHelpText : ''
            )}
        />
        <div style={{ backgroundColor: 'white' }}>
            <Table size="small">
                <TableHead key="headers">
                    <TableRow>
                        {showRowTitles ? (
                            <TableCell> {reportData.headers.rowHeader} </TableCell>
                        ) : null}
                        {reportData.headers.columnHeaders.map((header, i) => (
                            <TableCell
                                className={setHeaderCellClasses(
                                    classes,
                                    reportData.headers.varianceColumns.includes(i),
                                    reportData.headers.textColumns.includes(i),
                                    reportData.headers.totalColumns.includes(i),
                                    columnClasses ? columnClasses[i] : null
                                )}
                                key={header}
                            >
                                {header}
                            </TableCell>
                        ))}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {reportData.results.map((item, j) => (
                        // eslint-disable-next-line react/no-array-index-key
                        <TableRow key={j}>
                            {showRowTitles ? (
                                <TableCell className={classes.noWrap}>
                                    {setDrilldown(item.rowTitle, hasExternalLinks)}
                                </TableCell>
                            ) : null}
                            {item.values.map((value, i) => (
                                <TableCell
                                    style={
                                        getTextColour(value?.attributes)
                                            ? { color: getTextColour(value?.attributes) }
                                            : {}
                                    }
                                    className={setCellClasses(
                                        classes,
                                        value?.displayValue,
                                        value?.textDisplayValue,
                                        item.rowType,
                                        reportData.headers.varianceColumns.includes(i),
                                        reportData.headers.textColumns.includes(i),
                                        reportData.headers.totalColumns.includes(i),
                                        value?.allowWrap ?? true,
                                        null,
                                        value?.attributes
                                    )}
                                    // remove this if we implement reordering of columns
                                    // eslint-disable-next-line react/no-array-index-key
                                    key={i}
                                >
                                    {setValueDrilldown(value, hasExternalLinks)}
                                    {setTextValueDrilldown(value, hasExternalLinks)}
                                </TableCell>
                            ))}
                        </TableRow>
                    ))}

                    {showTotals ? (
                        <TableRow key="totals">
                            {showRowTitles ? (
                                <TableCell>{reportData.totals.rowTitle.displayString}</TableCell>
                            ) : null}
                            {reportData.totals.values.map((value, i) => (
                                <TableCell
                                    className={setCellClasses(
                                        classes,
                                        value?.displayValue,
                                        value?.textDisplayValue,
                                        'Total',
                                        reportData.headers.varianceColumns.includes(i),
                                        reportData.headers.textColumns.includes(i),
                                        reportData.headers.totalColumns.includes(i),
                                        value?.allowWrap ?? true
                                    )}
                                    // remove this if we implement reordering of columns
                                    // eslint-disable-next-line react/no-array-index-key
                                    key={i}
                                >
                                    {setValueDrilldown(value, hasExternalLinks)}
                                </TableCell>
                            ))}
                        </TableRow>
                    ) : (
                        false
                    )}
                </TableBody>
            </Table>
            {showRowCount && reportData && reportData.results && (
                <Typography
                    variant="body2"
                    style={{
                        float: 'left',
                        paddingLeft: '15px',
                        paddingTop: '10px',
                        paddingBottom: '10px'
                    }}
                >
                    Number of rows: {reportData.results.length}
                </Typography>
            )}
        </div>
    </Paper>
);

function ReportTable({
    placeholderRows,
    placeholderColumns,
    reportData,
    hasExternalLinks,
    title,
    showTitle,
    showTotals,
    showRowTitles,
    columnClasses,
    allowPageBreakInside,
    showRowCount
}) {
    const classes = useStyles();
    if (!reportData) {
        return (
            <Paper className={allowPageBreakInside ? classes.rootAllowsPageBreaks : classes.root}>
                <Table>
                    <TableBody>
                        {[...Array(placeholderRows).keys()].map(row => (
                            <TableRow key={row}>
                                {[...Array(placeholderColumns).keys()].map(column => (
                                    <TableCell key={column} />
                                ))}
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </Paper>
        );
    }
    if (reportData.message) {
        return <ErrorCard errorMessage={reportData.message} />;
    }
    return Results({
        reportData,
        hasExternalLinks,
        classes,
        title,
        showRowTitles,
        showTitle,
        showTotals,
        columnClasses,
        allowPageBreakInside,
        showRowCount
    });
}

Results.propTypes = {
    hasExternalLinks: PropTypes.bool,
    reportData: reportResultType,
    classes: PropTypes.shape({
        root: PropTypes.shape({}),
        noWrap: PropTypes.shape({}),
        rootAllowsPageBreaks: PropTypes.shape({})
    }).isRequired,
    title: PropTypes.oneOfType([PropTypes.string, PropTypes.shape({})]),
    showTitle: PropTypes.bool,
    showTotals: PropTypes.bool,
    showRowTitles: PropTypes.bool,
    columnClasses: PropTypes.arrayOf(PropTypes.string),
    allowPageBreakInside: PropTypes.bool.isRequired,
    showRowCount: PropTypes.bool
};

Results.defaultProps = {
    reportData: null,
    title: null,
    showTitle: true,
    showTotals: true,
    showRowTitles: true,
    hasExternalLinks: false,
    columnClasses: null,
    showRowCount: false
};

ReportTable.propTypes = {
    hasExternalLinks: PropTypes.bool,
    placeholderRows: PropTypes.number,
    placeholderColumns: PropTypes.number,
    reportData: PropTypes.shape({ message: PropTypes.string }),
    columnClasses: PropTypes.arrayOf(PropTypes.string),
    allowPageBreakInside: PropTypes.bool,
    title: PropTypes.shape({}),
    showTitle: PropTypes.bool,
    showTotals: PropTypes.bool,
    showRowTitles: PropTypes.bool,
    showRowCount: PropTypes.bool
};

ReportTable.defaultProps = {
    reportData: null,
    placeholderRows: 5,
    placeholderColumns: 6,
    hasExternalLinks: false,
    columnClasses: null,
    allowPageBreakInside: false,
    title: '',
    showTitle: true,
    showTotals: false,
    showRowTitles: false,
    showRowCount: false
};

export default ReportTable;
