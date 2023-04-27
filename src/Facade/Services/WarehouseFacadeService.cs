namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Wcs;
    using Linn.Stores.Resources;

    public class WarehouseFacadeService : IWarehouseFacadeService
    {
        private readonly IWarehouseService warehouseService;

        private readonly IRepository<Employee, int> employeeRepository;

        public WarehouseFacadeService(IWarehouseService warehouseService, IRepository<Employee, int> employeeRepository)
        {
            this.warehouseService = warehouseService;
            this.employeeRepository = employeeRepository;
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
    }
}
