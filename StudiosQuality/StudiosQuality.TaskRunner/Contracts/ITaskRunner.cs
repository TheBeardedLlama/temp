using System.Collections.Generic;

namespace StudiosQuality.TaskRunnerAssignment.Contracts
{
    public interface ITaskRunner
    {
        void ExecuteTasks(IEnumerable<ITask> tasks);
    }
}
