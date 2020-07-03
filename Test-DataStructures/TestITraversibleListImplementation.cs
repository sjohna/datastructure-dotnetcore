using NUnit.Framework;

using Datastructures;
using System.Collections.Generic;
using System.Linq;
using System;

using static Datastructures.IListExtensions;

namespace TestDataStructures
{
    /**
        Tests that all unbounded ITraversibleList implementations that also implement IList should pass.
    */
    [TestFixture(typeof(TraversibleList<Vector<int>,int>))]
    [TestFixture(typeof(TraversibleList<List<int>,int>))]
    public class TestITraversibleListImplementation<List> where List : ITraversibleList<int>, IList<int>, new()
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

        private void AssertInvalidOperationsOnTraverserAtStartOrEnd(IListTraverser<int> traverser)
        {
            Assert.Throws<InvalidOperationException>(() => { var x = traverser.Index; });
            Assert.Throws<InvalidOperationException>(() => { var x = traverser.Element; });
            Assert.Throws<InvalidOperationException>(() => traverser.Element = 1);
            Assert.Throws<InvalidOperationException>(() => traverser.RemoveAt());
        }

        private void AssertStateOfTraverserOnEmptyList(IListTraverser<int> traverser)
        {
            Assert.IsTrue(traverser.AtStart);
            Assert.IsTrue(traverser.AtEnd);
            AssertInvalidOperationsOnTraverserAtStartOrEnd(traverser);
            Assert.Throws<InvalidOperationException>(() => traverser.ToNext());
            Assert.Throws<InvalidOperationException>(() => traverser.ToPrevious());  
        }

        [Test]
        public void TraverserOnEmptyList_State()
        {
            AssertStateOfTraverserOnEmptyList(m_list.Traverser());
        }

        [Test]
        public void StartTraverserOnEmptyList_State()
        {
            AssertStateOfTraverserOnEmptyList(m_list.StartTraverser());
        }

        [Test]
        public void EndTraverserOnEmptyList_State()
        {
            AssertStateOfTraverserOnEmptyList(m_list.EndTraverser());
        }

        [Test]
        public void GetFirstElementTraverserThrowsOnEmptyList()
        {
            Assert.Throws<InvalidOperationException>(() => m_list.FirstElementTraverser());
        }

        [Test]
        public void GetLastElementTraverserThrowsOnEmptyList()
        {
            Assert.Throws<InvalidOperationException>(() => m_list.LastElementTraverser());
        }

        private void AssertStateOfTraverserOnSingleElementList(IListTraverser<int> traverser)
        {
            // not asserting the value of the element, since that is test-case dependent
            Assert.IsFalse(traverser.AtStart);
            Assert.IsFalse(traverser.AtEnd);
            Assert.AreEqual(0, traverser.Index);          
        }

        [Test]
        public void TraverserOnListWithOneElement_State()
        {
            m_list.Add(1);
            var traverser = m_list.Traverser();
            AssertStateOfTraverserOnSingleElementList(traverser);
            Assert.AreEqual(1, traverser.Element);
        }

        [Test]
        public void FirstElementTraverserOnListWithOneElement_State()
        {
            m_list.Add(1);
            var traverser = m_list.FirstElementTraverser();
            AssertStateOfTraverserOnSingleElementList(traverser);
            Assert.AreEqual(1, traverser.Element);
        }

        [Test]
        public void LastElementTraverserOnListWithOneElement_State()
        {
            m_list.Add(1);
            var traverser = m_list.LastElementTraverser();
            AssertStateOfTraverserOnSingleElementList(traverser);
            Assert.AreEqual(1, traverser.Element);
        }

        [Test]
        public void EndTraverserOnListWithOneElement_State()
        {
            m_list.Add(1);
            var traverser = m_list.EndTraverser();
            Assert.IsFalse(traverser.AtStart);
            Assert.IsTrue(traverser.AtEnd);
            AssertInvalidOperationsOnTraverserAtStartOrEnd(traverser);
            Assert.Throws<InvalidOperationException>(() => traverser.ToNext());
        }

