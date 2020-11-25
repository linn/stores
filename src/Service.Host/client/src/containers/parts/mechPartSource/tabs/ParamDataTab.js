import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import ParamDataTab from '../../../../components/parts/mechPartSource/tabs/ParamDataTab';
import partDataSheetValuesActions from '../../../../actions/partDataSheetValuesActions';
import {
    getResistorConstructionValues,
    getResistorPackageValues
} from '../../../../selectors/partDataSheetValuesSelectors';

const mapStateToProps = state => ({
    resistorConstructionValues: getResistorConstructionValues(state),
    resistorPackageValues: getResistorPackageValues(state)
});

const initialise = () => dispatch => {
    dispatch(partDataSheetValuesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ParamDataTab));
