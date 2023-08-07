import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { whatToWandReprintActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.whatToWandReprint.item,
    itemTypes.whatToWandReprint.actionType,
    itemTypes.whatToWandReprint.uri,
    actionTypes,
    config.appRoot
);
