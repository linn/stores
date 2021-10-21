import * as React from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import EditIcon from '@material-ui/icons/Edit';

export default function DialogInput({ name, onChange, propertyName, row }) {
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
            <Button variant="outlined" onClick={handleClickOpen}>
                <EditIcon />
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
                        id="name"
                        label={name}
                        type="number"
                        fullWidth
                        variant="standard"
                        value={value}
                        onChange={updateValue}
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
