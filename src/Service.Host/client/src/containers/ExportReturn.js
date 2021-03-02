import { connect } from 'react-redux';
import exportReturnSelectors from '../selectors/exportReturnSelectors';
import ExportReturn from '../components/ExportReturn';

const mapStateToProps = state => ({
    exportReturnLoading: exportReturnSelectors.getWorking(state)
});

export default connect(mapStateToProps)(ExportReturn);
