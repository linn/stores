import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
    //DatePicker
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function Part({
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
    setSnackbarVisible
}) {
    const [part, setPart] = useState({});
    const [prevPart, setPrevPart] = useState({});

    const [tab, setTab] = useState(0);

    const handleTabChange = (event, value) => {
        setTab(value);
    };

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevPart) {
            setPart(item);
            setPrevPart(item);
        }
    }, [item, prevPart]);

    const partInvalid = () => false;

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, part);
            setEditStatus('view');
        } else if (creating()) {
            addItem(part);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setPart(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/parts');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPart({ ...part, [propertyName]: newValue });
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
                        <Loading />
                    </Grid>
                ) : (
                    part &&
                    itemError?.part !== 404 && (
                        <>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            <Tabs
                                value={tab}
                                onChange={handleTabChange}
                                indicatorColor="primary"
                                textColor="primary"
                                style={{ paddingBottom: '40px' }}
                            >
                                <Tab label="General" />
                                <Tab label="Build" />
                                <Tab label="Purch" />
                                <Tab label="Stores" />
                                <Tab label="LifeCycle" />
                            </Tabs>
                            {tab === 0 && (
                                <Grid item xs={12}>
                                    <InputField
                                        fullWidth
                                        disabled={!creating()}
                                        value={part.partNumber}
                                        label="Part Number"
                                        maxLength={10}
                                        helperText={
                                            !creating()
                                                ? 'This field cannot be changed'
                                                : `${partInvalid() ? 'This field is required' : ''}`
                                        }
                                        required
                                        onChange={handleFieldChange}
                                        propertyName="partNumber"
                                    />
                                </Grid>
                            )}
                            {/* <Grid item xs={8}>
                                <DatePicker
                                    label="Date Invalid"
                                    value={part.dateInvalid ? part.dateInvalid.toString() : null}
                                    onChange={value => {
                                        handleFieldChange('dateInvalid', value);
                                    }}
                                />
                            </Grid> */}
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing() || partInvalid()}
                                    saveClick={handleSaveClick}
                                    cancelClick={handleCancelClick}
                                    backClick={handleBackClick}
                                />
                            </Grid>
                        </>
                    )
                )}
            </Grid>
        </Page>
    );
}

Part.propTypes = {
    item: PropTypes.shape({
        part: PropTypes.string,
        description: PropTypes.string,
        nextSerialNumber: PropTypes.number,
        dateClosed: PropTypes.string
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

Part.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null
};

export default Part;
