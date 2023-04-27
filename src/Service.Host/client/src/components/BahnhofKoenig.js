import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    Dropdown,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import Page from '../containers/Page';

function BahnhofKoenig({
    editStatus,
    itemError,
    history,
    loading,
    snackbarVisible,
    addItem,
    setEditStatus,
    setSnackbarVisible,
    palletLoc,
    getPalletLoc,
    queryPalletLoc,
    clearPalletLoc
}) {
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const [task, setTask] = useState(
        creating()
            ? {
                  priority: 50,
                  taskType: 'MV'
              }
            : null
    );

    const handleBackClick = () => {
        history.push('/logistics/parcels');
    };

    const useStyles = makeStyles(() => ({
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

    const saveEnabled = () => {
        if (!creating) {
            return false;
        }
        if (task.taskType === 'MV') {
            return !task.palletNumber || !task.destination || !task.priority;
        }
        if (task.taskType === 'EM') {
            return !palletLoc || !task.originalLocation || !task.priority;
        }
        return !task.palletNumber || !task.destination || !task.priority || !task.originalLocation;
    };

    const handleCancelClick = () => {
        clearPalletLoc();
        setTask({
            priority: 50,
            taskType: 'MV'
        });
        setEditStatus('create');
    };

    const handleSaveClick = () => {
        if (creating()) {
            if (task.taskType === 'EM') {
                // need to add in pallet number for empty
                addItem({ ...task, palletNumber: palletLoc.palletId });
                setEditStatus('view');
                return;
            }
            addItem(task);
        }
        setEditStatus('view');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (propertyName === 'palletNumber' && newValue && task.taskType !== 'EM') {
            getPalletLoc(newValue);
        } else if (
            propertyName === 'originalLocation' &&
            newValue &&
            newValue.length > 1 &&
            task.taskType === 'EM'
        ) {
            queryPalletLoc('locRef', newValue);
        }
        setTask({ ...task, [propertyName]: newValue });
    };

    return (
        <div className={classes.thinPage}>
            <Page>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        {creating() ? (
                            <Title text="Enter job for Warehouse System" />
                        ) : (
                            <Title text="Warehouse System Task" />
                        )}
                    </Grid>
                    {itemError && (
                        <Grid item xs={12}>
                            <ErrorCard
                                errorMessage={itemError?.details?.message || itemError.statusText}
                            />
                        </Grid>
                    )}
                    {loading ? (
                        <Grid item xs={12}>
                            <Loading />
                        </Grid>
                    ) : (
                        task && (
                            <>
                                <SnackbarMessage
                                    visible={snackbarVisible}
                                    onClose={() => setSnackbarVisible(false)}
                                    message="Save Successful"
                                />
                                <Grid item xs={5}>
                                    <Dropdown
                                        items={[
                                            { id: 'MV', displayText: 'Move' },
                                            { id: 'EM', displayText: 'Empty' },
                                            { id: 'AT', displayText: 'AtMove' }
                                        ]}
                                        propertyName="taskType"
                                        fullWidth
                                        value={task.taskType}
                                        label="Task Type"
                                        onChange={handleFieldChange}
                                        required
                                    />
                                </Grid>
                                <Grid item xs={7} />
                                <Grid item xs={5}>
                                    <InputField
                                        fullWidth
                                        value={
                                            task.taskType === 'EM'
                                                ? palletLoc?.palletId
                                                : task.palletNumber
                                        }
                                        label="Pallet Number"
                                        onChange={handleFieldChange}
                                        maxLength={10}
                                        required
                                        propertyName="palletNumber"
                                        disabled={task.taskType === 'EM'}
                                    />
                                </Grid>
                                <Grid item xs={5}>
                                    <InputField
                                        fullWidth
                                        label="Current Loc"
                                        value={palletLoc?.location}
                                        maxLength={10}
                                        required
                                        propertyName="currentLocation"
                                        disabled
                                    />
                                </Grid>
                                <Grid item xs={2} />
                                <Grid item xs={5}>
                                    <InputField
                                        fullWidth
                                        value={
                                            task.taskType === 'MV'
                                                ? palletLoc?.location
                                                : task.originalLocation
                                        }
                                        label="From Location"
                                        onChange={handleFieldChange}
                                        maxLength={10}
                                        required
                                        propertyName="originalLocation"
                                        disabled={task.taskType === 'MV'}
                                    />
                                </Grid>
                                <Grid item xs={7} />
                                <Grid item xs={5}>
                                    <InputField
                                        fullWidth
                                        value={task.destination}
                                        label="Destination"
                                        onChange={handleFieldChange}
                                        maxLength={10}
                                        required
                                        propertyName="destination"
                                        disabled={task.taskType === 'EM'}
                                    />
                                </Grid>
                                <Grid item xs={7} />
                                <Grid item xs={5}>
                                    <InputField
                                        fullWidth
                                        value={task.priority}
                                        label="Priority"
                                        onChange={handleFieldChange}
                                        maxLength={4}
                                        required
                                        propertyName="priority"
                                    />
                                </Grid>
                                <Grid item xs={7} />

                                <Grid item xs={12}>
                                    <SaveBackCancelButtons
                                        saveDisabled={viewing() || saveEnabled()}
                                        saveClick={handleSaveClick}
                                        cancelClick={handleCancelClick}
                                        backClick={() => handleBackClick()}
                                    />
                                </Grid>
                            </>
                        )
                    )}
                </Grid>
            </Page>
        </div>
    );
}

BahnhofKoenig.propTypes = {
    item: PropTypes.shape({
        taskNo: PropTypes.number,
        palletNumber: PropTypes.number,
        currentLocation: PropTypes.string
    }),
    palletLoc: PropTypes.shape({
        location: PropTypes.string,
        palletId: PropTypes.number
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({ errors: PropTypes.shape({}), message: PropTypes.string }),
        item: PropTypes.string
    }),
    snackbarVisible: PropTypes.bool,
    addItem: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    getPalletLoc: PropTypes.func.isRequired,
    queryPalletLoc: PropTypes.func.isRequired,
    clearPalletLoc: PropTypes.func.isRequired
};

BahnhofKoenig.defaultProps = {
    item: {
        palletNumber: -1,
        taskNo: null,
        currentLocation: null
    },
    palletLoc: null,
    snackbarVisible: false,
    loading: false,
    itemError: null
};

export default BahnhofKoenig;
