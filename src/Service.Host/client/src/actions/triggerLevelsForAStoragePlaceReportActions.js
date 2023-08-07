import { ReportActions } from '@linn-it/linn-form-components-library';
import { triggerLevelsForAStoragePlaceReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.triggerLevelsForStoragePlaceReport.item,
    reportTypes.triggerLevelsForStoragePlaceReport.actionType,
    reportTypes.triggerLevelsForStoragePlaceReport.uri,
    actionTypes,
    config.appRoot
);
