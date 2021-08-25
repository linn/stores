import { connect } from 'react-redux';

import QcLabelPrintScreen from '../../components/goodsIn/QcLabelPrintScreen';
import storagePlaceSelectors from '../../selectors/storagePlaceSelectors';
import partsSelectors from '../../selectors/partsSelectors';

const mapStateToProps = state => ({
    storagePlace: storagePlaceSelectors.getItem(state),
    qcInfo: partsSelectors.getSearchItems(state)?.[0]?.qcInformation
});

const mapDispatchToProps = {
    // printLabels
};

export default connect(mapStateToProps, mapDispatchToProps)(QcLabelPrintScreen);
