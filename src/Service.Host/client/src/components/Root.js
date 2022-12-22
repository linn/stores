import React from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { ConnectedRouter } from 'connected-react-router';
import CssBaseline from '@material-ui/core/CssBaseline';
import { Navigation } from '@linn-it/linn-form-components-library';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';
import MomentUtils from '@date-io/moment';
import PropTypes from 'prop-types';
import history from '../history';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';
import 'typeface-roboto';
import Part from '../containers/parts/Part';
import Parts from '../containers/parts/Parts';
import PartTemplate from '../containers/parts/PartTemplate';
import PartTemplates from '../containers/parts/PartTemplates';
import StartAllocation from '../containers/allocations/StartAllocation';
import SosAllocHeads from '../containers/allocations/SosAllocHeads';
import WwdReportOptions from '../containers/reports/WwdReportOptions';
import WwdReport from '../containers/reports/WwdReport';
import StoragePlaceAuditReportOptions from '../containers/reports/StoragePlaceAuditReportOptions';
import StoragePlaceAuditReport from '../containers/reports/StoragePlaceAuditReport';
import NotFoundPage from './NotFoundPage';
import Parcel from '../containers/parcels/Parcel';
import Parcels from '../containers/parcels/Parcels';
import MechPartSource from '../containers/parts/mechPartSource/MechPartSource';
import WorkstationTopUpStatus from '../containers/workstations/WorkstationTopUpStatus';
import DeptStockUtility from '../containers/DeptStockUtility/DeptStockUtility';
import DeptStockParts from '../containers/DeptStockUtility/DeptStockParts';
import DespatchPickingSummaryReport from '../containers/reports/DespatchPickingSummaryReport';
import DespatchPalletQueueReport from '../containers/reports/DespatchPalletQueueReport';
import StockViewerOptions from '../containers/stockLocatorUtility/StockViewerOptions';
import StockLocator from '../containers/stockLocatorUtility/StockLocator';
import Wand from '../containers/Wand';
import ExportRsns from '../containers/ExportRsns';
import Tpk from '../containers/Tpk';
import ExportReturn from '../containers/ExportReturn';
import StockMove from '../containers/StockMove';
import StockLocatorBatchView from '../containers/stockLocatorUtility/StockLocatorBatchView';
import StockLocatorPricesView from '../containers/stockLocatorUtility/StockLocatorPricesView';
import TqmsSummaryByCategoryReportOptions from '../containers/reports/TqmsSummaryByCategoryReportOptions';
import TqmsSummaryByCategoryReport from '../containers/reports/TqmsSummaryByCategoryReport';
import ConsignmentShipfiles from '../containers/ConsignmentShipfiles';
import Consignment from '../containers/consignments/Consignment';
import ExportConsignments from '../containers/consignments/ExportConsignments';
import GoodsInUtility from '../containers/goodsIn/GoodsInUtility';
import ImportBook from '../containers/importBooks/ImportBook';
import ImportBooks from '../containers/importBooks/ImportBooks';
import ImportBooksIprReportOptions from '../containers/reports/ImpbookIprReportOptions';
import ImportBooksIprReport from '../containers/reports/ImpbookIprReport';
import ImportBooksEuReportOptions from '../containers/reports/ImpbookEuReportOptions';
import ImportBooksEuReport from '../containers/reports/ImpbookEuReport';
import QcLabelPrintScreen from '../containers/goodsIn/QcLabelPrintScreen';
import StockBatchesInRotationOrder from '../containers/stockLocatorUtility/StockBatchesInRotationOrder';
import ZeroValuedInvoiceDetailsReportOptions from './reports/ZeroValuedInvoiceDetailsReportOptions';
import QcPartsReport from '../containers/reports/QcPartsReport';
import EuCreditInvoicesReport from '../containers/reports/EuCreditInvoicesReport';
import WhatToWandReprint from '../containers/WhatToWandReprint';
import TriggerLevelsForAStoragePlaceReport from '../containers/reports/TriggerLevelsForAStoragePlaceReport';

