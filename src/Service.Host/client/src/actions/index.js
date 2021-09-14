import {
    makeActionTypes,
    makeReportActionTypes,
    makeProcessActionTypes
} from '@linn-it/linn-form-components-library';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';
import * as processTypes from '../processTypes';

export const partActionTypes = makeActionTypes(itemTypes.part.actionType);
export const partsActionTypes = makeActionTypes(itemTypes.parts.actionType, false);

export const accountingCompaniesActionTypes = makeActionTypes(
    itemTypes.accountingCompanies.actionType
);

export const departmentsActionTypes = makeActionTypes(itemTypes.departments.actionType);

export const rootProductsActionTypes = makeActionTypes(itemTypes.rootProducts.actionType);

export const partCategoriesActionTypes = makeActionTypes(itemTypes.partCategories.actionType);

export const partTemplatesActionTypes = makeActionTypes(itemTypes.partTemplates.actionType);

export const partLiveTestActionTypes = makeActionTypes(itemTypes.partLiveTest.actionType);

export const suppliersActionTypes = makeActionTypes(itemTypes.suppliers.actionType);

export const suppliersApprovedCarrierActionTypes = makeActionTypes(
    itemTypes.suppliersApprovedCarrier.actionType
);

export const sernosSequencesActionTypes = makeActionTypes(itemTypes.sernosSequences.actionType);

export const unitsOfMeasureActionTypes = makeActionTypes(itemTypes.unitsOfMeasure.actionType);

export const allocationActionTypes = makeActionTypes(itemTypes.allocation.actionType);

export const productAnalysisCodesActionTypes = makeActionTypes(
    itemTypes.productAnalysisCodes.actionType
);

export const nominalActionTypes = makeActionTypes(itemTypes.nominal.actionType);

export const decrementRulesActionTypes = makeActionTypes(itemTypes.decrementRules.actionType);

export const assemblyTechnologiesActionTypes = makeActionTypes(
    itemTypes.assemblyTechnologies.actionType
);

export const wwdReportActionTypes = makeReportActionTypes(reportTypes.wwdReport.actionType);

export const stockPoolsActionTypes = makeActionTypes(itemTypes.stockPools.actionType);

export const despatchLocationsActionTypes = makeActionTypes(itemTypes.despatchLocations.actionType);

export const countriesActionTypes = makeActionTypes(itemTypes.countries.actionType);

export const storagePlaceActionTypes = makeActionTypes(itemTypes.storagePlace.actionType);

export const storagePlacesActionTypes = makeActionTypes(itemTypes.storagePlaces.actionType);

export const auditLocationActionTypes = makeActionTypes(itemTypes.auditLocation.actionType);

export const auditLocationsActionTypes = makeActionTypes(itemTypes.auditLocations.actionType);

export const storagePlaceAuditReportActionTypes = makeReportActionTypes(
    reportTypes.storagePlaceAuditReport.actionType
);

export const createAuditReqsActionTypes = makeProcessActionTypes(
    processTypes.createAuditReqs.actionType
);

export const sosAllocHeadsActionTypes = makeActionTypes(itemTypes.sosAllocHeads.actionType);

export const sosAllocDetailActionTypes = makeActionTypes(itemTypes.sosAllocDetail.actionType);

export const sosAllocDetailsActionTypes = makeActionTypes(itemTypes.sosAllocDetails.actionType);

export const mechPartSourceActionTypes = makeActionTypes(itemTypes.mechPartSource.actionType);

export const manufacturersActionTypes = makeActionTypes(itemTypes.manufacturers.actionType);

export const employeesActionTypes = makeActionTypes(itemTypes.employees.actionType);

export const partDataSheetValuesActionTypes = makeActionTypes(
    itemTypes.partDataSheetValues.actionType
);

export const finishAllocationActionTypes = makeProcessActionTypes(
    processTypes.finishAllocation.actionType
);

export const pickItemsAllocationActionTypes = makeProcessActionTypes(
    processTypes.pickItemsAllocation.actionType
);

export const unpickItemsAllocationActionTypes = makeProcessActionTypes(
    processTypes.unpickItemsAllocation.actionType
);

export const tqmsCategoriesActionTypes = makeActionTypes(itemTypes.tqmsCategories.actionType);

