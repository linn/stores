import { ReportActions } from '@linn-it/linn-form-components-library';
import { storesMoveLogReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.storesMoveLogReport.item,
    reportTypes.storesMoveLogReport.actionType,
    reportTypes.storesMoveLogReport.uri,
    actionTypes,
    config.appRoot
);
