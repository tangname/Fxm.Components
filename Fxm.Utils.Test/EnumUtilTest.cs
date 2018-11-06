using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xunit;

namespace Fxm.Utils.Test
{
    public class EnumUtilTest
    {
        public enum Week
        {
            [Description("Monday")]
            Mon = 1,
            [Description("Tuesday")]
            Tue = 2,
            Wed = 3,
            Thu = 4,
            Fri = 5,
            Sat = 6,
            Sun = 7
        }

        [Fact]
        public void GetEnumDescription()
        {
            var mon=EnumUtil.GetEnumDescription(Week.Mon);
            Assert.Equal("Monday", mon);

            var wed= EnumUtil.GetEnumDescription(Week.Wed);
            Assert.Equal("", wed);
        }

        [Fact]
        public void GetEnums()
        {
            var enums = EnumUtil.GetEnums<Week>();

            Assert.Equal(7, enums.Count());

            Assert.Equal("Monday", enums[0].Description);
            Assert.Equal("", enums[2].Description);

            Assert.Equal("Mon", enums[0].Name);
            Assert.Equal(1, enums[0].Value);
        }
    }
}