export const workstationTopUpStatusActionTypes = makeActionTypes(
    itemTypes.workstationTopUpStatus.actionType
);

export const deptStockPartsActionTypes = makeActionTypes(itemTypes.deptStockParts.actionType);

export const stockLocatorsActionTypes = makeActionTypes(itemTypes.stockLocators.actionType);

export const stockLocatorActionTypes = makeActionTypes(
    itemTypes.stockLocator.actionType,
    true,
    true
);

export const despatchPickingSummaryReportActionTypes = makeReportActionTypes(
    reportTypes.despatchPickingSummaryReport.actionType
);

export const despatchPalletQueueReportActionTypes = makeReportActionTypes(
    reportTypes.despatchPalletQueueReport.actionType
);

export const stockLocatorBatchesActionTypes = makeActionTypes(
    itemTypes.stockLocatorBatches.actionType
);

export const storageLocationsActionTypes = makeActionTypes(itemTypes.storageLocations.actionType);

export const inspectedStatesActionTypes = makeActionTypes(itemTypes.inspectedStates.actionType);

export const movePalletToUpperActionTypes = makeProcessActionTypes(
    processTypes.movePalletToUpper.actionType
);

export const movePalletsToUpperActionTypes = makeProcessActionTypes(
    processTypes.movePalletsToUpper.actionType
);

export const stockLocatorLocationsActionTypes = makeActionTypes(
    itemTypes.stockLocatorLocations.actionType
);

export const nominalAccountsActionTypes = makeActionTypes(itemTypes.nominalAccounts.actionType);
export const wandConsignmentsActionTypes = makeActionTypes(itemTypes.wandConsignments.actionType);

export const wandItemsActionTypes = makeActionTypes(itemTypes.wandItems.actionType);

export const salesOutletsActionTypes = makeActionTypes(itemTypes.salesOutlets.actionType);

export const salesAccountsActionTypes = makeActionTypes(itemTypes.salesAccounts.actionType);

export const exportRsnsActionTypes = makeActionTypes(itemTypes.exportRsns.actionType);

export const stockQuantitiesActionTypes = makeReportActionTypes(
    itemTypes.stockQuantities.actionType
);

export const doWandItemActionTypes = makeProcessActionTypes(processTypes.doWandItem.actionType);

export const unallocateConsignmentActionTypes = makeProcessActionTypes(
    processTypes.unallocateConsignment.actionType
);

export const transferableStockActionTypes = makeActionTypes(itemTypes.transferableStock.actionType);

export const tpkTransferStockActionTypes = makeProcessActionTypes(
    processTypes.tpkTransferStock.actionType
);

export const exportReturnActionTypes = makeActionTypes(itemTypes.exportReturn.actionType);

export const makeIntercompanyInvoicesActionTypes = makeProcessActionTypes(
    processTypes.makeIntercompanyInvoices.actionType
);

export const unallocateConsignmentLineActionTypes = makeProcessActionTypes(
    processTypes.unallocateConsignmentLine.actionType
);

export const availableStockActionTypes = makeActionTypes(itemTypes.availableStock.actionType);

export const doStockMoveActionTypes = makeProcessActionTypes(processTypes.doStockMove.actionType);

export const stockLocatorPricesActionTypes = makeActionTypes(
    itemTypes.stockLocatorPrices.actionType
);

export const reqMovesActionTypes = makeActionTypes(itemTypes.reqMoves.actionType);

export const partStorageTypesActionTypes = makeActionTypes(itemTypes.partStorageTypes.actionType);

export const interCompanyInvoicesActionTypes = makeActionTypes(
    itemTypes.interCompanyInvoices.actionType
);

export const tqmsSummaryByCategoryReportActionTypes = makeReportActionTypes(
    reportTypes.tqmsSummaryByCategoryReport.actionType
);

export const tqmsJobRefsActionTypes = makeActionTypes(itemTypes.tqmsJobRefs.actionType);

export const consignmentShipfileActionTypes = makeActionTypes(
    itemTypes.consignmentShipfile.actionType,
    true,
    true
);

export const consignmentShipfilesActionTypes = makeActionTypes(
    itemTypes.consignmentShipfiles.actionType
);

