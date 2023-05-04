using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using Constructors;
using Microsoft.VisualBasic;
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
    public class NoData
    {    
        [TestMethod]
        public void Char()
        {
            var test = new MultiSet<char>();
            CollectionAssert.AreEqual(test.dict, new Dictionary<char, int>());
        }
        [TestMethod]
        public void String()
        {
            var test = new MultiSet<string>();
            CollectionAssert.AreEqual(test.dict, new Dictionary<string, int>());
        }
    }

    [TestClass]
    public class WithData
    {
        [DataTestMethod]
        [DataRow('a', 'b', 'c', 'd')]
        [DataRow('.', '!', 't', '"')]
        [DataRow('1', '1', 'h', 'e')]
        public void Char(char ch1, char ch2, char ch3, char ch4)
        {
            var list = new List<char> { ch1, ch2, ch3 };
            var actual = new MultiSet<char>(list);
            Assert.AreEqual(actual.Contains(ch1), true);
            Assert.AreEqual(actual.Contains(ch2), true);
            Assert.AreEqual(actual.Contains(ch3), true);
            Assert.AreEqual(actual.Contains(ch4), false);
        }
        [DataTestMethod]
        [DataRow("dsa", "asd", "fds", "sdf")]
        [DataRow("fgh", "hgf", "dfg", "gfd")]
        [DataRow("jhg", "ghj", "hjk", "kjh")]
        public void String(string s1, string s2, string s3, string s4)
        {
            var list = new List<string> { s1, s2, s3 };
            var actual= new MultiSet<string>(list);
            Assert.AreEqual(actual.Contains(s1), true);
            Assert.AreEqual(actual.Contains(s2), true);
            Assert.AreEqual(actual.Contains(s3), true);
            Assert.AreEqual(actual.Contains(s4), false);
        }
    }
}

namespace DataManipulation
{
    [TestClass]
    public class AddMultiple
    {
        [DataTestMethod]
        [DataRow('a', 'b', 'c', 'd')]
        public void Char(char arg1, char arg2, char arg3, char arg4)
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
        public void String(string arg1, string arg2, string arg3, string arg4)
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
    
    [TestClass]
    public class AddSingle
    {
        [DataTestMethod]
        [DataRow('a', 'b', 'c', 'd')]
        public void Char(char arg1, char arg2, char arg3, char arg4)
        {
            var actual = new MultiSet<char>();
            var expected = new Dictionary<char, int>();

            actual.Add(arg1);
            expected.Add(arg1, 1);

            actual.Add(arg2);
            expected.Add(arg2, 1);

            actual.Add(arg3);
            expected.Add(arg3, 1);

            actual.Add(arg4);
            expected.Add(arg4, 1);

            CollectionAssert.AreEqual(expected, actual.dict);
        }

        [DataTestMethod]
        [DataRow("Albert", "Koneser", "YouTube", "WSEI")]
        public void String(string arg1, string arg2, string arg3, string arg4)
        {
            var actual = new MultiSet<string>();
            var expected = new Dictionary<string, int>();

            actual.Add(arg1);
            expected.Add(arg1, 1);

            actual.Add(arg2);
            expected.Add(arg2, 1);

            actual.Add(arg3);
            expected.Add(arg3, 1);

            actual.Add(arg4);
            expected.Add(arg4, 1);

            CollectionAssert.AreEqual(expected, actual.dict);
        }
    }

    [TestClass]
    public class Contains
    {
        [DataTestMethod]
        [DataRow(new char[] { '1', '2' }, '3', false)]
        [DataRow(new char[] {  }, '3', false)]
        [DataRow(new char[] { '1', '2' }, '2', true)]
        public void Char(char[] list, char ch, bool outcome)
        {
            var actual = new MultiSet<char>(list);
            Assert.AreEqual(actual.Contains(ch), outcome);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asd", "sdf" }, "dfg", false)]
        [DataRow(new string[] { }, "xcv", false)]
        [DataRow(new string[] { "asd", "asd", "sdf" }, "asd", true)]
        public void String(string[] list, string s, bool outcome)
        {
            var actual = new MultiSet<string>(list);
            Assert.AreEqual(actual.Contains(s), outcome);
        }
    }

