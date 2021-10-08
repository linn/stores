import {
    reducers as sharedLibraryReducers,
    fetchErrorReducer
} from '@linn-it/linn-form-components-library';
import { connectRouter } from 'connected-react-router';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import assemblyTechnologies from './parts/assemblyTechnologies';
import decrementRules from './parts/decrementRules';
import departments from './departments';
import accountingCompanies from './accountingCompanies';
import nominal from './nominal';
import parts from './parts/parts';
import part from './parts/part';
import partTemplates from './parts/partTemplates';
import partLiveTest from './parts/partLiveTest';
import partCategories from './partCategories';
import productAnalysisCodes from './productAnalysisCodes';
import rootProducts from './rootProducts';
import sernosSequences from './sernosSequences';
import suppliers from './suppliers';
import unitsOfMeasure from './unitsOfMeasure';
import allocation from './allocation';
import wwdReport from './wwdReport';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';
import * as processTypes from '../processTypes';
import stockPools from './stockPools';
import despatchLocations from './despatchLocations';
import countries from './countries';
import storagePlaceAuditReport from './storagePlaceAuditReport';
import storagePlace from './storagePlace';
import storagePlaces from './storagePlaces';
import auditLocation from './auditLocation';
import auditLocations from './auditLocations';
import createAuditReqs from './createAuditReqs';
import sosAllocHeads from './sosAllocHeads';
import sosAllocDetails from './sosAllocDetails';
import mechPartSource from './parts/mechPartSource';
import manufacturers from './manufacturers';
import employees from './employees';
import partDataSheetValues from './partDataSheetValues';
import finishAllocation from './finishAllocation';
import pickItemsAllocation from './pickItemsAllocation';
import unpickItemsAllocation from './unpickItemsAllocation';
import tqmsCategories from './tqmsCategories';
import workstationTopUpStatus from './workstationTopUpStatus';
import deptStockParts from './parts/deptStockParts';
import stockLocators from './stockLocators/stockLocators';
import stockLocator from './stockLocators/stockLocator';
import despatchPickingSummaryReport from './despatchPickingSummaryReport';
import stockLocatorBatches from './stockLocators/stockLocatorBatches';
import storageLocations from './stockLocators/storageLocations';
import inspectedStates from './stockLocators/inspectedStates';
import despatchPalletQueueReport from './despatchPalletQueueReport';
import movePalletToUpper from './movePalletToUpper';
import movePalletsToUpper from './movePalletsToUpper';
import stockLocatorLocations from './stockLocators/stockLocatorLocations';
import stockLocatorPrices from './stockLocators/stockLocatorPrices';
import stockQuantities from './stockLocators/stockQuantities';
import nominalAccounts from './nominalAccounts';
import wandConsignments from './wandConsignments';
import wandItems from './wandItems';
import salesOutlets from './salesOutlets';
import salesAccounts from './salesAccounts';
import exportRsns from './exportRsns';
import doWandItem from './doWandItem';
import transferableStock from './transferableStock';
import tpkTransferStock from './tpkTransferStock';
import unallocateConsignment from './unallocateConsignment';
import unallocateConsignmentLine from './unallocateConsignmentLine';
import availableStock from './availableStock';
import doStockMove from './doStockMove';
import historyStore from './history';
import reqMoves from './reqMoves';
import partStorageTypes from './partStorageTypes';
import exportReturn from './exportReturn';
import makeIntercompanyInvoices from './makeIntercompanyInvoices';
import interCompanyInvoices from './interCompanyInvoices';
import tqmsSummaryByCategoryReport from './tqmsSummaryByCategoryReport';
import tqmsJobRefs from './tqmsJobRefs';
import consignmentShipfile from './consignmentShipfile';
import consignmentShipfiles from './consignmentShipfiles';
import parcels from './parcels/parcels';
import parcel from './parcels/parcel';
import suppliersApprovedCarrier from './suppliersApprovedCarrier';
import shipfilesSendEmails from './shipfilesSendEmails';
import consignment from './consignment';
import consignments from './consignments';
import hub from './hub';
import hubs from './hubs';
import carrier from './carrier';
import carriers from './carriers';
import shippingTerm from './shippingTerm';
import shippingTerms from './shippingTerms';
import demLocations from './demLocations';
import loanDetails from './loanDetails';
import validatePurchaseOrderResult from './validatePurchaseOrderResult';
import salesArticles from './salesArticles';
import doBookIn from './doBookIn';
import importBook from './importBooks/importBook';
import importBooks from './importBooks/importBooks';
import impbookExchangeRates from './importBooks/impbookExchangeRates';
import impbookTransactionCodes from './importBooks/impbookTransactionCodes';
import impbookTransportCodes from './importBooks/impbookTransportCodes';
import impbookCpcNumbers from './importBooks/impbookCpcNumbers';
import impbookIprReport from './impbookIprReport';
import impbookEuReport from './impbookEuReport';
import impbookDeliveryTerms from './importBooks/impbookDeliveryTerms';
import ports from './importBooks/ports';
import cartonTypes from './cartonTypes';
import validatePurchaseOrderBookInQtyResult from './validatePurchaseOrderBookInQtyResult';
import req from './req';
import debitNote from './purchasing/debitNote';
import debitNotes from './purchasing/debitNotes';
import stockMoves from './stockLocators/stockMoves';
import printConsignmentLabel from './printConsignmentLabel';
import currencies from './currencies';
import exchangeRates from './exchangeRates';
import printGoodsInLabels from './printGoodsInLabels';
import validateStorageTypeResult from './validateStorageTypeResult';
import printConsignmentDocuments from './printConsignmentDocuments';
import saveConsignmentDocuments from './saveConsignmentDocuments';
import consignmentPackingList from './consignmentPackingList';
import rsns from './rsns';
import loanHeaders from './loanHeaders';
import purchaseOrders from './purchaseOrders';

