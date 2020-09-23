import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Title,
    ErrorCard,
    Dropdown,
    SnackbarMessage,
    Typeahead,
    Loading
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function CreatePart({
    setEditStatus,
    item,
    snackbarVisible,
    addItem,
    setSnackbarVisible,
    privileges,
    itemError,
    accountingCompanies,
    history,
    loading,
    searchItems,
    fetchItems,
    suppliersSearchResults,
    suppliersSearchLoading,
    searchSuppliers,
    clearSuppliersSearch
}) {
    const [part, setPart] = useState(item);

    const [safetyCriticalMessage, setSafetyCriticalMessage] = useState();

    useEffect(() => {
        const segments = part.partNumber?.split('/1');
        if (segments?.length > 1) {
            fetchItems(segments[0]);
        } else {
            setPart(p => ({ ...p, safetyCriticalPart: 'No' }));
            setSafetyCriticalMessage(null);
        }
    }, [fetchItems, part.partNumber]);

    useEffect(() => {
        if (searchItems?.length === 1 && searchItems[0].safetyCriticalPart) {
            setPart(p => ({ ...p, safetyCriticalPart: 'Yes' }));
            setSafetyCriticalMessage('Defaulted to yes since previous version is safety critical');
        }
    }, [searchItems]);

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
            setPart({
                ...part,
                accountingCompany: newValue,
                paretoCode: 'U',
                bomType: '',
                linnProduced: 'Yes'
            });
        }
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Create Part" />
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                )}

                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
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
                                //maxLength={10}
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
                        <Grid item xs={4}>
                            <Typeahead
                                onSelect={newValue => {
                                    setPart(p => ({
                                        ...p,
                                        preferredSupplier: newValue.name,
                                        preferredSupplierName: newValue.description
                                    }));
                                }}
                                label="Preferred Supplier"
                                modal
                                items={suppliersSearchResults}
                                value={part.preferredSupplier}
                                loading={suppliersSearchLoading}
                                fetchItems={searchSuppliers}
                                links={false}
                                searc
                                clearSearch={() => clearSuppliersSearch}
                                placeholder="Search Code or Description"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={part.preferredSupplierName}
                                label="Name"
                                disabled
                                onChange={handleFieldChange}
                                propertyName="preferredSupplierName"
                            />
                        </Grid>
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
                        <Grid item xs={4}>
                            <Dropdown
                                label="Rail Method"
                                propertyName="railMethod"
                                items={['MR9', 'SMM', 'POLICY', 'FIXED RAILS', 'OVERRIDE SAFETY']}
                                fullWidth
                                allowNoValue={false}
                                value={part.railMethod}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={4} />
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
                        <Grid item xs={4}>
                            <Dropdown
                                label="Safety Critical Part"
                                propertyName="safetyCriticalPart"
                                helperText={safetyCriticalMessage}
                                items={['Yes', 'No']}
                                fullWidth
                                allowNoValue={false}
                                value={part.safetyCriticalPart}
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
    loading: PropTypes.bool,
    searchItems: PropTypes.arrayOf(
        PropTypes.shape({ partNumber: PropTypes.string, safetyCriticalPart: PropTypes.bool })
    ),
    fetchItems: PropTypes.func.isRequired,
    suppliersSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            supplierCode: PropTypes.string,
            description: PropTypes.string
        })
    ),
    suppliersSearchLoading: PropTypes.bool,
    searchSuppliers: PropTypes.func.isRequired,
    clearSuppliersSearch: PropTypes.func.isRequired
};

CreatePart.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    itemError: null,
    privileges: null,
    accountingCompanies: [],
    loading: false,
    searchItems: [],
    suppliersSearchResults: [],
    suppliersSearchLoading: false
};

export default CreatePart;
