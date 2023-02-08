import { connect } from 'react-redux';
import TriggerLevelsForAStoragePlaceReport from '../../components/reports/TriggerLevelsForAStoragePlaceReport';
import actions from '../../actions/triggerLevelsForAStoragePlaceReportActions';
import * as reportTypes from '../../reportTypes';
import config from '../../config';
import storagePlacesSelectors from '../../selectors/storagePlacesSelectors';
import storagePlacesActions from '../../actions/storagePlacesActions';

const mapStateToProps = state => ({
    reportData: state[reportTypes.triggerLevelsForStoragePlaceReport.item].data,
    loading: state[reportTypes.triggerLevelsForStoragePlaceReport.item].loading,
    config,
    storagePlacesSearchResults: storagePlacesSelectors
        .getSearchItems(state)
        .map(i => ({ ...i, id: i.name })),
    storagePlacesSearchLoading: storagePlacesSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchReport: actions.fetchReport,
    searchStoragePlaces: storagePlacesActions.search
};

export default connect(mapStateToProps, mapDispatchToProps)(TriggerLevelsForAStoragePlaceReport);
