import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { debitNotesActionTypes as actionTypes } from '../index';
import * as itemTypes from '../../itemTypes';
import config from '../../config';

export default new FetchApiActions(
    itemTypes.debitNotes.item,
    itemTypes.debitNotes.actionType,
    itemTypes.debitNotes.uri,
    actionTypes,
    config.appRoot
);
