using Moq;
using StudiosQuality.TaskRunnerAssignment.Contracts;
using System.Collections.Generic;
using Xunit;

namespace StudiosQuality.TaskRunnerAssignment.Tests
{
    public class TaskRunnerTests
    {
        Mock<ITask> _taskE;
        Mock<ITask> _taskC;
        Mock<ITask> _taskD;
        Mock<ITask> _taskB;
        Mock<ITask> _taskA;

        public TaskRunnerTests()
        {
            _taskE = new Mock<ITask>(MockBehavior.Strict);
            _taskE.Setup(x => x.Children).Returns((IEnumerable<ITask>)null);
            _taskE.SetupGet(x => x.Executed).Returns(false);
            _taskE.SetupGet(x => x.Name).Returns(nameof(_taskE));
            _taskE.Setup(x => x.Execute()).Callback(() => _taskE.SetupGet(y => y.Executed).Returns(true));

            _taskC = new Mock<ITask>(MockBehavior.Strict);
            _taskC.Setup(x => x.Children).Returns(new[] { _taskE.Object });
            _taskC.SetupGet(x => x.Executed).Returns(false);
            _taskC.SetupGet(x => x.Name).Returns(nameof(_taskC));
            _taskC.Setup(x => x.Execute()).Callback(() => _taskC.SetupGet(y => y.Executed).Returns(true));

            _taskD = new Mock<ITask>(MockBehavior.Strict);
            _taskD.Setup(x => x.Children).Returns((IEnumerable<ITask>)null);
            _taskD.SetupGet(x => x.Executed).Returns(false);
            _taskD.SetupGet(x => x.Name).Returns(nameof(_taskD));
            _taskD.Setup(x => x.Execute()).Callback(() => _taskD.SetupGet(y => y.Executed).Returns(true));

            _taskB = new Mock<ITask>(MockBehavior.Strict);
            _taskB.Setup(x => x.Children).Returns(new[] { _taskE.Object, _taskD.Object });
            _taskB.SetupGet(x => x.Executed).Returns(false);
            _taskB.SetupGet(x => x.Name).Returns(nameof(_taskB));
            _taskB.Setup(x => x.Execute()).Callback(() => _taskB.SetupGet(y => y.Executed).Returns(true));

            _taskA = new Mock<ITask>(MockBehavior.Strict);
            _taskA.Setup(x => x.Children).Returns(new[] { _taskC.Object, _taskB.Object });
            _taskA.SetupGet(x => x.Executed).Returns(false);
            _taskA.SetupGet(x => x.Name).Returns(nameof(_taskA));
            _taskA.Setup(x => x.Execute()).Callback(() => _taskA.SetupGet(y => y.Executed).Returns(true));
        }

        [Fact]
        public void RunTaskE()
        {
            var taskRunner = new TaskRunner();
            taskRunner.ExecuteTasks(new[] { _taskE.Object });

            Assert.True(_taskE.Object.Executed);
            _taskE.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void RunTaskC()
        {
            var taskRunner = new TaskRunner();
            taskRunner.ExecuteTasks(new[] { _taskC.Object });

            Assert.True(_taskE.Object.Executed);
            _taskE.Verify(x => x.Execute(), Times.Once);
            Assert.True(_taskC.Object.Executed);
            _taskC.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void RunTaskB()
        {
            var taskRunner = new TaskRunner();
            taskRunner.ExecuteTasks(new[] { _taskB.Object });

            Assert.True(_taskE.Object.Executed);
            _taskE.Verify(x => x.Execute(), Times.Once);
            Assert.True(_taskD.Object.Executed);
            _taskD.Verify(x => x.Execute(), Times.Once);
            Assert.True(_taskB.Object.Executed);
            _taskB.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void RunTaskA()
        {
            var taskRunner = new TaskRunner();
            taskRunner.ExecuteTasks(new[] { _taskA.Object });

            Assert.True(_taskE.Object.Executed);
            _taskE.Verify(x => x.Execute(), Times.Once);
            Assert.True(_taskD.Object.Executed);
            _taskD.Verify(x => x.Execute(), Times.Once);
            Assert.True(_taskB.Object.Executed);
            _taskB.Verify(x => x.Execute(), Times.Once);
            Assert.True(_taskA.Object.Executed);
            _taskA.Verify(x => x.Execute(), Times.Once);
        }
    }
}
