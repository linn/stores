import { utilities } from '@linn-it/linn-form-components-library';
import history from '../history';

export default ({ getState }) => next => action => {
    const result = next(action);
    if (action.type.startsWith('RECEIVE_NEW_')) {
        if (!getState().router?.location?.pathname?.includes('/logistics/goods-in-utility')) {
            history.push(utilities.getSelfHref(action.payload.data));
        }
    }

    return result;
};
