import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { storagePlaceAuditReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.storagePlaceAuditReport.actionType,
    actionTypes,
    defaultState
);
