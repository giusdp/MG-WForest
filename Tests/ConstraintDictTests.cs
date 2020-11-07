using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
        private ConstraintsDict _cd;
        private Widget _w;
        
        [SetUp]
        public void SetupBeforeEachTest()
        {
            //Some code
            _cd = new ConstraintsDict();
            _w = WidgetFactory.CreateContainer(Rectangle.Empty);
        }
        
        [Test]
        public void GetConstraintsOf_Null_ThrowsError()
        {
            Assert.That(() => _cd.GetConstraintsOf(null), Throws.ArgumentNullException);
        }

        [Test]
        public void GetConstraintsOf_NotRegistered_ThrowsError()
        {
            Assert.That(
                () => _cd.GetConstraintsOf(new Tree<Widget>(_w)),
                Throws.TypeOf<WidgetNotRegisteredException>()
                );
        }
        
        [Test]
        public void GetConstraintsOf_Registered_ReturnsList()
        {
            var t = new Tree<Widget>(_w);
            _cd.Register(t);
            Assert.That(_cd.GetConstraintsOf(t), Is.InstanceOf<List<IConstraint>>());
        }

        [Test]
        public void AddConstraint_Null_ThrowsError()
        {
            Assert.That(() => _cd.AddConstraint(null, ConstraintFactory.CreateCenterConstraint()), Throws.ArgumentNullException);
            
            var t = new Tree<Widget>(_w);
            _cd.Register(t);
            Assert.That(() => _cd.AddConstraint(t, null), Throws.ArgumentNullException);
        }
        
        public void AddConstraint_NotRegistered_ThrowsError()
        {
            var t = new Tree<Widget>(_w);
            Assert.That(() => _cd.AddConstraint(t, null), Throws.TypeOf<WidgetNotRegisteredException>());
        }

        [Test]
        public void AddConstraint_Valid_AddToList()
        {
            var t = new Tree<Widget>(_w);
            _cd.Register(t);
            Assert.That(_cd.AddConstraint(t, ConstraintFactory.CreateCenterConstraint()).Count, Is.EqualTo(1));
        }
        
        [Test]
        public void Register_Null_Throws()
        {
            Assert.That(() => _cd.Register(null), Throws.Exception );
        }
        
        [Test]
        public void Register_AlreadyPresent_Throws()
        {
            var t = new Tree<Widget>(_w);
            _cd.Register(t);
            Assert.That(() => _cd.Register(t), Throws.Exception );
        }

        [Test]
        public void Register_Valid_AddsWidgetWithConstraintList()
        {
            var t = new Tree<Widget>(_w);
            _cd.Register(t);
            Assert.That(_cd.GetConstraintsOf(t), Is.Empty);
        }
        
    }
}