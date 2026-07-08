using Program.Model;

namespace LRUCache.Tests.Model
{
    public class LRUCacheTest
    {
        private LRUCache<string> _object; 

        [SetUp]
        public void SetUp()
        {
            _object = new LRUCache<string>();
        }

        [Test]
        public void LRUCache_Should_StoreOnlyFiveElements()
        {
            // Arrange
            var elementList = new List<string>()
            {
                "Create", "Replace", "Undo", "Add Text", "Change Title", // 5
                "Remove Title", "Change Title Style"
            };

            // Act
            elementList.ForEach(_object.PutValue);

            // Assert
           Assert.That(_object.GetDictionaryCount(), Is.EqualTo(5));
           Assert.That(_object.GetListCount(), Is.EqualTo(5));
        }

        [Test]
        public void LRUCache_Should_MoveElementFirstWhenPulled_1()
        {
            // Arrange
            var elementList = new List<string>()
            {
                "Create", "Replace", "Undo", "Add Text", "Change Title", // 5
            };

            // Act
            elementList.ForEach(_object.PutValue);
            _object.TryGetValue("Replace", out _);

            // Assert
            Assert.That(_object.GetFirstValue(), Is.EqualTo("Replace"));
            Assert.That(_object.GetLastValue(), Is.EqualTo("Create"));
        }

        [Test]
        public void LRUCache_Should_MoveElementFirstWhenPulled_2()
        {
            // Arrange
            var elementList = new List<string>()
            {
                "Create", "Replace", "Undo", "Add Text", "Change Title", // 5
            };

            // Act
            elementList.ForEach(_object.PutValue);
            _object.TryGetValue("Add Text", out var addTextReference);


            // Assert
            Assert.That(_object.GetFirstValue(), Is.EqualTo("Add Text"));
            Assert.That(addTextReference!.Next!.Value, Is.EqualTo("Change Title"));
            Assert.That(addTextReference!.Previous, Is.Null);
        }

        [Test]
        public void LRUCache_Should_ReceiveTheSameValueMultipleTimes()
        {
            // Arrange
            var elementList = new List<string>()
            {
                "Create", "Replace", "Undo", "Add Text", "Change Title", // 5
            };

            // Act
            elementList.ForEach(_object.PutValue);
            _object.PutValue("Create");


            // Assert
            Assert.That(_object.GetFirstValue(), Is.EqualTo("Create"));
            Assert.That(_object.GetLastValue(), Is.EqualTo("Replace"));
        }
    }
}
