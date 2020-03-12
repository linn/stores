import { render } from '@testing-library/react';
import { Provider } from 'react-redux';
import React from 'react';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import configureMockStore from 'redux-mock-store';
import { MemoryRouter } from 'react-router-dom';
import { SnackbarProvider } from 'notistack';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';
import MomentUtils from '@date-io/moment';

// eslint-disable-next-line react/prop-types
const Providers = ({ children }) => {
    const mockStore = configureMockStore();
    const store = mockStore({});
    return (
        <Provider store={store}>
            <MuiThemeProvider theme={createMuiTheme()}>
                <SnackbarProvider dense maxSnack={5}>
                    <MemoryRouter>
                        <MuiPickersUtilsProvider utils={MomentUtils}>
                            {children}
                        </MuiPickersUtilsProvider>
                    </MemoryRouter>
                </SnackbarProvider>
            </MuiThemeProvider>
        </Provider>
    );
};

const customRender = (ui, options) => render(ui, { wrapper: Providers, ...options });

export default customRender;
