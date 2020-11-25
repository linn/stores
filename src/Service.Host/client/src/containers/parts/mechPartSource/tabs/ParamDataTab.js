import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import ParamDataTab from '../../../../components/parts/mechPartSource/tabs/ParamDataTab';
import partDataSheetValuesActions from '../../../../actions/partDataSheetValuesActions';
import { getResistorConstructionValues } from '../../../../selectors/partDataSheetValuesSelectors';

const mapStateToProps = state => ({
    resistorConstructionValues: getResistorConstructionValues(state)
});

const initialise = () => dispatch => {
    dispatch(partDataSheetValuesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ParamDataTab));
