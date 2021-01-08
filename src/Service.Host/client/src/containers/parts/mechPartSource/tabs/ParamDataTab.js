import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import ParamDataTab from '../../../../components/parts/mechPartSource/tabs/ParamDataTab';
import partDataSheetValuesActions from '../../../../actions/partDataSheetValuesActions';
import {
    getResistorConstructionValues,
    getResistorPackageValues,
    getCapacitorDielectricValues,
    getCapacitorPackageValues,
    getTransistorPackageValues,
    getTransistorPolarityValues,
    getIcPackageValues
} from '../../../../selectors/partDataSheetValuesSelectors';

const mapStateToProps = state => ({
    resistorConstructionValues: getResistorConstructionValues(state),
    resistorPackageValues: getResistorPackageValues(state),
    capacitorDielectricValues: getCapacitorDielectricValues(state),
    capacitorPackageValues: getCapacitorPackageValues(state),
    transistorPackageValues: getTransistorPackageValues(state),
    transistorPolarityValues: getTransistorPolarityValues(state),
    icPackageValues: getIcPackageValues(state)
});

const initialise = () => dispatch => {
    dispatch(partDataSheetValuesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ParamDataTab));
