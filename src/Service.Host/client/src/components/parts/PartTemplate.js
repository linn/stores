import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    Typeahead,
    Dropdown
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';

function PartTemplate({
    editStatus,
    itemError,
    itemId,
    item,
    loading,
    snackbarVisible,
    addItem,
    updateItem,
    setEditStatus,
    setSnackbarVisible,
    privileges,
    productAnalysisCodeSearchResults,
    productAnalysisCodesSearchLoading,
    searchProductAnalysisCodes,
    clearProductAnalysisCodesSearch,
    assemblyTechnologies
}) {
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const [partTemplate, setPartTemplate] = useState(
        creating()
            ? {
                  partRoot: '',
                  description: '',
                  hasDataSheet: 'N',
                  hasNumberSequence: '',
                  nextNumber: '',
                  allowVariants: 'N',
                  variants: '',
                  accountingCompany: '',
                  productCode: '',
                  stockControlled: '',
                  linnProduced: '',
                  rmfgCode: '',
                  bomType: '',
                  assemblyTechnology: '',
                  allowPartCreation: '',
                  paretoCode: ''
              }
            : null
    );
    const [prevPartTemplate, setPrevPartTemplate] = useState(
        creating()
            ? {
                  partRoot: '',
                  description: '',
                  hasDataSheet: 'N',
                  hasNumberSequence: '',
                  nextNumber: '',
                  allowVariants: 'N',
                  variants: '',
                  accountingCompany: '',
                  productCode: '',
                  stockControlled: '',
                  linnProduced: '',
                  rmfgCode: '',
                  bomType: '',
                  assemblyTechnology: '',
                  allowPartCreation: '',
                  paretoCode: ''
              }
            : null
    );

    useEffect(() => {
        if (item && item !== prevPartTemplate) {
            setPartTemplate(item);
            setPrevPartTemplate(item);
        }
    }, [item, prevPartTemplate]);

    const useStyles = makeStyles(() => ({
        thinPage: {
            width: '60%',
            margin: '0 auto'
        }
    }));
    const classes = useStyles();

    const handleSaveClick = () => {
        if (creating()) {
            addItem(partTemplate);
        } else {
            updateItem(itemId, partTemplate);
        }
        setEditStatus('view');
    };

    const saveEnabled = () => {
        return (
            !partTemplate.partRoot ||
            !partTemplate.description ||
            !partTemplate.hasDataSheet ||
            !partTemplate.allowVariants ||
            !partTemplate.allowPartCreation ||
            !partTemplate.hasNumberSequence
        );
    };

    const handleCancelClick = () => {
        if (creating()) {
            setPartTemplate(prevPartTemplate);
        } else {
            setPartTemplate(item);
            setEditStatus('view');
        }
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing() || creating()) {
            setEditStatus('edit');
        }

        setPartTemplate(x => ({ ...x, [propertyName]: newValue }));
    };

    const handleProductCodeChange = product => {
        handleFieldChange('productCode', product.name);
        handleFieldChange('productAnalysisCodeDescription', product.description);
    };

    const allowedToEdit = () => {
        return privileges?.some(priv => priv === 'part.admin');
    };

    const content = () => (
        <Grid container spacing={3}>
            <Grid item xs={12}>
                {creating() ? (
                    <Title text="Create New Part Template" />
                ) : (
                    <Title text="Part Template Details" />
                )}
            </Grid>
            {itemError && (
                <Grid item xs={12}>
                    <ErrorCard errorMessage={itemError?.details?.message || itemError.statusText} />
                </Grid>
            )}
            {loading ? (
                <Grid item xs={12}>
                    <Loading />
                </Grid>
            ) : (
                partTemplate && (
                    <>
                        <SnackbarMessage
                            visible={snackbarVisible}
                            onClose={() => setSnackbarVisible(false)}
                            message="Save Successful"
                        />

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={partTemplate.partRoot}
                                label="Part Root"
                                maxLength={14}
                                required
                                onChange={handleFieldChange}
                                propertyName="partRoot"
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <InputField
                                fullWidth
                                value={partTemplate.description}
                                label="Description"
                                maxLength={50}
                                required
                                onChange={handleFieldChange}
                                propertyName="description"
                                rows={2}
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <Dropdown
                                label="Number Seq?"
                                propertyName="hasNumberSequence"
                                items={[
                                    { id: 'Y', displayText: 'Yes' },
                                    { id: 'N', displayText: 'No' }
                                ]}
                                fullWidth
                                required
                                value={partTemplate.hasNumberSequence}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                value={partTemplate.nextNumber}
                                label="Seq Next Number"
                                maxLength={6}
                                onChange={handleFieldChange}
                                allowNoValue
                                propertyName="nextNumber"
                            />
                        </Grid>
                        <Grid item xs={6} />

                        <Grid item xs={3}>
                            <Dropdown
                                label="Allow Variants"
                                propertyName="allowVariants"
                                items={[
                                    { id: 'N', displayText: 'No' },
                                    { id: 'R', displayText: 'Restricted' },
                                    { id: 'A', displayText: 'Any' }
                                ]}
                                required
                                fullWidth
                                value={partTemplate.allowVariants}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <InputField
                                fullWidth
                                value={partTemplate.variants}
                                label="Variants"
                                maxLength={255}
                                onChange={handleFieldChange}
                                allowNoValue
                                propertyName="variants"
                                rows={2}
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <Dropdown
                                label="Allow Part Creation"
                                propertyName="allowPartCreation"
                                items={[
                                    { id: 'Y', displayText: 'Yes' },
                                    { id: 'N', displayText: 'No' },
                                    { id: 'S', displayText: 'Sourced Only' }
                                ]}
                                fullWidth
                                required
                                value={partTemplate.allowPartCreation}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <Dropdown
                                label="Has Datasheet"
                                propertyName="hasDataSheet"
                                items={[
                                    { id: 'Y', displayText: 'Yes' },
                                    { id: 'N', displayText: 'No' }
                                ]}
                                fullWidth
                                required
                                value={partTemplate.hasDataSheet}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={12} />

                        <Grid item xs={4}>
                            <Dropdown
                                label="Accounting Company"
                                propertyName="accountingCompany"
                                items={[
                                    { id: 'LINN', displayText: 'Linn Products Ltd' },
                                    { id: 'RECORDS', displayText: 'Linn Records' }
                                ]}
                                fullWidth
                                required
                                value={partTemplate.accountingCompany}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={4}>
                            <Typeahead
                                onSelect={handleProductCodeChange}
                                label="Product Analysis Code"
                                modal
                                items={productAnalysisCodeSearchResults}
                                value={partTemplate.productCode}
                                loading={productAnalysisCodesSearchLoading}
                                fetchItems={searchProductAnalysisCodes}
                                links={false}
                                clearSearch={clearProductAnalysisCodesSearch}
                                placeholder="Search Codes"
                            />
                        </Grid>
                        <Grid item xs={8}>
                            <InputField
                                fullWidth
                                value={partTemplate.productAnalysisCodeDescription}
                                label="Description"
                                disabled
                                propertyName="ProductAnalysisCodeDescription"
                            />
                        </Grid>

                        <Grid item xs={4} />

                        <Grid item xs={3}>
                            <Dropdown
                                label="Linn Produced"
                                propertyName="linnProduced"
                                items={[
                                    { id: 'Y', displayText: 'Yes' },
                                    { id: 'N', displayText: 'No' }
                                ]}
                                fullWidth
                                value={partTemplate.linnProduced}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <Dropdown
                                label="Stock Controlled"
                                propertyName="linnProduced"
                                items={[
                                    { id: 'Y', displayText: 'Yes' },
                                    { id: 'N', displayText: 'No' }
                                ]}
                                fullWidth
                                value={partTemplate.linnProduced}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={6} />

                        <Grid item xs={3}>
                            <Dropdown
                                label="Bom Type"
                                propertyName="bomType"
                                items={[
                                    { id: 'C', displayText: 'Component' },
                                    { id: 'A', displayText: 'Assembly' },
                                    { id: 'P', displayText: 'Phantom' }
                                ]}
                                fullWidth
                                value={partTemplate.bomType}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <Dropdown
                                label="Rm fg"
                                propertyName="rmfgCode"
                                items={[
                                    { id: 'R', displayText: 'Raw Material' },
                                    { id: 'F', displayText: 'Finished Good' }
                                ]}
                                fullWidth
                                value={partTemplate.rmfgCode}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={6} />
                        <Grid item xs={6}>
                            <Dropdown
                                label="Assembly Technology"
                                propertyName="assemblyTechnology"
                                items={assemblyTechnologies.map(c => ({
                                    id: c.name,
                                    displayText: c.description
                                }))}
                                fullWidth
                                allowNoValue
                                value={partTemplate.assemblyTechnology}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <Dropdown
                                fullWidth
                                label="Pareto Code"
                                propertyName="paretoCode"
                                items={[
                                    { id: 'A', displayText: 'A - BOUGHT IN, TOP 80% OF SPEND' },
                                    { id: 'B', displayText: 'B - BOUGHT IN, NEXT 15% OF SPEND' },
                                    { id: 'C', displayText: 'C - BOUGHT IN, LEAST 5% OF SPEND' },
                                    { id: 'D', displayText: 'D - BOUGHT IN. NO USAGE' },
                                    { id: 'E', displayText: 'E - LINN PRODUCED PARTS' },
                                    { id: 'L', displayText: 'L - LOEWE TVS AND ACCESSORIES' },
                                    { id: 'R', displayText: 'R - ALL RECORDS, TAPES AND CDS' },
                                    { id: 'U', displayText: 'U - UNSOURCED COMPONENTS' },
                                    { id: 'X', displayText: 'X - PARTS OUTWITH PARETO CODES' }
                                ]}
                                value={partTemplate.paretoCode}
                                maxLength={2}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <SaveBackCancelButtons
                                saveDisabled={viewing() || saveEnabled()}
                                saveClick={handleSaveClick}
                                cancelClick={handleCancelClick}
                            />
                        </Grid>
                    </>
                )
            )}
        </Grid>
    );

    return (
        <>
            <div className={classes.thinPage}>
                <Page> {content()}</Page>
            </div>
        </>
    );
}

