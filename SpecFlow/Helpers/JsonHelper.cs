using System.Collections.Generic;
using System.Text;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;

namespace SpecFlowProject.Helpers
{
    public static class JsonHelper { 
        public static void ShouldBeEquivalentJson(this string actualJson, string expectedJson)
        {
            JToken.Parse(actualJson).Should().BeEquivalentTo(JToken.Parse(expectedJson));
        }
    }
}
