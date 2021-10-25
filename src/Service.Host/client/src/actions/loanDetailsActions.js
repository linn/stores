import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { loanDetailsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.loanDetails.item,
    itemTypes.loanDetails.actionType,
    itemTypes.loanDetails.uri,
    actionTypes,
    config.appRoot
);
