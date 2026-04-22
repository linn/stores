import React from 'react';
import { signOutEntra } from '../helpers/userManager';

class LoggedOut extends React.Component {
    componentDidMount() {
        signOutEntra();
    }

    render() {
        return <div>You have been logged out successfully.</div>;
    }
}

export default LoggedOut;
