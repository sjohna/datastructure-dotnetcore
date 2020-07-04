using NUnit.Framework;

using Datastructures;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TestDataStructures
{
    /**
        Tests that all unbounded IList implementations should pass.
    */
    [TestFixture(typeof(Vector<int>))]
    
    // run tests against .NET standard library collections to make sure behavior is consistent with new collections
    [TestFixture(typeof(List<int>))]

    // test traversible list implementations
    [TestFixture(typeof(TraversibleList<Vector<int>,int>))]
    [TestFixture(typeof(TraversibleList<List<int>,int>))]
    public class TestIListImplementation<List> where List : IList<int>, new()
    {
        private List m_list;

        [SetUp]
        public void Setup()
        {
            m_list = new List();
        }

        [Test]
        public void Create()
        {
            Assert.AreEqual(0, m_list.Count);
            Assert.IsFalse(m_list.IsReadOnly);
        }

        [Test]
        public void AddAndAccessSingleItem()
        {
            m_list.Add(7);
            Assert.AreEqual(7, m_list[0]);
        }

        [Test]
        public void IndexerGetThrowsExceptionOnEmptyList()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[0]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[-1]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[1]; });
        }

        [Test]
        public void IndexerGetOutOfRangeThrowsExceptionOnNonEmptyList()
        {
            m_list.Add(7);
            Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[1]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[-1]; });
        }

        [Test]
        public void SetIndexAfterAdding()
        {
            m_list.Add(7);
            m_list[0] = 9;
            Assert.AreEqual(1, m_list.Count);
            Assert.AreEqual(9, m_list[0]);
        }

        [Test]
        public void IndexerSetThrowsExceptionOnEmptyList()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[0] = 7; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[-1] = 7; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[1] = 7; });            
        }

        [Test]
        public void IndexerSetOutOfRangeThrowsExceptionOnNonEmptyList()
        {
            m_list.Add(7);
            Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[-1] = 7; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[1] = 7; });             
        }

        [Test]
        public void OtherItemsStillPresentAfterRemove()
        {
            m_list.Add(1);
            m_list.Add(2);
            m_list.Add(3);
            m_list.Remove(2);
            Assert.AreEqual(1, m_list[0]);
            Assert.AreEqual(3, m_list[1]);
        }

        [Test]
        public void EnumeratorReturnsItemsInCorrectOrder()
        {
            m_list.Add(1);
            m_list.Add(3);
            m_list.Add(2);

            var enumerator = m_list.GetEnumerator();

            enumerator.MoveNext();
            Assert.AreEqual(1, enumerator.Current);

            enumerator.MoveNext();
            Assert.AreEqual(3, enumerator.Current);

            enumerator.MoveNext();
            Assert.AreEqual(2, enumerator.Current);
        }

        [Test]
        public void RemoveRemovesFirstOccurrenceOfItemAndPreservesOthers()
        {
            m_list.Add(1);
            m_list.Add(2);
            m_list.Add(1);
            m_list.Add(3);

            m_list.Remove(1);
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {2,1,3}, m_list));
        }

        [Test]
        public void IndexOfItemsInList()
        {
            m_list.Add(1);
            m_list.Add(2);

            Assert.AreEqual(0, m_list.IndexOf(1));
            Assert.AreEqual(1, m_list.IndexOf(2));
        }

        [Test]
        public void IndexOfItemNotInList()
        {
            m_list.Add(1);
            m_list.Add(2);

            Assert.AreEqual(-1, m_list.IndexOf(3));
        }

        [Test]
        public void IndexOfOnEmptyList()
        {
            Assert.AreEqual(-1, m_list.IndexOf(1));
        }

        [Test]
        public void InsertSingleItem()
        {
            m_list.Insert(0,1);

            Assert.AreEqual(1, m_list.Count);
            Assert.AreEqual(1, m_list[0]);
        }

        [Test]
        public void InsertMultipleItems()
        {
            m_list.Insert(0,3);
            m_list.Insert(0,2);
            m_list.Insert(0,1);

            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3}, m_list));
        }

        [Test]
        public void InsertAtEndOfNonEmptyList()
        {
            m_list.Add(1);
            m_list.Add(2);
            m_list.Add(3);
            m_list.Insert(3,4);

            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3,4}, m_list));
        }

        [Test]
        public void InsertOutOfRangeInNonEmptyList()
        {
            m_list.Add(1);
            m_list.Add(2);
            m_list.Add(3);

            Assert.Throws<ArgumentOutOfRangeException>(() => { m_list.Insert(-1,4); });
            Assert.AreEqual(3, m_list.Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => { m_list.Insert(4,4); });
            Assert.AreEqual(3, m_list.Count);
        }

        [Test]
        public void RemoveAtStart()
        {
            m_list.Add(1);
            m_list.Add(2);
            m_list.Add(3);

            m_list.RemoveAt(0);
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {2,3}, m_list));
        }

        [Test]
        public void RemoveAtEnd()
        {
            m_list.Add(1);
            m_list.Add(2);
            m_list.Add(3);

            m_list.RemoveAt(2);
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2}, m_list));
        }

        [Test]
        public void RemoveAtMiddle()
        {
            m_list.Add(1);
            m_list.Add(2);
            m_list.Add(3);

            m_list.RemoveAt(1);
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,3}, m_list));
        }

        [Test]
        public void RemoveAtOutOfRange()
        {
            m_list.Add(1);
            m_list.Add(2);
            m_list.Add(3);

            Assert.Throws<ArgumentOutOfRangeException>(() => m_list.RemoveAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => m_list.RemoveAt(3));
        }

        [Test]
        public void IListScenario()
        {
            void DoScenario() 
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[0]; });
                Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[-1]; });
                Assert.Throws<ArgumentOutOfRangeException>(() => { var x = m_list[1]; });

                Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[0] = 7; });
                Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[-1] = 7; });
                Assert.Throws<ArgumentOutOfRangeException>(() => { m_list[1] = 7; }); 

                m_list.Add(1);
                m_list.Add(2);
                m_list.Add(3);
                Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3}, m_list));
                Assert.AreEqual(2, m_list.IndexOf(3));
                Assert.AreEqual(-1, m_list.IndexOf(4));
                Assert.AreEqual(1, m_list[0]);

                m_list.Remove(3);
                m_list.Remove(4);
                Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2}, m_list));
                Assert.AreEqual(-1, m_list.IndexOf(3));
                Assert.AreEqual(-1, m_list.IndexOf(4));
                Assert.AreEqual(1, m_list[0]);

                m_list.Add(3);
                m_list.Add(4);
                m_list.Add(5);
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
