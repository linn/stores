import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { sernosSequencesActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.sernosSequences.item,
    itemTypes.sernosSequences.actionType,
    itemTypes.sernosSequences.uri,
    actionTypes,
    config.appRoot
);
