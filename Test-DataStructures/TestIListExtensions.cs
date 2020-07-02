using NUnit.Framework;

using Datastructures;
using System.Collections.Generic;
using System.Linq;
using System;

using static Datastructures.IListExtensions;

namespace TestDataStructures
{
    /**
        Tests that all unbounded IList implementations should pass.
    */
    [TestFixture(typeof(Vector<int>))]
    
    // run tests against .NET standard library collections to make sure behavior is consistent with new collections
    [TestFixture(typeof(List<int>))]
    public class TestIListExtensions<List> where List : IList<int>, new()
    {
        private List m_list;

        [SetUp]
        public void Setup()
        {
            m_list = new List();
        }

        [Test]
        public void AddRangeWithItems()
        {
            m_list.AddRange(new int[] {1,2,3});
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3}, m_list));
        }

        [Test]
        public void AddEmptyRange()
        {
            var listToAdd = new List<int>();

            m_list.AddRange(listToAdd);

            Assert.AreEqual(0, m_list.Count);
        }

        [Test]
        public void RemoveNonEmptyRange()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});
            m_list.RemoveRange(1,2);
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,4,5}, m_list));
        }

        [Test]
        public void RemoveInvalidRange_IndexOutOfRange()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});
            Assert.Throws<ArgumentException>(() => m_list.RemoveRange(10,2));
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3,4,5}, m_list));
        }

        [Test]
        public void RemoveInvalidRange_CountOutOfRange()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});
            Assert.Throws<ArgumentException>(() => m_list.RemoveRange(3,10));
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3,4,5}, m_list));
        }

        [Test]
        public void IListExtensionsScenario()
        {
            void DoScenario() 
            {
                m_list.AddRange(new List<int>());
                Assert.AreEqual(0, m_list.Count);

                Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[0]; });
                Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[-1]; });
                Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[1]; });

                Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[0] = 7; });
                Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[-1] = 7; });
                Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[1] = 7; });

                m_list.AddRange(new int[] {1,2,3,4});
                Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3,4}, m_list));
                Assert.AreEqual(2, m_list.IndexOf(3));
                Assert.AreEqual(-1, m_list.IndexOf(5));
                Assert.AreEqual(1, m_list[0]);

                m_list.RemoveRange(2,2);
                Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2}, m_list));
                Assert.AreEqual(-1, m_list.IndexOf(3));
                Assert.AreEqual(-1, m_list.IndexOf(4));
                Assert.AreEqual(1, m_list[0]);

                m_list.AddRange(new int[] {3,4,5});
                Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3,4,5}, m_list));
                Assert.AreEqual(1, m_list[0]);
                Assert.AreEqual(3, m_list[2]);
                Assert.AreEqual(5, m_list[4]);

                m_list.AddRange(new List<int>());
                Assert.AreEqual(5, m_list.Count);
                Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3,4,5}, m_list));
                Assert.AreEqual(1, m_list[0]);
                Assert.AreEqual(3, m_list[2]);
                Assert.AreEqual(5, m_list[4]);

                m_list.RemoveAt(2);
                Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,4,5}, m_list));
                Assert.AreEqual(1, m_list[0]);
                Assert.AreEqual(5, m_list[3]);
                Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[4]; });

                m_list.Insert(2,3);
                Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3,4,5}, m_list));
                Assert.AreEqual(1, m_list[0]);

                m_list.Clear();
                
                for (int i = 0; i < 100; ++i)
                {
                    m_list.Add(100*i + 13);
                    Assert.AreEqual(m_list.Count-1, m_list.IndexOf(100*i + 13));
                }

                Assert.AreEqual(100, m_list.Count);

                for (int i = 0; i < m_list.Count; ++i)
                {
                    m_list[i] = i;
                }

                Assert.IsTrue(Enumerable.SequenceEqual(Enumerable.Range(0,100), m_list));
            }

            for (int i = 0; i < 5; ++i)
            {
                DoScenario();
                m_list.Clear();
            }
        }
    }
}
