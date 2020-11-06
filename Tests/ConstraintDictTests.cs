using System.Collections.Generic;
using NUnit.Framework;
using PiBa.Exceptions;
using PiBa.UI;
using PiBa.UI.Constraints;
using PiBa.UI.Factories;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.Tests
{
    [TestFixture]
    public class ConstraintDictTests
    {
        private ConstraintsDict cd;
        
        [SetUp]
        public void SetupBeforeEachTest()
        {
            //Some code
            cd = new ConstraintsDict();
        }
        
        [Test]
        public void GetConstraintsOf_Null_ThrowsError()
        {
            Assert.That(() => cd.GetConstraintsOf(null), Throws.ArgumentNullException);
        }

        [Test]
        public void GetConstraintsOf_NotRegistered_ThrowsError()
        {
            Assert.That(
                () => cd.GetConstraintsOf(new Tree<Widget>(WidgetFactory.CreateButton())) ,
                Throws.TypeOf<WidgetNotRegisteredException>());
        }
        
        [Test]
        public void GetConstraintsOf_Registered_ReturnsList()
        {
            var w = new Tree<Widget>(WidgetFactory.CreateButton());
            cd.Register(w);
            Assert.That(cd.GetConstraintsOf(w), Is.InstanceOf<List<IConstraint>>());
        }

        [Test]
        public void AddConstraint_Null_ThrowsError()
        {
            Assert.That(() => cd.AddConstraint(null, ConstraintFactory.CreateCenterConstraint()), Throws.ArgumentNullException);
            
            var w = new Tree<Widget>(WidgetFactory.CreateButton());
            cd.Register(w);
            Assert.That(() => cd.AddConstraint(w, null), Throws.ArgumentNullException);
        }
        
        public void AddConstraint_NotRegistered_ThrowsError()
        {
            var w = new Tree<Widget>(WidgetFactory.CreateButton());
            Assert.That(() => cd.AddConstraint(w, null), Throws.TypeOf<WidgetNotRegisteredException>());
        }

        [Test]
        public void AddConstraint_Valid_AddToList()
        {
            var w = new Tree<Widget>(WidgetFactory.CreateButton());
            cd.Register(w);
            Assert.That(cd.AddConstraint(w, ConstraintFactory.CreateCenterConstraint()).Count, Is.EqualTo(1));
        }
        
        [Test]
        public void Register_Null_Throws()
        {
            Assert.That(() => cd.Register(null), Throws.Exception );
        }
        
        [Test]
        public void Register_AlreadyPresent_Throws()
        {
            var t = new Tree<Widget>(WidgetFactory.CreateButton());
            cd.Register(t);
            Assert.That(() => cd.Register(t), Throws.Exception );
        }

        [Test]
        public void Register_Valid_AddsWidgetWithConstraintList()
        {
            var t = new Tree<Widget>(WidgetFactory.CreateButton());
            cd.Register(t);
            Assert.That(cd.GetConstraintsOf(t), Is.Empty);
        }
        
    }
}