import { makeActionTypes } from '@linn-it/linn-form-components-library';
import * as itemTypes from '../itemTypes';

export const partActionTypes = makeActionTypes(itemTypes.part.actionType);
export const partsActionTypes = makeActionTypes(itemTypes.parts.actionType, false);
