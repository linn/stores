﻿import { createStore, applyMiddleware, compose } from 'redux';
import { apiMiddleware as api } from 'redux-api-middleware';
import thunkMiddleware from 'redux-thunk';
import { createBrowserHistory } from 'history';
import reducer from './reducers';
import authorization from './middleware/authorization';
import itemCreated from './middleware/itemCreated';
import receiveSosDetail from './middleware/receiveSosDetail';
import allocationStarted from './middleware/allocationStarted';
import previousLocationMiddleware from './middleware/previousLocation';
import receiveStockLocator from './middleware/receiveStockLocator';
import receivePalletMove from './middleware/receivePalletMove';
import receiveDoWandItem from './middleware/receiveDoWandItem';
import receiveUnallocateConsignment from './middleware/receiveUnallocateConsignment';

// eslint-disable-next-line no-underscore-dangle
const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const middleware = [
    authorization,
    api,
    thunkMiddleware,
    itemCreated,
    receiveSosDetail,
    receiveStockLocator,
    previousLocationMiddleware,
    allocationStarted,
    receivePalletMove,
    receiveDoWandItem,
    receiveUnallocateConsignment
];

export const history = createBrowserHistory();

const configureStore = initialState => {
    const enhancers = composeEnhancers(applyMiddleware(...middleware));
    const store = createStore(reducer(history), initialState, enhancers);

    return store;
};

export default configureStore;
