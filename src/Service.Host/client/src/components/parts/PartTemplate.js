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
    Dropdown
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';

function PartTemplate({
    editStatus,
    itemError,
    history,
    itemId,
    item,
    loading,
    snackbarVisible,
    addItem,
    updateItem,
    setEditStatus,
    setSnackbarVisible,
    inDialogBox,
    privileges,
    closeDialog
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
        marginTop1: {
            marginTop: '10px',
            display: 'inline-block',
            width: '2em'
        },
        marginTopWiderLinkButton: {
            marginTop: '-14px',
            display: 'inline-block'
        },
        displayInline: {
            display: 'inline'
        },
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
            !partTemplate.allowPartCreation
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
        if (viewing()) {
            setEditStatus('edit');
        }

        setPartTemplate({ ...partTemplate, [propertyName]: newValue });
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
                                maxLength={20}
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
                                maxLength={2000}
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
                                items={['Y', 'N']}
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
                                maxLength={2000}
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
                                items={['N', 'R', 'A']}
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
                                maxLength={2000}
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
                                items={['Y', 'N', 'S']}
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
                                items={['Y', 'N']}
                                fullWidth
                                required
                                value={partTemplate.hasDataSheet}
                                onChange={handleFieldChange}
                            />
                        </Grid>
                        <Grid item xs={12} />

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={partTemplate.accountingCompany}
                                label="Accounting Company"
                                maxLength={10}
                                onChange={handleFieldChange}
                                propertyName="accountingCompany"
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                value={partTemplate.productCode}
                                label="Product Code"
                                maxLength={10}
                                onChange={handleFieldChange}
                                propertyName="productCode"
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={4} />

                        <Grid item xs={3}>
                            <Dropdown
                                label="Linn Produced"
                                propertyName="linnProduced"
                                items={['Y', 'N']}
                                fullWidth
                                value={partTemplate.linnProduced}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <Dropdown
                                label="Stock Controlled"
                                propertyName="linnProduced"
                                items={['Y', 'N']}
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
                                items={['C', 'A', 'P']}
                                fullWidth
                                value={partTemplate.bomType}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <Dropdown
                                label="Rm fg"
                                propertyName="rmfgCode"
                                items={['R', 'F']}
                                fullWidth
                                value={partTemplate.rmfgCode}
                                onChange={handleFieldChange}
                            />
                        </Grid>

                        <Grid item xs={6} />

                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                value={partTemplate.paretoCode}
                                label="Pareto Code"
                                maxLength={2}
                                onChange={handleFieldChange}
                                propertyName="paretoCode"
                                disabled={!allowedToEdit()}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <SaveBackCancelButtons
                                saveDisabled={viewing() || saveEnabled()}
                                saveClick={handleSaveClick}
                                cancelClick={handleCancelClick}
                                backClick={() =>
                                    inDialogBox
                                        ? closeDialog()
                                        : history.push('/inventory/part-templates')
                                }
                            />
                        </Grid>
                    </>
                )
            )}
        </Grid>
    );

    return (
        <>
            {inDialogBox ? (
                content()
            ) : (
                <div className={classes.thinPage}>
                    <Page> {content()}</Page>
                </div>
            )}
        </>
    );
}

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
    inDialogBox: PropTypes.bool,
    privileges: PropTypes.arrayOf(PropTypes.string).isRequired,
    closeDialog: PropTypes.func
};

PartTemplate.defaultProps = {
    item: {
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
        paretoCode: '',
        links: {}
    },
    snackbarVisible: false,
    loading: false,
    itemError: null,
    itemId: null,
    inDialogBox: false,
    closeDialog: null
};

export default PartTemplate;
