namespace Linn.Stores.Facade.Tests.ConsignmentFacadeServiceTests
{
    using System;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private ConsignmentUpdateResource updateResource;

        private IResult<Consignment> result;

        private int consignmentId;

        private int? newHubId;

        private string newCarrierCode;

        private string newShippingMethod;

        private string newTerms;

        private string newDespatchLocationCode;

        private string newCustomsEntryCodePrefix;

        private string newCustomsEntryCode;

        private string newCustomsEntryCodeDate;

        [SetUp]
        public void SetUp()
        {
            this.consignmentId = 493574354;
            this.newCarrierCode = "Careful";
            this.newHubId = 2533;
            this.newShippingMethod = "Air";
            this.newTerms = "C3PO";
            this.newDespatchLocationCode = "Linn";
            this.newCustomsEntryCodePrefix = "PP";
            this.newCustomsEntryCode = "ENTRY";
            this.newCustomsEntryCodeDate = 1.February(2030).ToString("o");

            var consignment = new Consignment
                                  {
                                      ConsignmentId = this.consignmentId,
                                      HubId = 1,
                                      Carrier = "Clumsy",
                                      Terms = "R2D2",
                                      ShippingMethod = "Throw",
                                      DespatchLocationCode = "MoonBase Alpha"
                                  };

            this.updateResource = new ConsignmentUpdateResource
                                      {
                                          HubId = this.newHubId,
                                          Carrier = this.newCarrierCode,
                                          ShippingMethod = this.newShippingMethod,
                                          Terms = this.newTerms,
                                          DespatchLocationCode = this.newDespatchLocationCode,
                                          CustomsEntryCodePrefix = this.newCustomsEntryCodePrefix,
                                          CustomsEntryCode = this.newCustomsEntryCode,
                                          CustomsEntryCodeDate = this.newCustomsEntryCodeDate
                                      };
                                          
            this.ConsignmentRepository.FindById(this.consignmentId).Returns(consignment);

            this.result = this.Sut.Update(this.consignmentId, this.updateResource);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<Consignment>>();
            var updatedConsignment = ((SuccessResult<Consignment>)this.result).Data;
            updatedConsignment.ConsignmentId.Should().Be(this.consignmentId);
            updatedConsignment.Carrier.Should().Be(this.newCarrierCode);
            updatedConsignment.Terms.Should().Be(this.newTerms);
            updatedConsignment.HubId.Should().Be(this.newHubId);
            updatedConsignment.ShippingMethod.Should().Be(this.newShippingMethod);
            updatedConsignment.DespatchLocationCode.Should().Be(this.newDespatchLocationCode);
            updatedConsignment.CustomsEntryCodePrefix.Should().Be(this.newCustomsEntryCodePrefix);
            updatedConsignment.CustomsEntryCode.Should().Be(this.newCustomsEntryCode);
            updatedConsignment.CustomsEntryCodeDate.Should().Be(DateTime.Parse(this.newCustomsEntryCodeDate));
        }
    }
}
