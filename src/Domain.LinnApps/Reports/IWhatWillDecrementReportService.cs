namespace Linn.Stores.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IWhatWillDecrementReportService
    {
        ResultsModel WhatWillDecrementReport(string partNumber, int quantity, string typeOfRun, string workstationCode);
    }
}
