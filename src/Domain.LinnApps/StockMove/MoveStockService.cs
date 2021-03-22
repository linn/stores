namespace Linn.Stores.Domain.LinnApps.StockMove
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public class MoveStockService : IMoveStockService
    {
        private readonly IStoresPack storesPack;

        private readonly IRepository<RequisitionHeader, int> requistionRepository;

        public MoveStockService(IStoresPack storesPack, IRepository<RequisitionHeader, int> requistionRepository)
        {
            this.storesPack = storesPack;
            this.requistionRepository = requistionRepository;
        }

        public RequisitionProcessResult MoveStock(
            int? reqNumber,
            string partNumber,
            int quantity,
            string @from,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockRotationDate,
            string to,
            int? toLocationId,
            int? toPalletNumber,
            DateTime? toStockRotationDate,
            int userNumber)
        {
            if (quantity <= 0)
            {
                return new RequisitionProcessResult(false, "You must have a valid quantity.");
            }

            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                return new RequisitionProcessResult(false, "A from and to location must be specified.");
            }

            if (this.IsKardexLocation(from) && this.IsKardexLocation(to))
            {
                return new RequisitionProcessResult(false, "You cannot move between two Kardex locations.");
            }

            var result = new RequisitionProcessResult();
            if (!reqNumber.HasValue)
            {
                var moveResult = this.storesPack.CreateMoveReq(userNumber);
                if (!moveResult.Success)
                {
                    throw new CreateReqFailureException("Failed to create move req");
                }

                result.ReqNumber = moveResult.ReqNumber;
            }

            var req = this.requistionRepository.FindById(result.ReqNumber);

            var test = this.requistionRepository.FindById(117004);

            if (!fromLocationId.HasValue && !fromPalletNumber.HasValue)
            {
                this.GetLocationDetails(from, out fromLocationId, out fromPalletNumber);
            }

            result.Success = true;
            return result;
        }

        public bool IsKardexLocation(string location)
        {
            var kardexList = new List<string> { "K1", "K2", "K3", "K4", "K5" };
            if (kardexList.Contains(location))
            {
                return true;
            }

            if (kardexList.Select(k => $"{k}-").Contains(location.Substring(0, 3)))
            {
                return true;
            }

            if (kardexList.Select(k => $"E-{k}-").Contains(location.Substring(0, 5)))
            {
                return true;
            }


            return false;
        }

        public void GetLocationDetails(string location, out int? locationId, out int? palletNumber)
        {
            if (int.TryParse(location, out _))
            {
                palletNumber = int.Parse(location);
                locationId = null;
                return;
            }

            if (location.Substring(0, 1) == "P" && int.TryParse(location.Substring(1), out _))
            {
                palletNumber = int.Parse(location.Substring(1));
                locationId = null;
                return;
            }

            palletNumber = null;
            locationId = null;
        }
    }
}
