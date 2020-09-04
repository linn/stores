// todo - move this into the shared components library
export const getPrivileges = state => {
    const { privilege } = state.oidc.user.profile;

    if (!privilege) {
        return [];
    }

    if (!Array.isArray(privilege)) {
        return [privilege];
    }

    return state.oidc.user.profile.privilege;
};

export const getUserName = state => {
    if (!state.oidc.user || !state.oidc.user.profile) {
        return null;
    }

    return state.oidc.user.profile.name;
};

export const getUserNumber = state => {
    if (!state.oidc.user || !state.oidc.user.profile) {
        return null;
    }

    return Number(state.oidc.user.profile.employee?.replace('/employees/', ''));
};
