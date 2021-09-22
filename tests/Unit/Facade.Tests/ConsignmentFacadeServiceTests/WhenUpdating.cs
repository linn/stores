namespace Linn.Stores.Facade.Tests.ConsignmentFacadeServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
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
                                      Address = new Address { Country = new Country { ECMember = "Y" } },
                                      Terms = "R2D2",
                                      ShippingMethod = "Throw",
                                      DespatchLocationCode = "MoonBase Alpha",
                                      Pallets = new List<ConsignmentPallet>
                                                    {
                                                        new ConsignmentPallet
                                                            {
                                                                PalletNumber = 1, Depth = 0, Height = 0, Weight = 0, Width = 0
                                                            },
                                                        new ConsignmentPallet
                                                            {
                                                                PalletNumber = 12, Depth = 0, Height = 0, Weight = 0, Width = 0
                                                            },
                                                    },
                                      Items = new List<ConsignmentItem>
                                                    {
                                                        new ConsignmentItem
                                                            {
                                                                ItemNumber = 1, Depth = 0, Height = 0, Weight = 0, Width = 0
                                                            },
                                                        new ConsignmentItem
                                                            {
                                                                ItemNumber = 12, Depth = 0, Height = 0, Weight = 0, Width = 0
                                                            },
                                                    }
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
            updatedConsignment.Pallets.Should().Contain(p => p.PalletNumber == 1 && p.Depth == 11 && p.Height == 21 && p.Weight == 31 & p.Width == 41);
            updatedConsignment.Pallets.Should().Contain(p => p.PalletNumber == 2 && p.Depth == 1 && p.Height == 2 && p.Weight == 3 & p.Width == 4);
            updatedConsignment.Pallets.Should().NotContain(p => p.PalletNumber == 12);
            updatedConsignment.Items.Should().Contain(p => p.ItemNumber == 1 && p.Depth == 11 && p.Height == 21 && p.Weight == 31 & p.Width == 41);
            updatedConsignment.Items.Should().Contain(p => p.ItemNumber == 2 && p.Depth == 1 && p.Height == 2 && p.Weight == 3 & p.Width == 4);
            updatedConsignment.Items.Should().NotContain(p => p.ItemNumber == 12);
        }
    }
}
