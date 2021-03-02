import { connect } from 'react-redux';
import exportReturnSelectors from '../selectors/exportReturnSelectors';
import CreateRep25 from '../components/CreateRep25';

const mapStateToProps = state => ({
    exportReturnLoading: exportReturnSelectors.getWorking(state)
});

export default connect(mapStateToProps)(CreateRep25);
