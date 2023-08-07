import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import bomStandardPricesSelectors from '../../../selectors/bomStandardPricesSelectors';
import bomStandardPricesActions from '../../../actions/bomStandardPricesActions';
import partLiveTestActions from '../../../actions/partLiveTestActions';
import partLiveTestSelectors from '../../../selectors/partLiveTestSelectors';
import LifeCycleTab from '../../../components/parts/tabs/LifeCycleTab';

const creating = match => match?.url?.endsWith('/create');

const mapStateToProps = (state, ownProps) => ({
    handleFieldChange: ownProps.handleFieldChange,
    dateCreated: ownProps.dateCreated,
    editStatus: ownProps.editStatus,
    createdByName: ownProps.createdByName,
    dateLive: ownProps.dateLive,
    madeLiveByName: ownProps.madeLiveByName,
    itemId: creating ? null : ownProps.match.params.id,
    phasedOutByName: ownProps.phasedOutByName,
    reasonPhasedOut: ownProps.reasonPhasedOut,
    scrapOrConvert: ownProps.scrapOrConvert,
    purchasingPhaseOutType: ownProps.purchasingPhaseOutType,
    datePhasedOut: ownProps.datePhasedOut,
    dateDesignObsolete: ownProps.dateDesignObsolete,
    canPhaseOut: ownProps.canPhaseOut,
    partNumber: ownProps.partNumber,
    handlePhaseOutClick: ownProps.handlePhaseOutClick,
    handlePhaseInClick: ownProps.handlePhaseInClick,
    handleChangeLiveness: ownProps.handleChangeLiveness,
    liveTestDialogOpe: ownProps.liveTestDialogOpen,
    bomStandardPrices: bomStandardPricesSelectors.getItem(state),
    bomStandardPricesLoading: bomStandardPricesSelectors.getLoading(state),
    liveTest: creating(ownProps.match) ? null : partLiveTestSelectors.getItem(state)
});
const initialise = ({ itemId }) => dispatch =>
    itemId && dispatch(partLiveTestActions.fetch(itemId));

const mapDispatchToProps = {
    initialise,
    setBomStandardPrices: bomStandardPricesActions.add,
    clearBomStandardPrices: bomStandardPricesActions.clearItem
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(LifeCycleTab));
