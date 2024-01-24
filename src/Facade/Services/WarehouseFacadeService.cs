namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Wcs;
    using Linn.Stores.Resources;
    
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Facade.Extensions;
    using Linn.Stores.Domain.LinnApps.Scs;
    using Linn.Stores.Resources.Scs;

    public class WarehouseFacadeService : IWarehouseFacadeService
    {
        private readonly IWarehouseService warehouseService;

        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IScsPalletsRepository scsPalletsRepository;

        public WarehouseFacadeService(IWarehouseService warehouseService, IRepository<Employee, int> employeeRepository, IScsPalletsRepository scsPalletsRepository)
        {
            this.warehouseService = warehouseService;
            this.employeeRepository = employeeRepository;
            this.scsPalletsRepository = scsPalletsRepository;
        }

        public IResult<MessageResult> MoveAllPalletsToUpper()
        {
            return new SuccessResult<MessageResult>(this.warehouseService.MoveAllPalletsToUpper());
        }

        public IResult<MessageResult> MovePalletToUpper(int palletNumber, string reference)
        {
            return new SuccessResult<MessageResult>(
                this.warehouseService.MovePalletToUpper(palletNumber, reference));
        }

        public IResult<MessageResult> MakeWarehouseTask(WarehouseTaskResource resource)
        {
            if (resource == null)
            {
                return new BadRequestResult<MessageResult>("No task to add");
            }

            var task = new WarehouseTask
                           {
                               PalletNumber = resource.PalletNumber,
                               Priority = resource.Priority,
                               OriginalLocation = resource.OriginalLocation,
                               Destination = resource.Destination,
                               TaskType = resource.TaskType
                           };

            if (!task.ValidPalletNumber())
            {
                return new BadRequestResult<MessageResult>("Not a valid pallet number");
            }

            if (!task.ValidPriority())
            {
                return new BadRequestResult<MessageResult>("Not a valid priority");
            }

            var employee = this.employeeRepository.FindById(resource.EmployeeId);
            if (employee == null)
            {
                return new NotFoundResult<MessageResult>("Employee not found");
            }

            if (task.TaskIsMove())
            {
                if (this.warehouseService.MovePallet(task.PalletNumber, task.Destination, task.Priority, employee))
                {
                    return new SuccessResult<MessageResult>(new MessageResult($"Task submitted to move pallet {resource.PalletNumber} to {resource.Destination}"));
                }
            }
            else if (task.TaskIsAtMove())
            {
                if (this.warehouseService.AtMovePallet(
                        task.PalletNumber,
                        task.OriginalLocation,
                        task.Destination,
                        task.Priority,
                        employee))
                {
                    return new SuccessResult<MessageResult>(
                        new MessageResult(
                            $"Task submitted to move pallet {resource.PalletNumber} from {resource.OriginalLocation} to {resource.Destination}"));
                }
            }
            else if (task.TaskIsEmpty())
            {
                if (this.warehouseService.EmptyLocation(task.PalletNumber, task.OriginalLocation, task.Priority, employee))
                {
                    return new SuccessResult<MessageResult>(new MessageResult($"Task submitted to move pallet {resource.PalletNumber} to {resource.Destination}"));
                }
            }

            return new ServerFailureResult<MessageResult>("Failed to make task");
        }

        public IResult<WarehouseLocation> GetPalletLocation(int palletNumber)
        {
            var location = this.warehouseService.GetWarehouseLocation(string.Empty, palletNumber);
            if (string.IsNullOrEmpty(location?.Location))
            {
                return new NotFoundResult<WarehouseLocation>("Pallet not on system");
            }

            return new SuccessResult<WarehouseLocation>(location);
        }

        public IResult<WarehouseLocation> GetPalletAtLocation(string location)
        {
            var warehouseLocation = this.warehouseService.GetWarehouseLocation(location, null);
            if (warehouseLocation?.PalletId == null)
            {
                return new NotFoundResult<WarehouseLocation>("Pallet not on system");
            }

            return new SuccessResult<WarehouseLocation>(warehouseLocation);
        }

        public IResult<IEnumerable<ScsPallet>> GetScsPallets()
        {
            var locations = this.warehouseService.GetWarehouseLocationsWithPallets().Select(l => l.ToScsPallet());

            if (locations.Any())
            {
                return new SuccessResult<IEnumerable<ScsPallet>>(locations);
            }

            return new NotFoundResult<IEnumerable<ScsPallet>>("No locations found");
        }

        public IResult<MessageResult> StorePallets(ScsPalletsResource resource)
        {
            var pallets = resource.data.Select(
                a => new ScsPallet()
                         {
                             PalletNumber = a.PalletNumber,
                             Allocated = a.Allocated,
                             Disabled = a.Disabled,
                             Height = a.Height,
                             HeatValue = a.HeatValue,
                             RotationAverage = a.RotationAverage,
                             Area = a.CurrentLocation.Area,
                             Column = a.CurrentLocation.Column,
                             Level = a.CurrentLocation.Level,
                             Side = a.CurrentLocation.Side
                         });

            var storePallets = pallets.Select(a => new ScsStorePallet(a));

            if (storePallets.Any())
            {

                // check for duplicate pallets
                var duplicatePallets = storePallets.GroupBy(p => p.PalletNumber).Where(d => d.Count() > 1).Select(p => p.Key)
                    .ToList();
                if (duplicatePallets.Any())
                {
                    return new ServerFailureResult<MessageResult>($"Failed store pallets. Found {duplicatePallets.Count} duplicate pallet numbers e.g. pallet {duplicatePallets.First()}");
                }

                this.scsPalletsRepository.ReplaceAll(storePallets.ToList());
                return new SuccessResult<MessageResult>(new MessageResult($"Saved {storePallets.Count()} pallets to Oracle"));
            }

            return new ServerFailureResult<MessageResult>("Failed store pallets");
        }
    }
}