    [TestClass]
    public class RemoveAll
    {
        [DataTestMethod]
        [DataRow(new char[] { '1', '0', '0' }, new char[] { '1' }, '0')]
        [DataRow(new char[] { '0', '0', '0' }, new char[] { }, '0')]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '1', '2', '3' }, '0')]
        public void Char(char[] og, char[] end, char toRemove)
        {
            var actual = new MultiSet<char>(og);
            actual.RemoveAll(toRemove);
            var expected = new MultiSet<char>(end);

            CollectionAssert.AreEqual(actual.dict, expected.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asd", "bad", "asd" }, new string[] { "asd" }, "asd")]
        [DataRow(new string[] { "asd", "asd", "asd" }, new string[] { }, "asd")]
        [DataRow(new string[] { "dfg", "sdf", "sdf" }, new string[] { "dfg", "sdf", "sdf" }, "asd")]
        public void Char(string[] og, string[] end, string toRemove)
        {
            var actual = new MultiSet<string>(og);
            actual.RemoveAll(toRemove);
            var expected = new MultiSet<string>(end);

            CollectionAssert.AreEqual(actual.dict, expected.dict);
        }
    }

    [TestClass]
    public class RemoveMultiple
    {
        [DataTestMethod]
        [DataRow(new char[] { '0', '0', '0' }, new char[] { '0' }, '0', 2)]
        [DataRow(new char[] { '0', '0', '0' }, new char[] {  }, '0', 3)]
        [DataRow(new char[] { '0', '0', '0' }, new char[] {  }, '0', 7)]
        public void Char(char[] og, char[] end, char toRemove, int numOfRemoval)
        {
            var actual = new MultiSet<char>(og);
            actual.Remove(toRemove, numOfRemoval);
            var expected = new MultiSet<char>(end);

            CollectionAssert.AreEqual(actual.dict, expected.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asd", "asd", "asd" }, new string[] { "asd" }, "asd", 2)]
        [DataRow(new string[] { "asd", "asd", "asd" }, new string[] { }, "asd", 3)]
        [DataRow(new string[] { "asd", "asd", "asd" }, new string[] { }, "asd", 7)]
        public void Char(string[] og, string[] end, string toRemove, int numOfRemoval)
        {
            var actual = new MultiSet<string>(og);
            actual.Remove(toRemove, numOfRemoval);
            var expected = new MultiSet<string>(end);

            CollectionAssert.AreEqual(actual.dict, expected.dict);
        }
    }

    [TestClass]
    public class RemoveSingle
    {
        [DataTestMethod]
        [DataRow(new char[] { '1', '2', '3', '4', '2' }, '2', new char[] { '1', '2', '3', '4' })]
        [DataRow(new char[] { '3' }, '3', new char[] { })]
        [DataRow(new char[] { '3', '3', '3' }, '3', new char[] { '3', '3' })]
        public void Char(char[] ogList, char toRemove, char[] endList)
        {
            var actual = new MultiSet<char>(ogList);
            var expected = new MultiSet<char>(endList);

            actual.Remove(toRemove);

            CollectionAssert.AreEqual(expected.dict, actual.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asd", "dwq", "dwq" }, "dwq", new string[] { "asd", "dwq" })]
        [DataRow(new string[] { "zxc" }, "zxc", new string[] { })]
        [DataRow(new string[] { "asd", "sdf", "dfg", "fgh" }, "asd", new string[] { "sdf", "dfg", "fgh" })]
        public void String(string[] ogList, string toRemove, string[] endList)
        {
            var actual = new MultiSet<string>(ogList);
            var expected = new MultiSet<string>(endList);

            actual.Remove(toRemove);

            CollectionAssert.AreEqual(expected.dict, actual.dict);
        }
    }

    [TestClass]
    public class GetNewEmpty
    {
        [TestMethod]
        public void Char() 
        {
            CollectionAssert.AreEqual(MultiSet<char>.Empty.dict, new Dictionary<char, int>());
        }
        
        [TestMethod]
        public void String()
        {
            CollectionAssert.AreEqual(MultiSet<string>.Empty.dict, new Dictionary<string, int>());
        }
    }

    [TestClass]
    public class Clear
    {
        [DataTestMethod]
        [DataRow(new char[] {'1', '2', '3'})]
        [DataRow(new char[] { })]
        public void Char(char[] list)
        {
            var actual = new MultiSet<char>(list);
            actual.Clear();
            var expected = new MultiSet<char>();

            CollectionAssert.AreEqual(actual.dict, expected.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "dfg", "sdf", "asd" })]
        [DataRow(new string[] { })]
        public void String(string[] list)
        {
            var actual = new MultiSet<string>(list);
            actual.Clear();
            var expected = new MultiSet<string>();

            CollectionAssert.AreEqual(actual.dict, expected.dict);
        }
    }

    [TestClass]
    public class GetCount
    {
        [DataTestMethod]
        [DataRow(new char[] { '3', '3', '3' })]
        [DataRow(new char[] { '2', '2', '3', '4', '5' })]
        [DataRow(new char[] {  })]
        [DataRow(new char[] { '1', '2', '3', '4', '5', '6', '7' })]
        public void Char(char[] list) 
        {
            var actual = new MultiSet<char>(list);
            Assert.AreEqual(actual.Count, list.Length);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asx", "sdc", "dfv", "fgb", "ghn", "hjm"})]
        [DataRow(new string[] { "asd", "asd", "fds", "asd" })]
        [DataRow(new string[] { "123", "234", "345", "345", "345" })]
        [DataRow(new string[] {  })]
        public void String(string[] list)
        {
            var actual = new MultiSet<string>(list);
            Assert.AreEqual(actual.Count, list.Length);
        }
    }

    [TestClass]
    public class CopyTo
    {
        [DataTestMethod]
        [DataRow(new char[] { '1', '2' }, 0, new char[] { '0', '0', '0' }, new char[] { '1', '2', '0' })]
        [DataRow(new char[] { '1', '2' }, 1, new char[] { '0', '0', '0' }, new char[] { '0', '1', '2' })]
        [DataRow(new char[] { '1', '2' }, 2, new char[] { '0', '0', '0' }, new char[] { '0', '0', '1' })]
        [DataRow(new char[] {  }, 0, new char[] { '0', '0', '0' }, new char[] { '0', '0', '0' })]
        public void Char(char[] list, int index, char[] og, char[] end)
        {
            var actual = new MultiSet<char>(list);
            actual.CopyTo(og, index);

            CollectionAssert.AreEqual(og, end);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asd", "sdf" }, 0, new string[] { "qwe", "wer", "ert" }, new string[] { "asd", "sdf", "ert" })]
        [DataRow(new string[] { "asd", "sdf" }, 1, new string[] { "qwe", "wer", "ert" }, new string[] { "qwe", "asd", "sdf" })]
        [DataRow(new string[] { "asd", "sdf" }, 2, new string[] { "qwe", "wer", "ert" }, new string[] { "qwe", "wer", "asd" })]
        [DataRow(new string[] { }, 0, new string[] { "qwe", "wer", "ert" }, new string[] { "qwe", "wer", "ert" })]
        public void String(string[] list, int index, string[] og, string[] end)
        {
            var actual = new MultiSet<string>(list);
            actual.CopyTo(og, index);

            CollectionAssert.AreEqual(og, end);
        }
    }

    [TestClass]
    public class Foreach
    {
        [DataTestMethod]
        [DataRow(new char[] { 'a', 'b', 'a', 'c' })]
        [DataRow(new char[] { 'c', 'c', 'c', 'c' })]
        public void Char(char[] list)
        {
            var actual = new MultiSet<char>(list);
            foreach(var element in actual)
            {
                Assert.AreEqual(list.Contains(element), true);
            }
            Assert.AreEqual(list.Length, actual.Count);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asd", "sdf", "dfg", "fgh" })]
        [DataRow(new string[] { "asd", "asd", "asd", "asd"})]
        public void String(string[] list)
        {
            var actual = new MultiSet<string>(list);
            foreach (var element in actual)
            {
                Assert.AreEqual(list.Contains(element), true);
            }
            Assert.AreEqual(list.Length, actual.Count);
        }
    }

    [TestClass]
    public class UnionWith
    {
        [DataTestMethod]
        [DataRow(new char[] { 'a', 'c' }, new char[] { 'b' }, new char[] { 'a', 'c', 'b' })]
        public void Char(char[] l1, char[] l2, char[] end)
        {
            var ms1 = new MultiSet<char>(l1);
            var expected = new MultiSet<char>(end);

            ms1.UnionWith(l2);

            CollectionAssert.AreEqual(expected.dict, ms1.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asd", "sdf" }, new string[] { "dfg" }, new string[] { "asd", "sdf", "dfg" })]
        public void String(string[] l1, string[] l2, string[] end)
        {
            var ms1 = new MultiSet<string>(l1);
            var expected = new MultiSet<string>(end);

            ms1.UnionWith(l2);

            CollectionAssert.AreEqual(expected.dict, ms1.dict);
        }
    }

    [TestClass]
    public class IntersectWith
    {
        [DataTestMethod]
        [DataRow(new char[] { 'a', 'c' }, new char[] { 'b', 'c' }, new char[] { 'c' })]
        public void Char(char[] l1, char[] l2, char[] end)
        {
            var ms1 = new MultiSet<char>(l1);
            var expected = new MultiSet<char>(end);

            ms1.IntersectWith(l2);

            CollectionAssert.AreEqual(expected.dict, ms1.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asd", "sdf" }, new string[] { "dfg", "sdf" }, new string[] { "sdf" })]
        public void String(string[] l1, string[] l2, string[] end)
        {
            var ms1 = new MultiSet<string>(l1);
            var expected = new MultiSet<string>(end);

            ms1.IntersectWith(l2);

            CollectionAssert.AreEqual(expected.dict, ms1.dict);
        }
    }

    [TestClass]
    public class ExceptWith
    {
        [DataTestMethod]
        [DataRow(new char[] { 'a', 'c' }, new char[] { 'b', 'c' }, new char[] { 'a' })]
        public void Char(char[] l1, char[] l2, char[] end)
        {
            var ms1 = new MultiSet<char>(l1);
            var expected = new MultiSet<char>(end);

            ms1.ExceptWith(l2);

            CollectionAssert.AreEqual(expected.dict, ms1.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asd", "sdf" }, new string[] { "dfg", "sdf" }, new string[] { "asd" })]
        public void String(string[] l1, string[] l2, string[] end)
        {
            var ms1 = new MultiSet<string>(l1);
            var expected = new MultiSet<string>(end);

            ms1.ExceptWith(l2);

            CollectionAssert.AreEqual(expected.dict, ms1.dict);
        }
    }

    [TestClass]
    public class SymmetricExceptWith
    {
        [DataTestMethod]
        [DataRow(new char[] { 'a', 'c' }, new char[] { 'b', 'c' }, new char[] { 'a', 'b' })]
        public void Char(char[] l1, char[] l2, char[] end)
        {
            var ms1 = new MultiSet<char>(l1);
            var expected = new MultiSet<char>(end);

            ms1.SymmetricExceptWith(l2);

            CollectionAssert.AreEqual(expected.dict, ms1.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "asd", "sdf" }, new string[] { "dfg", "sdf" }, new string[] { "asd", "dfg" })]
        public void String(string[] l1, string[] l2, string[] end)
        {
            var ms1 = new MultiSet<string>(l1);
            var expected = new MultiSet<string>(end);

            ms1.SymmetricExceptWith(l2);

            CollectionAssert.AreEqual(expected.dict, ms1.dict);
        }
    }

    [TestClass]
    public class IsSubsetOf
    {
        [DataTestMethod]
        [DataRow(new char[] { '1' }, new char[] { '1', '2', '3' }, true)]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '1', '2', '3' }, true)]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '1' }, false)]
        public void Char(char[] l1, char[] l2, bool outcome)
        {
            var ms1 = new MultiSet<char>(l1);
            var ms2 = new MultiSet<char>(l2);
            Assert.AreEqual(ms1.IsSubsetOf(ms2), outcome);
        }

        [DataTestMethod]
        [DataRow(new string[] { "1" }, new string[] { "1", "2", "3" }, true)]
        [DataRow(new string[] { "1", "2", "3" }, new string[] { "1", "2", "3" }, true)]
        [DataRow(new string[] { "1", "2", "3" }, new string[] { "1" }, false)]
        public void String(string[] l1, string[] l2, bool outcome)
        {
            var ms1 = new MultiSet<string>(l1);
            var ms2 = new MultiSet<string>(l2);
            Assert.AreEqual(ms1.IsSubsetOf(ms2), outcome);
        }
    }

    [TestClass]
    public class IsProperSubsetOf
    {
        [DataTestMethod]
        [DataRow(new char[] { '1' }, new char[] { '1', '2', '3' }, true)]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '1', '2', '3' }, false)]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '1' }, false)]
        public void Char(char[] l1, char[] l2, bool outcome)
        {
            var ms1 = new MultiSet<char>(l1);
            var ms2 = new MultiSet<char>(l2);
            Assert.AreEqual(ms1.IsProperSubsetOf(ms2), outcome);
        }

        [DataTestMethod]
        [DataRow(new string[] { "1" }, new string[] { "1", "2", "3" }, true)]
        [DataRow(new string[] { "1", "2", "3" }, new string[] { "1", "2", "3" }, false)]
        [DataRow(new string[] { "1", "2", "3" }, new string[] { "1" }, false)]
        public void String(string[] l1, string[] l2, bool outcome)
        {
            var ms1 = new MultiSet<string>(l1);
            var ms2 = new MultiSet<string>(l2);
            Assert.AreEqual(ms1.IsProperSubsetOf(ms2), outcome);
        }
    }