export const parcelActionTypes = makeActionTypes(itemTypes.parcel.actionType);

export const parcelsActionTypes = makeActionTypes(itemTypes.parcels.actionType, false);

export const shipfilesSendEmailsActionTypes = makeProcessActionTypes(
    processTypes.shipfilesSendEmails.actionType
);

export const consignmentActionTypes = makeActionTypes(itemTypes.consignment.actionType);
export const consignmentsActionTypes = makeActionTypes(itemTypes.consignments.actionType, false);

export const hubActionTypes = makeActionTypes(itemTypes.hub.actionType);
export const hubsActionTypes = makeActionTypes(itemTypes.hubs.actionType, false);

export const carrierActionTypes = makeActionTypes(itemTypes.carrier.actionType);
export const carriersActionTypes = makeActionTypes(itemTypes.carriers.actionType, false);

export const shippingTermActionTypes = makeActionTypes(itemTypes.shippingTerm.actionType);
export const shippingTermsActionTypes = makeActionTypes(itemTypes.shippingTerms.actionType, false);

export const demLocationsActionTypes = makeActionTypes(itemTypes.demLocations.actionType, false);

export const loanDetailsActionTypes = makeActionTypes(itemTypes.loanDetails.actionType, false);

export const validatePurchaseOrderActionTypes = makeActionTypes(
    itemTypes.validatePurchaseOrderResult.actionType
);

export const salesArticlesActionTypes = makeActionTypes(itemTypes.salesArticles.actionType);

export const doBookInActionTypes = makeProcessActionTypes(processTypes.doBookIn.actionType);

export const importBookActionTypes = makeActionTypes(itemTypes.importBook.actionType);
export const importBooksActionTypes = makeActionTypes(itemTypes.importBooks.actionType, false);

export const impbookExchangeRatesActionTypes = makeActionTypes(
    itemTypes.impbookExchangeRates.actionType,
    false
);
export const impbookTransactionCodesActionTypes = makeActionTypes(
    itemTypes.impbookTransactionCodes.actionType,
    false
);
export const impbookTransportCodesActionTypes = makeActionTypes(
    itemTypes.impbookTransportCodes.actionType,
    false
);
export const impbookCpcNumbersActionTypes = makeActionTypes(
    itemTypes.impbookCpcNumbers.actionType,
    false
);

export const impbookIprReportActionTypes = makeReportActionTypes(
    reportTypes.impbookIprReport.actionType
);

export const impbookEuReportActionTypes = makeReportActionTypes(
    reportTypes.impbookEuReport.actionType
);

export const impbookDeliveryTermsActionTypes = makeReportActionTypes(
    itemTypes.impbookDeliveryTerms.actionType,
    false
);

export const portsActionTypes = makeActionTypes(itemTypes.ports.actionType, false);

export const cartonTypesActionTypes = makeActionTypes(itemTypes.cartonTypes.actionType, false);

export const validatePurchaseOrderBookInQtyResultActionTypes = makeActionTypes(
    itemTypes.validatePurchaseOrderBookInQtyResult.actionType
);

export const reqActionTypes = makeActionTypes(itemTypes.req.actionType);

export const debitNoteActionTypes = makeActionTypes(itemTypes.debitNote.actionType);

export const debitNotesActionTypes = makeActionTypes(itemTypes.debitNotes.actionType);

export const stockMovesActionTypes = makeActionTypes(itemTypes.stockMoves.actionType);

export const printConsignmentLabelActionTypes = makeProcessActionTypes(
    processTypes.printConsignmentLabel.actionType
);

export const currenciesActionTypes = makeActionTypes(itemTypes.currencies.actionType);

export const exchangeRatesActionTypes = makeActionTypes(itemTypes.exchangeRates.actionType);

export const printGoodsInLabelsActionTypes = makeProcessActionTypes(
    processTypes.printGoodsInLabels.actionType
);

export const validateStorageTypeActionTypes = makeActionTypes(
    itemTypes.validateStorageTypeResult.actionType
);

export const printConsignmentDocumentsActionTypes = makeProcessActionTypes(
    processTypes.printConsignmentDocuments.actionType
);

export const parcelsByNumberActionTypes = makeActionTypes(itemTypes.parcelsByNumber.actionType);
