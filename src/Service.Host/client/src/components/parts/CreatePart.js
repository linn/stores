import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Title,
    ErrorCard,
    Dropdown,
    SnackbarMessage,
    Loading
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function CreatePart({
    editStatus,
    setEditStatus,
    item,
    snackbarVisible,
    addItem,
    setSnackbarVisible,
    privileges,
    userNumber,
    itemError,
    accountingCompanies,
    history,
    loading
}) {
    const [part, setPart] = useState({
        partNumber: '',
        description: '',
        accountingCompany: 'LINN',
        psuPart: 'No',
        stockControlled: 'Yes',
        cccCriticalPart: 'No',
        paretoCode: 'U',
        createdBy: userNumber,
        dateCreated: new Date()
    });

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';

    const canCreate = () => {
        if (!(privileges.length < 1)) {
            return privileges.some(priv => priv === 'part.admin');
        }
        return false;
    };

    const handleSaveClick = () => {
        const partResource = part;
        // convert Yes/No to true/false for resource to send
        Object.keys(partResource).forEach(k => {
            if (partResource[k] === 'Yes') {
                partResource[k] = true;
            }
            if (partResource[k] === 'No') {
                partResource[k] = false;
            }
        });
        addItem(partResource);
        setEditStatus('view');
    };

    const handleCancelClick = () => {
        setPart(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('inventory/parts');
    };

    const handleFieldChange = (propertyName, newValue) => {
        setEditStatus('edit');
        setPart({ ...part, [propertyName]: newValue });
    };

    const handleAccountingCompanyChange = newValue => {
        if (viewing()) {
            setEditStatus('edit');
        }
        if (newValue === 'RECORDS') {
            setPart({
                ...part,
                accountingCompany: newValue,
                paretoCode: 'R',
                bomType: 'C',
                linnProduced: 'No',
                qcOnReceipt: 'No'
            });
        } else {
            setPart({ ...part, accountingCompany: newValue, paretoCode: 'U' });
        }
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? <Title text="Create Part" /> : <Title text="Part Details" />}
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                )}

                {loading ? (
                    <Grid item xs={12}>
                        <Loading />{' '}
                    </Grid>
                ) : (
                    <>
                        <SnackbarMessage
                            visible={snackbarVisible}
                            onClose={() => setSnackbarVisible(false)}
                            message="Save Successful"
                        />
                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                value={part.partNumber}
                                label="Part Number"
                                maxLength={10}
                                required
                                onChange={handleFieldChange}
                                propertyName="partNumber"
                            />
                        </Grid>
                        <Grid item xs={8}>
                            <InputField
                                fullWidth
                                value={part.description}
                                label="Description"
                                maxLength={10}
                                required
                                onChange={handleFieldChange}
                                propertyName="description"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <Dropdown
                                label="Accounting Company"
                                propertyName="accountingCompany"
                                items={accountingCompanies.map(c => ({
                                    id: c.name,
                                    displayText: c.description
                                }))}
                                fullWidth
                                allowNoValue={false}
                                value={part.accountingCompany}
                                onChange={(_, newValue) => {
                                    handleAccountingCompanyChange(newValue);
                                }}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <Dropdown
                                label="Linn Produced Assembly"
                                propertyName="linnProduced"
                                items={['Yes', 'No']}
                                fullWidth
                                allowNoValue={false}
                                value={part.linnProduced}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <Dropdown
                                label="Stores Controlled?"
                                propertyName="stockControlled"
                                items={['Yes', 'No']}
                                fullWidth
                                allowNoValue={false}
                                value={part.stockControlled}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <Dropdown
                                label="CCC Critical?"
                                propertyName="cccCriticalPart"
                                items={['Yes', 'No']}
                                allowNoValue={false}
                                fullWidth
                                value={part.cccCriticalPart}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <Dropdown
                                label="PSU Part?"
                                propertyName="psuPart"
                                items={['Yes', 'No']}
                                allowNoValue={false}
                                fullWidth
                                value={part.psuPart}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={12}>
                            <SaveBackCancelButtons
                                saveDisabled={Object.values(part).some(v => !v) || !canCreate()}
                                saveClick={handleSaveClick}
                                cancelClick={handleCancelClick}
                                backClick={handleBackClick}
                            />
                        </Grid>
                    </>
                )}
            </Grid>
        </Page>
    );
}

const accountingCompanyShape = PropTypes.shape({
    name: PropTypes.string,
    description: PropTypes.string
});

CreatePart.propTypes = {
    item: PropTypes.shape({
        part: PropTypes.string,
        description: PropTypes.string,
        nextSerialNumber: PropTypes.number,
        dateClosed: PropTypes.string
    }),
    accountingCompanies: PropTypes.arrayOf(accountingCompanyShape),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    snackbarVisible: PropTypes.bool,
    addItem: PropTypes.func,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string),
    userNumber: PropTypes.number,
    loading: PropTypes.bool
};

CreatePart.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    itemError: null,
    privileges: null,
    userNumber: null,
    accountingCompanies: [],
    loading: false
};

export default CreatePart;
