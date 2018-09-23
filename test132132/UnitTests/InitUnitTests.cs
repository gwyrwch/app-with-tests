using System;

namespace test132132.UnitTests
{
    public static class InitUnitTests
    {
        public static void Assert(bool condition) {
            if (condition == false) {
                throw new Exception("Assertion failed");
            }
        }
        public static void Run() {
            Editor.TestSchemas.Run();
        }
    }
}