    [TestClass]
    public class IsSupersetOf
    {
        [DataTestMethod]
        [DataRow(new char[] { '1' }, new char[] { '1', '2', '3' }, false)]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '1', '2', '3' }, true)]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '1' }, true)]
        public void Char(char[] l1, char[] l2, bool outcome)
        {
            var ms1 = new MultiSet<char>(l1);
            var ms2 = new MultiSet<char>(l2);
            Assert.AreEqual(ms1.IsSupersetOf(ms2), outcome);
        }

        [DataTestMethod]
        [DataRow(new string[] { "1" }, new string[] { "1", "2", "3" }, false)]
        [DataRow(new string[] { "1", "2", "3" }, new string[] { "1", "2", "3" }, true)]
        [DataRow(new string[] { "1", "2", "3" }, new string[] { "1" }, true)]
        public void String(string[] l1, string[] l2, bool outcome)
        {
            var ms1 = new MultiSet<string>(l1);
            var ms2 = new MultiSet<string>(l2);
            Assert.AreEqual(ms1.IsSupersetOf(ms2), outcome);
        }
    }

    [TestClass]
    public class IsProperSupersetOf
    {
        [DataTestMethod]
        [DataRow(new char[] { '1' }, new char[] { '1', '2', '3' }, false)]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '1', '2', '3' }, false)]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '1' }, true)]
        public void Char(char[] l1, char[] l2, bool outcome)
        {
            var ms1 = new MultiSet<char>(l1);
            var ms2 = new MultiSet<char>(l2);
            Assert.AreEqual(ms1.IsProperSupersetOf(ms2), outcome);
        }

        [DataTestMethod]
        [DataRow(new string[] { "1" }, new string[] { "1", "2", "3" }, false)]
        [DataRow(new string[] { "1", "2", "3" }, new string[] { "1", "2", "3" }, false)]
        [DataRow(new string[] { "1", "2", "3" }, new string[] { "1" }, true)]
        public void String(string[] l1, string[] l2, bool outcome)
        {
            var ms1 = new MultiSet<string>(l1);
            var ms2 = new MultiSet<string>(l2);
            Assert.AreEqual(ms1.IsProperSupersetOf(ms2), outcome);
        }
    }

    [TestClass]
    public class Overlaps
    {
        [DataTestMethod]
        [DataRow(new char[] { '1' }, new char[] { '4', '2', '3' }, false)]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '4', '4', '4' }, false)]
        [DataRow(new char[] { '1', '2', '3' }, new char[] { '1' }, true)]
        public void Char(char[] l1, char[] l2, bool outcome)
        {
            var ms1 = new MultiSet<char>(l1);
            var ms2 = new MultiSet<char>(l2);
            Assert.AreEqual(ms1.Overlaps(ms2), outcome);
        }

        [DataTestMethod]
        [DataRow(new string[] { "1" }, new string[] { "1", "2", "3" }, true)]
        [DataRow(new string[] { "1", "2", "3" }, new string[] { "1", "2", "3" }, true)]
        [DataRow(new string[] { "1", "2", "3" }, new string[] { "77" }, false)]
        public void String(string[] l1, string[] l2, bool outcome)
        {
            var ms1 = new MultiSet<string>(l1);
            var ms2 = new MultiSet<string>(l2);
            Assert.AreEqual(ms1.Overlaps(ms2), outcome);
        }
    }

    [TestClass] 
    public class MultiSetEquals
    {
        [DataTestMethod]
        [DataRow(new char[] { '1', '2' }, new char[] { '1', '2' }, true)]
        [DataRow(new char[] { '2', '2' }, new char[] { '2', '2' }, true)]
        [DataRow(new char[] {  }, new char[] {  }, true)]
        public void Char(char[] l1, char[] l2, bool outcome)
        {
            var ms1 = new MultiSet<char>(l1);
            Assert.AreEqual(outcome, ms1.MultiSetEquals(l2));
        }

        [DataTestMethod]
        [DataRow(new string[] { "1", "2" }, new string[] { "1", "2" }, true)]
        [DataRow(new string[] { "2", "2" }, new string[] { "2", "2" }, true)]
        [DataRow(new string[] { }, new string[] { }, true)]
        public void String(string[] l1, string[] l2, bool outcome)
        {
            var ms1 = new MultiSet<string>(l1);
            Assert.AreEqual(outcome, ms1.MultiSetEquals(l2));
        }
    }

    [TestClass]
    public class IsEmpty
    {
        [DataTestMethod]
        [DataRow(new char[] { }, true)]
        [DataRow(new char[] { 's' }, false)]
        public void Char(char[] l1, bool outcome)
        {
            var ms1 = new MultiSet<char>(l1);
            Assert.AreEqual(outcome, ms1.IsEmpty);
        }

        [DataTestMethod]
        [DataRow(new string[] { }, true)]
        [DataRow(new string[] { "a" }, false)]
        public void String(string[] l1, bool outcome)
        {
            var ms1 = new MultiSet<string>(l1);
            Assert.AreEqual(outcome, ms1.IsEmpty);
        }
    }

    [TestClass]
    public class Indexer
    {
        [DataTestMethod]
        [DataRow(new char[] { 'a', 'a', 'b' }, 'a', 2)]
        [DataRow(new char[] { 'a', 'a', 'b' }, 'g', 0)]
        [DataRow(new char[] { 'a', 'a', 'b' }, 'b', 1)]
        public void Char(char[] l1, char toFind, int expected)
        {
            var ms = new MultiSet<char>(l1);
            Assert.AreEqual(expected, ms[toFind]);
        }

        [DataTestMethod]
        [DataRow(new string[] { "a", "a", "b" }, "a", 2)]
        [DataRow(new string[] { "a", "a", "b" }, "g", 0)]
        [DataRow(new string[] { "a", "a", "b" }, "b", 1)]
        public void String(string[] l1, string toFind, int expected)
        {
            var ms = new MultiSet<string>(l1);
            Assert.AreEqual(expected, ms[toFind]);
        }
    }
}

