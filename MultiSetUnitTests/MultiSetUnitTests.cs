using System;
using System.Collections.Generic;
using MultiSet;

namespace Constructors
{   
    public static class Utility
    {
        public static int RandomInt(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }

    [TestClass]
    public class Constructor_VariableTypes
    {    
        [TestMethod]
        public void Constructor_Char()
        {
            var test = new MultiSet<char>();
            CollectionAssert.AreEqual(test.dict, new Dictionary<char, int>());
        }
        [TestMethod]
        public void Constructor_String()
        {
            var test = new MultiSet<string>();
            CollectionAssert.AreEqual(test.dict, new Dictionary<string, int>());
        }
    }

    [TestClass]
    public class Constructor_VariableTypesWithData
    {
        [DataTestMethod]
        [DataRow('a', 'b', 'c', 'd')]
        public void Constructor_CharWithData(char arg1, char arg2, char arg3, char arg4)
        {
            var actual = new MultiSet<char>();
            var expected = new Dictionary<char, int>();
            
            int rand = Utility.RandomInt(0, 5);
            actual.Add(arg1, rand);
            expected.Add(arg1, rand);

            rand = Utility.RandomInt(0, 5);
            actual.Add(arg2, rand);
            expected.Add(arg2, rand);

            rand = Utility.RandomInt(0, 5);
            actual.Add(arg3, rand);
            expected.Add(arg3, rand);

            rand = Utility.RandomInt(0, 5);
            actual.Add(arg4, rand);
            expected.Add(arg4, rand);

            CollectionAssert.AreEqual(expected, actual.dict);
        }

        [DataTestMethod]
        [DataRow("Albert", "Koneser", "YouTube", "WSEI")]
        public void Constructor_StringWithData(string arg1, string arg2, string arg3, string arg4)
        {
            var actual = new MultiSet<string>();
            var expected = new Dictionary<string, int>();

            int rand = Utility.RandomInt(0, 5);
            actual.Add(arg1, rand);
            expected.Add(arg1, rand);

            rand = Utility.RandomInt(0, 5);
            actual.Add(arg2, rand);
            expected.Add(arg2, rand);

            rand = Utility.RandomInt(0, 5);
            actual.Add(arg3, rand);
            expected.Add(arg3, rand);

            rand = Utility.RandomInt(0, 5);
            actual.Add(arg4, rand);
            expected.Add(arg4, rand);

            CollectionAssert.AreEqual(expected, actual.dict);
        }
    }
}