        [Test]
        public void StartTraverserOnListWithOneElement_State()
        {
            m_list.Add(1);
            var traverser = m_list.StartTraverser();
            Assert.IsTrue(traverser.AtStart);
            Assert.IsFalse(traverser.AtEnd);
            AssertInvalidOperationsOnTraverserAtStartOrEnd(traverser);
            Assert.Throws<InvalidOperationException>(() => traverser.ToPrevious());
        }

        private void TestReplaceAndInsertOnSingleElementList(IListTraverser<int> traverser)
        {
            traverser.Element = 7;
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {7}, m_list));
            Assert.AreEqual(7, traverser.Element);
            Assert.AreEqual(0, traverser.Index);

            traverser.InsertBefore(4);
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {4, 7}, m_list));
            Assert.AreEqual(7, traverser.Element);
            Assert.AreEqual(1, traverser.Index);

            traverser.InsertAfter(10);
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {4, 7, 10}, m_list));
            Assert.AreEqual(7, traverser.Element);
            Assert.AreEqual(1, traverser.Index);
        }

        [Test]
        public void TraverserOnListWithOneElement_ReplaceAndInsert()
        {
            m_list.Add(1);
            TestReplaceAndInsertOnSingleElementList(m_list.Traverser());
        }

        [Test]
        public void FirstElementTraverserOnListWithOneElement_ReplaceAndInsert()
        {
            m_list.Add(1);
            TestReplaceAndInsertOnSingleElementList(m_list.FirstElementTraverser());
        }

        [Test]
        public void LastElementTraverserOnListWithOneElement_ReplaceAndInsert()
        {
            m_list.Add(1);
            TestReplaceAndInsertOnSingleElementList(m_list.LastElementTraverser());
        }

        [Test]
        public void TraverserOnListWithOneElement_RemoveAt()
        {
            m_list.Add(1);
            var traverser = m_list.Traverser();
            traverser.RemoveAt();
            Assert.AreEqual(0, m_list.Count);

            AssertStateOfTraverserOnEmptyList(traverser);
        }

        [Test]
        public void FirstElementTraverserOnListWithOneElement_RemoveAt()
        {
            m_list.Add(1);
            var traverser = m_list.FirstElementTraverser();
            traverser.RemoveAt();
            Assert.AreEqual(0, m_list.Count);

            AssertStateOfTraverserOnEmptyList(traverser);
        }

        [Test]
        public void LastElementTraverserOnListWithOneElement_RemoveAt()
        {
            m_list.Add(1);
            var traverser = m_list.LastElementTraverser();
            traverser.RemoveAt();
            Assert.AreEqual(0, m_list.Count);

            AssertStateOfTraverserOnEmptyList(traverser);
        }

        private void TestTraverserForwardTraversal(IListTraverser<int> traverser)
        {
            for(int i = 0; i <5; ++i)
            {
                Assert.AreEqual(i, traverser.Index);
                Assert.AreEqual(i+1, traverser.Element);
                traverser.ToNext();
            }
            Assert.IsTrue(traverser.AtEnd);
            AssertInvalidOperationsOnTraverserAtStartOrEnd(traverser);
            Assert.Throws<InvalidOperationException>(() => traverser.ToNext());
        }

        private void TestTraverserBackwardTraversal(IListTraverser<int> traverser)
        {
            for(int i = 4; i >= 0; --i)
            {
                Assert.AreEqual(i, traverser.Index);
                Assert.AreEqual(i+1, traverser.Element);
                traverser.ToPrevious();
            }
            Assert.IsTrue(traverser.AtStart);
            AssertInvalidOperationsOnTraverserAtStartOrEnd(traverser);
            Assert.Throws<InvalidOperationException>(() => traverser.ToPrevious());
        }

        [Test]
        public void Traverser_Traversal()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});

            var traverser = m_list.Traverser();

            TestTraverserForwardTraversal(traverser);
            traverser.ToPrevious();
            TestTraverserBackwardTraversal(traverser);
        }

        [Test]
        public void FirstElementTraverser_Traversal()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});

            var traverser = m_list.FirstElementTraverser();

            TestTraverserForwardTraversal(traverser);
            traverser.ToPrevious();
            TestTraverserBackwardTraversal(traverser);
        }

        [Test]
        public void LastElementTraverser_Traversal()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});

            var traverser = m_list.LastElementTraverser();

            TestTraverserBackwardTraversal(traverser);
            traverser.ToNext();
            TestTraverserForwardTraversal(traverser);
        }

        [Test]
        public void StartTraverser_Traversal()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});

            var traverser = m_list.StartTraverser();

            traverser.ToNext();
            TestTraverserForwardTraversal(traverser);
            traverser.ToPrevious();
            TestTraverserBackwardTraversal(traverser);
        }

        [Test]
        public void EndTraverser_Traversal()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});

            var traverser = m_list.EndTraverser();

            traverser.ToPrevious();
            TestTraverserBackwardTraversal(traverser);
            traverser.ToNext();
            TestTraverserForwardTraversal(traverser);
        }

        [Test]
        public void RemoveTwiceThrowsException()
        {
            m_list.AddRange(new int[] {1,2,3});

            var traverser = m_list.FirstElementTraverser();

            traverser.RemoveAt();
            Assert.Throws<InvalidOperationException>(() => traverser.RemoveAt());
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {2,3}, m_list));
        }

        [Test]
        public void TraverseToNextAfterRemove()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});
            var traverser = m_list.FirstElementTraverser();
            traverser.ToNext();
            traverser.RemoveAt();
            traverser.ToNext();
            Assert.AreEqual(1, traverser.Index);
        }

        [Test]
        public void TraverseToPreviousAfterRemove()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});
            var traverser = m_list.FirstElementTraverser();
            traverser.ToNext();
            traverser.RemoveAt();
            traverser.ToPrevious();
            Assert.AreEqual(0, traverser.Index);
        }

        [Test]
        public void StateAfterRemove()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});
            var traverser = m_list.FirstElementTraverser();
            traverser.ToNext();
            traverser.RemoveAt();

            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,3,4,5}, m_list));
            Assert.AreEqual(2,traverser.Element);
            Assert.Throws<InvalidOperationException>(() => {var x = traverser.Index;});
            Assert.Throws<InvalidOperationException>(() => {traverser.Element = 7;});
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,3,4,5}, m_list));        
        }

        [Test]
        public void InsertAfterRemove()
        {
            m_list.AddRange(new int[] {1,2,3,4,5});
            var traverser = m_list.FirstElementTraverser();
            traverser.ToNext();
            traverser.RemoveAt();

            traverser.InsertBefore(10);
            traverser.InsertBefore(20);
            traverser.InsertAfter(30);

            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,10,20,30,3,4,5}, m_list));
            Assert.AreEqual(2,traverser.Element);
            Assert.Throws<InvalidOperationException>(() => {var x = traverser.Index;});
            Assert.Throws<InvalidOperationException>(() => {traverser.Element = 7;});
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,10,20,30,3,4,5}, m_list));           
        }

        [Test]
        public void ITraversibleListScenario()
        {
            void DoScenario() 
            {
                TraverserOnEmptyList_State();
                StartTraverserOnEmptyList_State();
                EndTraverserOnEmptyList_State();

                m_list.AddRange(Enumerable.Range(1,10));

                for (var traverser = m_list.Traverser(); !traverser.AtEnd; traverser.ToNext())
                {
                    if (traverser.Element % 2 == 0) traverser.Element *= 10;
                }

                Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,20,3,40,5,60,7,80,9,100}, m_list));

                for (var traverser = m_list.Traverser(); !traverser.AtEnd; traverser.ToNext())
                {
                    /**
                        The following line causes the test to fail: after removing the last element of the list, we are at the end,
                        and then ToNext() throws when the loop increment executes. 

                        TODO: mutation testing with my other tests at this point, to determine whether I should have caught it, or whether
                        this test kills any new mutants.
                    */
                    if (traverser.Element >= 10) traverser.RemoveAt();
                    else 
                    {
                        traverser.Element += 1;
                        traverser.InsertBefore(traverser.Element - 1);
                    }
                }

                Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2,3,4,5,6,7,8,9,10}, m_list));
            }

            for (int i = 0; i < 5; ++i)
            {
                DoScenario();
                m_list.Clear();
            }
        }
    }
}