namespace Arithmetics
{
    [TestClass]
    public class PlusSign
    {
        [DataTestMethod]
        [DataRow(new char[] { 'a' }, new char[] { 'b' }, new char[] { 'a', 'b' })]
        public void Char(char[] l1, char[] l2, char[] l3)
        {
            var ms1 = new MultiSet<char>(l1);
            var ms2 = new MultiSet<char>(l2);
            var ms3 = new MultiSet<char>(l3);

            var ms4 = ms1 + ms2;

            CollectionAssert.AreEqual(ms4.dict, ms3.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "a" }, new string[] { "b" }, new string[] { "a", "b" })]
        public void String(string[] l1, string[] l2, string[] l3)
        {
            var ms1 = new MultiSet<string>(l1);
            var ms2 = new MultiSet<string>(l2);
            var ms3 = new MultiSet<string>(l3);

            var ms4 = ms1 + ms2;

            CollectionAssert.AreEqual(ms4.dict, ms3.dict);
        }
    }

    [TestClass]
    public class MinusSign
    {
        [DataTestMethod]
        [DataRow(new char[] { 'a', 'b' }, new char[] { 'b' }, new char[] { 'a' })]
        public void Char(char[] l1, char[] l2, char[] l3)
        {
            var ms1 = new MultiSet<char>(l1);
            var ms2 = new MultiSet<char>(l2);
            var ms3 = new MultiSet<char>(l3);

            var ms4 = ms1 - ms2;

            CollectionAssert.AreEqual(ms4.dict, ms3.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "a", "b" }, new string[] { "b" }, new string[] { "a" })]
        public void String(string[] l1, string[] l2, string[] l3)
        {
            var ms1 = new MultiSet<string>(l1);
            var ms2 = new MultiSet<string>(l2);
            var ms3 = new MultiSet<string>(l3);

            var ms4 = ms1 - ms2;

            CollectionAssert.AreEqual(ms4.dict, ms3.dict);
        }
    }

    [TestClass]
    public class StarSign
    {
        [DataTestMethod]
        [DataRow(new char[] { 'a', 'c' }, new char[] { 'b', 'c' }, new char[] { 'c' })]
        public void Char(char[] l1, char[] l2, char[] l3)
        {
            var ms1 = new MultiSet<char>(l1);
            var ms2 = new MultiSet<char>(l2);
            var ms3 = new MultiSet<char>(l3);

            var ms4 = ms1 * ms2;

            CollectionAssert.AreEqual(ms4.dict, ms3.dict);
        }

        [DataTestMethod]
        [DataRow(new string[] { "a", "c" }, new string[] { "b", "c" }, new string[] { "c" })]
        public void String(string[] l1, string[] l2, string[] l3)
        {
            var ms1 = new MultiSet<string>(l1);
            var ms2 = new MultiSet<string>(l2);
            var ms3 = new MultiSet<string>(l3);

            var ms4 = ms1 * ms2;

            CollectionAssert.AreEqual(ms4.dict, ms3.dict);
        }
    }
}