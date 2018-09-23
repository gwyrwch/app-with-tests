using System;
using System.Collections.Generic;

namespace test132132.UnitTests.Editor
{
    public static class TestSchemas
    {
        public static void Run() {
            TestSerializing();
        }
        private static void TestSerializing() {
            var b = new Models.Test (
                title: "Kek",
                subject: "Mda",
                timeLimit: TimeSpan.FromSeconds(5),
                mode: Models.TimeMode.limitForQuestion
            ) {
                new Models.OpenQuestion("Question1?", 12, "Answer1"),
                new Models.MultipleChoiceQuestion(
                    "Question2", 13, new List<Models.Variant> {
                        new Models.Variant("First answer", false),
                        new Models.Variant("Second answer", true),
                    }
                )
            };
            string s1 = Newtonsoft.Json.JsonConvert.SerializeObject(b);

            var c = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Test>(s1);

            string s2 = Newtonsoft.Json.JsonConvert.SerializeObject(c);

            InitUnitTests.Assert(s1 == s2);
        }
    }
}
