using MediatR;
using System;

namespace AbcCompany.Core.Commands.CreateSalesRecord
{
    public class CreateSalesRecordCommand : IRequest
    {
        public string CustomerName { get; set; }
        public int CityCode { get; set; }
        public DateTime DateOfSale { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