const productAnalysisCodeShape = PropTypes.shape({
    productCode: PropTypes.string,
    description: PropTypes.string
});

PartTemplate.propTypes = {
    item: PropTypes.shape({
        partTemplateNumber: PropTypes.number
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({ errors: PropTypes.shape({}), message: PropTypes.string }),
        item: PropTypes.string
    }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    addItem: PropTypes.func.isRequired,
    updateItem: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired,
    productAnalysisCodeSearchResults: PropTypes.arrayOf(productAnalysisCodeShape),
    searchProductAnalysisCodes: PropTypes.func.isRequired,
    clearProductAnalysisCodesSearch: PropTypes.func,
    productAnalysisCodesSearchLoading: PropTypes.bool.isRequired,
    assemblyTechnologies: PropTypes.arrayOf(PropTypes.shape({}))
};

PartTemplate.defaultProps = {
    item: {
        partRoot: null,
        description: null,
        hasDataSheet: null,
        hasNumberSequence: null,
        nextNumber: null,
        allowVariants: null,
        variants: null,
        accountingCompany: null,
        productCode: null,
        stockControlled: null,
        linnProduced: null,
        rmfgCode: null,
        bomType: null,
        assemblyTechnology: null,
        allowPartCreation: null,
        paretoCode: null,
        links: {}
    },
    snackbarVisible: false,
    loading: false,
    itemError: null,
    itemId: null,
    productAnalysisCodeSearchResults: [],
    assemblyTechnologies: [],
    clearProductAnalysisCodesSearch: () => {}
};

export default PartTemplate;
