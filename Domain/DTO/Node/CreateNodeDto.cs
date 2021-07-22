using Domain.DTO.FinancialDTO;
using System.Collections.Generic;

namespace Domain.DTO.Node
{
    public class CreateNodeDto
    {

        public string IntroductionCode { get; set; }

        public UserFinancialDTO UserFinancialDTO { get; set; }

    }
}
