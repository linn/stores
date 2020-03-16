import React from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Router } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';
import { Navigation } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import history from '../history';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';
import 'typeface-roboto';

const Root = ({ store }) => (
    <div>
        <div style={{ paddingTop: '40px' }}>
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <Router history={history}>
                        <div>
                            <Navigation />
                            <CssBaseline />

                            <Route exact path="/" render={() => <Redirect to="/inventory" />} />

                            <Route
                                path="/"
                                render={() => {
                                    document.title = 'Stores';
                                    return false;
                                }}
                            />

                            <Switch>
                                <Route exact path="/inventory" component={App} />

                                <Route
                                    exact
                                    path="/inventory/signin-oidc-client"
                                    component={Callback}
                                />
                            </Switch>
                        </div>
                    </Router>
                </OidcProvider>
            </Provider>
        </div>
    </div>
);

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;
