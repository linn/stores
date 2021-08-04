import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import OrderDetailsTab from '../../../components/importBooks/tabs/OrderDetailsTab';
import cpcNumbersActions from '../../../actions/impbookCpcNumbersActions';
import cpcNumbersSelectors from '../../../selectors/impbookCpcNumbersSelectors';

const mapStateToProps = state => ({
    cpcNumbers: cpcNumbersSelectors.getItems(state)?.map(x => ({
        displayText: `${x.cpcNumber === 13 ? `${x.cpcNumber} (IPR)` : x.cpcNumber} - ${
            x.description
        }`,
        id: parseInt(x.cpcNumber, 10)
    }))
});

const initialise = () => dispatch => {
    dispatch(cpcNumbersActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(OrderDetailsTab));