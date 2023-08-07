import React from 'react';
import PropTypes from 'prop-types';
import Dialog from '@material-ui/core/Dialog';
import { InputField } from '@linn-it/linn-form-components-library';
import makeStyles from '@material-ui/styles/makeStyles';
import { Typography } from '@material-ui/core';
import Button from '@material-ui/core/Button';

const useStyles = makeStyles(theme => ({
    pullRight: {
        float: 'right'
    },
    dialog: {
        margin: theme.spacing(6),
        minWidth: theme.spacing(62)
    }
}));

export default function LiveDialog(props) {
    const { onClose, dateLive, open, liveTest, handleChangeLiveness, setStandardPrices } = props;
    const classes = useStyles();

    const handleClose = () => {
        onClose();
    };
    return (
        <Dialog onClose={handleClose} open={open} fullWidth>
            <div className={classes.dialog}>
                <Typography variant="h5" gutterBottom>
                    Make Part Live
                </Typography>
                {!dateLive && (
                    <InputField
                        rows={4}
                        disabled
                        value={liveTest?.message}
                        label="Can be made Live?"
                        fullWidth
                    />
                )}
                {!liveTest?.message?.includes('YES') ? (
                    <Button
                        className={classes.pullRight}
                        onClick={setStandardPrices}
                        variant="outlined"
                        color="primary"
                    >
                        FIX IT
                    </Button>
                ) : (
                    <Button
                        className={classes.pullRight}
                        disabled={!liveTest?.canMakeLive}
                        onClick={handleChangeLiveness}
                        variant="contained"
                        color="primary"
                    >
                        CONFIRM
                    </Button>
                )}
            </div>
        </Dialog>
    );
}

LiveDialog.propTypes = {
    onClose: PropTypes.func.isRequired,
    open: PropTypes.bool.isRequired,
    dateLive: PropTypes.string,
    liveTest: PropTypes.shape({ canMakeLive: PropTypes.bool, message: PropTypes.string }),
    handleChangeLiveness: PropTypes.func.isRequired,
    setStandardPrices: PropTypes.func.isRequired
};

LiveDialog.defaultProps = {
    dateLive: null,
    liveTest: { canMakeLive: false, message: '' }
};
