import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import Wand from '../components/Wand';

const mapStateToProps = state => ({
    state
});

const initialise = () => dispatch => {

};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(Wand));
