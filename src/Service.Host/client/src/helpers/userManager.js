import { createUserManager } from 'redux-oidc';
import config from '../config';

const host = window.location.origin;

const oidcConfig = {
    authority: config.authorityUri,
    client_id: 'app',
    response_type: 'id_token token',
    scope: 'openid profile email associations',
    redirect_uri: `${host}/stores/signin-oidc-client`,
    post_logout_redirect_uri: `${host}`,
    silent_redirect_uri: `${host}/stores/signin-oidc-silent`,
    automaticSilentRenew: true,
    filterProtocolClaims: true,
    loadUserInfo: true
};

const userManager = createUserManager(oidcConfig);

export default userManager;
