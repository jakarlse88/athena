using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Athena.Test
{
    public class Test
    {
        [Fact]
        public void TestTest()
        {
            var a = 1;
            var b = 2;
            var result = a + b;

            Assert.Equal(3, result);
        }
    }
}
