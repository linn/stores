import * as React from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import EditIcon from '@material-ui/icons/Edit';
import PropTypes from 'prop-types';
import { InputField } from '@linn-it/linn-form-components-library';

function DialogInput({
    name,
    onChange,
    propertyName,
    row,
    maxLength,
    decimalPlaces,
    innerInputValue,
    disabled
}) {
    const [open, setOpen] = React.useState(false);
    const [value, setValue] = React.useState(0);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleAccept = () => {
        setOpen(false);
        onChange(row, propertyName, value);
    };

    const updateValue = event => {
        setValue(event.target.value);
    };

    return (
        <div>
            <Button onClick={handleClickOpen} type="text">
                <InputField
                    label={name}
                    fullWidth
                    propertyName={propertyName}
                    type="number"
                    value={innerInputValue}
                    disabled={disabled}
                    required
                    maxLength={maxLength}
                    decimalPlaces={decimalPlaces}
                    adornment={<EditIcon />}
                    tooltip="click to edit"
                />
            </Button>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>Enter Value</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        To enter value and have it auto converted to GBP, type value and click
                        `Accept`
                    </DialogContentText>
                    <TextField
                        autoFocus
                        margin="dense"
                        id="field"
                        label={name}
                        type="number"
                        fullWidth
                        variant="standard"
                        value={value}
                        onChange={updateValue}
                        maxLength={maxLength}
                        decimalPlaces={decimalPlaces}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>Cancel</Button>
                    <Button onClick={handleAccept}>Accept</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}
DialogInput.propTypes = {
    name: PropTypes.string,
    onChange: PropTypes.func.isRequired,
    propertyName: PropTypes.string,
    row: PropTypes.arrayOf(PropTypes.shape({})),
    maxLength: PropTypes.number,
    decimalPlaces: PropTypes.number,
    innerInputValue: PropTypes.number,
    disabled: PropTypes.bool
};

DialogInput.defaultProps = {
    name: 'Value',
    propertyName: 'value',
    row: [],
    maxLength: 14,
    decimalPlaces: 2,
    innerInputValue: null,
    disabled: true
};

export default DialogInput;
