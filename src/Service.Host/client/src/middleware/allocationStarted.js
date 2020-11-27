import { utilities } from '@linn-it/linn-form-components-library';
import history from '../history';

export default () => next => action => {
    const result = next(action);
    if (action.type === 'RECEIVE_NEW_ALLOCATION' || action.type === 'RECEIVE_FINISH_ALLOCATION') {
        history.push(utilities.getHref(action.payload.data, 'display-results'));
    }

    return result;
};
