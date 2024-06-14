import React, { useState } from 'react';
import queryString from 'query-string';
import PropTypes from 'prop-types';
import { Dropdown, InputField, DatePicker, utilities } from '@linn-it/linn-form-components-library';
import Button from '@material-ui/core/Button';
import { Link } from 'react-router-dom';
import Grid from '@material-ui/core/Grid';
import { DataGrid } from '@mui/x-data-grid';
import { CSVLink } from 'react-csv';

import Page from '../../containers/Page';

function PartSources({ items, loading, search, projectDepartments, employees }) {
    const [options, setOptions] = useState({ fromDate: null, toDate: null });
    const handleFieldChange = (property, value) => {
        setOptions(o => ({ ...o, [property]: value }));
    };

    const columns = [
        {
            headerName: 'Part No',
            field: 'partNumber',
            width: 200,
            renderCell: params => (
                <Link to={utilities.getSelfHref(params.row)}> {params.row.partNumber}</Link>
            )
        },
        {
            headerName: 'Desc',
            field: 'description',
            width: 200
        }
    ];

    const csvData = [
        columns.map(c => c.headerName),
        ...items.map(i => columns.map(col => i[col.field]))
    ];
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={6}>
                    <InputField
                        propertyName="partNumber"
                        label="Part Number Search Term (leave blank for all)"
                        fullWidth
                        onChange={handleFieldChange}
                        value={options.partNumber}
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        propertyName="description"
                        label="Descripton Search Term (leave blank for all)"
                        fullWidth
                        onChange={handleFieldChange}
                        value={options.description}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Dropdown
                        propertyName="projectDeptCode"
                        label="Project Dept (Leave blank for all)"
                        fullWidth
                        allowNoValue
                        items={projectDepartments?.map(d => ({
                            id: d.departmentCode,
                            displayText: `${d.description} (${d.departmentCode})`
                        }))}
                        onChange={handleFieldChange}
                        value={options.projectDeptCode}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Dropdown
                        propertyName="createdBy"
                        label="Entered By (Leave blank for all)"
                        fullWidth
                        allowNoValue
                        items={employees?.map(d => ({
                            id: d.id,
                            displayText: d.fullName
                        }))}
                        onChange={handleFieldChange}
                        value={options.createdBy}
                    />
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        label="From Date (leave blank for all)"
                        value={options.fromDate}
                        onChange={n => handleFieldChange('fromDate', n)}
                    />
                </Grid>
                <Grid item xs={3}>
                    <DatePicker
                        label="To Date (leave blank for all)"
                        value={options.toDate}
                        onChange={n => handleFieldChange('toDate', n)}
                    />
                </Grid>
                <Grid item xs={6} />
                <Grid item xs={2}>
                    <Button
                        variant="contained"
                        onClick={() => {
                            const body = {};
                            if (options.partNumber) {
                                body.partNumber = options.partNumber;
                            }
                            if (options.description) {
                                body.description = options.description;
                            }
                            if (options.fromDate) {
                                body.fromDate = options.fromDate.toISOString();
                            }
                            if (options.toDate) {
                                body.toDate = options.toDate.toISOString();
                            }
                            if (options.projectDeptCode) {
                                body.projectDeptCode = options.projectDeptCode;
                            }
                            search(null, `&${queryString.stringify(body)}`);
                        }}
                    >
                        RUN
                    </Button>
                </Grid>
                <>
                    {!!items?.length && (
                        <Grid item xs={2}>
                            <CSVLink data={csvData}>
                                <Button variant="contained">Export</Button>
                            </CSVLink>
                        </Grid>
                    )}
                </>
                <Grid items xs={12}>
                    <DataGrid
                        rows={items ?? []}
                        columns={columns}
                        loading={loading}
                        autoHeight
                        checkboxSelection={false}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

PartSources.propTypes = {
    items: PropTypes.arrayOf(PropTypes.shape({})),
    loading: PropTypes.bool,
    search: PropTypes.func.isRequired,
    projectDepartments: PropTypes.arrayOf(PropTypes.shape({})),
    employees: PropTypes.arrayOf(PropTypes.shape({}))
};

PartSources.defaultProps = {
    items: [],
    loading: false,
    projectDepartments: [],
    employees: []
};
export default PartSources;
