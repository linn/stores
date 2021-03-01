import history from '../history';

export default () => next => action => {
    const result = next(action);
    if (action.type === 'RECEIVE_MAKE_EXPORT_RETURN') {
        history.push('/inventory/exports/rep-25');
    }

    return result;
};
