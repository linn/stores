// todo -- add to components library
let prevPathname = '';

export default () => next => action => {
    if (action.type === '@@router/LOCATION_CHANGE') {
        const newAction = {
            ...action,
            payload: {
                ...action.payload,
                prevPathname
            }
        };
        prevPathname = action.payload.location.pathname;
        return next(newAction);
    }
    return next(action);
};
