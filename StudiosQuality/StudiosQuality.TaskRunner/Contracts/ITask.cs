using System.Collections.Generic;

namespace StudiosQuality.TaskRunnerAssignment.Contracts
{
    public interface ITask
    {
        IEnumerable<ITask> Children { get; set; }

        bool Executed { get; }

        string Name { get; set; }

        void Execute();
    }
}
