using StudiosQuality.TaskRunnerAssignment.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace StudiosQuality.TaskRunnerAssignment
{
    public class TaskRunner : ITaskRunner
    {
        public void ExecuteTasks(IEnumerable<ITask> tasks)
        {
            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    if (task.Children != null && task.Children.Any())
                    {
                        ExecuteTasks(task.Children);
                    }

                    if (!task.Executed)
                    {
                        task.Execute();
                    }
                }
            }
        }
    }
}