const errors = fetchErrorReducer({ ...itemTypes, ...reportTypes, ...processTypes });

const rootReducer = history =>
    combineReducers({
        oidc,
        historyStore,
        router: connectRouter(history),
        accountingCompanies,
        allocation,
        assemblyTechnologies,
        auditLocation,
        auditLocations,
        availableStock,
        carrier,
        carriers,
        cartonTypes,
        consignment,
        consignmentPackingList,
        consignments,
        consignmentShipfile,
        consignmentShipfiles,
        countries,
        createAuditReqs,
        currencies,
        debitNote,
        debitNotes,
        demLocations,
        departments,
        decrementRules,
        deptStockParts,
        despatchLocations,
        despatchPalletQueueReport,
        despatchPickingSummaryReport,
        doBookIn,
        doStockMove,
        doWandItem,
        employees,
        exchangeRates,
        exportReturn,
        exportRsns,
        finishAllocation,
        hub,
        hubs,
        impbookCpcNumbers,
        impbookDeliveryTerms,
        impbookExchangeRates,
        impbookEuReport,
        impbookIprReport,
        impbookTransactionCodes,
        impbookTransportCodes,
        importBook,
        importBooks,
        inspectedStates,
        interCompanyInvoices,
        loanDetails,
        loanHeaders,
        makeIntercompanyInvoices,
        manufacturers,
        mechPartSource,
        movePalletsToUpper,
        movePalletToUpper,
        nominal,
        nominalAccounts,
        parcel,
        parcels,
        part,
        partCategories,
        partDataSheetValues,
        partLiveTest,
        parts,
        partStorageTypes,
        partTemplates,
        pickItemsAllocation,
        ports,
        printConsignmentDocuments,
        printConsignmentLabel,
        printGoodsInLabels,
        productAnalysisCodes,
        purchaseOrders,
        req,
        reqMoves,
        rootProducts,
        rsns,
        salesAccounts,
        salesArticles,
        salesOutlets,
        saveConsignmentDocuments,
        sernosSequences,
        shipfilesSendEmails,
        shippingTerm,
        shippingTerms,
        sosAllocDetails,
        sosAllocHeads,
        stockLocator,
        stockLocatorBatches,
        stockLocatorLocations,
        stockLocatorPrices,
        stockLocators,
        stockMoves,
        stockPools,
        stockQuantities,
        storageLocations,
        storagePlace,
        storagePlaces,
        storagePlaceAuditReport,
        suppliers,
        suppliersApprovedCarrier,
        tpkTransferStock,
        tqmsCategories,
        tqmsJobRefs,
        tqmsSummaryByCategoryReport,
        transferableStock,
        unallocateConsignment,
        unallocateConsignmentLine,
        unitsOfMeasure,
        unpickItemsAllocation,
        validatePurchaseOrderBookInQtyResult,
        validatePurchaseOrderResult,
        validateStorageTypeResult,
        wandConsignments,
        wandItems,
        workstationTopUpStatus,
        wwdReport,
        ...sharedLibraryReducers,
        errors
    });

export default rootReducer;
