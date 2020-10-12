import { ReportActions } from '@linn-it/linn-form-components-library';
import { wwdReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.wwdReport.item,
    reportTypes.wwdReport.actionType,
    reportTypes.wwdReport.uri,
    actionTypes,
    config.appRoot
);
