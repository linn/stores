import React from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Router } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';
import { Navigation } from '@linn-it/linn-form-components-library';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';
import MomentUtils from '@date-io/moment';
import PropTypes from 'prop-types';
import history from '../history';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';
import 'typeface-roboto';
import Part from '../containers/parts/Part';
import Parts from '../containers/parts/Parts';
import StartAllocation from '../containers/allocations/StartAllocation';
import SosAllocHeads from '../containers/allocations/SosAllocHeads';
import WwdReportOptions from '../containers/reports/WwdReportOptions';
import WwdReport from '../containers/reports/WwdReport';
import NotFoundPage from './NotFoundPage';

const Root = ({ store }) => (
    <div>
        <div style={{ paddingTop: '40px' }}>
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <MuiPickersUtilsProvider utils={MomentUtils}>
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

                                    <Route exact path="/inventory/parts" component={Parts} />
                                    <Route exact path="/inventory/parts/create" component={Part} />
                                    <Route exact path="/inventory/parts/:id" component={Part} />

                                    <Route
                                        exact
                                        path="/logistics/allocations"
                                        component={StartAllocation}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/sos-alloc-heads/:jobId"
                                        component={SosAllocHeads}
                                    />

                                    <Route
                                        exact
                                        path="/inventory/reports/what-will-decrement/report"
                                        component={WwdReport}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/reports/what-will-decrement"
                                        component={WwdReportOptions}
                                    />
                                    <Route component={NotFoundPage} />
                                </Switch>
                            </div>
                        </Router>
                    </MuiPickersUtilsProvider>
                </OidcProvider>
            </Provider>
        </div>
    </div>
);

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;
