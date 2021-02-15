﻿namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseServiceTests
{
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IWarehouseService Sut { get; private set; }

        protected IWcsPack WcsPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WcsPack = Substitute.For<IWcsPack>();

            this.Sut = new WarehouseService(this.WcsPack);
        }
    }
}