const Root = ({ store }) => (
    <div>
        <div className="padding-top-when-not-printing">
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <MuiPickersUtilsProvider utils={MomentUtils}>
                        <ConnectedRouter history={history}>
                            <div>
                                <Navigation />
                                <CssBaseline />

                                <Route exact path="/" render={() => <Redirect to="/inventory" />} />
                                <Route
                                    exact
                                    path="/logistics"
                                    render={() => <Redirect to="/inventory" />}
                                />

                                <Route
                                    exact
                                    path="/inventory/reports"
                                    render={() => <Redirect to="/inventory" />}
                                />
                                <Route
                                    exact
                                    path="/inventory/parts"
                                    render={() => <Redirect to="/parts" />}
                                />
                                <Route
                                    exact
                                    path="/inventory/parts/sources"
                                    render={() => <Redirect to="/parts/sources" />}
                                />

                                <Route
                                    path="/"
                                    render={() => {
                                        document.title = 'Stores';
                                        return false;
                                    }}
                                />

                                <Switch>
                                    <Route exact path="/inventory" component={App} />
                                    <Route
                                        exact
                                        path="/inventory/signin-oidc-client"
                                        component={Callback}
                                    />
                                    <Route exact path="/parts/sources" component={Parts} />
                                    <Route exact path="/parts" component={Parts} />
                                    <Route exact path="/parts/create" component={Part} />
                                    <Route exact path="/parts/:id" component={Part} />
                                    <Route
                                        exact
                                        path="/parts/sources/create"
                                        component={MechPartSource}
                                    />
                                    <Route
                                        exact
                                        path="/parts/sources/:id"
                                        component={MechPartSource}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/allocations"
                                        component={StartAllocation}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/sos-alloc-heads/:jobId"
                                        component={SosAllocHeads}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/sos-alloc-heads/:jobId"
                                        component={SosAllocHeads}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/reports/what-will-decrement/report"
                                        component={WwdReport}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/reports/what-will-decrement"
                                        component={WwdReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/stock-locators"
                                        component={DeptStockUtility}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/dept-stock-parts"
                                        component={DeptStockParts}
                                    />

                                    <Route
                                        exact
                                        path="/inventory/part-templates/create"
                                        component={PartTemplate}
                                    />

                                    <Route
                                        exact
                                        path="/inventory/part-templates/:id"
                                        component={PartTemplate}
                                    />

                                    <Route
                                        exact
                                        path="/inventory/part-templates"
                                        component={PartTemplates}
                                    />

                                    <Route exact path="/logistics/parcels" component={Parcels} />
                                    <Route
                                        exact
                                        path="/logistics/parcels/create"
                                        component={Parcel}
                                    />
                                    <Route exact path="/logistics/parcels/:id" component={Parcel} />

                                    <Route
                                        exact
                                        path="/inventory/reports/storage-place-audit/report"
                                        component={StoragePlaceAuditReport}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/reports/storage-place-audit"
                                        component={StoragePlaceAuditReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/workstations/top-up"
                                        component={WorkstationTopUpStatus}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/workstations/top-up/:jobRef"
                                        component={WorkstationTopUpStatus}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/allocations/despatch-picking-summary"
                                        component={DespatchPickingSummaryReport}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/stock-locator"
                                        component={StockViewerOptions}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/stock-locator/locators"
                                        component={StockLocator}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/stock-locator/locators/batches"
                                        component={StockLocatorBatchView}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/stock-locator/locators/batches/details"
                                        component={StockLocatorPricesView}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/stock-locator/rotations"
                                        component={StockBatchesInRotationOrder}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/allocations/despatch-pallet-queue"
                                        component={DespatchPalletQueueReport}
                                    />
                                    <Route exact path="/logistics/wand" component={Wand} />
                                    <Route
                                        exact
                                        path="/inventory/exports/rsns"
                                        component={ExportRsns}
                                    />

                                    <Route exact path="/logistics/tpk" component={Tpk} />

                                    <Route
                                        exact
                                        path="/inventory/exports/returns/:id"
                                        component={ExportReturn}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/exports/returns"
                                        component={ExportRsns}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/move-stock"
                                        component={StockMove}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/tqms-category-summary/report"
                                        component={TqmsSummaryByCategoryReport}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/tqms-category-summary"
                                        component={TqmsSummaryByCategoryReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/shipfiles"
                                        component={ConsignmentShipfiles}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/consignments/:consignmentId"
                                        component={Consignment}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/exports/consignments"
                                        component={ExportConsignments}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/consignments"
                                        component={Consignment}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/goods-in-utility"
                                        component={GoodsInUtility}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/reqs/labels"
                                        component={QcLabelPrintScreen}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/import-books/ipr"
                                        component={ImportBooksIprReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/import-books/ipr/report"
                                        component={ImportBooksIprReport}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/import-books/eu"
                                        component={ImportBooksEuReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/import-books/eu/report"
                                        component={ImportBooksEuReport}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/import-books/:id"
                                        component={ImportBook}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/import-books"
                                        component={ImportBooks}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/zero-valued-items-on-invoices-report"
                                        component={ZeroValuedInvoiceDetailsReportOptions}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/reports/qc-parts"
                                        component={QcPartsReport}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/reports/eu-credit-invoices"
                                        component={EuCreditInvoicesReport}
                                    />
                                    <Route
                                        exact
                                        path="/logistics/what-to-wand-reprint"
                                        component={WhatToWandReprint}
                                    />
                                    <Route
                                        exact
                                        path="/inventory/storage-places/reports/stock-trigger-levels"
                                        component={TriggerLevelsForAStoragePlaceReport}
                                    />
                                    <Route component={NotFoundPage} />
                                </Switch>
                            </div>
                        </ConnectedRouter>
                    </MuiPickersUtilsProvider>
                </OidcProvider>
            </Provider>
        </div>
    </div>
);

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;
