import { connect } from 'react-redux';
import makeExportReturnSelectors from '../selectors/makeExportReturnSelectors';
import CreateRep25 from '../components/CreateRep25';

const mapStateToProps = state => ({
    makeExportReturnLoading: makeExportReturnSelectors.getWorking(state)
});

export default connect(mapStateToProps)(CreateRep25);
