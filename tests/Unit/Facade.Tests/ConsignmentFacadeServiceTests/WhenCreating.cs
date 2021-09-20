namespace Linn.Stores.Facade.Tests.ConsignmentFacadeServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using NUnit.Framework;

    public class WhenCreating : ContextBase
    {
        private ConsignmentResource createResource;

        private IResult<Consignment> result;

        private int? newHubId;

        private string newCarrierCode;

        private string newShippingMethod;

        private string newTerms;

        private string newDespatchLocationCode;

        private string newCustomsEntryCodePrefix;

        private string newCustomsEntryCode;

        private string newCustomsEntryCodeDate;

        private int? salesAccountId;

        private int? addressId;

        [SetUp]
        public void SetUp()
        {
            this.newCarrierCode = "Careful";
            this.newHubId = 2533;
            this.newShippingMethod = "Air";
            this.newTerms = "C3PO";
            this.newDespatchLocationCode = "Linn";
            this.newCustomsEntryCodePrefix = "PP";
            this.newCustomsEntryCode = "ENTRY";
            this.newCustomsEntryCodeDate = 1.February(2030).ToString("o");
            this.addressId = 1;
            this.salesAccountId = 101;

            this.createResource = new ConsignmentResource
                                      {
                                          SalesAccountId = this.salesAccountId,
                                          AddressId = this.addressId,
                                          HubId = this.newHubId,
                                          Carrier = this.newCarrierCode,
                                          ShippingMethod = this.newShippingMethod,
                                          Terms = this.newTerms,
                                          DespatchLocationCode = this.newDespatchLocationCode,
                                          CustomsEntryCodePrefix = this.newCustomsEntryCodePrefix,
                                          CustomsEntryCode = this.newCustomsEntryCode,
                                          CustomsEntryCodeDate = this.newCustomsEntryCodeDate,
                                          Pallets = new List<ConsignmentPalletResource>
                                                        {
                                                            new ConsignmentPalletResource
                                                                {
                                                                    PalletNumber = 1, Depth = 11, Height = 21, Weight = 31, Width = 41
                                                                },
                                                            new ConsignmentPalletResource
                                                                {
                                                                    PalletNumber = 2, Depth = 1, Height = 2, Weight = 3, Width = 4
                                                                }
                                                        },
                                          Items = new List<ConsignmentItemResource>
                                                        {
                                                            new ConsignmentItemResource
                                                                {
                                                                    ItemNumber = 1, Depth = 11, Height = 21, Weight = 31, Width = 41
                                                                },
                                                            new ConsignmentItemResource
                                                                {
                                                                    ItemNumber = 2, Depth = 1, Height = 2, Weight = 3, Width = 4
                                                                }
                                                        }
            };
                                          
            this.result = this.Sut.Add(this.createResource);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<CreatedResult<Consignment>>();
            var newConsignment = ((CreatedResult<Consignment>)this.result).Data;
            newConsignment.SalesAccountId.Should().Be(this.salesAccountId);
            newConsignment.AddressId.Should().Be(this.addressId);
            newConsignment.Carrier.Should().Be(this.newCarrierCode);
            newConsignment.Terms.Should().Be(this.newTerms);
            newConsignment.HubId.Should().Be(this.newHubId);
            newConsignment.ShippingMethod.Should().Be(this.newShippingMethod);
            newConsignment.DespatchLocationCode.Should().Be(this.newDespatchLocationCode);
            newConsignment.CustomsEntryCodePrefix.Should().Be(this.newCustomsEntryCodePrefix);
            newConsignment.CustomsEntryCode.Should().Be(this.newCustomsEntryCode);
            newConsignment.CustomsEntryCodeDate.Should().Be(DateTime.Parse(this.newCustomsEntryCodeDate));
            newConsignment.Pallets.Should().Contain(p => p.PalletNumber == 1 && p.Depth == 11 && p.Height == 21 && p.Weight == 31 & p.Width == 41);
            newConsignment.Pallets.Should().Contain(p => p.PalletNumber == 2 && p.Depth == 1 && p.Height == 2 && p.Weight == 3 & p.Width == 4);
            newConsignment.Pallets.Should().NotContain(p => p.PalletNumber == 12);
            newConsignment.Items.Should().Contain(p => p.ItemNumber == 1 && p.Depth == 11 && p.Height == 21 && p.Weight == 31 & p.Width == 41);
            newConsignment.Items.Should().Contain(p => p.ItemNumber == 2 && p.Depth == 1 && p.Height == 2 && p.Weight == 3 & p.Width == 4);
            newConsignment.Items.Should().NotContain(p => p.ItemNumber == 12);
            newConsignment.ConsignmentId.Should().Be(10101);
        }
    }
}
