import { connect } from 'react-redux';
import Callback from '../components/Callback';
import history from '../history';

function mapDispatchToProps() {
    return {
        onSuccess: user => {
            history.push(user.state.redirect);
        }
    };
}

export default connect(null, mapDispatchToProps)(Callback);
