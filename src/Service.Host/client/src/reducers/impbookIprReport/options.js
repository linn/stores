import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { impbookIprReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.impbookIprReport.actionType,
    actionTypes,
    defaultState
);
