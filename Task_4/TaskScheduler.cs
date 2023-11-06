using System;
using System.Collections.Generic;

namespace Task_4
{
    public class TaskScheduler<TTask, TPriority>
    {
        private List<Tuple<TTask, TPriority>> taskQueue = new List<Tuple<TTask, TPriority>>();
        private Func<TTask, TPriority> getPriorityFunc;
        private Func<TTask> initializeTaskFunc;
        private Action<TTask> resetTaskAction;
        private Dictionary<TPriority, string> priorityNames = new Dictionary<TPriority, string>();
        private List<TTask> taskPool = new List<TTask>();

        public TaskScheduler(
            Func<TTask, TPriority> getPriorityFunc,
            Func<TTask> initializeTaskFunc,
            Action<TTask> resetTaskAction)
        {
            this.getPriorityFunc = getPriorityFunc;
            this.initializeTaskFunc = initializeTaskFunc;
            this.resetTaskAction = resetTaskAction;
        }

        public void AddTask(TTask task, TPriority priority)
        {
            taskQueue.Add(Tuple.Create(task, priority));
            taskQueue.Sort((x, y) => Comparer<TPriority>.Default.Compare(x.Item2, y.Item2));
            Console.WriteLine($"Added task with priority: {GetPriorityName(priority)}");
        }

        public void ExecuteNext(TaskExecution<TTask> executionDelegate)
        {
            if (taskQueue.Count > 0)
            {
                Tuple<TTask, TPriority> nextTask = taskQueue[taskQueue.Count - 1];
                taskQueue.RemoveAt(taskQueue.Count - 1);
                TTask task = nextTask.Item1;
                executionDelegate(task);
            }
        }

        public TTask InitializeTask()
        {
            return taskPool.Count > 0 ? GetTaskFromPool() : initializeTaskFunc();
        }

        public void ResetTask(TTask task)
        {
            resetTaskAction(task);
        }

        public void AddTaskFromConsole()
        {
            Console.WriteLine("Enter a task:");
            TTask task = InitializeTask();
            Console.WriteLine("Enter priority (1 for Low, 2 for Medium, 3 for High):");
            if (int.TryParse(Console.ReadLine(), out int priorityNumber))
            {
                if (priorityNumber >= 1 && priorityNumber <= 3)
                {
                    AddTask(task, (TPriority)(object)priorityNumber);
                }
                else
                {
                    Console.WriteLine("Invalid priority. Task not added.");
                }
            }
            else
            {
                Console.WriteLine("Invalid priority. Task not added.");
            }
        }

        public void ExecuteAll(TaskExecution<TTask> executionDelegate)
        {
            while (taskQueue.Count > 0)
            {
                Tuple<TTask, TPriority> nextTask = taskQueue[taskQueue.Count - 1];
                taskQueue.RemoveAt(taskQueue.Count - 1);
                TTask task = nextTask.Item1;
                executionDelegate(task);
            }
        }

        public void SetPriorityName(TPriority priority, string name)
        {
            priorityNames[priority] = name;
        }

        public void ClearQueueAndResetPriorities()
        {
            taskQueue.Clear();
            priorityNames.Clear();
        }

        public void ClearQueue()
        {
            taskQueue.Clear();
        }

        public void ResetPriorities()
        {
            priorityNames.Clear();
        }

        public bool HasTasks()
        {
            return taskQueue.Count > 0;
        }

        public TPriority GetTaskPriority(TTask task)
        {
            return getPriorityFunc(task);
        }

        public TTask GetTaskFromPool()
        {
            if (taskPool.Count > 0)
            {
                TTask task = taskPool[taskPool.Count - 1];
                taskPool.RemoveAt(taskPool.Count - 1);
                return task;
            }
            return default(TTask);
        }

        public void ReturnTaskToPool(TTask task)
        {
            resetTaskAction(task);
            taskPool.Add(task);
        }

        private string GetPriorityName(TPriority priority)
        {
            if (priorityNames.TryGetValue(priority, out string name))
            {
                return name;
            }
            return priority.ToString();
        }
    }

    public delegate void TaskExecution<TTask>(TTask task);
}