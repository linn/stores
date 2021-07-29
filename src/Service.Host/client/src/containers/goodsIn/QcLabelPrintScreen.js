import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';

import QcLabelPrintScreen from '../../components/goodsIn/QcLabelPrintScreen';
import storagePlaceActions from '../../actions/storagePlaceActions';
import storagePlaceSelectors from '../../selectors/storagePlaceSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import reqSelectors from '../../selectors/reqSelectors'; // possible redundant - delete if so
import reqActions from '../../actions/reqActions';

const mapStateToProps = state => ({
    storagePlace: storagePlaceSelectors.getItem(state),
    qcInfo: partsSelectors.getSearchItems(state)?.[0]?.qcInformation 
});

const initialise = props => dispatch => {
    dispatch(
        storagePlaceActions.fetchByQueryString(
            'locationId',
            `${props.bookinLocationId ? props.bookinLocationId : ''}&palletNumber=${
                props.palletNumber
            }`
        )
    );
    dispatch(partsActions.search(props?.partNumber));
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(QcLabelPrintScreen));
