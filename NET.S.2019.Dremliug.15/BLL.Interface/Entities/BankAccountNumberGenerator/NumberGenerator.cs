using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Interfaces;

namespace BLL.Interface.Entities.BankAccountNumberGenerator
{
    public class NumberGenerator : INumberGenerator
    {
        private Func<string> _generate;

        public NumberGenerator(Func<string> generate)
        {
            _generate = generate;
        }

        public string Generate() => _generate();
    }
}
