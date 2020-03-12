import React from 'react';
import { CallbackComponent } from 'redux-oidc';
import { Loading } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import userManager from '../helpers/userManager';

const Callback = ({ onSuccess }) => (
    <CallbackComponent userManager={userManager} successCallback={onSuccess}>
        <Loading />
    </CallbackComponent>
);

Callback.propTypes = {
    onSuccess: PropTypes.func.isRequired
};

export default Callback;
